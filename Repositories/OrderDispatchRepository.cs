using CNF.Business.BusinessConstant;
using CNF.Business.Model.ChequeAccounting;
using CNF.Business.Model.Context;
using CNF.Business.Model.Login;
using CNF.Business.Model.OrderDispatch;
using CNF.Business.Repositories.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace CNF.Business.Repositories
{
    public class OrderDispatchRepository : IOrderDispatchRepository
    {
        private CFADBEntities _contextManager;
        private LoginRepository _loginRepository;
        //Inject DB Context instance from Unit of Work to avoid unwanted memory allocations.
        public OrderDispatchRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }

        #region PickListHeader list & AddEdit
        public string PickListHeaderAddEdit(PickListModel model)
        {
            return PickListHeaderAddEditPvt(model);
        }
        private string PickListHeaderAddEditPvt(PickListModel model)
        {

            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_PickListHeaderAddEdit(model.Picklistid, model.BranchId, model.CompId, model.PicklistDate, model.FromInv, model.ToInv, model.PicklistStatus, model.VerifiedBy, model.RejectReason, model.IsStockTransfer, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PickListHeaderAddEditPvt", "Picklist AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Get Picklist By BranchId and CompanyId
        public List<PickListModel> GetPickLst(int BranchId, int CompId, DateTime PicklistDate)
        {
            return GetPickLstPvt(BranchId, CompId, PicklistDate);
        }
        private List<PickListModel> GetPickLstPvt(int BranchId, int CompId, DateTime PicklistDate)
        {
            List<PickListModel> PckList = new List<PickListModel>();

            try
            {
                PckList = _contextManager.usp_GetPickList(BranchId, CompId, PicklistDate).Select(c => new PickListModel
                {
                    BranchId = c.BranchId,
                    CompId = c.CompId,
                    Picklistid = c.Picklistid,
                    PicklistNo = c.PicklistNo,
                    PicklistDate = DateTime.Parse(c.PicklistDate.ToString()),
                    FromInv = c.FromInv,
                    ToInv = c.ToInv,
                    PicklistStatus = Convert.ToInt32(c.PicklistStatus),
                    VerifiedBy = Convert.ToInt32(c.VerifiedBy),
                    StatusText = c.StatusText,
                    OnPriority = c.OnPriority
                }).OrderByDescending(x => x.OnPriority).ToList();

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickLstPvt", "Get Picklist By BranchId and CompanyId List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PckList;
        }
        #endregion

        #region Send Email to Stockist
        public string sendEmail(string EmailId, string PicklistNo)
        {
            bool bResult = false;
            string msg = string.Empty, messageformat = string.Empty, Date = string.Empty, Subject = string.Empty, MailFilePath = string.Empty, CCEmail = string.Empty;
            EmailSend Email = new EmailSend();
            try
            {
                Date = DateTime.Today.Date.ToString("dd-MM-yyyy");
                CCEmail = ConfigurationManager.AppSettings["ToEmail"];
                Subject = ConfigurationManager.AppSettings["StockistSubject"] + Date + " ";
                msg = ConfigurationManager.AppSettings["Message"] + PicklistNo;
                MailFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailFile\\SendEmailToStockiest.html");
                bResult = EmailNotification.SendEmails(EmailId, CCEmail, Subject, msg, MailFilePath);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "sendEmail", "Send Email to Stockist", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (bResult == true)
            {
                return Email.Status = BusinessCont.SuccessStatus;
            }
            else
            {
                return Email.Status = BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Send Email To Picker
        public string sendEmailToPicker(string EmailId, string PicklistNo)
        {
            bool bResult = false;
            string msg = string.Empty, messageformat = string.Empty, Date = string.Empty, Subject = string.Empty, MailFilePath = string.Empty, CCEmail = string.Empty;
            EmailSend Email = new EmailSend();
            try
            {
                Date = DateTime.Today.Date.ToString("dd-MM-yyyy");
                CCEmail = ConfigurationManager.AppSettings["ToEmail"];
                Subject = ConfigurationManager.AppSettings["PickerSubject"] + Date + " ";
                msg = ConfigurationManager.AppSettings["Message"] + PicklistNo;
                MailFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailFile\\SendEmailToPicker.html");
                bResult = EmailNotification.SendEmails(EmailId, CCEmail, Subject, msg, MailFilePath);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "sendEmailToPicker", "Send Email To Picker", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (bResult == true)
            {
                return Email.Status = BusinessCont.SuccessStatus;
            }
            else
            {
                return Email.Status = BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Picklist Allotment Add
        public string PicklistAllotmentAdd(PicklstAllotReallotModel model)
        {
            return PicklistAllotmentAddPvt(model);
        }
        private string PicklistAllotmentAddPvt(PicklstAllotReallotModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            _loginRepository = new LoginRepository(_contextManager);
            try
            {
                RetValue = _contextManager.usp_PicklistAllotmentAdd(model.BranchId, model.CompId, model.Picklistid, model.PicklistDtlsId, model.ProductCode, model.BatchNo, model.PickerId,
                    model.AllottedBy, obj);
                if (RetValue > 0)
                {
                    string[] values = model.PickerId.Split(',');
                    for (var i = 0; i < values.Length - 1; i++)
                    {
                        _loginRepository.SendNotification(model.BranchId, Convert.ToInt32(values[i]), BusinessCont.PickListNotificationHeader, BusinessCont.PickListNotificationMsg, BusinessCont.PayLoadPicker);
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PicklistAllotmentAddPvt", "Picklist Allotment Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Picklist ReAllotment Add
        public string PicklistReAllotmentAdd(PicklstAllotReallotModel model)
        {
            return PicklistReAllotmentAddPvt(model);
        }
        private string PicklistReAllotmentAddPvt(PicklstAllotReallotModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            _loginRepository = new LoginRepository(_contextManager);

            try
            {
                RetValue = _contextManager.usp_PicklistReAllotmentAdd(model.BranchId, model.CompId, model.Picklistid, model.PicklistDtlsId, model.AllotmentId, model.ProductCode, model.BatchNo, model.PickerId,
                    model.ReAllottedBy, obj);

                if (RetValue > 0)
                {
                    string[] values = model.PickerId.Split(',');
                    for (var i = 0; i < values.Length - 1; i++)
                    {
                        _loginRepository.SendNotification(model.BranchId, Convert.ToInt32(values[i]), BusinessCont.PickListNotificationHeader, BusinessCont.PickListNotificationMsg, "Picker");
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PicklistReAllotmentAddPvt", "Picklist ReAllotment Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Picklist Allotment Status
        public string PicklistAllotmentStatus(PicklistAllotmentStatusModel model)
        {
            return PicklistAllotmentStatusPvt(model);
        }
        private string PicklistAllotmentStatusPvt(PicklistAllotmentStatusModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_PicklistAllotmentStatus(model.AllotmentId, model.Picklistid, model.BranchId, model.AllotmentStatus, model.ReasonId, model.RejectRemark, model.UserId, model.StatusTime, obj);  //pass pickerid to userid
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PicklistAllotmentStatusPvt", "Picklist Allotment Status", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Get InvoiceHeaderList
        public List<InvoiceLstModel> GetInvoiceHeaderLst(int BranchId, int CompId, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate, int BillDrawerId, Nullable<int> InvStatus)
        {
            return GetInvoiceHeaderLstPvt(BranchId, CompId, FromDate, ToDate, BillDrawerId, InvStatus);
        }
        private List<InvoiceLstModel> GetInvoiceHeaderLstPvt(int BranchId, int CompId, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate, int BillDrawerId, Nullable<int> InvStatus)
        {
            List<InvoiceLstModel> InvoiceLst = new List<InvoiceLstModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    InvoiceLst = _contextManager.usp_InvoiceHeaderList(BranchId, CompId, FromDate, ToDate, BillDrawerId, InvStatus).Select(c => new InvoiceLstModel
                    {
                        InvId = c.InvId,
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        InvNo = c.InvNo,
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate),
                        IsColdStorage = c.IsColdStorage,
                        SoldTo_StokistId = Convert.ToInt32(c.SoldTo_StokistId),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        SoldTo_City = c.SoldTo_City,
                        CityName = c.CityName,
                        InvAmount = Convert.ToDouble(c.InvAmount),
                        InvStatus = Convert.ToInt32(c.InvStatus),
                        StatusText = c.StatusText,
                        CityCode = c.CityCode,
                        AcceptedBy = Convert.ToInt32(c.AcceptedBy),
                        BillDrawerName = c.BillDrawerName,
                        PackedByName = c.PackedByName,
                        PackedDateNewFormatForDayDiff = Convert.ToDateTime(c.PackedDate).ToString("dd"),
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                        InvWeight = Convert.ToInt32(c.InvWeight),
                        IsCourier = Convert.ToInt32(c.IsCourier),
                        PackingConcernText = c.PackingConcernText,
                        PackingRemark = c.PackingRemark,
                        ReadyToDispatchRemark = c.ReadyToDispatchRemark,
                        TotalInv = Convert.ToInt32(c.TotalInv),
                        OnPriority = Convert.ToInt32(c.OnPriority),
                        IsStockTransfer = c.IsStockTransfer,
                        Addedby = c.Addedby,
                        AddedOn = Convert.ToDateTime(c.AddedOn),
                        LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn)

                        //PrintStatus = c.PrintStatus,
                        //CancelBy = Convert.ToInt32(c.CancelBy),
                        //CancelDate = Convert.ToDateTime(c.CancelDate),
                        //ReadyToDispatchConcernId = Convert.ToInt32(c.ReadyToDispatchConcernId),
                        //ReadyToDispatchDateNewFormanCnt = Convert.ToDateTime(c.ReadyToDispatchDate).ToString("dd-MM-yyyy"),
                        //ReadyToDispatchBy = Convert.ToInt32(c.ReadyToDispatchBy),
                        //PackingConcernId = Convert.ToInt32(c.PackingConcernId),
                        //PackingConcernDate = Convert.ToDateTime(c.PackingConcernDate),
                        //PackedDate = Convert.ToDateTime(c.PackedDate),
                        //PackedDateNewFormatForFilter = Convert.ToDateTime(c.PackedDate).ToString("yyyy-MM-dd"),
                        //PackedBy = Convert.ToInt32(c.PackedBy),
                        //BillDrawnDate = Convert.ToDateTime(c.BillDrawnDate),
                        //AcceptedDate = Convert.ToDateTime(c.AcceptedDate),
                        //InvCreatedDateNewFormat = Convert.ToDateTime(c.InvCreatedDate).ToString("yyyy-MM-dd"),
                    }).OrderByDescending(x => x.OnPriority).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceHeaderLstPvt", "Get Invoice Header List " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Get Invoice Header List For Priority
        public List<InvoiceLstModel> GetInvoiceHeaderListForPriority(int BranchId, int CompId)
        {
            return GetInvoiceHeaderListForPriorityPvt(BranchId, CompId);
        }
        private List<InvoiceLstModel> GetInvoiceHeaderListForPriorityPvt(int BranchId, int CompId)
        {
            List<InvoiceLstModel> InvoiceLst = new List<InvoiceLstModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    InvoiceLst = _contextManager.usp_GetInvoiceHeaderListForPriority(BranchId, CompId).Select(c => new InvoiceLstModel
                    {
                        InvId = c.InvId,
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        InvNo = c.InvNo,
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate),
                        IsColdStorage = c.IsColdStorage,
                        SoldTo_StokistId = Convert.ToInt32(c.SoldTo_StokistId),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        SoldTo_City = c.SoldTo_City,
                        CityName = c.CityName,
                        InvAmount = Convert.ToDouble(c.InvAmount),
                        InvStatus = Convert.ToInt32(c.InvStatus),
                        StatusText = c.StatusText,
                        CityCode = c.CityCode,
                        AcceptedBy = Convert.ToInt32(c.AcceptedBy),
                        BillDrawerName = c.BillDrawerName,
                        PackedByName = c.PackedByName,
                        PackedDateNewFormatForDayDiff = Convert.ToDateTime(c.PackedDate).ToString("dd"),
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                        InvWeight = Convert.ToInt32(c.InvWeight),
                        IsCourier = Convert.ToInt32(c.IsCourier),
                        PackingConcernText = c.PackingConcernText,
                        PackingRemark = c.PackingRemark,
                        ReadyToDispatchRemark = c.ReadyToDispatchRemark,
                        TotalInv = Convert.ToInt32(c.TotalInv),
                        OnPriority = Convert.ToInt32(c.OnPriority),
                        IsStockTransfer = c.IsStockTransfer,
                        Addedby = c.Addedby,
                        AddedOn = Convert.ToDateTime(c.AddedOn),
                        LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn)
                    }).OrderByDescending(x => x.OnPriority).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceHeaderLstPvt", "Get Invoice Header List " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Get Invoice Header List for Mob
        public List<InvoiceLstModel> GetInvoiceHeaderLstForMob(int BranchId, int CompId, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate, int BillDrawerId, Nullable<int> InvStatus)
        {
            return GetInvoiceHeaderLstForMobPvt(BranchId, CompId, FromDate, ToDate, BillDrawerId, InvStatus);
        }
        private List<InvoiceLstModel> GetInvoiceHeaderLstForMobPvt(int BranchId, int CompId, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate, int BillDrawerId, Nullable<int> InvStatus)
        {
            List<InvoiceLstModel> InvoiceLst = new List<InvoiceLstModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    InvoiceLst = _contextManager.usp_InvoiceHeaderListforMob(BranchId, CompId, FromDate, ToDate, BillDrawerId, InvStatus).Select(c => new InvoiceLstModel
                    {
                        InvId = c.InvId,
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        InvNo = c.InvNo,
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate),
                        InvCreatedDateNewFormat = Convert.ToDateTime(c.InvCreatedDate).ToString("yyyy-MM-dd"),
                        IsColdStorage = c.IsColdStorage,
                        SoldTo_StokistId = Convert.ToInt32(c.SoldTo_StokistId),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        SoldTo_City = c.SoldTo_City,
                        CityName = c.CityName,
                        InvAmount = Convert.ToDouble(c.InvAmount),
                        InvStatus = Convert.ToInt32(c.InvStatus),
                        StatusText = c.StatusText,
                        Addedby = c.Addedby,
                        AddedOn = Convert.ToDateTime(c.AddedOn),
                        LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn),
                        CityCode = c.CityCode,
                        AcceptedBy = Convert.ToInt32(c.AcceptedBy),
                        BillDrawerName = c.BillDrawerName,
                        AcceptedDate = Convert.ToDateTime(c.AcceptedDate),
                        BillDrawnDate = Convert.ToDateTime(c.BillDrawnDate),
                        PackedBy = Convert.ToInt32(c.PackedBy),
                        PackedByName = c.PackedByName,
                        PackedDate = Convert.ToDateTime(c.PackedDate),
                        PackedDateNewFormatForFilter = Convert.ToDateTime(c.PackedDate).ToString("yyyy-MM-dd"),
                        PackedDateNewFormatForDayDiff = Convert.ToDateTime(c.PackedDate).ToString("dd"),
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                        InvWeight = Convert.ToInt32(c.InvWeight),
                        IsCourier = Convert.ToInt32(c.IsCourier),
                        PackingConcernDate = Convert.ToDateTime(c.PackingConcernDate),
                        PackingConcernId = Convert.ToInt32(c.PackingConcernId),
                        PackingConcernText = c.PackingConcernText,
                        PackingRemark = c.PackingRemark,
                        ReadyToDispatchBy = Convert.ToInt32(c.ReadyToDispatchBy),
                        ReadyToDispatchDateNewFormanCnt = Convert.ToDateTime(c.ReadyToDispatchDate).ToString("dd-MM-yyyy"),
                        ReadyToDispatchConcernId = Convert.ToInt32(c.ReadyToDispatchConcernId),
                        ReadyToDispatchRemark = c.ReadyToDispatchRemark,
                        CancelBy = Convert.ToInt32(c.CancelBy),
                        CancelDate = Convert.ToDateTime(c.CancelDate),
                        TotalInv = Convert.ToInt32(c.TotalInv),
                        OnPriority = Convert.ToInt32(c.OnPriority),
                        IsStockTransfer = c.IsStockTransfer,
                        PrintStatus = c.PrintStatus
                    }).OrderByDescending(x => x.OnPriority).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceHeaderLstPvtForMob", "Get Invoice Header List For Mob " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Get Alloted Picklist For Picker
        public List<Picklstmodel> GetAllotedPickListForPicker(int BranchId, int CompId, int PickerId, DateTime PicklistDate)
        {
            return GetAllotedPickListForPickerPvt(BranchId, CompId, PickerId, PicklistDate);
        }
        private List<Picklstmodel> GetAllotedPickListForPickerPvt(int BranchId, int CompId, int PickerId, DateTime PicklistDate)
        {
            List<Picklstmodel> picklstmodels = new List<Picklstmodel>();
            try
            {
                picklstmodels = _contextManager.usp_GetAllotedPickListForPicker(BranchId, CompId, PickerId, PicklistDate)
                    .Select(c => new Picklstmodel
                    {
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        PickerId = c.PickerId,
                        AllotmentId = c.AllotmentId,
                        Picklistid = c.Picklistid,
                        AllottedBy = Convert.ToInt32(c.AllottedBy),
                        AllottedDate = Convert.ToDateTime(c.AllottedDate),
                        AllotmentStatus = c.AllotmentStatus,
                        AllotmentStatusText = c.AllotmentStatusText,
                        PicklistNo = c.PicklistNo,
                        PicklistDate = Convert.ToDateTime(c.PicklistDate),
                        FromInv = c.FromInv,
                        ToInv = c.ToInv,
                        PicklistStatus = Convert.ToInt32(c.PicklistStatus),
                        PicklistStatusText = c.PicklistStatusText,
                        AcceptedDate = Convert.ToDateTime(c.AcceptedDate),
                        ReasonId = Convert.ToInt32(c.ReasonId),
                        ReasonText = c.ReasonText,
                        RejectRemark = c.RejectRemark,
                        PickedDate = Convert.ToDateTime(c.PickedDate),
                        PickerConcernId = Convert.ToInt32(c.PickerConcernId),
                        pickerconcerText = c.pickerconcerText,
                        PickerConcernRemark = c.PickerConcernRemark,
                        PickerConcernDate = Convert.ToDateTime(c.PickerConcernDate),
                        VerifiedBy = Convert.ToInt32(c.VerifiedBy),
                        VerifiedDate = Convert.ToDateTime(c.VerifiedDate),
                        VerifiedByName = c.VerifiedByName,
                        VerifiedConcernId = Convert.ToInt32(c.VerifiedConcernId),
                        VerifiedConcernText = c.VerifiedConcernText,
                        VerifiedConcernRemark = c.VerifiedConcernRemark,
                        OnPriority = c.OnPriority,
                        IsStockTransfer = c.IsStockTransfer
                    }).OrderByDescending(x => x.OnPriority).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetAllotedPickListForPickerPvt", "Get Alloted Picklist For Picker", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return picklstmodels;
        }
        #endregion

        #region Invoice Header Status Update
        public string InvoiceHeaderStatusUpdate(InvoiceHeaderStatusUpdateModel model)
        {
            return InvoiceHeaderStatusUpdatePvt(model);
        }
        private string InvoiceHeaderStatusUpdatePvt(InvoiceHeaderStatusUpdateModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetVal", typeof(int));
            try
            {
                RetValue = _contextManager.usp_InvoiceHeaderStatusUpdate(model.InvId, model.BranchId, model.CompId, model.InvStatus, model.NoOfBox, model.InvWeight, model.IsColdStorage, model.IsCourier, model.ConcernId, model.PackedBy, model.Remark, model.Addedby, model.UpdateDate, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "InvoiceHeaderStatusUpdatePvt", "Invoice Header Status Update " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Assign Transport Mode
        public string AssignTransportMode(AssignTransportModel model)
        {
            return AssignTransportModePvt(model);
        }
        private string AssignTransportModePvt(AssignTransportModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_AssignTransportMode(model.InvoiceId, model.TransportModeId, model.PersonName,
                    model.PersonAddress, model.PersonMobileNo, model.OtherDetails, model.TransporterId, model.Delivery_Remark,
                    model.CAId, model.CourierId, model.Addedby, model.AttachedInvId, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AssignTransportModePvt", "Assign Transport Mode", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (model.Action == "DELETE")
            {
                if (RetValue > 0)
                {
                    return BusinessCont.DeleteRecord;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
            else
            {
                if (RetValue > 0)
                {
                    return BusinessCont.SuccessStatus;
                }
                else if (RetValue == -1)
                {
                    return BusinessCont.msg_exist;
                }
                else
                {
                    return BusinessCont.FailStatus;
                }
            }
        }
        #endregion

        #region Get Picklist Summary Data 
        public PickLstSummaryData GetPickListSummaryData(int BranchId, int CompId, int PickerId, DateTime PicklistDate)
        {
            return GetPickListSummaryDataPvt(BranchId, CompId, PickerId, PicklistDate);
        }
        private PickLstSummaryData GetPickListSummaryDataPvt(int BranchId, int CompId, int PickerId, DateTime PicklistDate)
        {
            PickLstSummaryData pickLstdDtls = new PickLstSummaryData();
            try
            {
                pickLstdDtls = _contextManager.usp_GetPicklistSummaryData(BranchId, CompId, PickerId, PicklistDate).Select(c => new PickLstSummaryData
                {
                    BranchId = c.BranchId,
                    CompId = c.CompId,
                    PicklistDate = Convert.ToDateTime(c.PicklistDate),
                    PickerId = c.PickerId,
                    Alloted = Convert.ToInt32(c.TotalPL),
                    Accepted = Convert.ToInt32(c.Accepted),
                    Pending = Convert.ToInt32(c.Pending),
                    Rejected = Convert.ToInt32(c.Rejected),
                    Concern = Convert.ToInt32(c.Concern),
                    Completed = Convert.ToInt32(c.Completed),
                    Verified = Convert.ToInt32(c.Verified)
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListSummaryDataPvt", "Get Picklist Summary Data  ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return pickLstdDtls;
        }
        #endregion

        #region Invoice List For Assign Trans Mode
        public List<InvoiceLstModel> InvoiceListForAssignTransMode(int BranchId, int CompId)
        {
            return InvoiceListForAssignTransModePvt(BranchId, CompId);
        }
        private List<InvoiceLstModel> InvoiceListForAssignTransModePvt(int BranchId, int CompId)
        {
            List<InvoiceLstModel> InvoiceLst = new List<InvoiceLstModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    InvoiceLst = _contextManager.usp_InvoiceHeaderListForAssignTransMode(BranchId, CompId).Select(c => new InvoiceLstModel
                    {
                        InvId = c.InvId,
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        InvNo = c.InvNo,
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate),
                        IsColdStorage = c.IsColdStorage,
                        SoldTo_StokistId = c.SoldTo_StokistId,
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        SoldTo_City = c.SoldTo_City,
                        InvAmount = Convert.ToDouble(c.InvAmount),
                        InvStatus = Convert.ToInt32(c.InvStatus),
                        StatusText = c.StatusText,
                        Addedby = c.Addedby,
                        AddedOn = Convert.ToDateTime(c.AddedOn),
                        LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn),
                        CityCode = c.CityCode,
                        CityName = c.CityName,
                        IsCourier = c.IsCourier,
                        OnPriority = Convert.ToInt32(c.OnPriority)
                    }).OrderBy(x => Convert.ToInt64(x.InvNo)).OrderByDescending(x => x.OnPriority).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "InvoiceListForAssignTransModePvt", "Invoice List For Assign Trans Mode " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Get PickList Generate New No
        public string GetPickListGenerateNewNo(int BranchId, int CompId, DateTime PicklistDate)
        {
            return GetPickListGenerateNewNoPvt(BranchId, CompId, PicklistDate);
        }
        private string GetPickListGenerateNewNoPvt(int BranchId, int CompId, DateTime PicklistDate)
        {
            string pickListNo = string.Empty;

            try
            {
                pickListNo = _contextManager.usp_PickListGenerateNewNo(BranchId, CompId, PicklistDate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListGenerateNewNoPvt", "Get PickList Generate New No " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return pickListNo;
        }
        #endregion

        #region Get Invoice Details For Sticker - Web
        public List<GetInvoiceDetailsForStickerModel> GetInvoiceDetailsForSticker(int BranchId, int CompId, int InvId)
        {
            return GetInvoiceDetailsForStickerPvt(BranchId, CompId, InvId);
        }
        private List<GetInvoiceDetailsForStickerModel> GetInvoiceDetailsForStickerPvt(int BranchId, int CompId, int InvId)
        {
            List<GetInvoiceDetailsForStickerModel> modelList = new List<GetInvoiceDetailsForStickerModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    modelList = _contextManager.usp_GetInvoiceDetailsForSticker(BranchId, CompId, InvId).Select(s => new GetInvoiceDetailsForStickerModel
                    {
                        InvId = s.InvId,
                        BranchId = s.BranchId,
                        CompId = s.CompId,
                        InvNo = s.InvNo,
                        StockistNo = s.StockistNo,
                        StockistName = s.StockistName,
                        MobNo = s.MobNo,
                        StockistAddress = s.StockistAddress,
                        CityCode = s.CityCode,
                        CityName = s.CityName,
                        TransportModeId = s.TransportModeId,
                        PersonName = s.PersonName,
                        PersonAddress = s.PersonAddress,
                        PersonMobNo = s.PersonMobNo,
                        OtherDetails = s.OtherDetails,
                        TransporterId = Convert.ToInt32(s.TransporterId),
                        TransporterNo = s.TransporterNo,
                        TransporterName = s.TransporterName,
                        CourierId = Convert.ToInt32(s.CourierId),
                        CourierName = s.CourierName,
                        Delivery_Remark = s.Delivery_Remark,
                        SoldTo_StokistId = s.SoldTo_StokistId,
                        InvAmount = Convert.ToDouble(s.InvAmount),
                        InvStatus = Convert.ToInt32(s.InvStatus),
                        NoOfBox = Convert.ToInt32(s.NoOfBox),
                        InvWeight = Convert.ToDecimal(s.InvWeight),
                        IsCourier = Convert.ToInt32(s.IsCourier),
                        OnPriority = Convert.ToInt32(s.OnPriority),
                        IsStockTransfer = Convert.ToInt32(s.IsStockTransfer),
                        AttachedInvId = Convert.ToInt32(s.AttachedInvId)
                    }).OrderByDescending(x => x.OnPriority).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceDetailsForStickerPvt", "Get Invoice Details For Sticker - Web" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Invoice Details For Sticker - Print
        public GetInvoiceDetailsForStickerModel GetInvoiceDetailsForPrintSticker(int BranchId, int CompId, int InvId)
        {
            return GetInvoiceDetailsForPrintStickerPvt(BranchId, CompId, InvId);
        }
        private GetInvoiceDetailsForStickerModel GetInvoiceDetailsForPrintStickerPvt(int BranchId, int CompId, int InvId)
        {
            GetInvoiceDetailsForStickerModel model = new GetInvoiceDetailsForStickerModel();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    model = _contextManager.usp_GetInvoiceDetailsForSticker(BranchId, CompId, InvId).Select(s => new GetInvoiceDetailsForStickerModel
                    {
                        InvId = s.InvId,
                        BranchId = s.BranchId,
                        CompId = s.CompId,
                        InvNo = s.InvNo,
                        StockistNo = s.StockistNo,
                        StockistName = s.StockistName,
                        MobNo = s.MobNo,
                        StockistAddress = s.StockistAddress,
                        CityCode = s.CityCode,
                        CityName = s.CityName,
                        TransportModeId = s.TransportModeId,
                        PersonName = s.PersonName,
                        PersonAddress = s.PersonAddress,
                        PersonMobNo = s.PersonMobNo,
                        OtherDetails = s.OtherDetails,
                        TransporterId = Convert.ToInt32(s.TransporterId),
                        TransporterNo = s.TransporterNo,
                        TransporterName = s.TransporterName,
                        CourierId = Convert.ToInt32(s.CourierId),
                        CourierName = s.CourierName,
                        Delivery_Remark = s.Delivery_Remark,
                        SoldTo_StokistId = s.SoldTo_StokistId,
                        InvAmount = Convert.ToDouble(s.InvAmount),
                        InvStatus = Convert.ToInt32(s.InvStatus),
                        NoOfBox = Convert.ToInt32(s.NoOfBox),
                        InvWeight = Convert.ToDecimal(s.InvWeight),
                        IsCourier = Convert.ToInt32(s.IsCourier),
                        OnPriority = Convert.ToInt32(s.OnPriority),
                        IsStockTransfer = Convert.ToInt32(s.IsStockTransfer)
                    }).OrderByDescending(x => x.OnPriority).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceDetailsForPrintStickerPvt", "Get Invoice Details For Sticker - Print" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get LR Data List
        public List<ImportLrDataModel> GetLRDataLst(int BranchId, int CompId, DateTime LRDate)
        {
            return GetLRDataLstPvt(BranchId, CompId, LRDate);
        }
        private List<ImportLrDataModel> GetLRDataLstPvt(int BranchId, int CompId, DateTime LRDate)
        {
            List<ImportLrDataModel> LRDataLst = new List<ImportLrDataModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    LRDataLst = _contextManager.usp_GetInvoiceListWithLRDetails(BranchId, CompId, Convert.ToDateTime(LRDate)).Select(c => new ImportLrDataModel
                    {
                        InvId = c.InvId,
                        InvNo = c.InvNo,
                        StokistId = c.StokistId,
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        LRNo = c.LRNo,
                        LRDate = Convert.ToDateTime(c.LRDate),
                        LRBox = Convert.ToInt32(c.LRBox),
                        LRWeightInKG = Convert.ToDecimal(c.LRWeightInKG),
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                        OnPriority = Convert.ToInt32(c.OnPriority),
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer)
                    }).OrderByDescending(x => x.OnPriority).ToList();
                    //OrderBy(x => Convert.ToInt64(x.InvNo))
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRDataLstPvt", "Get LR List " + "BranchId:  " + BranchId + "CompId:  " + CompId + "LRDate:  " + LRDate, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return LRDataLst;
        }
        #endregion

        #region Get Picklist Summary Data 
        public PickLstSummaryData GetPickListSummaryCounts(PickLstSummaryData model)
        {
            return GetPickListSummaryCountsPvt(model);
        }
        private PickLstSummaryData GetPickListSummaryCountsPvt(PickLstSummaryData model)
        {
            PickLstSummaryData pickLstdDtls = new PickLstSummaryData();
            try
            {
                pickLstdDtls = _contextManager.usp_GetPicklistSummaryCounts(model.BranchId, model.CompId, model.PicklistDate).Select(c => new PickLstSummaryData
                {
                    BranchId = c.BranchId,
                    CompId = c.CompId,
                    TotalPL = Convert.ToInt32(c.TotalPL),
                    OperatorRejected = Convert.ToInt32(c.OperatorRejected),
                    Pending = Convert.ToInt32(c.AllotmentPending),
                    Alloted = Convert.ToInt32(c.Alloted),
                    Accepted = Convert.ToInt32(c.Accepted),
                    Concern = Convert.ToInt32(c.Concern),
                    Completed = Convert.ToInt32(c.Completed),
                    CompletedVerified = Convert.ToInt32(c.CompletedVerified)
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListSummaryCountsPvt", "Get Picklist Summary Counts  ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return pickLstdDtls;
        }
        #endregion

        #region Get Picklist For Re-Allotment
        public List<PickListModel> GetPickListForReAllotment(PickListModel model)
        {
            return GetPickForReAllotmentPvt(model);
        }
        private List<PickListModel> GetPickForReAllotmentPvt(PickListModel model)
        {
            List<PickListModel> PickList = new List<PickListModel>();

            try
            {
                PickList = _contextManager.usp_GetPickListForReallotment(model.BranchId, model.CompId, model.PicklistDate).Select(c => new PickListModel
                {
                    BranchId = c.BranchId,
                    CompId = c.CompId,
                    Picklistid = c.Picklistid,
                    PicklistNo = c.PicklistNo,
                    PickerId = c.PickerId,
                    PicklistDate = DateTime.Parse(c.PicklistDate.ToString()),
                    FromInv = c.FromInv,
                    ToInv = c.ToInv,
                    PickerName = c.PickerName,
                    PickerNo = c.PickerNo,
                    PicklistStatus = Convert.ToInt32(c.PicklistStatus),
                    VerifiedBy = Convert.ToInt32(c.VerifiedBy),
                    StatusText = c.StatusText,
                    AllotmentId = c.AllotmentId,
                    AllottedDate = DateTime.Parse(c.AllottedDate.ToString()),
                    AllotmentStatus = c.AllotmentStatus.ToString(),
                    AllotmentStatusText = c.AllotmentStatusText,
                    RejectRemark = c.RejectRemark,
                    OnPriority = c.OnPriority,
                    IsStockTransfer = c.IsStockTransfer
                }).OrderByDescending(x => x.OnPriority).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickLstPvt", "Get Picklist By BranchId and CompanyId List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PickList;
        }
        #endregion

        #region Get Invoice Summary Counts
        public InvCntModel InvoiceSummaryCounts(int BranchId, int CompId, DateTime InvDate)
        {
            return InvoiceSummaryCountsPvt(BranchId, CompId, InvDate);
        }
        private InvCntModel InvoiceSummaryCountsPvt(int BranchId, int CompId, DateTime InvDate)
        {
            InvCntModel InvCnts = new InvCntModel();

            try
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceSummaryCounts", "Get Invoice Summary Counts", "START", "");
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    InvCnts = _contextManager.usp_GetInvoiceSummaryCounts(BranchId, CompId, InvDate).Select(c => new InvCntModel
                    {
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        TotalInv = Convert.ToInt32(c.TotalInv),
                        CancelInv = Convert.ToInt32(c.CancelInv),
                        AcceptedInv = Convert.ToInt32(c.AcceptedInv),
                        PendingForAcceptInv = Convert.ToInt32(c.PendingForAcceptInv),
                        InvoiceDrawn = Convert.ToInt32(c.InvoiceDrawn),
                        Packed = Convert.ToInt32(c.Packed),
                        ReadyToDispatch = Convert.ToInt32(c.ReadyToDispatch),
                        Concern = Convert.ToInt32(c.Concern),
                        GetpassGenerated = Convert.ToInt32(c.GetpassGenerated),
                        Dispatched = Convert.ToInt32(c.Dispatched)
                    }).FirstOrDefault();
                }
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceSummaryCounts", "Get Invoice Summary Counts", "END", "");
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceSummaryCounts", "Get Invoice Summary Counts", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvCnts;
        }
        #endregion

        #region PickList Header Delete
        public string PickListHeaderDelete(PickListModel model)
        {
            return PickListHeaderDeletePvt(model);
        }
        private string PickListHeaderDeletePvt(PickListModel model)
        {

            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_PickListHeaderDelete(model.Picklistid, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PickListHeaderDeletePvt", "PickList Header Delete ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Invoice Header Status Update
        public List<InvSts> InvoiceStatusForMob()
        {
            return InvoiceStatusForMobPvt();
        }
        private List<InvSts> InvoiceStatusForMobPvt()
        {
            List<InvSts> InvSts = new List<InvSts>();
            try
            {
                InvSts = _contextManager.usp_InvStatusForMob().Select(s => new InvSts
                {
                    Id = Convert.ToInt32(s.id),
                    StatusText = s.StatusText
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "InvoiceStatusFroMobPvt", "Invoice Status FroMobPvt " + " " + 0 + " " + 0, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvSts;
        }
        #endregion

        #region Get Printer Details
        public List<PrinterDtls> GetPrinterDetails(int BranchId, int CompId)
        {
            return GetPrinterDetailsPvt(BranchId, CompId);
        }
        private List<PrinterDtls> GetPrinterDetailsPvt(int BranchId, int CompId)
        {
            List<PrinterDtls> printerDtls = new List<PrinterDtls>();
            try
            {
                printerDtls = _contextManager.usp_GetPrintDetails(BranchId, CompId).Select(c => new PrinterDtls
                {
                    CompanyName = Convert.ToString(c.CompanyName),
                    BranchName = Convert.ToString(c.BranchName),
                    PrinterId = Convert.ToInt32(c.PrinterId),
                    BranchId = Convert.ToInt32(c.BranchId),
                    CompId = Convert.ToInt32(c.CompId),
                    PrinterType = Convert.ToString(c.PrinterType),
                    PrinterIPAddress = Convert.ToString(c.PrinterIPAddress),
                    PrinterName = Convert.ToString(c.PrinterName),
                    PrinterPortNumber = Convert.ToInt32(c.PrinterPortNumber),
                    AddedBy = Convert.ToString(c.AddedBy),
                    LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn),
                    UtilityNo = Convert.ToInt32(c.UtilityNo)
                }).OrderByDescending(x => x.PrinterType).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrinterDetailsPvt", "Get Printer Details - BranchId: " + BranchId + "  CompanyId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return printerDtls;
        }
        #endregion

        #region Printer Log Add/Edit
        public string PrinterLogAddEdit(PrinterLogAddEditModel model)
        {
            return PrinterLogAddEditPvt(model);
        }
        private string PrinterLogAddEditPvt(PrinterLogAddEditModel model)
        {
            decimal RetValue = 0;
            try
            {
                var resultId = _contextManager.usp_PrinterLogAddEdit(model.PrinterLogID, model.PrinterLogFor, model.PrinterLogData, model.PrinterLogStatus, model.PrinterLogDatetime, model.PrinterLogException).FirstOrDefault();
                if (resultId != null)
                {
                    RetValue = resultId.Value;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PrinterLogAddEditPvt", "Printer Log Add/Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Printer PDF Data - Save PDF File Path
        public string PrinterPDFData(PrintPDFDataModel model)
        {
            return PrinterPDFDataPvt(model);
        }
        public string PrinterPDFDataPvt(PrintPDFDataModel model)
        {
            string msg = string.Empty;
            int RetValue = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    RetValue = _contextManager.usp_SavePrintPDFData(model.pkId, model.BranchId, model.CompId, model.InvId, model.GpId, model.Type, model.BoxNo, model.Flag, model.AddedBy, model.Action, objRetValue);
                }
                if (model.Flag == "Queued")
                {
                    using (CFADBEntities _contextManagerPrint = new CFADBEntities())
                    {
                        // New Instance of Repository to Avoid Object reference
                        _loginRepository = new LoginRepository(_contextManagerPrint);

                        if (model.Type == BusinessCont.SPrinterType) // Sticker
                        {
                            _loginRepository.SendNotification(model.BranchId, Convert.ToInt32(model.AddedBy), BusinessCont.PrintStatusHeader, BusinessCont.PrintStatusMsg, BusinessCont.PayLoadBilldrawer);
                        }
                        else // Gatepass
                        {
                            _loginRepository.SendNotification(model.BranchId, Convert.ToInt32(model.AddedBy), BusinessCont.PrintStatusHeader, BusinessCont.PrintStatusMsg, BusinessCont.PayLoadGateSupervisor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PrinterPDFDataPvt", "Printer PDF Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                msg = BusinessCont.SuccessStatus;
            }
            else if (RetValue == -1)
            {
                msg = BusinessCont.msg_exist;
            }
            else
            {
                msg = BusinessCont.FailStatus;
            }
            return msg;
        }
        #endregion

        #region Get PDF Print Data
        public List<PrintPDFDataModel> GetPrintPDFData(int BranchId, int CompId, string PrinterType)
        {
            return GetPrintPDFDataPvt(BranchId, CompId, PrinterType);
        }
        private List<PrintPDFDataModel> GetPrintPDFDataPvt(int BranchId, int CompId, string PrinterType)
        {
            List<PrintPDFDataModel> PrintDataList = new List<PrintPDFDataModel>();
            PrintPDFDataModel PrintData = null;
            try
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrintPDFDataPvt", "Get Print PDF Data", "START", "");

                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    var resultData = _contextManager.usp_GetPrintPDFData(BranchId, CompId, PrinterType).ToList();
                    if (resultData.Count > 0)
                    {
                        foreach (var c in resultData)
                        {
                            PrintData = new PrintPDFDataModel();
                            PrintData.pkId = c.pkId;
                            PrintData.BranchId = c.BranchId;
                            PrintData.CompId = c.CompId;
                            PrintData.InvId = c.InvId;
                            PrintData.GpId = c.GpId;
                            PrintData.Type = c.Type;
                            PrintData.BoxNo = c.BoxNo;
                            PrintData.Flag = c.Flag;
                            PrintData.AddedBy = c.AddedBy;
                            PrintData.LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn);
                            PrintDataList.Add(PrintData);
                        }
                    }
                    else
                    {
                        BusinessCont.SaveLog(0, 0, 0, "GetPrintPDFDataPvt", "Get Print PDF Data", "No Records", "");
                    }
                    BusinessCont.SaveLog(0, 0, 0, "GetPrintPDFDataPvt", "Get Print PDF Data", "END", "");
                    return PrintDataList;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrintPDFDataPvt", "Get Print PDF Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PrintDataList;
        }
        #endregion

        #region Generate Gatepass Add Edit
        public Responce GenerateGatepasAddEdit(GatePassModel model)
        {
            return GenerateGatepasAddEditPvt(model);
        }
        private Responce GenerateGatepasAddEditPvt(GatePassModel model)
        {
            Responce res = new Responce();

            try
            {
                res = _contextManager.usp_GenerateGatepassAddEdit(model.GatepassId, model.BranchId, model.CompId, model.CAId, model.GatepassDate, model.VehicleNo, model.InvStr,
                    model.GuardNameId, model.GuardNameText, model.DriverName, model.Addedby, model.Action)
                    .Select(r => new Responce
                    {
                        GatepassId = r.GatepassId,
                        InvIdStr = r.InvIdStr
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GenerateGatepasAddEditPvt", "Generate Gatepass Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }

            return res;
        }
        #endregion

        #region Gatepass Generate New No
        public string GatepassListGenerateNewNo(int BranchId, int CompId, DateTime GatepassDate)
        {
            return GatepassListGenerateNewNoPvt(BranchId, CompId, GatepassDate);
        }
        private string GatepassListGenerateNewNoPvt(int BranchId, int CompId, DateTime GatepassDate)
        {
            string GatepassNo = string.Empty;

            try
            {
                GatepassNo = _contextManager.usp_GatepassListGenerateNewNo(BranchId, CompId, GatepassDate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GatepassListGenerateNewNoPvt", "Gatepass Generate New No", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GatepassNo;
        }
        #endregion

        #region Get Gatepass Dtls For PDF
        public List<InvGatpassDtls> GetGatepassDtlsForPDF(int BranchId, int CompId, int GPid)
        {
            return GetGatepassDtlsForPDFPvt(BranchId, CompId, GPid);
        }
        private List<InvGatpassDtls> GetGatepassDtlsForPDFPvt(int BranchId, int CompId, int GPid)
        {
            List<InvGatpassDtls> invaGatepassDtls = new List<InvGatpassDtls>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    invaGatepassDtls = _contextManager.usp_GetGatepassDtlsForPDF(BranchId, CompId, GPid).Select(s => new InvGatpassDtls
                    {
                        GatepassId = Convert.ToInt32(s.GatepassId),
                        BranchId = Convert.ToInt32(s.BranchId),
                        CompId = Convert.ToInt32(s.CompId),
                        GatepassNo = Convert.ToString(s.GatepassNo),
                        GatepassDate = Convert.ToDateTime(s.GatepassDate),
                        VehicleNo = Convert.ToString(s.VehicleNo),
                        GuardNameId = Convert.ToInt32(s.GuardNameId),
                        EmpName = Convert.ToString(s.EmpName),
                        EmpNo = Convert.ToString(s.EmpNo),
                        DriverName = Convert.ToString(s.DriverName),
                        Addedby = Convert.ToString(s.Addedby),
                        AddedOn = Convert.ToDateTime(s.AddedOn),
                        UpdatedBy = Convert.ToString(s.UpdatedBy),
                        LastUpdatedOn = Convert.ToDateTime(s.LastUpdatedOn),
                        IsPrinted = Convert.ToInt32(s.IsPrinted),
                        TransportModeId = Convert.ToInt32(s.TransportModeId),
                        TrasportModeText = s.TrasportModeText,
                        TransporterId = Convert.ToInt32(s.TransporterId),
                        TransporterNo = Convert.ToString(s.TransporterNo),
                        TransporterName = Convert.ToString(s.TransporterName),
                        CourierId = Convert.ToInt32(s.CourierId),
                        CourierName = Convert.ToString(s.CourierName),
                        Delivery_Remark = Convert.ToString(s.Delivery_Remark),
                        LRNo = Convert.ToString(s.LRNo),
                        InvoiceId = Convert.ToString(s.InvoiceId),
                        InvNo = Convert.ToString(s.InvNo),
                        SoldTo_StokistId = Convert.ToInt32(s.SoldTo_StokistId),
                        StockistNo = Convert.ToString(s.StockistNo),
                        StockistName = Convert.ToString(s.StockistName),
                        MobNo = Convert.ToString(s.MobNo),
                        Emailid = Convert.ToString(s.Emailid),
                        SoldTo_City = Convert.ToString(s.SoldTo_City),
                        CityName = Convert.ToString(s.CityName),
                        NoOfBox = Convert.ToInt32(s.NoOfBox),
                        IsCourier = Convert.ToInt32(s.IsCourier),
                        BranchCode = Convert.ToString(s.BranchCode),
                        BranchName = Convert.ToString(s.BranchName),
                        BranchAddress = Convert.ToString(s.BranchAddress),
                        BrCitycode = Convert.ToString(s.BrCitycode),
                        BrCityName = Convert.ToString(s.BrCityName),
                        brPin = Convert.ToString(s.brPin),
                        brContactNo = Convert.ToString(s.brContactNo),
                        brEmail = Convert.ToString(s.brEmail),
                        CompanyCode = Convert.ToString(s.CompanyCode),
                        CompanyName = Convert.ToString(s.CompanyName),
                        CompanyEmail = Convert.ToString(s.CompanyEmail),
                        CompContactNo = Convert.ToString(s.CompContactNo),
                        CompanyAddress = Convert.ToString(s.CompanyAddress),
                        CompanyCityCode = Convert.ToString(s.CompanyCityCode),
                        CompanyCityName = Convert.ToString(s.CompanyCityName),
                        CompanyPin = Convert.ToString(s.CompanyPin),
                        CAId = Convert.ToInt32(s.CAId),
                        CAName = Convert.ToString(s.CAName),
                        GuardNameText = Convert.ToString(s.GuardNameText),
                        TransCourNameInv = Convert.ToString(s.TransCourNameInv)
                    }).OrderBy(i => Convert.ToString(i.TransCourNameInv)).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGatepassDtlsForPDFPvt", "Get Gatepass Dtls For PDF", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return invaGatepassDtls;
        }
        #endregion

        #region Get Gatepass Dtls For Mobile
        public List<GatepassDtls> GetGatepassDtlsForMobile(int BranchId, int CompId)
        {
            return GetGatepassDtlsForMobilePvt(BranchId, CompId);
        }
        private List<GatepassDtls> GetGatepassDtlsForMobilePvt(int BranchId, int CompId)
        {
            List<GatepassDtls> invaGatepassDtls = new List<GatepassDtls>();

            try
            {
                invaGatepassDtls = _contextManager.usp_GetGatepassDtlsForMob(BranchId, CompId).Select(s => new GatepassDtls
                {
                    GatepassId = s.GatepassId,
                    BranchId = s.BranchId,
                    CompId = s.CompId,
                    GatepassNo = s.GatepassNo,
                    GatepassDate = Convert.ToDateTime(s.GatepassDate),
                    VehicleNo = s.VehicleNo,
                    GuardNameId = Convert.ToInt32(s.GuardNameId),
                    EmpName = s.EmpName,
                    EmpNo = s.EmpNo,
                    DriverName = s.DriverName,
                    Addedby = s.Addedby,
                    AddedOn = Convert.ToDateTime(s.AddedOn),
                    UpdatedBy = s.UpdatedBy,
                    LastUpdatedOn = Convert.ToDateTime(s.LastUpdatedOn),
                    IsPrinted = Convert.ToInt32(s.IsPrinted),
                    NoOfInv = Convert.ToInt32(s.NoOfInv),
                    InvIdStr = s.InvIdStr,
                    CAId = s.CAId,
                    CAName = s.CAName,
                    GuardNameText = s.GuardNameText,
                    IsStockTransfer = s.IsStockTransfer
                }).OrderByDescending(x => x.GatepassNo).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGatepassDtlsForMobilePvt", "Get Gatepass Dtls For Mobile", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return invaGatepassDtls;
        }
        #endregion

        #region Invoice Details For Mobile
        public List<InvDtlsForMob> GetInvoiceDtlsForMobile(int BranchId, int CompId, int InvStatus)
        {
            return GetInvoiceDtlsForMobilePvt(BranchId, CompId, InvStatus);
        }
        private List<InvDtlsForMob> GetInvoiceDtlsForMobilePvt(int BranchId, int CompId, int InvStatus)
        {
            List<InvDtlsForMob> invaGatepassDtls = new List<InvDtlsForMob>();
            int APITimeout = 0;
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    APITimeout = Convert.ToInt32(ConfigurationManager.AppSettings["APITimeout"]);
                    _contextManager.Database.CommandTimeout = APITimeout; // API Timeout Added By Anil
                    invaGatepassDtls = _contextManager.usp_GetInvoiceListForMob(BranchId, CompId, InvStatus).Select(s => new InvDtlsForMob
                    {
                        InvId = s.InvId,
                        BranchId = s.BranchId,
                        CompId = s.CompId,
                        InvNo = s.InvNo,
                        InvCreatedDate = Convert.ToDateTime(s.InvCreatedDate),
                        StockistNo = s.StockistNo,
                        StockistName = s.StockistName,
                        MobNo = s.MobNo,
                        StockistAddress = s.StockistAddress,
                        CityCode = s.CityCode,
                        CityName = s.CityName,
                        PersonName = s.PersonName,
                        PersonAddress = s.PersonAddress,
                        PersonMobNo = s.PersonMobNo,
                        OtherDetails = s.OtherDetails,
                        TransporterId = Convert.ToInt32(s.TransporterId),
                        TransporterNo = s.TransporterNo,
                        TransporterName = s.TransporterName,
                        CourierId = Convert.ToInt32(s.CourierId),
                        CourierName = s.CourierName,
                        Delivery_Remark = s.Delivery_Remark,
                        SoldTo_StokistId = s.SoldTo_StokistId,
                        InvAmount = Convert.ToDouble(s.InvAmount),
                        InvStatus = Convert.ToInt32(s.InvStatus),
                        NoOfBox = Convert.ToInt32(s.NoOfBox),
                        InvWeight = Convert.ToDecimal(s.InvWeight),
                        IsCourier = Convert.ToInt32(s.IsCourier),
                        OnPriority = Convert.ToInt32(s.OnPriority),
                        TransportModeId = Convert.ToInt32(s.TransportModeId),
                        TransportModeText = s.TransportModeText,
                        AttachedInvId = Convert.ToInt32(s.AttachedInvId),
                        BrCityCode = s.BrCityCode,
                        IsStockTransfer = s.IsStockTransfer,
                        TrnsCourName = s.TransCourName,
                        ScannedBoxes = s.ScannedBoxes,
                        InvCnt = Convert.ToInt32(s.InvCnt)
                    }).OrderByDescending(x => x.InvId).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceDtlsForMobilePvt", "Get Invoice Dtls For Mobile", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return invaGatepassDtls;
        }
        #endregion

        #region Gatepass Dtls For DeleteBy Id
        public string GatepassDtlsForDeleteById(int GatepassId)
        {
            return GatepassDtlsForDeleteByIdPvt(GatepassId);
        }
        private string GatepassDtlsForDeleteByIdPvt(int GatepassId)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_GatepassDtlsForDeleteById(GatepassId, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GatepassDtlsForDeleteById", "Gatepass Dtls For DeleteBy Id:  " + GatepassId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Send Email To Stockies For Dispatch Done
        public string sendEmailForDispatchDone(string Emailid, string StockistName, string TransporterName, string CompanyName, int BranchId, int CompId)
        {
            bool bResult = false;
            string msg = string.Empty, messageformat = string.Empty, Date = string.Empty, Subject = string.Empty, MailFilePath = string.Empty, ToEmail = string.Empty, CCEmail = string.Empty, EmailCC = string.Empty;
            EmailSend Email = new EmailSend();
            List<CCEmailDtls> EmailCntDtls = new List<CCEmailDtls>();
            try
            {
                Date = DateTime.Today.Date.ToString("dd-MM-yyyy");
                ToEmail = Emailid;
                Subject = ConfigurationManager.AppSettings["DispatchSubj"] + Date + " ";
                MailFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailFile\\Dispatch_Done.html");
                ChequeAccountingRepository _ChequeAccountingRepository = new ChequeAccountingRepository(_contextManager);
                EmailCntDtls = _ChequeAccountingRepository.GetCCEmailDtlsPvt(BranchId, CompId, 5);
                if (EmailCntDtls.Count > 0)
                {
                    for (int i = 0; i < EmailCntDtls.Count; i++)
                    {
                        CCEmail += ";" + EmailCntDtls[i].Email;
                    }

                    EmailCC = CCEmail.TrimStart(';');
                    bResult = EmailNotification.SendEmailForDispatchDone(ToEmail, EmailCC, Subject, StockistName, TransporterName, CompanyName, MailFilePath);
                }

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "sendEmailForDispatchDone", "Dispatch Done Email", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (bResult == true)
            {
                return Email.Status = BusinessCont.SuccessStatus;
            }
            else
            {
                return Email.Status = BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Get Stockiest Invoice Details For Email
        public List<InvDtlsForEmail> GetInvDtlsForEmail(int BranchId, int CompId, int GatepassId)
        {
            return GetInvDtlsForEmailPvt(BranchId, CompId, GatepassId);
        }
        private List<InvDtlsForEmail> GetInvDtlsForEmailPvt(int BranchId, int CompId, int GatepassId)
        {
            List<InvDtlsForEmail> invaGatepassDtls = new List<InvDtlsForEmail>();

            try
            {
                invaGatepassDtls = _contextManager.usp_GetInvDtlsForEmail(BranchId, CompId, GatepassId).Select(s => new InvDtlsForEmail
                {
                    TransporterName = s.TransporterName,
                    LRNo = s.LRNo,
                    StockistName = s.StockistName,
                    Emailid = s.Emailid,
                    CompanyCode = s.CompanyCode,
                    CompanyName = s.CompanyName
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvDtlsForEmailPvt", "Get Inv Dtls For Email", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return invaGatepassDtls;
        }
        #endregion

        #region Print Details Add
        public string PrintDetailsAdd(PrinterDtls model)
        {
            return PrintDetailsAddPvt(model);
        }
        private string PrintDetailsAddPvt(PrinterDtls model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    RetValue = contextManager.usp_PrintDetailsAdd(model.PrinterId, model.BranchId, model.CompId, model.PrinterType, model.PrinterIPAddress, model.PrinterPortNumber, model.PrinterName, model.Action, model.UtilityNo, obj);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PrintDetailsAdd", "Print Details Add:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else if (RetValue == -1)
            {
                return BusinessCont.msg_exist;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Get Picklist By Picker Status
        public List<PickListModel> GetPicklistByPickerStatus(int BranchId, int CompId, DateTime PicklistDate)
        {
            return GetPicklistByPickerStatusPvt(BranchId, CompId, PicklistDate);
        }
        private List<PickListModel> GetPicklistByPickerStatusPvt(int BranchId, int CompId, DateTime PicklistDate)
        {
            List<PickListModel> PckList = new List<PickListModel>();
            List<PickListDetailsByPicker> PickListDetailsByPicker = new List<PickListDetailsByPicker>();

            try
            {
                PckList = _contextManager.usp_GetPickList(BranchId, CompId, PicklistDate).Select(c => new PickListModel
                {
                    BranchId = c.BranchId,
                    CompId = c.CompId,
                    Picklistid = c.Picklistid,
                    PicklistNo = c.PicklistNo,
                    PicklistDate = DateTime.Parse(c.PicklistDate.ToString()),
                    FromInv = c.FromInv,
                    ToInv = c.ToInv,
                    PicklistStatus = Convert.ToInt32(c.PicklistStatus),
                    VerifiedBy = Convert.ToInt32(c.VerifiedBy),
                    StatusText = c.StatusText,
                    OnPriority = c.OnPriority,
                    RejectReason = c.RejectReason,
                    IsStockTransfer = c.IsStockTransfer
                }).OrderByDescending(x => x.OnPriority).ToList();

                for (int i = 0; i < PckList.Count(); i++)
                {
                    PickListDetailsByPicker = _contextManager.usp_GetAllotmentDetailsPicklistWise(BranchId, CompId, PckList[i].Picklistid)
                        .Select(p => new PickListDetailsByPicker
                        {
                            Picklistid = p.Picklistid,
                            PickerId = p.PickerId,
                            PickerName = p.PickerName,
                            AllotmentId = p.AllotmentId,
                            AllotmentStatus = p.AllotmentStatus,
                            AllotmentStatusText = p.AllotmentStatusText,
                            ReasonId = Convert.ToInt32(p.ReasonId),
                            ReasonText = p.ReasonText,
                            RejectRemark = p.RejectRemark,
                            PickerConcernId = Convert.ToInt32(p.PickerConcernId),
                            pickerconcernText = p.pickerconcernText,
                            PickerConcernRemark = p.PickerConcernRemark,
                            VerifiedBy = Convert.ToInt32(p.VerifiedBy),
                            VerifiedConcernId = Convert.ToInt32(p.VerifiedConcernId),
                            VerifiedConcernText = p.VerifiedConcernText,
                            VerifiedConcernRemark = p.VerifiedConcernRemark,
                            AcceptedDate = Convert.ToString(p.AcceptedDate),
                            PickedDate = Convert.ToString(p.PickedDate),
                            PickerConcernDate = Convert.ToString(p.PickerConcernDate),
                            VerifiedDate = Convert.ToString(p.VerifiedDate)
                        }).ToList();
                    PckList[i].PickListByPicker = PickListDetailsByPicker;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPicklistByPickerStatusPvt", "GetPicklistByPickerStatusPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PckList;
        }
        #endregion

        #region Priority Invoice Flag Update
        public string PriorityInvoiceFlagUpdate(PriorityFlagUpdtModel model)
        {
            return PriorityInvoiceFlagUpdatePvt(model);
        }
        private string PriorityInvoiceFlagUpdatePvt(PriorityFlagUpdtModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetVal", typeof(int));

            DateTime? date = DateTime.Now; // New
            if (model.updateDate == Convert.ToDateTime("01 - 01 - 0001 00:00:00"))
            {
                date = null;
            }
            else
            {
                date = Convert.ToDateTime(model.updateDate.ToString("yyyy/MM/dd hh:mm:ss"));
            }
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    RetValue = contextManager.usp_InvoiceHeaderStatusPriority(model.InvId, model.PriorityFlag, model.Remark, model.Addedby, date, obj);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PriorityInvoiceFlagUpdatePvt", "Priority Invoice Flag Update: " + model.InvId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Picklist Resolve Concern Add
        public string ResolveConcernAdd(PickListModel model)
        {
            return ResolveConcernAddPvt(model);
        }
        private string ResolveConcernAddPvt(PickListModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            _loginRepository = new LoginRepository(_contextManager);
            try
            {
                RetValue = _contextManager.usp_PicklistResolveConcern(model.AllotmentId, model.Picklistid, model.BranchId, model.CurrentStatus,
                    model.ResolveRemark, Convert.ToDateTime(model.StatusTime), model.UpdatedBy, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveConcernAddPvt", "Resolve Concern Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                _loginRepository.SendNotification(model.BranchId, model.PickerId, BusinessCont.ResolveConcernHeader, BusinessCont.PickListResolveConcernMsg, "Picker");
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Get Resolve Concern List
        public List<PickListModel> ResolveConcernLst(int BranchId, int CompId, DateTime PicklistDate)
        {
            return ResolveConcernLstPvt(BranchId, CompId, PicklistDate);
        }
        private List<PickListModel> ResolveConcernLstPvt(int BranchId, int CompId, DateTime PicklistDate)
        {
            List<PickListModel> resolveConcrn = new List<PickListModel>();

            try
            {
                resolveConcrn = _contextManager.usp_GetPicklistForResolveConcern(BranchId, CompId, PicklistDate).Select(c => new PickListModel
                {
                    Picklistid = c.Picklistid,
                    PicklistNo = c.PicklistNo,
                    PicklistDate = DateTime.Parse(c.PicklistDate.ToString()),
                    FromInv = c.FromInv,
                    ToInv = c.ToInv,
                    PicklistStatus = Convert.ToInt32(c.PicklistStatus),
                    PicklistStatusText = c.PicklistStatusText,
                    PickerId = c.PickerId,
                    PickerName = c.PickerName,
                    AllotmentId = c.AllotmentId,
                    AllotmentStatus = Convert.ToString(c.AllotmentStatus),
                    AllotmentStatusText = Convert.ToString(c.AllotmentStatusText),
                    ReasonId = Convert.ToInt32(c.ReasonId),
                    ReasonText = c.ReasonText,
                    pickerconcernText = c.pickerconcernText,
                    RejectRemark = c.RejectRemark,
                    PickerConcernId = Convert.ToInt32(c.PickerConcernId),
                    PickerConcernRemark = c.PickerConcernRemark,
                    VerifiedBy = Convert.ToInt32(c.VerifiedBy),
                    VerifiedConcernId = Convert.ToInt32(c.VerifiedConcernId),
                    VerifiedConcernText = c.VerifiedConcernText,
                    VerifiedConcernRemark = c.VerifiedConcernRemark,
                    AcceptedDate = Convert.ToDateTime(c.AcceptedDate),
                    PickedDate = Convert.ToDateTime(c.PickedDate),
                    PickerConcernDate = Convert.ToDateTime(c.PickerConcernDate),
                    VerifiedDate = Convert.ToDateTime(c.VerifiedDate),
                    OnPriority = c.OnPriority,
                    IsStockTransfer = Convert.ToInt32(c.IsStockTransfer)
                }).OrderByDescending(x => x.OnPriority).ToList();

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveConcernLstPvt", "Resolve ConcernLst Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return resolveConcrn;
        }
        #endregion

        #region Invoice Resolve Concern Add
        public string ResolveInvConcernAdd(InvoiceLstModel model)
        {
            return ResolveInvConcernAddPvt(model);
        }
        private string ResolveInvConcernAddPvt(InvoiceLstModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetVal", typeof(int));
            _loginRepository = new LoginRepository(_contextManager);
            try
            {
                RetValue = _contextManager.usp_InvoiceHeaderResolveConcern(Convert.ToInt32(model.InvId), model.CurrentStatus, model.Remark, model.Addedby, model.updateDate, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveConcernAddPvt", "Resolve Concern Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                _loginRepository.SendNotification(model.BranchId, model.AcceptedBy, BusinessCont.ResolveConcernHeader, BusinessCont.InvoiceResolveConcernMsg, "Picker");
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Get Invoice Header List for Resolve Convern
        public List<InvoiceLstModel> GetInvoiceHeaderLstResolveCnrn(int BranchId, int CompId, int BillDrawerId)
        {
            return GetInvoiceHeaderLstResolveCnrnPvt(BranchId, CompId, BillDrawerId);
        }
        private List<InvoiceLstModel> GetInvoiceHeaderLstResolveCnrnPvt(int BranchId, int CompId, int BillDrawerId)
        {
            List<InvoiceLstModel> InvoiceLst = new List<InvoiceLstModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    InvoiceLst = _contextManager.usp_GetInvoiceHeaderResolveConcern(BranchId, CompId, BillDrawerId).Select(c => new InvoiceLstModel
                    {
                        InvId = c.InvId,
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        InvNo = c.InvNo,
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate),
                        IsColdStorage = c.IsColdStorage,
                        SoldTo_StokistId = c.SoldTo_StokistId,
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        CityName = c.CityName,
                        InvAmount = Convert.ToDouble(c.InvAmount),
                        InvStatus = Convert.ToInt32(c.InvStatus),
                        StatusText = c.StatusText,
                        CityCode = c.CityCode,
                        AcceptedBy = Convert.ToInt32(c.AcceptedBy),
                        BillDrawerName = c.BillDrawerName,
                        PackedBy = Convert.ToInt32(c.PackedBy),
                        PackedByName = c.PackedByName,
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                        InvWeight = Convert.ToInt32(c.InvWeight),
                        IsCourier = Convert.ToInt32(c.IsCourier),
                        PackingConcernDate = Convert.ToDateTime(c.PackingConcernDate),
                        PackingConcernId = Convert.ToInt32(c.PackingConcernId),
                        PackingConcernText = c.PackingConcernText,
                        PackingRemark = c.PackingRemark,
                        ReadyToDispatchBy = Convert.ToInt32(c.ReadyToDispatchBy),
                        ReadyToDispatchDate = Convert.ToDateTime(c.ReadyToDispatchDate),
                        ReadyToDispatchConcernId = Convert.ToInt32(c.ReadyToDispatchConcernId),
                        ReadyToDispatchRemark = c.ReadyToDispatchRemark,
                        DispatchByName = c.DispatchByName,
                        DispatchConcernText = c.DispatchConcernText,
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer),
                        OnPriority = Convert.ToInt32(c.OnPriority)
                    }).OrderByDescending(x => x.OnPriority).ToList();
                    //OrderBy(x => Convert.ToInt64(x.InvNo))
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceHeaderLstResolveCnrnPvt", "Get Invoice Header List for Resolve Convern " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Get Assigned Transporter List
        public List<AssignedTransportModel> GetAssignedTransporterList(int BranchId, int CompId)
        {
            return GetAssignedTransporterListPvt(BranchId, CompId);
        }
        private List<AssignedTransportModel> GetAssignedTransporterListPvt(int BranchId, int CompId)
        {
            List<AssignedTransportModel> AssignTransportLst = new List<AssignedTransportModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    AssignTransportLst = _contextManager.usp_GetAssignedTransporterList(BranchId, CompId).Select(c => new AssignedTransportModel
                    {
                        AssignTransMId = c.AssignTransMId,
                        InvoiceId = Convert.ToString(c.InvId),
                        InvNo = Convert.ToString(c.InvNo),
                        InvNo1 = Convert.ToString(c.InvNo1),
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        CityName = c.CityName,
                        TransportModeId = c.TransportModeId,
                        PersonName = c.PersonName,
                        PersonAddress = c.PersonAddress,
                        PersonMobileNo = c.PersonMobNo,
                        OtherDetails = c.OtherDetails,
                        TransporterName = c.TransporterName,
                        TransporterId = Convert.ToInt32(c.TransporterId),
                        CourierName = c.CourierName,
                        CourierId = Convert.ToInt32(c.CourierId),
                        Delivery_Remark = c.Delivery_Remark,
                        LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn),
                        OnPriority = Convert.ToInt32(c.OnPriority),
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer),
                        CityCode = c.CityCode,
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                        InvWeight = Convert.ToDecimal(c.InvWeight),
                        IsCourier = Convert.ToInt32(c.IsCourier)
                    }).OrderByDescending(x => x.OnPriority).ToList();
                    //OrderBy(x => Convert.ToInt64(x.InvNo))
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetAssignedTransporterListPvt", "Get Assigned Transporter List ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return AssignTransportLst;
        }
        #endregion

        #region Edit Assigned Transport Mode
        public string EditAssignedTransportMode(AssignedTransportModel model)
        {
            return EditAssignedTransportModePvt(model);
        }
        private string EditAssignedTransportModePvt(AssignedTransportModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_AssignTransportModeEdit(model.AssignTransMId, model.InvoiceId, model.TransportModeId, model.PersonName, model.PersonAddress, model.PersonMobileNo, model.OtherDetails, model.TransporterId, model.Delivery_Remark, model.CourierId, model.Addedby, model.OCnfCity, model.NoOfBox, model.InvWeight, model.IsCourier, obj);

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "EditAssignedTransportModePvt", "Edit Assigned Transport Mode", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }

            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.FailStatus;
            }


        }
        #endregion
              
        #region START - Get Print PDF Data With Printer
        public List<PrintPDFDataModel> GetPrintPDFDataWithPrinter(int BranchId, int CompId, int UtilityNo)
        {
            return GetPrintPDFDataWithPrinterPvt(BranchId, CompId, UtilityNo);
        }
        private List<PrintPDFDataModel> GetPrintPDFDataWithPrinterPvt(int BranchId, int CompId, int UtilityNo)
        {
            List<PrintPDFDataModel> PrintDataList = new List<PrintPDFDataModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    PrintDataList = _contextManager.usp_GetPrintPDFDataWithPrinter(BranchId, CompId, UtilityNo).Select(c => new PrintPDFDataModel
                    {
                        pkId = c.pkId,
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        InvId = c.InvId,
                        GpId = c.GpId,
                        Type = c.Type,
                        BoxNo = c.BoxNo,
                        Flag = c.Flag,
                        PrinterId = Convert.ToInt32(c.PrinterId),
                        PrinterIPAddress = c.PrinterIPAddress,
                        PrinterName = c.PrinterName,
                        PrinterPortNumber = Convert.ToInt32(c.PrinterPortNumber),
                        AddedBy = Convert.ToString(c.AddedBy),
                        PrintCount = Convert.ToInt32(c.PrintCount),
                        LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn),
                        UtilityNo = Convert.ToInt32(c.UtilityNo)
                    }).ToList();
                }
            } 
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrintPDFDataWithPrinterPvt", "Get Print PDF Data With Printer", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PrintDataList;
        }
        #endregion

        #region Check Invoice No Already Exist for mobile
        public string CheckInvNoExistMob(CheckInvNoExitModel model)
        {
            return CheckInvNoExistMobPvt(model);
        }
        private string CheckInvNoExistMobPvt(CheckInvNoExitModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetVal", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    var result = _contextManager.usp_CheckInvNo(model.BranchId, model.CompId, model.InvId, obj).FirstOrDefault();
                    RetValue = Convert.ToInt32(result.Value);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CheckInvNoExistMobPvt", "Check Inv No Exist Mob Pvt " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue >= 0)
            {
                //return BusinessCont.msg_alreadyprocess;
               return Convert.ToString(RetValue);
            }
            else if (RetValue == -1)
            {
                return BusinessCont.msg_dnotexst;
            }
            else
            {
                return BusinessCont.FailStatus;
            }
        }
        #endregion

        #region Save Assign Transport And Print Data
        public string SaveAndPrint(SaveAndPrintModel model)
        {
            return SaveAndPrintPvt(model);
        }
        private string SaveAndPrintPvt(SaveAndPrintModel model)
        {
            int RetValue = 0, InsertedCount = 0;
            string Result = string.Empty;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            int GpId = 0;
            string Action = BusinessCont.ADDRecord; // ADD
            string Flag = BusinessCont.PendingPrinterMsg; // After PDF Saved -> Flag to set default - Pending
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    if (model.OCnfCity > 0)
                    {
                        RetValue = _contextManager.usp_AssignTransportModeStkTransfer(model.InvoiceId, model.TransportModeId, model.TransporterId, model.OCnfCity, model.Addedby, model.AttachedInvId, model.Action, obj);
                    }
                    else
                    {
                        BusinessCont.SaveLog(0, 0, 0, "SaveAndPrintPvt", "Save And Print - MOB " + (" InvoiceId = " + model.InvoiceId + "  & AttachInvoiceId = " + model.AttachedInvId + " & RetValue = " + RetValue), "START", "");

                        RetValue = _contextManager.usp_AssignTransportMode(model.InvoiceId, model.TransportModeId, model.PersonName, model.PersonAddress, model.PersonMobileNo, model.OtherDetails, model.TransporterId, model.Delivery_Remark, model.CAId, model.CourierId, model.Addedby, model.AttachedInvId, model.Action, obj);
                        
                        BusinessCont.SaveLog(0, 0, 0, "SaveAndPrintPvt", "Save And Print - MOB " + (" InvoiceId = " + model.InvoiceId + "  & AttachInvoiceId = " + model.AttachedInvId + " & RetValue = " + RetValue), "END", "");
                    }

                    if (RetValue > 0)
                    {
                        InsertedCount = _contextManager.usp_SavePrintPDFData(model.pkId, model.BranchId, model.CompId, model.InvId, GpId, model.Type, model.BoxNo, Flag, model.Addedby, Action, objRetValue);

                        // UtilityNotification(model.Type, model.BranchId, model.CompId, model.Addedby);
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveAndPrintPvt", "Save And Print - MOB ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                if (InsertedCount > 0)
                {
                    return BusinessCont.PrintSuccessStatus;
                }
                else if (InsertedCount == -1)
                {
                    return BusinessCont.Printmsg_exist;
                }
                else
                {
                    return BusinessCont.PrintFailStatus;
                }
            }
            else if (RetValue == -1)
            {
                return BusinessCont.Transportmsg_exist;
            }
            else
            {
                return BusinessCont.TransportFailStatus;
            }
        }

        #region START - Utility Notification - (Sticker/Gatepass)
        private void UtilityNotification(string Type, int BranchId, int CompId, int UtilityNo, string Addedby)
        {
            string msgUtility = string.Empty;
            bool IsCheckFlag = false;
            try
            {
                BusinessCont.SaveLog(0, 0, 0, "UtilityNotification", "To Check Utility Connected or not (Sticker/Gatepass)", "START", "");
                var checkUtility = _contextManager.usp_GetPrintPDFDataWithPrinter(BranchId, CompId, UtilityNo).FirstOrDefault();
                if (checkUtility != null)
                {
                    // To Check Utility Connected or not (Sticker/Gatepass)
                    if (Type == BusinessCont.SPrinterType) // Sticker
                    {
                        IsCheckFlag = IsConnectedToUtility();
                        UpdateUtilityStatus(Type, BranchId, Addedby, IsCheckFlag);
                    }
                    else // Gatepass
                    {
                        IsCheckFlag = IsConnectedToUtility();
                        UpdateUtilityStatus(Type, BranchId, Addedby, IsCheckFlag);
                    }
                }
                BusinessCont.SaveLog(0, 0, 0, "UtilityNotification", "To Check Utility Connected or not (Sticker/Gatepass)", "END", "");
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "UtilityNotification", "Utility Notification - (Sticker/Gatepass)", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
        }
        #endregion END - Utility Notification - (Sticker/Gatepass)

        #region START - Is Connected To Utility
        private bool IsConnectedToUtility()
        {
            bool result = false;
            Ping p = new Ping();
            string UtilitySystemIpAdress = string.Empty;
            try
            {
                UtilitySystemIpAdress = ConfigurationManager.AppSettings["UtilitySystemIpAdress"];
                PingReply reply = p.Send(UtilitySystemIpAdress, 3000);
                if (reply.Status == IPStatus.Success)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "IsConnectedToUtility", "Is Connected To Utility", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return result;
        }
        #endregion END - Is Connected To Utility

        #region START - To Check Utility Connected or not (Sticker/Gatepass)
        private void UpdateUtilityStatus(string Type, int BranchId, string Addedby, bool IsCheckFlag)
        {
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    // New Instance of Repository to Avoid Object reference
                    _loginRepository = new LoginRepository(_contextManager);

                    if (Type == BusinessCont.SPrinterType && IsCheckFlag == true) // Sticker
                    {
                        _loginRepository.SendNotification(BranchId, Convert.ToInt32(Addedby), BusinessCont.UtilityStatusHeader, BusinessCont.UtilityConnectedMsg, BusinessCont.PayLoadBilldrawer);
                    }
                    else
                    {
                        _loginRepository.SendNotification(BranchId, Convert.ToInt32(Addedby), BusinessCont.UtilityStatusHeader, BusinessCont.UtilityNotConnectedMsg, BusinessCont.PayLoadBilldrawer);
                    }

                    if (Type == BusinessCont.GPrinterType && IsCheckFlag == true) // Gatepass
                    {
                        _loginRepository.SendNotification(BranchId, Convert.ToInt32(Addedby), BusinessCont.UtilityStatusHeader, BusinessCont.UtilityConnectedMsg, BusinessCont.PayLoadGateSupervisor);
                    }
                    else
                    {
                        _loginRepository.SendNotification(BranchId, Convert.ToInt32(Addedby), BusinessCont.UtilityStatusHeader, BusinessCont.UtilityNotConnectedMsg, BusinessCont.PayLoadGateSupervisor);
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "UpdateUtilityStatus", "To Check Utility Connected or not (Sticker/Gatepass)", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
        }
        #endregion END - To Check Utility Connected or not (Sticker/Gatepass)

        #endregion

        #region Get All Invoice Count
        public InvoiceCountsModel AllInvoiceCounts(int BranchId, int CompId)
        {
            return AllInvoiceCountsPvt(BranchId, CompId);
        }
        private InvoiceCountsModel AllInvoiceCountsPvt(int BranchId, int CompId)
        {
            InvoiceCountsModel allInvct = new InvoiceCountsModel();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    allInvct = _contextManager.usp_GetallInvoicepagesCount(BranchId, CompId).Select(c => new InvoiceCountsModel
                    {
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        TotalInvoices = Convert.ToInt32(c.TotalInvoices),
                        TodaysWithOldOpen = Convert.ToInt32(c.TodaysWithOldOpen),
                        CancelInvCtn = Convert.ToInt32(c.CancelInvCtn),
                        PendingInvCtn = Convert.ToInt32(c.PendingInvCtn),
                        OnPriorityCtn = Convert.ToInt32(c.OnPriorityCtn),
                        PackerConcern = Convert.ToInt32(c.PackerConcern),
                        GatpassGenCtn = Convert.ToInt32(c.GatpassGenCtn),
                        PendingLR = Convert.ToInt32(c.PendingLR),
                        IsStockTransferCtn = Convert.ToInt32(c.IsStockTransferCtn),
                        StkPrint = Convert.ToInt32(c.StkPrint),
                        LocalMode = Convert.ToInt32(c.LocalMode),
                        OtherCity = Convert.ToInt32(c.OtherCity),
                        ByHand = Convert.ToInt32(c.ByHand),
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AllInvoiceCounts", "Get Invoice All Summary Counts", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return allInvct;
        }

        #endregion

        #region Save Scanned Inv Data
        public int SaveScannedInvData(SaveScannedInvData model)
        {
            return SaveScannedInvDataPvt(model);
        }
        private int SaveScannedInvDataPvt(SaveScannedInvData model)
        {
            int RetValue = 0;
            var Data = JsonConvert.DeserializeObject<List<InvIdScannedBoxes>>(model.ScannedData);
            List<InvIdScannedBoxes> modelList = new List<InvIdScannedBoxes>();
            for (int i = 0; i < Data.Count; i++)
            {
                InvIdScannedBoxes SacannedData = new InvIdScannedBoxes();
                SacannedData.pkId = i + 1;
                SacannedData.InvId = Data[i].InvId;
                SacannedData.ScannedBoxes = Data[i].ScannedBoxes;
                modelList.Add(SacannedData);
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("pkId");
            dt.Columns.Add("InvId");
            dt.Columns.Add("ScannedBoxes");

            foreach (var item in modelList)
            {
                dt.Rows.Add(item.pkId, item.InvId, item.ScannedBoxes);
            }
            try
            {
                if (dt.Rows.Count > 0)
                {
                    using (var db = new CFADBEntities())
                    {
                        {
                            SqlConnection connection = (SqlConnection)db.Database.Connection;
                            SqlCommand cmd = new SqlCommand("CFA.usp_ScannedInvDataAddUpdate", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
                            BranchIdParameter.SqlDbType = SqlDbType.Int;
                            SqlParameter CompIdParameter = cmd.Parameters.AddWithValue("@CompId", model.CompId);
                            CompIdParameter.SqlDbType = SqlDbType.Int;
                            SqlParameter AddedByParameter = cmd.Parameters.AddWithValue("@AddedBy", model.AddedBy);
                            AddedByParameter.SqlDbType = SqlDbType.NVarChar;
                            SqlParameter ImportInvDataParameter = cmd.Parameters.AddWithValue("@tblData", dt);
                            ImportInvDataParameter.SqlDbType = SqlDbType.Structured;
                            if (connection.State == ConnectionState.Closed)
                                connection.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                RetValue = (int)dr["RetValue"];
                            }
                            if (connection.State == ConnectionState.Open)
                                connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveScannedInvDataPvt", "Save Scanned Inv Data Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Dashboard Count List
        public OrderDispatchCountFordashModel OrderDispatchCounts(int BranchId, int CompanyId)
        {
            return OrderDispatchCountsPvt(BranchId, CompanyId);
        }
        private OrderDispatchCountFordashModel OrderDispatchCountsPvt(int BranchId, int CompanyId)
        {
            OrderDispatchCountFordashModel ordis = new OrderDispatchCountFordashModel();
            try
            {
                using (CFADBEntities _contexetManager = new CFADBEntities())
                {
                    _contexetManager.Database.CommandTimeout = 1000;
                    ordis = _contextManager.usp_DashbordOrderDispatchCntNew(BranchId, CompanyId).Select(c => new OrderDispatchCountFordashModel
                    {
                        TodayInv = Convert.ToInt32(c.InvToday),
                        PendingInv = Convert.ToInt32(c.InvPending),
                        PriorityInvToday = Convert.ToInt32(c.PrioToday),
                        PriorityPending = Convert.ToInt32(c.PrioPending),
                        InvConcernPending = Convert.ToInt32(c.ConcernPending),
                        StkPrintedToday = Convert.ToInt32(c.StkrToday),
                        StkPending = Convert.ToInt32(c.StkrPending),
                        GPTodayCreated = Convert.ToInt32(c.GPToday),
                        GPPending = Convert.ToInt32(c.GPPending),
                        PendingLR = Convert.ToInt32(c.LRPending),
                        TPBox = Convert.ToInt32(c.TPBox),
                        TPStockiest = Convert.ToInt32(c.TPStockiest),
                        BoxForLR = Convert.ToInt32(c.BoxForLR),
                        StkInv = Convert.ToInt32(c.StkInv),
                        PLToday = Convert.ToInt32(c.PLToday),
                        PLPending = Convert.ToInt32(c.PLPending),
                        PLConcern = Convert.ToInt32(c.PLConcern),
                        PLVerifiedToday = Convert.ToInt32(c.PLVerifiedToday),
                        PLVerifiedPending = Convert.ToInt32(c.PLVerifiedPending),
                        PLAllotedToday = Convert.ToInt32(c.PLAllotedToday),
                        PLAllotedPending = Convert.ToInt32(c.PLAllotedPending),
                        LocalMode = Convert.ToInt32(c.LocalPending),
                        OtherCity = Convert.ToInt32(c.OtherCityPending),
                        ByHand = Convert.ToInt32(c.ByHandPending),
                        TotalInvoices = Convert.ToInt32(c.TotalInvoices),
                        DispatchN = Convert.ToInt32(c.DispatchN),
                        DispatchN1 = Convert.ToInt32(c.DispatchN1),
                        DispatchN2 = Convert.ToInt32(c.DispatchN2),
                        DispatchNPer = Convert.ToInt32(c.DispatchNPer),
                        DispatchN1Per = Convert.ToInt32(c.DispatchN1Per),
                        DispatchN2Per = Convert.ToInt32(c.DispatchN2Per),
                        DispatchPending = Convert.ToInt32(c.DispatchPending),
                        StkrPendingAmt = Convert.ToDecimal(c.StkrPendingAmt),
                        GPPendingAmt = Convert.ToDecimal(c.GPPendingAmt),
                        CummInvCnt = Convert.ToInt32(c.CummInvCnt),
                        CummBoxes = Convert.ToInt32(c.CummBoxCnt),
                        CummPLCnt = Convert.ToInt32(c.CummPLCnt),
                        TodaySalesAmt = Convert.ToDecimal(c.TodaySalesAmt),
                        CummSalesAmt = Convert.ToDecimal(c.CummSaleAmt),
                        LocalTotalDisp = Convert.ToInt32(c.LocalTotalDisp),
                        OtherTotalDisp = Convert.ToInt32(c.OtherTotalDisp),
                        ByHandTotalDisp = Convert.ToInt32(c.ByHandTotalDisp),
                        PLCompVerifyPending = Convert.ToInt32(c.PLCompVerifyPending),
                        PrevTotalDisp = Convert.ToInt32(c.PrevTotalDisp),
                        PrevN= Convert.ToInt32(c.PrevN),
                        PrevN1 = Convert.ToInt32(c.PrevN1),
                        PrevN2 = Convert.ToInt32(c.PrevN2),
                        PrevNPer= Convert.ToInt32(c.PrevNPer),
                        PrevN1Per= Convert.ToInt32(c.PrevN1Per),
                        PrevN2Per= Convert.ToInt32(c.PrevN2Per),
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "OrderDispatchCounts", "Order Dispatch Counts", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ordis;
        }

        #endregion

        #region Get Order Dispatch Filter List New
        public List<DashOrderDispatchList> GetOrderDispatchFilterListNew(int BranchId, int CompId)
        {
            return GetOrderDispatchFilterListNewPvt(BranchId, CompId);
        }
        private List<DashOrderDispatchList> GetOrderDispatchFilterListNewPvt(int BranchId, int CompId)
        {
            List<DashOrderDispatchList> DasInvoiceLst = new List<DashOrderDispatchList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    DasInvoiceLst = _contextManager.usp_DashbordOrderDispatchListNew(BranchId, CompId).Select(c => new DashOrderDispatchList
                    {
                        InvId = Convert.ToInt32(c.InvId),
                        InvNo = c.InvNo,
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate).ToString("yyyy-MM-dd"),
                        IsColdStorage = Convert.ToInt32(c.IsColdStorage),
                        InvAmount = Convert.ToDecimal(c.InvAmount),
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer),
                        InvStatus = Convert.ToInt32(c.InvStatus),
                        PackedDate = Convert.ToDateTime(c.PackedDate).ToString("yyyy-MM-dd"),
                        PackedBy = Convert.ToInt32(c.PackedBy),
                        StockistId = Convert.ToInt32(c.StockistId),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        InvoiceIdTM = Convert.ToInt32(c.InvoiceIdTM),
                        AttachedInvId = Convert.ToInt32(c.AttachedInvId),
                        LRDate = Convert.ToDateTime(c.LRDate).ToString("yyyy-MM-dd"),
                        TransportModeId = Convert.ToInt32(c.TransportModeId),
                        ReadyToDispatchDate = Convert.ToDateTime(c.ReadyToDispatchDate).ToString("yyyy-MM-dd"),
                        OnPriority = Convert.ToInt32(c.OnPriority),
                        PrioPending = Convert.ToInt32(c.PrioPending),
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                    }).OrderBy(x => Convert.ToString(x.InvNo)).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispatchFilterListNewPvt", "Get Order Dispatch Filter List Pvt " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return DasInvoiceLst;
        }
        #endregion

        #region Get Order Disp LR Filter List New
        public List<OrderDispPLModelList> GetOrderDispLRFilterListNew(int BranchId, int CompId)
        {
            return GetOrderDispLRFilterListNewPvt(BranchId, CompId);
        }
        private List<OrderDispPLModelList> GetOrderDispLRFilterListNewPvt(int BranchId, int CompId)
        {
            List<OrderDispPLModelList> OrderDispPLLst = new List<OrderDispPLModelList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    OrderDispPLLst = _contextManager.usp_DashbordOrderDispLRListNew(BranchId, CompId).Select(c => new OrderDispPLModelList
                    {
                        PicklistNo = c.PicklistNo,
                        PicklistDate = Convert.ToDateTime(c.PicklistDate),
                        PLDate = Convert.ToDateTime(c.PicklistDate).ToString("yyyy-MM-dd"),
                        FromInv = c.FromInv,
                        ToInv = c.ToInv,
                        PicklistStatus = Convert.ToInt32(c.PicklistStatus),
                        VerifiedDate = Convert.ToDateTime(c.VerifiedDate).ToString("yyyy-MM-dd"),
                        StatusText = c.StatusText,
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer),
                        AllottedDate = Convert.ToDateTime(c.AllottedDate).ToString("yyyy-MM-dd")
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispPLFilterListNewPvt", "Get Order Disp PL Filter List New Pvt " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OrderDispPLLst;
        }
        #endregion

        #region Get Order Dispatch Cumm Inv Filter List New
        public List<DashOrderDispatchList> GetOrderDispatchCummInvList(int BranchId, int CompId)
        {
            return GetOrderDispatchCummInvListPvt(BranchId, CompId);
        }
        private List<DashOrderDispatchList> GetOrderDispatchCummInvListPvt(int BranchId, int CompId)
        {
            List<DashOrderDispatchList> DasInvoiceLst = new List<DashOrderDispatchList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    DasInvoiceLst = _contextManager.usp_DashOrderDispatchCummInvList(BranchId, CompId).Select(c => new DashOrderDispatchList
                    {
                        InvId = Convert.ToInt32(c.InvId),
                        InvNo = c.InvNo,
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate).ToString("yyyy-MM-dd"),
                        InvAmount = Convert.ToDecimal(c.InvAmount),
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        DispatchN = Convert.ToInt32(c.DispatchN),
                        DispatchN1 = Convert.ToInt32(c.DispatchN1),
                        DispatchN2 = Convert.ToInt32(c.DispatchN2),
                        TransportModeId = Convert.ToInt32(c.TransportModeId),
                        NoOfBox = Convert.ToInt32(c.NoOfBox)
                    }).OrderBy(x => Convert.ToString(x.InvNo)).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispatchCummInvListPvt", "Get Order Dispatch Cumm Inv List Pvt ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return DasInvoiceLst;
        }
        #endregion

        #region Get Order Disp Cumm PL List New
        public List<OrderDispPLModelList> GetOrderDispCummPLListNew(int BranchId, int CompId)
        {
            return GetOrderDispCummPLListNewPvt(BranchId, CompId);
        }
        private List<OrderDispPLModelList> GetOrderDispCummPLListNewPvt(int BranchId, int CompId)
        {
            List<OrderDispPLModelList> OrderDispPLLst = new List<OrderDispPLModelList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    OrderDispPLLst = _contextManager.usp_DashOrderDispCummPLListNew(BranchId, CompId).Select(c => new OrderDispPLModelList
                    {
                        PicklistNo = c.PicklistNo,
                        PicklistDate = Convert.ToDateTime(c.PicklistDate),
                        PLDate = Convert.ToDateTime(c.PicklistDate).ToString("yyyy-MM-dd"),
                        FromInv = c.FromInv,
                        ToInv = c.ToInv,
                        PicklistStatus = Convert.ToInt32(c.PicklistStatus),
                        VerifiedDate = Convert.ToDateTime(c.VerifiedDate).ToString("yyyy-MM-dd"),
                        StatusText = c.StatusText,
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer),
                        AllottedDate = Convert.ToDateTime(c.AllottedDate).ToString("yyyy-MM-dd")
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispCummPLListNewPvt", "Get Order Disp Cumm PL List New Pvt ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OrderDispPLLst;
        }
        #endregion

        #region Get Order Dispatch Summary Count List
        public List<OrderDispatchSmmryCnt> GetOrderDispatchSummaryCount(int BranchId, int CompId)
        {
            return GetOrderDispatchSummaryCountPvt(BranchId, CompId);
        }
        private List<OrderDispatchSmmryCnt> GetOrderDispatchSummaryCountPvt(int BranchId, int CompId)
        {
            List<OrderDispatchSmmryCnt> OrderDispSmmryLst = new List<OrderDispatchSmmryCnt>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    OrderDispSmmryLst = _contextManager.usp_DashOrderDispatchSmmryCnt(BranchId, CompId).Select(c => new OrderDispatchSmmryCnt
                    {
                        InvDate = Convert.ToDateTime(c.InvDate),
                        InvCount = Convert.ToInt32(c.InvCount),
                        InvAmount = Convert.ToInt32(c.InvAmount),
                        NoOfBox = Convert.ToInt32(c.NoOfBox)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispatchSummaryCountPvt", "Get Order Dispatch Summary Count List Pvt ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OrderDispSmmryLst;
        }
        #endregion

        #region Get UtilityNo New
        public List<GetUtilityNoNewModel> GetUtilityNoNew()
        {
            return GetUtilityNoNewPvt();
        }
        private List<GetUtilityNoNewModel> GetUtilityNoNewPvt()
        {
            List<GetUtilityNoNewModel> modelList = new List<GetUtilityNoNewModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    modelList = _contextManager.usp_GetUtilityNoNew().Select(u => new GetUtilityNoNewModel
                    {
                       UtilityNo = Convert.ToInt32(u.UtilityNo)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetUtilityNoNewPvt", "Get UtilityNo New", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Owner Order Disp Dash Inv Smmry List
        public List<OwnOrdrDispInvSmmryList> GetOwnerOrderDispDashInvSmmryList()
        {
            return GetOwnerOrderDispDashInvSmmryListPvt();
        }
        private List<OwnOrdrDispInvSmmryList> GetOwnerOrderDispDashInvSmmryListPvt()
        {
            List<OwnOrdrDispInvSmmryList> smmrylist = new List<OwnOrdrDispInvSmmryList>();
            try
            {
                using (CFADBEntities _contexetManager = new CFADBEntities())
                {
                    smmrylist = _contextManager.usp_OwnerOrderDispDashInvSmmryList().Select(c => new OwnOrdrDispInvSmmryList
                    {
                        BranchId = c.BranchId,
                        BranchName = c.BranchName,
                        CompId = c.CompId,
                        CompanyName = c.CompanyName,
                        PrioPending = c.PrioPending,
                        StkrPending = c.StkrPending,
                        StkrPendingAmt = Convert.ToDecimal(c.StkrPendingAmt),
                        GPPending = c.GPPending,
                        GPPendingAmt = Convert.ToDecimal(c.GPPendingAmt)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerOrderDispDashInvSmmryListPvt", "Get Owner Order Disp Dash Inv Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return smmrylist;
        }
        #endregion

        #region Get Owner Order Disp Dash Boxes Smmry List
        public List<OwnOrdrDispBoxesSmmryList> GetOwnerOrderDispDashBoxesSmmryList()
        {
            return GetOwnerOrderDispDashBoxesSmmryListPvt();
        }
        private List<OwnOrdrDispBoxesSmmryList> GetOwnerOrderDispDashBoxesSmmryListPvt()
        {
            List<OwnOrdrDispBoxesSmmryList> smmrylist = new List<OwnOrdrDispBoxesSmmryList>();
            try
            {
                using (CFADBEntities _contexetManager = new CFADBEntities())
                {
                    smmrylist = _contextManager.usp_OwnerOrderDispDashBoxesSmmryList().Select(c => new OwnOrdrDispBoxesSmmryList
                    {
                        BranchId = Convert.ToInt32(c.BranchId),
                        BranchName = c.BranchName,
                        CompId = Convert.ToInt32(c.CompId),
                        CompanyName = c.CompanyName,
                        InvCount = Convert.ToInt32(c.InvCount),
                        InvAmount = Convert.ToDecimal(c.InvAmount),
                        NoOfBoxes = Convert.ToInt32(c.NoOfBox)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerOrderDispDashBoxesSmmryListPvt", "Get Owner Order Disp Dash Boxes Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return smmrylist;
        }
        #endregion

        #region Get Picklist Summary Data 
        public PickLstSummaryData GetPickListSummaryCountsForStockTrans(PickLstSummaryData model)
        {
            return GetPickListSummaryCountsForStockTransPvt(model);
        }
        private PickLstSummaryData GetPickListSummaryCountsForStockTransPvt(PickLstSummaryData model)
        {
            PickLstSummaryData pickLstdDtls = new PickLstSummaryData();
            try
            {
                pickLstdDtls = _contextManager.usp_GetPicklistSummaryCountsStockTrans(model.BranchId, model.CompId, model.PicklistDate).Select(c => new PickLstSummaryData
                {
                    BranchId = c.BranchId,
                    CompId = c.CompId,
                    TotalPL = Convert.ToInt32(c.TotalPL),
                    OperatorRejected = Convert.ToInt32(c.OperatorRejected),
                    Pending = Convert.ToInt32(c.AllotmentPending),
                    Alloted = Convert.ToInt32(c.Alloted),
                    Accepted = Convert.ToInt32(c.Accepted),
                    Concern = Convert.ToInt32(c.Concern),
                    Completed = Convert.ToInt32(c.Completed),
                    CompletedVerified = Convert.ToInt32(c.CompletedVerified)
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListSummaryCountsForStockTransPvt", "Get Pick List Summary Counts For Stock Trans Pvt  ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return pickLstdDtls;
        }
        #endregion

        #region Get All Invoice Count For Stock Transfer Count
        public InvoiceCountsModel AllInvoiceCountsStkCount(int BranchId, int CompId)
        {
            return AllInvoiceCountsStkCountPvt(BranchId, CompId);
        }
        private InvoiceCountsModel AllInvoiceCountsStkCountPvt(int BranchId, int CompId)
        {
            InvoiceCountsModel allInvct = new InvoiceCountsModel();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    allInvct = _contextManager.usp_GetallInvoicepagesForStkTransCnt(BranchId, CompId).Select(c => new InvoiceCountsModel
                    {
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        TotalInvoices = Convert.ToInt32(c.TotalInvoices),
                        TodaysWithOldOpen = Convert.ToInt32(c.TodaysWithOldOpen),
                        CancelInvCtn = Convert.ToInt32(c.CancelInvCtn),
                        PendingInvCtn = Convert.ToInt32(c.PendingInvCtn),
                        OnPriorityCtn = Convert.ToInt32(c.OnPriorityCtn),
                        PackerConcern = Convert.ToInt32(c.PackerConcern),
                        GatpassGenCtn = Convert.ToInt32(c.GatpassGenCtn),
                        PendingLR = Convert.ToInt32(c.PendingLR),
                        IsStockTransferCtn = Convert.ToInt32(c.IsStockTransferCtn),
                        StkPrint = Convert.ToInt32(c.StkPrint),
                        LocalMode = Convert.ToInt32(c.LocalMode),
                        OtherCity = Convert.ToInt32(c.OtherCity),
                        ByHand = Convert.ToInt32(c.ByHand),
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AllInvoiceCountsStkCountPvt", "All Invoice Counts Stk Count Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return allInvct;
        }

        #endregion

        #region Get LR Details List For Dash New
        public List<LRDetailsListForDash> GetLRDetailsListForDashNew(int BranchId, int CompId)
        {
            return GetLRDetailsListForDashNewPvt(BranchId, CompId);
        }
        private List<LRDetailsListForDash> GetLRDetailsListForDashNewPvt(int BranchId, int CompId)
        {
            List<LRDetailsListForDash> LRDataLst = new List<LRDetailsListForDash>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    LRDataLst = _contextManager.usp_GetLRDetailsListForDashNew(BranchId, CompId).Select(c => new LRDetailsListForDash
                    {
                        InvId = c.InvId,
                        InvNo = c.InvNo,
                        StokistId = c.StokistId,
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        LRNo = c.LRNo,
                        LRDate = Convert.ToDateTime(c.LRDate),
                        LRBox = Convert.ToInt32(c.LRBox),
                        LRWeightInKG = Convert.ToDecimal(c.LRWeightInKG),
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                        OnPriority = Convert.ToInt32(c.OnPriority),
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRDetailsListForDashNewPvt", "Get LR Details List For Dash New Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return LRDataLst;
        }
        #endregion

        #region Get Prio Pending Inv For Dash New 
        public List<DashOrderDispatchList> GetPrioPendingInvForDashNew(int BranchId, int CompId)
        {
            return GetPrioPendingInvForDashNewPvt(BranchId, CompId);
        }
        private List<DashOrderDispatchList> GetPrioPendingInvForDashNewPvt(int BranchId, int CompId)
        {
            List<DashOrderDispatchList> prioInvoiceLst = new List<DashOrderDispatchList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    prioInvoiceLst = _contextManager.usp_DashODPrioPendingInvListNew(BranchId, CompId).Select(c => new DashOrderDispatchList
                    {
                        InvId = Convert.ToInt32(c.InvId),
                        InvNo = c.InvNo,
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate).ToString("yyyy-MM-dd"),
                        IsColdStorage = Convert.ToInt32(c.IsColdStorage),
                        InvAmount = Convert.ToDecimal(c.InvAmount),
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer),
                        InvStatus = Convert.ToInt32(c.InvStatus),
                        PackedDate = Convert.ToDateTime(c.PackedDate).ToString("yyyy-MM-dd"),
                        PackedBy = Convert.ToInt32(c.PackedBy),
                        StockistId = Convert.ToInt32(c.StockistId),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        InvoiceIdTM = Convert.ToInt32(c.InvoiceIdTM),
                        AttachedInvId = Convert.ToInt32(c.AttachedInvId),
                        LRDate = Convert.ToDateTime(c.LRDate).ToString("yyyy-MM-dd"),
                        TransportModeId = Convert.ToInt32(c.TransportModeId),
                        ReadyToDispatchDate = Convert.ToDateTime(c.ReadyToDispatchDate).ToString("yyyy-MM-dd"),
                        OnPriority = Convert.ToInt32(c.OnPriority),
                        PrioPending = Convert.ToInt32(c.PrioPending)
                    }).OrderBy(x => Convert.ToString(x.InvNo)).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrioPendingInvForDashNew", "Get Prio Pending Inv For Dash New ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return prioInvoiceLst;
        }
        #endregion

        #region Get Column Header List for import check dynmically
        public List<GetColumnHeaderModel> GetColumnHeaderList(int BranchId, int CompId,string Importfor)
        {
            return GetColumnHeaderListPvt(BranchId, CompId, Importfor);
        }
        private List<GetColumnHeaderModel> GetColumnHeaderListPvt(int BranchId, int CompId,string Importfor)
        {
            List<GetColumnHeaderModel> ColumnHeaderList = new List<GetColumnHeaderModel>();
            try
            {
                ColumnHeaderList = _contextManager.usp_GetColumnHeaderList(BranchId, CompId, Importfor)
                    .Select(c => new GetColumnHeaderModel
                    {
                        pkId = c.pkId,
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        ImpFor = c.ImpFor,
                        FieldName = c.FieldName,
                        ExcelColName = c.ExcelColName,
                        ColumnDatatype = c.ColumnDatatype,
                        UpdatedBy = Convert.ToInt32(c.UpdatedBy),
                        UpdatedOn = Convert.ToDateTime(c.UpdatedOn),
                    })
                    .OrderBy(x => Convert.ToString(x.pkId))
                    .ToList();
            }

            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetColumnHeaderListPvt", "Ge tColumn Header List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ColumnHeaderList;
        }
        #endregion

        #region Get Transporter list Details
        public List<transporterForMonthlyModel> GettransporterForMonthlyModelList(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate, int TransporterId,int CourierId,int TransportModeId)
        {
            return GettransporterForMonthlyModelListPvt(BranchId, CompId, FromDate, ToDate, TransporterId, CourierId, TransportModeId);
        }
        private List<transporterForMonthlyModel> GettransporterForMonthlyModelListPvt(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate, int TransporterId, int CourierId,int TransportModeId)

        {
            List<transporterForMonthlyModel> TransportSummaryForMonthlyList = new List<transporterForMonthlyModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    TransportSummaryForMonthlyList = _contextManager.usp_TransportersummaryDetail(BranchId, CompId, FromDate, ToDate, TransporterId,CourierId, TransportModeId).
                        Select(c => new transporterForMonthlyModel
                        {                            
                        TransporterId = Convert.ToInt32(c.TransporterId),
                        TransportName = c.TransporterName,                 
                        GatepassDate = c.DispatchDate != DateTime.Parse("0001-01-01T00:00:00") ? c.DispatchDate : (DateTime?)null,
                        InvNo = Convert.ToString(c.InvNo),
                        NoOfBoxes = Convert.ToInt32(c.NoOfBoxes),
                        RatePerBox = Convert.ToInt32(c.RatePerBox),
                        TotalAmount =Convert.ToInt32(c.TotalAmount)
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GettransporterForMonthlyModelListPvt", "Get Transporter Summary of previous month/Week " + "CompId:  " + CompId + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransportSummaryForMonthlyList;
        }
        #endregion

        #region Get Transporter list For Summary
        public List<TransporterForMonthlyModels> GetTransporterForMonthlyModelSummary(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate)
        {
            return GetTransporterForMonthlyModelSummaryPvt(BranchId, CompId, FromDate, ToDate);
        }
        private List<TransporterForMonthlyModels> GetTransporterForMonthlyModelSummaryPvt(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate)
        {
            List<TransporterForMonthlyModels> TransportSummaryWithMonthlyList = new List<TransporterForMonthlyModels>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    TransportSummaryWithMonthlyList = _contextManager.usp_TransporterSummary(BranchId, CompId, FromDate, ToDate).Select(c => new TransporterForMonthlyModels
                    {
                        TransportName = c.TransporterName,
                        NoOfInvoice = Convert.ToString(c.NoOfInvoice),
                        NoOfBoxes = Convert.ToInt32(c.NoOfBoxes),
                        RatePerBox=Convert.ToInt32(c.RatePerBox),
                        TotalAmt=Convert.ToInt32(c.TotalAmt)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterForMonthlyModelSummaryPvt", "Get Transporter Summary of previous month/Week " + "CompId:  " + CompId + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransportSummaryWithMonthlyList;
        }
        #endregion

        #region Get Order Dispatch Prev Month Data
        public List<DashOrderDispatchListPrevMonth> GetOrderDispatchPrevMonthData(int BranchId, int CompId)
        {
            return GetOrderDispatchPrevMonthDataPvt(BranchId, CompId);
        }
        private List<DashOrderDispatchListPrevMonth> GetOrderDispatchPrevMonthDataPvt(int BranchId, int CompId)
        {
            List<DashOrderDispatchListPrevMonth> DasInvoiceLst = new List<DashOrderDispatchListPrevMonth>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    DasInvoiceLst = _contextManager.usp_DashOrderDispatchInvListPrevdata(BranchId, CompId).Select(c => new DashOrderDispatchListPrevMonth
                    {
                        InvId = Convert.ToInt32(c.InvId),
                        InvNo = c.InvNo,
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate).ToString("yyyy-MM-dd"),
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        InvAmount = Convert.ToDecimal(c.InvAmount),
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer),
                        PrevN = Convert.ToInt32(c.PrevN),
                        PrevN1 = Convert.ToInt32(c.PrevN1),
                        PrevN2 = Convert.ToInt32(c.PrevN2),
                        PrevTotalDisp = Convert.ToInt32(c.PrevTotalDisp),
                        TransportModeId = Convert.ToInt32(c.TransportModeId)
                    }).OrderBy(x => Convert.ToString(x.InvNo)).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispatchPrevMonthDataPvt", "Get Order Dispat chPrev Month Data Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return DasInvoiceLst;
        }
        #endregion

        #region Get Order Dispatch Filter List For Sticker
        public List<DashOrderDispatchList> GetOrderDispatchFilterListForSticker(int BranchId, int CompId)
        {
            return GetOrderDispatchFilterListForStickerPvt(BranchId, CompId);
        }
        private List<DashOrderDispatchList> GetOrderDispatchFilterListForStickerPvt(int BranchId, int CompId)
        {
            List<DashOrderDispatchList> DasInvoiceLst = new List<DashOrderDispatchList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    DasInvoiceLst = _contextManager.usp_DashbordOrderDispatchListForSticker(BranchId, CompId).Select(c => new DashOrderDispatchList
                    {
                        InvId = Convert.ToInt32(c.InvId),
                        InvNo = c.InvNo,
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate).ToString("yyyy-MM-dd"),
                        IsColdStorage = Convert.ToInt32(c.IsColdStorage),
                        InvAmount = Convert.ToDecimal(c.InvAmount),
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer),
                        InvStatus = Convert.ToInt32(c.InvStatus),
                        PackedDate = Convert.ToDateTime(c.PackedDate).ToString("yyyy-MM-dd"),
                        PackedBy = Convert.ToInt32(c.PackedBy),
                        StockistId = Convert.ToInt32(c.StockistId),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        ReadyToDispatchDate = Convert.ToDateTime(c.ReadyToDispatchDate).ToString("yyyy-MM-dd"),
                        OnPriority = Convert.ToInt32(c.OnPriority),
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                    }).OrderBy(x => Convert.ToString(x.InvNo)).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispatchFilterListForStickerPvt", "Get Order Dispatch Filter List For Sticker Pvt " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return DasInvoiceLst;
        }
        #endregion
        
        #region Get Gatepass List for Print
        public List<GatepassListModal> GetGatepassList(int? BranchId, int? CompanyId, DateTime? fromDate, DateTime? toDate)
        {
            return GetGatepassListpvt(BranchId, CompanyId, fromDate, toDate);
        }

        private List<GatepassListModal> GetGatepassListpvt(int? BranchId, int? CompanyId, DateTime? fromDate, DateTime? toDate)
        {
            List<GatepassListModal> list = new List<GatepassListModal>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    list = _contextManager.usp_GetGatepassPrint(BranchId, CompanyId, fromDate, toDate).Select(x => new GatepassListModal
                    {
                        GatepassId = Convert.ToInt32(x.GatepassId),
                        GatepassNo = x.GatepassNo,
                        GatepassDate = Convert.ToDateTime(x.GatepassDate)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGatepassListpvt", "Get Gatepass List pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return list;
        }

        #endregion

        #region Printer Gatepass Data
        public int PrinterGatepassData(GatepassPrintModal model)
        {
            return PrinterGatepassDataPvt(model);
        }
        private int PrinterGatepassDataPvt(GatepassPrintModal model)
        {
            int RetVal = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetVal", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    RetVal = _contextManager.usp_SavePrintGatepass(model.BranchId, model.CompanyId, model.GatepassId, model.UserId, objRetValue);
                }
            }
            catch(Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PrinterGatepassData_pvt", "Printer Gatepass Data _pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetVal;
        }
        #endregion

        #region Get Invoice List For Delete
        public List<InvoiceLstDltModel> GetInvoiceLstForDlt(int BranchId, int CompId, string InvNo)
        {
            return GetInvoiceLstForDltPvt(BranchId, CompId,InvNo);
        }
        private List<InvoiceLstDltModel> GetInvoiceLstForDltPvt(int BranchId, int CompId, string InvNo)
        {
            List<InvoiceLstDltModel> InvoiceLst = new List<InvoiceLstDltModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    InvoiceLst = _contextManager.usp_GetInvoiceListForDelete(BranchId, CompId, InvNo).Select(c => new InvoiceLstDltModel
                    {
                        InvId = Convert.ToInt32(c.InvId),
                        BranchId = Convert.ToInt32(c.BranchId),
                        CompId = Convert.ToInt32(c.CompId),
                        InvNo = Convert.ToString(c.InvNo),
                        InvCreatedDate = Convert.ToDateTime(c.InvCreatedDate),
                        StockistName = Convert.ToString(c.StockistName),
                        CityName = Convert.ToString(c.CityName),
                        InvAmount = Convert.ToInt32(c.InvAmount),
                        InvStatus = c.StatusText,
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer),
                        StockistNo = Convert.ToString(c.StockistNo)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceLstForDltPvt", "Get Invoice List For Delete " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Delete Invoice Details 
        public int DeleteInvoiceDetails(int InvId, int BranchId)
        {
            return DeleteInvoiceDetailsPvt(InvId,BranchId);
        }
        private int DeleteInvoiceDetailsPvt(int InvId, int BranchId)
        {
            int result = 0;
            ObjectParameter RetVal = new ObjectParameter("ReturnVal", typeof(int));
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    result = contextManager.usp_DeleteInvoiceData(InvId, BranchId, RetVal);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "DeleteInvoiceDetailsPvt", "Delete Invoice Details  BranchId:  ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return result;
        }
        #endregion

    }
}
