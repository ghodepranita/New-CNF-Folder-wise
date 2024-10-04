using CNF.Business.Model.Master;
using System;
using System.Collections.Generic;

namespace CNF.Business.Repositories.Repository
{
    public interface IMastersRepository
    {
        GeneralMasterList GetGeneralMaster(string CategoryName, string Status);
        GetStateList GetStateList(string Flag);
        GetCityList GetCityList(string StateCode, string districtCode, string Flag);
        List<BranchList> GetBranchList(string Status);
        string BranchMasterAddEdit(BranchList model);
        List<CompanyDtls> CompanyDtls(string Status);
        string CompanyDtlsAddEdit(CompanyDtls model);
        string EmployeeMasterActivate(EmployeeActiveModel model);
        int AddEmployeeDtls(AddEmployeeModel model);
        int EditEmployeeDtls(AddEmployeeModel model);
        List<EmployeeDtls> GetEmployeeDtls(int EmpId);
        GetCategoryList GetCategoryList();
        string AddEditDivisionMaster(DivisionMasterLst model);
        List<DivisionMasterLst> GetDivisionMasterList(string Status);
        string AddEditGeneralMaster(GeneralMasterLst model);
        string AddEditTransporterMaster(TransporterMasterLst model);
        List<TransporterMasterLst> GetTransporterMasterList(string DistrictCode, string Status,int BranchId);
        List<TransporterMasterLst> GetTransporterMasterListForBranch(string DistrictCode, string Status, int BranchId);
        List<RoleModel> GetRoleLst();
        List<RoleModel> GetRoleLstForUser();
        List<StockistModel> GetStockistLst(int BranchId, int CompanyId, string Status);
        string StockistDtlsAddEdit(StockistModel model);
        List<BankModel> GetStockistBankList(int StockistId);
        List<StokistTransportModel> GetStokistTransportMappingList(int BranchId,int CompanyId);
        string StokistTransportMappingAddEdit(StokistTransportModel model);
        GetDistrictList GetDistrictList(string StateCode, string Flag);
        List<cartingAgentmodel> GetCartingAgentLst(string Status, int BranchId);
        string CartingAgentMasterAddEdit(cartingAgentmodel model);
        List<EmployeeMasterList> GetEmployeeMasterList(int BranchId, string Status);
        string UserActiveDeactive(EmployeeActiveModel model);
        string AddEditCourierMaster(CourierMasterLst model);
        List<CourierMasterLst> GetcourierMasterList(int BranchId,string DistrictCode, string Status);
        UserDtls GetUserDtls(int UserId);
        TransporterMasterLst GetTransporterById(int StockistId);
        List<BranchIdDtls> GetBranchByIdDtls(int BranchId);
        string AddEditStockistCompanyRelation(StockistRelation model);
        string AddEditStockistBranchRelation(StockistRelation model);
        List<StockistRelation> GetStockistBranchRelationList(int BranchId);
        List<StockistRelation> GetStockistCompanyRelationList(int CompId);
        CheckUsernameAvailableModel GetCheckUsernameAvailable(string Username);
        List<StockistModel> GetStockistListByBranch(int BranchId, string Status);
        List<StockistModel> GetStockistListByCompany(int CompanyId, string Status);
        List<RolesModel> GetRolesls(int EmpId);
        List<GuardDetails> GetGuardDetails(int BranchId, int CompId);
        string CreateUser(CreateUserModel model);
        StockistModel GetStockistNoAvailables(string StockistNo);
        TransporterMasterLst GetTransporterNoAvailables(string TransporterNo);
        AddEmployeeModel GetCheckEmployeeNumberAvilable( int EmpId, string EmpNo, string EmpEmail, string EmpMobNo);
        cartingAgentmodel GetCheckCartingAgentAvilable(string CAName);
        CourierMasterLst GetCheckCourierNameAvilable(string CAName);
        string AddEditCityMaster(CityMaster model);
        List<ThresholdValueDtls> GetThresholdvalueDtls(int BranchId,int CompanyId);
        int AddEditThresholdValueMaster(ThresholdValueDtls model);
        int ChecklistMastersAddEdit(ChecklistMastersAddEditModel model);
        List<ChecklistMastersAddEditModel> GetChecklistMasterList(int BranchId, int CompId, string Status);
        ChecklistMastersAddEditModel GetcheckSequenceNoAvailable(int SeqNo, int BranchId, int CompId);
        int OtherCNFMasterAddEdit(OtherCNFDtlsModel model);
        List<OtherCNFDtlsModel> GetOtherCNFList(int BranchId, int CompId, string Status);
        string AddVersionDetails(VersionDetailsModel model);
        int DeleteVersionDetails(int VersionId);
        List<VersionDetailsModel> GetVersionDetails();
        CheckVersionNoModel CheckVersionNo(string VersionNo);
        string GetLatestVersionDetails();
        List<CompanyRelation> GetComapnyBranchRelationList(int BranchId);
        List<CompanyDtls> GetCompanyListByBranch(int BranchId, string Status);
        int AddEditCompanyBranchRelation(CompBranchRelation model);
        int AddVendorDetails(VendorDetailsModel model);
        List<VendorDetailsModel> GetVendorList(int Branch, string Status);
        int VendorDeleteDeactivate(VendorDetailsModel model);
        int TaxMasterAddEdit(TaxMastermodel model);
        List<TaxMastermodel> GetTaxMaster(int BranchId);
        List<HeadMasterModel> HeadMasterList(int BranchId);
        int HeadMasterAddEdit(HeadMasterModel model);
        int TransporterParentAddEdit(TransporterParentModel model);
        List<TransporterParentModel> GetTransporterParentList(int BranchId,string Status);
        int ParentTransporterMappingAddEdit(ParentTranporterMappingModel model);
        List<ParentTransportMappList> GetParentTransportMappedList(int Tid, string Status,int BranchId);
        TransporterParentModel GetTransporterParent(int Tpid);
        List<CourierParentModel> GetCourierParentList(int BranchId, string Status);
        CourierParentModel GetCourierParent(int Cpid);
        int CourierParentAddEdit(CourierParentModel model);
        List<ParentCourierMappList> GetParentCourierMappList(int CPid, string Status, int BranchId);
        int ParentCourierMappingAddEdit(ParentCourierMappingModel model);
        List<vendordtlsMapping> GetVendorListByCompany(int CompanyId, string Status,int BranchId);
        int AddEditCompanyVendorMapping(CompVendorAddEditMapping model);
        List<vendordBranchMapping> GetVendorListByBranch(int CompanyId, string Status);
        int AddEditBranchVendorMapping(VendorBranchAddEditMapping model);
        List<ExpiryStockistNotidashModel> ExpiryStockistNotificationDashboard(int BranchId, int CompId,string Flag);
        List<ExpiryListForNotiListModel> ExpiryListForNotificationList(int BranchId, int CompId);
        GSTTypeModel GetGSTTypeList(int TaxId , int BranchId);
        List<CompanyListByBRModel> GetCompanyListByBRIdForEMP(int BranchId ,string Status);
       ThresholdValueDtls SLAMasterlist(int BranchId, int CompId);
        List<CompanyDtls> GetCompanyListForLogin(string Status);
        List<TranLstwithStkModel> TransportListwithStockies(int BranchId, int CompanyId, int StockiesId,string Status);
        OwnerLoginDashCnt GetOwnerDashboardCountList(int BranchId, int CompanyId);
        List<StockistModel> GetStockistListByBranchCompany(int BranchId, int CompanyId, string Status);
        List<StockistModel> GetStockistListforVerifyDataList(int BranchId, int CompanyId, string Status);
        List<ImportTypeList> GetImportTypeList();
        List<ImportFileColumnRelList> GetImportFileandColumnRelList(int BranchId, int CompId, int ImportId);
        List<ImportFileColumnRelList> OnChangeColFieldList(int BranchId, int CompId, int ImpId);
        int ImportDymAddEdit(ImportDymAddEditModel model);
        List<ImportDynaListModel> GetImportDyanamically(int BranchId, int CompId);
        List<BranchDtlsList> GetBranchCompListForMob(string Status);
        List<VenderIsTds> GetExpenseRegisterTds(int Branch, int VendorId);
        List<CompanyDtls> GetCompanyLstForAccount(string Status, int BranchId);
        List<TransporterIsTds> GetExpenseTransporterTds(int BranchId, int TransporterId);
        List<CourierIsTds> GetExpenseCourierTds(int BranchId, int CourierId);

        // Hrishikesh Method
        List<BranchDetails> GetBranchDetailsById(int BranchId);

        // Hrishi Method
        List<CompanyRelationHrishi> GetBranchWiseCompany(int BranchId);
        //Pratyush Method
        List<PrinterDataModal> GetPrinterDataList(int? BranchId, int? CompanyId, DateTime? fromDate, DateTime? toDate, string flag);
        //Hrishi Method
        string UpdatePrinterDetails(PrinterValueModal model);       
    } 
}
 