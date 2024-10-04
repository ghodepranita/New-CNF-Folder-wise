using System;
using System.Collections.Generic;

namespace CNF.Business.Model.Account
{
    public class ExpenseRegister
    {
        public int ExpInvId { get; set; }
        public int BranchId { get; set; }
        public int InvTypeId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int TransId { get; set; }
        public string TransName { get; set; }
        public int CourierId { get; set; }
        public string CourierName { get; set; }
        public string BillFromName { get; set; }
        public string ExpInvNo { get; set; }
        public DateTime? InvDate { get; set; }
        public int CompId { get; set; }
        public string CompanyName { get; set; }
        public int ExpHeadId { get; set; }
        public string ExpHeadName { get; set; }
        public int NoOfBox { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string isGSTApply { get; set; }
        public int TaxableAmt { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public int TaxId { get; set; }
        public string GSTType { get; set; }
        public decimal TotalAmt { get; set; }
        public string IsReimbursable { get; set; }
        public decimal Balance { get; set; }
        public int ExpInvStatus { get; set; }
        public string ExpInvStatusText { get; set; }
        public int AddedBy { get; set; }
        public string Action { get; set; }
        public string ExpBillImagePdfName { get; set; }
        public string ImageName { get; set; }
        public string NewImgByte { get; set; }
        public string PdfName { get; set; }
        public string IsTDS { get; set; }
        public int TDSPer { get; set; }
        public string NewPDFEdit { get; set; }
        public int SGSTPer { get; set; }
        public int CGSTPer { get; set; }
    }

    public class ExpenseRegisterPayment
    {
        public int ExpPaymentId { get; set; }
        public int ExpInvId { get; set; }
        public string ExpInvNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal TDS { get; set; }
        public decimal PaymentAmt { get; set; }
        public int PayMode { get; set; }
        public string PaymentModeText { get; set; }
        public string UTRNo { get; set; }
        public string Remark { get; set; }
        public string AddedBy { get; set; }
        public string Action { get; set; }
    }

    #region Start - Reimbursment Invoice
    public class ReimbursementInvoiceAddEditModel
    {
        public int ReimId { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string InvNo { get; set; }
        public DateTime InvDate { get; set; }
        public string ExpInvIdstr { get; set; }
        public int ExpeInvNoId { get; set; }
        public string ExpeInvNo { get; set; }
        public int TaxableAmt { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double TotalAmt { get; set; }
        public int ExpeHeadId { get; set; }
        public int TDS { get; set; }
        public string ExpeHeadName { get; set; }
        public string Remark { get; set; }
        public string Addedby { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Action { get; set; }
        public double PaymentAmt { get; set; }
    }
    #endregion End - Reimbursment Invoice

    #region Start - Reimbursement Payment
    public class ReimbursementPaymentAddEditModel
    {
        public int ReimPaymentId { get; set; }
        public int ReimId { get; set; }
        public string InvNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public double TDS { get; set; }
        public string PaymentMode { get; set; }
        public double PaymentAmt { get; set; }
        public int PaymentModeId { get; set; }
        public string UTRNo { get; set; }
        public string Remark { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
    }
    #endregion End - Reimbursement Payment

    public class CommissionInv
    {
        public int ComInvId { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int InvType { get; set; }
        public string Description { get; set; }
        public int TaxId { get; set; }
        public int TaxableAmount { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal TotalAmt { get; set; }
        public string AddedBy { get; set; }
        public string Action { get; set; }
    }

    public class CommissionInvList
    {
        public int ComInvId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string InvNo { get; set; }
        public Nullable<System.DateTime> InvDate { get; set; }
        public Nullable<int> pkId { get; set; }
        public string InvType { get; set; }
        public string Discription { get; set; }
        public int TaxId { get; set; }
        public string GSTType { get; set; }
        public Nullable<int> TaxableAmt { get; set; }
        public Nullable<decimal> CGST { get; set; }
        public Nullable<decimal> SGST { get; set; }
        public Nullable<decimal> TotalAmt { get; set; }
        public string Addedby { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCity { get; set; }
        public string CityName { get; set; }
        public string CompanyAddress { get; set; }
        public Nullable<double> PaymentAmt { get; set; }
        public Nullable<decimal> SGSTPer { get; set; }
        public Nullable<decimal> CGSTPer { get; set; }
    }

    public class AddCommissionInvPay
    {
        public int ComInvPaymentId { get; set; }
        public int ComInvId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double TDSAmt { get; set; }
        public double PaymentAmt { get; set; }
        public int PaymentModeId { get; set; }
        public string UTRNo { get; set; }
        public string Remark { get; set; }
        public string Addedby { get; set; }
        public string Action { get; set; }
    }

    public class CommissionInvPayList
    {
        public int ComInvPaymentId { get; set; }
        public int ComInvId { get; set; }
        public string InvNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public double TDSAmt { get; set; }
        public double PaymentAmt { get; set; }
        public int PaymentModeId { get; set; }
        public string Remark { get; set; }
        public string PaymentModeText { get; set; }
        public string UTRNo { get; set; }
        
    }

    public class GatepassBillSummaryModel
    {
        public int ExpInvId { get; set; }
        public DateTime GPDate { get; set; }
        public int TransporterId { get; set; }
        public int TranspNoOfBox { get; set; }
        public int GPNoOfInv { get; set; }
        public int GPNoOfBox { get; set; }
        public int RatePerBox { get; set; }
        public int Amount { get; set; }
        public List<TranspExpInvLstModel> GpSummaryById { get; set; }
        public string dtctID { get; set; }
        public string CityName { get; set; }
        public int SoldTo_City { get; set; }
    }
    public class TranspExpInvLstModel
    {
        public int ExpInvDtlsId { get; set; }
        public DateTime GPDate { get; set; }
        public int GatepassId { get; set; }
        public string GatepassNo { get; set; }
        public int GPNoOfInv { get; set; }
        public int GPNoOfBox { get; set; }
        public int TranspNoOfBox { get; set; }
        public int RatePerBox { get; set; }
        public int Amount { get; set; }
        public string Remark { get; set; }
        public int DtlsStatus { get; set; }
        public string DtlsStatusText { get; set; }
        public string ResolveRemark { get; set; }
        public Boolean IsRaiseConcern = false;
        public long gpctId { get; set; }
    }
    public class CheckInvModel
    {
        public int ExpInvId { get; set; }
        public string ExpInvNo { get; set; }
        public int TransporterId { get; set; }
        public int CompId { get; set; }
        public List<VerifyDataLst> VerifyData { get; set; }
        public int AddedBy { get; set; }
        public int RetValue { get; set; }
    }
    public class VerifyDataLst
    {
        public int pkId { get; set; }
        public int GatepassId { get; set; }
        public int TransBillBox { get; set; }
        public int DtlsStatus { get; set; }
    }

    public class ExpRegLstModel
    {
        public int BranchId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int ExpInvId { get; set; }
        public string ExpInvNo { get; set; }
        public int InvTypeId { get; set; }
        public DateTime InvDate { get; set; }
        public int CompId { get; set; }
        public string CompanyName { get; set; }
        public int ExpHeadId { get; set; }
        public int NoOfBox { get; set; }
        public int TaxableAmt { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal Balance { get; set; }
        public string ExpInvStatusText { get; set; }
        public int TransId { get; set; }
        public string ParentTranspName { get; set; }
        public DateTime InvFromDt { get; set; }
        public DateTime InvToDt { get; set; }
        public int AddedBy { get; set; }
        public string BillFromName { get; set; }
    }

    public class ReimbursementInvByIdModel
    {
        public int ExpInvId { get; set; }
        public string ExpInvNo { get; set; }
        public DateTime InvDate { get; set; }
        public int NoOfBox { get; set; }
        public string BillFromName { get; set; }
        public int BranchId { get; set; }
        public DateTime InvFromDt { get; set; }
        public DateTime InvToDt { get; set; }
        public int TaxableAmt { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal TotalAmt { get; set; }
        public string IsReimbursable { get; set; }
        public int ExpInvStatus { get; set; }
        public int ReimId { get; set; }
        public decimal SGSTPer { get; set; }
        public decimal CGSTPer { get; set; }
    }

}
