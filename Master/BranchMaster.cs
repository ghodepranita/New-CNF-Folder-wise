using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CNF.Business.Model.Master
{
    public class GeneralMasterList
    {
        public List<GeneralMasterDetail> GeneralMasterParameter { get; set; }
    }
    public class GeneralMasterDetail
    {
        public int pkId { get; set; }
        public string CategoryName { get; set; }
        public string MasterName { get; set; }
        public string DescriptionText { get; set; }
        public string isActive { get; set; }
    }


    public class GetStateList
    {
        public List<GetStateDtls> GetStateParameter { get; set; }
    }
    public class GetStateDtls
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string ActiveFlag { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
    public class GetCityList
    {
        public List<GetCityDtls> GetCityParameter { get; set; }
    }
    public class GetCityDtls
    {
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public string ActiveFlag { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string StateName { get; set; }

    }
    public class BranchList
    {
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Pan { get; set; }
        public string GSTNo { get; set; }
        public string IsActive { get; set; }
        public string Addedby { get; set; }
        public string CityName { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Action { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }

    }
    //Created by Hrishi
    public class BranchDetails
    {
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string IsActive { get; set; }



    }

    public class CompanyDtls
    {
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string ContactNo { get; set; }
        public string CompanyAddress { get; set; }
        public int MyProperty { get; set; }
        public string CompanyCity { get; set; }
        public string CityName { get; set; }
        public string Pin { get; set; }
        public string CompanyPAN { get; set; }
        public string GSTNo { get; set; }
        public bool? IsPicklistAvailable { get; set; }
        public string IsActive { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Action { get; set; }
        public int Checked { get; set; }
    }

    public class EmployeeActiveModel
    {
        public int EmpId { get; set; }
        public string IsActive { get; set; }
        public string Addedby { get; set; }
    }

    public class AddEmployeeModel
    {
        public int BranchId { get; set; }
        public string EmpNo { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpPAN { get; set; }
        public string EmpEmail { get; set; }
        public string EmpMobNo { get; set; }
        public string EmpAddress { get; set; }
        public string CityCode { get; set; }
        public long DesignationId { get; set; }
        public string BloodGroup { get; set; }
        //public int pkId { get; set; }
        public string AadharNo { get; set; }
        public string IsUser { get; set; }
        public string Addedby { get; set; }
        public string companyStr { get; set; }
        public string RoleIdStr { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EncryptPassword { get; set; }

        //new add

        public string IsActive { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int flag { get; set; }

    }

    public class EmployeeDtls
    {
        public int? EmpId { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CompanyCode { get; set; }

    }
    public class GetCategoryList
    {
        public List<GetCategoryDetails> CategoryParameter { get; set; }
    }
    public class GetCategoryDetails
    {
        public int CatId { get; set; }
        public string CategoryName { get; set; }
        public string isActive { get; set; }
    }
    public class DivisionMasterLst
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int DivisionId { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string FloorName { get; set; }
        public int IsColdStorage { get; set; }
        public string IsActive { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string SupplyType { get; set; }
        public string Action { get; set; }
    }
    public class GeneralMasterLst
    {
        public int pkId { get; set; }
        public string CategoryName { get; set; }
        public string MasterName { get; set; }
        public string DescriptionText { get; set; }
        public string IsActive { get; set; }
        public string Action { get; set; }
    }
    public class TransporterMasterLst
    {
        public int TransporterId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public string TransporterEmail { get; set; }
        public string TransporterMobNo { get; set; }
        public string TransporterAddress { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public int Flag { get; set; }
        public string IsActive { get; set; }
        public int RatePerBox { get; set; }
        public string Addedby { get; set; }
        public string DisplayName { get; set; }
        public string Action { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }

    public class RoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class StockistModel
    {
        public int StockistId { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string StockistPAN { get; set; }
        public string Emailid { get; set; }
        public string MobNo { get; set; }
        public string StockistAddress { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string GSTNo { get; set; }
        public int? LocationId { get; set; }
        public string MasterName { get; set; }
        public string Pincode { get; set; }
        public string DLNo { get; set; }
        public DateTime DLExpDate { get; set; }
        public string FoodLicNo { get; set; }
        public DateTime FoodLicExpDate { get; set; }
        public int BankId { get; set; }
        public string BankAccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string IsActive { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Action { get; set; }
        public int Checked { get; set; }
        public List<BankModel> BnkDtls { get; set; }
        public int Flag { get; set; }
        public int DLExpDateCount { get; set; }
        public int FoodLicExpDateCount { get; set; }
        public int MappedWithBR { get; set; }
        public int MappedWithCMp { get; set; }
        public int BRCRNotMap { get; set; }
        #region Stockist Data verification method start
        public bool IsCustomerCodeValid()
        {
            return !string.IsNullOrEmpty(StockistNo) && !StockistNo.StartsWith("0");
        }

        public bool HasOnlyAlphanumericCharacters()
        {
            return !string.IsNullOrEmpty(StockistNo) && Regex.IsMatch(StockistNo, "^[a-zA-Z0-9]+$");
        }

        public bool EmailAddressOnly()
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            return !string.IsNullOrEmpty(Emailid) && Regex.IsMatch(Emailid, pattern);
        }

        public bool NumberOnlyAndComma()
        {
            string pattern = @"^\d+(?:-\d+)?(?:,\d+)*$";
            return !string.IsNullOrEmpty(MobNo) && Regex.IsMatch(MobNo, pattern);
        }
        
        public bool PanAlphaNumberOnly()
        {
        string pattern = @"[a-zA-Z0-9]";
        return !string.IsNullOrEmpty(StockistPAN) && Regex.IsMatch(StockistPAN, pattern);
        }

        public bool GstAlphaNumberOnly()
        {
            string pattern = @"^[a-zA-Z0-9]+$";
            return !string.IsNullOrEmpty(GSTNo) && Regex.IsMatch(GSTNo, pattern);
        }
        
        public bool DlNoCodeOnly()
        {
            return !string.IsNullOrEmpty(DLNo) && DLNo.Length <= 30;
         }

        public bool AcntNoLalidation()
        {
            string pattern = @"^[0-9+\-\ ]+$";
            return !string.IsNullOrEmpty(BankAccountNo) && Regex.IsMatch(BankAccountNo, pattern);
        }

        public bool BankIdValidation()
        {
            string pattern = @"[^0-9]"; // Non-numeric characters
            string bankIdStr = BankId.ToString(); // Convert BankId to string
            return !string.IsNullOrEmpty(bankIdStr) && Regex.IsMatch(bankIdStr, pattern);
        }

        public bool DLExpiryrDateValidation()
        {
            return !string.IsNullOrEmpty(Convert.ToString(DLExpDate));
        }
        public bool FoodExpryDateValidation()
        {
            return !string.IsNullOrEmpty(Convert.ToString(FoodLicExpDate));
        }

        public bool FoodLicNoValidate()
        {
            return !string.IsNullOrEmpty(FoodLicNo) && DLNo.Length <= 25;
        }
        public bool CityValidate()
        {
            return !string.IsNullOrEmpty(CityName);
        }

        #endregion Stockist Data verification method end 

    }

    public class BankModel
    {
        public int StockistId { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNo { get; set; }
    }

    public class StokistTransportModel
    {
        public int Mappingid { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string Emailid { get; set; }
        public string MobNo { get; set; }
        public string CityCode { get; set; }
        public int LocationId { get; set; }
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public int TransitDays { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public int SupplyTypeId { get; set; }
        public string MasterName { get; set; }
        public string Action { get; set; }
    }
    public class cartingAgentmodel
    {
        public int CAId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string CAName { get; set; }
        public string CAMobNo { get; set; }
        public string CAEmail { get; set; }
        public string CAPan { get; set; }
        public string GSTNo { get; set; }
        public string CAAddress { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string DistrictCode { get; set; }
        public string CityCode { get; set; }
        public string DistrictName { get; set; }
        public string CityName { get; set; }
        public string IsActive { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Action { get; set; }

        public int flag { get; set; }
    }
    public class GetDistrictList
    {
        public List<GetDistrictDtls> GetDistrictParameter { get; set; }
    }
    public class GetDistrictDtls
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string StateCode { get; set; }
        public string ActiveFlag { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string StateName { get; set; }

    }

    public class EmployeeMasterList
    {
        public int EmpId { get; set; }
        public int BranchId { get; set; }
        public string EmpNo { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string EmpName { get; set; }
        public string EmpPAN { get; set; }
        public string EmpEmail { get; set; }
        public string EmpMobNo { get; set; }
        public string EmpAddress { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public int pkId { get; set; }
        public string BloodGroupName { get; set; }
        public string AadharNo { get; set; }
        public string IsUser { get; set; }
        public string IsActive { get; set; }
        public string Addedby { get; set; }
        public string AddedOn { get; set; }
        public string LastUpdatedOn { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserStatus { get; set; }
    }

    public class CourierMasterLst
    {
        public int CourierId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string CourierName { get; set; }
        public string CourierEmail { get; set; }
        public string CourierMobNo { get; set; }
        public string CourierAddress { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string DistrictCode { get; set; }
        public int RatePerBox { get; set; }
        public string IsActive { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
        public string DistrictName { get; set; }
        public int flag { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string DisplayName { get; set; }
    }

    public class UserDtls
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public int EmpId { get; set; }
        public string RoleName { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string EmpEmail { get; set; }
        public string Password { get; set; }
        public string EncryptPassword { get; set; }
        public string EmpMobNo { get; set; }
        public string IsActive { get; set; }
        public string Designation { get; set; }
        public string BloodGroup { get; set; }
        public string AadharNo { get; set; }
        public string EmpNo { get; set; }
    }
    public class BranchIdDtls
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }

    

    public class StockistRelation
    {
        public int pkid { get; set; }
        public int StockistId { get; set; }
        public string StockistName { get; set; }
        public int BranchId { get; set; }
        public string Stockieststr { get; set; }
        public string BranchName { get; set; }
        public int CompId { get; set; }
        public string CompName { get; set; }
        public int AddedBy { get; set; }
        public string Action { get; set; }
        public string StockistNo { get; set; }
        public string CompanyCode { get; set; }
        public string BranchCode { get; set; }
    }



    public class CheckUsernameAvailableModel
    {
        public long UserId { get; set; }
        public int BranchId { get; set; }
        public int RoleId { get; set; }
        public int EmpId { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EncryptPassword { get; set; }
        public string IsActive { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
    public class RolesModel
    {
        public int? EmpId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class GuardDetails
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string GatepassNo { get; set; }
    }

    public class CreateUserModel
    {
        public int BranchId { get; set; }
        public int EmpId { get; set; }
        public string IsUser { get; set; }
        public string RoleIdStr { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EncryptPassword { get; set; }
        public string Addedby { get; set; }

    }

    public class CityMaster
    {
        public string CityCode { get; set; }
        public string StateCode { get; set; }
        public string CityName { get; set; }
        public string ActiveFlag { get; set; }
        public string AddedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Action { get; set; }

    }

    public class ThresholdValueDtls
    {
        public int PkId { get; set; }
        public int BranchId { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public int ThresholdValue { get; set; }
        public int RaiseClaimDay { get; set; }
        public int ClaimSettlementDay { get; set; }
        public long InStateAmt { get; set; }
        public long OutStateAmt { get; set; }
        public int SaleSettlePeriod { get; set; }
        public int NonSaleSettlePeriod { get; set; }
        public string Addedby { get; set; }
        public string AddedOn { get; set; }
        public int CompanyId { get; set; }
        public string Action { get; set; }

    }
        public class ChecklistMastersAddEditModel
    {
        public long ChecklistTypeId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int CompanyId { get; set; }
        public string QuestionName { get; set; }
        public string ControlType { get; set; }
        public int SeqNo { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
        public string IsActive { get; set; }
        public string Status { get; set; }
        public string CompanyName { get; set; }
        public int Flag { get; set; }
        public string BranchName { get; set; }

    }

    public class OtherCNFDtlsModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int CNFId { get; set; }
        public string CNFCode { get; set; }
        public string CNFName { get; set; }
        public string CityCode { get; set; }
        public string CNFEmail { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string CNFAddress { get; set; }
        public string CityName { get; set; }
        public string IsActive { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }

    public class VersionDetailsModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string IsActive { get; set; }
        public int VersionId { get; set; }
        public string VersionNo { get; set; }
        public string VersionDate { get; set; }
        public DateTime VersionDateTime { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }

    public class CheckVersionNoModel
    {
        public int Flag { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string VersionNo { get; set; }
    }
 
    public class CompanyRelation
    {
        public int PkId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
    public class CompBranchRelation
    {
        public string CompanyIdstr { get; set; }
        public int BranchId { get; set; }
        public int AddedBy { get; set; }
    }

    public class VendorDetailsModel
    {
        public int VendorId { get; set; }
        public int BranchId { get; set; }
        public string VendorName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string PANNumber { get; set; }
        public string IsGST { get; set; }
        public string GSTNumber { get; set; }
        public int City { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string IsActive { get; set; }
        public int AddedBy { get; set; }
        public string Action { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int CompanyId { get; set; }
        public string Status { get; set; }
        public string IsTDS { get; set; }
        public int TDSPer { get; set; }
    }

    public class TaxMastermodel
    {
        public int TaxId { get; set; }
        public int BranchId { get; set; }
        public string GSTType { get; set; }
        public int CGST { get; set; }
        public int SGST { get; set; }
        public string AddedBy { get; set; }
        public string Action { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }

    public class HeadMasterModel
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public string HeadName { get; set; }
        public string HeadType { get; set; }
        public string IsActiveStatus { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int HeadTypeId { get; set; }
        public string Action { get; set; }
    }

    public class TransporterParentModel
    {
        public int Tid { get; set; }
        public int BranchId { get; set; }
        public string ParentTranspNo { get; set; }
        public string ParentTranspName { get; set; }
        public string ParentTranspEmail { get; set; }
        public string ParentTranspMobNo { get; set; }
        public string IsTDS { get; set; }
        public int TDSPer { get; set; }
        public string IsGST { get; set; }
        public string GSTNumber { get; set; }
        public string IsActive { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
    }

    public class ParentTranporterMappingModel
    {
        public int BranchId { get; set; }
        public int Tid { get; set; }
        public string TransporterNumber { get; set; }
        public string TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public string AddedBy { get; set; }
    }

    public class ParentTransportMappList
    {
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public int Checked { get; set; }
    }

    public class CourierParentModel
    {
        public int Cpid { get; set; }
        public int BranchId { get; set; }
        public string ParentCourierName { get; set; }
        public string ParentCourierEmail { get; set; }
        public string ParentCourierMobNo { get; set; }
        public int TDSPer { get; set; }
        public string IsTDS { get; set; }
        public string IsGST { get; set; }
        public string GSTNumber { get; set; }
        public string IsActive { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
    }

    public class ParentCourierMappList
    {
        public int CourierId { get; set; }
        public string CourierName { get; set; }
        public string CityCode { get; set; }
        public string CourierEmail { get; set; }
        public string CourierMobNo { get; set; }
        public int Checked { get; set; }
    }

    public class ParentCourierMappingModel
    {
        public int BranchId { get; set; }
        public int CPid { get; set; }
        public string CourierId { get; set; }
        public string CourierName { get; set; }
        public string AddedBy { get; set; }
    }

    public class vendordtlsMapping
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int Checked { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }

    }
    public class CompVendorAddEditMapping
    {
        public string VendorIdStr { get; set; }
        public int CompanyId { get; set; }
        public int AddedBy { get; set; }
    }

    public class vendordBranchMapping
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int Checked { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public int BranchId { get; set; }

    }
    public class VendorBranchAddEditMapping
    {
        public string VendorIdStr { get; set; }
        public int BranchId { get; set; }
        public int AddedBy { get; set; }
    }
    public class ExpiryStockistNotidashModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int DLFoodNotiCnt { get; set; }
    }

    public class ExpiryListForNotiListModel
    {
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string DLNo { get; set; }
        public string DLExpDate { get; set; }
        public string FoodLicNo { get; set; }
        public string FoodLicExpDate { get; set; }
        public string Addedby { get; set; }
        public string LastUpdatedOn { get; set; }
        public int DLExpDateCount { get; set; }
        public int FoodLicExpDateCount { get; set; }

    }
    public class GSTTypeModel
    {
        public List<GSTTypeListModel> GSTType { get; set; }
    }
    public class GSTTypeListModel
    {
        public int TaxId { get; set; }
        public string GSTType { get; set; }
        public int CGST { get; set; }
        public int SGST { get; set; }
        public string AddedBy { get; set; }
        public int BranchId { get; set; }
    }
    public class CompanyListByBRModel
    {
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
    }

    public class TranLstwithStkModel
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public int StockistId { get; set; }
        public string isActive { get; set; }
    }


    public class QueryBuilderModel
    {
        public string Query { get; set; }
        public string Status { get; set; }
        public string TableName { get; set; }
    }

    public class OwnerLoginDashCnt
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int OPrioPending { get; set; }
        public int OStkrPending { get; set; }
        public decimal OStkrPendingAmt { get; set; }
        public int OGPPending { get; set; }
        public decimal OGPPendingAmt { get; set; }
        public int OTPBox { get; set; }
        public int OTotalChqBounced { get; set; }
        public int ODueforFirstNotice { get; set; }
        public int ODueforLegalNotice { get; set; }
        public int OOverDueStk { get; set; }
        public string OOverDueAmt { get; set; }
        public int OPendSANCnt { get; set; }
        public int OPendClaimCnt { get; set; }
        public int OConsignPending { get; set; }
        public int OSalebleCN2_7 { get; set; }
        public int OMore11Days { get; set; }
        public int OStkStickerPending { get; set; }
        public decimal OStkSticerPendingAmt { get; set; }
        public int OStkGPPending { get; set; }
        public decimal OStkGPPendingAmt { get; set; }
        public int ONoOfBoxes { get; set; }
        public int SalelablePen2 { get; set; }
        public int NonSalelablePen45 { get; set; }
        
    }

    public class ImportTypeList
    {
        public int ImportId { get; set; }
        public string ImportType { get; set; }
       
    }

    public class ImportFileColumnRelList
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string FieldName { get; set; }
        public string ColumnDatatype { get; set; }
        public int ImportId { get; set; }
        public string ImpFor { get; set; }
        public int ImpId { get; set; }
    }
    public class ImportDymAddEditModel
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string ImpFor { get; set; }
        public string FieldName { get; set; }
        public string ExcelColName { get; set; }
        public string ColumnDatatype { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }

    }
    public class ImportDynaListModel
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public int CompId { get; set; }
        public string ImpFor { get; set; }
        public string FieldName { get; set; }
        public string ExcelColName { get; set; }
        public string ColumnDatatype { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class BranchDtlsList
    {
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string IsActive { get; set; }
        public List<CompDtlsList> compList = new List<CompDtlsList>();
    }
    public class CompDtlsList
    {
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string IsActive { get; set; }
    }

    public class VenderIsTds
    {
        public int VendorId { get; set; }
        public int BranchId { get; set; }
        public string VendorName { get; set; }
        public string IsGST { get; set; }
        public string GSTNumber { get; set; }
        public string IsActive { get; set; }
        public string Action { get; set; }
        public DateTime AddedOn { get; set; }
        public int CompanyId { get; set; }
        public string IsTDS { get; set; }
        public int TDSPer { get; set; }
    }

    public class TransporterIsTds
    {
        public int TransporterId { get; set; }
        public int BranchId { get; set; }
        public string ParentTranspName { get; set; }
        public string IsGST { get; set; }
        public string GSTNumber { get; set; }
        public string IsActive { get; set; }
        public string Action { get; set; }
        public DateTime AddedOn { get; set; }
        public int CompanyId { get; set; }
        public string IsTDS { get; set; }
        public int TDSPer { get; set; }
    }

    public class CourierIsTds
    {
        public int CourierId { get; set; }
        public int BranchId { get; set; }
        public string CourierName { get; set; }
        public string IsGST { get; set; }
        public string GSTNumber { get; set; }
        public string IsActive { get; set; }
        public string Action { get; set; }
        public DateTime AddedOn { get; set; }
        public int CompanyId { get; set; }
        public string IsTDS { get; set; }
        public int TDSPer { get; set; }
    }

    public class CompanyRelationHrishi
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }

    //Created by Pratyush
    public class PrinterDataModal
    {
        public int pkId { get; set; }
        public string BranchName { get; set; }
        public string CompanyName { get; set; }
        public long InvId { get; set; }
        public string InvNo { get; set; }
        public DateTime InvCreatedDate { get; set; }
        public string Status { get; set; }
        public string StokistNo { get; set; }
        public string StokistName { get; set; }
        public int NoOfBox { get; set; }
        public int IsStockTransfer { get; set; }
    }

    //Created by Pratyush
    public class PrinterValueModal
    {
        public int InvId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public string Flag { get; set; }
        

    }
}

