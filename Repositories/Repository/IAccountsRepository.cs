using CNF.Business.Model.Account;
using System;
using System.Collections.Generic;

namespace CNF.Business.Repositories.Repository
{
    public interface IAccountsRepository
    {
        int AddExpenseRegister(ExpenseRegister model);
        List<ExpenseRegister> GetExpenseRegisterList(int BranchId);
        int AddExpPayment(ExpenseRegisterPayment model);
        List<ExpenseRegisterPayment> GetExpenseRegisterPaymentList(int InvId);
        int ReimbursementInvoiceAddEdit(ReimbursementInvoiceAddEditModel model);
        List<ReimbursementInvoiceAddEditModel> ReimbursementInvoiceList(int BranchId);
        string GetReimInvNo(int BranchId, DateTime InvDate);
        int ReimbursementPaymentAddEdit(ReimbursementPaymentAddEditModel model);
        List<ReimbursementPaymentAddEditModel> ReimbursementPaymentList(int ReimId);
        string AddEditCommissionInv(CommissionInv model);
        List<CommissionInvList> GetCommissionInvList(int BranchId);
        string GetInvoiceGenerateNewNo(int BranchId, DateTime InvDate);
        string AddCommissionInvPayment(AddCommissionInvPay model);
        List<CommissionInvPayList> GetCommissionInvPaymentList(int ComInvId);
        List<ExpRegLstModel> GetExpRegisterListForCheckInv(int BranchId);
        List<GatepassBillSummaryModel> GetGatepassBillSummaryList(int ExpInvId);
        int SaveCheckInvVerifyData(CheckInvModel model);
        List<ReimbursementInvByIdModel> GetReimbursementInvById(int BranchId, int CompId, int ReimId);
        int ResolveConcern(TranspExpInvLstModel model);
    }
}
