using System;
using System.Collections.Generic;

namespace CNF.Business.Model.OrderDispatch
{
    public class PickListModel
    {
        public int Picklistid { get; set; }
        public int PickerId { get; set; }
        public string PickerName { get; set; }
        public string PickerNo { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string PicklistNo { get; set; }
        public DateTime PicklistDate { get; set; }
        public string FromInv { get; set; }
        public string ToInv { get; set; }
        //public string divisionStr { get; set; }
        public int PicklistStatus { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime VerifiedDate { get; set; }
        public int AllottedBy { get; set; }
        public int AllotmentId { get; set; }
        public DateTime AllottedDate { get; set; }
        public int ReAllottedBy { get; set; }
        public DateTime ReAllotedDate { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Action { get; set; }
        public string StatusText { get; set; }
        public string RejectReason { get; set; }
        public string RConvern { get; set; }
        public string DisplayName { get; set; }
        public int ReAllotmentId { get; set; }
        public int ReAllotmentStatus { get; set; }
        public int PickerConcernId { get; set; }
        public string RejectRemark { get; set; }
        public string MasterName { get; set; }
        public string PickerConcernRemark { get; set; }
        public int VerifiedConcernId { get; set; }
        public string VerifiedConcernText { get; set; }
        public string VerifiedConcernRemark { get; set; }
        public DateTime AcceptedDate { get; set; }
        public DateTime PickedDate { get; set; }
        public DateTime PickerConcernDate { get; set; }
        public string PicklistStatusText { get; set; }
        public int ReasonId { get; set; }
        public string pickerconcernText { get; set; }
        public string AllotmentStatusText { get; set; }
        public string AllotmentStatus { get; set; }
        public int CurrentStatus { get; set; }
        public string ResolveRemark { get; set; }
        public int UpdatedBy { get; set; }
        public int RetValue { get; set; }
        public DateTime StatusTime { get; set; }
        public string ReasonText { get; set; }       

        public List<PickListDetailsByPicker> PickListByPicker { get; set; }
        public int InvId { get; set; }
        public string Remark { get; set; }
        public DateTime updateDate { get; set; }
        public int OnPriority { get; set; }
        public int IsStockTransfer { get; set; }
    }

    public class PickListDetailsByPicker
    {
        public int Picklistid { get; set; }
        public int PickerId { get; set; }
        public string PickerName { get; set; }
        public int AllotmentId { get; set; }
        public int AllotmentStatus { get; set; }
        public string AllotmentStatusText { get; set; }
        public int ReasonId { get; set; }
        public string ReasonText { get; set; }
        public string RejectRemark { get; set; }
        public int PickerConcernId { get; set; }
        public string pickerconcernText { get; set; }
        public string PickerConcernRemark { get; set; }
        public int VerifiedBy { get; set; }
        public string VerifiedByName { get; set; }
        public int VerifiedConcernId { get; set; }
        public string VerifiedConcernText { get; set; }
        public string VerifiedConcernRemark { get; set; }
        public string AcceptedDate { get; set; }
        public string PickedDate { get; set; }
        public string PickerConcernDate { get; set; }
        public string VerifiedDate { get; set; }
        public string RejectReason { get; set; }
        public string PickListAddFlag { get; set; }
    }

    public class PickListDetailsByIdModel
    {
        public int PicklistDtlsId { get; set; }
        public int Picklistid { get; set; }
        public int DivisionId { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }

    public class PicklstAllotReallotModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int Picklistid { get; set; }
        public int AllotmentId { get; set; }
        public int PicklistDtlsId { get; set; }
        public string ProductCode { get; set; }
        public string BatchNo { get; set; }
        public string PickerId { get; set; }
        public int AllottedBy { get; set; }
        public int ReAllottedBy { get; set; }
        public string PickListAddFlag { get; set; }
    }

    public class PicklistAllotmentStatusModel
    {
        public int AllotmentId { get; set; }
        public int Picklistid { get; set; }
        public int BranchId { get; set; }
        public int AllotmentStatus { get; set; }
        public int ReasonId { get; set; }
        public string RejectRemark { get; set; }
        public DateTime StatusTime { get; set; }
        public int UserId { get; set; }
    }

    public class InvoiceLstModel
    {
        public long InvId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string InvNo { get; set; }
        public DateTime InvCreatedDate { get; set; }
        public string InvCreatedDateNewFormat { get; set; }
        public bool IsColdStorage { get; set; }
        public int SoldTo_StokistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string SoldTo_City { get; set; }
        public Double InvAmount { get; set; }
        public Nullable<int> InvStatus { get; set; }
        public string StatusText { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedOnNewforFilter { get; set; }
        public string CityCode { get; set; }
        public string Action { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public int BillDrawerId { get; set; }
        public string CityName { get; set; }
        public int IsCourier { get; set; }
        public int AcceptedBy { get; set; }
        public string BillDrawerName { get; set; }
        public DateTime AcceptedDate { get; set; }
        public DateTime BillDrawnDate { get; set; }
        public int PackedBy { get; set; }
        public string PackedByName { get; set; }
        public DateTime PackedDate { get; set; }
        public string PackedDateNewFormatForFilter { get; set; }
        public string PackedDateNewFormatForDayDiff { get; set; }
        public int MyProperty { get; set; }
        public int NoOfBox { get; set; }
        public decimal InvWeight { get; set; }
        public DateTime PackingConcernDate { get; set; }
        public int PackingConcernId { get; set; }
        public string PackingConcernText { get; set; }
        public string PackingRemark { get; set; }
        public int ReadyToDispatchBy { get; set; }
        public string ReadyToDispatchDateNewFormanCnt { get; set; }
        public DateTime ReadyToDispatchDate { get; set; }
        public int ReadyToDispatchConcernId { get; set; }
        public string ReadyToDispatchRemark { get; set; }
        public int CancelBy { get; set; }
        public DateTime CancelDate { get; set; }
        public int TotalInv { get; set; }
        public string DispatchByName { get; set; }
        public string DispatchConcernText { get; set; }
        public int OnPriority { get; set; }
        public int CurrentStatus { get; set; }
        public string Remark { get; set; }
        public DateTime updateDate { get; set; }
        public int IsStockTransfer { get; set; }
        public string PrintStatus { get; set; }
        public int TransportModeId { get; set; }
        public string LRNo { get; set; }
        public string LRDate { get; set; }
        public int LRBox { get; set; }
        public DateTime currenDateTime { get; set; }
        public string InvCreatedDayDiff { get; set; }
    }

    public class DivisionDtls
    {
        public int? InvDtlsId { get; set; }
        public int? InvId { get; set; }
        public int? DivisionId { get; set; }
        public string ProdCode { get; set; }
        public string BatchNo { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }

    }

    // Import Invoice Data
    public class ImportInvoiceDataModel
    {
        public long pkId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceCreateDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string SoldToCode { get; set; }
        public string SoldToName { get; set; }
        public string SoldToCity { get; set; }
        public string SONo { get; set; }
        public string SODate { get; set; }
        public string ItmCtgryDescription { get; set; }
    }

    public class Picklstmodel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int PickerId { get; set; }
        public int AllotmentId { get; set; }
        public int Picklistid { get; set; }
        public int AllottedBy { get; set; }
        public DateTime AllottedDate { get; set; }
        public int AllotmentStatus { get; set; }
        public string AllotmentStatusText { get; set; }
        public string PicklistNo { get; set; }
        public DateTime PicklistDate { get; set; }
        public string FromInv { get; set; }
        public string ToInv { get; set; }
        public int PicklistStatus { get; set; }
        public string PicklistStatusText { get; set; }
        public DateTime AcceptedDate { get; set; }
        public int ReasonId { get; set; }
        public string ReasonText { get; set; }
        public string RejectRemark { get; set; }
        public DateTime PickedDate { get; set; }
        public int PickerConcernId { get; set; }
        public string pickerconcerText { get; set; }
        public string PickerConcernRemark { get; set; }
        public DateTime PickerConcernDate { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime VerifiedDate { get; set; }
        public string VerifiedByName { get; set; }
        public int VerifiedConcernId { get; set; }
        public string VerifiedConcernText { get; set; }
        public string VerifiedConcernRemark { get; set; }
        public int OnPriority { get; set; }
        public int IsStockTransfer { get; set; }
    }

    public class InvoiceHeaderStatusUpdateModel
    {
        public int InvId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int InvStatus { get; set; }
        public int NoOfBox { get; set; }
        public int PackedBy { get; set; }
        public decimal InvWeight { get; set; }
        public bool IsColdStorage { get; set; }
        public int IsCourier { get; set; }
        public int ConcernId { get; set; }
        public string Remark { get; set; }
        public string Addedby { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    
    public class AssignTransportModel
    {
        public string InvoiceId { get; set; }
        public int TransportModeId { get; set; }
        public string PersonName { get; set; }
        public string PersonAddress { get; set; }
        public string PersonMobileNo { get; set; }
        public string OtherDetails { get; set; }
        public int TransporterId { get; set; }
        public string Delivery_Remark { get; set; }
        public int CAId { get; set; }
        public int CourierId { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
        public long AttachedInvId { get; set; }
    }

    public class PickLstSummaryData
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public DateTime PicklistDate { get; set; }
        public int PickerId { get; set; }
        public int TotalPL { get; set; }
        public int OperatorRejected { get; set; }
        public int Alloted { get; set; }
        public int Pending { get; set; }
        public int Rejected { get; set; }
        public int Accepted { get; set; }
        public int Concern { get; set; }
        public int Completed { get; set; }
        public int Verified { get; set; }
        public int CompletedVerified { get; set; }
    }

    public class ImportLrDataModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public long pkId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string LRNo { get; set; }
        public DateTime LRDate { get; set; }
        public int LRBox { get; set; }
        public decimal LRWeightInKG { get; set; }
        public int StokistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public int NoOfBox { get; set; }
        public long InvId { get; set; }
        public string InvNo { get; set; }
        public int OnPriority { get; set; }
        public string LRDatestring { get; set; }
        public int IsStockTransfer { get; set; }
    }

    public class ImportLRNewModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public long pkId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string LRNo { get; set; }
        public string LRDate { get; set; }
        public string LRDatestring { get; set; }
        public string LRBox { get; set; }
        public decimal LRWeightInKG { get; set; }
        public int StokistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public int NoOfBox { get; set; }
        public long InvId { get; set; }
        public string InvNo { get; set; }
        public int OnPriority { get; set; }
        public string RetResult { get; set; }
    }

    public class GeneratePDFModel
    {
        public int InvId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string Type { get; set; }
        public string BoxNo { get; set; }
        public string AddedBy { get; set; }
    }

    public class GetInvoiceDetailsForStickerModel
    {
        public long InvId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string InvNo { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string MobNo { get; set; }
        public string StockistAddress { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public int TransportModeId { get; set; }
        public string PersonName { get; set; }
        public string PersonAddress { get; set; }
        public string PersonMobNo { get; set; }
        public string OtherDetails { get; set; }
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public int CourierId { get; set; }
        public string CourierName { get; set; }
        public string Delivery_Remark { get; set; }
        public int SoldTo_StokistId { get; set; }
        public double InvAmount { get; set; }
        public int InvStatus { get; set; }
        public int NoOfBox { get; set; }
        public decimal InvWeight { get; set; }
        public int IsCourier { get; set; }
        public int OnPriority { get; set; }
        public int IsStockTransfer { get; set; }
        public int AttachedInvId { get; set; }
    }

    public class InvCntModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public DateTime InvDate { get; set; }
        public int TotalInv { get; set; }
        public int CancelInv { get; set; }
        public int AcceptedInv { get; set; }
        public int PendingForAcceptInv { get; set; }
        public int InvoiceDrawn { get; set; }
        public int Packed { get; set; }
        public int ReadyToDispatch { get; set; }
        public int Concern { get; set; }
        public int GetpassGenerated { get; set; }
        public int Dispatched { get; set; }
    }

    public class InvSts
    {
        public int Id { get; set; }
        public string StatusText { get; set; }
    }

    public class GatePassModel
    {
        public int GatepassId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int CAId { get; set; }
        public DateTime GatepassDate { get; set; }
        public string VehicleNo { get; set; }
        public string InvStr { get; set; }
        public int GuardNameId { get; set; }
        public string GuardNameText { get; set; }
        public string DriverName { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
    }

    public class PrinterDtls
    {
        public int PrinterId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string PrinterName { get; set; }
        public string PrinterType { get; set; }
        public string PrinterIPAddress { get; set; }
        public int PrinterPortNumber { get; set; }
        public string Flag { get; set; }
        public string AddedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Action { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public int UtilityNo { get; set; }
    }

    // Printer Log Add/Edit
    public class PrinterLogAddEditModel
    {
        public decimal PrinterLogID { get; set; }
        public string PrinterLogFor { get; set; }
        public string PrinterLogData { get; set; }
        public string PrinterLogStatus { get; set; }
        public DateTime PrinterLogDatetime { get; set; }
        public string PrinterLogException { get; set; }
    }

    // Print PDF Data - PDF File SAVE PATH ADD/EDIT
    public class PrintPDFDataModel
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public long InvId { get; set; }
        public int GpId { get; set; }
        public string Type { get; set; }
        public string Flag { get; set; }
        public string Action { get; set; }
        public string AddedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string BoxNo { get; set; }
        public int PrinterId { get; set; }
        public string PrinterIPAddress { get; set; }
        public string PrinterName { get; set; }
        public int PrinterPortNumber { get; set; }
        public int PrintCount { get; set; }
        public int UtilityNo { get; set; }
    }

    public class InvGatpassDtls
    {
        public int GatepassId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string GatepassNo { get; set; }
        public DateTime GatepassDate { get; set; }
        public string VehicleNo { get; set; }
        public int GuardNameId { get; set; }
        public string EmpName { get; set; }
        public string EmpNo { get; set; }
        public string DriverName { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int IsPrinted { get; set; }
        public int TransportModeId { get; set; }
        public string TrasportModeText { get; set; }
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public int CourierId { get; set; }
        public string CourierName { get; set; }
        public string Delivery_Remark { get; set; }
        public string LRNo { get; set; }
        public string InvoiceId { get; set; }
        public string InvNo { get; set; }
        public int SoldTo_StokistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string MobNo { get; set; }
        public string Emailid { get; set; }
        public string SoldTo_City { get; set; }
        public string CityName { get; set; }
        public int NoOfBox { get; set; }
        public int IsCourier { get; set; }
        public int CancelBy { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BrCitycode { get; set; }
        public string BrCityName { get; set; }
        public string brPin { get; set; }
        public string brContactNo { get; set; }
        public string brEmail { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompContactNo { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCityCode { get; set; }
        public string CompanyCityName { get; set; }
        public string CompanyPin { get; set; }
        public int CAId { get; set; }
        public string GuardNameText { get; set; }
        public string CAName { get; set; }
        public string TransCourNameInv { get; set; }
    }

    public class GenerateGatepassPDFModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int GPid { get; set; }
        public string Type { get; set; }
        public string AddedBy { get; set; }
    }

    public class GatepassDtls
    {
        public int GatepassId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string GatepassNo { get; set; }
        public DateTime GatepassDate { get; set; }
        public string VehicleNo { get; set; }
        public int GuardNameId { get; set; }
        public string EmpName { get; set; }
        public string EmpNo { get; set; }
        public string DriverName { get; set; }
        public string Addedby { get; set; }
        public DateTime AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int IsPrinted { get; set; }
        public int NoOfInv { get; set; }
        public string InvIdStr { get; set; }
        public int CAId { get; set; }
        public string CAName { get; set; }
        public string GuardNameText { get; set; }
        public int IsStockTransfer { get; set; }
    }

    public class InvDtlsForMob
    {
        public long InvId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string InvNo { get; set; }
        public DateTime InvCreatedDate { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string MobNo { get; set; }
        public string StockistAddress { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public int TransportModeId { get; set; }
        public string PersonName { get; set; }
        public string PersonAddress { get; set; }
        public string PersonMobNo { get; set; }
        public string OtherDetails { get; set; }
        public int TransporterId { get; set; }
        public string TransporterNo { get; set; }
        public string TransporterName { get; set; }
        public int CourierId { get; set; }
        public string CourierName { get; set; }
        public string Delivery_Remark { get; set; }
        public int SoldTo_StokistId { get; set; }
        public double InvAmount { get; set; }
        public int InvStatus { get; set; }
        public int NoOfBox { get; set; }
        public decimal InvWeight { get; set; }
        public int IsCourier { get; set; }
        public int OnPriority { get; set; }
        public string TransportModeText { get; set; }
        public int AttachedInvId { get; set; }
        public string BrCityCode { get; set; }
        public int IsStockTransfer { get; set; }
        public string ScannedBoxes { get; set; }
        public string TrnsCourName { get; set; }
        public int InvCnt { get; set; }

    }

    public class InvDtlsForEmail
    {
        public string TransporterName { get; set; }
        public string LRNo { get; set; }
        public string StockistName { get; set; }
        public string Emailid { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }

    }

    public class ImportReturnModel
    {
        public string RetResult { get; set; }
    }

    public class PriorityFlagUpdtModel
    {
        public int InvId { get; set; }
        public int PriorityFlag { get; set; }
        public string Remark { get; set; }
        public string Addedby { get; set; }
        public DateTime updateDate { get; set; }
    }

    public class AssignedTransportModel
    {
        public int AssignTransMId { get; set; }
        public string InvoiceId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string InvNo { get; set; }
        public string InvNo1 { get; set; }
        public DateTime InvCreatedDate { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string SoldTo_City { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public int TransportModeId { get; set; }
        public string PersonName { get; set; }
        public string PersonAddress { get; set; }
        public string PersonMobileNo { get; set; }
        public string OtherDetails { get; set; }
        public string TransporterName { get; set; }
        public int TransporterId { get; set; }
        public string CourierName { get; set; }
        public int CourierId { get; set; }
        public string Delivery_Remark { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Addedby { get; set; }
        public int OnPriority { get; set; }
        public int OCnfCity { get; set; }
        public int IsStockTransfer { get; set; }
        public int NoOfBox { get; set; }
        public decimal InvWeight { get; set; }
        public int IsCourier { get; set; }
    }

    public class CheckInvNoExitModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int InvId { get; set; }
    }

    public class Responce
    {
        public int GatepassId { get; set; }
        public string InvIdStr { get; set; }
    }

    public class SaveAndPrintModel
    {
        public int pkId { get; set; }
        public string InvoiceId { get; set; }
        public int TransportModeId { get; set; }
        public string PersonName { get; set; }
        public string PersonAddress { get; set; }
        public string PersonMobileNo { get; set; }
        public string OtherDetails { get; set; }
        public int TransporterId { get; set; }
        public string Delivery_Remark { get; set; }
        public int CAId { get; set; }
        public int CourierId { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
        public long AttachedInvId { get; set; }
        public int InvId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string Type { get; set; }
        public string BoxNo { get; set; }
        public int OCnfCity { get; set; }
    }

    public class InvoiceCountsModel
    {
        public int TotalInvoices { get; set; }
        public int TodaysWithOldOpen { get; set; }
        public int CancelInvCtn { get; set; }
        public int PendingInvCtn { get; set; }
        public int OnPriorityCtn { get; set; }
        public int PackerConcern { get; set; }
        public int GatpassGenCtn { get; set; }
        public int PendingLR { get; set; }
        public int IsStockTransferCtn { get; set; }
        public int StkPrint { get; set; }
        public int LocalMode { get; set; }
        public int OtherCity { get; set; }
        public int ByHand { get; set; }
        public int InvoiceId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }

    }

    public class SaveScannedInvData
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string ScannedData { get; set; }
        public int AddedBy { get; set; }
    }

    public class InvIdScannedBoxes
    {
        public int pkId { get; set; }
        public int InvId { get; set; }
        public string ScannedBoxes { get; set; }
    }
    public class OrderDispatchCountFordashModel  
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int TotalInvoices { get; set; }
        public int TodayInv { get; set; }
        public int TodaysBoxes { get; set; }
        public int CummSaleAmt { get; set; }
        public int CummPL { get; set; }
        public int OrderDispatchCummPL { get; set; }
        public int InvPerMonth { get; set; }
        public int CummBoxes { get; set; }
        public int PendingLR { get; set; }
        public int DispatchN { get; set; }
        public int DispatchN1 { get; set; }
        public int DispatchN2 { get; set; }
        public int DispatchPending { get; set; }
        public int SalesAmtToday { get; set; }
        public int SalesAmtCumm { get; set; }
        public int NotDispPckdInv { get; set; }
        public int NotDispPckdBox { get; set; }
        public int LocalMode { get; set; }
        public int OtherCity { get; set; }
        public int ByHand { get; set; }
        public int PriorityInvToday { get; set; }
        public int PriorityPending { get; set; }
        public int InvConcernPending { get; set; }
        public int StkPrintedToday { get; set; }
        public int StkPending { get; set; }
        public int GPTodayCreated { get; set; }
        public int GPPending { get; set; }
        public int TPBox { get; set; }
        public int TPStockiest { get; set; }
        public int BoxForLR { get; set; }
        public int StkInv { get; set; }
        public int PLToday { get; set; }
        public int PLPending { get; set; }
        public int PLConcern { get; set; }
        public int PLVerifiedToday { get; set; }
        public int PLVerifiedPending { get; set; }
        public int PLAllotedToday { get; set; }
        public int PLAllotedPending { get; set; }
        public int PendingInv { get; set; }
        public decimal StkrPendingAmt { get; set; }
        public decimal GPPendingAmt { get; set; }
        public int CummInvCnt { get; set; }
        public int CummBoxCnt { get; set; }
        public int CummPLCnt { get; set; }
        public decimal TodaySalesAmt { get; set; }
        public decimal CummSalesAmt { get; set; }
        public int LocalTotalDisp { get; set; }
        public int OtherTotalDisp { get; set; }
        public int ByHandTotalDisp { get; set; }
        public int DispatchNPer { get; set; }
        public int DispatchN1Per { get; set; }
        public int DispatchN2Per { get; set; }
        public int PLCompVerifyPending { get; set; }
        public int PrevTotalDisp { get; set; }
        public int PrevN { get; set; }
        public int PrevN1 { get; set; }
        public int PrevN2 { get; set; }
        public int PrevNPer { get; set; }
        public int PrevN1Per { get; set; }
        public int PrevN2Per { get; set; }
    } 
    public class DashOrderDispatchList
    {
        public long InvId { get; set; }
        public string InvNo { get; set; }
        public string InvCreatedDate { get; set; }
        public int IsColdStorage { get; set; }
        public decimal InvAmount { get; set; }
        public int IsStockTransfer { get; set; }
        public int InvStatus { get; set; }
        public string PackedDate { get; set; }
        public int PackedBy { get; set; }
        public long StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public int DispatchN { get; set; }
        public int DispatchN1 { get; set; }
        public int DispatchN2 { get; set; }
        public long InvoiceIdTM { get; set; }
        public long AttachedInvId { get; set; }
        public int NoOfBox { get; set; }
        public string LRDate { get; set; }
        public int TransportModeId { get; set; }
        public DateTime GatepassDate { get; set; }
        public string ReadyToDispatchDate { get; set; }
        public int OnPriority { get; set; }
        public int PrioPending { get; set; }
    }
    public class OrderDispPLModelList
    {
        public string PicklistNo { get; set; }
        public DateTime PicklistDate { get; set; }
        public string PLDate { get; set; }
        public string FromInv { get; set; }
        public string ToInv { get; set; }
        public int PicklistStatus { get; set; }
        public string VerifiedDate { get; set; }
        public string StatusText { get; set; }
        public int IsStockTransfer { get; set; }
        public string AllottedDate { get; set; }
    }

    public class OrderDispatchSmmryCnt
    {
        public DateTime InvDate { get; set; }
        public int InvCount { get; set; }
        public decimal InvAmount { get; set; }
        public long NoOfBox { get; set; }
    }

    public class GetUtilityNoNewModel
    {
        public int UtilityNo { get; set; }
    }

    public class OwnOrdrDispInvSmmryList
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CompId { get; set; }
        public string CompanyName { get; set; }
        public int PrioPending { get; set; }
        public int StkrPending { get; set; }
        public decimal StkrPendingAmt { get; set; }
        public int GPPending { get; set; }
        public decimal GPPendingAmt { get; set; }
    }

    public class OwnOrdrDispBoxesSmmryList
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CompId { get; set; }
        public string CompanyName { get; set; }
        public int InvCount { get; set; }
        public decimal InvAmount { get; set; }
        public int NoOfBoxes { get; set; }
    }
    public class LRDetailsListForDash
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public long pkId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string LRNo { get; set; }
        public DateTime LRDate { get; set; }
        public int LRBox { get; set; }
        public decimal LRWeightInKG { get; set; }
        public int StokistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public int NoOfBox { get; set; }
        public long InvId { get; set; }
        public string InvNo { get; set; }
        public int OnPriority { get; set; }
        public string LRDatestring { get; set; }
        public int IsStockTransfer { get; set; }
    }

    public class GetColumnHeaderModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public long pkId { get; set; }
        public string ImpFor { get; set; }
        public string FieldName { get; set; }
        public string ExcelColName { get; set; }
        public string ColumnDatatype { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }       
    }

    public class transporterForMonthlyModel
    {
        public int TransporterId { get; set; }      
        public string TransportName { get; set; }
        public DateTime? GatepassDate { get; set; }
        public string InvNo { get; set; }
        public int NoOfBoxes { get; set; }
        public int CompId { get; set; }
        public int BranchId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int RatePerBox { get; set; }
        public int TotalAmount { get; set; }
        public string RatePerBoxString { get; set; }
        public int CourierId { get; set; }
        public int TransportModeId { get; set; }
    }
    public class TransporterForMonthlyModels
    {
        public int TransporterId { get; set; }
        public string TransportName { get; set; }
        public string NoOfInvoice { get; set; }
        public int NoOfBoxes { get; set; }
        public int CompId { get; set; }
        public int BranchId { get; set; }
        public DateTime? FromDate1 { get; set; }
        public DateTime? ToDate1 { get; set; }
        public int RatePerBox { get; set; }
        public int TotalAmt { get; set; }
    }
    public class DashOrderDispatchListPrevMonth
    {
        public long InvId { get; set; }
        public string InvNo { get; set; }
        public string InvCreatedDate { get; set; }
        public int NoOfBox { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public decimal InvAmount { get; set; }
        public int IsStockTransfer { get; set; }
        public int PrevN { get; set; }
        public int PrevN1 { get; set; }
        public int PrevN2 { get; set; }
        public int PrevTotalDisp { get; set; }
        public int InvStatus { get; set; }
        public int TransportModeId { get; set; }

    }

    public class GatepassDetailsModal
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int BranchId { get; set; }

        public int CompanyId { get; set; }
    }

    public class GatepassListModal
    {
        public int GatepassId { get; set; }

        public string GatepassNo { get; set; }

        public DateTime GatepassDate { get; set; }
    }

    public class GatepassPrintModal
    {
        public int BranchId { get; set; }

        public int CompanyId { get; set; }

        public int GatepassId { get; set; }

        public int UserId { get; set; }
    }

    public class InvoiceLstDltModel
    {
        public long InvId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string InvNo { get; set; }
        public DateTime InvCreatedDate { get; set; }
        public string StockistName { get; set; }
        public Double InvAmount { get; set; }
        public string InvStatus { get; set; }
        public string CityName { get; set; }
        public int IsStockTransfer { get; set; }
        public string StockistNo { get; set; }
    }
       
    }

