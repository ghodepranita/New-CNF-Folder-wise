using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Model.OrderReturn
{
    public class OrderReturn
    {
        public class InwardGatepassModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public int City { get; set; }
            public string CityName { get; set; }
            public int TransporterId { get; set; }
            public string TransporterName { get; set; }
            public int CourierId { get; set; }
            public string CourierName { get; set; }
            public string OtherTransport { get; set; }
            public string LRNumber { get; set; }
            public DateTime LRDate { get; set; }
            public DateTime ReceiptDate { get; set; }
            public int NoOfBoxes { get; set; }
            public int AmountPaid { get; set; }
            public int IsClaimAvilable { get; set; }
            public int Addedby { get; set; }
            public DateTime AddedOn { get; set; }
            public int LastUpdatedBy { get; set; }
            public DateTime LastUpdatedOn { get; set; }
            public string Action { get; set; }
            public int LREntryId { get; set; }
            public string GatepassNo { get; set; }
            public string TransCourName { get; set; }
            public int Flag { get; set; }
            public DateTime LREntryDate { get; set; }
            public long GoodsReceived { get; set; }
            public string LREntryNo { get; set; }
            public long RecvdAtOP { get; set; }
            public DateTime RecvdAtOPDate { get; set; }
            public int PhyChkId { get; set; }
            public int PhyChkAgeing { get; set; }
            public long GoodNotRecAgeing { get; set; }
            public int ClaimMissingAgeing { get; set; }
            public DateTime CashmemoDate { get; set; }
        }

        public class StokistDtlsModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string Emailid { get; set; }
            public string LRNumber { get; set; }
            public DateTime LRDate { get; set; }
            public DateTime ReceiptDate { get; set; }
            public int Flag { get; set; }
            public int LREntryId { get; set; }
            public string TransporterName { get; set; }
            public string TransporterNo { get; set; }
            public string CourierName { get; set; }
            public string TransCourName { get; set; }
            public int ClaimFormAvailable { get; set; }
        }
        public class InwardGatepassLRDtlsModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string Emailid { get; set; }
            public string LRNo { get; set; }
            public DateTime LRDate { get; set; }
            public DateTime ReceiptDate { get; set; }
            public int Flag { get; set; }
            public int LREntryId { get; set; }
            public string TransporterName { get; set; }
            public string TransporterNo { get; set; }
            public string CourierName { get; set; }
            public string TransCourName { get; set; }
            public int ClaimFormAvailable { get; set; }
            public string ClaimNo { get; set; }
            public DateTime ClaimDate { get; set; }
            public string GatepassNo { get; set; }
            public long GoodsReceived { get; set; }
            public int IsEmailSent { get; set; }
        }
        public class PhysicalCheck1
        {
            public int PhyChkId { get; set; }
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int LREntryId { get; set; }
            public int ReturnCatId { get; set; }
            public string RetCatName { get; set; }
            public string ClaimNo { get; set; }
            public DateTime ClaimDate { get; set; }
            public int AddedBy { get; set; }
            public DateTime AddedOn { get; set; }
            public int ClaimStatus { get; set; }
            public int ConcernId { get; set; }
            public string ConcernText { get; set; }
            public string ConcernRemark { get; set; }
            public DateTime ConcernDate { get; set; }
            public int ConcernBy { get; set; }
            public string ConcernByName { get; set; }
            public int ResolveConcernBy { get; set; }
            public DateTime ResolveConcernDate { get; set; }
            public string ResolveRemark { get; set; }
            public string Action { get; set; }
        }

        public class PhysicalCheck1ListModel
        {
            public int PhyChkId { get; set; }
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int LREntryId { get; set; }
            public string GatepassNo { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string LRNo { get; set; }
            public int ReturnCatId { get; set; }
            public string RetCatName { get; set; }
            public string ClaimNo { get; set; }
            public DateTime ClaimDate { get; set; }
            public int AddedBy { get; set; }
            public DateTime AddedOn { get; set; }
            public int LastUpdatedBy { get; set; }
            public string LastUpdatedOn { get; set; }
            public int ClaimStatus { get; set; }
            public int ConcernId { get; set; }
            public string ConcernText { get; set; }
            public string ConcernRemark { get; set; }
            public DateTime ConcernDate { get; set; }
            public int ConcernBy { get; set; }
            public string ConcernByName { get; set; }
            public int ResolveConcernBy { get; set; }
            public string ResolveConcernByName { get; set; }
            public DateTime ResolveConcernDate { get; set; }
            public string ResolveRemark { get; set; }
            public string Action { get; set; }
            public DateTime ReceiptDate { get; set; }
            public string CityName { get; set; }
            public DateTime LRDate { get; set; }
            public int NoOfBox { get; set; }
            public int AmountPaid { get; set; }
            public int ClaimFormAvailable { get; set; }
        }

        public class AuditorCheckCorrectionModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int SRSId { get; set; }
            public string Action { get; set; }
            public int ActionBy { get; set; }
            public DateTime ActionDate { get; set; }
            public string CorrectionReqRemark { get; set; }
        }

        public class SRSClaimListForVerifyModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public long PhyChkId { get; set; }
            public string ClaimNo { get; set; }
            public DateTime ClaimDate { get; set; }
            public int ClaimStatus { get; set; }
            public DateTime DocDate { get; set; }
            public string BaseUOM { get; set; }
            public string DocStatus { get; set; }
            public string SalesDocNo { get; set; }
            public string SalesDocType { get; set; }
            public int SRSId { get; set; }
            public int SRSStatus { get; set; }
            public string PONoLRNo { get; set; }
            public int SoldtoPartyId { get; set; }
            public int ReturnCatId { get; set; }
            public string ReturnCatName { get; set; }
            public string LRNo { get; set; }
            public int StockistId { get; set; }
            public string StockistName { get; set; }
            public string StockistNo { get; set; }
            public string IsVerified { get; set; }
            public string IsCorrectionReq { get; set; }
            public string CorrectionReqRemark { get; set; }
            public int VerifyCorrectionBy { get; set; }
            public DateTime VerifyCorrectionDate { get; set; }
            public string Netvalue { get; set; }
            public int LRIdGPId { get; set; }
            public int AuditChkAgeing { get; set; }
        }

        public class ImportCNData
        {
            public int pkId { get; set; }
            public string CompanyCode { get; set; }
            public string DistChannel { get; set; }
            public string Division { get; set; }
            public string SalesOrderNo { get; set; }
            public string SalesOrderDate { get; set; }
            public string CrDrNoteNo { get; set; }
            public string CRDRCreationDate { get; set; }
            public decimal CrDrAmt { get; set; }
            public string SoldToCode { get; set; }
            public string SoldToName { get; set; }
            public string SoldToCity { get; set; }
            public string BillingTypeDescription { get; set; }
            public string OrderReason { get; set; }
            public string OrderReasonDescription { get; set; }
            public string LRNo { get; set; }
            public string LRDate { get; set; }
            public string CFAGRDate { get; set; }
            public string MaterialNumber { get; set; }
            public string MaterialDescription { get; set; }
            public string BatchNo { get; set; }
            public decimal BillingQty { get; set; }
            public string ItemCatagoryDescription { get; set; }
            public decimal BasicAmt { get; set; }
            public int AddedBy { get; set; }
            public string AddedOn { get; set; }
            public string RetResult { get; set; }
        }
        public class ImportCNDataList
        {
            public int pkId { get; set; }
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public string CompanyCode { get; set; }
            public string DistChannel { get; set; }
            public string Division { get; set; }
            public string SalesOrderNo { get; set; }
            public string SalesOrderDate { get; set; }
            public string CrDrNoteNo { get; set; }
            public DateTime CRDRCreationDate { get; set; }
            public string CrDrAmt { get; set; }
            public string SoldToCode { get; set; }
            public string SoldToName { get; set; }
            public string SoldToCity { get; set; }
            public string BillingTypeDescription { get; set; }
            public string OrderReason { get; set; }
            public string OrderReasonDescription { get; set; }
            public string LRNo { get; set; }
            public string LRDate { get; set; }
            public DateTime CFAGRDate { get; set; }
            public string MaterialNumber { get; set; }
            public string MaterialDescription { get; set; }
            public string BatchNo { get; set; }
            public string BillingQty { get; set; }
            public string ItemCatagoryDescription { get; set; }
            public int AddedBy { get; set; }
            public string AddedOn { get; set; }
            public string RetResult { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string CityName { get; set; }
            public DateTime LastUpdateDate { get; set; }
            public DateTime ReceiptDate { get; set; }
            public int ReceiptandCNDoneDiff { get; set; }
        }

        public class CNDataForEmail
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public string StockistName { get; set; }
            public string CrDrNoteNo { get; set; }
            public string Emailid { get; set; }
            public string ClaimNo { get; set; }
            public DateTime ClaimDate { get; set; }
            public string StockistNo { get; set; }
        }

        public class FirstPhysicalCheckModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public string GatepassNo { get; set; }
            public string StockistName { get; set; }
            public string LrNo { get; set; }
            public string ReturnOrder { get; set; }
            public string ClaimNo { get; set; }
            public DateTime ClaimDate { get; set; }
        }

        public class AddClaimSRSMappingModel
        {
            public int SRSId { get; set; }
            public int CompId { get; set; }
            public int BranchId { get; set; }
            public string SalesDocNo { get; set; }
            public string SalesDocType { get; set; }
            public Decimal SRSNumber { get; set; }
            public DateTime SRSDate { get; set; }
            public DateTime DocDate { get; set; }
            public string PONoLRNo { get; set; }
            public int SoldtoPartyId { get; set; }
            public string BaseUOM { get; set; }
            public string Netvalue { get; set; }
            public string Division { get; set; }
            public string DocStatus { get; set; }
            public string OrderReason { get; set; }
            public int StockistId { get; set; }
            public string StockistName { get; set; }
            public string StockistNo { get; set; }
            public string CityCode { get; set; }
            public int LRIdGPId { get; set; }
            public string LREntryNo { get; set; }
            public string LRNo { get; set; }
            public DateTime LRDate { get; set; }
            public string ClaimNo { get; set; }
            public DateTime ClaimDate { get; set; }
            public int ReturnCatId { get; set; }
            public int SRSStatus { get; set; }
            public string DelayReason { get; set; }
            public string Plant { get; set; }
            public string SalesOrganization { get; set; }
            public DateTime ReceiptDate { get; set; }
            public int AgingReceiptdate { get; set; }
        }

        public class AddSRSMapping
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public string SRSId { get; set; }
            public int LRIdGPId { get; set; }
            public int AddedBy { get; set; }

        }
        public class GetClaimNoListModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public string ClaimNo { get; set; }
            public DateTime ClaimDate { get; set; }
            public int ClaimStatus { get; set; }
            public int LREntryId { get; set; }
            public string GatepassNo { get; set; }
            public string LRNo { get; set; }
            public int MyProperty { get; set; }
            public int AddedBy { get; set; }
            public int PhyChkId { get; set; }
            public int ReturnCatId { get; set; }
            public DateTime ReceiptDate { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string RetCatName { get; set; }
            public int ConcernId { get; set; }
            public string ConcernText { get; set; }
            public string ConcernRemark { get; set; }
            public DateTime ConcernDate { get; set; }
            public int ConcernBy { get; set; }
            public string ConcernByName { get; set; }
            public int ResolveConcernBy { get; set; }
            public string ResolveConcernByName { get; set; }
            public DateTime ResolveConcernDate { get; set; }
            public string ResolveRemark { get; set; }
            public string CityName { get; set; }
            public DateTime LRDate { get; set; }
            public int NoOfBox { get; set; }
            public int AmountPaid { get; set; }
            public int ClaimFormAvailable { get; set; }
        }

        public class UpdateCNDelayReason
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int SRSId { get; set; }
            public int CNDelayReasonId { get; set; }
            public string CNDelayRemark { get; set; }
            public int AddedBy { get; set; }
        }
        public class ImportReturnSRSModel
        {
            public string RetResult { get; set; }
        }
        public class ImportSRSDataModel
        {
            public long pkId { get; set; }
            public string SalesDocNo { get; set; }
            public int ItemSD { get; set; }
            public string Descr { get; set; }
            public string SalesDocType { get; set; }
            public string DocDate { get; set; }
            public int ConfirmedQty { get; set; }
            public string PONoLRNo { get; set; }
            public string Batch { get; set; }
            public string DelDate { get; set; }
            public string Createdby { get; set; }
            public string Soldtoparty { get; set; }
            public string ExchangeRate { get; set; }
            public int OrderQuantity { get; set; }
            public string Material { get; set; }
            public string BaseUnitofMeasure { get; set; }
            public string Name1 { get; set; }
            public decimal CustExpPrice { get; set; }
            public decimal Netprice { get; set; }
            public decimal PricingUnit { get; set; }
            public string UOM { get; set; }
            public decimal Netvalue { get; set; }
            public decimal NetValue1 { get; set; }
            public string Division { get; set; }
            public string Status { get; set; }
            public string Status1 { get; set; }
            public string SalesOrganization { get; set; }
            public string Salesunit { get; set; }
            public string ShippingPoint { get; set; }
            public string DistributionChannel { get; set; }
            public string GoodsIssueDate { get; set; }
            public string DocumentCurrency { get; set; }
            public string Plant { get; set; }
            public string Orderquantity1 { get; set; }
            public string Description1 { get; set; }
            public string Orderreason { get; set; }
            public string Probability { get; set; }
            public string Soldtoaddress { get; set; }
            public string Pricingdate { get; set; }
            public string Createdon { get; set; }
            public string Time1 { get; set; }
            public string LRNo { get; set; }
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int AddedBy { get; set; }
            public string AddedOn { get; set; }
            public string PONo { get; set; }
            public string ReturnCatCode { get; set; } //
            public string SalesDocDate { get; set; } //

        }

        public class ImportSRSList
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int SRSId { get; set; }
            public string SalesDocNo { get; set; }
            public string DocDate { get; set; }
            public string StockistName { get; set; }
            public string PONoLRNo { get; set; }
            public int SoldtoPartyId { get; set; }
            public string StockistNo { get; set; }
            public string CityCode { get; set; }
            public string CityName { get; set; }
            public string Netvalue { get; set; }
            public string Division { get; set; }
            public string DocStatus { get; set; }
            public string SalesOrganization { get; set; }
            public string DistributionChannel { get; set; }
            public string Plant { get; set; }
            public string Description1 { get; set; }
            public string OrderReason { get; set; }
            public int SRSStatus { get; set; }
            public string BaseUOM { get; set; }
            public string SalesDocType { get; set; }
            public string Createdby { get; set; }
            public DateTime Date { get; set; }

        }
        public class uploadimgreturn
        {
            public string RetResult { get; set; }
        }
        public class DestructionCertificateModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int CNId { get; set; }
            public decimal CNNo { get; set; }
            public DateTime CNDate { get; set; }
            public string InvType { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string CityCode { get; set; }
            public string CityName { get; set; }
            public string DestrCertFile { get; set; }
            public DateTime DestrCertDate { get; set; }
            public string Addedby { get; set; }
            public string Action { get; set; }
            public string CNIdStr { get; set; }

        }

        public class DestCetModel
        {
            public string CNIdStr { get; set; }
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public string DestrCertFile { get; set; }
            public int AddedBy { get; set; }

        }

        public class CreditNoteListModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public long CNId { get; set; }
            public string CrDrNoteNo { get; set; }
            public DateTime CRDRCreationDate { get; set; }
            public float CrDrAmt { get; set; }
            public int StockiestId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string CityCode { get; set; }
            public string CityName { get; set; }
            public string SalesOrderNo { get; set; }
            public DateTime SalesOrderDate { get; set; }
            public string OrderReason { get; set; }
            public string LRNo { get; set; }
            public DateTime LRDate { get; set; }
            public string CompanyCode { get; set; }
            public string DistChannel { get; set; }
            public string Division { get; set; }
            public string MaterialNumber { get; set; }
            public string BatchNo { get; set; }
            public float BillingQty { get; set; }
            public string DestrCertFile { get; set; }
        }
        public class GetLRReceivedOpModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string LRNo { get; set; }
            public DateTime LRDate { get; set; }
            public string LRDatenewFormat { get; set; }
            public int AmountPaid { get; set; }
            public string CityName { get; set; }
            public int TransporterId { get; set; }
            public string TransporterName { get; set; }
            public string TransporterNo { get; set; }
            public int LREntryId { get; set; }
            public string LREntryNo { get; set; }
            public DateTime LREntryDate { get; set; }
            public string LREntryDatenewFormat { get; set; }
            public bool GoodsReceived { get; set; }
            public int ClaimFormAvailable { get; set; }
            public string GatepassNo { get; set; }
            public string ReceiptDate { get; set; }
            public string CourierName { get; set; }
            public string OtherTrasport { get; set; }
            public string TransCourName { get; set; }
            public bool RecvdAtOP { get; set; }
            public string RecvdAtOPDate { get; set; }
            public string ConcernDate { get; set; }
            public string ResolveConcernDate { get; set; }
            public int SRSId { get; set; }
            public int SRSStatus { get; set; }
            public string ClaimNo { get; set; }
            public int ClaimStatus { get; set; }
            public DateTime ClaimDate { get; set; }
            public string ClaimDateFormat { get; set; }
            public DateTime SalesOrderDate { get; set; }
            public string SalesOrderDateNewFormat { get; set; }
            public long CNId { get; set; }
            public string IsVerified { get; set; }
            public string LREntryDatenewDayDiff { get; set; }
            public string AddedOnFormat { get; set; }
            public string AddedOnFormatDayDiff { get; set; }
            public string SalesDocNo { get; set; }
            public string CrDrNoteNo { get; set; }
            public string CRDRCreationDate { get; set; }
            public int ReturnCatId { get; set; }
            public string LREntryDateFormat { get; set; }
            public int SalebleCN1 { get; set; }
            public int SalebleCN2 { get; set; }
            public int SalebleMore2 { get; set; }
            public int ExpCN15D { get; set; }
            public int ExpCN30D { get; set; }
            public int ExpCN45D { get; set; }
            public int ExpCNMore45D { get; set; }
            public DateTime DestrCertDate { get; set; }
            public string DestrCertFile { get; set; }
            public int DestrCertAddedBy { get; set; }
            public int RecvdAtOPForNob { get; set; }
            public int SaleblePendCN { get; set; }
            public int SRSAgeingDays { get; set; }
        }

        public class UpdateInwrdGtpassRecvedModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public string LRNo { get; set; }
            public int AddedBy { get; set; }
            public string AddedOn { get; set; }
            public string LREntryId { get; set; }
            public long RecvdAtOPFlag { get; set; }

        }
        public class GetLrMisMatchListModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int LREntryId { get; set; }
            public string LREntryNo { get; set; }
            public DateTime LREntryDate { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string LRNo { get; set; }
            public DateTime LRDate { get; set; }
            public int AmountPaid { get; set; }
            public string CityName { get; set; }
            public int TransporterId { get; set; }
            public string TransporterName { get; set; }
            public string TransporterNo { get; set; }
            public int CourierId { get; set; }
            public string CourierName { get; set; }
            public string TransCourName { get; set; }
            public string OtherTrasport { get; set; }
            public double GoodsReceived { get; set; }
            public int ClaimFormAvailable { get; set; }
            public string GatepassNo { get; set; }
            public DateTime ReceiptDate { get; set; }
            public double RecvdAtOP { get; set; }
            public DateTime RecvdAtOPDate { get; set; }
            public int SRSId { get; set; }
            public string SalesDocNo { get; set; }
        }

        public class GetLRMisMatchcountModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public DateTime LREntryDate { get; set; }
            public int SRSId { get; set; }
            public int TodayTotalLR { get; set; }
            public int TodayReceivedLR { get; set; }
            public int TodayImportedLR { get; set; }
            public int NotFoundLR { get; set; }
            public int TodayVerifiedCount { get; set; }
            public int CorrReqCount { get; set; }
        }

        public class LRPageCounts
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int LREntryId { get; set; }
            public int TodayLRGP { get; set; }
            public int ConcernCnt { get; set; }
            public int RecvdAtOPCnt { get; set; }
            public int PendingAtExpSCnt { get; set; }
            public int ConcernResolveCnt { get; set; }
        }

        public class GetSRSCNMappingCountsModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int TodaysSRSCnt { get; set; }
            public int TodayCNCnt { get; set; }
            public int PendingForCNCnt { get; set; }
            public int PendingDestrCertCnt { get; set; }

        }

        public class GetORdashbordsupervisorModel
        {
            public int BranchId { get; set; }
            public int CompanyId { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public int ConsignToday { get; set; }
            public int ConsignPending { get; set; }
            public int atWarehouse { get; set; }
            public int atOperator { get; set; }
            public int atAuditorChk { get; set; }
            public int SalebleClaim { get; set; }
            public int DestrPending { get; set; }
            public int SalebleCN1 { get; set; }
            public int SalebleCN2 { get; set; }
            public int SalebleMore2 { get; set; }
            public int SalebleCN1Per { get; set; }
            public int SalebleCN2Per { get; set; }
            public int Salemore2DaysPer { get; set; }
            public int PendingCN { get; set; }
            public int ExpCN15D { get; set; }
            public int ExpCN30D { get; set; }
            public int ExpCN45D { get; set; }
            public int ExpCNMore45D { get; set; }
            public int ExpCN15DPer { get; set; }
            public int ExpCN30DPer { get; set; }
            public int ExpCN45DPer { get; set; }
            public int ExpCNMore45DPer { get; set; }
            public int NonSalelablePen45 { get; set; }
            public int SalelablePen2 { get; set; }
        }

        public class InvoicendPickLstModel
        {
            public long InvId { get; set; }
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public string InvNo { get; set; }
            public string InvCreatedDate { get; set; }
            public bool IsColdStorage { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string SoldTo_City { get; set; }
            public Double InvAmount { get; set; }
            public Nullable<int> InvStatus { get; set; }
            public string StatusText { get; set; }
            public string Addedby { get; set; }
            public DateTime AddedOn { get; set; }
            public DateTime LastUpdatedOn { get; set; }
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
            public string BillDrawnDate { get; set; }
            public int PackedBy { get; set; }
            public string PackedByName { get; set; }
            public string PackedDate { get; set; }
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
            public string PODate { get; set; }
            public string PONo { get; set; }
            public int PicklistStatus { get; set; }
            public int VerifiedBy { get; set; }
            public string ToInv { get; set; }
            public string ReAllotedDate { get; set; }
            public string PicklistDate { get; set; }
            public string PicklistNo { get; set; }
        }


        public class OrderReturnModelNewCount
        {
            public int BranchId { get; set; }
            public int CompanyId { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public int Onedays { get; set; }
            public int twodays { get; set; }
            public int morethentwodays { get; set; }
            public int moresevdays { get; set; }
            public int PendingCN { get; set; }
            public int PendingSRS { get; set; }
            public int FifteenDays { get; set; }
            public int ThirthyDays { get; set; }
            public int fortyfiveDays { get; set; }
            public int abovefortyfiveDays { get; set; }
        }

        public class ExpSupCountmodel
        {
            public long CNCount { get; set; }
            public long PendingLR { get; set; }
        }

        public class ExpirySupervisorDashboardMobModel
        {
            public int LREntryId { get; set; }
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public string LREntryNo { get; set; }
            public DateTime LREntryDate { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public int City { get; set; }
            public string CityName { get; set; }
            public int TransporterId { get; set; }
            public string TransporterName { get; set; }
            public int CourierId { get; set; }
            public string CourierName { get; set; }
            public string OtherTrasport { get; set; }
            public string LRNo { get; set; }
            public DateTime LRDate { get; set; }
            public int NoOfBox { get; set; }
            public int AmountPaid { get; set; }
            public DateTime CashmemoDate { get; set; }
            public int ClaimFormAvailable { get; set; }
            public int GoodsReceived { get; set; }
            public string GatepassNo { get; set; }
            public string ReceiptDate { get; set; }
            public int RecvdAtOP { get; set; }
            public DateTime RecvdAtOPDate { get; set; }
            public string TransCourName { get; set; }
            public int PhyChkId { get; set; }
            public int PhyChkAgeing { get; set; }
            public int GoodNotRecAgeing { get; set; }
            public int ClaimMissingAgeing { get; set; }
            public int ReturnCatId { get; set; }
            public string RetCatName { get; set; }
            public string ClaimNo { get; set; }
            public DateTime ClaimDate { get; set; }
            public int ClaimStatus { get; set; }
            public int ConcernId { get; set; }
            public string ConcernText { get; set; }
            public string ConcernRemark { get; set; }
            public DateTime ConcernDate { get; set; }
            public int ConcernBy { get; set; }
            public string ConcernByName { get; set; }
            public int ResolveConcernBy { get; set; }
            public string ResolveConcernByName { get; set; }
            public DateTime ResolveConcernDate { get; set; }
            public string ResolveRemark { get; set; }
            public int SRSId { get; set; }
            public string IsVerified { get; set; }
            public string IsCorrectionReq { get; set; }
            public string CorrectionReqRemark { get; set; }
            public int VerifyCorrectionBy { get; set; }
            public DateTime VerifyCorrectionDate { get; set; }
            public int AuditChkAgeing { get; set; }
            public string SalesDocNo { get; set; }
        }

        public class OwnORPendConsigDashSmmryList
        {
            public int BranchId { get; set; }
            public string BranchName { get; set; }
            public int CompId { get; set; }
            public string CompanyName { get; set; }
            public int TotalPending { get; set; }
        }
        public class OwnSaleableCNDashSmmryList
        {
            public int BranchId { get; set; }
            public string BranchName { get; set; }
            public int CompId { get; set; }
            public string CompanyName { get; set; }
            public int More2Day { get; set; }
            public int More11Day { get; set; }
        }
        public class GetPendingCNModel
        {
            public int SRSId { get; set; }
            public int LREntryId { get; set; }
            public string LRNo { get; set; }
            public DateTime LREntryDate { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string GatepassNo { get; set; }
            public string ClaimNo { get; set; }
            public long CNId { get; set; }
            public string TransporterName { get; set; }
            public string LREntryDateFormat { get; set; }
            public string SalesDocNo { get; set; }
            public string CrDrNoteNo { get; set; }
        }

        public class GetSaleableModel
        {
            public int SRSId { get; set; }
            public int LREntryId { get; set; }
            public string LRNo { get; set; }
            public DateTime LREntryDate { get; set; }
            public int StockistId { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public string GatepassNo { get; set; }
            public string ClaimNo { get; set; }
            public long CNId { get; set; }
            public string TransporterName { get; set; }
            public string LREntryDateFormat { get; set; }
        }

        public class inwardLREditModel
        {
            public int LREntryId { get; set; }
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public string LRNo { get; set; }
            public int UpdatedBy { get; set; }
        }

        public class InwardGatepassListModel
        {
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int LREntryId { get; set; }
            public int City { get; set; }
            public string CityName { get; set; }
            public string LRNo { get; set; }
            public DateTime LRDate { get; set; }
            public DateTime ReceiptDate { get; set; }
            public int Addedby { get; set; }       
            public DateTime LREntryDate { get; set; }
            public List<srsListModelByLR> srsListByLRNo { get; set; }
            public DateTime RecvdAtOPDate { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
        }
        public class srsListModelByLR
        {
            public int SRSId { get; set; }
            public int BranchId { get; set; }
            public int CompId { get; set; }
            public int LREntryId { get; set; }
            public string LRNo { get; set; }
            public string SalesDocNo { get; set; }
            public string StockistNo { get; set; }
            public string StockistName { get; set; }
            public DateTime AddedOn { get; set; }
        }

    }
}

