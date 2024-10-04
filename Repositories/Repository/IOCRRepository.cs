using CNF.Business.Model.Master;
using CNF.Business.Model.OCR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories.Repository
{
    public interface IOCRRepository
    {
        List<ProductDataListModel> GetProductDataList(int BranchId, int CompId);
        ProductBatchModel GetProductDetailsByBatchNo(string BatchNo, int BranchId, int CompId, int AddedBy);
        int SaveOCRTextData(OCRModel model);
        //List<OCRModel> GetOCRTextData(int BranchId, int CompId);
        int SaveRGBData(RGBColorModel model);
        List<OCRModel> GetOCRTextData(int BranchId, int CompId,int StockistId, int LREntryId);
        List<StockistLrModel> GetLRList(int BranchId, int CompanyId,int StockistId);
        List<StockistLrModel> GetStockistLstForOCR(int BranchId, int CompanyId);
        List<RptOCRDataSummaryListModel> GetSummaryReportList(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate, int StockistId);
        List<OCRDetailsRept> GetDetailsReportList(int BranchId, int CompId, DateTime? FromDate, DateTime? ToDate, int StockistId);
        OCRCountModel OCrSummaryCounts(int BranchId, int CompId, int StockistId, int LREntryId);
    }
}
