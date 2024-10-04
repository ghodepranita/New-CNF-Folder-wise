using CNF.Business.Model.OrderDispatch;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories.Repository
{
   public interface IOrderDispatchRepository
    {
        string PickListHeaderAddEdit(PickListModel model);
        List<PickListModel> GetPickLst(int BranchId, int CompId, DateTime PicklistDate);
        string sendEmail(string EmailId, string PicklistNo);
        string sendEmailToPicker(string EmailId, string PicklistNo);
        string PicklistAllotmentAdd(PicklstAllotReallotModel model);
        string PicklistReAllotmentAdd(PicklstAllotReallotModel model);
        string PicklistAllotmentStatus(PicklistAllotmentStatusModel model);
        List<InvoiceLstModel> GetInvoiceHeaderLst(int BranchId, int CompId, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate, int BillDrawerId, Nullable<int> InvStatus);
        List<InvoiceLstModel> GetInvoiceHeaderListForPriority(int BranchId, int CompId);
        List<InvoiceLstModel> GetInvoiceHeaderLstForMob(int BranchId, int CompId, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate, int BillDrawerId, Nullable<int> InvStatus);
        List<Picklstmodel> GetAllotedPickListForPicker(int BranchId, int CompId, int PickerId, DateTime PicklistDate);
        string InvoiceHeaderStatusUpdate(InvoiceHeaderStatusUpdateModel model);
        string AssignTransportMode(AssignTransportModel model);
        PickLstSummaryData GetPickListSummaryData(int BranchId, int CompId, int PickerId, DateTime PicklistDate);
        List<InvoiceLstModel> InvoiceListForAssignTransMode(int BranchId, int CompId);
        string GetPickListGenerateNewNo(int BranchId, int CompId, DateTime PicklistDate);
        List<GetInvoiceDetailsForStickerModel> GetInvoiceDetailsForSticker(int BranchId, int CompId, int InvId);
        GetInvoiceDetailsForStickerModel GetInvoiceDetailsForPrintSticker(int BranchId, int CompId, int InvId);
        List<ImportLrDataModel> GetLRDataLst(int BranchId, int CompId, DateTime LRDate);
        PickLstSummaryData GetPickListSummaryCounts(PickLstSummaryData model);
        List<PickListModel> GetPickListForReAllotment(PickListModel model);
        InvCntModel InvoiceSummaryCounts(int BranchId, int CompId, DateTime InvDate);
        string PickListHeaderDelete(PickListModel model);
        List<InvSts> InvoiceStatusForMob();
        Responce GenerateGatepasAddEdit(GatePassModel model);
        List<PrinterDtls> GetPrinterDetails(int BranchId, int CompId);
        string PrinterLogAddEdit(PrinterLogAddEditModel model);
        string PrinterPDFData(PrintPDFDataModel model);
        List<PrintPDFDataModel> GetPrintPDFData(int BranchId, int CompId, string PrinterType);
        string GatepassListGenerateNewNo(int BranchId, int CompId, DateTime GatepassDate);
        List<InvGatpassDtls> GetGatepassDtlsForPDF(int BranchId, int CompId, int GPid);
        List<GatepassDtls> GetGatepassDtlsForMobile(int BranchId, int CompId);
        List<InvDtlsForMob> GetInvoiceDtlsForMobile(int BranchId, int CompId,int InvStatus);
        string GatepassDtlsForDeleteById(int GatepassId);
        string sendEmailForDispatchDone(string Emailid, string StockistName, string TransporterName,string CompanyName,int BranchId, int CompId);
        List<InvDtlsForEmail> GetInvDtlsForEmail(int BranchId, int CompId, int GatepassId);
        string PrintDetailsAdd(PrinterDtls model);
        List<PickListModel> GetPicklistByPickerStatus(int BranchId, int CompId, DateTime PicklistDate);
        string PriorityInvoiceFlagUpdate(PriorityFlagUpdtModel model);
        string ResolveConcernAdd(PickListModel model);
        List<PickListModel> ResolveConcernLst(int BranchId, int CompId, DateTime PicklistDate);
        List<InvoiceLstModel> GetInvoiceHeaderLstResolveCnrn(int BranchId, int CompId, int BillDrawerId);
        string ResolveInvConcernAdd(InvoiceLstModel model);
        List<AssignedTransportModel> GetAssignedTransporterList(int BranchId, int CompId);
        string EditAssignedTransportMode(AssignedTransportModel model);
        List<PrintPDFDataModel> GetPrintPDFDataWithPrinter(int BranchId, int CompId, int UtilityNo);
        string CheckInvNoExistMob(CheckInvNoExitModel model);
        string SaveAndPrint(SaveAndPrintModel model);
        InvoiceCountsModel AllInvoiceCounts(int BranchId, int CompId);
        int SaveScannedInvData(SaveScannedInvData model);
        OrderDispatchCountFordashModel OrderDispatchCounts(int BranchId, int CompId);
        List<DashOrderDispatchList> GetOrderDispatchFilterListNew(int BranchId, int CompId);
        List<OrderDispPLModelList> GetOrderDispLRFilterListNew(int BranchId, int CompId);
        List<DashOrderDispatchList> GetOrderDispatchCummInvList(int BranchId, int CompId);
        List<OrderDispPLModelList> GetOrderDispCummPLListNew(int BranchId, int CompId);
        List<OrderDispatchSmmryCnt> GetOrderDispatchSummaryCount(int BranchId, int CompId);
        List<GetUtilityNoNewModel> GetUtilityNoNew();
        List<OwnOrdrDispInvSmmryList> GetOwnerOrderDispDashInvSmmryList();
        List<OwnOrdrDispBoxesSmmryList> GetOwnerOrderDispDashBoxesSmmryList();
        PickLstSummaryData GetPickListSummaryCountsForStockTrans(PickLstSummaryData model);
        InvoiceCountsModel AllInvoiceCountsStkCount(int BranchId, int CompId);
        List<LRDetailsListForDash> GetLRDetailsListForDashNew(int BranchId, int CompId);
        List<DashOrderDispatchList> GetPrioPendingInvForDashNew(int BranchId, int CompId);
        List<GetColumnHeaderModel> GetColumnHeaderList(int BranchId, int CompId,string Importfor);
        List<transporterForMonthlyModel> GettransporterForMonthlyModelList(int CompId, int BranchId, DateTime? FromDate, DateTime? ToDate, int TransporterId,int CourierId,int TransportModeId);
        List<TransporterForMonthlyModels> GetTransporterForMonthlyModelSummary(int CompId, int BranchId, DateTime? FromDate1, DateTime? ToDate1);
        List<DashOrderDispatchListPrevMonth> GetOrderDispatchPrevMonthData(int BranchId, int CompId);
        List<DashOrderDispatchList> GetOrderDispatchFilterListForSticker(int BranchId, int CompId);
        List<GatepassListModal> GetGatepassList(int? BranchId, int? CompanyId, DateTime? fromDate, DateTime? toDate);
       int PrinterGatepassData(GatepassPrintModal model);
        List<InvoiceLstDltModel> GetInvoiceLstForDlt(int BranchId, int CompId, string InvNo);
        int DeleteInvoiceDetails(int InvId, int BranchId );
    }
}
 