using CNF.Business.BusinessConstant;
using CNF.Business.Model.Context;
using CNF.Business.Model.StockTransfer;
using CNF.Business.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories
{
    public class StockTransferRepository : IStockTransferRepository
    {
        private CFADBEntities _contextManager;

        //Inject DB Context instance from Unit of Work to avoid unwanted memory allocations.
        public StockTransferRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }

        #region Add Stock Transfer
        public int AddStockTransfer(AddStockTransferModel model)
        {
            return AddStockTransferPvt(model);
        }
        private int AddStockTransferPvt(AddStockTransferModel model)
        {
            int RetValue = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetVal", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    RetValue = _contextManager.usp_AddStockTransfer(model.InvId,model.BranchId, model.CompId, model.StkTransInvNo, model.InvDate, model.SendToCNFId, model.IsStockTransfer, model.Addedby, model.Action, objRetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddStockTransferPvt", "Add Stock Transfer - BranchId:  " + model.BranchId + " CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Stock Transfer List
        public List<GetStockTransferListModel> GetStockTransferList(int BranchId, int CompId)
        {
            return GetStockTransferListPvt(BranchId, CompId);
        }
        private List<GetStockTransferListModel> GetStockTransferListPvt(int BranchId, int CompId)
        {
            List<GetStockTransferListModel> modelList = new List<GetStockTransferListModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    modelList = _contextManager.usp_GetStockTransferList(BranchId, CompId).Select(s => new GetStockTransferListModel
                    {
                        BranchId = Convert.ToInt32(s.BranchId),
                        CompId = Convert.ToInt32(s.CompId),
                        InvNo = s.InvNo,
                        InvCreatedDate = Convert.ToDateTime(s.InvCreatedDate).ToString("yyyy-MM-dd"),
                        SendToCNFId = Convert.ToInt32(s.CNFId),
                        CNFName = s.CNFName,
                        CNFCode = s.CNFCode,
                        InvId = Convert.ToInt64(s.InvId)
                    }).OrderByDescending(c => Convert.ToInt64(c.InvId)).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockTransferListPvt", "Get Stock Transfer List - BranchId:  " + BranchId + " CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Assign Transport Mode For Stock Transfer
        public int AssignTransportModeForStkTrnfer(AssignTransportModeStkTrnsfr model)
        {
            return AssignTransportModeForStkTrnferPvt(model);
        }
        private int AssignTransportModeForStkTrnferPvt(AssignTransportModeStkTrnsfr model)
        {
            int RetValue = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(int));
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    RetValue = _contextManager.usp_AssignTransportModeStkTransfer(model.InvoiceId, model.TransportModeId, model.TransporterId, model.OCnfCity, model.Addedby, model.AttachedInvId, model.Action, objRetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AssignTransportModeForStkTrnferPvt", "Add Stock Transfer", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Check Stock Transfer InvNo
        public CheckStockTransferInvNoModel CheckStockTransferInvNo(int BranchId, int CompId, string InvNo)
        {
            return CheckStockTransferInvNoPvt(BranchId, CompId, InvNo);
        }
        private CheckStockTransferInvNoModel CheckStockTransferInvNoPvt(int BranchId, int CompId, string InvNo)
        {
            CheckStockTransferInvNoModel modelList = new CheckStockTransferInvNoModel();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    modelList = _contextManager.usp_checkStkTransferINVNo(BranchId, CompId, InvNo).Select(sinv => new CheckStockTransferInvNoModel
                    {
                        BranchId = sinv.BranchId,
                        CompId = sinv.CompId,
                        InvNo = sinv.InvNo,
                        Flag = sinv.Flag
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CheckStockTransferInvNoPvt", "Check Stock Transfer  - BranchId:  " + BranchId + " CompId:  " + CompId + " InvNo:  " + InvNo, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Stock Transfer Dashbord Count 
        public StockTranDashbordCountModel GetStockTransferDashbordCount(int BranchId, int CompId)
        {
            return GetStockTransferDashbordCountPvt(BranchId, CompId);
        }
        private StockTranDashbordCountModel GetStockTransferDashbordCountPvt(int BranchId, int CompId)
        {
            StockTranDashbordCountModel stdshCont = new StockTranDashbordCountModel();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    stdshCont = _contextManager.usp_DashbordStockTransferCntNew(BranchId, CompId).Select(c => new StockTranDashbordCountModel
                    {
                        InvPending = Convert.ToInt32(c.InvPending),
                        InvToday = Convert.ToInt32(c.InvToday),
                        ConcernPending = Convert.ToInt32(c.ConcernPending),
                        StkrToday = Convert.ToInt32(c.StkrToday),
                        StkrPending = Convert.ToInt32(c.StkrPending),
                        StkGPToday = Convert.ToInt32(c.GPToday),
                        StkGPPending = Convert.ToInt32(c.GPPending),
                        StkCummInvCnt = Convert.ToInt32(c.StkCummInvCnt),
                        StkCummBoxCnt = Convert.ToInt32(c.StkCummBoxCnt),
                        StkCummPLCnt = Convert.ToInt32(c.StkCummPLCnt),
                        NoOfBoxes = Convert.ToInt32(c.NoOfBoxes),
                        LRPendingStockTrnsfer = Convert.ToInt32(c.LRPending),
                        StkSticerPendingAmt = Convert.ToDecimal(c.StkGPPendingAmt),
                        StkGPPendingAmt = Convert.ToDecimal(c.StkGPPendingAmt),
                        StkPLCompVerifyPending = Convert.ToInt32(c.StkPLCompVerifyPending),
                        StkBoxForLR = Convert.ToInt32(c.BoxForLR),
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockTransferDashbordCountPvt", "Get Stock Transfer Dashbord Count Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return stdshCont;
        }

        #endregion

        #region Get Stock Transfer dashboard Filtered List -- Not In Use
        public List<InvoicendPickLstModel> GetStockTransferdashboardFilteredList(int BranchId, int CompId)
        {
            return GetStockTransferdashboardFilteredListPvt(BranchId, CompId);
        }
        private List<InvoicendPickLstModel> GetStockTransferdashboardFilteredListPvt(int BranchId, int CompId)
        {
            List<InvoicendPickLstModel> modelList = new List<InvoicendPickLstModel>();
            try
            {
                modelList = _contextManager.usp_GetStockTransferdashboardFilteredList(BranchId, CompId).Select(b => new InvoicendPickLstModel
                {
                    InvId = b.InvId,
                    InvNo = b.InvNo,
                    InvCreatedDate = Convert.ToDateTime(b.InvCreatedDate).ToString("yyyy-MM-dd"),          
                    InvAmount = Convert.ToDouble(b.InvAmount),
                    InvStatus = b.InvStatus,
                    PODate = Convert.ToDateTime(b.PODate).ToString("yyyy-MM-dd"),
                    PONo = b.PONo,
                    Addedby = b.Addedby,
                    LastUpdatedOn = Convert.ToDateTime(b.LastUpdatedOn),
                    AddedOn = Convert.ToDateTime(b.AddedOn),
                    PackedDate = Convert.ToDateTime(b.PackedDate).ToString("yyyy-MM-dd"),
                    NoOfBox = Convert.ToInt32(b.NoOfBox),
                    InvWeight = Convert.ToDecimal(b.InvWeight),
                    PackingRemark = b.PackingRemark,
                    ReadyToDispatchBy = Convert.ToInt32(b.ReadyToDispatchBy),
                    ReadyToDispatchDate = Convert.ToDateTime(b.ReadyToDispatchDate),
                    ReadyToDispatchRemark = b.ReadyToDispatchRemark,
                    CancelBy = Convert.ToInt32(b.CancelBy),
                    CancelDate = Convert.ToDateTime(b.CancelDate),
                    TransportModeId =Convert.ToInt32(b.TransportModeId),
                    LRDate = Convert.ToDateTime(b.LRDate).ToString("yyyy-MM-dd"),
                    LRBox = Convert.ToInt32(b.LRBox),
                    LRNo = b.LRNo,
                    OCnfCity = Convert.ToInt32(b.OCnfCity),
                    IsStockTransfer = Convert.ToInt32(b.IsStockTransfer)
                }).OrderByDescending(x => x.InvNo).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockTransferdashboardFilteredListPvt", "GetStockTransferdashboardFilteredListPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Stock Transfer Filtered List
        public List<InvoicendPickLstModel> GetStockTransferFilteredList(int BranchId, int CompId)
        {
            return GetStockTransferFilteredListPvt(BranchId, CompId);
        }
        private List<InvoicendPickLstModel> GetStockTransferFilteredListPvt(int BranchId, int CompId)
        {
            List<InvoicendPickLstModel> modelList = new List<InvoicendPickLstModel>();
            try
            {
                modelList = _contextManager.usp_GetStockTransferFilterListNew(BranchId, CompId).Select(b => new InvoicendPickLstModel
                {
                    InvId = b.InvId,
                    InvNo = b.InvNo,
                    InvCreatedDate = Convert.ToDateTime(b.InvCreatedDate).ToString("yyyy-MM-dd"),
                    InvAmount = Convert.ToDouble(b.InvAmount),
                    InvStatus = b.InvStatus,
                    PODate = Convert.ToDateTime(b.PODate).ToString("yyyy-MM-dd"),
                    PONo = b.PONo,
                    Addedby = b.Addedby,
                    LastUpdatedOn = Convert.ToDateTime(b.LastUpdatedOn),
                    AddedOn = Convert.ToDateTime(b.AddedOn),
                    PackedDate = Convert.ToDateTime(b.PackedDate).ToString("yyyy-MM-dd"),
                    NoOfBox = Convert.ToInt32(b.NoOfBox),
                    InvWeight = Convert.ToDecimal(b.InvWeight),
                    PackingRemark = b.PackingRemark,
                    ReadyToDispatchBy = Convert.ToInt32(b.ReadyToDispatchBy),
                    ReadyToDispatchDate = Convert.ToDateTime(b.ReadyToDispatchDate),
                    ReadyToDispatchRemark = b.ReadyToDispatchRemark,
                    CancelBy = Convert.ToInt32(b.CancelBy),
                    CancelDate = Convert.ToDateTime(b.CancelDate),
                    TransportModeId = Convert.ToInt32(b.TransportModeId),
                    LRDate = Convert.ToDateTime(b.LRDate).ToString("yyyy-MM-dd"),
                    LRBox = Convert.ToInt32(b.LRBox),
                    LRNo = b.LRNo,
                    OCnfCity = Convert.ToInt32(b.OCnfCity),
                    IsStockTransfer = Convert.ToInt32(b.IsStockTransfer),
                    StockistNo = b.StockistNo,
                    StockistName = b.StockistName
                }).OrderByDescending(x => x.InvNo).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockTransferFilteredListPvt", "GetStockTransferFilteredListPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Stock Transfer Summary Count List
        public List<StkTransferSmmryCnt> GetStkTrnsferSummaryCount(int BranchId, int CompId)
        {
            return GetStkTrnsferSummaryCountPvt(BranchId, CompId);
        }
        private List<StkTransferSmmryCnt> GetStkTrnsferSummaryCountPvt(int BranchId, int CompId)
        {
            List<StkTransferSmmryCnt> OrderDispSmmryLst = new List<StkTransferSmmryCnt>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    OrderDispSmmryLst = _contextManager.usp_DashStkTrnsferSmmryCnt(BranchId, CompId).Select(c => new StkTransferSmmryCnt
                    {
                        StkInvDate = Convert.ToDateTime(c.StkInvDate),
                        StkInvCount = Convert.ToInt32(c.StkInvCount),
                        StkInvAmount = Convert.ToInt32(c.StkInvAmount),
                        StkNoOfBox = Convert.ToInt32(c.StkNoOfBox)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStkTrnsferSummaryCountPvt", "Get Stk Trnsfer Summary CountPvt ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OrderDispSmmryLst;
        }
        #endregion

        #region Get Owner Stk Trnsfr Dash Inv Smmry List
        public List<OwnStkTrnferInvSmmryList> GetOwnerStkTrnsfrDashInvSmmryList()
        {
            return GetOwnerStkTrnsfrDashInvSmmryListPvt();
        }
        private List<OwnStkTrnferInvSmmryList> GetOwnerStkTrnsfrDashInvSmmryListPvt()
        {
            List<OwnStkTrnferInvSmmryList> smmrylist = new List<OwnStkTrnferInvSmmryList>();
            try
            {
                using (CFADBEntities _contexetManager = new CFADBEntities())
                {
                    smmrylist = _contextManager.usp_OwnerStkTrnsfrDashInvSmmryList().Select(c => new OwnStkTrnferInvSmmryList
                    {
                        BranchId = c.BranchId,
                        BranchName = c.BranchName,
                        CompId = c.CompId,
                        CompanyName = c.CompanyName,
                        StkPrioPending = c.StkPrioPending,
                        StkStkrPending = c.StkrPending,
                        StkStkrPendingAmt = Convert.ToDecimal(c.StkrPendingAmt),
                        StkGPPending = c.StkGPPending,
                        StkGPPendingAmt = Convert.ToDecimal(c.StkGPPendingAmt)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerStkTrnsfrDashInvSmmryListPvt", "Get Owner Stk Trnsfr Dash Inv Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return smmrylist;
        }
        #endregion

        #region Get Owner Stk Trnsfr Dash Boxes Smmry List
        public List<OwnStkTrnferBoxesSmmryList> GetOwnerStkTrnsfrDashBoxesSmmryList()
        {
            return GetOwnerStkTrnsfrDashBoxesSmmryListPvt();
        }
        private List<OwnStkTrnferBoxesSmmryList> GetOwnerStkTrnsfrDashBoxesSmmryListPvt()
        {
            List<OwnStkTrnferBoxesSmmryList> smmrylist = new List<OwnStkTrnferBoxesSmmryList>();
            try
            {
                using (CFADBEntities _contexetManager = new CFADBEntities())
                {
                    smmrylist = _contextManager.usp_OwnerStkTrnsfrDashBoxesSmmryList().Select(c => new OwnStkTrnferBoxesSmmryList
                    {
                        BranchId = Convert.ToInt32(c.BranchId),
                        BranchName = c.BranchName,
                        CompId = Convert.ToInt32(c.CompId),
                        CompanyName = c.CompanyName,
                        StkInvCount = Convert.ToInt32(c.InvCount),
                        StkInvAmount = Convert.ToDecimal(c.InvAmount),
                        StkNoOfBoxes = Convert.ToInt32(c.NoOfBox)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerStkTrnsfrDashBoxesSmmryListPvt", "Get Owner Stk Trnsfr Dash Boxes Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return smmrylist;
        }
        #endregion

        #region Get Stk LR Details List For Dash New
        public List<StkLRDetailsListForDash> GetStkLRDetailsListForDashNew(int BranchId, int CompId)
        {
            return GetStkLRDetailsListForDashNewPvt(BranchId, CompId);
        }
        private List<StkLRDetailsListForDash> GetStkLRDetailsListForDashNewPvt(int BranchId, int CompId)
        {
            List<StkLRDetailsListForDash> LRDataLst = new List<StkLRDetailsListForDash>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    LRDataLst = _contextManager.usp_GetStkLRDetailsListForDashNew(BranchId, CompId).Select(c => new StkLRDetailsListForDash
                    {
                        InvId = c.InvId,
                        InvNo = c.InvNo,
                        StokistId = c.StokistId,
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        LRNo = c.LRNo,
                        LRDate = Convert.ToDateTime(c.LRDate),
                        LRBox = Convert.ToInt32(c.LRBox),
                        LRWeightInKG = Convert.ToDecimal(c.LRWeightInKG),
                        NoOfBox = Convert.ToInt32(c.NoOfBox),
                        OnPriority = Convert.ToInt32(c.OnPriority),
                        IsStockTransfer = Convert.ToInt32(c.IsStockTransfer)
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStkLRDetailsListForDashNewPvt", "Get Stk LR Details List For Dash New", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return LRDataLst;
        }
        #endregion
    }

}
