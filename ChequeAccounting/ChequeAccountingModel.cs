using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CNF.Business.Model.ChequeAccounting
{
    public class ChequeRegisterModel
    {
        public long ChqRegId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public DateTime ChqReceivedDate { get; set; }
        public int StokistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string StockistCity { get; set; }
        public string CityName { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string ChqNo { get; set; }
        public int FromChqNo { get; set; }
        public int ToChqNo { get; set; }
        public int ChqStatus { get; set; }
        public string ChqStatusText { get; set; }
        public string InvNo { get; set; }
        public double InvAmt { get; set; }
        public double ChqAmount { get; set; }
        public int Addedby { get; set; }
        public int Updatedby { get; set; }
        public string Action { get; set; }
        public string RetValue { get; set; }
        public int date_difference { get; set; }
        public string DepositedDate { get; set; }
        public string ReturnedDateDayDiff { get; set; }
        public string DepositedDateFormat { get; set; }
        public string LastUpdatedOnFormat { get; set; }
        public string ReturnedDateFormat { get; set; }
        public string FirstNoticeDateFormat { get; set; }
        public string LegalNoticeDateFormat { get; set; }
        public int IsFirstNoticeFlag { get; set; }
        public int IsLegalNoticeFlag { get; set; }
    }

    // Import Stockist OutStanding
    public class ImportStockistOutStandingData
    {
        public int pkId { get; set; }
        public long Div_Cd { get; set; }
        public string StockistCode { get; set; }
        public string CustomerCode { get; set; }
        public string StockistName { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string DocName { get; set; }
        public string DocDate { get; set; }
        public string DueDate { get; set; }
        public decimal OpenAmt { get; set; }
        public string DistrChannel { get; set; }
        public string ChqNo { get; set; }
        public string DocTypeDesc { get; set; }
        public string DocType { get; set; }
        public decimal OverdueAmt { get; set; }
        public string RetResult { get; set; }
    }

    public class ImportStockistOutStandingModel
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public DateTime OSDate { get; set; }
        public int StockistId { get; set; }
        public string Div_Cd { get; set; }
        public string StockistCode { get; set; }
        public string CustomerCode { get; set; }
        public string StockistName { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string Addedby { get; set; }
        public string CityCode { get; set; }
        public string DocName { get; set; }
        public string DocDate { get; set; }
        public string DueDate { get; set; }
        public string OpenAmt { get; set; }
        public string DistrChannel { get; set; }
        public string ChqNo { get; set; }
        public string DocTypeDesc { get; set; }
        public string DocType { get; set; }
        public string OverdueAmt { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string PaymentStatus { get; set; }
    }

    public class DetailsForEmail
    {
        public Nullable<Int64> Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public string OfficerRole { get; set; }
        public int IsSelect { get; set; }
    }

    public class EmailModel
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int EmailForId { get; set; }
        public string EmailCCPersonId { get; set; }
    }

    public class EmailConfigModel
    {
        public string PersonName { get; set; }
        public string Email { get; set; }
    }

    public class ChequeSummyCountModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int Total { get; set; }
        public int Blank { get; set; }
        public int Utilised { get; set; }
        public int Prepare { get; set; }
        public int Deposited { get; set; }
        public int Discarded { get; set; }
        public int Returned { get; set; }
        public int Settled { get; set; }
    }

    public class UpdateChequeSts
    {
        public int ChqRegId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string ChqNo { get; set; }
        public int StockistId { get; set; }
        public int ChqStatus { get; set; }
        public string InvData { get; set; }
        public string Remark { get; set; }
        public int ReturnReasonId { get; set; }
        public string Addedby { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime updateDate { get; set; }
        public DateTime BlockedDate { get; set; }

    }

    public class InvoiceForChqBlockModel
    {
        public long InvId { get; set; }
        public int CbInvId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string InvNo { get; set; }   
        public DateTime InvCreatedDate { get; set; }
        public Boolean IsColdStorage { get; set; }
        public int SoldTo_StokistId { get; set; }
        public string StockistName { get; set; }
        public string StockistNo { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public float InvAmount { get; set; }
        public int InvStatus { get; set; }
    }    
    public class StockistOSDetailsModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string StockistName { get; set; }
        public int StockistCode { get; set; }
        public DateTime Date { get; set; }
        public int RV { get; set; }
        public int DR { get; set; }
        public int AB { get; set; }
        public int DG { get; set; }
        public int CC { get; set; }
        public int CD { get; set; }
        public int Total { get; set; }
    }

    // Import Deposited Cheque Data
    public class ImportDepositedChequeData
    {
        public int pkId { get; set; }
        public string StockistName { get; set; }
        public string StockistCode { get; set; }
        public string DepositeDate { get; set; }
        public string BankName { get; set; }
        public long AccountNo { get; set; }
        public long ChequeNo { get; set; }
        public decimal Amount { get; set; }
        public string RetResult { get; set; }
    }

    public class ImportDepositedChequeModel
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string StockistName { get; set; }
        public string StockistCode { get; set; }
        public int StockistId { get; set; }
        public DateTime DepositeDate { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string ChequeNo { get; set; }
        public string ChqRegId { get; set; }
        public string Amount { get; set; }
        public DateTime DepoDate { get; set; }
    }
    public class StockistDetails
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string Emailid { get; set; }
        public string MobNo { get; set; }
        public string StockistCity { get; set; }
        public string CityName { get; set; }
        public long ChqRegId { get; set; }
    }
    public class ChequeRegstrSmmryRptModel
    {
        public int StokistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string MasterName { get; set; }
        public string BankName { get; set; }
        public int BankId { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNo { get; set; }
        public int TotalChqCount { get; set; }
        public int ChqRegId { get; set; }
        public int BlankChqs { get; set; }
        public int UtilisedChqs { get; set; }
        public int PrepareChqs { get; set; }
        public int DiscardedChqs { get; set; }
        public int DepositedChqs { get; set; }
        public int ReturnedChqs { get; set; }
        public int SettledChqs { get; set; }
    }

    public class OutStandingDtls
    {
        public DateTime OSDate { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int StockistId { get; set; }
        public string StockistCode { get; set; }
        public string StockistName { get; set; }
        public string MobNo { get; set; }
        public int PaymentStatus { get; set; }
        public decimal TotOverdueAmt { get; set; }
        public string Emailid { get; set; }
    }

    public class CCEmailDtls
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int EmailForId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class ChqDepositDetails
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string AccountNo { get; set; }
        public string ChqNo { get; set; }
        public int ChqStatus { get; set; }
        public DateTime DepositedDate { get; set; }
        public string Emailid { get; set; }
        public string MobNo { get; set; }
        public string InvNo { get; set; }
        public decimal ChqAmount { get; set; }
    }

    public class GetLRDetailsModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string PONo { get; set; }
        public DateTime PODate { get; set; }
        public string TransporterName { get; set; }
        public string LRNo { get; set; }
        public string LRDate { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string Emailid { get; set; }
    }

    public class StockistOsReportModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string StockistName { get; set; }
        public string City { get; set; }
        public string StockistCode { get; set; }
        public string DocName { get; set; }
        public string DocDate { get; set; }
        public string DueDate { get; set; }
        public string OpenAmt { get; set; }
        public string ChqNo { get; set; }
        public string DistrChannel { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime Date { get; set; }
        public decimal AB { get; set; }
        public decimal CD { get; set; }
        public decimal CC { get; set; }
        public decimal DG { get; set; }
        public decimal DR { get; set; }
        public decimal DZ { get; set; }
        public decimal RV { get; set; }
        public decimal Other { get; set; }
        public int Total { get; set; }
        public int Div_Cd { get; set; }
    }

    /// <summary>
    /// Cheque Summary of previous month/Week
    /// </summary>
    public class ChqSummaryForMonthlyModel
    {
        public string CompanyName { get; set; }
        public long InvId { get; set; }
        public string InvNo { get; set; }
        public int CompId { get; set; }
        public int BranchId { get; set; }
        public DateTime? InvCreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public double ChqAmount { get; set; }
        public string ChqNo { get; set; }
        public string Emailid { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
    }
    public class ChqSummaryForSalesTeamModel
    {
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string CityName { get; set; }
        public long TotalChqCount { get; set; }
        public int BlankChqs { get; set; }
        public string Emailid { get; set; }
    }

    public class OfficerDetails
    {
        public int EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public decimal MobileNo { get; set; }
        public string Email { get; set; }
        public string OfficerRole { get; set; }
    }

    public class ChqSummaryForMonthly
    {
        public int CompId { get; set; }
        public int BranchId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class AuditDtls
    {
        public int StokistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string Emailid { get; set; }
        public string CityName { get; set; }
        public string ChqNo { get; set; }
        public int ChqStatus { get; set; }
        public string ChqStatusText { get; set; }
        public string ReleasedRemark { get; set; }
    }

    public class chqaccountDashcnt
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int TotalChqBounced { get; set; }
        public int TodayBounce { get; set; }
        public int DueforFirstNotice { get; set; }
        public int DueforLegalNotice { get; set; }
        public int TodayDeposited { get; set; }
        public int Overduestk { get; set; }
        public int DealyDeposited { get; set; }
        public int NTotalFirstNotice { get; set; }
        public int NTotalLegalNotice { get; set; }
        public string OverDueAmt { get; set; }
        public int CummDiposited { get; set; }
    }

    public class DashBoardCommonModel
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class DashBoardCommonModelNew
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
    }

    public class OrderDispatchDashBoardCommonModel
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
    }
        public class DashbordChequeRegListModel
    {
        public long ChqRegId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string ChqReceivedDate { get; set; }
        public int StokistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string StockistCity { get; set; }
        public string CityName { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNo { get; set; }
        public string ChqNo { get; set; }
        public int ChqStatus { get; set; }
        public string ChqStatusText { get; set; }
        public int BlockedBy { get; set; }
        public string BlockedDate { get; set; }
        public int DiscardedBy { get; set; }
        public string DiscardedDate { get; set; }
        public string DiscardedRemark { get; set; }
        public int ReleasedBy { get; set; }
        public string ReleasedDate { get; set; }
        public string ReleasedRemark { get; set; }
        public int PrintedBy { get; set; }
        public string PrintedDate { get; set; }
        public int ChqAmount { get; set; }
        public int DepositedBy { get; set; }
        public string DepositedDate { get; set; }
        public int SettledBy { get; set; }
        public string SettledDate { get; set; }
        public int ReturnedBy { get; set; }
        public string ReturnedDate { get; set; }
        public int ReturnedReasonId { get; set; }
        public string MasterName { get; set; }
        public string FirstNoticeDate { get; set; }
        public string LegalNoticeDate { get; set; }
        public string LastUpdatedOn { get; set; }
        public int IsDueforFirstNotice { get; set; }
        public int IsDueforLegalNotice { get; set; }
        public int IsOverDueForFirstNotice { get; set; }
        public int date_difference { get; set; }
    }
    public class StockistOutStkList
    {
        public int StkOSId { get; set; }
        public DateTime OSDate { get; set; }
        public int StockistId { get; set; }
        public string StockistCode { get; set; }
        public string StockistName { get; set; }
        public DateTime DueDate { get; set; }
        public long OverdueAmt { get; set; }
        public string CityName { get; set; }
        public int AgeingDays { get; set; }
        public DateTime DocDate { get; set; }
        public string CityCode { get; set; }
        public string DocName { get; set; }
    }

    public class ChequeStatusReportModel
    {
        public int ChqRegId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string MonthStr { get; set; }
        public DateTime? ChqReceivedDate { get; set; }
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string CityName { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string ChqNo { get; set; }
        public decimal ChqAmount { get; set; }
        public int ChqStatus { get; set; }
        public string InvNo { get; set; }
        public string DueDate { get; set; }
        public string ChqRemark { get; set; }
    }

    public class ChqStatusReport
    {
        public int CompId { get; set; }
        public int BranchId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public class OwnChqAccDashSmmryList
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CompId { get; set; }
        public string CompanyName { get; set; }
        public int TotalBounce { get; set; }
        public int DueforFirstNotice { get; set; }
        public int DueforLegalNotice { get; set; }
    }
}