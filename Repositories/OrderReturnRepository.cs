using CNF.Business.BusinessConstant;
using CNF.Business.Model.Context;
using CNF.Business.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Linq;
using static CNF.Business.Model.OrderReturn.OrderReturn;

namespace CNF.Business.Repositories
{
    public class OrderReturnRepository : IOrderReturnRepository
    {
        private CFADBEntities _contextManager;

        //Inject DB Context instance from Unit of Work to avoid unwanted memory allocations.
        public OrderReturnRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }

        #region Get New Generated Gatepass No
        public string GetNewGeneratedGatepassNo(int BranchId, int CompId, DateTime ReceiptDate)
        {
            return GetNewGeneratedGatepassNoPvt(BranchId, CompId, ReceiptDate);
        }
        private string GetNewGeneratedGatepassNoPvt(int BranchId, int CompId, DateTime ReceiptDate)
        {
            string gatepassNo = string.Empty;

            try
            {
                gatepassNo = _contextManager.usp_InwardGatepassNewNo(BranchId, CompId, ReceiptDate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetNewGeneratedGatepassNoPvt", "Get New Generated Gatepass No " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return gatepassNo;
        }
        #endregion

        #region Get New Generated LR No.
        public string GetNewGeneratedLRNo(int BranchId, int CompId, DateTime LREntryDate)
        {
            return GetNewGeneratedLRNoPvt(BranchId, CompId, LREntryDate);
        }
        private string GetNewGeneratedLRNoPvt(int BranchId, int CompId, DateTime LREntryDate)
        {
            string newlrentryNo = string.Empty;

            try
            {
                newlrentryNo = _contextManager.usp_LREntryNewNo(BranchId, CompId, LREntryDate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetNewGeneratedGatepassNoPvt", "Get New Generated Gatepass No " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return newlrentryNo;
        }
        #endregion

        #region LR Details Add Edit - for Mobile
        public string AddEditLRDetails(InwardGatepassModel model)
        {
            return AddEditLRDetailsPvt(model);
        }
        private string AddEditLRDetailsPvt(InwardGatepassModel model)
        {
            string RetValue = "";
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                var result = _contextManager.usp_LREntryAddEdit(model.LREntryId, model.BranchId, model.CompId, model.LREntryDate, model.StockistId, model.City, model.TransporterId, model.CourierId, model.OtherTransport, model.LRNumber, model.LRDate, model.NoOfBoxes, model.AmountPaid, model.CashmemoDate, model.IsClaimAvilable, Convert.ToInt32(model.GoodsReceived), model.Addedby, model.Action, obj).ToList();
                var LREntryId = result[0];
                RetValue = Convert.ToString(LREntryId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditLRDetailsPvt", "Add Edit LR Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Generate Inward Gatepass- for Mobile
        public string GenerateInwardGatepass(InwardGatepassModel model)
        {
            return GenerateInwardGatepassPvt(model);
        }
        private string GenerateInwardGatepassPvt(InwardGatepassModel model)
        {
            string RetValue = "";
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                var result = _contextManager.usp_InwardGatepassGenerate(model.LREntryId, model.BranchId, model.CompId, Convert.ToDateTime(model.ReceiptDate), model.Addedby, obj).ToList();
                var LREntryId = result[0];
                RetValue = Convert.ToString(LREntryId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GenerateInwardGatepassPvt", "Generate Inward Gatepass ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Stockiest Details For Email
        public List<StokistDtlsModel> GetStockistDtlsForEmail(int BranchId, int CompId, int LREntryId)
        {
            return GetStockistDtlsForEmailPvt(BranchId, CompId, LREntryId);
        }
        private List<StokistDtlsModel> GetStockistDtlsForEmailPvt(int BranchId, int CompId, int LREntryId)
        {
            List<StokistDtlsModel> stockistDtls = new List<StokistDtlsModel>();
            try
            {
                stockistDtls = _contextManager.usp_GetStockistDtlsForSendEmail(BranchId, CompId, LREntryId).Select(s => new StokistDtlsModel
                {
                    BranchId = s.BranchId,
                    CompId = s.CompId,
                    LRDate = Convert.ToDateTime(s.LRDate),
                    LRNumber = s.LRNo,
                    ReceiptDate = Convert.ToDateTime(s.ReceiptDate),
                    StockistNo = s.StockistNo,
                    StockistName = s.StockistName,
                    Emailid = s.Emailid,
                    TransporterName = s.TransporterName,
                    TransporterNo = s.TransporterNo,
                    CourierName = s.CourierName,
                    TransCourName = s.TransCourName,
                    ClaimFormAvailable = s.ClaimFormAvailable
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistDtlsForEmailPvt", "Get Stockist Dtls For Email", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return stockistDtls;
        }
        #endregion

        #region Get Inward Gatepass List - For Mobile
        public List<InwardGatepassModel> GetInwardGatepassList(int BranchId, int CompId)
        {
            return GetInwardGatepassListPvt(BranchId, CompId);
        }
        private List<InwardGatepassModel> GetInwardGatepassListPvt(int BranchId, int CompId)
        {
            List<InwardGatepassModel> GatepassLst = new List<InwardGatepassModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    GatepassLst = _contextManager.usp_GetInwradGatepassList(BranchId, CompId).Select(c => new InwardGatepassModel
                    {
                        LREntryId = c.LREntryId,
                        GatepassNo = c.GatepassNo,
                        StockistId = c.StockistId,
                        StockistName = c.StockistName,
                        StockistNo = c.StockistNo,
                        City = Convert.ToInt32(c.City),
                        CityName = c.CityName,
                        TransporterId = Convert.ToInt32(c.TransporterId),
                        TransCourName = c.TransCourName,
                        TransporterName = c.TransporterName,
                        CourierId = Convert.ToInt32(c.CourierId),
                        CourierName = c.CourierName,
                        OtherTransport = c.OtherTrasport,
                        LRNumber = c.LRNo,
                        LRDate = Convert.ToDateTime(c.LRDate),
                        ReceiptDate = Convert.ToDateTime(c.ReceiptDate),
                        NoOfBoxes = Convert.ToInt32(c.NoOfBox),
                        AmountPaid = Convert.ToInt32(c.AmountPaid),
                        CashmemoDate = Convert.ToDateTime(c.CashmemoDate),
                        LREntryDate = Convert.ToDateTime(c.LREntryDate),
                        GoodsReceived = Convert.ToInt64(c.GoodsReceived),
                        IsClaimAvilable = c.ClaimFormAvailable,
                        LREntryNo = c.LREntryNo,
                        RecvdAtOP = Convert.ToInt32(c.RecvdAtOP),
                        RecvdAtOPDate = Convert.ToDateTime(c.RecvdAtOPDate),
                        PhyChkId = c.PhyChkId,
                        PhyChkAgeing = Convert.ToInt32(c.PhyChkAgeing),
                        GoodNotRecAgeing = Convert.ToInt32(c.GoodNotRecAgeing),
                        ClaimMissingAgeing = Convert.ToInt32(c.ClaimMissingAgeing)
                    }).OrderByDescending(x => x.AddedOn).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInwardGatepassListPvt", "Get Inward Gatepass List Pvt ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GatepassLst;
        }
        #endregion

        #region Get Inward Gatepass List For Edit - For Mobile
        public List<InwardGatepassModel> GetInwardGatepassListForEdit(int BranchId, int CompId)
        {
            return GetInwardGatepassListForEditPvt(BranchId, CompId);
        }
        private List<InwardGatepassModel> GetInwardGatepassListForEditPvt(int BranchId, int CompId)
        {
            List<InwardGatepassModel> GatepassLst = new List<InwardGatepassModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    GatepassLst = _contextManager.usp_GetInwradGatepassListForEdit(BranchId, CompId).Select(c => new InwardGatepassModel
                    {
                        LREntryId = c.LREntryId,
                        GatepassNo = c.GatepassNo,
                        StockistId = c.StockistId,
                        StockistName = c.StockistName,
                        StockistNo = c.StockistNo,
                        City = Convert.ToInt32(c.City),
                        CityName = c.CityName,
                        TransporterId = Convert.ToInt32(c.TransporterId),
                        TransCourName = c.TransCourName,
                        TransporterName = c.TransporterName,
                        CourierId = Convert.ToInt32(c.CourierId),
                        CourierName = c.CourierName,
                        OtherTransport = c.OtherTrasport,
                        LRNumber = c.LRNo,
                        LRDate = Convert.ToDateTime(c.LRDate),
                        ReceiptDate = Convert.ToDateTime(c.ReceiptDate),
                        NoOfBoxes = Convert.ToInt32(c.NoOfBox),
                        AmountPaid = Convert.ToInt32(c.AmountPaid),
                        CashmemoDate = Convert.ToDateTime(c.CashmemoDate),
                        LREntryDate = Convert.ToDateTime(c.LREntryDate),
                        GoodsReceived = Convert.ToInt64(c.GoodsReceived),
                        IsClaimAvilable = c.ClaimFormAvailable,
                        LREntryNo = c.LREntryNo,
                        RecvdAtOP = Convert.ToInt32(c.RecvdAtOP),
                        RecvdAtOPDate = Convert.ToDateTime(c.RecvdAtOPDate),
                        PhyChkId = c.PhyChkId
                    }).OrderByDescending(x => x.AddedOn).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInwardGatepassListForEditPvt", "Get Inward Gatepass List For Edit Pvt ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GatepassLst;
        }
        #endregion

        #region Edit Inward LR Details - for Mobile
        public int EditInwardLRDetails(inwardLREditModel model)
        {
            return EditInwardLRDetailsPvt(model);
        }
        private int EditInwardLRDetailsPvt(inwardLREditModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_EditInwardLRDetails(model.LREntryId, model.BranchId, model.CompId, model.LRNo, model.UpdatedBy, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "EditInwardLRDetailsPvt", "Edit Inward LR Details Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get LR Dtls List Of Cnsgmnt Recvd Fr Snd Mail 
        public List<InwardGatepassLRDtlsModel> GetLRDtlsLstOfCnsgmntRecvdFrSndMail(int BranchId, int CompId)
        {
            return GetLRDtlsLstOfCnsgmntRecvdFrSndMailPvt(BranchId, CompId);
        }
        private List<InwardGatepassLRDtlsModel> GetLRDtlsLstOfCnsgmntRecvdFrSndMailPvt(int BranchId, int CompId)
        {
            List<InwardGatepassLRDtlsModel> count = new List<InwardGatepassLRDtlsModel>();
            try
            {
                count = _contextManager.usp_GetLRDtlsLstOfCnsgmntRecvdFrSndMail(BranchId, CompId).Select(x => new InwardGatepassLRDtlsModel
                {
                    BranchId = x.BranchId,
                    CompId = x.CompId,
                    LREntryId = x.LREntryId,
                    LRNo = x.LRNo,
                    LRDate = Convert.ToDateTime(x.LRDate),
                    StockistId = x.StockistId,
                    StockistNo = x.StockistNo,
                    StockistName = x.StockistName,
                    Emailid = x.Emailid,
                    ClaimFormAvailable = x.ClaimFormAvailable,
                    GatepassNo = x.GatepassNo,
                    GoodsReceived = Convert.ToInt64(x.GoodsReceived),
                    TransporterNo = x.TransporterNo,
                    TransporterName = x.TransporterName,
                    TransCourName = x.TransCourName,
                    ClaimNo = x.ClaimNo,
                    ClaimDate = Convert.ToDateTime(x.ClaimDate),
                    IsEmailSent = x.IsEmailSent
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRDtlsLstOfCnsgmntRecvdFrSndMailPvt", "GetLRDtlsLstOfCnsgmntRecvdFrSndMailPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return count;
        }
        #endregion

        #region Add Send Email Flag
        public int AddSendEmailFlag(int BranchId, int CompId, int LREntryId, int Flag)
        {
            return AddSendEmailFlagPvt(BranchId, CompId, LREntryId, Flag);
        }
        private int AddSendEmailFlagPvt(int BranchId, int CompId, int LREntryId, int Flag)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_AddSendEmailFlag(BranchId, CompId, LREntryId, Flag, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddSendEmailFlagPvt", "Add Send Email Flag", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Physical check 1 List
        public List<PhysicalCheck1ListModel> GetPhysicalCheck1List(int BranchId, int CompId)
        {
            return GetPhysicalCheck1ListPvt(BranchId, CompId);
        }
        private List<PhysicalCheck1ListModel> GetPhysicalCheck1ListPvt(int BranchId, int CompId)
        {
            List<PhysicalCheck1ListModel> PhysicalCheck1List = new List<PhysicalCheck1ListModel>();
            try
            {
                PhysicalCheck1List = _contextManager.usp_PhysicalCheck1List(BranchId, CompId).Select(p => new PhysicalCheck1ListModel
                {
                    PhyChkId = p.PhyChkId,
                    BranchId = p.BranchId,
                    CompId = p.CompId,
                    LREntryId = Convert.ToInt32(p.LREntryId),
                    GatepassNo = p.GatepassNo,
                    ReceiptDate = Convert.ToDateTime(p.ReceiptDate),
                    StockistId = p.StockistId,
                    StockistNo = p.StockistNo,
                    StockistName = p.StockistName,
                    LRNo = p.LRNo,
                    ReturnCatId = Convert.ToInt32(p.ReturnCatId),
                    RetCatName = p.RetCatName,
                    ClaimNo = p.ClaimNo,
                    ClaimDate = Convert.ToDateTime(p.ClaimDate),
                    ClaimStatus = Convert.ToInt32(p.ClaimStatus),
                    ConcernId = Convert.ToInt32(p.ConcernId),
                    ConcernText = p.ConcernText,
                    ConcernRemark = p.ConcernRemark,
                    ConcernDate = Convert.ToDateTime(p.ConcernDate),
                    ConcernBy = Convert.ToInt32(p.ConcernBy),
                    ConcernByName = p.ConcernByName,
                    ResolveConcernBy = Convert.ToInt32(p.ResolveConcernBy),
                    ResolveConcernByName = p.ResolveConcernByName,
                    ResolveConcernDate = Convert.ToDateTime(p.ResolveConcernDate),
                    ResolveRemark = p.ResolveRemark,
                    CityName = p.CityName,
                    LRDate = Convert.ToDateTime(p.LRDate),
                    NoOfBox = Convert.ToInt32(p.NoOfBox),
                    AmountPaid = Convert.ToInt32(p.AmountPaid),
                    ClaimFormAvailable = p.ClaimFormAvailable
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPhysicalCheck1ListPvt", "Get Physical Check1 List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PhysicalCheck1List;
        }
        #endregion

        #region 1st Physical Check Add Edit
        public int PhysicalCheck1AddEdit(PhysicalCheck1 model)
        {
            return PhysicalCheck1AddEditPvt(model);
        }
        private int PhysicalCheck1AddEditPvt(PhysicalCheck1 model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_PhysicalCheck1AddEdit(model.PhyChkId, model.BranchId, model.CompId, model.LREntryId, model.ReturnCatId, model.ClaimNo, model.ClaimDate, model.AddedBy,
                    model.AddedOn, model.ClaimStatus, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PhysicalCheck1AddEditPvt", "Physical Check Add Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Physical check 1 Concern
        public int PhysicalCheck1Concern(PhysicalCheck1 model)
        {
            return PhysicalCheck1ConcernPvt(model);
        }
        public int PhysicalCheck1ConcernPvt(PhysicalCheck1 model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                if (model.Action == "RAISECONCERN")
                {
                    RetValue = _contextManager.usp_PhysicalCheck1Concern(model.PhyChkId, model.LREntryId, model.ClaimStatus, model.ConcernId,
                        model.ConcernRemark, model.ConcernDate, model.ConcernBy, model.ResolveConcernBy,
                        DateTime.Now, model.ResolveRemark, model.Action, obj);
                }
                else
                {
                    RetValue = _contextManager.usp_PhysicalCheck1Concern(model.PhyChkId, model.LREntryId, model.ClaimStatus, model.ConcernId,
                        model.ConcernRemark, DateTime.Now, model.ConcernBy, model.ResolveConcernBy, model.ResolveConcernDate, model.ResolveRemark, model.Action, obj);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PhysicalCheck1Concern", "Physical Check 1 Concern", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Auditor Check - Verify and Correction List
        public List<SRSClaimListForVerifyModel> GetSRSClaimListForVerifyList(int BranchId, int CompId)
        {
            return GetSRSClaimListForVerifyListPvt(BranchId, CompId);
        }
        private List<SRSClaimListForVerifyModel> GetSRSClaimListForVerifyListPvt(int BranchId, int CompId)
        {
            List<SRSClaimListForVerifyModel> SRSClaimListForVerifyList = new List<SRSClaimListForVerifyModel>();
            try
            {
                SRSClaimListForVerifyList = _contextManager.usp_GetSRSClaimListForVerify(BranchId, CompId).Select(s => new SRSClaimListForVerifyModel
                {
                    BranchId = Convert.ToInt32(s.BranchId),
                    CompId = Convert.ToInt32(s.CompId),
                    ClaimNo = s.ClaimNo,
                    ClaimDate = Convert.ToDateTime(s.ClaimDate),
                    ClaimStatus = Convert.ToInt32(s.ClaimStatus),
                    SRSId = s.SRSId,
                    SRSStatus = Convert.ToInt32(s.SRSStatus),
                    PONoLRNo = s.PONoLRNo,
                    SoldtoPartyId = Convert.ToInt32(s.SoldtoPartyId),
                    ReturnCatId = Convert.ToInt32(s.ReturnCatId),
                    ReturnCatName = s.ReturnCatName,
                    LRNo = s.LRNo,
                    StockistId = Convert.ToInt32(s.StockistId),
                    StockistName = s.StockistName,
                    StockistNo = s.StockistNo,
                    IsVerified = s.IsVerified,
                    IsCorrectionReq = s.IsCorrectionReq,
                    CorrectionReqRemark = s.CorrectionReqRemark,
                    VerifyCorrectionBy = Convert.ToInt32(s.VerifyCorrectionBy),
                    VerifyCorrectionDate = Convert.ToDateTime(s.VerifyCorrectionDate),
                    SalesDocNo = s.SalesDocNo,
                    Netvalue = s.Netvalue,
                    AuditChkAgeing = Convert.ToInt32(s.AuditChkAgeing)
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetSRSClaimListForVerifyList", "Get Auditor Check - Verify and Correction List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return SRSClaimListForVerifyList;
        }
        #endregion

        #region Auditor Check - Verify and Correction Required(Remark)
        public int AuditorCheckCorrection(AuditorCheckCorrectionModel model)
        {
            return AuditorCheckCorrectionPvt(model);
        }
        public int AuditorCheckCorrectionPvt(AuditorCheckCorrectionModel model)
        {
            int RetValue = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(string));
            try
            {
                if (model.Action == "VERIFY")
                {
                    RetValue = _contextManager.usp_AuditorCheckUpdate(model.BranchId, model.CompId, model.SRSId, model.Action, model.ActionBy, model.ActionDate, "", objRetValue);
                }
                else
                {
                    RetValue = _contextManager.usp_AuditorCheckUpdate(model.BranchId, model.CompId, model.SRSId, model.Action, model.ActionBy, model.ActionDate, model.CorrectionReqRemark, objRetValue);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AuditorCheckCorrection", "Auditor Check - Verify and Correction Required(Remark)", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Import CN Details List
        public List<ImportCNDataList> GetImportCNDataList(int BranchId, int CompId)
        {
            return GetImportCNDataListPvt(BranchId, CompId);
        }
        private List<ImportCNDataList> GetImportCNDataListPvt(int BranchId, int CompId)
        {
            List<ImportCNDataList> ImportCNDataList = new List<ImportCNDataList>();
            try
            {
                ImportCNDataList = _contextManager.usp_GetImportCNDataList(BranchId, CompId).Select(c => new ImportCNDataList
                {
                    BranchId = Convert.ToInt32(c.BranchId),
                    CompId = Convert.ToInt32(c.CompId),
                    SalesOrderNo = c.SalesOrderNo,
                    SalesOrderDate = Convert.ToString(c.SalesOrderDate),
                    CrDrNoteNo = c.CrDrNoteNo,
                    CRDRCreationDate = Convert.ToDateTime(c.CRDRCreationDate),
                    CrDrAmt = Convert.ToString(c.CrDrAmt),
                    StockistNo = c.StockistNo,
                    StockistName = c.StockistName,
                    CityName = c.CityName,
                    OrderReason = c.OrderReason,
                    LRNo = c.LRNo,
                    LRDate = Convert.ToString(c.LRDate),
                    CFAGRDate = Convert.ToDateTime(c.CFAGRDate),
                    AddedBy = Convert.ToInt32(c.AddedBy),
                    LastUpdateDate = Convert.ToDateTime(c.LastUpdateDate),
                    ReceiptDate = Convert.ToDateTime(c.ReceiptDate),
                    ReceiptandCNDoneDiff = Convert.ToInt32(c.ReceiptandCNDoneDiff)
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetImportCNDataList", "Get Import CN Data List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ImportCNDataList;
        }
        #endregion

        #region Get Import CN Data For Email
        public List<CNDataForEmail> GetImportCNDataForEmail(int BranchId, int CompId)
        {
            return GetImportCNDataForEmailPvt(BranchId, CompId);
        }
        private List<CNDataForEmail> GetImportCNDataForEmailPvt(int BranchId, int CompId)
        {
            List<CNDataForEmail> ImportCNDataList = new List<CNDataForEmail>();
            try
            {
                ImportCNDataList = _contextManager.usp_ImportCNDetaForEmail(BranchId, CompId).Select(c => new CNDataForEmail
                {
                    BranchId = Convert.ToInt32(c.BranchId),
                    CompId = Convert.ToInt32(c.CompId),
                    CrDrNoteNo = c.CrDrNoteNo,
                    StockistName = c.StockistName,
                    Emailid = c.Emailid,
                    StockistNo = c.StockistNo,
                    ClaimNo = c.ClaimNo,
                    ClaimDate = Convert.ToDateTime(c.ClaimDate)
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetImportCNDataForEmailPvt", "Get CN Data For Email", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ImportCNDataList;
        }
        #endregion

        #region Add Claim - SRS Mapping
        public int ClaimSRSMappingAddEdits(AddSRSMapping model)
        {
            return ClaimSRSMappingAddEditPvt(model);
        }
        private int ClaimSRSMappingAddEditPvt(AddSRSMapping model)
        {

            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_ClaimSRSMapping(model.BranchId, model.CompId, model.LRIdGPId, model.SRSId, model.AddedBy, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ClaimSRSMappingAddEditPvt", "Claim SRS Mapping AddEditPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Claim - SRS Mapping List
        public List<AddClaimSRSMappingModel> GetClaimSRSMappingLists(int BranchId, int CompId, int SRSId)
        {
            return GetClaimSRSMappingListsPvt(BranchId, CompId, SRSId);
        }
        private List<AddClaimSRSMappingModel> GetClaimSRSMappingListsPvt(int BranchId, int CompId, int SRSId)
        {
            List<AddClaimSRSMappingModel> modelList = new List<AddClaimSRSMappingModel>();
            try
            {
                modelList = _contextManager.usp_GetSRSHeaderListForDelayCN(BranchId, CompId, SRSId).Select(i => new AddClaimSRSMappingModel
                {
                    SRSId = i.SRSId,
                    CompId = Convert.ToInt32(i.CompId),
                    BranchId = Convert.ToInt32(i.BranchId),
                    SalesDocNo = i.SalesDocNo,
                    PONoLRNo = i.PONoLRNo,
                    SoldtoPartyId = Convert.ToInt32(i.SoldtoPartyId),
                    Netvalue = i.Netvalue,
                    Division = i.Division,
                    StockistId = Convert.ToInt32(i.StockistId),
                    StockistName = Convert.ToString(i.StockistName),
                    StockistNo = Convert.ToString(i.StockistNo),
                    CityCode = i.CityCode,
                    DelayReason = i.DelayReason,
                    ReceiptDate=Convert.ToDateTime(i.ReceiptDate),
                    AgingReceiptdate = Convert.ToInt32(i.AgingReceiptdate)
                }).OrderBy(x => x.DelayReason).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetClaimSRSMappingListsPvt", "Get Claim SRS Mapping Lists Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get ClaimNo List
        public List<GetClaimNoListModel> GetClaimNoLists(int BranchId, int CompId)
        {
            return GetClaimNoListPvt(BranchId, CompId);
        }
        private List<GetClaimNoListModel> GetClaimNoListPvt(int BranchId, int CompId)
        {
            List<GetClaimNoListModel> modelList = new List<GetClaimNoListModel>();
            try
            {
                modelList = _contextManager.usp_PhysicalCheck1List(BranchId, CompId).Select(b => new GetClaimNoListModel
                {
                    PhyChkId = Convert.ToInt32(b.PhyChkId),
                    BranchId = b.BranchId,
                    CompId = b.CompId,
                    LREntryId = Convert.ToInt32(b.LREntryId),
                    GatepassNo = b.GatepassNo,
                    ReceiptDate = Convert.ToDateTime(b.ReceiptDate),
                    StockistId = b.StockistId,
                    StockistNo = b.StockistNo,
                    StockistName = b.StockistName,
                    RetCatName = b.RetCatName,
                    ReturnCatId = Convert.ToInt32(b.ReturnCatId),
                    ClaimNo = b.ClaimNo,
                    ClaimDate = Convert.ToDateTime(b.ClaimDate),
                    ClaimStatus = Convert.ToInt32(b.ClaimStatus),
                    ConcernId = Convert.ToInt32(b.ConcernId),
                    ConcernText = b.ConcernText,
                    ConcernRemark = b.ConcernRemark,
                    ConcernDate = Convert.ToDateTime(b.ConcernDate),
                    ConcernBy = Convert.ToInt32(b.ConcernBy),
                    ConcernByName = b.ConcernByName,
                    ResolveConcernBy = Convert.ToInt32(b.ResolveConcernBy),
                    ResolveConcernByName = b.ResolveConcernByName,
                    ResolveConcernDate = Convert.ToDateTime(b.ResolveConcernDate),
                    ResolveRemark = Convert.ToString(b.ResolveRemark),
                    CityName = Convert.ToString(b.CityName),
                    LRDate = Convert.ToDateTime(b.LRDate),
                    NoOfBox = Convert.ToInt32(b.NoOfBox),
                    AmountPaid = Convert.ToInt32(b.AmountPaid),
                    ClaimFormAvailable = b.ClaimFormAvailable,
                    LRNo = b.LRNo
                }).OrderByDescending(x => x.ClaimNo).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetClaimNoListPvt", "Get ClaimNo List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get SRS Claim Mapped List
        public List<AddClaimSRSMappingModel> GetSRSClaimMappedLists(int BranchId, int CompId)
        {
            return GetSRSClaimMappedListPvt(BranchId, CompId);
        }
        private List<AddClaimSRSMappingModel> GetSRSClaimMappedListPvt(int BranchId, int CompId)
        {
            List<AddClaimSRSMappingModel> modelList = new List<AddClaimSRSMappingModel>();
            try
            {
                modelList = _contextManager.usp_GetClaimSRSMappedList(BranchId, CompId).Select(b => new AddClaimSRSMappingModel
                {
                    BranchId = Convert.ToInt32(b.BranchId),
                    CompId = Convert.ToInt32(b.CompId),
                    SRSId = b.SRSId,
                    LREntryNo = b.LREntryNo,
                    LRNo = b.LRNo,
                    LRDate = Convert.ToDateTime(b.LRDate),
                    ClaimNo = b.ClaimNo,
                    ClaimDate = Convert.ToDateTime(b.ClaimDate),
                    ReturnCatId = Convert.ToInt32(b.ReturnCatId),
                    SRSStatus = Convert.ToInt32(b.SRSStatus),
                    PONoLRNo = b.PONoLRNo,
                    StockistId = Convert.ToInt32(b.StockistId),
                    StockistNo = b.StockistNo,
                    StockistName = b.StockistName,
                }).OrderByDescending(x => x.ClaimNo).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetClaimNoListPvt", "Get ClaimNo List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region  Add Delay Reason Of Pending CN
        public int UpdateCNDelayReason(UpdateCNDelayReason model)
        {
            return AddDelayReasonOfPendingCNPvt(model);
        }
        public int AddDelayReasonOfPendingCNPvt(UpdateCNDelayReason model)
        {
            int RetValue = 0;
            ObjectParameter objRetValue = new ObjectParameter("RetValue", typeof(string));
            try
            {
                RetValue = _contextManager.usp_UpdateCNDelayReason(model.BranchId, model.CompId, model.SRSId, model.CNDelayReasonId, model.CNDelayRemark, model.AddedBy, objRetValue);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddDelayReasonOfPendingCN", "Add Delay Reason Of Pending CN", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Stockiest Details For Missing Claim
        public List<StokistDtlsModel> GetStockistDtlsForMissingClaim(int BranchId, int CompId, int Flag)
        {
            return GetStockistDtlsForMissingClaimPvt(BranchId, CompId, Flag);
        }
        private List<StokistDtlsModel> GetStockistDtlsForMissingClaimPvt(int BranchId, int CompId, int Flag)
        {
            List<StokistDtlsModel> stockistDtls = new List<StokistDtlsModel>();
            try
            {
                stockistDtls = _contextManager.usp_GetStockistDtlsForMissingClaim(BranchId, CompId, Flag).Select(s => new StokistDtlsModel
                {
                    BranchId = s.BranchId,
                    CompId = s.CompId,
                    LRDate = Convert.ToDateTime(s.LRDate),
                    LRNumber = s.LRNo,
                    ReceiptDate = Convert.ToDateTime(s.ReceiptDate),
                    StockistNo = s.StockistNo,
                    StockistName = s.StockistName,
                    Emailid = s.Emailid,
                    LREntryId = s.LREntryId,
                    TransporterName = s.TransporterName,
                    TransporterNo = s.TransporterNo,
                    CourierName = s.CourierName,
                    TransCourName = s.TransCourName
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistDtlsForEmailPvt", "Get Stockist Dtls For Email", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return stockistDtls;
        }
        #endregion

        #region Get SRS Data List
        public List<ImportSRSList> GetSRSDataLst(int BranchId, int CompId, DateTime Date)
        {
            return GetSRSDataLstPvt(BranchId, CompId, Date);
        }
        private List<ImportSRSList> GetSRSDataLstPvt(int BranchId, int CompId, DateTime Date)
        {
            List<ImportSRSList> srsDataLst = new List<ImportSRSList>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    srsDataLst = _contextManager.usp_SRSHeaderList(BranchId, CompId, Date)
                        .Select(c => new ImportSRSList
                        {
                            BranchId = c.BranchId,
                            CompId = c.CompId,
                            SRSId = c.SRSId,
                            SalesDocNo = c.SalesDocNo,
                            StockistName = c.StockistName,
                            PONoLRNo = c.PONoLRNo,
                            SoldtoPartyId = Convert.ToInt32(c.SoldtoPartyId),
                            StockistNo = c.StockistNo,
                            CityCode = c.CityCode,
                            CityName = c.CityName,
                            Netvalue = c.Netvalue,
                            Division = c.Division,
                            SalesOrganization = c.SalesOrganization,
                            Plant = c.Plant,
                            SRSStatus = Convert.ToInt32(c.SRSStatus)
                        }).OrderBy(x => x.SRSId).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetSRSDataLstPvt", "Get SRS Data List" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return srsDataLst;
        }
        #endregion

        #region Upload Destruction Add Certificate
        public int AddUploadDesCertificate(DestCetModel model)
        {
            return AddUploadDesCertificatePvt(model);
        }
        private int AddUploadDesCertificatePvt(DestCetModel model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_UploadDesCertificate(model.CNIdStr, model.BranchId, model.CompId, model.DestrCertFile, model.AddedBy, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddUploadDesCertificate", "Add Upload Destruction Certificate", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Credit Note List for Upload Destruction Certificate
        public List<CreditNoteListModel> GetCreaditNoteUploadList(int BranchId, int CompId)
        {
            return GetCreaditNoteUploadListPvt(BranchId, CompId);
        }
        private List<CreditNoteListModel> GetCreaditNoteUploadListPvt(int BranchId, int CompId)
        {
            List<CreditNoteListModel> DesCNLst = new List<CreditNoteListModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    DesCNLst = _contextManager.usp_CreditNoteList_UploadDestruction(BranchId, CompId).Select(c => new CreditNoteListModel
                    {

                        CNId = c.CNId,
                        CrDrNoteNo = c.CrDrNoteNo,
                        CRDRCreationDate = Convert.ToDateTime(c.CRDRCreationDate),
                        CrDrAmt = Convert.ToInt64(c.CrDrAmt),
                        StockiestId = Convert.ToInt32(c.StockiestId),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        CityCode = c.CityCode,
                        CityName = c.CityName,
                        SalesOrderNo = c.SalesOrderNo,
                        SalesOrderDate = Convert.ToDateTime(c.SalesOrderDate),
                        OrderReason = c.OrderReason,
                        LRNo = c.LRNo,
                        LRDate = Convert.ToDateTime(c.LRDate),
                        BranchId = Convert.ToInt32(c.BranchId),
                        CompId = Convert.ToInt32(c.CompId),
                        //CompanyCode = c.CompanyCode,
                        //DistChannel = c.DistChannel,
                        //Division = c.Division,
                        //MaterialNumber = c.MaterialNumber,
                        //BatchNo = c.BatchNo,
                        //BillingQty = Convert.ToInt64(c.BillingQty),
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCreaditNoteUploadListPvt", "Get Creadit Note Destruction Certificate List Pvt ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return DesCNLst;
        }
        #endregion

        #region Get Credit Note List Destruction Certificate
        public List<CreditNoteListModel> GetCreaditNoteDestructionList(int BranchId, int CompId)
        {
            return GetCreaditNoteDestructionListPvt(BranchId, CompId);
        }
        private List<CreditNoteListModel> GetCreaditNoteDestructionListPvt(int BranchId, int CompId)
        {
            List<CreditNoteListModel> DesCNLst = new List<CreditNoteListModel>();
            string mainPath = ConfigurationManager.AppSettings["imagepath"];
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    _contextManager.Database.CommandTimeout = 1000;
                    DesCNLst = _contextManager.usp_CreditNoteList_DestructionDue(BranchId, CompId).Select(c => new CreditNoteListModel
                    {

                        CNId = c.CNId,
                        CrDrNoteNo = c.CrDrNoteNo,
                        CRDRCreationDate = Convert.ToDateTime(c.CRDRCreationDate),
                        CrDrAmt = Convert.ToInt64(c.CrDrAmt),
                        StockiestId = Convert.ToInt32(c.StockiestId),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName,
                        CityCode = c.CityCode,
                        CityName = c.CityName,
                        SalesOrderNo = c.SalesOrderNo,
                        SalesOrderDate = Convert.ToDateTime(c.SalesOrderDate),
                        OrderReason = c.OrderReason,
                        LRNo = c.LRNo,
                        LRDate = Convert.ToDateTime(c.LRDate),
                        BranchId = Convert.ToInt32(c.BranchId),
                        CompId = Convert.ToInt32(c.CompId),
                        //CompanyCode = c.CompanyCode,
                        //DistChannel = c.DistChannel,
                        //Division = c.Division,
                        //MaterialNumber = c.MaterialNumber,
                        //BatchNo = c.BatchNo,
                        //BillingQty = Convert.ToInt64(c.BillingQty),
                        DestrCertFile = mainPath + c.DestrCertFile
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCreaditNoteUploadListPvt", "Get Creadit Note Destruction Certificate List Pvt ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return DesCNLst;
        }
        #endregion

        #region Get LR Received Op List
        public List<GetLRReceivedOpModel> GetLRReceivedOpLists(int BranchId, int CompId)
        {
            return GetLRReceivedOpListPvt(BranchId, CompId);
        }
        private List<GetLRReceivedOpModel> GetLRReceivedOpListPvt(int BranchId, int CompId)
        {
            List<GetLRReceivedOpModel> modelList = new List<GetLRReceivedOpModel>();
            try
            {
                modelList = _contextManager.usp_GetReceivedOpList(BranchId, CompId).Select(b => new GetLRReceivedOpModel
                {
                    BranchId = Convert.ToInt32(b.BranchId),
                    CompId = Convert.ToInt32(b.CompId),
                    StockistId = b.StockistId,
                    StockistNo = b.StockistNo,
                    StockistName = b.StockistName,
                    LRNo = b.LRNo,
                    LRDate = Convert.ToDateTime(b.LRDate),
                    AmountPaid = Convert.ToInt32(b.AmountPaid),
                    CityName = b.CityName,
                    TransporterId = Convert.ToInt32(b.TransporterId),
                    TransporterName = b.TransporterName,
                    TransporterNo = b.TransporterNo,
                    CourierName = b.CourierName,
                    OtherTrasport = b.OtherTrasport,
                    TransCourName = b.TransCourName,
                    LREntryId = b.LREntryId,
                    LREntryNo = b.LREntryNo,
                    LREntryDate = Convert.ToDateTime(b.LREntryDate),
                    GoodsReceived = b.GoodsReceived,
                    ClaimFormAvailable = b.ClaimFormAvailable,
                    GatepassNo = b.GatepassNo,
                    ReceiptDate = Convert.ToDateTime(b.ReceiptDate).ToString("yyyy-MM-dd"),
                    RecvdAtOP = Convert.ToBoolean(b.RecvdAtOP),
                    RecvdAtOPDate = Convert.ToDateTime(b.RecvdAtOPDate).ToString("yyyy-MM-dd"),
                    ConcernDate = (b.ConcernDate != null ? Convert.ToDateTime(b.ConcernDate).ToString() : null),
                    ResolveConcernDate = (b.ResolveConcernDate != null ? Convert.ToDateTime(b.ResolveConcernDate).ToString() : null)
                }).OrderByDescending(x => x.LRNo).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRReceivedOpListPvt", "Get LR ReceivedOp List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Add Inward Gatepass Received
        public int UpdateInwrdGtpassRecvedAdd(UpdateInwrdGtpassRecvedModel model)
        {
            return UpdateInwrdGtpassRecvedAddPvt(model);
        }
        private int UpdateInwrdGtpassRecvedAddPvt(UpdateInwrdGtpassRecvedModel model)
        {

            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
            try
            {
                RetValue = _contextManager.usp_InwardGatepassReceived(model.LREntryId, model.BranchId, model.CompId, model.AddedBy, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "UpdateInwrdGtpassRecvedAddPvt", "Update InwrdGtpass Recved AddPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Lr Mis Match List
        public List<GetLrMisMatchListModel> GetLrMisMatchLists(int BranchId, int CompId)
        {
            return GetLrMisMatchListPvt(BranchId, CompId);
        }
        private List<GetLrMisMatchListModel> GetLrMisMatchListPvt(int BranchId, int CompId)
        {
            List<GetLrMisMatchListModel> modelList = new List<GetLrMisMatchListModel>();
            try
            {
                modelList = _contextManager.usp_GetLrMisMatchList(BranchId, CompId).Select(b => new GetLrMisMatchListModel
                {
                    BranchId = Convert.ToInt32(b.BranchId),
                    CompId = Convert.ToInt32(b.CompId),
                    LREntryId = b.LREntryId,
                    LREntryNo = b.LREntryNo,
                    LREntryDate = Convert.ToDateTime(b.LREntryDate),
                    StockistId = b.StockistId,
                    StockistNo = b.StockistNo,
                    StockistName = b.StockistName,
                    LRNo = b.LRNo,
                    LRDate = Convert.ToDateTime(b.LRDate),
                    AmountPaid = Convert.ToInt32(b.AmountPaid),
                    CityName = b.CityName,
                    TransporterId = Convert.ToInt32(b.TransporterId),
                    TransporterName = b.TransporterName,
                    TransporterNo = b.TransporterNo,
                    CourierId = Convert.ToInt32(b.CourierId),
                    CourierName = b.CourierName,
                    TransCourName = b.TransCourName,
                    OtherTrasport = b.OtherTrasport,
                    GoodsReceived = Convert.ToDouble(b.GoodsReceived),
                    ClaimFormAvailable = b.ClaimFormAvailable,
                    GatepassNo = b.GatepassNo,
                    ReceiptDate = Convert.ToDateTime(b.ReceiptDate),
                    RecvdAtOP = Convert.ToDouble(b.RecvdAtOP),
                    RecvdAtOPDate = Convert.ToDateTime(b.RecvdAtOPDate),
                    SRSId = Convert.ToInt32(b.SRSId),
                    SalesDocNo = b.SalesDocNo
                }).OrderByDescending(x => x.LRNo).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLrMisMatchListPvt", "Get Lr MisMatch List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Lr MisMatch Count List
        public GetLRMisMatchcountModel GetLRSRSMappingCounts(GetLRMisMatchcountModel model)
        {
            return GetLRSRSMappingCountPvt(model);
        }
        private GetLRMisMatchcountModel GetLRSRSMappingCountPvt(GetLRMisMatchcountModel model)
        {
            GetLRMisMatchcountModel modelList = new GetLRMisMatchcountModel();
            try
            {
                modelList = _contextManager.usp_GetLR_SRSMappingCounts(model.BranchId, model.CompId).Select(b => new GetLRMisMatchcountModel
                {
                    BranchId = Convert.ToInt32(b.BranchId),
                    CompId = Convert.ToInt32(b.CompId),
                    TodayTotalLR = Convert.ToInt32(b.TodayTotalLR),
                    TodayReceivedLR = Convert.ToInt32(b.TodayReceivedLR),
                    TodayImportedLR = Convert.ToInt32(b.TodayImportedLR),
                    NotFoundLR = Convert.ToInt32(b.NotFoundLR),
                    TodayVerifiedCount = Convert.ToInt32(b.TodayVerifiedCount),
                    CorrReqCount = Convert.ToInt32(b.CorrReqCount)
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRSRSMappingCountPvt", "Get Lr MisMatch Count List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get LR Page Count List
        public LRPageCounts GetLRPageCounts(LRPageCounts model)
        {
            return GetLRPageCountsPvt(model);
        }
        private LRPageCounts GetLRPageCountsPvt(LRPageCounts model)
        {
            LRPageCounts modelList = new LRPageCounts();
            try
            {
                modelList = _contextManager.usp_GetLRPageCounts(model.BranchId, model.CompId).Select(b => new LRPageCounts
                {
                    BranchId = Convert.ToInt32(b.BranchId),
                    CompId = Convert.ToInt32(b.CompId),
                    TodayLRGP = Convert.ToInt32(b.TodayLRGP),
                    ConcernCnt = Convert.ToInt32(b.ConcernCnt),
                    ConcernResolveCnt = Convert.ToInt32(b.ConcernResolveCnt),
                    RecvdAtOPCnt = Convert.ToInt32(b.RecvdAtOPCnt),
                    PendingAtExpSCnt = Convert.ToInt32(b.PendingAtExpSCnt)
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRPageCountsPvt", "Get Lr MisMatch Count List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get SRS CN Mapping Counts
        public GetSRSCNMappingCountsModel GetSRSCNMappingCountList(GetSRSCNMappingCountsModel model)
        {
            return GetSRSCNMappingCountListPvt(model);
        }
        private GetSRSCNMappingCountsModel GetSRSCNMappingCountListPvt(GetSRSCNMappingCountsModel model)
        {
            GetSRSCNMappingCountsModel modelList = new GetSRSCNMappingCountsModel();
            try
            {
                modelList = _contextManager.usp_GetSRS_CN_MappingCounts(model.BranchId, model.CompId).Select(b => new GetSRSCNMappingCountsModel
                {
                    BranchId = Convert.ToInt32(b.BranchId),
                    CompId = Convert.ToInt32(b.CompId),
                    TodaysSRSCnt = Convert.ToInt32(b.TodaysSRSCnt),
                    TodayCNCnt = Convert.ToInt32(b.TodayCNCnt),
                    PendingForCNCnt = Convert.ToInt32(b.PendingForCNCnt),
                    PendingDestrCertCnt = Convert.ToInt32(b.PendingDestrCertCnt)
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRPageCountsPvt", "Get SRS CN Mapping Counts", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        //#region Get LR SRS CN List for FilterDataOrdrRtrnList --Not In Use
        //public List<GetLRReceivedOpModel> GetLRSRSCNListforFilterDataOrdrRtrnList(int BranchId, int CompanyId, DateTime FromDate, DateTime ToDate)
        //{
        //    return GetLRSRSCNListforFilterDataOrdrRtrnListPvt(BranchId, CompanyId, FromDate, ToDate);
        //}
        //private List<GetLRReceivedOpModel> GetLRSRSCNListforFilterDataOrdrRtrnListPvt(int BranchId, int CompanyId, DateTime FromDate, DateTime ToDate)
        //{
        //    List<GetLRReceivedOpModel> modelList = new List<GetLRReceivedOpModel>();
        //    try
        //    {
        //        modelList = _contextManager.usp_DashbordOrderReturnList(BranchId, CompanyId, FromDate, ToDate).Select(b => new GetLRReceivedOpModel
        //        {                 
                   
        //            LREntryId = b.LREntryId,
        //            LRNo = b.LRNo,
        //            LREntryDateFormat = Convert.ToDateTime(b.LREntryDate).ToString("yyyy-MM-dd"),
        //            StockistId = b.StockistId,
        //            StockistNo = b.StockistNo,
        //            StockistName = b.StockistName,
        //            GatepassNo = b.GatepassNo, 
        //            ClaimNo = b.ClaimNo,
        //            SalesDocNo = b.SalesDocNo,
        //            CrDrNoteNo = b.CrDrNoteNo,
        //            CRDRCreationDate = Convert.ToDateTime(b.CRDRCreationDate).ToString("yyyy-MM-dd"),
        //            ReceiptDate = Convert.ToDateTime(b.ReceiptDate).ToString("yyyy-MM-dd"),
        //            RecvdAtOP = b.RecvdAtOP,
        //            ReturnCatId = Convert.ToInt32(b.ReturnCatId),
        //            IsVerified =b.IsVerified,
        //            Day1 = b.Day1,
        //            day2 = b.day2,
        //            day2_7 = b.day2_7,
        //            day7Plus = b.day7Plus,
        //            //D15 = b.D15,
        //            //D30 = b.D30,
        //            //D45 = b.D45,
        //            //D45Plus = b.D45Plus

        //        }).OrderByDescending(x => x.LRNo).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetLRSRSCNListforFilterDataOrdrRtrnListPvt", "GetLRSRSCNListforFilterDataOrdrRtrnListPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return modelList;
        //}
        //#endregion

        //#region For Order Return Separated New Count -- Not In Use
        //public OrderReturnModelNewCount GetDashboradCountOrderReturn(int BranchId, int CompanyId, DateTime FromDate, DateTime ToDate)
        //{
        //    return GetDashboradCountOrderReturnPvt(BranchId, CompanyId, FromDate, ToDate);
        //}
        //private OrderReturnModelNewCount GetDashboradCountOrderReturnPvt(int BranchId,int CompanyId,DateTime FromDate, DateTime ToDate)
        //{
        //    OrderReturnModelNewCount OrderReturncnt = new OrderReturnModelNewCount();
        //    try
        //    {
        //        using (CFADBEntities _contextManager = new CFADBEntities())
        //        {
        //            _contextManager.Database.CommandTimeout = 1000;
        //            OrderReturncnt = _contextManager.usp_GetDashOrderRetAllLoginCN(BranchId, CompanyId, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate)).Select(b => new OrderReturnModelNewCount
        //            {
        //                Onedays = b.Onedays,
        //                twodays = b.twodays,
        //                morethentwodays = b.morethentwodays,
        //                moresevdays = b.moresevdays,
        //                PendingCN = b.PendingCN,
        //                PendingSRS = b.PendingSRS,
        //                FifteenDays = b.FifteenDays,
        //                ThirthyDays = b.ThirthyDays,
        //                fortyfiveDays = b.fortyfiveDays,
        //                abovefortyfiveDays = b.abovefortyfiveDays
        //            }).FirstOrDefault();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetDashboradCountOrderReturnPvt", "Get Dashborad Count Order Return Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return OrderReturncnt;
        //}

        //#endregion

        #region Get DashBoard Order Return Count
        public GetORdashbordsupervisorModel GetDashBordSupervisorCount(int BranchId, int CompanyId)
        {
            return GetDashBordSupervisorCountPvt(BranchId, CompanyId);
        }
        private GetORdashbordsupervisorModel GetDashBordSupervisorCountPvt(int BranchId, int CompanyId)
        {
            GetORdashbordsupervisorModel Ordash = new GetORdashbordsupervisorModel();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    Ordash = _contextManager.usp_DashbordOrderReturnCntNew(BranchId, CompanyId).Select(b => new GetORdashbordsupervisorModel
                    {
                        ConsignToday = b.ConsignToday,
                        ConsignPending = b.ConsignPending,
                        atWarehouse = b.atWarehouse,
                        atOperator = b.atOperator,
                        atAuditorChk = b.atAuditorChk,
                        SalebleClaim = b.SalebleClaim,
                        DestrPending = b.DestrPending,
                        SalebleCN1 = b.SalebleCN1,
                        SalebleCN2 = b.SalebleCN2,
                        SalebleMore2 = b.Salemore2Days,
                        PendingCN = b.PendingCN,
                        SalebleCN1Per = b.SalebleCN1Per,
                        SalebleCN2Per = b.SalebleCN2Per,
                        Salemore2DaysPer = b.Salemore2DaysPer,
                        ExpCN15D = b.ExpCN15D,
                        ExpCN30D = b.ExpCN30D,
                        ExpCN45D = b.ExpCN45D,
                        ExpCNMore45D = b.ExpCNMore45D,
                        ExpCN15DPer = b.ExpCN15DPer,
                        ExpCN30DPer = b.ExpCN30DPer,
                        ExpCN45DPer = b.ExpCN45DPer,
                        ExpCNMore45DPer = b.ExpCNMore45DPer,
                        SalelablePen2 = Convert.ToInt32(b.SalelablePen2),
                        NonSalelablePen45 = Convert.ToInt32(b.NonSalelablePen45)
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetDashBordSupervisorCountPvt", "Get Dash Bord Supervisor Count", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Ordash;
        }
        #endregion

        #region Get Order Return Filtered List New
        public List<GetLRReceivedOpModel> GetOrderReturnFilterNewList(int BranchId, int CompanyId)
        {
            return GetOrderReturnFilterNewListPvt(BranchId, CompanyId);
        }
        private List<GetLRReceivedOpModel> GetOrderReturnFilterNewListPvt(int BranchId, int CompanyId)
        {
            List<GetLRReceivedOpModel> modelList = new List<GetLRReceivedOpModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    modelList = _contextManager.usp_DashbordOrderReturnListNew(BranchId, CompanyId).Select(b => new GetLRReceivedOpModel
                    {
                        LREntryId = b.LREntryId,
                        LRNo = b.LRNo,
                        LREntryDateFormat = Convert.ToDateTime(b.LREntryDate).ToString("yyyy-MM-dd"),
                        StockistId = b.StockistId,
                        StockistNo = b.StockistNo,
                        StockistName = b.StockistName,
                        GatepassNo = b.GatepassNo,
                        ClaimNo = b.ClaimNo,
                        SalesDocNo = b.SalesDocNo,
                        CrDrNoteNo = b.CrDrNoteNo,
                        CRDRCreationDate = Convert.ToDateTime(b.CRDRCreationDate).ToString("yyyy-MM-dd"),
                        ReceiptDate = Convert.ToDateTime(b.ReceiptDate).ToString("yyyy-MM-dd"),
                        RecvdAtOP = b.RecvdAtOP,
                        ReturnCatId = Convert.ToInt32(b.ReturnCatId),
                        IsVerified = b.IsVerified,
                        SRSId = Convert.ToInt32(b.SRSId),
                        CNId = Convert.ToInt64(b.CNId),
                        TransporterName = b.TransporterName,
                        SalebleCN1 = b.Day1,
                        SalebleCN2 = b.day2,
                        SalebleMore2 = b.morethan2Days,
                        ExpCN15D = b.ExpCN15D,
                        ExpCN30D = b.ExpCN30D,
                        ExpCN45D = b.ExpCN45D,
                        ExpCNMore45D = b.ExpCNMore45D,
                        DestrCertDate = Convert.ToDateTime(b.DestrCertDate),
                        DestrCertFile = b.DestrCertFile,
                        DestrCertAddedBy = Convert.ToInt32(b.DestrCertAddedBy),
                        RecvdAtOPForNob = Convert.ToInt32(b.RecvdAtOP),
                        SRSAgeingDays = Convert.ToInt32(b.SRSAgeingDays)
                    }).OrderByDescending(x => x.LRNo).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderReturnFilterListPvt", "GetOrderReturnFilterListPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Pend Sale CN New List
        public List<GetLRReceivedOpModel> GetPendSaleCNNewList(int BranchId, int CompanyId, string Flag)
        {
            return GetPendSaleCNNewListPvt(BranchId, CompanyId, Flag);
        }
        private List<GetLRReceivedOpModel> GetPendSaleCNNewListPvt(int BranchId, int CompanyId, string Flag)
        {
            List<GetLRReceivedOpModel> modelList = new List<GetLRReceivedOpModel>();
            try
            {
                modelList = _contextManager.usp_DashOrderRetrnPendSaleCNLstNew(BranchId, CompanyId, Flag).Select(b => new GetLRReceivedOpModel
                {
                    LREntryId = b.LREntryId,
                    LRNo = b.LRNo,
                    LREntryDateFormat = Convert.ToDateTime(b.LREntryDate).ToString("yyyy-MM-dd"),
                    StockistId = b.StockistId,
                    StockistNo = b.StockistNo,
                    StockistName = b.StockistName,
                    GatepassNo = b.GatepassNo,
                    ClaimNo = b.ClaimNo,
                    SalesDocNo = b.SalesDocNo,
                    CrDrNoteNo = b.CrDrNoteNo,
                    CRDRCreationDate = Convert.ToDateTime(b.CRDRCreationDate).ToString("yyyy-MM-dd"),
                    ReceiptDate = Convert.ToDateTime(b.ReceiptDate).ToString("yyyy-MM-dd"),
                    RecvdAtOP = b.RecvdAtOP,
                    ReturnCatId = Convert.ToInt32(b.ReturnCatId),
                    IsVerified = b.IsVerified,
                    SRSId = Convert.ToInt32(b.SRSId),
                    CNId = Convert.ToInt64(b.CNId),
                    TransporterName = b.TransporterName,
                    SaleblePendCN = b.SalelablePendCN,            
                    DestrCertDate = Convert.ToDateTime(b.DestrCertDate),
                    DestrCertFile = b.DestrCertFile,
                    DestrCertAddedBy = Convert.ToInt32(b.DestrCertAddedBy),
                    RecvdAtOPForNob = Convert.ToInt32(b.RecvdAtOP)
                }).OrderByDescending(x => x.LRNo).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPendSaleCNNewListPvt", "Get Pend Sale CN New List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get LR SRS CN List for FilterDataOrdrRtrnList
        public ExpSupCountmodel ExpSupCount(int BranchId, int CompId)
        {
            return ExpSupCountPvt(BranchId, CompId);
        }
        private ExpSupCountmodel ExpSupCountPvt(int BranchId, int CompId)
        {
            ExpSupCountmodel model = new ExpSupCountmodel();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    model = _contextManager.usp_ExpSupCount(BranchId, CompId).Select(e => new ExpSupCountmodel
                    {
                        CNCount = Convert.ToInt64(e.CNCount),
                        PendingLR = Convert.ToInt64(e.PendingLR)
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ExpSupCountPvt", "ExpSupCountPvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Start - Expiry Supervisor Dashboard Mob
        public List<ExpirySupervisorDashboardMobModel> ExpirySupervisorDashboardMob(int BranchId, int CompId)
        {
            return ExpirySupervisorDashboardMobPvt(BranchId, CompId);
        }
        private List<ExpirySupervisorDashboardMobModel> ExpirySupervisorDashboardMobPvt(int BranchId, int CompId)
        {
            List<ExpirySupervisorDashboardMobModel> modelList = new List<ExpirySupervisorDashboardMobModel>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    modelList = _contextManager.usp_ExpirySupervisorDashboard_Mob(BranchId, CompId).Select(e => new ExpirySupervisorDashboardMobModel
                    {
                        LREntryId = Convert.ToInt32(e.LREntryId),
                        BranchId = Convert.ToInt32(e.BranchId),
                        CompId = Convert.ToInt32(e.CompId),
                        LREntryNo = Convert.ToString(e.LREntryNo),
                        LREntryDate = Convert.ToDateTime(e.LREntryDate),
                        StockistId = Convert.ToInt32(e.StockistId),
                        StockistNo = Convert.ToString(e.StockistNo),
                        StockistName = Convert.ToString(e.StockistName),
                        City = Convert.ToInt32(e.City),
                        CityName = Convert.ToString(e.CityName),
                        TransporterId = Convert.ToInt32(e.TransporterId),
                        TransporterName = Convert.ToString(e.TransporterName),
                        CourierId = Convert.ToInt32(e.CourierId),
                        CourierName = Convert.ToString(e.CourierName),
                        OtherTrasport = Convert.ToString(e.OtherTrasport),
                        LRNo = Convert.ToString(e.LRNo),
                        LRDate = Convert.ToDateTime(e.LRDate),
                        NoOfBox = Convert.ToInt32(e.NoOfBox),
                        AmountPaid = Convert.ToInt32(e.AmountPaid),
                        CashmemoDate = Convert.ToDateTime(e.CashmemoDate),
                        ClaimFormAvailable = Convert.ToInt32(e.ClaimFormAvailable),
                        GoodsReceived = Convert.ToInt32(e.GoodsReceived),
                        GatepassNo = Convert.ToString(e.GatepassNo),
                        ReceiptDate = Convert.ToDateTime(e.ReceiptDate).ToString("yyyy-MM-dd"),
                        RecvdAtOP = Convert.ToInt32(e.RecvdAtOP),
                        RecvdAtOPDate = Convert.ToDateTime(e.RecvdAtOPDate),
                        TransCourName = Convert.ToString(e.TransCourName),
                        PhyChkId = Convert.ToInt32(e.PhyChkId),
                        PhyChkAgeing = Convert.ToInt32(e.PhyChkAgeing),
                        GoodNotRecAgeing = Convert.ToInt32(e.GoodNotRecAgeing),
                        ClaimMissingAgeing = Convert.ToInt32(e.ClaimMissingAgeing),
                        ReturnCatId = Convert.ToInt32(e.ReturnCatId),
                        RetCatName = Convert.ToString(e.RetCatName),
                        ClaimNo = Convert.ToString(e.ClaimNo),
                        ClaimDate = Convert.ToDateTime(e.ClaimDate),
                        ClaimStatus = Convert.ToInt32(e.ClaimStatus),
                        ConcernId = Convert.ToInt32(e.ConcernId),
                        ConcernText = Convert.ToString(e.ConcernText),
                        ConcernRemark = Convert.ToString(e.ConcernRemark),
                        ConcernDate = Convert.ToDateTime(e.ConcernDate),
                        ConcernBy = Convert.ToInt32(e.ConcernBy),
                        ConcernByName = Convert.ToString(e.ConcernByName),
                        ResolveConcernBy = Convert.ToInt32(e.ResolveConcernBy),
                        ResolveConcernByName = Convert.ToString(e.ResolveConcernByName),
                        ResolveConcernDate = Convert.ToDateTime(e.ResolveConcernDate),
                        ResolveRemark = Convert.ToString(e.ResolveRemark),
                        SRSId = Convert.ToInt32(e.SRSId),
                        IsVerified = Convert.ToString(e.IsVerified),
                        IsCorrectionReq = Convert.ToString(e.IsCorrectionReq),
                        CorrectionReqRemark = Convert.ToString(e.CorrectionReqRemark),
                        VerifyCorrectionBy = Convert.ToInt32(e.VerifyCorrectionBy),
                        VerifyCorrectionDate = Convert.ToDateTime(e.VerifyCorrectionDate),
                        AuditChkAgeing = Convert.ToInt32(e.AuditChkAgeing),
                        SalesDocNo = e.SalesDocNo
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ExpirySupervisorDashboardMob", "Expiry Supervisor Dashboard Mob", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return modelList;
        }
        #endregion End - Expiry Supervisor Dashboard Mob

        #region Get Owner OR Pend Consig Dash Smmry List
        public List<OwnORPendConsigDashSmmryList> GetOwnerORPendConsigDashSmmryList()
        {
            return GetOwnerORPendConsigDashSmmryListPvt();
        }
        private List<OwnORPendConsigDashSmmryList> GetOwnerORPendConsigDashSmmryListPvt()
        {
            List<OwnORPendConsigDashSmmryList> modelList = new List<OwnORPendConsigDashSmmryList>();
            try
            {
                modelList = _contextManager.usp_OwnerORPendConsigDashSmmryList().Select(c => new OwnORPendConsigDashSmmryList
                {
                    BranchId = c.BranchId,
                    BranchName = c.BranchName,
                    CompId = c.CompId,
                    CompanyName = c.CompanyName,
                    TotalPending = Convert.ToInt32(c.TotalPending)
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerORPendConsigDashSmmryListPvt", "Get Owner OR Pend Consig Dash Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Owner Saleable CN Dash Smmry List
        public List<OwnSaleableCNDashSmmryList> GetOwnerSaleableCNDashSmmryList(string FlagType)
        {
            return GetOwnerSaleableCNDashSmmryListPvt(FlagType);
        }
        private List<OwnSaleableCNDashSmmryList> GetOwnerSaleableCNDashSmmryListPvt(string FlagType)
        {
            List<OwnSaleableCNDashSmmryList> modelList = new List<OwnSaleableCNDashSmmryList>();
            try
            {
                modelList = _contextManager.usp_OwnerSaleableCNDashSmmryList(FlagType).Select(c => new OwnSaleableCNDashSmmryList
                {
                    BranchId = c.BranchId,
                    BranchName = c.BranchName,
                    CompId = c.CompId,
                    CompanyName = c.CompanyName,
                    More2Day = c.More2Day,
                    More11Day = c.More11Day
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerSaleableCNDashSmmryListPvt", "Get Owner Saleable CN Dash Smmry List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region  Get Order Return List For Pending CN
        public List<GetPendingCNModel> GetOrderReturnPendingCN(int BranchId,int CompId)
        {
            return GetOrderReturnPendingCNPvt(BranchId, CompId);
        }
        private List<GetPendingCNModel> GetOrderReturnPendingCNPvt(int BranchId, int CompId)
        {
            List<GetPendingCNModel> PenCNList = new List<GetPendingCNModel>();
            try
            {
               using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    PenCNList = _contextManager.usp_DashbordOrderReturnPendCNListNew(BranchId, CompId).Select(b => new GetPendingCNModel
                    {
                        SRSId = Convert.ToInt32(b.SRSId),
                        LREntryId = b.LREntryId,
                        LRNo = b.LRNo,
                        LREntryDateFormat = Convert.ToDateTime(b.LREntryDate).ToString("yyyy-MM-dd"),
                        StockistId = b.StockistId,
                        StockistNo = b.StockistNo,
                        StockistName = b.StockistName,
                        GatepassNo = b.GatepassNo,
                        ClaimNo = b.ClaimNo,
                        CNId = Convert.ToInt64(b.CNId),
                        TransporterName = b.TransporterName,
                        SalesDocNo = Convert.ToString(b.SalesDocNo),
                        CrDrNoteNo = Convert.ToString(b.CrDrNoteNo)
                    }).OrderByDescending(x => x.LRNo).ToList();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderReturnPendingCNPvt", "Get Order Return Pending CN Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PenCNList;
        }
        #endregion

        #region  Get Order Return List For Saleable Count
        public List<GetSaleableModel> GetOrderReturnSaleable(int BranchId, int CompId)
        {
            return GetOrderReturnSaleablePvt(BranchId, CompId);
        }
        private List<GetSaleableModel> GetOrderReturnSaleablePvt(int BranchId, int CompId)
        {
            List<GetSaleableModel> SaleableList = new List<GetSaleableModel>();
            try
            {
                SaleableList = _contextManager.usp_DashOrderRtunSaleableClaimListNew(BranchId, CompId).Select(b => new GetSaleableModel
                {
                    SRSId = Convert.ToInt32(b.SRSId),
                    LREntryId = b.LREntryId,
                    LRNo = b.LRNo,
                    LREntryDateFormat = Convert.ToDateTime(b.LREntryDate).ToString("yyyy-MM-dd"),
                    StockistId = b.StockistId,
                    StockistNo = b.StockistNo,
                    StockistName = b.StockistName,
                    GatepassNo = b.GatepassNo,
                    ClaimNo = b.ClaimNo,
                    CNId = Convert.ToInt64(b.CNId),
                    TransporterName = b.TransporterName,
                }).OrderByDescending(x => x.LRNo).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderReturnSaleablePvt", "Get Order Return Saleable Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return SaleableList;
        }
        #endregion

        #region  Get Inward SRS List Pvt
        public List<InwardGatepassListModel> GetInwardSRSList(int BranchId, int CompId)
        {
            return GetInwardSRSListPvt(BranchId, CompId);
        }
        private List<InwardGatepassListModel> GetInwardSRSListPvt(int BranchId, int CompId)
        {
            List<InwardGatepassListModel> lrRecievedList = new List<InwardGatepassListModel>();
            List<srsListModelByLR> srslistbyLR = new List<srsListModelByLR>();
            try
            {
                using (CFADBEntities _contextManager = new CFADBEntities())
                {
                    lrRecievedList = _contextManager.usp_GetInwradLRRecievedList(BranchId, CompId).Select(c => new InwardGatepassListModel
                    {
                        BranchId = c.BranchId,
                        CompId = c.CompId,
                        LREntryId = c.LREntryId,
                        LRNo = c.LRNo,
                        CityName = c.CityName,
                        LRDate  = Convert.ToDateTime(c.LRDate),
                        ReceiptDate = Convert.ToDateTime(c.ReceiptDate),
                        RecvdAtOPDate = Convert.ToDateTime(c.RecvdAtOPDate),
                        StockistNo = c.StockistNo,
                        StockistName = c.StockistName
                    }).ToList();

                    for (int i = 0; i < lrRecievedList.Count(); i++)
                    {
                        srslistbyLR = _contextManager.usp_GetInwradSRSListByLRNo(BranchId, CompId,lrRecievedList[i].LREntryId)
                            .Select(p => new srsListModelByLR
                            {
                                BranchId = p.BranchId,
                                CompId = p.CompId,
                                SRSId = p.SRSId,
                                LREntryId = Convert.ToInt32(p.LREntryId),
                                SalesDocNo = p.SalesDocNo,
                                StockistNo = p.StockistNo,
                                StockistName = p.StockistName,
                                LRNo = p.LRNo,
                                AddedOn = Convert.ToDateTime(p.AddedOn)                               
                            }).ToList();
                        lrRecievedList[i].srsListByLRNo = srslistbyLR;
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInwardSRSListPvt", "Get Inward SRS List Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return lrRecievedList;
        }
        #endregion

        #region Delete Inward SRS details
        public int DeleteInwardSRSDetails(int BranchId, int CompId,int SRSId)
        {
            return DeleteInwardSRSDetailsPvt(BranchId,CompId, SRSId);
        }
        private int DeleteInwardSRSDetailsPvt(int BranchId, int CompId, int SRSId)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));

            try
            {
                RetValue = _contextManager.usp_DeleteInwardSRSDetails(BranchId,CompId, SRSId, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "DeleteInwardSRSDetailsPvt", "Delete Inward SRS Details Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion
    }
}

