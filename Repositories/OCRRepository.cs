using CNF.Business.BusinessConstant;
using CNF.Business.Model.Context;
using CNF.Business.Model.Master;
using CNF.Business.Model.OCR;
using CNF.Business.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories
{
    public class OCRRepository : IOCRRepository
    {
        private CFADBEntities _contextManager;
        public OCRRepository (CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }

        #region Get Product Master List 
        public List<ProductDataListModel> GetProductDataList(int BranchId, int CompId)
        {
            return GetProductDataListPvt(BranchId, CompId);
        }
        private List<ProductDataListModel> GetProductDataListPvt(int BranchId, int CompId)
        {
            List<ProductDataListModel> ProductDataLst = new List<ProductDataListModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    ProductDataLst = _contextManager.usp_GetProductDataList(BranchId, CompId)
                        .Select(c => new ProductDataListModel
                        {
                            BranchId = Convert.ToInt32(c.BranchId),
                            CompId = Convert.ToInt32(c.CompId),
                            Division = Convert.ToInt32(c.Division),
                            BatchNo = c.BatchNo,
                            ProductName = c.ProductName,
                            Code = c.code,
                            EXP_Date = Convert.ToDateTime(c.EXP_Date).ToString("yyyy/MM/dd"),
                            Addedby = Convert.ToInt32(c.Addedby)
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetProductDataListPvt", "Get Product Data List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ProductDataLst;
        }
        #endregion

        #region Get Product Details By Batch No
        public ProductBatchModel GetProductDetailsByBatchNo(string BatchNo, int BranchId, int CompId, int AddedBy)
        {
            return GetProductDetailsByBatchNoPvt(BatchNo, BranchId, CompId, AddedBy);
        }
        private ProductBatchModel GetProductDetailsByBatchNoPvt(string BatchNo, int BranchId, int CompId, int AddedBy)
        {
            ProductBatchModel productModel = new ProductBatchModel();
            try
            {
                using (CFADBEntities ContextManager = new CFADBEntities())
                {
                    productModel = ContextManager.usp_GetProductDetailsByBatchNo(BranchId, CompId, BatchNo, AddedBy).Select(x => new ProductBatchModel()
                    {
                        ProductName = x.ProductName,
                        EXP_Date = Convert.ToDateTime(x.EXP_Date),
                        Division = Convert.ToInt32(x.Division),
                        Code = Convert.ToString(x.Code)
                    }).FirstOrDefault();
                    if (productModel == null)
                    {
                        var saveBatchNo = ContextManager.usp_SaveNotFoundBatchNo(BranchId, CompId, BatchNo, AddedBy);
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetProductDetailsByBatchNoPvt", "Get Product Details By Batch No Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return productModel;
        }
        #endregion

        #region Save OCR Text Data
        public int SaveOCRTextData(OCRModel model)
        {
            return SaveOCRTextDataPvt(model);
        }
        private int SaveOCRTextDataPvt(OCRModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetVal", typeof(int));
            try
            {
                RetValue = _contextManager.usp_SaveOCRTextData(model.pkId, model.BranchId, model.CompId, model.StockistId, model.LR_ClaimNo,model.LREntryId,model.ClaimAmount,model.TotalLineOfItem, model.BatchNo, model.Quantity, model.Unit, model.Code, model.ProductName, model.ReturnType, model.Division, model.Plant, model.MFG_Date, model.EXP_Date, model.MRP_Price,model.Action,obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveOCRTextDataPvt", "Save OCR Text Data Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        //#region Get OCR Text Data
        //public List<OCRModel> GetOCRTextData(int BranchId, int CompId)
        //{
        //   return GetOCRTextDataPvt(BranchId, CompId);
        //}
        //private List<OCRModel> GetOCRTextDataPvt(int BranchId, int CompId)
        //{
        //    List<OCRModel> ocrList = new List<OCRModel>();
        //    try
        //    {
        //        using (CFADBEntities ContextManager = new CFADBEntities())
        //        {
        //            ocrList= ContextManager.usp_GetOCRTextData(BranchId, CompId).Select(x => new OCRModel()
        //            {
        //                pkId = x.pkId,
        //                BranchId = Convert.ToInt32(x.BranchId),
        //                CompId = Convert.ToInt32(x.CompId),
        //                StockistId = Convert.ToInt32(x.StockistId),
        //                StockistNo = x.StockistNo,
        //                StockistName = x.StockistName,
        //                BatchNo = x.BatchNo,
        //                LR_ClaimNo = x.LR_ClaimNo,
        //                Quantity = Convert.ToInt32(x.Quantity),
        //                Unit = x.Unit,
        //                Code = x.Code,
        //                ProductName = x.ProductName,
        //                ReturnType = x.ReturnType,
        //                Division = Convert.ToInt32(x.Division),
        //                Plant = Convert.ToInt32(x.Plant),
        //                EXP_Date = x.EXP_Date,
        //                MFG_Date = x.MFG_Date,
        //                MRP_Price = x.MRP_Price                       
        //            }).OrderByDescending(x => x.pkId).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetOCRTextDataPvt", "Get OCR Text Data Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return ocrList;
        //}
        //#endregion

        #region Get OCR Text Data
        public List<OCRModel> GetOCRTextData(int BranchId, int CompId, int StockistId, int LREntryId)
        {
            return GetOCRTextDataPvt(BranchId, CompId, StockistId, LREntryId);
        }
        private List<OCRModel> GetOCRTextDataPvt(int BranchId, int CompId, int StockistId, int LREntryId)
        {
            List<OCRModel> ocrList = new List<OCRModel>();
            //  List<OCRDetailsBysubtable> subtableocr = new List<OCRDetailsBysubtable>();

            try
            {
                using (CFADBEntities ContextManager = new CFADBEntities())
                {
                    ocrList = ContextManager.usp_GetOCRTextData(BranchId, CompId, StockistId, LREntryId).Select(x => new OCRModel()
                    {
                        pkId = x.pkId,
                        BranchId = Convert.ToInt32(x.BranchId),
                        CompId = Convert.ToInt32(x.CompId),
                        StockistId = Convert.ToInt32(x.StockistId),
                        StockistNo = x.StockistNo,
                        StockistName = x.StockistName,
                        BatchNo = x.BatchNo,
                        LREntryId = Convert.ToInt32(x.LREntryId),
                        LR_ClaimNo = x.LR_ClaimNo,
                        Quantity = Convert.ToInt32(x.Quantity),
                        Unit = x.Unit,
                        Code = x.Code,
                        ProductName = x.ProductName,
                        ReturnType = x.ReturnType,
                        Division = Convert.ToInt32(x.Division),
                        Plant = Convert.ToInt32(x.Plant),
                        EXP_Date = x.EXP_Date,
                        MFG_Date = x.MFG_Date,
                        MRP_Price = x.MRP_Price,
                        ClaimAmount = Convert.ToInt32(x.ClaimAmount),
                        TotalLineOfItem = Convert.ToInt32(x.TotalLineOfItem),
                    }).OrderByDescending(x => x.pkId).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOCRTextDataPvt", "GetOCRTextDataPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ocrList;
        }
        #endregion

        #region Save RGB Data
        public int SaveRGBData(RGBColorModel model)
        {
            return SaveRGBDataPvt(model);
        }
        private int SaveRGBDataPvt(RGBColorModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetVal", typeof(int));
            try
            {
                RetValue = _contextManager.usp_SaveRGBColorCode(0,model.R_Color,model.G_Color, model.B_Color, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveOCRTextDataPvt", "Save OCR Text Data Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get LR List
        public List<StockistLrModel> GetLRList(int BranchId, int CompanyId,int StockistId)
        {
            return GetLRListPvt(BranchId, CompanyId, StockistId);
        }
        private List<StockistLrModel> GetLRListPvt(int BranchId, int CompanyId, int StockistId)
        {
            List<StockistLrModel> StockLst = new List<StockistLrModel>();
            try
            {
                StockLst = _contextManager.usp_GetOCRLRListByStockistWise(BranchId, CompanyId, StockistId).Select(c => new StockistLrModel
                {             
                    BranchId = c.BranchId,
                    LREntryId = c.LREntryId,
                    LREntryNo = c.LREntryNo,
                    StockistId = c.StockistId,
                    LREntryDate = Convert.ToDateTime(c.LREntryDate),
                    LRNo = c.LRNo,
                    LRDate = Convert.ToDateTime(c.LRDate),
                    AmountPaid = Convert.ToInt32(c.AmountPaid),
                    GoodsReceived = c.GoodsReceived,
                    ClaimFormAvailable= c.ClaimFormAvailable,
                    GatepassNo = c.GatepassNo,
                    ReceiptDate = Convert.ToDateTime(c.ReceiptDate),
                }).OrderByDescending(x => Convert.ToInt64(x.StockistId)).ToList();

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockiestPvt", "Get Role List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }
        #endregion

        #region Get Stockist List
        public List<StockistLrModel> GetStockistLstForOCR(int BranchId, int CompanyId)
        {
            return GetStockistLstForOCRPvt(BranchId, CompanyId);
        }
        private List<StockistLrModel> GetStockistLstForOCRPvt(int BranchId, int CompanyId)
        {
            List<StockistLrModel> StockLst = new List<StockistLrModel>();
            try
            {
                StockLst = _contextManager.usp_GetStockiestForOCR(BranchId, CompanyId).Select(c => new StockistLrModel
                {
                    StockistId = c.StockistId,
                    StockistNo = c.StockistNo,
                    StockistName = c.StockistName,
                    BranchId = c.BranchId,
                }).OrderByDescending(x => Convert.ToInt64(x.StockistId)).ToList();

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistLstForOCRPvt", "Get StockistLst ForOCR Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }
        #endregion

        #region Get Summary Report List
        public List<RptOCRDataSummaryListModel> GetSummaryReportList(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate, int StockistId)
        {
            return GetSummaryReportListPvt(BranchId, CompId, FromDate, ToDate, StockistId);
        }
        private List<RptOCRDataSummaryListModel> GetSummaryReportListPvt(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate, int StockistId)
        {
            List<RptOCRDataSummaryListModel> SummaryReportList = new List<RptOCRDataSummaryListModel>();
            List<RptOCRDataSummaryTableListModel> RptOCRDataSummaryTable = new List<RptOCRDataSummaryTableListModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    SummaryReportList = _contextManager.usp_RptOCRDataSummary(BranchId, CompId, FromDate, ToDate, StockistId).Select(s => new RptOCRDataSummaryListModel
                    {
                        BranchId = Convert.ToInt32(s.BranchId),
                        CompId = Convert.ToInt32(s.CompId),
                        StockistId = Convert.ToInt32(s.StockistId),
                        StockistNo = Convert.ToString(s.StockistNo),
                        StockistName = Convert.ToString(s.StockistName),
                        LRCnt = Convert.ToInt32(s.LRCnt),
                        BatchCnt = Convert.ToInt32(s.BatchCnt),
                        ProdCnt = Convert.ToInt32(s.ProdCnt),
                        TotQty = Convert.ToInt32(s.TotQty),
                        TotClaimAmt = Convert.ToInt32(s.TotClaimAmt),
                        Expired = Convert.ToInt32(s.Expired),
                        Damage = Convert.ToInt32(s.Damage),
                        Salable = Convert.ToInt32(s.Salable)
                    }).OrderByDescending(s => s.StockistId).ToList();

                    if (SummaryReportList.Count > 0)
                    {
                        for (int i = 0; i < SummaryReportList.Count(); i++)
                        {
                            // StockistId Wise Details Table
                            RptOCRDataSummaryTable = _contextManager.usp_RptOCRDataDetails(SummaryReportList[i].BranchId, SummaryReportList[i].CompId, FromDate, ToDate, SummaryReportList[i].StockistId).Select(sd => new RptOCRDataSummaryTableListModel
                            {
                                pkId = Convert.ToInt32(sd.pkId),
                                BranchId = Convert.ToInt32(sd.BranchId),
                                CompId = Convert.ToInt32(sd.CompId),
                                StockistId = Convert.ToInt32(sd.StockistId),
                                StockistNo = Convert.ToString(sd.StockistNo),
                                StockistName = Convert.ToString(sd.StockistName),
                                LREntryId = Convert.ToInt32(sd.LREntryId),
                                LRNo = Convert.ToString(sd.LRNo),
                                LRDate = Convert.ToDateTime(sd.LRDate),
                                LR_ClaimNo = Convert.ToString(sd.LR_ClaimNo),
                                ClaimAmount = Convert.ToInt32(sd.ClaimAmount),
                                TotalLineOfItem = Convert.ToInt32(sd.TotalLineOfItem),
                                BatchNo = Convert.ToString(sd.BatchNo),
                                Code = Convert.ToString(sd.Code),
                                ProductName = Convert.ToString(sd.ProductName),
                                Quantity = Convert.ToInt32(sd.Quantity),
                                Unit = Convert.ToString(sd.Unit),
                                MFG_Date = Convert.ToString(sd.MFG_Date),
                                EXP_Date = Convert.ToString(sd.EXP_Date),
                                ReturnType = Convert.ToString(sd.ReturnType),
                                MRP_Price = Convert.ToString(sd.MRP_Price),
                                SalesDocNo = Convert.ToString(sd.SalesDocNo),
                                SRSStatus = Convert.ToInt32(sd.SRSStatus),
                                AddedOn = Convert.ToDateTime(sd.AddedOn)
                            }).ToList();
                            SummaryReportList[i].RptOCRDataSummaryTable = RptOCRDataSummaryTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetSummaryReportListPvt", "Get Summary Report List Pvt" + "CompId:  " + CompId + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return SummaryReportList;
        }
        #endregion

        #region Get Details Report List
        public List<OCRDetailsRept> GetDetailsReportList(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate, int StockistId)
        {
            return GetDetailsReportListPvt(BranchId, CompId, FromDate, ToDate, StockistId);
        }
        private List<OCRDetailsRept> GetDetailsReportListPvt(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate, int StockistId)
        {
            List<OCRDetailsRept> SummaryrptList = new List<OCRDetailsRept>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    SummaryrptList = _contextManager.usp_RptOCRDataDetails(BranchId, CompId, FromDate, ToDate, StockistId).Select(c => new OCRDetailsRept
                    {
                        pkId = c.pkId,
                        BranchId = Convert.ToInt32(c.BranchId),
                        CompId = Convert.ToInt32(c.CompId),
                        StockistId = Convert.ToInt32(c.StockistId),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        LREntryId = c.LREntryId,
                        LRNo = c.LRNo,
                        LRDate = c.LRDate,
                        LR_ClaimNo = c.LR_ClaimNo,
                        ClaimAmount = c.ClaimAmount,
                        TotalLineOfItem = c.TotalLineOfItem,
                        BatchNo = c.BatchNo,
                        Code = c.Code,
                        ProductName = c.ProductName,
                        Quantity = c.Quantity,
                        Unit = c.Unit,
                        MFG_Date = c.MFG_Date,
                        EXP_Date = c.EXP_Date,
                        ReturnType = c.ReturnType,
                        MRP_Price = c.MRP_Price,
                        SalesDocNo = c.SalesDocNo,
                        SRSStatus = c.SRSStatus,
                        SalesOrganization = c.SalesOrganization,
                        Division = c.Division,
                        SoldtoPartyId = Convert.ToInt32(c.SoldtoPartyId),
                        PONo = c.PONo,
                        Plant = c.Plant,
                        AddedOn = c.AddedOn
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetDetailsReportListPvt", "Get Details Report List Pvt" + "CompId:  " + CompId + "BranchId:  " + BranchId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return SummaryrptList;
        }
        #endregion

        #region Get OCR Count data
        public OCRCountModel OCrSummaryCounts(int BranchId, int CompId, int StockistId, int LREntryId)
        {
            return OCrSummaryCountsPvt(BranchId, CompId, StockistId, LREntryId);
        }
        private OCRCountModel OCrSummaryCountsPvt(int BranchId, int CompId, int StockistId, int LREntryId)
        {
            OCRCountModel ocrsummryCnts = new OCRCountModel();

            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    ocrsummryCnts = _contextManager.usp_GetOCRTextDataSummary(BranchId, CompId, StockistId, LREntryId).Select(c => new OCRCountModel
                    {
                        StockistId = Convert.ToInt32(c.StockistId),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        LREntryId = Convert.ToInt32(c.LREntryId),
                        LR_ClaimNo = c.LR_ClaimNo,
                        NoOfItems = Convert.ToInt32(c.NoOfItems),
                        ExpiredQty = Convert.ToInt32(c.ExpiredQty),
                        DamageQty = Convert.ToInt32(c.DamageQty),
                        SALABLEQty = Convert.ToInt32(c.SALABLEQty),
                        NoOfBatches = Convert.ToInt32(c.NoOfBatches)
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "OCrSummaryCounts", "OCr Summary Counts", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ocrsummryCnts;
        }
        #endregion

    }
}
