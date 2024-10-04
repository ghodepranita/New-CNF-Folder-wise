using CNF.Business.Model.StockTransfer;
using System;
using System.Collections.Generic;

namespace CNF.Business.Repositories.Repository
{
    public interface IStockTransferRepository
    {
        int AddStockTransfer(AddStockTransferModel model);
        List<GetStockTransferListModel> GetStockTransferList(int BranchId, int CompId);
        int AssignTransportModeForStkTrnfer(AssignTransportModeStkTrnsfr model);
        CheckStockTransferInvNoModel CheckStockTransferInvNo(int BranchId, int CompId, string InvNo);
        StockTranDashbordCountModel GetStockTransferDashbordCount(int BranchId, int CompId);
        List<InvoicendPickLstModel> GetStockTransferdashboardFilteredList(int BranchId, int CompId);
        List<InvoicendPickLstModel> GetStockTransferFilteredList(int BranchId, int CompId);
        List<StkTransferSmmryCnt> GetStkTrnsferSummaryCount(int BranchId, int CompId);
        List<OwnStkTrnferInvSmmryList> GetOwnerStkTrnsfrDashInvSmmryList();
        List<OwnStkTrnferBoxesSmmryList> GetOwnerStkTrnsfrDashBoxesSmmryList();
        List<StkLRDetailsListForDash> GetStkLRDetailsListForDashNew(int BranchId, int CompId);
    }
}
