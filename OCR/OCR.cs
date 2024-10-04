using System;
using System.Collections.Generic;

namespace CNF.Business.Model.OCR
{
    public class ImportReturnModel
    {
        public string RetResult { get; set; }
    }

    // Import Product Data
    public class ImportProductDataModel
    {
        public long pkId { get; set; }
        public int Division { get; set; }
        public string BatchNo { get; set; }
        public string ProductName { get; set; }
        public string Code { get; set; }
        public string EXP_Date { get; set; }
    }

    //Get List For Product Data
    public class ProductDataListModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int Division { get; set; }
        public string BatchNo { get; set; }
        public string ProductName { get; set; }
        public string Code { get; set; }
        public string EXP_Date { get; set; }
        public int Addedby { get; set; }
    }
    public class OCRTextModel
    {
        public string ImageName { get; set; }
        public string DetectedText { get; set; }

    }
    public class OCRModel
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string BatchNo { get; set; }
        public string LR_ClaimNo { get; set; }
        public int Quantity { get; set; }
        public int ClaimAmount { get; set; }
        public int LREntryId { get; set; }
        public int TotalLineOfItem { get; set; }
        public string Unit { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string ReturnType { get; set; }
        public int Division { get; set; }
        public int Plant { get; set; }
        public string MFG_Date { get; set; }
        public string EXP_Date { get; set; }
        public string MRP_Price { get; set; }
        public string ImageName { get; set; }
        public string DetectedText { get; set; }
        public string Action { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public string LRNo { get; set; }
        public string SalesDocNo { get; set; }
        public string SRSStatus { get; set; }
        public DateTime LRDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<OCRDetailsBysubtable> subtableocr { get; set; }
    }

    public class OCRDetailsBysubtable
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string BatchNo { get; set; }
        public string LR_ClaimNo { get; set; }
        public int Quantity { get; set; }
        public int ClaimAmount { get; set; }
        public int TotalLineOfItem { get; set; }
        public string Unit { get; set; }
        public string Code { get; set; }
        public int LREntryId { get; set; }
        public string ProductName { get; set; }
        public string ReturnType { get; set; }
        public int Division { get; set; }
        public int Plant { get; set; }
        public string MFG_Date { get; set; }
        public string EXP_Date { get; set; }
        public string MRP_Price { get; set; }
        public string ImageName { get; set; }
        public string DetectedText { get; set; }
        public string Action { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
    }
    public class ProductBatchModel
    {
        public string BatchNo { get; set; }
        public string ProductName { get; set; }
        public DateTime EXP_Date { get; set; }
        public int Division { get; set; }
        public string Code { get; set; }
    }
    public class OCRDetectedText
    {
        //public string locale { get; set; }
        public string description { get; set; }
    }

  public class RGBColorModel{
        public int R_Color { get; set; }
        public int G_Color { get; set; }
        public int B_Color { get; set; }
    }

    public class StockistLrModel
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
        public int LREntryId { get; set; }
        public string LREntryNo { get; set; }
        public DateTime LREntryDate { get; set; }
        public string LRNo { get; set; }
        public DateTime LRDate { get; set; }
        public int AmountPaid { get; set; }
        public string CityName { get; set; }
        public Boolean GoodsReceived { get; set; }
        public int ClaimFormAvailable { get; set; }
        public string GatepassNo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public Boolean RecvdAtOP { get; set; }
        public DateTime RecvdAtOPDate { get; set; }
        public DateTime ConcernDate { get; set; }
        public DateTime ResolveConcernDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }      
    }

    public class RptOCRDataSummaryListModel
    {
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int StockistId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public int LRCnt { get; set; }
        public int BatchCnt { get; set; }
        public int ProdCnt { get; set; }
        public int TotQty { get; set; }
        public int TotClaimAmt { get; set; }
        public int Expired { get; set; }
        public int Damage { get; set; }
        public int Salable { get; set; }
        public List<RptOCRDataSummaryTableListModel> RptOCRDataSummaryTable { get; set; }
    }

    public class RptOCRDataSummaryTableListModel
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public int LREntryId { get; set; }
        public string LRNo { get; set; }
        public DateTime LRDate { get; set; }
        public string LR_ClaimNo { get; set; }
        public int ClaimAmount { get; set; }
        public int TotalLineOfItem { get; set; }
        public string BatchNo { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string MFG_Date { get; set; }
        public string EXP_Date { get; set; }
        public string ReturnType { get; set; }
        public string MRP_Price { get; set; }
        public string SalesDocNo { get; set; }
        public int SRSStatus { get; set; }
        public DateTime AddedOn { get; set; }
    }

    public class OCRDetailsRept
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public Nullable<int> LREntryId { get; set; }
        public string LRNo { get; set; }
        public Nullable<System.DateTime> LRDate { get; set; }
        public string LR_ClaimNo { get; set; }
        public Nullable<int> ClaimAmount { get; set; }
        public Nullable<int> TotalLineOfItem { get; set; }
        public string BatchNo { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Unit { get; set; }
        public string MFG_Date { get; set; }
        public string EXP_Date { get; set; }
        public string ReturnType { get; set; }
        public string MRP_Price { get; set; }
        public string SalesDocNo { get; set; }
        public Nullable<int> SRSStatus { get; set; }
        public string SalesOrganization { get; set; }
        public Nullable<int> Division { get; set; }
        public Nullable<int> SoldtoPartyId { get; set; }
        public string PONo { get; set; }
        public Nullable<int> Plant { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
    }
    public class OCRCountModel
    {
        public int pkId { get; set; }
        public int BranchId { get; set; }
        public int CompId { get; set; }
        public int StockistId { get; set; }
        public string StockistNo { get; set; }
        public string StockistName { get; set; }
        public string BatchNo { get; set; }
        public string LR_ClaimNo { get; set; }
        public int Quantity { get; set; }
        public int ClaimAmount { get; set; }
        public int LREntryId { get; set; }
        public int NoOfItems { get; set; }
        public int ExpiredQty { get; set; }
        public int DamageQty { get; set; }
        public int SALABLEQty { get; set; }
        public int NoOfBatches { get; set; }
    }


}


