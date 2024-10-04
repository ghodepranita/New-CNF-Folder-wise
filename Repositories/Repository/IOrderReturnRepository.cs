using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CNF.Business.Model.OrderReturn.OrderReturn;

namespace CNF.Business.Repositories.Repository
{
    public interface IOrderReturnRepository
    {
        string GetNewGeneratedGatepassNo(int BranchId, int CompId, DateTime ReceiptDate);
        string GetNewGeneratedLRNo(int BranchId, int CompId, DateTime LREntryDate);
        string AddEditLRDetails(InwardGatepassModel model);
        string GenerateInwardGatepass(InwardGatepassModel model);
        List<StokistDtlsModel> GetStockistDtlsForEmail(int BranchId, int CompId, int LREntryId);
        int AddSendEmailFlag(int BranchId, int CompId, int LREntryId, int Flag);
        List<InwardGatepassModel> GetInwardGatepassList(int BranchId, int CompId);
        List<InwardGatepassModel> GetInwardGatepassListForEdit(int BranchId, int CompId);
        int EditInwardLRDetails(inwardLREditModel model);
        //string SendEmailForConsignmentReceived(string Emailid, string StockistName, string TransCourName, string LRNumber, DateTime LRDate, DateTime ReceiptDate,int ClaimFormAvailable, int BranchId, int CompId);
        List<InwardGatepassLRDtlsModel> GetLRDtlsLstOfCnsgmntRecvdFrSndMail(int BranchId, int CompId);
        List<StokistDtlsModel> GetStockistDtlsForMissingClaim(int BranchId, int CompId, int Flag);
        //List<InwardGatepassModel> GetMissingClaimFormList(int BranchId, int CompId, int Flag);
        //string SendEmailForMissingClaimForm(string Emailid, int BranchId, int CompId, string StockistName);
        int PhysicalCheck1AddEdit(PhysicalCheck1 model);
        List<PhysicalCheck1ListModel> GetPhysicalCheck1List(int BranchId, int CompId);
        int PhysicalCheck1Concern(PhysicalCheck1 model);
        List<SRSClaimListForVerifyModel> GetSRSClaimListForVerifyList(int BranchId, int CompId);
        int AuditorCheckCorrection(AuditorCheckCorrectionModel model);
        List<ImportCNDataList> GetImportCNDataList(int BranchId, int CompId);
        List<CNDataForEmail> GetImportCNDataForEmail(int BranchId, int CompId);
        int ClaimSRSMappingAddEdits(AddSRSMapping model);
        List<AddClaimSRSMappingModel> GetClaimSRSMappingLists(int BranchId, int CompId, int SRSId);
        List<GetClaimNoListModel> GetClaimNoLists(int BranchId, int CompId);
        List<AddClaimSRSMappingModel> GetSRSClaimMappedLists(int BranchId, int CompId);
        int UpdateCNDelayReason(UpdateCNDelayReason model);
        List<ImportSRSList> GetSRSDataLst(int BranchId, int CompId, DateTime Date);
        int AddUploadDesCertificate(DestCetModel model);
        List<CreditNoteListModel> GetCreaditNoteUploadList(int BranchId, int CompId);
        List<CreditNoteListModel> GetCreaditNoteDestructionList(int BranchId, int CompId);
        List<GetLRReceivedOpModel> GetLRReceivedOpLists(int BranchId, int CompId);
        int UpdateInwrdGtpassRecvedAdd(UpdateInwrdGtpassRecvedModel model);
        List<GetLrMisMatchListModel> GetLrMisMatchLists(int BranchId, int CompId);
        GetLRMisMatchcountModel GetLRSRSMappingCounts(GetLRMisMatchcountModel model);
        LRPageCounts GetLRPageCounts(LRPageCounts model);
        GetSRSCNMappingCountsModel GetSRSCNMappingCountList(GetSRSCNMappingCountsModel model);
        GetORdashbordsupervisorModel GetDashBordSupervisorCount(int BranchId, int CompId);
        //List<GetLRReceivedOpModel> GetLRSRSCNListforFilterDataOrdrRtrnList(int BranchId, int CompId, DateTime FromDate, DateTime ToDate);
        //OrderReturnModelNewCount GetDashboradCountOrderReturn(int BranchId, int CompId, DateTime FromDate, DateTime ToDate);
        List<GetLRReceivedOpModel> GetOrderReturnFilterNewList(int BranchId, int CompanyId);
        List<GetLRReceivedOpModel> GetPendSaleCNNewList(int BranchId, int CompanyId,string Flag);
        ExpSupCountmodel ExpSupCount(int BranchId, int CompId);
        List<ExpirySupervisorDashboardMobModel> ExpirySupervisorDashboardMob(int BranchId, int CompId);
        List<OwnORPendConsigDashSmmryList> GetOwnerORPendConsigDashSmmryList();
        List<OwnSaleableCNDashSmmryList> GetOwnerSaleableCNDashSmmryList(string FlagType);
        List<GetPendingCNModel> GetOrderReturnPendingCN(int BranchId, int CompId);
        List<GetSaleableModel> GetOrderReturnSaleable(int BranchId, int CompId);
        List<InwardGatepassListModel> GetInwardSRSList(int BranchId, int CompId);
        int DeleteInwardSRSDetails(int BranchId, int CompId, int SRSId);
    }
}
