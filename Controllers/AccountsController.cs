using CNF.Business.BusinessConstant;
using CNF.Business.Model.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;

namespace CNF.API.Controllers
{
    public class AccountsController : BaseApiController
    {
        # region Expence Register Add Edit
        [HttpPost]
        [Route("Accounts/ExpenseRegisterAddEdit")]
        public int ExpenseRegisterAddEdit([FromBody] ExpenseRegister model)
        {
            int result = 0;
            try
            {                           
               result = _unitOfWork.AccountsRepository.AddExpenseRegister(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ExpenceRegisterAddEdit", "Expence Register Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        # region Expence Register Add Edit
        [HttpPost]
        [Route("Accounts/ExpenseRegisterUploadImg")]
        public string ExpenseRegisterUploadImg()
        {
            string result = string.Empty, FileName = string.Empty;
            var httpRequest = HttpContext.Current.Request;
            try
            {
                FileName = "Img" + DateTime.Now.ToString("dd-MM-yyyy_HH_mm_sss");
                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    if ((postedFile.FileName.EndsWith(".jpg")) || (postedFile.FileName.EndsWith(".jpeg")) || (postedFile.FileName.EndsWith(".png")) || (postedFile.FileName.EndsWith(".pdf")))
                    {
                        if ((postedFile.FileName.EndsWith(".jpg"))) { result = FileName + (".jpg"); }
                        else if ((postedFile.FileName.EndsWith(".jpeg"))) { result = FileName + (".jpeg"); }
                        else if ((postedFile.FileName.EndsWith(".png"))) { result = FileName + (".png"); }
                        else if ((postedFile.FileName.EndsWith(".pdf"))) { result = FileName + (".pdf"); }
                        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExpenseBillImg\\") + result;
                        postedFile.SaveAs(filePath);
                    }
                    else
                    {
                        result = "This file format is not supported";
                    }

                }

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ExpenceRegisterAddEdit", "Expence Register Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        # region Expence Register List
        [HttpGet]
        [Route("Accounts/ExpenseRegisterList/{BranchId}")]
        public List<ExpenseRegister> ExpenseRegisterList(int BranchId)
        {
            List<ExpenseRegister> model = new List<ExpenseRegister>();
            try
            {
                model = _unitOfWork.AccountsRepository.GetExpenseRegisterList(BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ExpenseRegisterList", "Expence Register List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion 

        # region Expence Payment Add
        [HttpPost]
        [Route("Accounts/ExpPaymentAdd")]
        public int ExpPaymentAdd([FromBody] ExpenseRegisterPayment model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.AccountsRepository.AddExpPayment(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ExpenceRegisterAddEdit", "Expence Register Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        # region Expence Payment List
        [HttpGet]
        [Route("Accounts/ExpenseRegisterPaymentList/{InvId}")]
        public List<ExpenseRegisterPayment> ExpenseRegisterPaymentList(int InvId)
        {
            List<ExpenseRegisterPayment> model = new List<ExpenseRegisterPayment>();
            try
            {
                model = _unitOfWork.AccountsRepository.GetExpenseRegisterPaymentList(InvId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ExpenseRegisterAddEdit", "Expence Register Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return model;
        }
        #endregion 

        #region Start - Reimbursment Invoice - Add,Edit,Delete/Cancel
        [HttpPost]
        [Route("Accounts/ReimbursementInvoiceAddEdit")]
        public int ReimbursementInvoiceAddEdit([FromBody] ReimbursementInvoiceAddEditModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.AccountsRepository.ReimbursementInvoiceAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ReimbursementInvoiceAddEditPvt", "Reimbursment Invoice - Add,Edit,Delete/Cancel " + "BranchId:  " + model.BranchId + "CompanyId:  " + model.CompanyId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion End - Reimbursment Invoice - Add,Edit,Delete/Cancel

        #region Start - Reimbursment Invoice List
        [HttpGet]
        [Route("Accounts/ReimbursementInvoiceList/{BranchId}")]
        public List<ReimbursementInvoiceAddEditModel> ReimbursementInvoiceList(int BranchId)
        {
            List<ReimbursementInvoiceAddEditModel> modelList = new List<ReimbursementInvoiceAddEditModel>();
            try
            {
                modelList = _unitOfWork.AccountsRepository.ReimbursementInvoiceList(BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ReimbursementInvoiceList", "Reimbursment Invoice List" + "BranchId:  " + BranchId , BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion End - Reimbursment Invoice List

        #region Start - Reimbursment Invoice Generate New Number
        [HttpGet]
        [Route("Accounts/GetReimInvNo/{BranchId}/{InvDate}")]
        public string GetReimInvNo(int BranchId, DateTime InvDate)
        {
            string RemInv = string.Empty;
            try
            {
                RemInv = _unitOfWork.AccountsRepository.GetReimInvNo(BranchId, InvDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetReimInvNo", "Reimbursment Invoice Generate New Number:  " + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RemInv;
        }
        #endregion End - Reimbursment Invoice Generate New Number

        #region Start - Get Reimbursement Invoice By Id List
        [HttpGet]
        [Route("Accounts/GetReimbursementInvById/{BranchId}/{CompId}/{ReimId}")]
        public List<ReimbursementInvByIdModel> GetReimbursementInvById(int BranchId, int CompId, int ReimId)
        {
            List<ReimbursementInvByIdModel> modelList = new List<ReimbursementInvByIdModel>();
            try
            {
                modelList = _unitOfWork.AccountsRepository.GetReimbursementInvById(BranchId, CompId, ReimId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetReimbursementInvById", "Reimbursement Invoice By Id List:  " + "BranchId:  " + BranchId + " CompId:  " + CompId + "  ReimId:  " + ReimId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion End - Get Reimbursement Invoice By Id List

        #region Start - Reimbursment Payment - Add,Edit,Delete/Cancel
        [HttpPost]
        [Route("Accounts/ReimbursementPaymentAddEdit")]
        public int ReimbursementPaymentAddEdit([FromBody] ReimbursementPaymentAddEditModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.AccountsRepository.ReimbursementPaymentAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ReimbursementPaymentAddEdit", "Reimbursment Payment - Add,Edit,Delete/Cancel ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion End - Reimbursment Payment - Add,Edit,Delete/Cancel

        #region Start - Reimbursement Payment List
        [HttpGet]
        [Route("Accounts/ReimbursementPaymentList/{ReimId}")]
        public List<ReimbursementPaymentAddEditModel> ReimbursementPaymentList(int ReimId)
        {
            List<ReimbursementPaymentAddEditModel> modelList = new List<ReimbursementPaymentAddEditModel>();
            try
            {
                modelList = _unitOfWork.AccountsRepository.ReimbursementPaymentList(ReimId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ReimbursementPaymentList", "Reimbursement Payment List:  " + ReimId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion End - Reimbursement Payment List

        #region Add Edit Commission Invoice
        [HttpPost]
        [Route("Accounts/AddEditCommissionInv")]
        public string AddEditCommissionInv([FromBody] CommissionInv model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.AccountsRepository.AddEditCommissionInv(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditCommissionInv", "Add Edit Commission Invoice", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Commission Invoice List
        [HttpGet]
        [Route("Accounts/GetCommissionInvList/{BranchId}")]
        public List<CommissionInvList> GetCommissionInvList(int BranchId)
        {
            List<CommissionInvList> commissionInvList = new List<CommissionInvList>();
            try
            {
                commissionInvList = _unitOfWork.AccountsRepository.GetCommissionInvList(BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCommissionInvList", "Get Commission Invoice List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return commissionInvList;
        }
        #endregion

        #region Get PickList Generate New No
        [HttpGet]
        [Route("Accounts/GetInvoiceGenerateNewNo/{BranchId}/{InvDate}")]
        public string GetInvoiceGenerateNewNo(int BranchId, DateTime InvDate)
        {
            string InvNo = string.Empty;

            try
            {
                InvNo = _unitOfWork.AccountsRepository.GetInvoiceGenerateNewNo(BranchId, InvDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceGenerateNewNo", "Get Invoice Generate New No", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvNo;
        }
        #endregion

        #region Add Commission Invoice Payment
        [HttpPost]
        [Route("Accounts/AddCommissionInvPayment")]
        public string AddCommissionInvPayment([FromBody] AddCommissionInvPay model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.AccountsRepository.AddCommissionInvPayment(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddCommissionInvPayment", "Add Commission Invoice Payment", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Commission Invoice Payment List
        [HttpGet]
        [Route("Accounts/GetCommissionInvPaymentList/{ComInvId}")]
        public List<CommissionInvPayList> GetCommissionInvPaymentList(int ComInvId)
        {
            List<CommissionInvPayList> commissionInvPayList = new List<CommissionInvPayList>();
            try
            {
                commissionInvPayList = _unitOfWork.AccountsRepository.GetCommissionInvPaymentList(ComInvId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCommissionInvPaymentList", "Get Commission Invoice Payment List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return commissionInvPayList;
        }
        #endregion

        #region Get Exp Register List For Check Invoice
        [HttpGet]
        [Route("Accounts/GetExpRegisterListForCheckInv/{BranchId}")]
        public List<ExpRegLstModel> GetExpRegisterListForCheckInv(int BranchId)
        {
            List<ExpRegLstModel> ExpRegLst = new List<ExpRegLstModel>();
            try
            {
                ExpRegLst = _unitOfWork.AccountsRepository.GetExpRegisterListForCheckInv(BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetExpRegisterListForCheckInv", "Get Exp Register List For Check Invoice", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return ExpRegLst;
        }
        #endregion

        #region Get Gatepass Bill Summary List
        [HttpGet]
        [Route("Accounts/GetGatepassBillSummaryList/{ExpInvId}")]
        public List<GatepassBillSummaryModel> GetGatepassBillSummaryList(int ExpInvId)
        {
            List<GatepassBillSummaryModel> BillSummaryList = new List<GatepassBillSummaryModel>();
            try
            {
                BillSummaryList = _unitOfWork.AccountsRepository.GetGatepassBillSummaryList(ExpInvId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGatepassBillSummaryList", "Get Gatepass Bill Summary List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return BillSummaryList;
        }
        #endregion

        #region Save Check Inv Verify Data
        [HttpPost]
        [Route("Accounts/SaveCheckInvVerifyData")]
        public int SaveCheckInvVerifyData([FromBody]CheckInvModel model)
        {
            int retValue = 0;
            try
            {
                retValue = _unitOfWork.AccountsRepository.SaveCheckInvVerifyData(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveCheckInvVerifyData", "Save Check Inv Verify Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return retValue;
        }
        #endregion

        #region Expense Invoice Resolve Concern
        [HttpPost]
        [Route("Accounts/ResolveConcern")]
        public int ResolveConcern([FromBody]TranspExpInvLstModel model)
        {
            int retValue = 0;
            try
            {
                retValue = _unitOfWork.AccountsRepository.ResolveConcern(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveConcern", "Resolve Concern", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return retValue;
        }
        #endregion

    }
}
