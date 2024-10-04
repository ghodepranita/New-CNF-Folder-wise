using System;
using System.Collections.Generic;

namespace CNF.Business.Model.InventoryInward
{
    // Import Transit Data
    public class ImportTransitDataModel
    {
        public long pkId { get; set; }
        public string DeliveryNo { get; set; }
        public string ActualGIDate { get; set; }
        public string RecPlant { get; set; }
        public string RecPlantDesc { get; set; }
        public string DispPlant { get; set; }
        public string DispPlantDesc { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string MaterialNo { get; set; }
        public string MatDesc { get; set; }
        public string UoM { get; set; }
        public string BatchNo { get; set; }
        public decimal Quantity { get; set; }
        public string TransporterCode { get; set; }
        public string TransporterName { get; set; }
        public string LrNo { get; set; }
        public string LrDate { get; set; }
        public decimal TotalCaseQty { get; set; }
        public string VehicleNo { get; set; }
        public string RetResult { get; set; }
    }

    // Get List Transit data
    public class ImportTransitListModel
    {
        public long TransitId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public long pkId { get; set; }
        public string DeliveryNo { get; set; }
        public string ActualGIDate { get; set; }
        public string RecPlant { get; set; }
        public string RecPlantDesc { get; set; }
        public string DispPlant { get; set; }
        public string DispPlantDesc { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string MaterialNo { get; set; }
        public string MatDesc { get; set; }
        public string UOM { get; set; }
        public string BatchNo { get; set; }
        public string Quantity { get; set; }
        public string TransporterCode { get; set; }
        public string TransporterName { get; set; }
        public string LrNo { get; set; }
        public string LrDate { get; set; }
        public string TotalCaseQty { get; set; }
        public string VehicleNo { get; set; }
        public int AddedBy { get; set; }
        public string AddedOn { get; set; }
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public int TransitLRStatus { get; set; }
        public int IsMapDone { get; set; }
        public int RaiseConcernId { get; set; }
        public string RaiseConcernBy { get; set; }
        public string RaiseConcernRemarks { get; set; }
        public DateTime RaiseConcernUpdatedOn { get; set; }
    }

    public class GetTransitListModel
    {
        public long TransitId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public long pkId { get; set; }
        public string DeliveryNo { get; set; }
        public DateTime ActualGIDate { get; set; }
        public string RecPlant { get; set; }
        public string RecPlantDesc { get; set; }
        public string DispPlant { get; set; }
        public string DispPlantDesc { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string MaterialNo { get; set; }
        public string MatDesc { get; set; }
        public string UOM { get; set; }
        public string BatchNo { get; set; }
        public string Quantity { get; set; }
        public string TransporterCode { get; set; }
        public string TransporterName { get; set; }
        public string LrNo { get; set; }
        public string LrDate { get; set; }
        public string TotalCaseQty { get; set; }
        public string VehicleNo { get; set; }
        public int AddedBy { get; set; }
        public string AddedOn { get; set; }
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public int TransitLRStatus { get; set; }
        public int IsMapDone { get; set; }
        public int IsCheckListDone { get; set; }
        public string LrDateNewFormat { get; set; }


    }

    public class InsuranceClaimModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceId { get; set; }
        public long ClaimId { get; set; }
        public string InvNo { get; set; }
        public string ClaimNo { get; set; }
        public DateTime ClaimDate { get; set; }
        public string ClaimAmount { get; set; }
        public string ClaimNote { get; set; }
        public string DebitAmount { get; set; }
        public string ClaimType { get; set; }
        public long ClaimTypeId { get; set; }
        public string Remark { get; set; }
        public string ClaimStatus { get; set; }
        public string DebitNo { get; set; }
        public DateTime DebitDate { get; set; }
        public string DebitNote { get; set; }
        public int Addedby { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Action { get; set; }
        public string EmailId { get; set; }
        public bool IsEmail { get; set; }
    }

    public class InvoiceListModel
    {
        public string InvNo { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int DeliveryNo { get; set; }
        public DateTime ActualGIDate { get; set; }
        public string RecPlant { get; set; }
        public string RecPlantDesc { get; set; }
        public string DispPlant { get; set; }
        public string DispPlantDesc { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string MaterialNo { get; set; }
        public string MatDesc { get; set; }
        public string UoM { get; set; }
        public string BatchNo { get; set; }
        public string Quantity { get; set; }
        public string TransporterCode { get; set; }
        public string TransporterName { get; set; }
        public string LrNo { get; set; }
        public long InvId { get; set; }
        public DateTime LrDate { get; set; }
        public string TotalCaseQty { get; set; }
        public string VehicleNo { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
    }

    public class MapInwardVehicleForMobModel
    {
        public long PkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public long InvId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InwardDate { get; set; }
        public int TransporterId { get; set; }
        public string MobileNo { get; set; }
        public string DriverName { get; set; }
        public string VehicleNo { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public string InvNo { get; set; }
        public string LRNo { get; set; }
        public DateTime LRDate { get; set; }
        public int NoOfCasesQty { get; set; }
        public int ActualNoOfCasesQty { get; set; }
        public string ConcernRemark { get; set; }
        public int ConcernBy { get; set; }
        public DateTime ConcernUpdatedOn { get; set; }
        public int IsConcern { get; set; }
        public int IsClaim { get; set; }
        public int IsSAN { get; set; }
        public int ResolvedBy { get; set; }
        public int IsChecklistDone { get; set; }
        public long TransitId { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }
    }

    public class RaiseConcernForTransitDataModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string LrNo { get; set; }
        public string InvoiceNo { get; set; }
        public string Remark { get; set; }
        public int RaiseConcernId { get; set; }
        public string AddedBy { get; set; }
    }    

    public class RaiseInsuranceClaimModel
    {
        public long ClaimId { get; set; }
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string LRNo { get; set; }
        public string ClaimNo { get; set; }
        public DateTime? ClaimDate { get; set; }
        public string ClaimAmount { get; set; }
        public string ClaimType { get; set; }
        public long ClaimTypeId { get; set; }
        public DateTime? EmailSendDate { get; set; }
        public string Remark { get; set; }
        public int IsEmailSend { get; set; }
        public int AddedBy { get; set; }
        public string Action { get; set; }
        public int IsConcern { get; set; }
        public string SANNo { get; set; }
        public string SANApproveBy { get; set; }
        public string ClaimApproveBy { get; set; }
        public DateTime? SANDate { get; set; }
        public string SANAmount { get; set; }
        public string ClaimSANNo { get; set; }
        public string ClaimSANAmount { get; set; }
        public DateTime? ClaimSANDate { get; set; }
        public long TransitId { get; set; }
        public int ActualNoOfCasesQty { get; set; }
        public int NoOfCasesQty { get; set; }
        public string ResolveVehicleRemark { get; set; }
    }

    public class GetRaiseInsuranceClaimListModel
    {
        public long TransitId { get; set; }
        public long ClaimId { get; set; }
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string LRNo { get; set; }
        public string ClaimNo { get; set; }
        public DateTime? ClaimDate { get; set; }
        public string ClaimAmount { get; set; }
        public string ClaimType { get; set; }
        public long ClaimTypeId { get; set; }
        public string ClaimStatus { get; set; }
        public string EmailSendDate { get; set; }
        public string Remark { get; set; }
        public int IsEmailSend { get; set; }
        public int AddedBy { get; set; }
        public string Action { get; set; }
        public int IsConcern { get; set; }
        public string SANNo { get; set; }
        public string SANApproveBy { get; set; }
        public string ClaimApproveBy { get; set; }
        public DateTime? SANDate { get; set; }
        public string SANAmount { get; set; }
        public string ClaimSANNo { get; set; }
        public string ClaimSANAmount { get; set; }
        public string ClaimSANDate { get; set; }
        public DateTime? EmailDate { get; set; }
        public string ApproveRemark { get; set; }
        public int IsClaim { get; set; }
        public int IsSAN { get; set; }
    }

    public class ApproveSAN
    {
        public long TransitId { get; set; }
        public int ClaimId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string SANNo { get; set; }
        public string SANApproveBy { get; set; }
        public DateTime SANDate { get; set; }
        public string SANRemark { get; set; }
    }

    public class ApproveClaim
    {
        public long TransitId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int? ClaimId { get; set; }
        public string ClaimNo { get; set; }
        public DateTime ApproveClaimDate { get; set; }
        public string ClaimApproveBy { get; set; }
        public string ClaimRemark { get; set; }

    }

    public class VehicleChecklistDtlsModel
    {
        public long ChkListMstId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int LREntryId { get; set; }
        public string Remarks { get; set; }
        public string SealNumber { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }
        public string Status { get; set; }
        public string IsApprove { get; set; }
        public string IsApproveBy { get; set; }
        public DateTime IsApproveOn { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public long ChkListDtlsId { get; set; }
        public string CLQueId { get; set; }
        public string CLQueText { get; set; }
        public string FieldType { get; set; }
        public int SortId { get; set; }
        public string AnsText { get; set; }
        public string ExpAnsText { get; set; }
        public int RetValue { get; set; }
        public long TransitId { get; set; }
    }

    public class ChecklistDtlsModel
    {
        public int pkId { get; set; }
        public string CLQueId { get; set; }
        public string AnsText { get; set; }
    }


    public class ChckelistDtls
    {
        public long ChkListMstId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int LREntryId { get; set; }
        public string Remarks { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public string LRNo { get; set; }
        public DateTime LRDate { get; set; }
        public string VehicleNo { get; set; }
        //public List<QueAnsDtls> QueAnsDetails { get; set; }
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public long TransitId { get; set; }
        public int IsChecklistDone { get; set; }
    }
    public class QueAnsDtls
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public long ChkListDtlsId { get; set; }
        public long ChkListMstId { get; set; }
        public string CLQueId { get; set; }
        public string CLQueText { get; set; }
        public string FieldType { get; set; }
        public int SortId { get; set; }
        public string AnsText { get; set; }
        public string ExpAnsText { get; set; }
    }

    public class RaiseConcernForTransitData
    {
        public long TransitId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int RaieseReqId { get; set; }
        public string LrNo { get; set; }
        public string InvoiceNo { get; set; }
        public string Remark { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public int ResolvedBy { get; set; }
    }

    public class InvInwardAllCountModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int TodayLR { get; set; }
        public int TodayVehicleMapped { get; set; }
        public int TodayChklistDone { get; set; }
        public int TodayConcernRaised { get; set; }
        public int TotalClaimRaised { get; set; }
        public int TotalSANRaised { get; set; }
        public int TodayMapConcernRaised { get; set; }
        public int TodayMapConcernResolved { get; set; }
        public int TotalClaimApproved { get; set; }
        public int TotalSANApproved { get; set; }
        public int PendingClaim { get; set; }
        public int PendingSAN { get; set; }
        public int TotalClaimSAN { get; set; }
        public int PendingClaimSANApproved { get; set; }
        public int TotalTodaysMapCnrnRaise { get; set; }
    }

    public class dashbordcountmodel
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int TotalVeh { get; set; }
        public int TodayVeh { get; set; }
        public int TotalCaseQty { get; set; }
        public int TodayCaseQty { get; set; }
        public int TodayClaimCnt { get; set; }
        public int PendClaimCnt { get; set; }
        public int TodaySANCnt { get; set; }
        public int PendSANCnt { get; set; }
        public int CummVehicle { get; set; }
        public int CummBoxes { get; set; }
    }

    public class DashboardInventoryInwrdMdal
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string DeliveryNo { get; set; }
        public DateTime ActualGIDate { get; set; }
        public string RecPlant { get; set; }
        public string RecPlantDesc { get; set; }
        public string DispPlant { get; set; }
        public string DispPlantDesc { get; set; }
        public string InvNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string MaterialNo { get; set; }
        public string MatDesc { get; set; }
        public string UOM { get; set; }
        public string Quantity { get; set; }
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public string LrNo { get; set; }
        public string LrDate { get; set; }
        public string Addedby { get; set; }
        public string AddedOn { get; set; }
        public string AddedOnNewFormat { get; set; }
        public int IsMapDone { get; set; }
        public int IsChecklistDone { get; set; }
        public long ClaimId { get; set; }
        public string ClaimDate { get; set; }
        public string ClaimDateNewFormat { get; set; }
        public string ClaimAmount { get; set; }
        public string ClaimNo { get; set; }
        public string ClaimRemark { get; set; }
        public string ClaimStatus { get; set; }
        public string ClaimType { get; set; }
        public DateTime DebitDate { get; set; }
        public string SANNo { get; set; }
        public string SANDate { get; set; }
        public long TransitId { get; set; }
        public string VehicleNo { get; set; }
        public string ClaimApproveBy { get; set; }
        public string SANApproveBy { get; set; }
        public string SANDateNewFormat { get; set; }
        public DateTime ClaimSANDate { get; set; }
        public string ClaimSANNo { get; set; }
        public int TotalCaseQty { get; set; }
    }

    // Not Used Class
    //public class InsuranceClaimforApprovalModel
    //{
    //    public string Emailid { get; set; }
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public string Email { get; set; }
    //    public string ClaimNo { get; set; }
    //    public DateTime ClaimDate { get; set; }
    //    public string ClaimAmount { get; set; }
    //}

    //public class VehicleChecklistModel
    //{
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public long ChecklistTypeId { get; set; }
    //    public string ChecklistType { get; set; }
    //    public long InvId { get; set; }
    //    public DateTime InvoiceDate { get; set; }
    //    public int TransporterId { get; set; }
    //    public string VehicleNo { get; set; }
    //    public string IsColdStorage { get; set; }
    //    public string Remarks { get; set; }
    //    public string SealNumber { get; set; }
    //    public string Comments { get; set; }
    //    public string Action { get; set; }
    //    public string AddedBy { get; set; }
    //}

    //public class InvInVehicleCheckListmodel
    //{
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public long ChecklistTypeId { get; set; }
    //    public string ChecklistType { get; set; }
    //    public long InvId { get; set; }
    //    public string InvNo { get; set; }
    //    public DateTime InvoiceDate { get; set; }
    //    public int TransporterId { get; set; }
    //    public string TransporterNo { get; set; }
    //    public string TransporterName { get; set; }
    //    public string VehicleNo { get; set; }
    //    public string IsColdStorage { get; set; }
    //    public string Status { get; set; }
    //    public string Remarks { get; set; }
    //    public string SealNumber { get; set; }
    //    public string Comments { get; set; }
    //    public string AddedBy { get; set; }
    //    public DateTime AddedOn { get; set; }
    //    public DateTime LastUpdatedOn { get; set; }
    //    public long pkId { get; set; }
    //    public string IsApprove { get; set; }
    //}

    //public class UpdateResolveVehicleLRIssueModel
    //{
    //    public long pkId { get; set; }
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public int IsConcern { get; set; }
    //    public string IsApproveBy { get; set; }
    //}

    //public class InvInwardRaiseRequestByIdForModel
    //{
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public long InvId { get; set; }
    //    public string Remarks { get; set; }
    //    public string RaiseRequestBy { get; set; }
    //}

    //public class LRDetailsModel
    //{
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public long InvId { get; set; }
    //    public DateTime InvoiceDate { get; set; }
    //    public string LRNo { get; set; }
    //    public DateTime LRDate { get; set; }
    //    public string NoOfCase { get; set; }
    //    public string ActualNoOfCase { get; set; }
    //    public string Remarks { get; set; }
    //    public string Action { get; set; }
    //    public string AddedBy { get; set; }
    //    public DateTime AddedOn { get; set; }
    //    public DateTime LastUpdatedOn { get; set; }
    //}

    //public class InvInVehicleCheckListmodel
    //{
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public long ChecklistTypeId { get; set; }
    //    public string ChecklistType { get; set; }
    //    public long InvId { get; set; }
    //    public string InvNo { get; set; }
    //    public DateTime InvoiceDate { get; set; }
    //    public int TransporterId { get; set; }
    //    public string TransporterNo { get; set; }
    //    public string TransporterName { get; set; }
    //    public string VehicleNo { get; set; }
    //    public string IsColdStorage { get; set; }
    //    public string Status { get; set; }
    //    public string Remarks { get; set; }
    //    public string SealNumber { get; set; }
    //    public string Comments { get; set; }
    //    public string AddedBy { get; set; }
    //    public DateTime AddedOn { get; set; }
    //    public DateTime LastUpdatedOn { get; set; }
    //}

    //public class InvInVehicleChecklistMaster
    //{
    //    public long ChecklistTypeId { get; set; }
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public string ChecklistType { get; set; }
    //    public string Section { get; set; }
    //    public string QuestionName { get; set; }
    //    public string IsActive { get; set; }
    //    public string AddedBy { get; set; }
    //    public DateTime AddedOn { get; set; }
    //    public DateTime LastUpdatedOn { get; set; }
    //}

    //public class RaiseConcernForTransitDataModel
    //{
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public string LrNo { get; set; }
    //    public string InvoiceNo { get; set; }
    //    public string Remark { get; set; }
    //    public string AddedBy { get; set; }
    //}

    //public class MapInwardVehicleRsolveCncrnModel
    //{
    //    public long pkId { get; set; }
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public string LRNo { get; set; }
    //    public DateTime LRDate { get; set; }
    //    public DateTime InwardDate { get; set; }
    //    public int TransporterId { get; set; }
    //    public string VehicleNo { get; set; }
    //    public string DriverName { get; set; }
    //    public string MobileNo { get; set; }
    //    public int NoOfCasesQty { get; set; }
    //    public int ActualNoOfCasesQty { get; set; }
    //    public string ConcernRemark { get; set; }
    //    public DateTime AddedOn { get; set; }
    //    public string AddedBy { get; set; }
    //    public int ConcernBy { get; set; }
    //    public DateTime ConcernUpdatedOn { get; set; }
    //    public string TransporterName { get; set; }
    //    public string TransporterNo { get; set; }
    //    public int IsConcern { get; set; }
    //}

    //public class RaiseInsuranceClaimModel
    //{
    //    public long ClaimId { get; set; }
    //    public int pkId { get; set; }
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public string LRNo { get; set; }
    //    public string ClaimNo { get; set; }
    //    public DateTime? ClaimDate { get; set; }
    //    public string ClaimAmount { get; set; }
    //    public string ClaimType { get; set; }
    //    public long ClaimTypeId { get; set; }
    //    public DateTime? EmailSendDate { get; set; }
    //    public string Remark { get; set; }
    //    public int IsEmailSend { get; set; }
    //    public int AddedBy { get; set; }
    //    public string Action { get; set; }
    //    public int IsConcern { get; set; }
    //    public string SANNo { get; set; }
    //    public string SANApproveBy { get; set; }
    //    public string ClaimApproveBy { get; set; }
    //    public DateTime? SANDate { get; set; }
    //    public string SANAmount { get; set; }
    //    public string ClaimSANNo { get; set; }
    //    public string ClaimSANAmount { get; set; }
    //    public DateTime? ClaimSANDate { get; set; }
    //}

    //public class GetRaiseInsuranceClaimListModel
    //{
    //    public long ClaimId { get; set; }
    //    public int pkId { get; set; }
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public string LRNo { get; set; }
    //    public string ClaimNo { get; set; }
    //    public DateTime? ClaimDate { get; set; }
    //    public string ClaimAmount { get; set; }
    //    public string ClaimType { get; set; }
    //    public long ClaimTypeId { get; set; }
    //    public string EmailSendDate { get; set; }
    //    public string Remark { get; set; }
    //    public int IsEmailSend { get; set; }
    //    public int AddedBy { get; set; }
    //    public string Action { get; set; }
    //    public int IsConcern { get; set; }
    //    public string SANNo { get; set; }
    //    public string SANApproveBy { get; set; }
    //    public string ClaimApproveBy { get; set; }
    //    public DateTime? SANDate { get; set; }
    //    public string SANAmount { get; set; }
    //    public string ClaimSANNo { get; set; }
    //    public string ClaimSANAmount { get; set; }
    //    public string ClaimSANDate { get; set; }
    //}

    //public class ApproveSAN
    //{
    //    public int ClaimId { get; set; }
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public int SANNo { get; set; }
    //    public string SANApproveBy { get; set; }
    //    public DateTime SANDate { get; set; }
    //    public string SANRemark { get; set; }
    //}

    //public class ApproveClaim
    //{
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public int? ClaimId { get; set; }
    //    public string ClaimNo { get; set; }
    //    public DateTime ApproveClaimDate { get; set; }
    //    public string ClaimApproveBy { get; set; }
    //    public string ClaimRemark { get; set; }
    //}

    //public class VehicleChecklistDtlsModel
    //{
    //    public long ChkListMstId { get; set; }
    //    public int BranchId { get; set; }
    //    public int CompId { get; set; }
    //    public int LREntryId { get; set; }
    //    public string Remarks { get; set; }
    //    public string SealNumber { get; set; }
    //    public string Img1 { get; set; }
    //    public string Img2 { get; set; }
    //    public string Img3 { get; set; }
    //    public string Img4 { get; set; }
    //    public string Status { get; set; }
    //    public string IsApprove { get; set; }
    //    public string IsApproveBy { get; set; }
    //    public DateTime IsApproveOn { get; set; }
    //    public string AddedBy { get; set; }
    //    public DateTime AddedOn { get; set; }
    //    public DateTime LastUpdatedOn { get; set; }
    //    public long ChkListDtlsId { get; set; }
    //    public string CLQueId { get; set; }
    //    public string CLQueText { get; set; }
    //    public string FieldType { get; set; }
    //    public int SortId { get; set; }
    //    public string AnsText { get; set; }
    //    public string ExpAnsText { get; set; }
    //    public int RetValue { get; set; }
    //}

    //public class ChecklistDtlsModel
    //{
    //    public int pkId { get; set; }
    //    public string CLQueId { get; set; }
    //    public string AnsText { get; set; }
    //}

    public class InwardSupervisorDashboardMobModel
    {
        public long TransitId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string InvNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Quantity { get; set; }
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public string LrNo { get; set; }
        public DateTime LrDate { get; set; }
        public string TotalCaseQty { get; set; }
        public string VehicleNo { get; set; }
        public int IsMapDone { get; set; }
        public long mivpkId { get; set; }
        public DateTime mivInwardDate { get; set; }
        public string mivVehicleNo { get; set; }
        public string mivDriverName { get; set; }
        public string mivMobileNo { get; set; }
        public int mivNoOfCasesQty { get; set; }
        public int mivActualNoOfCasesQty { get; set; }
        public string mivConcernRemark { get; set; }
        public DateTime mivConcernUpdatedOn { get; set; }
        public int mivIsConcern { get; set; }
        public int mivResolvedBy { get; set; }
        public int IsChecklistDone { get; set; }
        public int ConcernBy { get; set; }
        public long ChkListMstId { get; set; }
        public int LREntryId { get; set; }
        public string Remarks { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }

    }

    public class VehicleChkLstReport
    {
        public int CompId { get; set; }
        public int BranchId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class VhcleChkLstForViewImgModel
    {
        public long pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string LRNo { get; set; }
        public DateTime LRDate { get; set; }
        public DateTime InwardDate { get; set; }
        public int TransporterId { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string MobileNo { get; set; }
        public int NoOfCasesQty { get; set; }
        public int ActualNoOfCasesQty { get; set; }
        public string ConcernRemark { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }
        public int ConcernBy { get; set; }
        public DateTime ConcernUpdatedOn { get; set; }
        public int IsConcern { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public int IsClaim { get; set; }
        public int IsSAN { get; set; }
        public int ResolvedBy { get; set; }
        public long TransitId { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }
    }

    public class OwnInvInwardDashSmmryList
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CompId { get; set; }
        public string CompanyName { get; set; }
        public int PendSANCnt { get; set; }
        public int PendClaimCnt { get; set; }
    }

}
