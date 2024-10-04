using CNF.Business.Model.ChequeAccounting;
using System;
using System.Collections.Generic;

namespace CNF.Business.Repositories.Repository
{
    public interface IChequeAccountingRepository
    {
        string ChequeRegisterAdd(ChequeRegisterModel model);
        string ChequeRegisterEditDelete(ChequeRegisterModel model);
        List<ChequeRegisterModel> ChequeRegisterList(int BranchId, int CompanyId, int StockistId);
        ChequeSummyCountModel ChequeSummyCountLst(int BranchId, int CompId, int StockistId);
        List<ImportStockistOutStandingModel> GeStockistOSLst(int BranchId, int CompId);
        List<DetailsForEmail> GetAdminDetails(int EmailFor);
        List<DetailsForEmail> GetCCEmailandPurposeDetails(string Flag);
        string EmailConfigurationAdd(EmailModel model);
        List<EmailConfigModel> GetEmailConfigList(int BranchId, int CompanyId);
        string UpdateChequeStatus(UpdateChequeSts model);
        List<InvoiceForChqBlockModel> InvoiceForChqBlockList(int stockistId, int CompId, DateTime FromDate, DateTime ToDate);
        List<ImportDepositedChequeModel> GetChequeReceiptLst(int BranchId, int CompId);
        List<StockistDetails> GetEmailCountDetails(int BranchId, int CompId);
        List<ChequeRegstrSmmryRptModel> ChequeRegisterLst(int BranchId, int CompId);
        List<OutStandingDtls> GetStkOutstandingDtlsForEmail(int BranchId, int CompId);
        string sendEmailForOutstanding(string Emailid, int BranchId, int CompId, decimal TotOverdueAmt, string StockistName, DateTime OSDate);
        List<ChqDepositDetails> GetChequeDepositedList(int BranchId, int CompId);
        List<CCEmailDtls> GetCCEmailDtlsPvt(int BranchId, int CompId, int EmailFor);
        List<AuditDtls> GetInternalTeamEmailList(int BranchId, int CompId);
        List<GetLRDetailsModel> GetLRImportDetailsList(int BranchId, int CompId);
        List<StockistOsReportModel> OsdocTypesReportLst(int BranchId, int CompId);
        List<ChqSummaryForMonthlyModel> GetChqSummaryForMonthlyList(int CompId, int BranchId, DateTime? FromDate, DateTime? ToDate);
        List<OfficerDetails> GetSalesTeamEmailList();
        List<ChqSummaryForSalesTeamModel> GetRptChqSummaryForSalesTeamList(int BranchId, int CompId);
        string GetChequeSummaryForSalesTeamForReport(List<ChqSummaryForSalesTeamModel> modelList, string MailFilePath);
        chqaccountDashcnt GetDashbordCnt(int BranchId, int CompId);
        List<DashbordChequeRegListModel> CheqAccountingList(int BranchId, int CompId);
        List<StockistOutStkList> OverDueStockistList(int BranchId, int CompId);
        List<ChequeStatusReportModel> GetChequeStatusReportList(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate);
        List<DashbordChequeRegListModel> ChqRegCummDepositedList(int BranchId, int CompId);
        List<OwnChqAccDashSmmryList> GetOwnerChqAccDashSmmryList();
    }
}