using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Model.StockTransfer
{
    public class AddStockTransferModel
    {
        public int InvId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string StkTransInvNo { get; set; }
        public DateTime InvDate { get; set; }
        public int SendToCNFId { get; set; }
        public int IsStockTransfer { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
    }

    public class GetStockTransferListModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string InvNo { get; set; }
        public string InvCreatedDate { get; set; }
        public int SendToCNFId { get; set; }
        public string CNFName { get; set; }
        public string CNFCode { get; set; }
        public long InvId { get; set; }
    }

    public class AssignTransportModeStkTrnsfr
    {
        public string InvoiceId { get; set; }
        public int TransportModeId { get; set; }
        public int TransporterId { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
        public long AttachedInvId { get; set; }
        public int OCnfCity { get; set; }
    }

    public class CheckStockTransferInvNoModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public string InvNo { get; set; }
        public int Flag { get; set; }
    }

    public class StockTranDashbordCountModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int InvPending { get; set; }
        public int InvToday { get; set; }
        public int ConcernPending { get; set; }
        public int StkrToday { get; set; }
        public int StkrPending { get; set; }
        public int StkGPToday { get; set; }
        public int StkGPPending { get; set; }
        public int StkCummInvCnt { get; set; }
        public int StkCummPLCnt { get; set; }
        public int StkCummBoxCnt { get; set; }
        public int NoOfBoxes { get; set; }
        public int LRPendingStockTrnsfer { get; set; }
        public decimal StkSticerPendingAmt { get; set; }
        public decimal StkGPPendingAmt { get; set; }
        public int StkPLCompVerifyPending { get; set; }
        public int StkBoxForLR { get; set; }
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
        public int TransportModeId { get; set; }
        public string LRDate { get; set; }
        public int OCnfCity { get; set; }
        public string LRNo { get; set; }
        public int LRBox { get; set; }     
    }
    public class StkTransferSmmryCnt
    {
        public DateTime StkInvDate { get; set; }
        public int StkInvCount { get; set; }
        public decimal StkInvAmount { get; set; }
        public long StkNoOfBox { get; set; }
    }

    public class OwnStkTrnferInvSmmryList
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CompId { get; set; }
        public string CompanyName { get; set; }
        public int StkPrioPending { get; set; }
        public int StkStkrPending { get; set; }
        public decimal StkStkrPendingAmt { get; set; }
        public int StkGPPending { get; set; }
        public decimal StkGPPendingAmt { get; set; }
    }

    public class OwnStkTrnferBoxesSmmryList
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CompId { get; set; }
        public string CompanyName { get; set; }
        public int StkInvCount { get; set; }
        public decimal StkInvAmount { get; set; }
        public int StkNoOfBoxes { get; set; }
    }
    public class StkLRDetailsListForDash
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
}
