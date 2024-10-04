using CNF.Business.Model.InventoryInward;
using System;
using System.Collections.Generic;

namespace CNF.Business.Repositories.Repository
{
    public interface IInventoryInwardRepository
    {
        // Web Method START
        List<GetTransitListModel> GetTransitDataLst(int BranchId, int CompId);
        int RaiseInsuranceClaim(RaiseInsuranceClaimModel model);
        List<GetRaiseInsuranceClaimListModel> GetRaiseInsuranceClaimList(int BranchId, int CompId);
        List<MapInwardVehicleForMobModel> GetMapInwardVehicleRaiseCncrnList(int BranchId, int CompId);
        int ResolveVehicleIssue(RaiseInsuranceClaimModel model);
        int ApproveSANAdd(ApproveSAN model);
        int ApproveClaimAdd(ApproveClaim model);
        string SendEmailForClaimApprove(string ClaimNo, DateTime ApproveClaimDate, int BranchId, int CompId);
        // Web Method END

        // Mobile Method START
        List<ImportTransitListModel> GetTransitDataListForMob(int BranchId, int CompId);
        int RaiseConcernForTransitData(RaiseConcernForTransitDataModel model);
        int MapInwardVehicleWithTransitLR(MapInwardVehicleForMobModel model);
        List<MapInwardVehicleForMobModel> GetMapInwardVehicleWithTransitLR(int BranchId, int CompId);
        int VehicleChecklistMstAdd(VehicleChecklistDtlsModel model);
        List<ChckelistDtls> GetVehicleChecklistDetailsForMob(int BranchId, int CompId);
        int DeleteVehicleChecklistDetailsForMob(VehicleChecklistDtlsModel model);
        List<RaiseConcernForTransitData> GetRaiseConcernListForMob(int BranchId, int CompId);
        InvInwardAllCountModel GetInvInwardPagesAllCount(InvInwardAllCountModel model);
        int ResolveRaisedConcernAtOpLevel(RaiseConcernForTransitData model);
        dashbordcountmodel GetInventoryCountForSupervisor(int BranchId, int CompId);
        List<DashboardInventoryInwrdMdal> GetListForDashboardInventoryInwrdList(int BranchId, int CompId);
        List<InwardSupervisorDashboardMobModel> InwardSupervisorDashboardMob(int BranchId, int CompId);
        List<DashboardInventoryInwrdMdal> GetInvInwardCummVehicleList(int BranchId, int CompId);
        List<VhcleChkLstForViewImgModel> TransitVhcleChkLstForViewImg(int BranchId, int CompId, DateTime FromDate, DateTime ToDate);
        List<OwnInvInwardDashSmmryList> GetOwnerInvInwardDashSmmryList();

        // Mobile Method END


        // Not Used Method
        //int InsuranceClaimAddEdit(InsuranceClaimModel model);
        //List<InsuranceClaimModel> GetInsuranceClaimList(int BranchId, int CompId);
        //List<InvoiceListModel> GetInvoiceList(int BranchId, int CompId);
        //List<InsuranceClaimModel> GetInsuranceClaimInvByIdList(int BranchId, int CompanyId, string InvoiceId);
        //List<InsuranceClaimModel> GetInsuranceClmDtlsForEmail(int BranchId, int CompId, int ClaimId);
        //string InsuranceClaimforApproval(List<InsuranceClaimModel> modelList, string MailFilePath);
        //string UpdateMailForApproval(int BranchId, int CompId, long ClaimId, bool IsEmailSend);
        //int MapInwardVehicleAddEditForMob(MapInwardVehicleForMobModel model);
        //List<MapInwardVehicleForMobModel> GetMapInwardVehicleListForMobs(int BranchId, int CompId);
        //int VehicleCheckListAddEdits(VehicleChecklistModel model);
        //List<InvInVehicleCheckListmodel> GetInvInVehicleCheckList(int BranchId, int CompId);
        //int UpdateResolveVehicleLRIssue(UpdateResolveVehicleLRIssueModel model);
        //List<ImportTransitListModel> GetTransitDataLst(int BranchId, int CompId);
        //string UpdateInvInwardRaiseRequestById(InvInwardRaiseRequestByIdForModel model);
        //string AddEditDeleteLrDetails(LRDetailsModel model);
        //List<LRDetailsModel> GetLRDetailsList(int BranchId, int CompId);
        //List<InvInVehicleChecklistMaster> GetInvInVehicleCheckListMaster(int BranchId, int CompId, string ChecklistType);
        //List<MapInwardVehicleRsolveCncrnModel> GetMapInwardVehicleRsolveCncrnList(int BranchId, int CompId);
        //List<InsuranceClaimModel> GetClaimTypeList(int BranchId, int CompanyId);

    }
}
