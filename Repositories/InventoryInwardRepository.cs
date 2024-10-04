using CNF.Business.BusinessConstant;
using CNF.Business.Model.ChequeAccounting;
using CNF.Business.Model.Context;
using CNF.Business.Model.InventoryInward;
using CNF.Business.Model.Login;
using CNF.Business.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CNF.Business.Repositories
{
    public class InventoryInwardRepository : IInventoryInwardRepository
    {
        private CFADBEntities _contextManager;

        // Inject DB Context instance from Unit of Work to avoid unwanted memory allocations.
        public InventoryInwardRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }

        #region Get Transit Data List
        public List<GetTransitListModel> GetTransitDataLst(int BranchId, int CompId)
        {
            return GetTransitDataLstPvt(BranchId, CompId);
        }
        private List<GetTransitListModel> GetTransitDataLstPvt(int BranchId, int CompId)
        {
            List<GetTransitListModel> TransitDataLst = new List<GetTransitListModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    TransitDataLst = _contextManager.usp_GetTransitDataList(BranchId, CompId)
                        .Select(c => new GetTransitListModel
                        {

                            BranchId = c.BranchId,
                            CompId = c.CompId,
                            DeliveryNo = c.DeliveryNo,
                            ActualGIDate = Convert.ToDateTime(c.ActualGIDate),
                            RecPlant = c.RecPlant,
                            RecPlantDesc = c.RecPlantDesc,
                            DispPlant = c.DispPlant,
                            DispPlantDesc = c.DispPlantDesc,
                            InvoiceNo = c.InvNo,
                            InvoiceDate = Convert.ToDateTime(c.InvoiceDate),
                            MaterialNo = c.MaterialNo,
                            MatDesc = c.MatDesc,
                            UOM = c.UoM,
                            BatchNo = c.BatchNo,
                            Quantity = c.Quantity,
                            TransporterId = Convert.ToInt32(c.TransporterId),
                            TransporterNo = c.TransporterNo,
                            TransporterName = c.TransporterName,
                            LrNo = c.LrNo,
                            LrDate = Convert.ToDateTime(c.LrDate).ToString("yyyy-MM-dd"),
                            TotalCaseQty = c.TotalCaseQty,
                            VehicleNo = c.VehicleNo,
                            AddedBy = Convert.ToInt32(c.AddedBy),
                            AddedOn = Convert.ToDateTime(c.AddedOn).ToString("yyyy-MM-dd"),
                            IsMapDone = c.IsMapDone,
                            IsCheckListDone = c.IsChecklistDone
                        }).OrderBy(x => x.InvoiceNo).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransitDataLstPvt", "Get Transit Data List " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransitDataLst;
        }
        #endregion

        #region Raise Insurance Claim Add Edit And Delete
        public int RaiseInsuranceClaim(RaiseInsuranceClaimModel model)
        {
            return RaiseInsuranceClaimPvt(model);
        }
        private int RaiseInsuranceClaimPvt(RaiseInsuranceClaimModel model)
        {
            int RetValue = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    if (Convert.ToDateTime(model.ClaimDate).ToString("yyyy-MM-dd") == "1970-01-01" && Convert.ToDateTime(model.EmailSendDate).ToString("yyyy-MM-dd") == "1970-01-01")
                    {
                        model.ClaimDate = null;
                        model.EmailSendDate = null;
                    }
                    if (Convert.ToDateTime(model.EmailSendDate).ToString("yyyy-MM-dd") == "1970-01-01")
                    {
                        model.EmailSendDate = null;
                    }
                    if (model.EmailSendDate == null)
                    {
                        model.IsEmailSend = 0;
                    }
                    if (Convert.ToDateTime(model.SANDate).ToString("yyyy-MM-dd") == "1970-01-01")
                    {
                        model.SANDate = null;
                    }

                    RetValue = _contextManager.usp_RaiseInsuranceClaimAndSAN(model.TransitId, model.ClaimId, model.BranchId, model.CompId, model.LRNo, model.ClaimNo, model.ClaimDate, model.ClaimAmount, model.ClaimTypeId, model.EmailSendDate, model.SANNo, model.SANDate, model.SANAmount, model.IsEmailSend, model.Remark, model.AddedBy,
                        model.Action, objRetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "RaiseInsuranceClaimPvt", "Raise Insurance Claim Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Raise Insurance Claim List
        public List<GetRaiseInsuranceClaimListModel> GetRaiseInsuranceClaimList(int BranchId, int CompId)
        {
            return GetRaiseInsuranceClaimListPvt(BranchId, CompId);
        }
        private List<GetRaiseInsuranceClaimListModel> GetRaiseInsuranceClaimListPvt(int BranchId, int CompId)
        {
            List<GetRaiseInsuranceClaimListModel> ClaimList = new List<GetRaiseInsuranceClaimListModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    var raiseInsuranceClaim = _contextManager.usp_GetRaiseInsuranceClaimList(BranchId, CompId).OrderByDescending(x => x.ClaimId).ToList();
                    foreach (var item in raiseInsuranceClaim)
                    {
                        GetRaiseInsuranceClaimListModel model = new GetRaiseInsuranceClaimListModel();
                        model.ClaimId = item.ClaimId;
                        model.BranchId = item.BranchId;
                        model.CompId = item.CompId;
                        model.LRNo = item.LRNo;
                        model.ClaimNo = item.ClaimNo;
                        model.ClaimDate = Convert.ToDateTime(item.ClaimDate);
                        model.ClaimAmount = item.ClaimAmount;
                        model.ClaimType = item.MasterName;
                        model.ClaimTypeId = Convert.ToUInt32(item.ClaimTypeId);
                        model.EmailSendDate = (item.EmailSendDate != null ? Convert.ToDateTime(item.EmailSendDate).ToString("dd/MM/yyyy") : null);
                        if (model.EmailSendDate != null)
                        {
                            model.EmailSendDate = model.EmailSendDate.Replace("-", "/");
                        }
                        if (model.EmailSendDate == null)
                        {
                            model.EmailDate = null;
                        }
                        else
                        {
                            model.EmailDate = Convert.ToDateTime(item.EmailSendDate);
                        }
                        model.Remark = item.Remark;
                        model.IsEmailSend = Convert.ToInt32(item.IsEmailSend);
                        model.SANNo = item.SANNo;
                        model.SANApproveBy = item.SANApproveBy;
                        model.ClaimApproveBy = item.ClaimApproveBy;
                        model.SANDate = Convert.ToDateTime(item.SANDate);
                        model.SANAmount = item.SANAmount;
                        model.ClaimSANAmount = item.ClaimSANAmount;
                        model.ClaimSANNo = item.ClaimSANNo;
                        model.ClaimSANDate = (item.ClaimSANDate != null ? Convert.ToDateTime(item.ClaimSANDate).ToString("dd/MM/yyyy") : null);
                        if (model.ClaimSANDate != null)
                        {
                            model.ClaimSANDate = model.ClaimSANDate.Replace("-", "/");
                        }
                        model.ClaimStatus = item.ClaimStatus;
                        model.ApproveRemark = item.ApproveRemark;
                        model.IsClaim = Convert.ToInt32(item.IsClaim);
                        model.IsSAN = Convert.ToInt32(item.IsSAN);
                        model.TransitId = Convert.ToInt64(item.TransitId);
                        ClaimList.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetRaiseInsuranceClaimListPvt", "Get Raise Insurance Claim List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ClaimList;
        }
        #endregion

        #region Get Map Inward Vehicle Raise Cncrn List
        public List<MapInwardVehicleForMobModel> GetMapInwardVehicleRaiseCncrnList(int BranchId, int CompId)
        {
            return GetMapInwardVehicleRaiseCncrnListPvt(BranchId, CompId);
        }
        private List<MapInwardVehicleForMobModel> GetMapInwardVehicleRaiseCncrnListPvt(int BranchId, int CompId)
        {
            string mainPath = ConfigurationManager.AppSettings["VhcleInspImgPath"];
            List<MapInwardVehicleForMobModel> concernList = new List<MapInwardVehicleForMobModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    concernList = _contextManager.usp_GetMapInwardVehicleRsolveCncrnList(BranchId, CompId).Select(v => new MapInwardVehicleForMobModel
                    {
                        PkId = v.pkId,
                        BranchId = v.BranchId,
                        CompId = v.CompId,
                        LRNo = v.LRNo,
                        LRDate = Convert.ToDateTime(v.LRDate),
                        InwardDate = Convert.ToDateTime(v.InwardDate),
                        TransporterId = Convert.ToInt32(v.TransporterId),
                        TransporterName = v.TransporterName,
                        TransporterNo = v.TransporterNo,
                        VehicleNo = v.VehicleNo,
                        DriverName = v.DriverName,
                        MobileNo = v.MobileNo,
                        NoOfCasesQty = Convert.ToInt32(v.NoOfCasesQty),
                        ActualNoOfCasesQty = Convert.ToInt32(v.ActualNoOfCasesQty),
                        ConcernRemark = v.ConcernRemark,
                        ConcernBy = Convert.ToInt32(v.ConcernBy),
                        Addedby = v.AddedBy,
                        AddedOn = Convert.ToDateTime(v.AddedOn),
                        ConcernUpdatedOn = Convert.ToDateTime(v.ConcernUpdatedOn),
                        IsClaim = Convert.ToInt32(v.IsClaim),
                        IsSAN = Convert.ToInt32(v.IsSAN),
                        ResolvedBy = Convert.ToInt32(v.ResolvedBy),
                        IsConcern = Convert.ToInt32(v.IsConcern),
                        TransitId = Convert.ToInt64(v.TransitId),
                        Img1 = (v.Img1 == "" || v.Img1 == null ? "" : mainPath + v.Img1),
                        Img2 = (v.Img2 == "" || v.Img2 == null ? "" : mainPath + v.Img2),
                        Img3 = (v.Img3 == "" || v.Img3 == null ? "" : mainPath + v.Img3),
                        Img4 = (v.Img4 == "" || v.Img4 == null ? "" : mainPath + v.Img4),
                    }).OrderByDescending(v => v.PkId).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetMapInwardVehicleRaiseCncrnListPvt", "Get Map Inward Vehicle Raise Cncrn List (BranchId: " + BranchId + "  CompanyId: " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return concernList;
        }
        #endregion

        #region Resolve Vehicle Issue
        public int ResolveVehicleIssue(RaiseInsuranceClaimModel model)
        {
            return ResolveVehicleIssuePvt(model);
        }
        private int ResolveVehicleIssuePvt(RaiseInsuranceClaimModel model)
        {

            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    RetValue = _contextManager.usp_ResolveVehicleIssue(model.pkId, model.BranchId, model.CompId,
                    model.IsConcern,model.ActualNoOfCasesQty,model.NoOfCasesQty,model.ResolveVehicleRemark, model.AddedBy, obj);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveVehicleIssuePvt", "Resolve Vehicle Issue Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region After Approval Stock Adjustment Note (SAN)
        public int ApproveSANAdd(ApproveSAN model)
        {
            return ApproveSANAddPvt(model);
        }
        private int ApproveSANAddPvt(ApproveSAN model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    RetValue = _contextManager.usp_SANApproveAdd(model.TransitId, model.ClaimId, model.BranchId, model.CompId, model.SANNo, model.SANApproveBy, model.SANDate, model.SANRemark, obj);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ApproveSANAddPvt", "Approve SAN Add Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region After Approval Claim 
        public int ApproveClaimAdd(ApproveClaim model)
        {
            return ApproveClaimAddPvt(model);
        }
        private int ApproveClaimAddPvt(ApproveClaim model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    RetValue = _contextManager.usp_ClaimApproveAdd(model.TransitId, model.BranchId, model.CompId, model.ClaimId, model.ClaimApproveBy, model.ApproveClaimDate, model.ClaimRemark, obj);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ApproveClaimAddPvt", "Approve Claim Add Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Send Email for Claim Approvals
        public string SendEmailForClaimApprove(string ClaimNo, DateTime ApproveClaimDate, int BranchId, int CompId)
        {
            bool bResult = false;
            string msg = string.Empty, messageformat = string.Empty, Date = string.Empty, Subject = string.Empty, MailFilePath = string.Empty, ToEmail = string.Empty, CCEmail = string.Empty, EmailCC = string.Empty;
            EmailSend Email = new EmailSend();
            List<CCEmailDtls> EmailCntDtls = new List<CCEmailDtls>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    Date = DateTime.Today.Date.ToString("dd-MM-yyyy");
                    var date = ApproveClaimDate.Date.ToString("dd-MM-yyyy");
                    Subject = ConfigurationManager.AppSettings["ClaimApproved"] + Date + " ";
                    MailFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailFile\\SendEmailForApproveClaim.html");
                    ChequeAccountingRepository _ChequeAccountingRepository = new ChequeAccountingRepository(_contextManager);
                    EmailCntDtls = _ChequeAccountingRepository.GetCCEmailDtlsPvt(BranchId, CompId, 5);
                    if (EmailCntDtls.Count > 0)
                    {
                        for (int i = 0; i < EmailCntDtls.Count; i++)
                        {
                            CCEmail += ";" + EmailCntDtls[i].Email;
                        }
                        EmailCC = CCEmail.TrimStart(';');
                        ToEmail = EmailCC;
                    }
                    bResult = EmailNotification.SendEmailForClaimApprove(ToEmail, EmailCC, Subject, ClaimNo, ApproveClaimDate, MailFilePath);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendEmailForClaimApprove", "ClaimApproved", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
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

        #region Get Transit Data List For Mobile
        public List<ImportTransitListModel> GetTransitDataListForMob(int BranchId, int CompId)
        {
            return GetTransitDataListForMobPvt(BranchId, CompId);
        }
        private List<ImportTransitListModel> GetTransitDataListForMobPvt(int BranchId, int CompId)
        {
            List<ImportTransitListModel> TransitDataLst = new List<ImportTransitListModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    TransitDataLst = _contextManager.usp_GetTransitDataListForMob(BranchId, CompId)
                        .Select(c => new ImportTransitListModel
                        {
                            TransitId = c.TransitId,
                            BranchId = c.BranchId,
                            CompId = c.CompId,
                            DeliveryNo = c.DeliveryNo,
                            ActualGIDate = Convert.ToDateTime(c.ActualGIDate).Date.ToString("dd-MM-yyyy"),
                            RecPlant = c.RecPlant,
                            RecPlantDesc = c.RecPlantDesc,
                            DispPlant = c.DispPlant,
                            DispPlantDesc = c.DispPlantDesc,
                            InvoiceNo = c.InvNo,
                            InvoiceDate = Convert.ToDateTime(c.InvoiceDate).Date.ToString("dd-MM-yyyy"),
                            MaterialNo = c.MaterialNo,
                            MatDesc = c.MatDesc,
                            UOM = c.UOM,
                            BatchNo = c.BatchNo,
                            Quantity = c.Quantity,
                            TransporterId = Convert.ToInt32(c.TransporterId),
                            TransporterNo = c.TransporterNo,
                            TransporterName = c.TransporterName,
                            LrNo = c.LrNo,
                            LrDate = Convert.ToDateTime(c.LrDate).Date.ToString("dd-MM-yyyy"),
                            TotalCaseQty = c.TotalCaseQty,
                            VehicleNo = c.VehicleNo,
                            AddedBy = Convert.ToInt32(c.AddedBy),
                            AddedOn = Convert.ToDateTime(c.AddedOn).ToString("dd-MM-yyyy"),
                            TransitLRStatus = Convert.ToInt32(c.TransitLRStatus),
                            IsMapDone = Convert.ToInt32(c.IsMapDone),
                            RaiseConcernId = Convert.ToInt32(c.RaiseConcernId),
                            RaiseConcernBy = Convert.ToString(c.RaiseConcernBy),
                            RaiseConcernRemarks = Convert.ToString(c.RaiseConcernRemarks),
                            RaiseConcernUpdatedOn = Convert.ToDateTime(c.RaiseConcernUpdatedOn),
                        }).OrderBy(x => x.InvoiceNo).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransitDataListForMobPvt", "Get Transit Data List For Mob Pvt " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransitDataLst;
        }
        #endregion

        #region Raise Concern For Transit Data
        public int RaiseConcernForTransitData(RaiseConcernForTransitDataModel model)
        {
            return RaiseConcernForTransitDataPvt(model);
        }
        private int RaiseConcernForTransitDataPvt(RaiseConcernForTransitDataModel model)
        {
            int RetValue = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    RetValue = _contextManager.usp_RaiseRequestForTransitData(model.BranchId, model.CompId, model.LrNo, model.InvoiceNo, model.Remark,model.AddedBy, objRetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "RaiseConcernForTransitDataPvt", "Raise Concern For Transit Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Add Edit Map Inward Vehicle with Transit LR Details
        public int MapInwardVehicleWithTransitLR(MapInwardVehicleForMobModel model)
        {
            return MapInwardVehicleWithTransitLRPvt(model);
        }
        private int MapInwardVehicleWithTransitLRPvt(MapInwardVehicleForMobModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    RetValue = _contextManager.usp_MapInwardVehicleWithTransitLRForMobile(model.PkId, model.BranchId, model.CompId, model.TransitId, model.LRNo, model.LRDate, model.InwardDate, model.TransporterId, model.VehicleNo, model.DriverName, model.MobileNo, model.NoOfCasesQty,
                model.ActualNoOfCasesQty, model.IsConcern, model.ConcernRemark, model.ConcernBy, model.Addedby, model.Action, obj);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "MapInwardVehicleWithTransitLRPvt", "Map Inward Vehicle With Transit LR", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Map Inward Vehicle With Transit LR For Mob
        public List<MapInwardVehicleForMobModel> GetMapInwardVehicleWithTransitLR(int BranchId, int CompId)
        {
            return GetMapInwardVehicleWithTransitLRPvt(BranchId, CompId);
        }
        private List<MapInwardVehicleForMobModel> GetMapInwardVehicleWithTransitLRPvt(int BranchId, int CompId)
        {
            List<MapInwardVehicleForMobModel> vehicleList = new List<MapInwardVehicleForMobModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    vehicleList = _contextManager.usp_GetMapInwardVehicleWithLRListForMob(BranchId, CompId).Select(v => new MapInwardVehicleForMobModel
                    {
                        TransitId = Convert.ToInt64(v.TransitId),
                        PkId = Convert.ToInt64(v.pkId),
                        BranchId = Convert.ToInt32(v.BranchId),
                        CompId = Convert.ToInt32(v.CompId),
                        LRNo = Convert.ToString(v.LRNo),
                        LRDate = Convert.ToDateTime(v.LRDate),
                        InwardDate = Convert.ToDateTime(v.InwardDate),
                        TransporterId = Convert.ToInt32(v.TransporterId),
                        TransporterName = Convert.ToString(v.TransporterName),
                        TransporterNo = Convert.ToString(v.TransporterNo),
                        VehicleNo = Convert.ToString(v.VehicleNo),
                        DriverName = Convert.ToString(v.DriverName),
                        MobileNo = Convert.ToString(v.MobileNo),
                        NoOfCasesQty = Convert.ToInt32(v.NoOfCasesQty),
                        ActualNoOfCasesQty = Convert.ToInt32(v.ActualNoOfCasesQty),
                        IsConcern = Convert.ToInt32(v.IsConcern),
                        ConcernRemark = Convert.ToString(v.ConcernRemark),
                        ConcernBy = Convert.ToInt32(v.ConcernBy),
                        Addedby = Convert.ToString(v.AddedBy),
                        AddedOn = Convert.ToDateTime(v.AddedOn),
                        ConcernUpdatedOn = Convert.ToDateTime(v.ConcernUpdatedOn),
                        ResolvedBy = Convert.ToInt32(v.ResolvedBy),
                        IsChecklistDone = Convert.ToInt32(v.IsChecklistDone)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetMapInwardVehicleWithTransitLRPvt", "Get Map Inward Vehicle With Transit LR", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return vehicleList;
        }
        #endregion

        #region Update Vehicle CheckList And Upload Img
        public int VehicleChecklistMstAdd(VehicleChecklistDtlsModel model)
        {
            return VehicleChecklistMstAddPvt(model);
        }
        private int VehicleChecklistMstAddPvt(VehicleChecklistDtlsModel model)
        {
            string Img1 = string.Empty, Img2 = string.Empty, Img3 = string.Empty, Img4 = string.Empty;
            int RetValue = 0;
            var Data = JsonConvert.DeserializeObject<List<ChecklistDtlsModel>>(model.CLQueId);
            List<ChecklistDtlsModel> modelList = new List<ChecklistDtlsModel>();
            for (int i = 0; i < Data.Count; i++)
            {
                ChecklistDtlsModel checklistModel = new ChecklistDtlsModel();
                checklistModel.pkId = i + 1;
                checklistModel.CLQueId = Data[i].CLQueId;
                checklistModel.AnsText = Data[i].AnsText;
                modelList.Add(checklistModel);
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("pkId");
            dt.Columns.Add("CLQueId");
            dt.Columns.Add("AnsText");

            foreach (var item in modelList)
            {
                dt.Rows.Add(item.pkId, item.CLQueId, item.AnsText);
            }
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    if (dt.Rows.Count > 0)
                    {
                        using (var db = new CFADBEntities())
                        {
                            {
                                SqlConnection connection = (SqlConnection)db.Database.Connection;
                                SqlCommand cmd = new SqlCommand("CFA.usp_InvInVehicleChecklistMstAdd", connection);
                                cmd.CommandType = CommandType.StoredProcedure;
                                SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
                                BranchIdParameter.SqlDbType = SqlDbType.Int;
                                SqlParameter CompIdParameter = cmd.Parameters.AddWithValue("@CompId", model.CompId);
                                CompIdParameter.SqlDbType = SqlDbType.Int;
                                SqlParameter LREntryIdParameter = cmd.Parameters.AddWithValue("@LREntryId", model.LREntryId);
                                LREntryIdParameter.SqlDbType = SqlDbType.Int;
                                if (model.Img1 != null) { Img1 = model.Img1; }
                                else { Img1 = ""; }
                                if (model.Img2 != null) { Img2 = model.Img2; }
                                else { Img2 = ""; }
                                if (model.Img3 != null) { Img3 = model.Img3; }
                                else { Img3 = ""; }
                                if (model.Img4 != null) { Img4 = model.Img4; }
                                else { Img4 = ""; }
                                SqlParameter Img1Parameter = cmd.Parameters.AddWithValue("@Img1", Img1);
                                Img1Parameter.SqlDbType = SqlDbType.NVarChar;
                                SqlParameter Img2Parameter = cmd.Parameters.AddWithValue("@Img2", Img2);
                                Img2Parameter.SqlDbType = SqlDbType.NVarChar;
                                SqlParameter Img3Parameter = cmd.Parameters.AddWithValue("@Img3", Img3);
                                Img3Parameter.SqlDbType = SqlDbType.NVarChar;
                                SqlParameter Img4Parameter = cmd.Parameters.AddWithValue("@Img4", Img4);
                                Img4Parameter.SqlDbType = SqlDbType.NVarChar;
                                SqlParameter AddedByParameter = cmd.Parameters.AddWithValue("@AddedBy", model.AddedBy);
                                AddedByParameter.SqlDbType = SqlDbType.NVarChar;
                                SqlParameter TransitIdParameter = cmd.Parameters.AddWithValue("@TransitId", model.TransitId);
                                TransitIdParameter.SqlDbType = SqlDbType.BigInt;
                                SqlParameter ImportInvDataParameter = cmd.Parameters.AddWithValue("@tblData", dt);
                                ImportInvDataParameter.SqlDbType = SqlDbType.Structured;
                                if (connection.State == ConnectionState.Closed)
                                    connection.Open();
                                SqlDataReader dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    RetValue = (int)dr["RetValue"] + model.RetValue;
                                }
                                if (connection.State == ConnectionState.Open)
                                    connection.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "VehicleChecklistMstAddPvt", "Vehicle Checklist Mst Add Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Vehicle Checklist Details For Mobile
        public List<ChckelistDtls> GetVehicleChecklistDetailsForMob(int BranchId, int CompId)
        {
            return GetVehicleChecklistDetailsForMobPvt(BranchId, CompId);
        }
        private List<ChckelistDtls> GetVehicleChecklistDetailsForMobPvt(int BranchId, int CompId)
        {
            string mainPath = ConfigurationManager.AppSettings["VhcleInspImgPath"];
            List<ChckelistDtls> checklistDetails = new List<ChckelistDtls>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    checklistDetails = _contextManager.usp_GetVehChklistDtls_Mob(BranchId, CompId).Select(a => new ChckelistDtls
                    {
                        TransitId = Convert.ToInt64(a.TransitId),
                        BranchId = Convert.ToInt32(a.BranchId),
                        CompId = Convert.ToInt32(a.CompId),
                        ChkListMstId = a.ChkListMstId,
                        LREntryId = Convert.ToInt32(a.LREntryId),
                        Img1 = (a.Img1 == "" ? "" : mainPath + a.Img1),
                        Img2 = (a.Img2 == "" ? "" : mainPath + a.Img2),
                        Img3 = (a.Img3 == "" ? "" : mainPath + a.Img3),
                        Img4 = (a.Img4 == "" ? "" : mainPath + a.Img4),
                        AddedBy = a.AddedBy,
                        AddedOn = Convert.ToDateTime(a.AddedOn),
                        LRNo = a.LRNo,
                        LRDate = Convert.ToDateTime(a.LRDate),
                        VehicleNo = a.VehicleNo,
                        TransporterId = Convert.ToInt32(a.TransporterId),
                        TransporterName = a.TransporterName,
                        TransporterNo = a.TransporterNo,
                        IsChecklistDone = Convert.ToInt32(a.IsChecklistDone)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetVehicleChecklistDetailsForMobPvt", "Get Vehicle Checklist Details For Mob Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return checklistDetails;
        }
        #endregion

        #region Delete Vehicle Checklist Details For Mob
        public int DeleteVehicleChecklistDetailsForMob(VehicleChecklistDtlsModel model)
        {
            return DeleteVehicleChecklistDetailsForMobPvt(model);
        }
        private int DeleteVehicleChecklistDetailsForMobPvt(VehicleChecklistDtlsModel model)
        {
            int RetValue = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    RetValue = _contextManager.usp_DeleteVehicleChecklistDetailsForMob(model.TransitId, model.BranchId, model.CompId, model.ChkListMstId, objRetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "DeleteVehicleChecklistDetailsForMobPvt", "Delete Vehicle Checklist Details For Mob Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Raise Concern Of Transit Data for mobile
        public List<RaiseConcernForTransitData> GetRaiseConcernListForMob(int BranchId, int CompId)
        {
            return GetRaiseConcernListForMobPvt(BranchId, CompId);
        }
        private List<RaiseConcernForTransitData> GetRaiseConcernListForMobPvt(int BranchId, int CompId)
        {
            List<RaiseConcernForTransitData> concernList = new List<RaiseConcernForTransitData>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    concernList = _contextManager.usp_GetRaiseConcernListforMob(BranchId, CompId).Select(v => new RaiseConcernForTransitData
                    {
                        RaieseReqId = v.RaieseReqId,
                        BranchId = v.BranchId,
                        CompId = v.CompId,
                        LrNo = v.LrNo,
                        InvoiceNo = v.InvoiceNo,
                        Remark = v.Remark,
                        AddedBy = Convert.ToInt32(v.AddedBy),
                        AddedOn = Convert.ToDateTime(v.AddedOn),
                        ResolvedBy = Convert.ToInt32(v.ResolvedBy),
                        TransitId = Convert.ToInt64(v.TransitId)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetRaiseConcernListForMobPvt", "Get Raise Concern Of Transit Data for mobile Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return concernList;
        }
        #endregion

        #region Get Inv Inward Pages All Count
        public InvInwardAllCountModel GetInvInwardPagesAllCount(InvInwardAllCountModel model)
        {
            return GetInvInwardPagesAllCountPvt(model);
        }
        private InvInwardAllCountModel GetInvInwardPagesAllCountPvt(InvInwardAllCountModel model)
        {
            InvInwardAllCountModel Count = new InvInwardAllCountModel();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    Count = _contextManager.usp_InvInwardPageCounts(model.BranchId, model.CompId).Select(c => new InvInwardAllCountModel
                    {
                        TodayLR = c.TodayLR,
                        TodayVehicleMapped = c.TodayVehicleMapped,
                        TodayChklistDone = c.TodayChklistDone,
                        TodayConcernRaised = c.TodayConcernRaised,
                        TotalClaimRaised = c.TotalClaimRaised,
                        TotalSANRaised = c.TotalSANRaised,
                        TodayMapConcernRaised = c.TodayMapConcernRaised,
                        TodayMapConcernResolved = c.TodayMapConcernResolved,
                        TotalClaimApproved = c.TotalClaimApproved,
                        TotalSANApproved = c.TotalSANApproved,
                        PendingClaim = c.PendingClaim,
                        PendingSAN = c.PendingSAN,
                        TotalClaimSAN = c.TotalClaimSAN,
                        PendingClaimSANApproved = c.PendingClaimSANApproved,
                        TotalTodaysMapCnrnRaise = c.TotalTodaysMapCnrnRaise
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvInwardPagesAllCountPvt", "Get Raise Concern for Mob Pvt (BranchId: " + model.BranchId + " CompanyId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Count;
        }
        #endregion

        #region Resolve Raised Concern At Op Level
        public int ResolveRaisedConcernAtOpLevel(RaiseConcernForTransitData model)
        {
            return ResolveRaisedConcernAtOpLevelPvt(model);
        }
        private int ResolveRaisedConcernAtOpLevelPvt(RaiseConcernForTransitData model)
        {
            int result = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    result = _contextManager.usp_ResolveRaisedConcernAtOpLevel(model.RaieseReqId, model.BranchId, model.CompId, model.AddedBy, objRetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveRaisedConcernAtOpLevelPvt", "Resolve Raised Concern At Op Level Pvt (BranchId: " + model.BranchId + " CompanyId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return result;
        }
        #endregion

        #region  Inventory Inward Dashbord Count for supervisor
        public dashbordcountmodel GetInventoryCountForSupervisor(int BranchId, int CompanyId)
        {
            return GetInventoryCountForSupervisorPvt(BranchId, CompanyId);
        }
        private dashbordcountmodel GetInventoryCountForSupervisorPvt(int BranchId, int CompanyId)
        {
            dashbordcountmodel dashcount = new dashbordcountmodel();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    dashcount = _contextManager.usp_DashbordInvInwardCntNew(BranchId, CompanyId).Select(s => new dashbordcountmodel
                    {
                        TotalVeh = Convert.ToInt32(s.TotalVeh),
                        TodayVeh = Convert.ToInt32(s.TodayVeh),
                        TotalCaseQty = Convert.ToInt32(s.TotalCaseQty),
                        TodayCaseQty = Convert.ToInt32(s.TodayCaseQty),
                        TodayClaimCnt = Convert.ToInt32(s.TodayClaimCnt),
                        PendClaimCnt = Convert.ToInt32(s.PendClaimCnt),
                        TodaySANCnt = Convert.ToInt32(s.TodaySANCnt),
                        PendSANCnt = Convert.ToInt32(s.PendSANCnt),
                        CummVehicle = Convert.ToInt32(s.CummVehicle),
                        CummBoxes = Convert.ToInt32(s.CummBoxes)
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInventoryCountForSupervisor", "Get Inventory Count For Supervisor", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return dashcount;
        }

        #endregion

        #region Get Inventory Inwrd dashboard Filtered List
        public List<DashboardInventoryInwrdMdal> GetListForDashboardInventoryInwrdList(int BranchId, int CompanyId)
        {
            return GetListForDashboardInventoryInwrdListPvt(BranchId, CompanyId);
        }
        private List<DashboardInventoryInwrdMdal> GetListForDashboardInventoryInwrdListPvt(int BranchId, int CompanyId)
        {
            List<DashboardInventoryInwrdMdal> GetListForDashboardInventoryInwrd = new List<DashboardInventoryInwrdMdal>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    GetListForDashboardInventoryInwrd = _contextManager.usp_DashbordInvInwardListNew(BranchId, CompanyId).Select(c => new DashboardInventoryInwrdMdal
                    {
                        TransitId = Convert.ToInt64(c.TransitId),
                        InvNo = c.InvNo,
                        InvoiceDate = Convert.ToDateTime(c.InvoiceDate),
                        LrNo = c.LrNo,
                        LrDate = Convert.ToDateTime(c.LrDate).ToString("yyyy-MM-dd"),
                        TransporterId = Convert.ToInt32(c.TransporterId),
                        TransporterNo = c.TransporterNo,
                        TransporterName = c.TransporterName,
                        VehicleNo = c.VehicleNo,
                        AddedOn = Convert.ToDateTime(c.AddedOn).ToString("yyyy-MM-dd"),
                        ClaimNo = c.ClaimNo,
                        ClaimDate = Convert.ToDateTime(c.ClaimDate).ToString("yyyy-MM-dd"),
                        ClaimAmount = c.ClaimAmount,
                        SANNo = c.SANNo,
                        SANDate = Convert.ToDateTime(c.SANDate).ToString("yyyy-MM-dd"),
                        ClaimApproveBy = c.ClaimApproveBy,
                        SANApproveBy = c.SANApproveBy,
                        ClaimSANNo = c.ClaimSANNo,
                        ClaimSANDate = Convert.ToDateTime(c.ClaimSANDate)
                    }).OrderByDescending(x => x.TransitId).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetListForDashboardInventoryInwrdList", "GetListForDashboardInventoryInwrdList" + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GetListForDashboardInventoryInwrd;
        }
        #endregion

        #region Start - Inward Supervisor Dashboard Mob
        public List<InwardSupervisorDashboardMobModel> InwardSupervisorDashboardMob(int BranchId, int CompId)
        {
            return InwardSupervisorDashboardMobPvt(BranchId, CompId);
        }
        private List<InwardSupervisorDashboardMobModel> InwardSupervisorDashboardMobPvt(int BranchId, int CompId)
        {
            string mainPath = ConfigurationManager.AppSettings["VhcleInspImgPath"];
            List<InwardSupervisorDashboardMobModel> modelList = new List<InwardSupervisorDashboardMobModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    modelList = _contextManager.usp_InwardSupervisorDashboard_Mob(BranchId, CompId).Select(i => new InwardSupervisorDashboardMobModel
                    {
                        TransitId = Convert.ToInt64(i.TransitId),
                        BranchId = Convert.ToInt32(i.BranchId),
                        CompId = Convert.ToInt32(i.CompId),
                        InvNo = Convert.ToString(i.InvNo),
                        InvoiceDate = Convert.ToDateTime(i.InvoiceDate),
                        Quantity = Convert.ToString(i.Quantity),
                        TransporterId = Convert.ToInt32(i.TransporterId),
                        TransporterNo = Convert.ToString(i.TransporterNo),
                        TransporterName = Convert.ToString(i.TransporterName),
                        LrNo = Convert.ToString(i.LrNo),
                        LrDate = Convert.ToDateTime(i.LrDate),
                        TotalCaseQty = Convert.ToString(i.TotalCaseQty),
                        VehicleNo = Convert.ToString(i.VehicleNo),
                        IsMapDone = Convert.ToInt32(i.IsMapDone),
                        mivpkId = Convert.ToInt64(i.mivpkId),
                        mivInwardDate = Convert.ToDateTime(i.mivInwardDate),
                        mivVehicleNo = Convert.ToString(i.mivVehicleNo),
                        mivDriverName = Convert.ToString(i.mivDriverName),
                        mivMobileNo = Convert.ToString(i.mivMobileNo),
                        mivNoOfCasesQty = Convert.ToInt32(i.mivNoOfCasesQty),
                        mivActualNoOfCasesQty = Convert.ToInt32(i.mivActualNoOfCasesQty),
                        mivConcernRemark = Convert.ToString(i.mivConcernRemark),
                        mivConcernUpdatedOn = Convert.ToDateTime(i.mivConcernUpdatedOn),
                        mivIsConcern = Convert.ToInt32(i.mivIsConcern),
                        mivResolvedBy = Convert.ToInt32(i.mivResolvedBy),
                        IsChecklistDone = Convert.ToInt32(i.IsChecklistDone),
                        ChkListMstId = Convert.ToInt64(i.ChkListMstId),
                        LREntryId = Convert.ToInt32(i.LREntryId),
                        Img1 = (i.Img1 == "" || i.Img1 == null ? "" : mainPath + i.Img1),
                        Img2 = (i.Img2 == "" || i.Img2 == null ? "" : mainPath + i.Img2),
                        Img3 = (i.Img3 == "" || i.Img3 == null ? "" : mainPath + i.Img3),
                        Img4 = (i.Img4 == "" || i.Img4 == null ? "" : mainPath + i.Img4),
                        AddedBy = i.AddedBy,
                        AddedOn = Convert.ToDateTime(i.AddedOn),
                        ConcernBy = Convert.ToInt32(i.ConcernBy)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "InwardSupervisorDashboardMobPvt", "Inward Supervisor Dashboard Mob", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return modelList;
        }
        #endregion End - Inward Supervisor Dashboard Mob

        #region Get Inv Inward Cumm Vehicle List
        public List<DashboardInventoryInwrdMdal> GetInvInwardCummVehicleList(int BranchId, int CompId)
        {
            return GetInvInwardCummVehicleListPvt(BranchId, CompId);
        }
        private List<DashboardInventoryInwrdMdal> GetInvInwardCummVehicleListPvt(int BranchId, int CompId)
        {
            List<DashboardInventoryInwrdMdal> CummVehicleLst = new List<DashboardInventoryInwrdMdal>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    CummVehicleLst = _contextManager.usp_DashInvInwardCummVehicleListNew(BranchId, CompId).Select(c => new DashboardInventoryInwrdMdal
                    {
                        TransitId = Convert.ToInt64(c.TransitId),
                        InvNo = c.InvNo,
                        InvoiceDate = Convert.ToDateTime(c.InvoiceDate),
                        LrNo = c.LrNo,
                        LrDate = Convert.ToDateTime(c.LrDate).ToString("yyyy-MM-dd"),
                        TransporterId = Convert.ToInt32(c.TransporterId),
                        TransporterNo = c.TransporterNo,
                        TransporterName = c.TransporterName,
                        VehicleNo = c.VehicleNo,
                        AddedOn = Convert.ToDateTime(c.AddedOn).ToString("yyyy-MM-dd"),
                        TotalCaseQty = Convert.ToInt32(c.TotalCaseQty)
                    }).OrderByDescending(x => x.TransitId).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvInwardCummVehicleListPvt", "GetInvInwardCummVehicleListPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return CummVehicleLst;
        }
        #endregion

        #region Get List Transit Vehicle Checlist For View Image
        public List<VhcleChkLstForViewImgModel> TransitVhcleChkLstForViewImg (int BranchId, int CompId, DateTime FromDate, DateTime ToDate)
        {
            return TransitVhcleChkLstForViewImgPvt(BranchId, CompId, FromDate, ToDate);
        }
        private List<VhcleChkLstForViewImgModel> TransitVhcleChkLstForViewImgPvt(int BranchId, int CompId, DateTime FromDate, DateTime ToDate)
        {
            List<VhcleChkLstForViewImgModel> vhcleChkLsts = new List<VhcleChkLstForViewImgModel>();
            try
            {
                string mainPath = ConfigurationManager.AppSettings["VhcleInspImgPath"];
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    vhcleChkLsts = _contextManager.usp_TransitVhcleChkLstForViewImg(BranchId, CompId, FromDate, ToDate).Select(c => new VhcleChkLstForViewImgModel
                    {
                        pkId = c.pkId,
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        LRNo = c.LRNo,
                        LRDate =Convert.ToDateTime(c.LRDate),
                        InwardDate =Convert.ToDateTime(c.InwardDate),
                        TransporterId =Convert.ToInt32(c.TransporterId),
                        VehicleNo = c.VehicleNo,
                        DriverName = c.DriverName,
                        MobileNo =c.MobileNo,
                        NoOfCasesQty =Convert.ToInt32(c.NoOfCasesQty),
                        ActualNoOfCasesQty = Convert.ToInt32(c.ActualNoOfCasesQty),
                        ConcernRemark = c.ConcernRemark,
                        AddedOn = Convert.ToDateTime(c.AddedOn),
                        AddedBy =c.AddedBy,
                        ConcernBy = Convert.ToInt32(c.ConcernBy),
                        ConcernUpdatedOn = Convert.ToDateTime(c.ConcernUpdatedOn),
                        IsConcern = Convert.ToInt32(c.IsConcern),
                        TransporterNo =c.TransporterNo,
                        TransporterName =c.TransporterName,
                        IsClaim = Convert.ToInt32(c.IsClaim),
                        IsSAN= Convert.ToInt32(c.IsSAN),
                        ResolvedBy= Convert.ToInt32(c.ResolvedBy),
                        TransitId= Convert.ToInt32(c.TransitId),
                        Img1 = (c.Img1 == "" || c.Img1 == null ? "" : mainPath + c.Img1),
                        Img2 = (c.Img2 == "" || c.Img2 == null ? "" : mainPath + c.Img2),
                        Img3 = (c.Img3 == "" || c.Img3 == null ? "" : mainPath + c.Img3),
                        Img4 = (c.Img4 == "" || c.Img4 == null ? "" : mainPath + c.Img4),
                    }).OrderByDescending(x => x.TransitId).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "TransitVhcleChkLstForViewImgPvt", "TransitVhcleChkLstForViewImgPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return vhcleChkLsts;
        }
        #endregion

        #region Get Owner Inv Inward Dash Smmry List
        public List<OwnInvInwardDashSmmryList> GetOwnerInvInwardDashSmmryList()
        {
            return GetOwnerInvInwardDashSmmryListPvt();
        }
        private List<OwnInvInwardDashSmmryList> GetOwnerInvInwardDashSmmryListPvt()
        {
            List<OwnInvInwardDashSmmryList> smmryList = new List<OwnInvInwardDashSmmryList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    smmryList = _contextManager.usp_OwnerInvInwardDashSmmryList().Select(c => new OwnInvInwardDashSmmryList
                    {
                        BranchId = c.BranchId,
                        BranchName = c.BranchName,
                        CompId = c.CompId,
                        CompanyName = c.CompanyName,
                        PendSANCnt = c.PendSANCnt,
                        PendClaimCnt = c.PendClaimCnt
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerInvInwardDashSmmryListPvt", "Get Owner Inv Inward Dash Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return smmryList;
        }
        #endregion

        // Not Used
        //#region InsuranceClaim AddEdit AddEdit
        //public int InsuranceClaimAddEdit(InsuranceClaimModel model)
        //{
        //    return InsuranceClaimAddEditPvt(model);
        //}
        //private int InsuranceClaimAddEditPvt(InsuranceClaimModel model)
        //{

        //    int RetValue = 0;
        //    ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
        //    try
        //    {
        //        RetValue = _contextManager.usp_InsuranceClaimAddEdit(model.ClaimId, model.BranchId, model.CompId, model.InvoiceId,
        //            model.ClaimNo, model.ClaimDate, model.ClaimAmount, model.ClaimType, model.DebitNote, model.DebitDate,
        //            model.DebitAmount, model.Remark, model.Addedby, model.Action, obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "InsuranceClaimAddEditPvt", "InsuranceClaim AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return RetValue;
        //}
        //#endregion

        //#region Insurance Claim List
        //public List<InsuranceClaimModel> GetInsuranceClaimList(int BranchId, int CompId)
        //{
        //    return GetGetInsuranceClaimListPvt(BranchId, CompId);
        //}
        //private List<InsuranceClaimModel> GetGetInsuranceClaimListPvt(int BranchId, int CompId)
        //{
        //    List<InsuranceClaimModel> modelList = new List<InsuranceClaimModel>();
        //    try
        //    {
        //        modelList = _contextManager.usp_GetInsuranceClaimList(BranchId, CompId).Select(i => new InsuranceClaimModel
        //        {
        //            BranchId = i.BranchId,
        //            ClaimId = i.ClaimId,
        //            InvoiceId = i.InvoiceId,
        //            InvNo = i.InvNo,
        //            ClaimNo = i.ClaimNo,
        //            ClaimDate = Convert.ToDateTime(i.ClaimDate),
        //            ClaimAmount = i.ClaimAmount,
        //            DebitAmount = i.DebitAmount,
        //            ClaimType = i.ClaimType,
        //            ClaimTypeId = Convert.ToInt64(i.ClaimTypeId),
        //            DebitDate = Convert.ToDateTime(i.DebitDate),
        //            DebitNote = i.DebitNote,
        //            ClaimStatus = i.ClaimStatus,
        //            Remark = i.Remark,
        //            AddedBy = Convert.ToInt32(i.AddedBy),
        //            AddedOn = Convert.ToDateTime(i.AddedOn),
        //            LastUpdatedDate = Convert.ToDateTime(i.LastUpdatedDate),
        //            IsEmail = Convert.ToBoolean(i.IsEmail)
        //        }).OrderByDescending(x => x.ClaimId).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetGetInsuranceClaimListPvt", "Get Insurance Claim List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return modelList;
        //}
        //#endregion

        //#region Get Invoice List
        //public List<InvoiceListModel> GetInvoiceList(int BranchId, int CompId)
        //{
        //    return GetInvoiceListPvt(BranchId, CompId);
        //}
        //private List<InvoiceListModel> GetInvoiceListPvt(int BranchId, int CompId)
        //{
        //    List<InvoiceListModel> modelList = new List<InvoiceListModel>();
        //    try
        //    {
        //        modelList = _contextManager.usp_GetInsuranceClaimInvoiceList(BranchId, CompId).Select(b => new InvoiceListModel
        //        {
        //            BranchId = b.BranchId,
        //            CompId = b.CompId,
        //            InvId = b.InvId,
        //            InvNo = b.InvNo
        //        }).OrderByDescending(x => x.InvNo).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetInvoiceListPvt", "Get Invoice List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return modelList;
        //}
        //#endregion

        //#region Get Insurance Claim Invoice By Id List
        //public List<InsuranceClaimModel> GetInsuranceClaimInvByIdList(int BranchId, int CompanyId, string InvoiceId)
        //{
        //    return GetInsuranceClaimInvByIdListPvt(BranchId, CompanyId, InvoiceId);
        //}
        //private List<InsuranceClaimModel> GetInsuranceClaimInvByIdListPvt(int BranchId, int CompanyId, string InvoiceId)
        //{
        //    List<InsuranceClaimModel> modelList = new List<InsuranceClaimModel>();
        //    try
        //    {
        //        modelList = _contextManager.usp_GetInsuranceClaimInvById(BranchId, CompanyId, InvoiceId).Select(b => new InsuranceClaimModel
        //        {
        //            BranchId = b.BranchId,
        //            CompId = b.CompId,
        //            InvoiceId = b.InvoiceId,
        //            ClaimId = b.ClaimId,
        //            ClaimNo = b.ClaimNo,
        //            ClaimDate = Convert.ToDateTime(b.ClaimDate),
        //            ClaimAmount = b.ClaimAmount,
        //        }).OrderByDescending(x => x.InvoiceId).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetClaimTypeListPvt", "Get Claim Type List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return modelList;
        //}
        //#endregion

        //#region Send Email For approval Update Alert
        //public List<InsuranceClaimModel> GetInsuranceClmDtlsForEmail(int BranchId, int CompId, int ClaimId)
        //{
        //    return GetInsuranceClmDtlsForEmailPvt(BranchId, CompId, ClaimId);
        //}
        //private List<InsuranceClaimModel> GetInsuranceClmDtlsForEmailPvt(int BranchId, int CompId, int ClaimId)
        //{
        //    List<InsuranceClaimModel> EmailDtls = new List<InsuranceClaimModel>();
        //    try
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmDtlsForEmailPvt", "Send Email For approval Update Alert", "START", "");

        //        EmailDtls = _contextManager.usp_GetInsuranceClaimByIdApprovalEmail(BranchId, CompId, ClaimId).Select(i => new InsuranceClaimModel
        //        {
        //            BranchId = i.BranchId,
        //            CompId = i.CompId,
        //            ClaimId = i.ClaimId,
        //            InvoiceId = i.InvoiceId,
        //            InvNo = i.InvNo,
        //            ClaimNo = i.ClaimNo,
        //            ClaimDate = Convert.ToDateTime(i.ClaimDate),
        //            ClaimAmount = i.ClaimAmount,
        //            DebitAmount = i.DebitAmount,
        //            ClaimType = i.ClaimType,
        //            DebitDate = Convert.ToDateTime(i.DebitDate),
        //            DebitNote = i.DebitNote,
        //            ClaimStatus = i.ClaimStatus,
        //            Remark = i.Remark,
        //            EmailId = i.EmailId,
        //            AddedBy = Convert.ToInt32(i.AddedBy),
        //            AddedOn = Convert.ToDateTime(i.AddedOn),
        //            LastUpdatedDate = Convert.ToDateTime(i.LastUpdatedDate),
        //            IsEmail = Convert.ToBoolean(i.IsEmail)
        //        }).OrderByDescending(x => x.InvoiceId).ToList();

        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmDtlsForEmailPvt", "Send Email For approval Update Alert", "END", "");
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmDtlsForEmailPvt", "Get Insurance Clm Dtls For Email Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return EmailDtls;
        //}
        //#endregion

        //#region Get Insurance Claim Approval Report Or mail Table format     
        //public string InsuranceClaimforApproval(List<InsuranceClaimModel> modelList, string MailFilePath)
        //{
        //    string Table = string.Empty, TableList = string.Empty, msgHtml = string.Empty;
        //    try
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "InsuranceClaimforApproval", "Get Insurance Claim Approval Report ", "START", "");
        //        msgHtml = File.OpenText(MailFilePath).ReadToEnd().ToString();
        //        Table = "";
        //        TableList = "";
        //        Table += "<table style='border-collapse: collapse;width: 52%; min-width: 400px;; white-space:nowrap;'>";
        //        Table += "<thead><tr style = 'font-family: Verdana; font-size: 12px; font-weight: bold; background-color:#3c8dbc; color:white;'>";
        //        Table += "<th style='border: 1px solid black;padding: 2px;text-align:center;'  width='10%' border='1'> Claim No. </th>";
        //        Table += "<th style='border: 1px solid black;padding: 2px;text-align:center;'  width='15%' border='1'> Claim Date </th>";
        //        Table += "<th style='border: 1px solid black;padding: 2px;text-align:center;'  width='10%' border='1'> Claim Amount </th>";
        //        Table += "</tr></thead><tbody>";
        //        TableList += Table;

        //        foreach (var item in modelList)
        //        {
        //            TableList += "<tr style='font-size:13px;text-align:center;color:black;'>";
        //            TableList += "<td style='border: 1px solid black;padding: 3px;' border='1'>" + item.ClaimNo + "</td>";
        //            TableList += "<td style='border: 1px solid black;padding: 10px;' width='10%' border='1'>" + item.ClaimDate +
        //                     "</td>";
        //            TableList += "<td style='border: 1px solid black;padding: 3px;' border='1'>" + item.ClaimAmount + "</td>";
        //            TableList += "</tr>";
        //        }
        //        TableList += "</tbody></table></br></br>";

        //        if (TableList != "" && TableList != null)
        //        {
        //            msgHtml = msgHtml.Replace("<!--ApprovalTableString-->", TableList);
        //        }
        //        BusinessCont.SaveLog(0, 0, 0, "InsuranceClaimforApproval", "Get Insurance Claim Approval Report", "END", "");
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "InsuranceClaimforApproval", "Get Insurance Claim Approval Report", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return msgHtml;
        //}
        //#endregion

        //#region Update Email Insurance Claim Approval Update
        //public string UpdateMailForApproval(int BranchId, int CompId, long ClaimId, bool IsEmailSend)
        //{
        //    return UpdateMailForApprovalPvt(BranchId, CompId, ClaimId, IsEmailSend);
        //}
        //private string UpdateMailForApprovalPvt(int BranchId, int CompId, long ClaimId, bool IsEmailSend)
        //{
        //    int RetValue = 0;
        //    ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
        //    try
        //    {
        //        RetValue = _contextManager.usp_UpdateInsuranceClaimByIdApprovalEmail(BranchId, CompId, ClaimId, IsEmailSend, obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "UpdateMailForApprovalPvt", "Update Mail For Approval Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    if (RetValue > 0)
        //    {
        //        return BusinessCont.SuccessStatus;
        //    }
        //    else
        //    {
        //        return BusinessCont.FailStatus;
        //    }

        //}
        //#endregion

        //#region Add/Edit Map Inward Vehicle for Mobile
        //public int MapInwardVehicleAddEditForMob(MapInwardVehicleForMobModel model)
        //{
        //    return MapInwardVehicleAddEditForMobPvt(model);
        //}
        //private int MapInwardVehicleAddEditForMobPvt(MapInwardVehicleForMobModel model)
        //{

        //    int RetValue = 0;
        //    ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
        //    try
        //    {
        //        RetValue = _contextManager.usp_MapInwardVehicleAddEditForMob(model.BranchId, model.CompId, model.InvId, model.InvoiceDate, model.InwardDate,
        //            model.TransporterId, model.MobileNo, model.DriverName, model.VehicleNo, model.Addedby, model.Action, obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "MapInwardVehicleAddEditForMobPvt", "MapInward Vehicle AddEdit For Mob Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return RetValue;
        //}
        //#endregion

        //#region Get Maping Inward Vehicle List For Mob
        //public List<MapInwardVehicleForMobModel> GetMapInwardVehicleListForMobs(int BranchId, int CompId)
        //{
        //    return GetMapInwardVehicleListPvt(BranchId, CompId);
        //}
        //private List<MapInwardVehicleForMobModel> GetMapInwardVehicleListPvt(int BranchId, int CompId)
        //{
        //    List<MapInwardVehicleForMobModel> modelList = new List<MapInwardVehicleForMobModel>();
        //    try
        //    {
        //        modelList = _contextManager.usp_GetMapInwardVehicleListForMob(BranchId, CompId).Select(i => new MapInwardVehicleForMobModel
        //        {
        //            BranchId = i.BranchId,
        //            CompId = i.CompId,
        //            InvId = i.InvId,
        //            InvoiceDate = Convert.ToDateTime(i.InvoiceDate),
        //            InwardDate = Convert.ToDateTime(i.InwardDate),
        //            TransporterId = Convert.ToInt32(i.TransporterId),
        //            MobileNo = i.MobileNo,
        //            DriverName = i.DriverName,
        //            VehicleNo = i.VehicleNo,
        //            InvNo = i.InvNo,
        //            TransporterNo = i.TransporterNo,
        //            TransporterName = i.TransporterName,
        //            Addedby = i.Addedby,
        //            AddedOn = Convert.ToDateTime(i.AddedOn),
        //            LastUpdatedOn = Convert.ToDateTime(i.LastUpdatedOn)
        //        }).OrderByDescending(x => x.InvId).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetMapInwardVehicleListPvt", "Get Map InwardVehicle List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return modelList;
        //}
        //#endregion

        //#region Inv In Vehicle Checklist Add Edit For Mob
        //public int VehicleCheckListAddEdits(VehicleChecklistModel model)
        //{
        //    return VehicleCheckListAddEditsPvt(model);
        //}
        //private int VehicleCheckListAddEditsPvt(VehicleChecklistModel model)
        //{
        //    int RetValue = 0;
        //    ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
        //    try
        //    {
        //        RetValue = _contextManager.usp_InvInVehicleChecklistAddEditForMob(model.BranchId, model.CompId, model.ChecklistTypeId,
        //            model.ChecklistType, model.InvId, model.InvoiceDate, model.TransporterId, model.VehicleNo, model.IsColdStorage,
        //            model.Remarks, model.SealNumber, model.Comments, model.Action, model.AddedBy, obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "VehicleCheckListAddEditsPvt", "Vehicle CheckList AddEdits Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return RetValue;
        //}
        //#endregion

        //#region Get Invoice In Vehicle Check List For Mob
        //public List<InvInVehicleCheckListmodel> GetInvInVehicleCheckList(int BranchId, int CompId)
        //{
        //    return GetInvInVehicleCheckListPvt(BranchId, CompId);
        //}
        //private List<InvInVehicleCheckListmodel> GetInvInVehicleCheckListPvt(int BranchId, int CompId)
        //{
        //    List<InvInVehicleCheckListmodel> InvInVehicleCheckLst = new List<InvInVehicleCheckListmodel>();
        //    try
        //    {
        //        using (CFADBEntities _contextManager = new CFADBEntities())
        //        {
        //            _contextManager.Database.CommandTimeout = 1000;
        //            InvInVehicleCheckLst = _contextManager.usp_GetInvInVehicleCheckListForMob(BranchId, CompId)
        //               .Select(c => new InvInVehicleCheckListmodel
        //               {
        //                   pkId = c.pkId,
        //                   BranchId = Convert.ToInt32(c.BranchId),
        //                   CompId = Convert.ToInt32(c.CompId),
        //                   ChecklistTypeId = Convert.ToInt32(c.ChecklistTypeId),//
        //                   ChecklistType = c.ChecklistType,
        //                   InvId = c.InvId,
        //                   InvNo = c.InvNo,
        //                   InvoiceDate = c.InvoiceDate,
        //                   TransporterId = c.TransporterId,
        //                   TransporterNo = c.TransporterNo,
        //                   TransporterName = c.TransporterName,
        //                   VehicleNo = c.VehicleNo,
        //                   IsColdStorage = c.IsColdStorage,
        //                   Status = c.Status,
        //                   Remarks = c.Remarks,
        //                   SealNumber = c.SealNumber,
        //                   Comments = c.Comments,
        //                   AddedBy = c.AddedBy,
        //                   AddedOn = Convert.ToDateTime(c.AddedOn),
        //                   LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn),
        //                   IsApprove = c.IsApprove
        //               }).OrderBy(x => x.VehicleNo).ToList();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetInvInVehicleCheckListPvt", " Get Invoice In Vehicle Check List" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return InvInVehicleCheckLst;
        //}
        //#endregion

        //#region Update Resolve Vehicle LR Issue
        //public int UpdateResolveVehicleLRIssue(UpdateResolveVehicleLRIssueModel model)
        //{
        //    return UpdateResolveVehicleLRIssuePvt(model);
        //}
        //private int UpdateResolveVehicleLRIssuePvt(UpdateResolveVehicleLRIssueModel model)
        //{

        //    int RetValue = 0;
        //    ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
        //    try
        //    {
        //        RetValue = _contextManager.usp_ResolveVehicleLRIssue(model.pkId, model.BranchId, model.CompId,
        //            model.IsConcern, model.IsApproveBy, obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "UpdateResolveVehicleLRIssuePvt", "Update Resolve Vehicle LR Issue Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return RetValue;
        //}
        //#endregion

        //#region Invoice Inward Raise Request By Id For Mobile
        //public string UpdateInvInwardRaiseRequestById(InvInwardRaiseRequestByIdForModel model)
        //{
        //    return UpdateInvInwardRaiseRequestByIdPvt(model);
        //}
        //private string UpdateInvInwardRaiseRequestByIdPvt(InvInwardRaiseRequestByIdForModel model)
        //{
        //    int RetValue = 0;
        //    ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
        //    try
        //    {
        //        RetValue = _contextManager.usp_UpdateInvInwardRaiseRequestByIdForMob(model.BranchId, model.CompId, model.InvId, model.Remarks, model.RaiseRequestBy, obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "UpdateInvInwardRaiseRequestByIdPvt", "Invoice Inward Raise Request By Id", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    if (RetValue > 0)
        //    {
        //        return BusinessCont.SuccessStatus;
        //    }
        //    else
        //    {
        //        return BusinessCont.FailStatus;
        //    }

        //}
        //#endregion

        //#region  Add/Edit and Delete LR Details  List For Mob

        //public string AddEditDeleteLrDetails(LRDetailsModel model)
        //{
        //    return AddEditDeleteLrDetailsPvt(model);
        //}
        //public string AddEditDeleteLrDetailsPvt(LRDetailsModel model)
        //{
        //    int RetValue = 0;
        //    ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
        //    try
        //    {
        //        RetValue = _contextManager.usp_InvInLRDetailsAddEditForMob(model.BranchId, model.CompId, model.InvId, model.InvoiceDate, model.LRNo, model.LRDate, model.NoOfCase, model.ActualNoOfCase, model.Remarks, model.Action, model.AddedBy, obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "AddEditDeleteLrDetails", "Add Edit Delete Lr Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    if (model.Action == "STATUS")
        //    {
        //        if (RetValue > 0)
        //        {
        //            return BusinessCont.msg_stsChange;
        //        }
        //        else
        //        {
        //            return BusinessCont.FailStatus;
        //        }
        //    }
        //    else
        //    {
        //        if (RetValue > 0)
        //        {
        //            return BusinessCont.SuccessStatus;
        //        }
        //        else if (RetValue == -1)
        //        {
        //            return BusinessCont.msg_exist;
        //        }
        //        else
        //        {
        //            return BusinessCont.FailStatus;
        //        }
        //    }
        //}

        //#endregion

        //#region Get LR List Details  List For Mob
        //public List<LRDetailsModel> GetLRDetailsList(int BranchId, int CompId)
        //{
        //    return GetLRDetailsListPvt(BranchId, CompId);
        //}
        //private List<LRDetailsModel> GetLRDetailsListPvt(int BranchId, int CompId)
        //{
        //    List<LRDetailsModel> LRDetailsLst = new List<LRDetailsModel>();
        //    try
        //    {
        //        using (CFADBEntities _contextManager = new CFADBEntities())
        //        {
        //            _contextManager.Database.CommandTimeout = 1000;
        //            LRDetailsLst = _contextManager.usp_GetInvInLRDetailsForMob(BranchId, CompId)    //need to update sp  
        //               .Select(c => new LRDetailsModel
        //               {
        //                   BranchId = c.BranchId,
        //                   CompId = c.CompId,
        //                   InvId = c.InvId,
        //                   InvoiceDate = c.InvoiceDate,
        //                   LRNo = c.LRNo,
        //                   LRDate = c.LRDate,
        //                   NoOfCase = c.NoOfCase,
        //                   ActualNoOfCase = c.ActualNoOfCase,
        //                   Remarks = c.ActualNoOfCase,
        //                   AddedBy = c.AddedBy,
        //                   AddedOn = Convert.ToDateTime(c.AddedOn),
        //                   LastUpdatedOn = Convert.ToDateTime(c.LastUpdatedOn)
        //               }).OrderBy(x => x.LRNo).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetLRDetailsListPvt", "Get LR List Details" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return LRDetailsLst;
        //}
        //#endregion       

        //#region Get Invoice In Vehicle Check List Master
        //public List<InvInVehicleChecklistMaster> GetInvInVehicleCheckListMaster(int BranchId, int CompId, string ChecklistType)
        //{
        //    return GetInvInVehicleCheckListMasterPvt(BranchId, CompId, ChecklistType);
        //}
        //private List<InvInVehicleChecklistMaster> GetInvInVehicleCheckListMasterPvt(int BranchId, int CompId, string ChecklistType)
        //{
        //    List<InvInVehicleChecklistMaster> InvInVehicleMasterLst = new List<InvInVehicleChecklistMaster>();
        //    try
        //    {
        //        using (CFADBEntities _contextManager = new CFADBEntities())
        //        {
        //            //_contextManager.Database.CommandTimeout = 1000;
        //            //InvInVehicleMasterLst = _contextManager.usp_GetInvInVehicleCheckListMaster(BranchId, CompId)//need to update sp  
        //            //   .Select(c => new InvInVehicleChecklistMaster
        //            //   {
        //            //       ChecklistTypeId = c.ChecklistTypeId,
        //            //       BranchId = Convert.ToInt32(c.BranchId),
        //            //       CompId = Convert.ToInt32(c.CompId),
        //            //       ChecklistType = c.ChecklistType,
        //            //       Section = c.Section,
        //            //       QuestionName = c.QuestionName,
        //            //       IsActive = c.IsActive,
        //            //       AddedBy = c.AddedBy,
        //            //       AddedOn = Convert.ToDateTime(c.AddedOn)
        //            //   }).ToList();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetInvInVehicleCheckListMasterPvt", " Get Invoice In Vehicle Check List Master" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return InvInVehicleMasterLst;
        //}
        //#endregion

        //#region Get Map Inward Vehicle Raise Cncrn List
        //public List<MapInwardVehicleForMobModel> GetMapInwardVehicleRaiseCncrnList(int BranchId, int CompId)
        //{
        //    return GetMapInwardVehicleRaiseCncrnListPvt(BranchId, CompId);
        //}
        //private List<MapInwardVehicleForMobModel> GetMapInwardVehicleRaiseCncrnListPvt(int BranchId, int CompId)
        //{
        //    List<MapInwardVehicleForMobModel> concernList = new List<MapInwardVehicleForMobModel>();
        //    try
        //    {
        //        concernList = _contextManager.usp_GetMapInwardVehicleRsolveCncrnList(BranchId, CompId).Select(v => new MapInwardVehicleForMobModel
        //        {
        //            PkId = v.pkId,
        //            BranchId = v.BranchId,
        //            CompId = v.CompId,
        //            LRNo = v.LRNo,
        //            LRDate = Convert.ToDateTime(v.LRDate),
        //            InwardDate = Convert.ToDateTime(v.InwardDate),
        //            TransporterId = Convert.ToInt32(v.TransporterId),
        //            TransporterName = v.TransporterName,
        //            TransporterNo = v.TransporterNo,
        //            VehicleNo = v.VehicleNo,
        //            DriverName = v.DriverName,
        //            MobileNo = v.MobileNo,
        //            NoOfCasesQty = Convert.ToInt32(v.NoOfCasesQty),
        //            ActualNoOfCasesQty = Convert.ToInt32(v.ActualNoOfCasesQty),
        //            ConcernRemark = v.ConcernRemark,
        //            ConcernBy = Convert.ToInt32(v.ConcernBy),
        //            Addedby = v.AddedBy,
        //            AddedOn = Convert.ToDateTime(v.AddedOn),
        //            ConcernUpdatedOn = Convert.ToDateTime(v.ConcernUpdatedOn),
        //            IsClaim = Convert.ToInt32(v.IsClaim),
        //            IsSAN = Convert.ToInt32(v.IsSAN)
        //        }).OrderByDescending(v => v.PkId).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetMapInwardVehicleRaiseCncrnListPvt", "Get Map Inward Vehicle Raise Cncrn List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return concernList;
        //}
        //#endregion

        //#region Get Claim Types List
        //public List<InsuranceClaimModel> GetClaimTypeList(int BranchId, int CompanyId)
        //{
        //    return GetClaimTypeListPvt(BranchId, CompanyId);
        //}
        //private List<InsuranceClaimModel> GetClaimTypeListPvt(int BranchId, int CompanyId)
        //{
        //    List<InsuranceClaimModel> modelList = new List<InsuranceClaimModel>();
        //    try
        //    {
        //        modelList = _contextManager.uspGetInsuranceClaimTypeList(BranchId, CompanyId).Select(b => new InsuranceClaimModel
        //        {
        //            ClaimTypeId = b.ClaimTypeId,
        //            ClaimType = b.ClaimType
        //        }).OrderByDescending(x => x.ClaimTypeId).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetClaimTypeListPvt", "Get Claim Type List Pvt (BranchId: " + BranchId + "  CompanyId: " + CompanyId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return modelList;
        //}
        //#endregion
        // GetVehicleChecklistDetailsForMobPvt
        //private List<ChckelistDtls> GetVehicleChecklistDetailsForMobPvt(int BranchId, int CompId)
        //{
        //    string mainPath = ConfigurationManager.AppSettings["VhcleInspImgPath"];
        //    List<ChckelistDtls> checklistDetails = new List<ChckelistDtls>();
        //    List<QueAnsDtls> model = new List<QueAnsDtls>();
        //    try
        //    {
        //        checklistDetails = _contextManager.usp_GetVehChklistDtls_Mob(BranchId, CompId).Select(a => new ChckelistDtls
        //        {
        //            BranchId = Convert.ToInt32(a.BranchId),
        //            CompId = Convert.ToInt32(a.CompId),
        //            ChkListMstId = a.ChkListMstId,
        //            LREntryId = Convert.ToInt32(a.LREntryId),
        //            Img1 = (a.Img1 == "" ? "" : mainPath + a.Img1),
        //            Img2 = (a.Img2 == "" ? "" : mainPath + a.Img2),
        //            Img3 = (a.Img3 == "" ? "" : mainPath + a.Img3),
        //            Img4 = (a.Img4 == "" ? "" : mainPath + a.Img4),
        //            //Img1 = mainPath + a.Img1,
        //            //Img2 = mainPath + a.Img2,
        //            //Img3 = mainPath + a.Img3,
        //            //Img4 = mainPath + a.Img4,
        //            AddedBy = a.AddedBy,
        //            AddedOn = Convert.ToDateTime(a.AddedOn),
        //            LRNo = a.LRNo,
        //            LRDate = Convert.ToDateTime(a.LRDate),
        //            VehicleNo = a.VehicleNo,
        //            TransporterId = Convert.ToInt32(a.TransporterId),
        //            TransporterName = a.TransporterName,
        //            TransporterNo = a.TransporterNo
        //        }).ToList();
        //        for (int i = 0; i < checklistDetails.Count; i++)
        //        {
        //            model = _contextManager.usp_GetVehChklistDtlsQA_Mob(BranchId, CompId).Select(b => new QueAnsDtls
        //            {
        //                BranchId = Convert.ToInt32(b.BranchId),
        //                CompId = Convert.ToInt32(b.CompId),
        //                ChkListMstId = Convert.ToInt32(b.ChkListMstId),
        //                ChkListDtlsId = Convert.ToInt64(b.ChkListDtlsId),
        //                CLQueId = Convert.ToString(b.CLQueId),
        //                CLQueText = b.CLQueText,
        //                FieldType = b.FieldType,
        //                SortId = Convert.ToInt32(b.SortId),
        //                AnsText = b.AnsText,
        //                ExpAnsText = b.ExpAnsText
        //            }).ToList();
        //            checklistDetails[i].QueAnsDetails = model;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetVehicleChecklistDetailsForMobPvt", "Get Vehicle Checklist Details For Mob Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return checklistDetails;
        //}

    }
}
