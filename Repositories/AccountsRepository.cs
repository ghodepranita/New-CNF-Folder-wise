using CNF.Business.BusinessConstant;
using CNF.Business.Model.Account;
using CNF.Business.Model.Context;
using CNF.Business.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace CNF.Business.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private CFADBEntities _contextManager;

        // Inject DB Context instance from Unit of Work to avoid unwanted memory allocations.
        public AccountsRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }

        #region Add Expence Register
        public int AddExpenseRegister(ExpenseRegister model)
        {
            return AddExpenseRegisterPvt(model);
        }
        private int AddExpenseRegisterPvt(ExpenseRegister model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    RetValue = contextManager.usp_ExpenseRegisterAddEdit(model.ExpInvId, model.BranchId, model.InvTypeId, model.VendorId, model.TransId, model.CourierId, model.ExpInvNo,
                   model.InvDate, model.CompId, model.ExpHeadId, model.NoOfBox, model.FromDate, model.ToDate, model.isGSTApply, model.TaxableAmt, model.CGST, model.SGST, model.TaxId, model.TotalAmt, model.IsReimbursable,
                   model.ExpInvStatus, model.PdfName, model.AddedBy, model.Action, model.IsTDS, model.TDSPer, obj);
                }

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddExpenceRegisterPvt", "Add Expence Register Pvt - BranchId:  " + model.BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Expence Register List
        public List<ExpenseRegister> GetExpenseRegisterList(int BranchId)
        {
            return GetExpenseRegisterListPvt(BranchId);
        }

        private List<ExpenseRegister> GetExpenseRegisterListPvt(int BranchId)
        {
            List<ExpenseRegister> model = new List<ExpenseRegister>();
            try
            {
                string mainPath = ConfigurationManager.AppSettings["ExpImgPath"];
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    model = contextManager.usp_ExpenseRegisterList(BranchId).Select(e => new ExpenseRegister()
                    {
                        ExpInvId = e.ExpInvId,
                        BranchId = Convert.ToInt16(e.BranchId),
                        ExpBillImagePdfName = (e.ExpBillImagePdfName != null && e.ExpBillImagePdfName != "" ? mainPath + e.ExpBillImagePdfName : null),
                        InvTypeId = e.InvTypeId,
                        VendorId = Convert.ToInt16(e.VendorId),
                        VendorName = e.VendorName,
                        BillFromName = e.BillFromName,
                        TransId = Convert.ToInt16(e.TransId),
                        TransName = e.ParentTranspName,
                        CourierId = Convert.ToInt16(e.CourierId),
                        CourierName = e.ParentCourierName,
                        ExpInvNo = e.ExpInvNo,
                        InvDate = Convert.ToDateTime(e.InvDate),
                        CompId = Convert.ToInt16(e.CompId),
                        CompanyName = e.CompanyName,
                        ExpHeadId = Convert.ToInt16(e.ExpHeadId),
                        ExpHeadName = e.HeadName,
                        NoOfBox = Convert.ToInt16(e.NoOfBox),
                        FromDate = e.InvFromDt,
                        ToDate = e.InvToDt,
                        isGSTApply = e.IsGSTApply,
                        TaxableAmt = Convert.ToInt32(e.TaxableAmt),
                        CGST = Convert.ToDecimal(e.CGST),
                        SGST = Convert.ToDecimal(e.SGST),
                        TaxId = Convert.ToInt32(e.TaxId),
                        GSTType = e.GSTType,
                        TotalAmt = Convert.ToDecimal(e.TotalAmt),
                        IsReimbursable = e.IsReimbursable,
                        Balance = Convert.ToDecimal(e.Balance),
                        ExpInvStatus = Convert.ToInt16(e.ExpInvStatus),
                        ExpInvStatusText = e.ExpInvStatusText,
                        IsTDS = e.IsTDS,
                        TDSPer = Convert.ToInt32(e.TDSPer),
                        NewPDFEdit = e.ExpBillImagePdfName,
                        AddedBy = Convert.ToInt16(e.AddedBy),
                        SGSTPer = Convert.ToInt16(e.SGSTPer),
                        CGSTPer = Convert.ToInt16(e.CGSTPer)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetExpenceRegisterListPvt", "Get ExpenceRegister List Pvt - BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Add Expence Register Payment
        public int AddExpPayment(ExpenseRegisterPayment model)
        {
            return AddExpPaymentPvt(model);
        }

        private int AddExpPaymentPvt(ExpenseRegisterPayment model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    RetValue = contextManager.usp_ExpenseRegisterPaymentAdd(model.ExpPaymentId, model.ExpInvId, model.PaymentDate, model.TDS,
                        model.PaymentAmt, model.PayMode, model.UTRNo, model.Remark, model.AddedBy, model.Action, obj);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddExpPayment", "Add Exp Payment", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Expence Register Payment List
        public List<ExpenseRegisterPayment> GetExpenseRegisterPaymentList(int ExpInvId)
        {
            return GetExpenseRegisterListPaymentPvt(ExpInvId);
        }

        private List<ExpenseRegisterPayment> GetExpenseRegisterListPaymentPvt(int ExpInvId)
        {
            List<ExpenseRegisterPayment> model = new List<ExpenseRegisterPayment>();
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    model = contextManager.usp_ExpenseRegisterPaymentList(ExpInvId).Select(e => new ExpenseRegisterPayment()
                    {
                        ExpPaymentId = e.ExpPaymentId,
                        ExpInvId = e.ExpInvId,
                        ExpInvNo = e.ExpInvNo,
                        PaymentDate = Convert.ToDateTime(e.PaymentDate),
                        TDS = Convert.ToDecimal(e.TDS),
                        PaymentAmt = Convert.ToDecimal(e.PaymentAmt),
                        PayMode = Convert.ToInt16(e.PayMode),
                        PaymentModeText = e.PaymentModeText,
                        UTRNo = e.UTRNo,
                        Remark = e.Remark
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetExpenceRegisterListPaymentPvt", "Get Expence Register List Payment Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Start - Reimbursment Invoice - Add,Edit,Delete/Cancel
        public int ReimbursementInvoiceAddEdit(ReimbursementInvoiceAddEditModel model)
        {
            return ReimbursementInvoiceAddEditPvt(model);
        }
        private int ReimbursementInvoiceAddEditPvt(ReimbursementInvoiceAddEditModel model)
        {
            int RetValue = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    RetValue = contextManager.usp_ReimbursementInvAddEdit(model.ReimId, model.BranchId, model.CompanyId, model.InvDate, model.ExpInvIdstr, model.TaxableAmt, model.CGST, model.SGST, model.TotalAmt, model.ExpeHeadId, model.TDS, model.Remark, model.Addedby, model.Action, objRetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ReimbursementInvoiceAddEditPvt", "Reimbursment Invoice - Add,Edit,Delete/Cancel " + "BranchId:  " + model.BranchId + "CompanyId:  " + model.CompanyId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion End - Reimbursment Invoice - Add,Edit,Delete/Cancel

        #region Start - Reimbursment Invoice List
        public List<ReimbursementInvoiceAddEditModel> ReimbursementInvoiceList(int BranchId)
        {
            return ReimbursementInvoiceListPvt(BranchId);
        }
        private List<ReimbursementInvoiceAddEditModel> ReimbursementInvoiceListPvt(int BranchId)
        {
            List<ReimbursementInvoiceAddEditModel> modelList = new List<ReimbursementInvoiceAddEditModel>();
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    modelList = contextManager.usp_ReimbursementInvList(BranchId).Select(r => new ReimbursementInvoiceAddEditModel
                    {
                        ReimId = Convert.ToInt32(r.ReimId),
                        BranchId = Convert.ToInt32(r.BranchId),
                        CompanyId = Convert.ToInt32(r.CompId),
                        CompanyName = Convert.ToString(r.CompanyName),
                        InvNo = Convert.ToString(r.InvNo),
                        InvDate = Convert.ToDateTime(r.InvDate),
                        TaxableAmt = Convert.ToInt32(r.TaxableAmt),
                        CGST = Convert.ToDouble(r.CGST),
                        SGST = Convert.ToDouble(r.SGST),
                        TDS = Convert.ToInt32(r.TDS),
                        TotalAmt = Convert.ToDouble(r.TotalAmt),
                        ExpeHeadId = Convert.ToInt32(r.ExpeHeadId),
                        ExpeHeadName = Convert.ToString(r.ExpeHeadName),
                        Remark = Convert.ToString(r.Remark),
                        PaymentAmt = Convert.ToDouble(r.PaymentAmt)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ReimbursementInvoiceListPvt", "Reimbursment Invoice List" + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion End - Reimbursment Invoice List

        #region Start - Reimbursment Invoice Generate New Number
        public string GetReimInvNo(int BranchId, DateTime InvDate)
        {
            return GetReimInvNoPvt(BranchId, InvDate);
        }
        private string GetReimInvNoPvt(int BranchId, DateTime InvDate)
        {
            string RemInv = string.Empty;
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    RemInv = contextManager.usp_GetReimInvNo(BranchId, InvDate).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetReimInvNoPvt", "Reimbursment Invoice Generate New Number:  " + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RemInv;
        }
        #endregion End - Reimbursment Invoice Generate New Number

        #region Start - Get Reimbursement Invoice By Id List
        public List<ReimbursementInvByIdModel> GetReimbursementInvById(int BranchId, int CompId, int ReimId)
        {
            return GetReimbursementInvByIdPvt(BranchId, CompId, ReimId);
        }
        private List<ReimbursementInvByIdModel> GetReimbursementInvByIdPvt(int BranchId, int CompId, int ReimId)
        {
            List<ReimbursementInvByIdModel> modelList = new List<ReimbursementInvByIdModel>();
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    modelList = contextManager.usp_ReimbursementInvById(BranchId, CompId, ReimId).Select(r => new ReimbursementInvByIdModel
                    {
                        ExpInvId = Convert.ToInt32(r.ExpInvId),
                        ExpInvNo = Convert.ToString(r.ExpInvNo),
                        InvDate = Convert.ToDateTime(r.InvDate),
                        NoOfBox = Convert.ToInt32(r.NoOfBox),
                        BillFromName = Convert.ToString(r.BillFromName),
                        BranchId = Convert.ToInt32(r.BranchId),
                        InvFromDt = Convert.ToDateTime(r.InvFromDt),
                        InvToDt = Convert.ToDateTime(r.InvToDt),
                        TaxableAmt = Convert.ToInt32(r.TaxableAmt),
                        CGST = Convert.ToDecimal(r.CGST),
                        SGST = Convert.ToDecimal(r.SGST),
                        TotalAmt = Convert.ToDecimal(r.TotalAmt),
                        IsReimbursable = Convert.ToString(r.IsReimbursable),
                        ExpInvStatus = Convert.ToInt32(r.ExpInvStatus),
                        ReimId = Convert.ToInt32(r.ReimId),
                        SGSTPer = Convert.ToDecimal(r.SGSTPer),
                        CGSTPer = Convert.ToDecimal(r.CGSTPer)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetReimbursementInvByIdPvt", "Reimbursement Invoice By Id List:  " + "BranchId:  " + BranchId + " CompId:  " + CompId + "  ReimId:  " + ReimId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion End - Get Reimbursement Invoice By Id List

        #region Start - Reimbursment Payment - Add,Edit,Delete/Cancel
        public int ReimbursementPaymentAddEdit(ReimbursementPaymentAddEditModel model)
        {
            return ReimbursementPaymentAddEditPvt(model);
        }
        private int ReimbursementPaymentAddEditPvt(ReimbursementPaymentAddEditModel model)
        {
            int RetValue = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    RetValue = contextManager.usp_ReimbursementPaymentDtlsAdd(model.ReimPaymentId, model.ReimId, model.PaymentDate, model.TDS, model.PaymentAmt, model.PaymentModeId, model.UTRNo, model.Remark, model.Addedby, model.Action, objRetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ReimbursementPaymentAddEditPvt", "Reimbursment Payment - Add,Edit,Delete/Cancel ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion End - Reimbursment Payment - Add,Edit,Delete/Cancel

        #region Start - Reimbursement Payment List
        public List<ReimbursementPaymentAddEditModel> ReimbursementPaymentList(int ReimId)
        {
            return ReimbursementPaymentListPvt(ReimId);
        }
        private List<ReimbursementPaymentAddEditModel> ReimbursementPaymentListPvt(int ReimId)
        {
            List<ReimbursementPaymentAddEditModel> modelList = new List<ReimbursementPaymentAddEditModel>();
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    modelList = contextManager.usp_ReimbursementPaymentList(ReimId).Select(r => new ReimbursementPaymentAddEditModel
                    {
                        ReimPaymentId = Convert.ToInt32(r.ReimPaymentId),
                        ReimId = Convert.ToInt32(r.ReimId),
                        InvNo = Convert.ToString(r.InvNo),
                        PaymentDate = Convert.ToDateTime(r.PaymentDate),
                        TDS = Convert.ToDouble(r.TDS),
                        PaymentModeId = Convert.ToInt32(r.PaymentModeId),
                        //PaymentMode = Convert.ToString(r.PaymentMode),
                        PaymentAmt = Convert.ToDouble(r.PaymentAmt),
                        Remark = Convert.ToString(r.Remark),
                        UTRNo = Convert.ToString(r.UTRNo)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ReimbursementPaymentList", "Reimbursement Payment List: " + ReimId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion End - Reimbursement Payment List

        #region Add Edit Commission Invoice
        public string AddEditCommissionInv(CommissionInv model)
        {
            return AddEditCommissionInvpvt(model);
        }
        private string AddEditCommissionInvpvt(CommissionInv model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_CommissionInvAddEdit(model.ComInvId, model.BranchId, model.CompanyId, model.InvoiceDate, model.InvType,
                    model.Description, model.TaxId, model.TaxableAmount, model.CGST, model.SGST, model.TotalAmt, model.AddedBy, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditCommissionInvpvt", "Add Edit Commission Invoice", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
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

        #region Get Commission Invoice List
        public List<CommissionInvList> GetCommissionInvList(int BranchId)
        {
            return GetCommissionInvListPvt(BranchId);
        }
        private List<CommissionInvList> GetCommissionInvListPvt(int BranchId)
        {
            List<CommissionInvList> commissionInvLists = new List<CommissionInvList>();
            try
            {
                commissionInvLists = _contextManager.usp_CommissionInvList(BranchId).Select(x => new CommissionInvList
                {
                    ComInvId = x.ComInvId,
                    BranchId = x.BranchId,
                    CompanyId = x.CompanyId,
                    CompanyCode = x.CompanyCode,
                    CompanyName = x.CompanyName,
                    CompanyCity = x.CompanyCity,
                    CityName = x.CityName,
                    CompanyAddress = x.CompanyAddress,
                    InvNo = x.InvNo,
                    InvDate = x.InvDate,
                    pkId = x.InvTypeId,
                    InvType = x.InvType,
                    Discription = x.Discription,
                    GSTType = x.GSTType,
                    TaxId = Convert.ToInt32(x.TaxId),
                    TaxableAmt = x.TaxableAmt,
                    CGST = x.CGST,
                    SGST = x.SGST,
                    TotalAmt = x.TotalAmt,
                    PaymentAmt = x.PaymentAmt,
                    Addedby = x.Addedby,
                    AddedOn = x.AddedOn,
                    LastUpdatedOn = x.LastUpdatedOn,
                    SGSTPer = x.SGSTPer,
                    CGSTPer = x.CGSTPer,
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCommissionInvListPvt", "Get Commission Invoice List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return commissionInvLists;
        }
        #endregion

        #region Get Invoice Generate New No
        public string GetInvoiceGenerateNewNo(int BranchId, DateTime InvDate)
        {
            return GetInvoiceGenerateNewNoPvt(BranchId, InvDate);
        }
        private string GetInvoiceGenerateNewNoPvt(int BranchId, DateTime InvDate)
        {
            string InvNo = string.Empty;

            try
            {
                InvNo = _contextManager.usp_GetCommInvNo(BranchId, InvDate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceGenerateNewNoPvt", "Get Invoice Generate New No", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvNo;
        }
        #endregion

        #region Add Commission Invoice Payment
        public string AddCommissionInvPayment(AddCommissionInvPay model)
        {
            return AddCommissionInvPaymentpvt(model);
        }
        private string AddCommissionInvPaymentpvt(AddCommissionInvPay model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_CommissionInvPaymentDtlsAdd(model.ComInvPaymentId, model.ComInvId, model.PaymentDate, model.TDSAmt, model.PaymentAmt,
                                    model.PaymentModeId, model.UTRNo, model.Remark, model.Addedby, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddCommissionInvPaymentpvt", "Add Commission Invoice Payment", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
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

        #region Get Commission Invoice Payment List
        public List<CommissionInvPayList> GetCommissionInvPaymentList(int ComInvId)
        {
            return GetCommissionInvPaymentListPvt(ComInvId);
        }
        private List<CommissionInvPayList> GetCommissionInvPaymentListPvt(int ComInvId)
        {
            List<CommissionInvPayList> commissionInvPayLists = new List<CommissionInvPayList>();
            try
            {
                commissionInvPayLists = _contextManager.usp_CommissionInvPaymentDtlsList(ComInvId).Select(x => new CommissionInvPayList
                {
                    ComInvPaymentId = x.ComInvPaymentId,
                    ComInvId = Convert.ToInt32(x.ComInvId),
                    InvNo = x.InvNo,
                    PaymentDate = Convert.ToDateTime(x.PaymentDate),
                    TDSAmt = Convert.ToDouble(x.TDSAmt),
                    PaymentAmt = Convert.ToDouble(x.PaymentAmt),
                    PaymentModeId = Convert.ToInt32(x.PaymentModeId),
                    PaymentModeText = x.PaymentModeText,
                    Remark = x.Remark,
                    UTRNo = Convert.ToString(x.UTRNo)
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCommissionInvPaymentListPvt", "Get Commission Invoice Payment List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return commissionInvPayLists;
        }
        #endregion

        #region  Get Exp Register List For Check Invoice
        public List<ExpRegLstModel> GetExpRegisterListForCheckInv(int BranchId)
        {
            return GetExpRegisterListForCheckInvPvt(BranchId);
        }
        private List<ExpRegLstModel> GetExpRegisterListForCheckInvPvt(int BranchId)
        {
            List<ExpRegLstModel> ExpRegLst = new List<ExpRegLstModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    ExpRegLst = _contextManager.usp_GetExpRegisterListForCheckInv(BranchId).Select(c => new ExpRegLstModel
                    {
                        VendorId = Convert.ToInt32(c.VendorId),
                        VendorName = c.VendorName,
                        ExpInvId = c.ExpInvId,
                        ExpInvNo = c.ExpInvNo,
                        InvTypeId = c.InvTypeId,
                        InvDate = Convert.ToDateTime(c.InvDate),
                        CompId = Convert.ToInt32(c.CompId),
                        CompanyName = c.CompanyName,
                        ExpHeadId = Convert.ToInt32(c.ExpHeadId),
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                        TaxableAmt = Convert.ToInt32(c.TaxableAmt),
                        TotalAmt = Convert.ToInt32(c.TotalAmt),
                        Balance = Convert.ToInt32(c.Balance),
                        ExpInvStatusText = c.ExpInvStatusText,
                        TransId = Convert.ToInt32(c.TransId),
                        ParentTranspName = c.ParentTranspName,
                        InvFromDt = Convert.ToDateTime(c.InvFromDt),
                        InvToDt = Convert.ToDateTime(c.InvToDt),
                        BillFromName = c.BillFromName
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetExpRegisterListForCheckInvPvt", "Get Exp Register List For Check Inv Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ExpRegLst;
        }
        #endregion

        #region  Get Gatepass Bill Summary List
        public List<GatepassBillSummaryModel> GetGatepassBillSummaryList(int ExpInvId)
        {
            return GetGatepassBillSummaryListPvt(ExpInvId);
        }
        private List<GatepassBillSummaryModel> GetGatepassBillSummaryListPvt(int ExpInvId)
        {
            List<GatepassBillSummaryModel> GpSummryLst = new List<GatepassBillSummaryModel>();
            List<TranspExpInvLstModel> GpSummryLstById = new List<TranspExpInvLstModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    GpSummryLst = _contextManager.usp_GetGPSummaryChecklist(ExpInvId).Select(c => new GatepassBillSummaryModel
                    {
                        GPDate = Convert.ToDateTime(c.GPDate),
                        GPNoOfInv = Convert.ToInt32(c.NoOfInv),
                        GPNoOfBox = Convert.ToInt32(c.GPNoOfBox),
                        TranspNoOfBox = Convert.ToInt32(c.TranspNoOfBox),
                        RatePerBox = Convert.ToInt32(c.Rate),
                        Amount = Convert.ToInt32(c.FreightAmt),
                        dtctID = c.dtctID,
                        CityName = c.CityName,
                        SoldTo_City = Convert.ToInt32(c.CityCode)
                    }).ToList();

                    for (int i = 0; i < GpSummryLst.Count(); i++)
                    {
                        GpSummryLstById = _contextManager.usp_GetGPSummaryChecklistByGPDate(ExpInvId, GpSummryLst[i].GPDate, GpSummryLst[i].SoldTo_City)
                            .Select(p => new TranspExpInvLstModel
                            {
                                ExpInvDtlsId = p.ExpInvDtlsId,
                                GPDate = Convert.ToDateTime(p.GPDate),
                                GatepassId = Convert.ToInt32(p.GatepassId),
                                GatepassNo = p.GatepassNo,
                                GPNoOfInv = Convert.ToInt32(p.NoOfInv),
                                GPNoOfBox = Convert.ToInt32(p.GPNoOfBox),
                                TranspNoOfBox = Convert.ToInt32(p.TranspNoOfBox),
                                RatePerBox = Convert.ToInt32(p.Rate),
                                Amount = Convert.ToInt32(p.FreightAmt),
                                Remark = p.Remark,
                                DtlsStatus = Convert.ToInt32(p.DtlsStatus),
                                DtlsStatusText = p.StatusText,
                                ResolveRemark = p.ResolveRemark,
                                gpctId = Convert.ToInt64(p.gpctId)
                            }).ToList();
                        GpSummryLst[i].GpSummaryById = GpSummryLstById;
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGatepassBillSummaryListPvt", "Get Gatepass Bill Summary List pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GpSummryLst;
        }
        #endregion

        #region Save Check Inv Verify Data
        public int SaveCheckInvVerifyData(CheckInvModel model)
        {
            return SaveCheckInvVerifyDataPvt(model);
        }
        private int SaveCheckInvVerifyDataPvt(CheckInvModel model)
        {
            int RetValue = 0;
            string RetResult = string.Empty;
            try
            {
                List<VerifyDataLst> modelList = new List<VerifyDataLst>();
                if (model.VerifyData.Count > 0)
                {
                    for (int i = 0; i < model.VerifyData.Count; i++)
                    {
                        VerifyDataLst checkInvModel = new VerifyDataLst();
                        checkInvModel.pkId = i + 1;
                        checkInvModel.GatepassId = model.VerifyData[i].GatepassId;
                        checkInvModel.TransBillBox = model.VerifyData[i].TransBillBox;
                        checkInvModel.DtlsStatus = model.VerifyData[i].DtlsStatus;
                        modelList.Add(checkInvModel);
                    }

                    DataTable dt = new DataTable();
                    dt.Columns.Add("pkId");
                    dt.Columns.Add("GatepassId");
                    dt.Columns.Add("TransBillBox");
                    dt.Columns.Add("DtlsStatus");

                    foreach (var item in modelList)
                    {
                        dt.Rows.Add(item.pkId, item.GatepassId, item.TransBillBox, item.DtlsStatus);
                    }

                    using (CFADBEntities _contextManager = new CFADBEntities())
                    {
                        {
                            SqlConnection connection = (SqlConnection)_contextManager.Database.Connection;
                            SqlCommand cmd = new SqlCommand("CFA.usp_ExpRegDtlsUpdate", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter ExpInvIdParameter = cmd.Parameters.AddWithValue("@ExpInvId", model.ExpInvId);
                            ExpInvIdParameter.SqlDbType = SqlDbType.Int;
                            SqlParameter AddedByParameter = cmd.Parameters.AddWithValue("@AddedBy", model.AddedBy);
                            AddedByParameter.SqlDbType = SqlDbType.Int;
                            SqlParameter VerifyDataParameter = cmd.Parameters.AddWithValue("@SummDt", dt);
                            VerifyDataParameter.SqlDbType = SqlDbType.Structured;
                            if (connection.State == ConnectionState.Closed)
                                connection.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                RetResult = (string)dr["RetResult"];
                                RetValue = Convert.ToInt32(RetResult);
                            }
                            if (connection.State == ConnectionState.Open)
                                connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveCheckInvVerifyDataPvt", "Save Check Inv Verify Data pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Expense Invoice Resolve Concern
        public int ResolveConcern(TranspExpInvLstModel model)
        {
            return ResolveConcernpvt(model);
        }
        private int ResolveConcernpvt(TranspExpInvLstModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetVal", typeof(int));

            try
            {
                RetValue = _contextManager.usp_ExpInvResConcern(model.ExpInvDtlsId, model.DtlsStatus, model.ResolveRemark, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveConcernpvt", "Resolve Concern pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        private string ImageBase64(string imagePath)
        {
            using (Image image = Image.FromFile(imagePath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

    }
}
