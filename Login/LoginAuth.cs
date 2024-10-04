using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Model.Login
{
    public class LoginDetails 
    {
        public LoginAuth UserInfo { get; set; }
        public List<LoginAuth> UserDetails { get; set; }
        public string Token { get; set; }
        public DateTime expiration { get; set; }
        public string refresh_token { get; set; }       
        public HttpResponseMessage XSRF_token { get; set; }
        public string Status { get; set; }
        public string ExMsg { get; set; }
    }

    public class Login 
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public int EmpId { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string IsActive { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string RoleName { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string EmpMobNo { get; set; }
        public string EncryptPassword { get; set; }
        public string GrantType { get; set; }
    }

 public class EmployeeDtls
    {
        public int EmpId { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpMobNo { get; set; }
        public long UserId { get; set; }
        public int CompanyId { get; set; }
        public int RoleId { get; set; }
        public string CompanyName { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
    }
    public class UpdateFeedbackSystem 
    {
        public long Id { get; set; } 
        public int DistributorId { get; set; }
        public bool IsFeedbackSystem { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class LoginDetailsForAT 
    {
        public LoginAuth UserInfo { get; set; }
        public string Token { get; set; }
        public DateTime expiration { get; set; }
        public string refresh_token { get; set; }
        public UserModel userModel { get; set; }
    }


    public class LoginAuth
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EncryptPassword { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int EmpId { get; set; }
        public string IsActive { get; set; }
        public Nullable<int> BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string EmpMobNo { get; set; }
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompCityName { get; set; }
        public string BranchCity { get; set; }
        public string StateCode { get; set; }
    }

    public class LoginManagerList 
    {
        public UserLogin UserInfo { get; set; }
        public string Token { get; set; }
    }
    public class UserLogin
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public string RefNo { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string LastUpdatedDate { get; set; }
        public string RoleName { get; set; }
    }
    public class SaveLogModel 
    {
        public string saveLogStr { get; set; }
        public string Status { get; set; }
        public string ExMsg { get; set; }

    }


    public class UserModel
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Username.
        /// </summary>
        public string Username { get; set; }
        public string Password { get; set; }

        public string CurrentEncryptPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewEncryptPassword { get; set; }

        /// <summary>
        /// User email.
        /// </summary>
        public string Email { get; set; }
        public string GrantType { get; set; } // password or refresh_token

        public string ClientId { get; set; } // Key
        public string RefreshToken { get; set; }//Pass Generated Refresh Token
        /// <summary>
        /// List of user roles the user belongs to.
        /// </summary>
        public IEnumerable<string> Roles { get; set; }
        public string MobileNo { get; set; }
        public decimal StaffRefNo { get; set; }
        public string Message { get; set; }
        public LoginAuth UserDtls { get; set; }
        public string DistCode { get; set; }
        public string ProfileId { get; set; }
        public string unm { get; set; }
        public int EmpId { get; set; }
        public string upwd { get; set; }
        public string Encryptpwd { get; set; }
        public int AddedBy { get; set; }
        public string PicklistNo { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int RoleId { get; set; }
    }
    public class EmailSend
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public Nullable<int> Id { get; set; }
        public int DistributorId { get; set; }
        public string Exceptionmsg { get; set; }
    }

    public class ResetPasswordModel : UserModel
    {
        public decimal ResetID { get; set; }
        public int DistributorId { get; set; }
        public string ResetStatus { get; set; }
        public string DistributorCode { get; set; }
    }

    public class AppConfiguration
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Info { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }
    }

    public class QueryBuilder
    {
        public string Query { get; set; }
    }

    public class IssueTrackerLogin
    {
        public string DealerCode { get; set; }
        public string ProfileId { get; set; }
        public string DateStr { get; set; }
    }

    public class ActiveUser
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int EmpId { get; set; }
        public string Username { get; set; }
        public string MobileNo { get; set; }
        public int RoleId { get; set; }
        public string VersionNo { get; set; }
        public string DeviceId { get; set; }
    }

    public class UserPermissions
    {
        public int RoleId { get; set; }
        public int Dashboard { get; set; }
        public int EmployeeMaster { get; set; }
        public int StockistMaster { get; set; }
        public int CartingMaster { get; set; }
        public int CourierMaster { get; set; }
        public int BranchMaster { get; set; }
        public int CompanyMaster { get; set; }
        public int StockistTransporterMapping { get; set; }
        public int TransporterMaster { get; set; }
        public int StockistBranchRelation { get; set; }
        public int StockistCompanyRelation { get; set; }
        public int GeneralMaster { get; set; }
        public int EmailConfig { get; set; }
        public int PicklistOperation { get; set; }
        public int PicklistAdd { get; set; }
        public int PicklistVerify { get; set; }
        public int PicklistAllot { get; set; }
        public int ReAllotPicklist { get; set; }
        public int ImportInvData { get; set; }
        public int InvCancelList { get; set; }
        public int ReadyToDispatch { get; set; }
        public int AssignTransportMode { get; set; }
        public int ImportLRData { get; set; }
        public int PrintSticker { get; set; }
        public int ChqRegister { get; set; }
        public int ImportOS { get; set; }
        public int ImportChqDeposit { get; set; }
        public int ChqSummRpt { get; set; }
        public int ChqSummMonthlyRpt { get; set; }
        public int ResolveConcernPL { get; set; }
        public int ResolveConcernINV { get; set; }
        public int PriotiseINV { get; set; }
        public int AssignTransportModeEdit { get; set; }
        public int AppConfig { get; set; }
        public int ImportTransitReport { get; set; }
        public int ApproveVehicleIssue { get; set; }
        public int InsuranceClaim { get; set; }
        public int ApprovalClaim { get; set; }
        public int LRReceivedList { get; set; }
        public int ResolveClaimConcern { get; set; }
        public int ImportSRS { get; set; }
        public int CorrectionRequiredList { get; set; }
        public int SRSPendingCNList { get; set; }
        public int ImportCreditNote { get; set; }
        public int UploadDestructionCertificate { get; set; }
        public int DestructionCertificateList { get; set; }
        public int AppConfiguration { get; set; }
        public int StockTransferAdd { get; set; }
        public int CityMaster { get; set; }
        public int ThresholdValueMaster { get; set; }
        public int ChecklistMaster { get; set; }
        public int OtherCNFMaster { get; set; }
        public int ImportDepositedCheque { get; set; }
        public int ChequeRegisterSummaryReport { get; set; }
        public int RaisedConcernList { get; set; }
        public int VersionDetails { get; set; }
        public int BranchCompanyRelationMaster { get; set; }
        public int DashBoardOrderReturn { get; set; }
        public int DashBoardOrderDispatch { get; set; }
        public int DashBoradChequeAcc { get; set; }
        public int DashBoardInventoyInward { get; set; }
        public int DashBoradStockTrans { get; set; }
        public int DashMiniORPendingSRS { get; set; }
        public int DashORFOROperator { get; set; }
        public int DashCheackAccForOprator { get; set; }
        public int DashOrdDisForOperator { get; set; }
        public int DashBoardORForSupervisor { get; set; }
        public int DashOrdDisForSupervisor { get; set; }
        public int DshBranchDRP { get; set; }
        public int DshCompanyDRP { get; set; }
        public int ExpenseRegister { get; set; }
        public int ReimbursmentInvoice { get; set; }
        public int ComissionInvoice { get; set; }
        public int GatepassbillSummary { get; set; }
        public int CheckInvoice { get; set; }
        public int VendorMaster { get; set; }
        public int TaxMaster { get; set; }
        public int HeadMaster { get; set; }
        public int TransportParentMaster { get; set; }
        public int TransportParentMapping { get; set; }
        public int CourierParentMaster { get; set; }
        public int CourierParentMapping { get; set; }
        public int CompanyVendorMapping { get; set; }
        public int ChequeStatusReport { get; set; }
        public int VendorBranchMapping { get; set; }
        public int VehicleChecklistForImg { get; set; }
        public int VerifyStockistData { get; set; }
        public int ImportDynamically { get; set; }
        public int OCRImportProduct { get; set; }
        public int OCRIntegration { get; set; }
        public int OCRSummaryReport { get; set; }
        public int OCRDetailReport { get; set; }
        public int TransporterReport { get; set; }
        public int ImportSRSList { get; set; }
        public int PrinterDetailsMaster { get; set; }
        public int PrinterHistoryReport { get; set; }
        public int PrintGatepass { get; set; }
        public int DeleteInvoiceDetails { get; set; }
    }
}
