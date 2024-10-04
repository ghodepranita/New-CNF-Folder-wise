using CNF.Business.BusinessConstant;
using CNF.Business.Model.ChequeAccounting;
using CNF.Business.Model.Context;
using CNF.Business.Model.StockTransfer;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CNF.API.Controllers
{
    public class StockTransferController : BaseApiController
    {
        #region Add Stock Transfer
        [HttpPost]
        [Route("StockTransfer/AddStockTransfer")]
        public int AddStockTransfer([FromBody]AddStockTransferModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.StockTransferRepository.AddStockTransfer(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddStockTransfer", "Add Stock Transfer - BranchId:  " + model.BranchId + " CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Stock Transfer List
        [HttpGet]
        [Route("StockTransfer/GetStockTransferList/{BranchId}/{CompId}")]
        public List<GetStockTransferListModel> GetStockTransferList(int BranchId, int CompId)
        {
            List<GetStockTransferListModel> modelList = new List<GetStockTransferListModel>();
            try
            {
                modelList = _unitOfWork.StockTransferRepository.GetStockTransferList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockTransferList", "Get Stock Transfer List - BranchId:  " + BranchId + " CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Assign Transport Mode For Stock Transfer
        [HttpPost]
        [Route("StockTransfer/AssignTransportModeForStkTrnfer")]
        public int AssignTransportModeForStkTrnfer([FromBody]AssignTransportModeStkTrnsfr model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.StockTransferRepository.AssignTransportModeForStkTrnfer(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AssignTransportModeForStkTrnfer", "Assign Transport Mode For Stock Transfer", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Check Stock Transfer InvNo
        [HttpGet]
        [Route("StockTransfer/CheckStockTransferInvNo/{BranchId}/{CompId}/{InvNo}")]
        public CheckStockTransferInvNoModel CheckStockTransferInvNo(int BranchId, int CompId, string InvNo)
        {
            CheckStockTransferInvNoModel modelList = new CheckStockTransferInvNoModel();
            try
            {
                modelList = _unitOfWork.StockTransferRepository.CheckStockTransferInvNo(BranchId, CompId, InvNo);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CheckStockTransferInvNo", "Check Stock Transfer  - BranchId:  " + BranchId + " CompId:  " + CompId + " InvNo:  " + InvNo, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Stock transfer dashbord count 
        [HttpPost]
        [Route("StockTransfer/GetStockTransferDashbordCount")]
        public StockTranDashbordCountModel GetStockTransferDashbordCount(DashBoardCommonModel model)
        {
            StockTranDashbordCountModel stocktr = new StockTranDashbordCountModel();
            try
            {
                stocktr = _unitOfWork.StockTransferRepository.GetStockTransferDashbordCount(model.BranchId, model.CompanyId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AllInvoiceCounts", "Get Invoice All Summary Counts" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompanyId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return stocktr;
        }


        #endregion

        #region Get Stock Transfer dashboard Filtered List
        [HttpGet]
        [Route("StockTransfer/GetStockTransferdashboardFilteredList/{BranchId}/{CompId}")]
        public List<InvoicendPickLstModel> GetStockTransferdashboardFilteredList(int BranchId, int CompId)
        {
            List<InvoicendPickLstModel> modelList = new List<InvoicendPickLstModel>();
            try
            {
                modelList = _unitOfWork.StockTransferRepository.GetStockTransferdashboardFilteredList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockTransferdashboardFilteredList", "Get StockTransfer dashboard FilteredList:  " + BranchId + " CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Stock Transfer Filtered List
        [HttpGet]
        [Route("StockTransfer/GetStockTransferFilteredList/{BranchId}/{CompId}")]
        public List<InvoicendPickLstModel> GetStockTransferFilteredList(int BranchId, int CompId)
        {
            List<InvoicendPickLstModel> modelList = new List<InvoicendPickLstModel>();
            try
            {
                modelList = _unitOfWork.StockTransferRepository.GetStockTransferFilteredList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockTransferFilteredList", "Get Stock Transfer Filtered List:  " + BranchId + " CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Stock Transfer Summary Count List
        [HttpGet]
        [Route("StockTransfer/GetStkTrnsferSummaryCount/{BranchId}/{CompId}")]
        public List<StkTransferSmmryCnt> GetStkTrnsferSummaryCount(int BranchId, int CompId)
        {
            List<StkTransferSmmryCnt> OrderDispLst = new List<StkTransferSmmryCnt>();
            try
            {
                OrderDispLst = _unitOfWork.StockTransferRepository.GetStkTrnsferSummaryCount(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStkTrnsferSummaryCount", "Get Stk Trnsfer Summary Count", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OrderDispLst;
        }
        #endregion

        #region Get Owner Stk Trnsfr Dash Inv Smmry List
        [HttpGet]
        [Route("StockTransfer/GetOwnerStkTrnsfrDashInvSmmryList")]
        public List<OwnStkTrnferInvSmmryList> GetOwnerStkTrnsfrDashInvSmmryList()
        {
            List<OwnStkTrnferInvSmmryList> modelList = new List<OwnStkTrnferInvSmmryList>();
            try
            {
                modelList = _unitOfWork.StockTransferRepository.GetOwnerStkTrnsfrDashInvSmmryList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerStkTrnsfrDashInvSmmryList", "Get Owner Stk Trnsfr Dash Inv Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Owner Stk Trnsfr Dash Boxes Smmry List
        [HttpGet]
        [Route("StockTransfer/GetOwnerStkTrnsfrDashBoxesSmmryList")]
        public List<OwnStkTrnferBoxesSmmryList> GetOwnerStkTrnsfrDashBoxesSmmryList()
        {
            List<OwnStkTrnferBoxesSmmryList> modelList = new List<OwnStkTrnferBoxesSmmryList>();
            try
            {
                modelList = _unitOfWork.StockTransferRepository.GetOwnerStkTrnsfrDashBoxesSmmryList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerStkTrnsfrDashBoxesSmmryList", "Get Owner Stk Trnsfr Dash Boxes Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Stk LR Details List For Dash New
        [HttpGet]
        [Route("StockTransfer/GetStkLRDetailsListForDashNew/{BranchId}/{CompId}")]
        public List<StkLRDetailsListForDash> GetLRDataList(int BranchId,int CompId)
        {
            List<StkLRDetailsListForDash> LRLst = new List<StkLRDetailsListForDash>();
            try
            {
                LRLst = _unitOfWork.StockTransferRepository.GetStkLRDetailsListForDashNew(BranchId,CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStkLRDetailsListForDashNew", "Get Stk LR Details List For Dash New", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return LRLst;
        }
        #endregion
    }
}
