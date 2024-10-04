using CNF.Business.BusinessConstant;
using CNF.Business.Model.ChequeAccounting;
using CNF.Business.Model.Context;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using static CNF.Business.Model.OrderReturn.OrderReturn;

namespace CNF.API.Controllers
{
    public class OrderReturnController : BaseApiController
    {
        #region Get New Generated Gatepass No
        [HttpPost]
        [Route("OrderReturn/GetNewGeneratedGatepassNo")]
        public string GetNewGeneratedGatepassNo(InwardGatepassModel model)
        {
            string gatepassNo = string.Empty;

            try
            {
                gatepassNo = _unitOfWork.OrderReturnRepository.GetNewGeneratedGatepassNo(model.BranchId, model.CompId, model.ReceiptDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListGenerateNewNo", "Get New Generated Gatepass No " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return gatepassNo;
        }
        #endregion

        #region Get New Generated LR No.
        [HttpPost]
        [Route("OrderReturn/GetNewGeneratedLRNo")]
        public string GetNewGeneratedLRNo(InwardGatepassModel model)
        {
            string newlrNo = string.Empty;
            try
            {
                newlrNo = _unitOfWork.OrderReturnRepository.GetNewGeneratedLRNo(model.BranchId, model.CompId, model.LREntryDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListGenerateNewNo", "Get New Generated Gatepass No " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return newlrNo;
        }
        #endregion

        #region LR Details Add Edit - for Mobile
        [HttpPost]
        [Route("OrderReturn/AddEditLRDetails")]
        public string AddEditLRDetails([FromBody] InwardGatepassModel model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.OrderReturnRepository.AddEditLRDetails(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditLRDetails", "Add Edit LR Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion       

        #region Generate Inward Gatepass - for Mobile
        [HttpPost]
        [Route("OrderReturn/GenerateInwardGatepass")]
        public string GenerateInwardGatepass([FromBody] InwardGatepassModel model)
        {
            string result = string.Empty, emailDtls = string.Empty;
            List<StokistDtlsModel> InwardGatepassDtls = new List<StokistDtlsModel>();
            try
            {
                result = _unitOfWork.OrderReturnRepository.GenerateInwardGatepass(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GenerateInwardGatepass", "Generate Inward Gatepass", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Inward Gatepass List - For Mobile
        [HttpPost]
        [Route("OrderReturn/GetInwardGatepassList")]
        public List<InwardGatepassModel> GetInwardGatepassList(InwardGatepassModel model)
        {
            List<InwardGatepassModel> GatepassLst = new List<InwardGatepassModel>();
            try
            {
                GatepassLst = _unitOfWork.OrderReturnRepository.GetInwardGatepassList(model.BranchId, model.CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInwardGatepassList", "Get Inward Gatepass List ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GatepassLst;
        }
        #endregion

        #region Get Inward Gatepass List For Edit- For Mobile
        [HttpPost]
        [Route("OrderReturn/GetInwardGatepassListForEdit")]
        public List<InwardGatepassModel> GetInwardGatepassListForEdit(InwardGatepassModel model)
        {
            List<InwardGatepassModel> GatepassLst = new List<InwardGatepassModel>();
            try
            {
                GatepassLst = _unitOfWork.OrderReturnRepository.GetInwardGatepassListForEdit(model.BranchId, model.CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInwardGatepassList", "Get Inward Gatepass List ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GatepassLst;
        }
        #endregion

        #region Edit Inward LR Details - for Mobile
        [HttpPost]
        [Route("OrderReturn/EditInwardLRDetails")]
        public int EditInwardLRDetails([FromBody] inwardLREditModel model)
        {
            int result = 0;
            try
            {
                result = _unitOfWork.OrderReturnRepository.EditInwardLRDetails(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "EditInwardLRDetails", "Edit Inward LR Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region 1st Physical Check Add Edit
        [HttpPost]
        [Route("OrderReturn/PhysicalCheckAddEdit")]
        public int PhysicalCheck1AddEdit([FromBody]PhysicalCheck1 model)
        {
            int physicalcheck = 0;
            try
            {
                physicalcheck = _unitOfWork.OrderReturnRepository.PhysicalCheck1AddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PhysicalCheckAddEdit", "Physical Check Add Edit ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return physicalcheck;
        }
        #endregion

        #region Physical check 1 List
        [HttpGet]
        [Route("OrderReturn/GetPhysicalCheck1List/{BranchId}/{CompId}")]
        public List<PhysicalCheck1ListModel> GetPhysicalCheck1List(int BranchId, int CompId)
        {
            List<PhysicalCheck1ListModel> PhysicalCheck1List = new List<PhysicalCheck1ListModel>();
            try
            {
                PhysicalCheck1List = _unitOfWork.OrderReturnRepository.GetPhysicalCheck1List(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPhysicalCheck1List", "Get Physical Check1 List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PhysicalCheck1List;
        }
        #endregion

        #region Physical check 1 List Concerns
        [HttpPost]
        [Route("OrderReturn/PhysicalCheck1Concern")]
        public int PhysicalCheck1Concern([FromBody] PhysicalCheck1 model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.OrderReturnRepository.PhysicalCheck1Concern(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPhysicalCheck1List", "Get Physical Check1 List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Auditor Check - Verify and Correction List
        [HttpGet]
        [Route("OrderReturn/GetSRSClaimListForVerifyList/{BranchId}/{CompId}")]
        public List<SRSClaimListForVerifyModel> GetSRSClaimListForVerifyList(int BranchId, int CompId)
        {
            List<SRSClaimListForVerifyModel> SRSClaimListForVerifyList = new List<SRSClaimListForVerifyModel>();
            try
            {
                SRSClaimListForVerifyList = _unitOfWork.OrderReturnRepository.GetSRSClaimListForVerifyList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetSRSClaimListForVerifyList", "Get Auditor Check - Verify and Correction List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return SRSClaimListForVerifyList;
        }
        #endregion

        #region Auditor Check - Verify and Correction Required(Remark)
        [HttpPost]
        [Route("OrderReturn/AuditorCheckCorrection")]
        public int AuditorCheckCorrection([FromBody] AuditorCheckCorrectionModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.OrderReturnRepository.AuditorCheckCorrection(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AuditorCheckCorrection", "Auditor Check - Verify and Correction Required(Remark)", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Import CN Data - BranchId, CompanyId and AddedBy
        [HttpPost]
        [Route("OrderReturn/ImportCNData/{BranchId}/{CompanyId}/{AddedBy}")]
        public string ImportCNData(int BranchId, int CompanyId, string AddedBy)
        {
            ImportCNData message = new ImportCNData();

            bool IsColumnFlag = false;
            bool InsertFlag = false;

            string columnErrorMessage_SalesOrderNo = string.Empty, columnErrorMessage_SalesOrderDate = string.Empty,
                   columnErrorMessage_CrDrNoteNo = string.Empty, columnErrorMessage_CRDRCreationDate = string.Empty,
                   columnErrorMessage_CrDrAmt = string.Empty, columnErrorMessage_SoldToCode = string.Empty,
                   columnErrorMessage_SoldToName = string.Empty, columnErrorMessage_SoldToCity = string.Empty,
                   columnErrorMessage_OrderReason = string.Empty, columnErrorMessage_OrderReasonDesc = string.Empty,
                   columnErrorMessage_LRNo = string.Empty, columnErrorMessage_LRDate = string.Empty,
                   columnErrorMessage_CFAGRDate = string.Empty, columnErrorMessage_BasicAmt = string.Empty;

            bool columnErrorFlag_SalesOrderNo = false, columnErrorFlag_SalesOrderDate = false, columnErrorFlag_CrDrNoteNo = false,
                 columnErrorFlag_CRDRCreationDate = false, columnErrorFlag_CrDrAmt = false, columnErrorFlag_SoldToCode = false,
                 columnErrorFlag_SoldToName = false, columnErrorFlag_SoldToCity = false,
                 columnErrorFlag_OrderReason = false, columnErrorFlag_OrderReasonDesc = false, columnErrorFlag_LRNo = false,
                 columnErrorFlag_LRDate = false, columnErrorFlag_CFAGRDate = false, columnErrorFlag_BasicAmt = false, columnErrorFlag_isAlphanumeric = false;

            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportCNTemplate.xls" || file.FileName == "ImportCNTemplate.XLS" || file.FileName == "ImportCNTemplate.xlsx" || file.FileName == "ImportCNTemplate.XLSX")
                    {
                        if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".XLS"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (file.FileName.EndsWith(".xlsx") || file.FileName.EndsWith(".XLSX"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else
                        {
                            message.RetResult = "This file format is not supported";
                        }

                        DataSet excelRecords = reader.AsDataSet();
                        reader.Close();

                        var finalRecords = excelRecords.Tables[0];
                        if (finalRecords.Rows.Count > 0)
                        {
                            List<ImportCNData> modelList = new List<ImportCNData>();
                            ImportCNData model;
                            IsColumnFlag = IsColumnValidationForCN(finalRecords); // To Check Column Name
                            if (IsColumnFlag)
                            {
                                for (int j = 1; j < finalRecords.Rows.Count; j++)  // To Row Values Append
                                {
                                    model = new ImportCNData();
                                    model.pkId = j;

                                    InsertFlag = true;

                                    // Sales Order No
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][0].ToString(), j, ref InsertFlag, ref columnErrorFlag_SalesOrderNo, ref columnErrorMessage_SalesOrderNo, columnErrorFlag_isAlphanumeric);
                                    // Sales Order Date
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][1].ToString(), j, ref InsertFlag, ref columnErrorFlag_SalesOrderDate, ref columnErrorMessage_SalesOrderDate, columnErrorFlag_isAlphanumeric);
                                    // Cr/Dr Note No
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][2].ToString(), j, ref InsertFlag, ref columnErrorFlag_CrDrNoteNo, ref columnErrorMessage_CrDrNoteNo, columnErrorFlag_isAlphanumeric);
                                    // CR/DR Creation Date
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][3].ToString(), j, ref InsertFlag, ref columnErrorFlag_CRDRCreationDate, ref columnErrorMessage_CRDRCreationDate, columnErrorFlag_isAlphanumeric);
                                    // Cr/Dr Amt.
                                    BusinessCont.TypeCheckWithData("Decimal", finalRecords.Rows[j][4].ToString(), j, ref InsertFlag, ref columnErrorFlag_CrDrAmt, ref columnErrorMessage_CrDrAmt, columnErrorFlag_isAlphanumeric);
                                    // Sold To Code
                                    BusinessCont.TypeCheckWithData("Alphanumeric", finalRecords.Rows[j][5].ToString(), j, ref InsertFlag, ref columnErrorFlag_SoldToCode, ref columnErrorMessage_SoldToCode, columnErrorFlag_isAlphanumeric);
                                    // Sold To Name
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][6].ToString(), j, ref InsertFlag, ref columnErrorFlag_SoldToName, ref columnErrorMessage_SoldToName, columnErrorFlag_isAlphanumeric);
                                    // Sold to City
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][7].ToString(), j, ref InsertFlag, ref columnErrorFlag_SoldToCity, ref columnErrorMessage_SoldToCity, columnErrorFlag_isAlphanumeric);
                                    // Order Reason
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][8].ToString(), j, ref InsertFlag, ref columnErrorFlag_OrderReason, ref columnErrorMessage_OrderReason, columnErrorFlag_isAlphanumeric);
                                    // Order Reason Description
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][9].ToString(), j, ref InsertFlag, ref columnErrorFlag_OrderReasonDesc, ref columnErrorMessage_OrderReasonDesc, columnErrorFlag_isAlphanumeric);
                                    // LR No.
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][10].ToString(), j, ref InsertFlag, ref columnErrorFlag_LRNo, ref columnErrorMessage_LRNo, columnErrorFlag_isAlphanumeric);
                                    // LR Date
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][11].ToString(), j, ref InsertFlag, ref columnErrorFlag_LRDate, ref columnErrorMessage_LRDate, columnErrorFlag_isAlphanumeric);
                                    // CFA GR date
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][12].ToString(), j, ref InsertFlag, ref columnErrorFlag_CFAGRDate, ref columnErrorMessage_CFAGRDate, columnErrorFlag_isAlphanumeric);
                                    // Basic Amount
                                    BusinessCont.TypeCheckWithData("Decimal", finalRecords.Rows[j][13].ToString(), j, ref InsertFlag, ref columnErrorFlag_BasicAmt, ref columnErrorMessage_BasicAmt, columnErrorFlag_isAlphanumeric);

                                    // Valid Data - Insert Flag = true
                                    if (InsertFlag)
                                    {
                                        model.SalesOrderNo = Convert.ToString(finalRecords.Rows[j][0]);
                                        model.SalesOrderDate = Convert.ToDateTime(finalRecords.Rows[j][1]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.CrDrNoteNo = Convert.ToString(finalRecords.Rows[j][2]);
                                        model.CRDRCreationDate = Convert.ToDateTime(finalRecords.Rows[j][3]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.CrDrAmt = Convert.ToDecimal(finalRecords.Rows[j][4]);
                                        model.SoldToCode = Convert.ToString(finalRecords.Rows[j][5]);
                                        model.SoldToName = Convert.ToString(finalRecords.Rows[j][6]);
                                        model.SoldToCity = Convert.ToString(finalRecords.Rows[j][7]);
                                        model.OrderReason = Convert.ToString(finalRecords.Rows[j][8]);
                                        model.OrderReasonDescription = Convert.ToString(finalRecords.Rows[j][9]);
                                        model.LRNo = Convert.ToString(finalRecords.Rows[j][10]);
                                        model.LRDate = Convert.ToDateTime(finalRecords.Rows[j][11]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.CFAGRDate = Convert.ToDateTime(finalRecords.Rows[j][12]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.BasicAmt = Convert.ToDecimal(finalRecords.Rows[j][13]);
                                        model.AddedBy = Convert.ToInt32(AddedBy);
                                        model.AddedOn = Convert.ToDateTime(DateTime.Now).ToString("yyyy/MM/dd hh:mm:ss");
                                        modelList.Add(model);
                                    }
                                }

                                // To Check Column Error Flag Maintain Column Wise
                                if (columnErrorFlag_SalesOrderNo || columnErrorFlag_SalesOrderDate || columnErrorFlag_CrDrNoteNo ||
                                    columnErrorFlag_CRDRCreationDate || columnErrorFlag_CrDrAmt || columnErrorFlag_SoldToCode ||
                                    columnErrorFlag_SoldToName || columnErrorFlag_SoldToCity ||
                                    columnErrorFlag_OrderReason || columnErrorFlag_OrderReasonDesc || columnErrorFlag_LRNo ||
                                    columnErrorFlag_LRDate || columnErrorFlag_CFAGRDate || columnErrorFlag_BasicAmt
                                    )
                                {
                                    message.RetResult = "\n Below Columns has invalid data:  ";
                                    if (columnErrorFlag_SalesOrderNo)
                                    {
                                        message.RetResult += "\n Sales Order No ---- \n " + columnErrorMessage_SalesOrderNo;
                                    }
                                    if (columnErrorFlag_SalesOrderDate)
                                    {
                                        message.RetResult += "\n Sales Order Date ---- \n " + columnErrorMessage_SalesOrderDate;
                                    }
                                    if (columnErrorFlag_CrDrNoteNo)
                                    {
                                        message.RetResult += "\n Cr/Dr Note No ---- \n " + columnErrorMessage_CrDrNoteNo;
                                    }
                                    if (columnErrorFlag_CRDRCreationDate)
                                    {
                                        message.RetResult += "\n CR/DR Creation Date ---- \n" + columnErrorMessage_CRDRCreationDate;
                                    }
                                    if (columnErrorFlag_CrDrAmt)
                                    {
                                        message.RetResult += "\n Cr/Dr Amt. ---- \n " + columnErrorMessage_CrDrAmt;
                                    }
                                    if (columnErrorFlag_SoldToCode)
                                    {
                                        message.RetResult += "\n Sold To Code ---- \n " + columnErrorMessage_SoldToCode;
                                    }
                                    if (columnErrorFlag_SoldToName)
                                    {
                                        message.RetResult += "\n Sold To Name ---- \n " + columnErrorMessage_SoldToName;
                                    }
                                    if (columnErrorFlag_SoldToCity)
                                    {
                                        message.RetResult += "\n Sold to City ---- \n " + columnErrorMessage_SoldToCity;
                                    }
                                    if (columnErrorFlag_OrderReason)
                                    {
                                        message.RetResult += "\n Order Reason ---- \n " + columnErrorMessage_OrderReason;
                                    }
                                    if (columnErrorFlag_OrderReasonDesc)
                                    {
                                        message.RetResult += "\n Order Reason Description ---- \n " + columnErrorMessage_OrderReasonDesc;
                                    }
                                    if (columnErrorFlag_LRNo)
                                    {
                                        message.RetResult += "\n LR No. ---- \n " + columnErrorMessage_LRNo;
                                    }
                                    if (columnErrorFlag_LRDate)
                                    {
                                        message.RetResult += "\n LR Date ---- \n " + columnErrorMessage_LRDate;
                                    }
                                    if (columnErrorFlag_CFAGRDate)
                                    {
                                        message.RetResult += "\n CFA GR date ---- \n " + columnErrorMessage_CFAGRDate;
                                    }
                                    if (columnErrorFlag_BasicAmt)
                                    {
                                        message.RetResult += "\n Item Catagory Description ---- \n " + columnErrorMessage_BasicAmt;
                                    }
                                }

                                if (modelList.Count > 0)
                                {
                                    // Create DataTable
                                    DataTable dt = new DataTable();
                                    dt.Columns.Add("pkId");
                                    dt.Columns.Add("SalesOrderNo");
                                    dt.Columns.Add("SalesOrderDate");
                                    dt.Columns.Add("CrDrNoteNo");
                                    dt.Columns.Add("CRDRCreationDate");
                                    dt.Columns.Add("CrDrAmt");
                                    dt.Columns.Add("SoldToCode");
                                    dt.Columns.Add("SoldToName");
                                    dt.Columns.Add("SoldToCity");
                                    dt.Columns.Add("OrderReason");
                                    dt.Columns.Add("OrderReasonDescription");
                                    dt.Columns.Add("LRNo");
                                    dt.Columns.Add("LRDate");
                                    dt.Columns.Add("CFAGRDate");
                                    dt.Columns.Add("BasicAmt");
                                    dt.Columns.Add("AddedBy");
                                    dt.Columns.Add("AddedOn");

                                    foreach (var itemList in modelList)
                                    {
                                        // Add Rows DataTable
                                        dt.Rows.Add(itemList.pkId, itemList.SalesOrderNo, itemList.SalesOrderDate,
                                                    itemList.CrDrNoteNo, itemList.CRDRCreationDate, itemList.CrDrAmt, itemList.SoldToCode, itemList.SoldToName, itemList.SoldToCity,
                                                    itemList.OrderReason, itemList.OrderReasonDescription, itemList.LRNo,
                                                    itemList.LRDate, itemList.CFAGRDate, itemList.BasicAmt, itemList.AddedBy, itemList.AddedOn);
                                    }

                                    if (dt.Rows.Count > 0)
                                    {
                                        using (var db = new CFADBEntities())
                                        {
                                            {
                                                SqlConnection connection = (SqlConnection)db.Database.Connection;
                                                SqlCommand cmd = new SqlCommand("CFA.usp_ImportCNData", connection);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                                BranchIdParameter.SqlDbType = SqlDbType.Int;
                                                SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                                CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                                SqlParameter ImportInvDataParameter = cmd.Parameters.AddWithValue("@ImportCNData", dt);
                                                ImportInvDataParameter.SqlDbType = SqlDbType.Structured;
                                                SqlParameter AddedbyParameter = cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                                                AddedbyParameter.SqlDbType = SqlDbType.NVarChar;
                                                if (connection.State == ConnectionState.Closed)
                                                    connection.Open();
                                                SqlDataReader dr = cmd.ExecuteReader();
                                                while (dr.Read())
                                                {
                                                    message.RetResult = (string)dr["RetResult"] + message.RetResult;
                                                }
                                                if (connection.State == ConnectionState.Open)
                                                    connection.Close();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        message.RetResult = BusinessCont.msg_NoRecordFound;
                                    }
                                }
                            }
                        }
                        else
                        {
                            message.RetResult = BusinessCont.msg_NoRecordFoundExcelFile;
                        }
                    }
                    else
                    {
                        message.RetResult = BusinessCont.msg_InvalidExcelFile;
                    }
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportCNData", "Import CN Data - BranchId, CompanyId and AddedBy:  " + AddedBy, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        /// <summary>
        /// Is Column Validation CN
        /// </summary>
        /// <param name="finalRecords"></param>
        /// <returns></returns>
        private bool IsColumnValidationForCN(DataTable finalRecords)
        {
            bool IsValid = false;
            try
            {
                if (finalRecords.Rows.Count > 0)
                {
                    string SalesOrderNo = Convert.ToString(finalRecords.Rows[0][0]);
                    string SalesOrderDate = Convert.ToString(finalRecords.Rows[0][1]);
                    string CrDrNoteNo = Convert.ToString(finalRecords.Rows[0][2]);
                    string CRDRCreationDate = Convert.ToString(finalRecords.Rows[0][3]);
                    string CrDrAmt = Convert.ToString(finalRecords.Rows[0][4]);
                    string SoldToCode = Convert.ToString(finalRecords.Rows[0][5]);
                    string SoldToName = Convert.ToString(finalRecords.Rows[0][6]);
                    string SoldToCity = Convert.ToString(finalRecords.Rows[0][7]);
                    string OrderReason = Convert.ToString(finalRecords.Rows[0][8]);
                    string OrderReasonDescription = Convert.ToString(finalRecords.Rows[0][9]);
                    string LRNo = Convert.ToString(finalRecords.Rows[0][10]);
                    string LRDate = Convert.ToString(finalRecords.Rows[0][11]);
                    string CFAGRDate = Convert.ToString(finalRecords.Rows[0][12]);
                    string BasicAmt = Convert.ToString(finalRecords.Rows[0][13]);

                    if (SalesOrderNo == "Sales Order No" && SalesOrderDate == "Sales Order Date" && CrDrNoteNo == "Cr/Dr Note No" &&
                        CRDRCreationDate == "CR/DR Creation Date" && CrDrAmt == "Cr/Dr Amt." && SoldToCode == "Sold To Code" &&
                        SoldToName == "Sold To Name" && SoldToCity == "Sold to City" && OrderReason == "Order Reason" &&
                        OrderReasonDescription == "Order Reason Description" && LRNo == "LR No." && LRDate == "LR Date" &&
                        BasicAmt == "Basic Amt")
                    {
                        IsValid = true;
                    }
                    else
                    {
                        IsValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "IsColumnValidationForCN", "Is Column Validation For CN", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return IsValid;
        }
        #endregion

        #region Import CN Data - BranchId, CompanyId and Addedby New
        [HttpPost]
        [Route("OrderReturn/ImportCNDataNew/{BranchId}/{CompanyId}/{AddedBy}")]
        public string ImportCNDataNew(int BranchId, int CompanyId, string AddedBy)
        {
            ImportCNData message = new ImportCNData();

            bool IsColumnFlag = false;
            bool InsertFlag = false;
            string columnErrorMessage_SalesOrderNo = string.Empty, columnErrorMessage_SalesOrderDate = string.Empty,
                           columnErrorMessage_CrDrNoteNo = string.Empty, columnErrorMessage_CRDRCreationDate = string.Empty,
                           columnErrorMessage_CrDrAmt = string.Empty, columnErrorMessage_SoldToCode = string.Empty,
                           columnErrorMessage_SoldToName = string.Empty, columnErrorMessage_SoldToCity = string.Empty,
                           columnErrorMessage_OrderReason = string.Empty, columnErrorMessage_OrderReasonDesc = string.Empty,
                           columnErrorMessage_LRNo = string.Empty, columnErrorMessage_LRDate = string.Empty,
                           columnErrorMessage_CFAGRDate = string.Empty, columnErrorMessage_BasicAmt = string.Empty;

            bool columnErrorFlag_SalesOrderNo = false, columnErrorFlag_SalesOrderDate = false, columnErrorFlag_CrDrNoteNo = false,
                 columnErrorFlag_CRDRCreationDate = false, columnErrorFlag_CrDrAmt = false, columnErrorFlag_SoldToCode = false,
                 columnErrorFlag_SoldToName = false, columnErrorFlag_SoldToCity = false,
                 columnErrorFlag_OrderReason = false, columnErrorFlag_OrderReasonDesc = false, columnErrorFlag_LRNo = false,
                 columnErrorFlag_LRDate = false, columnErrorFlag_CFAGRDate = false, columnErrorFlag_BasicAmt = false, columnErrorFlag_isAlphanumeric = false;

            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var ImportFor = "Import CN";
                    var columnHeaderList = _unitOfWork.OrderDispatchRepository.GetColumnHeaderList(BranchId, CompanyId, ImportFor);
                    if (columnHeaderList.Count == 0)
                    {
                        message.RetResult = "Master data is not configured, Please contact to system admin";
                        return message.RetResult;
                    }
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportCNTemplate.xls" || file.FileName == "ImportCNTemplate.XLS" || file.FileName == "ImportCNTemplate.xlsx" || file.FileName == "ImportCNTemplate.XLSX")
                    {
                        if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".XLS"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (file.FileName.EndsWith(".xlsx") || file.FileName.EndsWith(".XLSX"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else
                        {
                            message.RetResult = "This file format is not supported";
                        }

                        DataSet excelRecords = reader.AsDataSet();
                        reader.Close();

                        var finalRecords = excelRecords.Tables[0];
                        if (finalRecords.Rows.Count > 0)
                        {
                            List<ImportCNData> modelList = new List<ImportCNData>();
                            ImportCNData model;

                            // Create a dictionary to map Excel column names to their corresponding database column names and data types
                            Dictionary<int, Tuple<string, string>> columnIndexMapping = new Dictionary<int, Tuple<string, string>>();

                            // the columnIndexMapping based on the Excel to DB column mapping from columnHeaderList
                            foreach (var columnHeader in columnHeaderList)
                            {
                                bool matchFound = false;
                                for (int columnIndex = 0; columnIndex < finalRecords.Columns.Count; columnIndex++)
                                {
                                    string excelColumnName = finalRecords.Rows[0][columnIndex].ToString().Trim();
                                    if (excelColumnName == columnHeader.ExcelColName.TrimEnd('\r', '\n'))
                                    {
                                        // Map the Excel column index to its corresponding database column name
                                        columnIndexMapping[columnIndex] = Tuple.Create(columnHeader.FieldName, columnHeader.ColumnDatatype);
                                        matchFound = true;
                                        break;
                                    }
                                }
                                if (!matchFound)
                                {
                                    // Handle column name mismatch                          
                                    message.RetResult += $"Invalid Excel - {columnHeader.ExcelColName} mismatch.";
                                    return message.RetResult;
                                }
                                matchFound = false;
                            }
                            for (int j = 1; j < finalRecords.Rows.Count; j++)
                            {
                                model = new ImportCNData();
                                model.pkId = j;
                                InsertFlag = true;
                                // Iterate through the columns dynamically using the columnIndexMapping
                                foreach (var columnIndex in columnIndexMapping.Keys)
                                {
                                    string dbColumnName = columnIndexMapping[columnIndex].Item1;
                                    string columnDataType = columnIndexMapping[columnIndex].Item2;
                                    string cellValue = finalRecords.Rows[j][columnIndex].ToString();
                                    // Sales Order No
                                    if (dbColumnName == "SalesOrderNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_SalesOrderNo, ref columnErrorMessage_SalesOrderNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Sales Order Date
                                    if (dbColumnName == "SalesOrderDate")
                                    {
                                        BusinessCont.TypeCheckWithData("String", cellValue, j, ref InsertFlag, ref columnErrorFlag_SalesOrderDate, ref columnErrorMessage_SalesOrderDate, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Cr Dr Note No
                                    if (dbColumnName == "CrDrNoteNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_CrDrNoteNo, ref columnErrorMessage_CrDrNoteNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // CR DR Creation Date
                                    if (dbColumnName == "CRDRCreationDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_CRDRCreationDate, ref columnErrorMessage_CRDRCreationDate, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Cr Dr Amt
                                    if (dbColumnName == "CrDrAmt")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_CrDrAmt, ref columnErrorMessage_CrDrAmt, columnErrorFlag_isAlphanumeric);
                                    }
                                    // SoldToCode
                                    if (dbColumnName == "SoldToCode")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_SoldToCode, ref columnErrorMessage_SoldToCode, columnErrorFlag_isAlphanumeric);
                                    }
                                    // SoldToName
                                    if (dbColumnName == "SoldToName")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_SoldToName, ref columnErrorMessage_SoldToName, columnErrorFlag_isAlphanumeric);
                                    }
                                    // SoldToCity
                                    if (dbColumnName == "SoldToCity")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_SoldToCity, ref columnErrorMessage_SoldToCity, columnErrorFlag_isAlphanumeric);
                                    }
                                    // OrderReason
                                    if (dbColumnName == "OrderReason")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_OrderReason, ref columnErrorMessage_OrderReason, columnErrorFlag_isAlphanumeric);
                                    }

                                    // OrderReasonDesc
                                    if (dbColumnName == "OrderReasonDesc")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_OrderReasonDesc, ref columnErrorMessage_OrderReasonDesc, columnErrorFlag_isAlphanumeric);
                                    }

                                    // LRNo
                                    if (dbColumnName == "LRNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_LRNo, ref columnErrorMessage_LRNo, columnErrorFlag_isAlphanumeric);
                                    }

                                    // LRDate
                                    if (dbColumnName == "LRDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_LRDate, ref columnErrorMessage_LRDate, columnErrorFlag_isAlphanumeric);
                                    }

                                    // CFAGRDate
                                    if (dbColumnName == "CFAGRDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_CFAGRDate, ref columnErrorMessage_CFAGRDate, columnErrorFlag_isAlphanumeric);
                                    }
                                    // BasicAmt
                                    if (dbColumnName == "BasicAmt")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_BasicAmt, ref columnErrorMessage_BasicAmt, columnErrorFlag_isAlphanumeric);
                                    }

                                    // Assign values to model properties dynamically based on dbColumnName
                                    switch (dbColumnName)
                                    {
                                        case "SalesOrderNo":
                                            model.SalesOrderNo = cellValue;
                                            break;
                                        case "SalesOrderDate":
                                            model.SalesOrderDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "CrDrNoteNo":
                                            model.CrDrNoteNo = cellValue;
                                            break;
                                        case "CRDRCreationDate":
                                            model.CRDRCreationDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "SoldToCode":
                                            model.SoldToCode = cellValue;
                                            break;
                                        case "SoldToName":
                                            model.SoldToName = cellValue;
                                            break;
                                        case "SoldToCity":
                                            model.SoldToCity = cellValue;
                                            break;
                                        case "OrderReason":
                                            model.OrderReason = cellValue;
                                            break;
                                        case "OrderReasonDesc":
                                            model.OrderReasonDescription = cellValue;
                                            break;
                                        case "LRNo":
                                            model.LRNo = cellValue;
                                            break;
                                        case "LRDate":
                                            model.LRDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "CFAGRDate":
                                            model.CFAGRDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "BasicAmt":
                                            model.BasicAmt = Convert.ToInt32(cellValue);
                                            break;
                                    }
                                }

                                // Valid Data -Insert Flag = true
                                if (InsertFlag)
                                {
                                    modelList.Add(model);
                                }
                            }

                            // To Check Column Error Flag Maintain Column Wise
                            if (columnErrorFlag_SalesOrderNo || columnErrorFlag_SalesOrderDate || columnErrorFlag_CrDrNoteNo ||
                                    columnErrorFlag_CRDRCreationDate || columnErrorFlag_CrDrAmt || columnErrorFlag_SoldToCode ||
                                    columnErrorFlag_SoldToName || columnErrorFlag_SoldToCity ||
                                    columnErrorFlag_OrderReason || columnErrorFlag_OrderReasonDesc || columnErrorFlag_LRNo ||
                                    columnErrorFlag_LRDate || columnErrorFlag_CFAGRDate || columnErrorFlag_BasicAmt)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_SalesOrderNo)
                                {
                                    message.RetResult += "\n Sales Order No ---- \n " + columnErrorMessage_SalesOrderNo;
                                }
                                if (columnErrorFlag_SalesOrderDate)
                                {
                                    message.RetResult += "\n Sales Order Date ---- \n " + columnErrorMessage_SalesOrderDate;
                                }
                                if (columnErrorFlag_CrDrNoteNo)
                                {
                                    message.RetResult += "\n Cr/Dr Note No ---- \n " + columnErrorMessage_CrDrNoteNo;
                                }
                                if (columnErrorFlag_CRDRCreationDate)
                                {
                                    message.RetResult += "\n CR/DR Creation Date ---- \n" + columnErrorMessage_CRDRCreationDate;
                                }
                                if (columnErrorFlag_CrDrAmt)
                                {
                                    message.RetResult += "\n Cr/Dr Amt. ---- \n " + columnErrorMessage_CrDrAmt;
                                }
                                if (columnErrorFlag_SoldToCode)
                                {
                                    message.RetResult += "\n Sold To Code ---- \n " + columnErrorMessage_SoldToCode;
                                }
                                if (columnErrorFlag_SoldToName)
                                {
                                    message.RetResult += "\n Sold To Name ---- \n " + columnErrorMessage_SoldToName;
                                }
                                if (columnErrorFlag_SoldToCity)
                                {
                                    message.RetResult += "\n Sold to City ---- \n " + columnErrorMessage_SoldToCity;
                                }
                                if (columnErrorFlag_OrderReason)
                                {
                                    message.RetResult += "\n Order Reason ---- \n " + columnErrorMessage_OrderReason;
                                }
                                if (columnErrorFlag_OrderReasonDesc)
                                {
                                    message.RetResult += "\n Order Reason Description ---- \n " + columnErrorMessage_OrderReasonDesc;
                                }
                                if (columnErrorFlag_LRNo)
                                {
                                    message.RetResult += "\n LR No. ---- \n " + columnErrorMessage_LRNo;
                                }
                                if (columnErrorFlag_LRDate)
                                {
                                    message.RetResult += "\n LR Date ---- \n " + columnErrorMessage_LRDate;
                                }
                                if (columnErrorFlag_CFAGRDate)
                                {
                                    message.RetResult += "\n CFA GR date ---- \n " + columnErrorMessage_CFAGRDate;
                                }
                                if (columnErrorFlag_BasicAmt)
                                {
                                    message.RetResult += "\n Item Catagory Description ---- \n " + columnErrorMessage_BasicAmt;
                                }
                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("SalesOrderNo");
                                dt.Columns.Add("SalesOrderDate");
                                dt.Columns.Add("CrDrNoteNo");
                                dt.Columns.Add("CRDRCreationDate");
                                dt.Columns.Add("CrDrAmt");
                                dt.Columns.Add("SoldToCode");
                                dt.Columns.Add("SoldToName");
                                dt.Columns.Add("SoldToCity");
                                dt.Columns.Add("OrderReason");
                                dt.Columns.Add("OrderReasonDescription");
                                dt.Columns.Add("LRNo");
                                dt.Columns.Add("LRDate");
                                dt.Columns.Add("CFAGRDate");
                                dt.Columns.Add("BasicAmt");
                                dt.Columns.Add("AddedBy");
                                dt.Columns.Add("AddedOn");

                                foreach (var itemList in modelList)
                                {
                                    // Add Rows DataTable
                                    dt.Rows.Add(itemList.pkId, itemList.SalesOrderNo, itemList.SalesOrderDate,
                                                    itemList.CrDrNoteNo, itemList.CRDRCreationDate, itemList.CrDrAmt, itemList.SoldToCode, itemList.SoldToName, itemList.SoldToCity,
                                                    itemList.OrderReason, itemList.OrderReasonDescription, itemList.LRNo,
                                                    itemList.LRDate, itemList.CFAGRDate, itemList.BasicAmt, itemList.AddedBy, itemList.AddedOn);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    using (var db = new CFADBEntities())
                                    {
                                        {
                                            SqlConnection connection = (SqlConnection)db.Database.Connection;
                                            SqlCommand cmd = new SqlCommand("CFA.usp_ImportCNData", connection);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                            BranchIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                            CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter ImportInvDataParameter = cmd.Parameters.AddWithValue("@ImportCNData", dt);
                                            ImportInvDataParameter.SqlDbType = SqlDbType.Structured;
                                            SqlParameter AddedbyParameter = cmd.Parameters.AddWithValue("@AddedBy", AddedBy);
                                            AddedbyParameter.SqlDbType = SqlDbType.NVarChar;
                                            if (connection.State == ConnectionState.Closed)
                                                connection.Open();
                                            SqlDataReader dr = cmd.ExecuteReader();
                                            while (dr.Read())
                                            {
                                                message.RetResult = (string)dr["RetResult"] + message.RetResult;
                                            }
                                            if (connection.State == ConnectionState.Open)
                                                connection.Close();
                                        }
                                    }
                                }
                                else
                                {
                                    message.RetResult = BusinessCont.msg_NoRecordFound;
                                }
                            }
                        }
                        else
                        {
                            message.RetResult = BusinessCont.msg_NoRecordFoundExcelFile;
                        }
                    }
                    else
                    {
                        message.RetResult = BusinessCont.msg_InvalidExcelFile;
                    }
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportCNData", "Import CN Data - BranchId, CompanyId and AddedBy:  " + AddedBy, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        #endregion

        #region Get CN Data List
        [HttpGet]
        [Route("OrderReturn/GetImportCNDataList/{BranchId}/{CompId}")]
        public List<ImportCNDataList> GetImportCNDataList(int BranchId, int CompId)
        {
            List<ImportCNDataList> ImportCNDataList = new List<ImportCNDataList>();
            try
            {
                ImportCNDataList = _unitOfWork.OrderReturnRepository.GetImportCNDataList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetImportCNDataList", "Get Import CN Data List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ImportCNDataList;
        }
        #endregion

        #region Get Import CN Data For Email
        [HttpGet]
        [Route("OrderReturn/GetImportCNDataForEmail/{BranchId}/{CompId}")]
        public List<CNDataForEmail> GetImportCNDataForEmail(int BranchId, int CompId)
        {
            List<CNDataForEmail> ImportCNDataList = new List<CNDataForEmail>();
            try
            {
                ImportCNDataList = _unitOfWork.OrderReturnRepository.GetImportCNDataForEmail(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetImportCNDataForEmail", "Get Import CN Data For Email", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ImportCNDataList;
        }
        #endregion

        #region Add/Edit Claim - SRS Mapping
        [HttpPost]
        [Route("OrderReturn/ClaimSRSMappingAddEdit")]
        public int ClaimSRSMappingAddEdit([FromBody] AddSRSMapping model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.OrderReturnRepository.ClaimSRSMappingAddEdits(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ClaimSRSMappingAddEdit", "Claim SRS Mapping AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Claim - SRS Mapping List
        [HttpGet]
        [Route("OrderReturn/ClaimSRSMappingList/{BranchId}/{CompId}/{SRSId}")]
        public List<AddClaimSRSMappingModel> ClaimSRSMappingList(int BranchId, int CompId, int SRSId)
        {
            List<AddClaimSRSMappingModel> ClaimSRSList = new List<AddClaimSRSMappingModel>();
            try
            {
                ClaimSRSList = _unitOfWork.OrderReturnRepository.GetClaimSRSMappingLists(BranchId, CompId, SRSId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ClaimSRSMappingList", "Claim SRS Mapping List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return ClaimSRSList;
        }
        #endregion

        #region Get ClaimNo List
        [HttpGet]
        [Route("OrderReturn/GetClaimNoList/{BranchId}/{CompId}")]
        public List<GetClaimNoListModel> getClaimNolist(int BranchId, int CompId)
        {
            List<GetClaimNoListModel> getClaimNolist = new List<GetClaimNoListModel>();
            try
            {
                getClaimNolist = _unitOfWork.OrderReturnRepository.GetClaimNoLists(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "getClaimNolist", "Get ClaimNo list", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return getClaimNolist;
        }
        #endregion

        #region Get SRS Claim Mapped List
        [HttpGet]
        [Route("OrderReturn/GetSRSClaimMappedList/{BranchId}/{CompId}")]
        public List<AddClaimSRSMappingModel> GetSRSClaimMappedList(int BranchId, int CompId)
        {
            List<AddClaimSRSMappingModel> Claimappedlist = new List<AddClaimSRSMappingModel>();
            try
            {
                Claimappedlist = _unitOfWork.OrderReturnRepository.GetSRSClaimMappedLists(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetSRSClaimMappedList", "Get SRS Claim Mapped List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return Claimappedlist;
        }
        #endregion

        #region Add Delay Reason Of Pending CN
        [HttpPost]
        [Route("OrderReturn/AddDelayReasonOfPendingCN")]
        public int AddDelayReasonOfPendingCN([FromBody] UpdateCNDelayReason model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.OrderReturnRepository.UpdateCNDelayReason(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddDelayReasonOfPendingCN", "Add Delay Reason Of Pending CN", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Import SRS Data - BranchId, CompanyId and Addedby
        [HttpPost]
        [Route("OrderReturn/ImportSRSData/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportSRSData(int BranchId, int CompanyId, string AddedBy)
        {
            ImportReturnSRSModel message = new ImportReturnSRSModel();

            bool IsColumnFlag = false;
            bool InsertFlag = false;

            string columnErrorMessage_SalesDocNo = string.Empty, columnErrorMessage_PONoLRNo = string.Empty, columnErrorMessage_SalesOrganization = string.Empty,
                   columnErrorMessage_Soldtoparty = string.Empty, columnErrorMessage_Name1 = string.Empty, columnErrorMessage_Netvalue = string.Empty,
                   columnErrorMessage_NetValue1 = string.Empty, columnErrorMessage_Division = string.Empty, columnErrorMessage_Plant = string.Empty,
                   columnErrorMessage_PONo = string.Empty,
            columnErrorMessage_SalesDocDate = string.Empty,//
            columnErrorMessage_ReturnOrdRs = string.Empty;//

            bool columnErrorFlag_SalesDocNo = false, columnErrorFlag_PONoLRNo = false, columnErrorFlag_Netvalue = false, columnErrorFlag_Division = false,
               columnErrorFlag_Soldtoparty = false, columnErrorFlag_Name1 = false, columnErrorFlag_SalesOrganization = false,
               columnErrorFlag_Plant = false, columnErrorFlag_PONo = false, columnErrorFlag_SalesDocDate = false,
               columnErrorFlag_ReturnOrdRs = false, columnErrorFlag_isAlphanumeric = false;


            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;

                    if (file.FileName == "ImportSRSTemplate.xls" || file.FileName == "ImportSRSTemplate.XLS" || file.FileName == "ImportSRSTemplate.xlsx" || file.FileName == "ImportSRSTemplate.XLSX")
                    {
                        if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".XLS"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (file.FileName.EndsWith(".xlsx") || file.FileName.EndsWith(".XLSX"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else
                        {
                            message.RetResult = "This file format is not supported";
                        }

                        DataSet excelRecords = reader.AsDataSet();
                        reader.Close();

                        var finalRecords = excelRecords.Tables[0];
                        if (finalRecords.Rows.Count > 0)
                        {
                            List<ImportSRSDataModel> modelList = new List<ImportSRSDataModel>();
                            ImportSRSDataModel model;
                            IsColumnFlag = IsColumnValidationForSRS(finalRecords); // To Check Column Name
                            if (IsColumnFlag)
                            {
                                for (int j = 1; j < finalRecords.Rows.Count; j++) // To Row Values Append
                                {
                                    model = new ImportSRSDataModel();
                                    model.pkId = j;

                                    InsertFlag = true;
                                    // Sales Document
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][0].ToString(), j, ref InsertFlag, ref columnErrorFlag_SalesDocNo, ref columnErrorMessage_SalesDocNo, columnErrorFlag_isAlphanumeric);

                                    // PO No LR No
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][1].ToString(), j, ref InsertFlag, ref columnErrorFlag_PONoLRNo, ref columnErrorMessage_PONoLRNo, columnErrorFlag_isAlphanumeric);

                                    //PONO
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][2].ToString(), j, ref InsertFlag, ref columnErrorFlag_PONo, ref columnErrorMessage_PONo, columnErrorFlag_isAlphanumeric);

                                    // Sold to party
                                    BusinessCont.TypeCheckWithData("Alphanumeric", finalRecords.Rows[j][3].ToString(), j, ref InsertFlag, ref columnErrorFlag_Soldtoparty, ref columnErrorMessage_Soldtoparty, columnErrorFlag_isAlphanumeric);

                                    // Name1
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][4].ToString(), j, ref InsertFlag, ref columnErrorFlag_Name1, ref columnErrorMessage_Name1, columnErrorFlag_isAlphanumeric);

                                    // Netvalue
                                    BusinessCont.TypeCheckWithData("Decimal", finalRecords.Rows[j][5].ToString(), j, ref InsertFlag, ref columnErrorFlag_Netvalue, ref columnErrorMessage_Netvalue, columnErrorFlag_isAlphanumeric);

                                    // Division
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][6].ToString(), j, ref InsertFlag, ref columnErrorFlag_Division, ref columnErrorMessage_Division, columnErrorFlag_isAlphanumeric);

                                    // Sales Organization
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][7].ToString(), j, ref InsertFlag, ref columnErrorFlag_SalesOrganization, ref columnErrorMessage_SalesOrganization, columnErrorFlag_isAlphanumeric);

                                    // Plant
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][8].ToString(), j, ref InsertFlag, ref columnErrorFlag_Plant, ref columnErrorMessage_Plant, columnErrorFlag_isAlphanumeric);

                                    // Doc Date
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][9].ToString(), j, ref InsertFlag, ref columnErrorFlag_SalesDocDate, ref columnErrorMessage_SalesDocDate, columnErrorFlag_isAlphanumeric);

                                    // Return Cat Code
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][10].ToString(), j, ref InsertFlag, ref columnErrorFlag_ReturnOrdRs, ref columnErrorMessage_ReturnOrdRs, columnErrorFlag_isAlphanumeric);

                                    // Valid Data - Insert Flag = true
                                    if (InsertFlag)
                                    {
                                        model.SalesDocNo = Convert.ToString(finalRecords.Rows[j][0]);
                                        model.PONoLRNo = Convert.ToString(finalRecords.Rows[j][1]);
                                        model.PONo = Convert.ToString(finalRecords.Rows[j][2]);
                                        model.Soldtoparty = Convert.ToString(finalRecords.Rows[j][3]);
                                        model.Name1 = Convert.ToString(finalRecords.Rows[j][4]);
                                        model.Netvalue = Convert.ToDecimal(finalRecords.Rows[j][5]);
                                        model.Division = Convert.ToString(finalRecords.Rows[j][6]);
                                        model.SalesOrganization = Convert.ToString(finalRecords.Rows[j][7]);
                                        model.Plant = Convert.ToString(finalRecords.Rows[j][8]);
                                        //model.SalesDocDate = Convert.ToString(finalRecords.Rows[j][9]);
                                        model.SalesDocDate = Convert.ToDateTime(finalRecords.Rows[j][9]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.ReturnCatCode = Convert.ToString(finalRecords.Rows[j][10]);
                                        model.AddedBy = Convert.ToInt32(AddedBy);
                                        model.AddedOn = Convert.ToDateTime(DateTime.Now).ToString("yyyy/MM/dd hh:mm:ss");
                                        modelList.Add(model);
                                    }
                                }
                            }

                            // To Check Column Error Flag Maintain Column Wise
                            if (columnErrorFlag_SalesDocNo ||
                                columnErrorFlag_PONoLRNo || columnErrorFlag_Soldtoparty ||
                                columnErrorFlag_Name1 || columnErrorFlag_Netvalue || columnErrorFlag_Division || columnErrorFlag_SalesOrganization || columnErrorFlag_Plant ||  columnErrorFlag_SalesDocDate || columnErrorFlag_ReturnOrdRs)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_SalesDocNo)
                                {
                                    message.RetResult += "\n Sales Document ---- \n " + columnErrorMessage_SalesDocNo;
                                }
                                if (columnErrorFlag_PONoLRNo)
                                {
                                    message.RetResult += "\n Purchase order no./LR No ---- \n" + columnErrorMessage_PONoLRNo;
                                }

                                if (columnErrorFlag_PONo)
                                {
                                    message.RetResult += "\n Purchase order number ---- \n" + columnErrorMessage_PONo;
                                }

                                if (columnErrorFlag_Soldtoparty)
                                {
                                    message.RetResult += "\n Sold-to party ---- \n " + columnErrorMessage_Soldtoparty;
                                }

                                if (columnErrorFlag_Name1)
                                {
                                    message.RetResult += "\n Name 1 ---- \n " + columnErrorMessage_Name1;
                                }

                                if (columnErrorFlag_Netvalue)
                                {
                                    message.RetResult += "\n Net value ---- \n " + columnErrorMessage_Netvalue;
                                }

                                if (columnErrorFlag_Division)
                                {
                                    message.RetResult += "\n Division ---- \n " + columnErrorMessage_Division;
                                }

                                if (columnErrorFlag_SalesOrganization)
                                {
                                    message.RetResult += "\n Sales Organization ---- \n " + columnErrorMessage_SalesOrganization;
                                }

                                if (columnErrorFlag_Plant)
                                {
                                    message.RetResult += "\n Plant ---- \n " + columnErrorMessage_Plant;
                                }

                                if (columnErrorFlag_SalesDocDate)
                                {
                                    message.RetResult += "\n Document Date ---- \n " + columnErrorMessage_SalesDocDate;
                                }

                                if (columnErrorFlag_ReturnOrdRs)
                                {
                                    message.RetResult += "\n OrdRs ---- \n " + columnErrorMessage_ReturnOrdRs;
                                }

                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable headers
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("SalesDocNo");
                                dt.Columns.Add("PONoLRNo");
                                dt.Columns.Add("PONo");
                                dt.Columns.Add("Soldtoparty");
                                dt.Columns.Add("Name1");
                                dt.Columns.Add("Netvalue");
                                dt.Columns.Add("Division");
                                dt.Columns.Add("SalesOrganization");
                                dt.Columns.Add("Plant");
                                dt.Columns.Add("SalesDocDate");
                                dt.Columns.Add("ReturnCatCode");

                                foreach (var itemList in modelList)
                                {
                                    // Add Rows DataTable
                                    dt.Rows.Add(itemList.pkId, itemList.SalesDocNo, itemList.PONoLRNo, itemList.PONo, itemList.Soldtoparty,
                                       itemList.Name1, itemList.Netvalue, itemList.Division, itemList.SalesOrganization, itemList.Plant,itemList.SalesDocDate, itemList.ReturnCatCode);
                                }
                                if (dt.Rows.Count > 0)
                                {
                                    using (var db = new CFADBEntities())
                                    {
                                        {
                                            SqlConnection connection = (SqlConnection)db.Database.Connection;
                                            SqlCommand cmd = new SqlCommand("CFA.usp_ImportSRSData", connection);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                            BranchIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                            CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter ImportTransDataParameter = cmd.Parameters.AddWithValue("@ImportSRSData", dt);
                                            ImportTransDataParameter.SqlDbType = SqlDbType.Structured;
                                            SqlParameter AddedbyParameter = cmd.Parameters.AddWithValue("@Addedby", AddedBy);
                                            AddedbyParameter.SqlDbType = SqlDbType.NVarChar;
                                            if (connection.State == ConnectionState.Closed)
                                                connection.Open();
                                            SqlDataReader dr = cmd.ExecuteReader();
                                            while (dr.Read())
                                            {
                                                message.RetResult = (string)dr["RetResult"] + message.RetResult;
                                            }
                                            if (connection.State == ConnectionState.Open)
                                                connection.Close();
                                        }
                                    }
                                }
                                else
                                {
                                    message.RetResult = BusinessCont.msg_NoRecordFound;
                                }

                            }
                        }
                        else
                        {
                            message.RetResult = BusinessCont.msg_NoRecordFoundExcelFile;
                        }
                    }
                    else
                    {
                        message.RetResult = BusinessCont.msg_InvalidExcelFile;
                    }
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportSRSData", "Import SRS Data:  " + AddedBy, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;

        }

        private bool IsColumnValidationForSRS(DataTable finalRecords)
        {
            bool IsValid = false;
            try
            {
                if (finalRecords.Rows.Count > 0)
                {
                    string SalesDocNo = Convert.ToString(finalRecords.Rows[0][0]);
                    string PONoLRNo = Convert.ToString(finalRecords.Rows[0][1]);
                    string PONo = Convert.ToString(finalRecords.Rows[0][2]);
                    string Soldtoparty = Convert.ToString(finalRecords.Rows[0][3]);
                    string Name1 = Convert.ToString(finalRecords.Rows[0][4]);
                    string Netvalue = Convert.ToString(finalRecords.Rows[0][5]);
                    string Division = Convert.ToString(finalRecords.Rows[0][6]);
                    string SalesOrganization = Convert.ToString(finalRecords.Rows[0][7]);
                    string Plant = Convert.ToString(finalRecords.Rows[0][8]);
                    string SalesDocDate = Convert.ToString(finalRecords.Rows[0][9]);
                    string ReturnCatCode = Convert.ToString(finalRecords.Rows[0][10]);

                    if (SalesDocNo == "Sales Document" && PONoLRNo == "Purchase order no./LR No"
                        && PONo == "Purchase order number" && Soldtoparty == "Sold-to party"
                       && Name1 == "Name 1" && Netvalue == "Net value"
                        && Division == "Division" && SalesOrganization == "Sales Organization" && Plant == "Plant"
                        && ReturnCatCode == "OrdRs" && SalesDocDate == "Document Date") // Need To Change
                    {

                        IsValid = true;
                    }
                    else
                    {
                        IsValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "IsColumnValidationForSRS", "Is Column Validation For SRS", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return IsValid;
        }
        #endregion

        #region Import SRS Data - BranchId, CompanyId and Addedby New
        [HttpPost]
        [Route("OrderReturn/ImportSRSDataNew/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportLrDataNew(int BranchId, int CompanyId, string Addedby)
        {
            ImportReturnSRSModel message = new ImportReturnSRSModel();

            bool IsColumnFlag = false;
            bool InsertFlag = false;

            string columnErrorMessage_SalesDocNo = string.Empty, columnErrorMessage_PONoLRNo = string.Empty, columnErrorMessage_SalesOrganization = string.Empty,
                   columnErrorMessage_Soldtoparty = string.Empty, columnErrorMessage_Name1 = string.Empty, columnErrorMessage_Netvalue = string.Empty,
                   columnErrorMessage_NetValue1 = string.Empty, columnErrorMessage_Division = string.Empty, columnErrorMessage_Plant = string.Empty,
                   columnErrorMessage_PONo = string.Empty,
            columnErrorMessage_SalesDocDate = string.Empty,//
            columnErrorMessage_ReturnCatCode = string.Empty;//

            bool columnErrorFlag_SalesDocNo = false, columnErrorFlag_PONoLRNo = false, columnErrorFlag_Netvalue = false, columnErrorFlag_Division = false,
               columnErrorFlag_Soldtoparty = false, columnErrorFlag_Name1 = false, columnErrorFlag_SalesOrganization = false,
               columnErrorFlag_Plant = false, columnErrorFlag_PONo = false, columnErrorFlag_SalesDocDate = false,
               columnErrorFlag_ReturnCatCode = false,
               columnErrorFlag_isAlphanumeric = false;

            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var ImportFor = "Import SRS";
                    var columnHeaderList = _unitOfWork.OrderDispatchRepository.GetColumnHeaderList(BranchId, CompanyId, ImportFor);
                    if (columnHeaderList.Count == 0)
                    {
                        message.RetResult = "Master data is not configured, Please contact to system admin";
                        return message.RetResult;
                    }
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportSRSTemplate.xls" || file.FileName == "ImportSRSTemplate.XLS" || file.FileName == "ImportSRSTemplate.xlsx" || file.FileName == "ImportSRSTemplate.XLSX")
                    {
                        if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".XLS"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (file.FileName.EndsWith(".xlsx") || file.FileName.EndsWith(".XLSX"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else
                        {
                            message.RetResult = "This file format is not supported";
                        }

                        DataSet excelRecords = reader.AsDataSet();
                        reader.Close();

                        var finalRecords = excelRecords.Tables[0];
                        if (finalRecords.Rows.Count > 0)
                        {
                            List<ImportSRSDataModel> modelList = new List<ImportSRSDataModel>();
                            ImportSRSDataModel model;

                            // Create a dictionary to map Excel column names to their corresponding database column names and data types
                            Dictionary<int, Tuple<string, string>> columnIndexMapping = new Dictionary<int, Tuple<string, string>>();

                            // the columnIndexMapping based on the Excel to DB column mapping from columnHeaderList
                            foreach (var columnHeader in columnHeaderList)
                            {
                                bool matchFound = false;
                                for (int columnIndex = 0; columnIndex < finalRecords.Columns.Count; columnIndex++)
                                {
                                    string excelColumnName = finalRecords.Rows[0][columnIndex].ToString().Trim();
                                    if (excelColumnName == columnHeader.ExcelColName.TrimEnd('\r', '\n'))
                                    {
                                        // Map the Excel column index to its corresponding database column name
                                        columnIndexMapping[columnIndex] = Tuple.Create(columnHeader.FieldName, columnHeader.ColumnDatatype);
                                        matchFound = true;
                                        break;
                                    }
                                }
                                if (!matchFound)
                                {
                                    // Handle column name mismatch                          
                                    message.RetResult += $"Invalid Excel - {columnHeader.ExcelColName} mismatch.";
                                    return message.RetResult;
                                }
                                matchFound = false;
                            }
                            for (int j = 1; j < finalRecords.Rows.Count; j++)
                            {
                                model = new ImportSRSDataModel();
                                model.pkId = j;
                                InsertFlag = true;
                                // Iterate through the columns dynamically using the columnIndexMapping
                                foreach (var columnIndex in columnIndexMapping.Keys)
                                {
                                    string dbColumnName = columnIndexMapping[columnIndex].Item1;
                                    string columnDataType = columnIndexMapping[columnIndex].Item2;
                                    string cellValue = finalRecords.Rows[j][columnIndex].ToString();
                                    // SalesDocNo
                                    if (dbColumnName == "SalesDocNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_SalesDocNo, ref columnErrorMessage_SalesDocNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // PONoLRNo
                                    if (dbColumnName == "PONoLRNo")
                                    {
                                        BusinessCont.TypeCheckWithData("String", cellValue, j, ref InsertFlag, ref columnErrorFlag_PONoLRNo, ref columnErrorMessage_PONoLRNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // SalesOrganization
                                    if (dbColumnName == "SalesOrganization")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_SalesOrganization, ref columnErrorMessage_SalesOrganization, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Soldtoparty
                                    if (dbColumnName == "Soldtoparty")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_Soldtoparty, ref columnErrorMessage_Soldtoparty, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Name1
                                    if (dbColumnName == "Name1")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_Name1, ref columnErrorMessage_Name1, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Netvalue
                                    if (dbColumnName == "Netvalue")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_Netvalue, ref columnErrorMessage_Netvalue, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Division
                                    if (dbColumnName == "Division")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_Division, ref columnErrorMessage_Division, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Plant
                                    if (dbColumnName == "Plant")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_Plant, ref columnErrorMessage_Plant, columnErrorFlag_isAlphanumeric);
                                    }
                                  
                                    // Sales Doc Date
                                    if (dbColumnName == "DocumentDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_SalesDocDate, ref columnErrorMessage_SalesDocDate, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Return Cat Code
                                    if (dbColumnName == "OrdRs")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_ReturnCatCode, ref columnErrorMessage_ReturnCatCode, columnErrorFlag_isAlphanumeric);
                                    }

                                    // Assign values to model properties dynamically based on dbColumnName
                                    switch (dbColumnName)
                                    {
                                        case "SalesDocNo":
                                            model.SalesDocNo = cellValue;
                                            break;
                                        case "PONoLRNo":
                                            model.PONoLRNo = cellValue;
                                            break;
                                        case "PONo":
                                            model.PONo = cellValue;
                                            break;
                                        case "Soldtoparty":
                                            model.Soldtoparty = cellValue;
                                            break;
                                        case "Name1":
                                            model.Name1 = cellValue;
                                            break;
                                        case "Netvalue":
                                            model.Netvalue = Convert.ToDecimal(cellValue);
                                            break;
                                        case "Division":
                                            model.Division = cellValue;
                                            break;
                                        case "SalesOrganization":
                                            model.SalesOrganization = cellValue;
                                            break;
                                        case "Plant":
                                            model.Plant = cellValue;
                                            break;
                                        case "DocumentDate":
                                            model.SalesDocDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd hh:mm:ss");
                                            break;
                                        case "OrdRs":
                                            model.ReturnCatCode = cellValue;
                                            break;
                                        case "AddedBy":
                                            model.AddedBy = Convert.ToInt32(cellValue);
                                            break;
                                    }
                                }

                                // Valid Data -Insert Flag = true
                                if (InsertFlag)
                                {
                                    modelList.Add(model);
                                }
                            }

                            // To Check Column Error Flag Maintain Column Wise
                            if (columnErrorFlag_SalesDocNo ||
                                columnErrorFlag_PONoLRNo || columnErrorFlag_Soldtoparty ||
                                columnErrorFlag_Name1 || columnErrorFlag_Netvalue || columnErrorFlag_Division || columnErrorFlag_SalesOrganization ||
                                columnErrorFlag_Plant || columnErrorFlag_SalesDocDate || columnErrorFlag_ReturnCatCode)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_SalesDocNo)
                                {
                                    message.RetResult += "\n Sales Document ---- \n " + columnErrorMessage_SalesDocNo;
                                }
                                if (columnErrorFlag_PONoLRNo)
                                {
                                    message.RetResult += "\n Purchase order no./LR No ---- \n" + columnErrorMessage_PONoLRNo;
                                }

                                if (columnErrorFlag_PONo)
                                {
                                    message.RetResult += "\n Purchase order number ---- \n" + columnErrorMessage_PONo;
                                }

                                if (columnErrorFlag_Soldtoparty)
                                {
                                    message.RetResult += "\n Sold-to party ---- \n " + columnErrorMessage_Soldtoparty;
                                }

                                if (columnErrorFlag_Name1)
                                {
                                    message.RetResult += "\n Name 1 ---- \n " + columnErrorMessage_Name1;
                                }

                                if (columnErrorFlag_Netvalue)
                                {
                                    message.RetResult += "\n Net value ---- \n " + columnErrorMessage_Netvalue;
                                }

                                if (columnErrorFlag_Division)
                                {
                                    message.RetResult += "\n Division ---- \n " + columnErrorMessage_Division;
                                }

                                if (columnErrorFlag_SalesOrganization)
                                {
                                    message.RetResult += "\n Sales Organization ---- \n " + columnErrorMessage_SalesOrganization;
                                }

                                if (columnErrorFlag_Plant)
                                {
                                    message.RetResult += "\n Plant ---- \n " + columnErrorMessage_Plant;
                                }
                                if (columnErrorFlag_SalesDocDate)
                                {
                                    message.RetResult += "\n Document Date ---- \n " + columnErrorMessage_SalesDocDate;
                                }
                                if (columnErrorFlag_ReturnCatCode)
                                {
                                    message.RetResult += "\n OrdRs ---- \n " + columnErrorMessage_ReturnCatCode;
                                }

                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("SalesDocNo");
                                dt.Columns.Add("PONoLRNo");
                                dt.Columns.Add("PONo");
                                dt.Columns.Add("Soldtoparty");
                                dt.Columns.Add("Name1");
                                dt.Columns.Add("Netvalue");
                                dt.Columns.Add("Division");
                                dt.Columns.Add("SalesOrganization");
                                dt.Columns.Add("Plant");
                                dt.Columns.Add("SalesDocDate");
                                dt.Columns.Add("ReturnCatCode");

                                foreach (var itemList in modelList)
                                {
                                    // Add Rows DataTable
                                    dt.Rows.Add(itemList.pkId, itemList.SalesDocNo, itemList.PONoLRNo, itemList.PONo, itemList.Soldtoparty,
                                       itemList.Name1, itemList.Netvalue, itemList.Division, itemList.SalesOrganization, itemList.Plant,itemList.SalesDocDate, itemList.ReturnCatCode);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    using (var db = new CFADBEntities())
                                    {
                                        {
                                            SqlConnection connection = (SqlConnection)db.Database.Connection;
                                            SqlCommand cmd = new SqlCommand("CFA.usp_ImportSRSData", connection);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                            BranchIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                            CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter ImportTransDataParameter = cmd.Parameters.AddWithValue("@ImportSRSData", dt);
                                            ImportTransDataParameter.SqlDbType = SqlDbType.Structured;
                                            SqlParameter AddedbyParameter = cmd.Parameters.AddWithValue("@Addedby", Addedby);
                                            AddedbyParameter.SqlDbType = SqlDbType.NVarChar;
                                            if (connection.State == ConnectionState.Closed)
                                                connection.Open();
                                            SqlDataReader dr = cmd.ExecuteReader();
                                            while (dr.Read())
                                            {
                                                message.RetResult = (string)dr["RetResult"] + message.RetResult;
                                            }
                                            if (connection.State == ConnectionState.Open)
                                                connection.Close();
                                        }
                                    }
                                }
                                else
                                {
                                    message.RetResult = BusinessCont.msg_NoRecordFound;
                                }
                            }
                        }
                        else
                        {
                            message.RetResult = BusinessCont.msg_NoRecordFoundExcelFile;
                        }
                    }
                    else
                    {
                        message.RetResult = BusinessCont.msg_InvalidExcelFile;
                    }
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportSRSData", "Import SRS Data:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        #endregion

        #region Get ImportSRS List
        [HttpPost]
        [Route("OrderReturn/GetSRSDataList")]
        public List<ImportSRSList> GetSRSDataList(ImportSRSList model)
        {
            List<ImportSRSList> TransitLst = new List<ImportSRSList>();
            try
            {
                TransitLst = _unitOfWork.OrderReturnRepository.GetSRSDataLst(model.BranchId, model.CompId, model.Date);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetSRSDataList", "Get SRS List" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransitLst;
        }
        #endregion

        #region Uploading Destruction Certificate
        [HttpPost]
        [Route("OrderReturn/UploadImages/{CNIdStr}/{BranchId}/{CompId}/{DestrCertFile}/{AddedBy}")]

        public string UploadImages(string CNIdStr, int BranchId, int CompId, string DestrCertFile, string AddedBy)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            DestCetModel model = new DestCetModel();
            int result1 = 0;
            string Saveimg = string.Empty;
            uploadimgreturn message = new uploadimgreturn();
            var docfiles = new List<string>();
            var fileName = new List<string>();
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    if ((postedFile.FileName.EndsWith(".jpg")) || (postedFile.FileName.EndsWith(".jpeg")) || (postedFile.FileName.EndsWith(".png")) || (postedFile.FileName.EndsWith(".pdf")))
                    {
                        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadImages\\") + postedFile.FileName;
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath); //Path Of Image   //fileName , filePath
                        fileName.Add(postedFile.FileName);
                        model.CNIdStr = CNIdStr;
                        model.BranchId = BranchId;
                        model.CompId = CompId;
                        model.DestrCertFile = DestrCertFile;
                        model.AddedBy = Convert.ToInt32(AddedBy);
                        result1 = _unitOfWork.OrderReturnRepository.AddUploadDesCertificate(model);
                    }
                    else
                    {
                        message.RetResult = "This file format is not supported";
                    }

                    if (result1 > 0)
                    {
                        Saveimg = Convert.ToString(result1 + "  " + postedFile.FileName);
                    }
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "UploadImages", "Upload Destruction Certificate" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Saveimg;
        }

        #endregion

        # region Get Creadit Note Upload List for Upload Des Certificate

        [HttpGet]
        [Route("OrderReturn/GetCreaditNoteUploadList/{BranchId}/{CompId}")]
        public List<CreditNoteListModel> GetCreaditNoteUploadList(int BranchId, int CompId)
        {
            List<CreditNoteListModel> GetCnDLst = new List<CreditNoteListModel>();
            try
            {
                GetCnDLst = _unitOfWork.OrderReturnRepository.GetCreaditNoteUploadList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCreaditNoteUploadList", "Get Credit Note Upload Destruction Certificate List" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GetCnDLst;
        }

        #endregion

        #region Get Creadit Note List for Destruction Certificate
        [HttpGet]
        [Route("OrderReturn/GetCreaditNoteDestructionList/{BranchId}/{CompId}")]
        public List<CreditNoteListModel> GetCreaditNoteDestructionList(int BranchId, int CompId)
        {
            List<CreditNoteListModel> GetCnDLst = new List<CreditNoteListModel>();
            try
            {
                GetCnDLst = _unitOfWork.OrderReturnRepository.GetCreaditNoteDestructionList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCreaditNoteDestructionList", "Get Credit Note Destruction Certificate List" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GetCnDLst;
        }

        #endregion

        #region Get LR Received Op List
        [HttpGet]
        [Route("OrderReturn/GetLRReceivedOpList/{BranchId}/{CompId}")]
        public List<GetLRReceivedOpModel> GetLRReceivedOpList(int BranchId, int CompId)
        {
            List<GetLRReceivedOpModel> LRReceivedOpListlist = new List<GetLRReceivedOpModel>();
            try
            {
                LRReceivedOpListlist = _unitOfWork.OrderReturnRepository.GetLRReceivedOpLists(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRReceivedOpList", "Get LR ReceivedOp List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return LRReceivedOpListlist;
        }
        #endregion

        #region Add Inward Gatepass Received
        [HttpPost]
        [Route("OrderReturn/UpdateInwrdGtpassRecved")]
        public int UpdateInwrdGtpassRecved([FromBody] UpdateInwrdGtpassRecvedModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.OrderReturnRepository.UpdateInwrdGtpassRecvedAdd(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "UpdateInwrdGtpassRecvedAdd", "Update Inward Gatepass RecvedAdd", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Lr Mis Match List
        [HttpGet]
        [Route("OrderReturn/GetLrMisMatchList/{BranchId}/{CompId}")]
        public List<GetLrMisMatchListModel> GetLrMisMatchList(int BranchId, int CompId)
        {
            List<GetLrMisMatchListModel> GetLrMisMatchList = new List<GetLrMisMatchListModel>();
            try
            {
                GetLrMisMatchList = _unitOfWork.OrderReturnRepository.GetLrMisMatchLists(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLrMisMatchList", "Get Lr Mis Match List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return GetLrMisMatchList;
        }
        #endregion

        #region Get Lr MisMatch Count List
        [HttpPost]
        [Route("OrderReturn/GetLRSRSMappingCounts")]
        public GetLRMisMatchcountModel GetLRSRSMappingCount(GetLRMisMatchcountModel model)
        {
            GetLRMisMatchcountModel GetLRSRSMappingCount = new GetLRMisMatchcountModel();
            try
            {
                GetLRSRSMappingCount = _unitOfWork.OrderReturnRepository.GetLRSRSMappingCounts(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRSRSMappingCount", "Get Lr MisMatch Count List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return GetLRSRSMappingCount;
        }
        #endregion

        #region Get LR Page Count List
        [HttpPost]
        [Route("OrderReturn/GetLRPageCounts")]
        public LRPageCounts GetLRPageCount(LRPageCounts model)
        {
            LRPageCounts GetLRPageCount = new LRPageCounts();
            try
            {
                GetLRPageCount = _unitOfWork.OrderReturnRepository.GetLRPageCounts(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRPageCounts", "Get LR Page Count List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return GetLRPageCount;
        }
        #endregion

        #region Get SRS CN Mapping Counts
        [HttpPost]
        [Route("OrderReturn/GetSRSCNMappingCounts")]
        public GetSRSCNMappingCountsModel GetSRSCNMappingCount(GetSRSCNMappingCountsModel model)
        {
            GetSRSCNMappingCountsModel GetSRSCNMappingCount = new GetSRSCNMappingCountsModel();
            try
            {
                GetSRSCNMappingCount = _unitOfWork.OrderReturnRepository.GetSRSCNMappingCountList(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetSRSCNMappingCount", "Get SRS CN Mapping Counts", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return GetSRSCNMappingCount;
        }
        #endregion

        //#region Get LR SRS CN List for FilterDataOrdrRtrnList --Not In Use
        //[HttpGet]
        //[Route("OrderReturn/GetLRSRSCNListforFilterDataOrdrRtrn/{BranchId}/{CompId}/{FromDate}/{ToDate}")]
        //public List<GetLRReceivedOpModel> GetLRSRSCNListforFilterDataOrdrRtrnList(int BranchId, int CompId,DateTime FromDate, DateTime ToDate)
        //{
        //    List<GetLRReceivedOpModel> LRSRSCNListforFilterDatalist = new List<GetLRReceivedOpModel>();
        //    try
        //    {
        //        LRSRSCNListforFilterDatalist = _unitOfWork.OrderReturnRepository.GetLRSRSCNListforFilterDataOrdrRtrnList(BranchId, CompId, FromDate, ToDate);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetLRSRSCNListforFilterDataOrdrRtrnList", "Get LRSRSCNListforFilterDataOrdrRtrnList", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
        //    }
        //    return LRSRSCNListforFilterDatalist;
        //}
        //#endregion

        //#region OrderReturn/GetCountDashbordforOrderReturn --not in use
        //[HttpPost]
        //[Route("OrderReturn/GetCountDashbordforOrderReturn")]
        //public OrderReturnModelNewCount GetDashboradCountOrderReturn (DashBoardCommonModel model)
        //{
        //    OrderReturnModelNewCount OrderReturn = new OrderReturnModelNewCount();
        //    try
        //    {
        //        OrderReturn = _unitOfWork.OrderReturnRepository.GetDashboradCountOrderReturn(model.BranchId, model.CompanyId, model.FromDate, model.ToDate);
        //    }
        //    catch(Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetDashboradCountOrderReturn", "Get Dashborad Count Order Return", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
        //    }
        //    return OrderReturn;
        //}
        //#endregion

        #region Get Order Return Dashbod count For all Login
        [HttpPost]
        [Route("OrderReturn/GetDashBordCount")]
        public GetORdashbordsupervisorModel GetDashBordSupervisorCount(DashBoardCommonModelNew model)
        {
            GetORdashbordsupervisorModel getordsh = new GetORdashbordsupervisorModel();
            try
            {
                getordsh = _unitOfWork.OrderReturnRepository.GetDashBordSupervisorCount(model.BranchId, model.CompanyId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetDashBordSupervisorCount", "Get DashBord Supervisor Count", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return getordsh;
        }
        #endregion

        #region Get Order Return Filtered List New
        [HttpGet]
        [Route("OrderReturn/GetOrderReturnFilterNewList/{BranchId}/{CompId}")]
        public List<GetLRReceivedOpModel> GetOrderReturnFilterNewList(int BranchId, int CompId)
        {
            List<GetLRReceivedOpModel> dataList = new List<GetLRReceivedOpModel>();
            try
            {
                dataList = _unitOfWork.OrderReturnRepository.GetOrderReturnFilterNewList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderReturnFilterNewList", "Get Order Return Filter New List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return dataList;
        }
        #endregion

        #region Get Pend Sale CN New List
        [HttpGet]
        [Route("OrderReturn/GetPendSaleCNNewList/{BranchId}/{CompId}/{Flag}")]
        public List<GetLRReceivedOpModel> GetPendSaleCNNewList(int BranchId, int CompId,string Flag)
        {
            List<GetLRReceivedOpModel> dataList = new List<GetLRReceivedOpModel>();
            try
            {
                dataList = _unitOfWork.OrderReturnRepository.GetPendSaleCNNewList(BranchId, CompId, Flag);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPendSaleCNNewList", "Get Pend Sale CN New List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return dataList;
        }
        #endregion

        #region Mob Order return Dashboard
        [HttpGet]
        [Route("OrderReturn/ExpSupCount/{BranchId}/{CompId}")]
        public ExpSupCountmodel ExpSupCount(int BranchId, int CompId)
        {
            ExpSupCountmodel OrderReturn = new ExpSupCountmodel();
            try
            {
                OrderReturn = _unitOfWork.OrderReturnRepository.ExpSupCount(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetDashboradCountOrderReturn", "Get Dashborad Count Order Return", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return OrderReturn;
        }
        #endregion

        #region Start - Expiry Supervisor Dashboard Mob
        [HttpGet]
        [Route("OrderReturn/ExpirySupervisorDashboardMob/{BranchId}/{CompId}")]
        public List<ExpirySupervisorDashboardMobModel> ExpirySupervisorDashboardMob(int BranchId, int CompId)
        {
            List<ExpirySupervisorDashboardMobModel> modelList = new List<ExpirySupervisorDashboardMobModel>();
            try
            {
                modelList = _unitOfWork.OrderReturnRepository.ExpirySupervisorDashboardMob(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ExpirySupervisorDashboardMob", "Expiry Supervisor Dashboard Mob", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return modelList;
        }
        #endregion End - Expiry Supervisor Dashboard Mob

        #region Get Owner OR Pend Consig Dash Smmry List
        [HttpGet]
        [Route("OrderReturn/GetOwnerORPendConsigDashSmmryList")]
        public List<OwnORPendConsigDashSmmryList> GetOwnerORPendConsigDashSmmryList()
        {
            List<OwnORPendConsigDashSmmryList> modelList = new List<OwnORPendConsigDashSmmryList>();
            try
            {
                modelList = _unitOfWork.OrderReturnRepository.GetOwnerORPendConsigDashSmmryList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerORPendConsigDashSmmryList", "Get Owner OR Pend Consig Dash Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return modelList;
        }
        #endregion

        #region Get Owner Saleable CN Dash Smmry List
        [HttpGet]
        [Route("OrderReturn/GetOwnerSaleableCNDashSmmryList/{FlagType}")]
        public List<OwnSaleableCNDashSmmryList> GetOwnerSaleableCNDashSmmryList(string FlagType)
        {
            List<OwnSaleableCNDashSmmryList> modelList = new List<OwnSaleableCNDashSmmryList>();
            try
            {
                modelList = _unitOfWork.OrderReturnRepository.GetOwnerSaleableCNDashSmmryList(FlagType);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerSaleableCNDashSmmryList", "Get Owner Saleable CN Dash Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return modelList;
        }
        #endregion

        #region Get Order Return List For Pending CN Count
        [HttpGet]
        [Route("OrderReturn/GetOrderReturnPendingCN/{BranchId}/{CompId}")]
        public List<GetPendingCNModel> GetOrderReturnPendingCN(int BranchId, int CompId)
        {
            List<GetPendingCNModel> PendingCNList = new List<GetPendingCNModel>();
            try
            {
                PendingCNList = _unitOfWork.OrderReturnRepository.GetOrderReturnPendingCN(BranchId, CompId);
            }
            catch(Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderReturnPendingCN", "Get Order Return Pending CN List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return PendingCNList;
        }
        #endregion

        #region Get Order Return List For Saleable Count
        [HttpGet]
        [Route("OrderReturn/GetOrderReturnSaleable/{BranchId}/{CompId}")]
        public List<GetSaleableModel> GetOrderReturnSaleable(int BranchId, int CompId)
        {
            List<GetSaleableModel> SaleableList = new List<GetSaleableModel>();
            try
            {
                SaleableList = _unitOfWork.OrderReturnRepository.GetOrderReturnSaleable(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderReturnPendingCN", "Get Order Return Pending CN List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return SaleableList;
        }
        #endregion

        #region Get Inward SRS List
        [HttpGet]
        [Route("OrderReturn/GetInwardSRSList/{BranchId}/{CompId}")]
        public List<InwardGatepassListModel> GetInwardSRSList(int BranchId,int CompId)
        {
            List<InwardGatepassListModel> srslist = new List<InwardGatepassListModel>();
            try
            {
                srslist = _unitOfWork.OrderReturnRepository.GetInwardSRSList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInwardSRSList", "Get Inward SRS List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return srslist;
        }
        #endregion

        #region Delete Inward SRS Details
        [HttpGet]
        [Route("OrderReturn/DeleteInwardSRSDetails/{BranchId}/{CompId}/{SRSId}")]
        public int DeleteInwardSRSDetails(int BranchId, int CompId,int SRSId)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.OrderReturnRepository.DeleteInwardSRSDetails(BranchId, CompId, SRSId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "DeleteInwardSRSDetails", "Delete Inward SRS Details", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

    }
}
