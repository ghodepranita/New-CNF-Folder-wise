using System;
using CNF.Business.BusinessConstant;
using System.Web.Http;
using CNF.Business.Model.ChequeAccounting;
using System.Collections.Generic;
using CNF.Business.Model.Context;
using ExcelDataReader;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;

namespace CNF.API.Controllers
{
    public class ChequeAccountingController : BaseApiController
    {
        #region Add Cheque Register 
        [HttpPost]
        [Route("ChequeAccounting/ChequeRegisterAdd")]
        public string ChequeRegisterAdd([FromBody] ChequeRegisterModel model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.chequeAccountingRepository.ChequeRegisterAdd(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ChequeRegisterAdd", "Add Cheque Register Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Cheque Register Edit Delete
        [HttpPost]
        [Route("ChequeAccounting/ChequeRegisterEditDelete")]
        public string ChequeRegisterEditDelete([FromBody] ChequeRegisterModel model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.chequeAccountingRepository.ChequeRegisterEditDelete(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ChequeRegisterAdd", "Add Cheque Register Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Cheque Register List
        [HttpGet]
        [Route("ChequeAccounting/ChequeRegisterList/{BranchId}/{CompId}/{StockistId}")]
        public List<ChequeRegisterModel> ChequeRegisterList(int BranchId, int CompId, int StockistId)
        {
            List<ChequeRegisterModel> ChequeRegisterList = new List<ChequeRegisterModel>();
            try
            {
                ChequeRegisterList = _unitOfWork.chequeAccountingRepository.ChequeRegisterList(BranchId, CompId, StockistId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "Cheque Register List", "Cheque Register List " + "BranchId:  " + BranchId + "CompId:  " + CompId + "StockistId:  " + StockistId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ChequeRegisterList;
        }
        #endregion

        #region Get Cheque Summary Count List
        [HttpGet]
        [Route("ChequeAccounting/ChequeSummyCount/{BranchId}/{CompId}/{StockistId}")]
        public ChequeSummyCountModel GetChequeSummyCountList(int BranchId, int CompId, int StockistId)
        {
            ChequeSummyCountModel ChequeSummyCounts = new ChequeSummyCountModel();
            try
            {
                ChequeSummyCounts = _unitOfWork.chequeAccountingRepository.ChequeSummyCountLst(BranchId, CompId, StockistId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetChequeSummyCountList", "Cheque Summary Count List " + "BranchId:  " + BranchId + "CompId:  " + CompId + ":" + StockistId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ChequeSummyCounts;
        }
        #endregion

        #region Import Stockist OutStanding - BranchId, CompanyId, OSData and Addedby
        [HttpPost]
        [Route("ChequeAccounting/ImportStockistOutStanding/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportStockistOutStanding(int BranchId, int CompanyId, string Addedby)
        {
            ImportStockistOutStandingData message = new ImportStockistOutStandingData();

            bool IsColumnFlag = false, InsertFlag = false, columnErrorFlag_DivCd = false, 
                 columnErrorFlag_CustomerCode = false, columnErrorFlag_CustomerName = false,
                 columnErrorFlag_City = false, columnErrorFlag_DocName = false, 
                 columnErrorFlag_DocDate = false, columnErrorFlag_DueDate = false,
                 columnErrorFlag_OpenAmt = false, columnErrorFlag_ChqNo = false, 
                 columnErrorFlag_DistrChannel = false, columnErrorFlag_DocTypeDesc = false,
                 columnErrorFlag_DocType = false, columnErrorFlag_OverdueAmt = false, 
                 columnErrorFlag_isAlphanumeric = false;

            string columnErrorMessage_DivCd = string.Empty, columnErrorMessage_CustomerCode = string.Empty, 
                   columnErrorMessage_CustomerName = string.Empty, columnErrorMessage_City = string.Empty,
                   columnErrorMessage_DocName = string.Empty, columnErrorMessage_DocDate = string.Empty,
                   columnErrorMessage_DueDate = string.Empty, columnErrorMessage_OpenAmt = string.Empty, 
                   columnErrorMessage_ChqNo = string.Empty, columnErrorMessage_DistrChannel = string.Empty, 
                   columnErrorMessage_DocTypeDesc = string.Empty, columnErrorMessage_DocType = string.Empty,
                   columnErrorMessage_OverdueAmt = string.Empty, columnErrorMessage_isAlphanumeric = string.Empty;
            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportStockistOutStandingTemplate.xls" || file.FileName == "ImportStockistOutStandingTemplate.XLS" || file.FileName == "ImportStockistOutStandingTemplate.xlsx" || file.FileName == "ImportStockistOutStandingTemplate.XLSX")
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
                            List<ImportStockistOutStandingData> modelList = new List<ImportStockistOutStandingData>();

                            IsColumnFlag = IsColumnValidationForStockistOutStanding(finalRecords); // To Check Column Name
                            if (IsColumnFlag)
                            {
                                for (int j = 1; j < finalRecords.Rows.Count; j++)
                                {
                                    ImportStockistOutStandingData model = new ImportStockistOutStandingData();
                                    model.pkId = j;

                                    InsertFlag = true;

                                    // Div_Cd
                                    BusinessCont.TypeCheckWithData("Long", finalRecords.Rows[j][0].ToString(), j, ref InsertFlag, ref columnErrorFlag_DivCd, ref columnErrorMessage_DivCd,  columnErrorFlag_isAlphanumeric);
                                    // CustomerCode
                                    BusinessCont.TypeCheckWithData("Alphanumeric", finalRecords.Rows[j][1].ToString(), j, ref InsertFlag, ref columnErrorFlag_CustomerCode, ref columnErrorMessage_CustomerCode, columnErrorFlag_isAlphanumeric);
                                    //CustomerName
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][2].ToString(), j, ref InsertFlag, ref columnErrorFlag_CustomerName, ref columnErrorMessage_CustomerName,  columnErrorFlag_isAlphanumeric);
                                    // City
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][3].ToString(), j, ref InsertFlag, ref columnErrorFlag_City, ref columnErrorMessage_City,  columnErrorFlag_isAlphanumeric);
                                    // DocName
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][4].ToString(), j, ref InsertFlag, ref columnErrorFlag_DocName, ref columnErrorMessage_DocName,  columnErrorFlag_isAlphanumeric);
                                    // DocDate
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][5].ToString(), j, ref InsertFlag, ref columnErrorFlag_DocDate, ref columnErrorMessage_DocDate,  columnErrorFlag_isAlphanumeric);
                                    // DueDate
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][6].ToString(), j, ref InsertFlag, ref columnErrorFlag_DueDate, ref columnErrorMessage_DueDate,  columnErrorFlag_isAlphanumeric);
                                    // OpenAmt
                                    BusinessCont.TypeCheckWithData("Decimal", finalRecords.Rows[j][7].ToString(), j, ref InsertFlag, ref columnErrorFlag_OpenAmt, ref columnErrorMessage_OpenAmt,  columnErrorFlag_isAlphanumeric);
                                    // ChqNo
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][8].ToString(), j, ref InsertFlag, ref columnErrorFlag_ChqNo, ref columnErrorMessage_ChqNo,  columnErrorFlag_isAlphanumeric);
                                    // DistrChannel
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][9].ToString(), j, ref InsertFlag, ref columnErrorFlag_DistrChannel, ref columnErrorMessage_DistrChannel,  columnErrorFlag_isAlphanumeric);
                                    // DocTypeDesc
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][10].ToString(), j, ref InsertFlag, ref columnErrorFlag_DocTypeDesc, ref columnErrorMessage_DocTypeDesc,  columnErrorFlag_isAlphanumeric);
                                    // DocType
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][11].ToString(), j, ref InsertFlag, ref columnErrorFlag_DocType, ref columnErrorMessage_DocType,  columnErrorFlag_isAlphanumeric);
                                    // OverdueAmt
                                    BusinessCont.TypeCheckWithData("Decimal", finalRecords.Rows[j][12].ToString(), j, ref InsertFlag, ref columnErrorFlag_OverdueAmt, ref columnErrorMessage_OverdueAmt,  columnErrorFlag_isAlphanumeric);

                                    // Valid Data - Insert Flag = true
                                    if (InsertFlag)
                                    {
                                        model.Div_Cd = Convert.ToInt64(finalRecords.Rows[j][0]);
                                        model.CustomerCode = Convert.ToString(finalRecords.Rows[j][1]);
                                        model.CustomerName = Convert.ToString(finalRecords.Rows[j][2]);
                                        model.City = Convert.ToString(finalRecords.Rows[j][3]);
                                        model.DocName = Convert.ToString(finalRecords.Rows[j][4]);
                                        model.DocDate = Convert.ToDateTime(finalRecords.Rows[j][5]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.DueDate = Convert.ToDateTime(finalRecords.Rows[j][6]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.OpenAmt = Convert.ToDecimal(finalRecords.Rows[j][7]);
                                        model.ChqNo = Convert.ToString(finalRecords.Rows[j][8]);
                                        model.DistrChannel = Convert.ToString(finalRecords.Rows[j][9]);
                                        model.DocTypeDesc = Convert.ToString(finalRecords.Rows[j][10]);
                                        model.DocType = Convert.ToString(finalRecords.Rows[j][11]);
                                        model.OverdueAmt = Convert.ToDecimal(finalRecords.Rows[j][12]);
                                        modelList.Add(model);
                                    }
                                }

                                // To Check Column Error Flag Maintain Column Wise
                                if (columnErrorFlag_DivCd || columnErrorFlag_CustomerCode || columnErrorFlag_CustomerName || columnErrorFlag_City ||
                                    columnErrorFlag_DocName || columnErrorFlag_DocDate || columnErrorFlag_DueDate || columnErrorFlag_OpenAmt ||
                                    columnErrorFlag_ChqNo || columnErrorFlag_DistrChannel || columnErrorFlag_DocTypeDesc || columnErrorFlag_DocType ||
                                    columnErrorFlag_OverdueAmt)
                                {
                                    message.RetResult = "\n Below Columns has invalid data:  ";
                                    if (columnErrorFlag_DivCd)
                                    {
                                        message.RetResult += "\n Div_Cd ---- \n " + columnErrorMessage_DivCd;
                                    }
                                    if (columnErrorFlag_CustomerCode)
                                    {
                                        message.RetResult += "\n CustomerCode ---- \n " + columnErrorMessage_CustomerCode;
                                    }
                                    if (columnErrorFlag_CustomerName)
                                    {
                                        message.RetResult += "\n CustomerName ---- \n " + columnErrorMessage_CustomerName;
                                    }
                                    if (columnErrorFlag_City)
                                    {
                                        message.RetResult += "\n City ---- \n " + columnErrorMessage_City;
                                    }
                                    if (columnErrorFlag_DocName)
                                    {
                                        message.RetResult += "\n DocName ---- \n " + columnErrorMessage_DocName;
                                    }
                                    if (columnErrorFlag_DocDate)
                                    {
                                        message.RetResult += "\n DocDate ---- \n " + columnErrorMessage_DocDate;
                                    }
                                    if (columnErrorFlag_DueDate)
                                    {
                                        message.RetResult += "\n DueDate ---- \n " + columnErrorMessage_DueDate;
                                    }
                                    if (columnErrorFlag_OpenAmt)
                                    {
                                        message.RetResult += "\n OpenAmt ---- \n" + columnErrorMessage_OpenAmt;
                                    }
                                    if (columnErrorFlag_ChqNo)
                                    {
                                        message.RetResult += "\n ChqNo ---- \n " + columnErrorMessage_ChqNo;
                                    }
                                    if (columnErrorFlag_DistrChannel)
                                    {
                                        message.RetResult += "\n DistrChannel ---- \n " + columnErrorMessage_DistrChannel;
                                    }
                                    if (columnErrorFlag_DocTypeDesc)
                                    {
                                        message.RetResult += "\n DocTypeDesc ---- \n " + columnErrorMessage_DocTypeDesc;
                                    }
                                    if (columnErrorFlag_DocType)
                                    {
                                        message.RetResult += "\n DocType ---- \n " + columnErrorMessage_DocType;
                                    }
                                    if (columnErrorFlag_OverdueAmt)
                                    {
                                        message.RetResult += "\n OverdueAmt ---- \n " + columnErrorMessage_OverdueAmt;
                                    }
                                }
                                else
                                {
                                    if (modelList.Count > 0)
                                    {
                                        // Create DataTable
                                        DataTable dt = new DataTable();
                                        dt.Columns.Add("pkId");
                                        dt.Columns.Add("Div_Cd");
                                        dt.Columns.Add("CustomerCode");
                                        dt.Columns.Add("CustomerName");
                                        dt.Columns.Add("City");
                                        dt.Columns.Add("DocName");
                                        dt.Columns.Add("DocDate");
                                        dt.Columns.Add("DueDate");
                                        dt.Columns.Add("OpenAmt");
                                        dt.Columns.Add("ChqNo");
                                        dt.Columns.Add("DistrChannel");
                                        dt.Columns.Add("DocTypeDesc");
                                        dt.Columns.Add("DocType");
                                        dt.Columns.Add("OverdueAmt");

                                        foreach (var itemList in modelList)
                                        {
                                            // Add Rows DataTable
                                            dt.Rows.Add(itemList.pkId, itemList.Div_Cd, itemList.CustomerCode, itemList.CustomerName, itemList.City, itemList.DocName, itemList.DocDate, itemList.DueDate, itemList.OpenAmt, itemList.ChqNo, itemList.DistrChannel, itemList.DocTypeDesc, itemList.DocType, itemList.OverdueAmt);
                                        }

                                        if (dt.Rows.Count > 0)
                                        {
                                            using (var db = new CFADBEntities())
                                            {
                                                SqlConnection connection = (SqlConnection)db.Database.Connection;
                                                SqlCommand cmd = new SqlCommand("CFA.usp_ImportStockistOSData", connection);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                                BranchIdParameter.SqlDbType = SqlDbType.Int;
                                                SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                                CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                                SqlParameter OSDateParameter = cmd.Parameters.AddWithValue("@OSDate", DateTime.Now);
                                                OSDateParameter.SqlDbType = SqlDbType.DateTime;
                                                SqlParameter ImportSODataParameter = cmd.Parameters.AddWithValue("@OSData", dt);
                                                ImportSODataParameter.SqlDbType = SqlDbType.Structured;
                                                SqlParameter AddedbyParameter = cmd.Parameters.AddWithValue("@Addedby", Addedby);
                                                AddedbyParameter.SqlDbType = SqlDbType.NVarChar;
                                                SqlParameter RetValParameter = cmd.Parameters.AddWithValue("@RetVal", 0);
                                                RetValParameter.Direction = ParameterDirection.Output;
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
                                        else
                                        {
                                            message.RetResult = BusinessCont.msg_NoRecordFound;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                message.RetResult = BusinessCont.msg_InvalidColumnName;
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportStockistOutStanding", "Import Stockist OutStanding Data:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        /// <summary>
        /// Is Column Validation For Stockist OutStanding
        /// </summary>
        /// <param name="finalRecords"></param>
        /// <returns></returns>
        private bool IsColumnValidationForStockistOutStanding(DataTable finalRecords)
        {
            bool IsValid = false;
            try
            {
                if (finalRecords.Rows.Count > 0)
                {
                    string Div_Cd = Convert.ToString(finalRecords.Rows[0][0]);
                    string CustomerCode = Convert.ToString(finalRecords.Rows[0][1]);
                    string CustomerName = Convert.ToString(finalRecords.Rows[0][2]);
                    string City = Convert.ToString(finalRecords.Rows[0][3]);
                    string DocName = Convert.ToString(finalRecords.Rows[0][4]);
                    string DocDate = Convert.ToString(finalRecords.Rows[0][5]);
                    string DueDate = Convert.ToString(finalRecords.Rows[0][6]);
                    string OpenAmt = Convert.ToString(finalRecords.Rows[0][7]);
                    string ChqNo = Convert.ToString(finalRecords.Rows[0][8]);
                    string DistrChannel = Convert.ToString(finalRecords.Rows[0][9]);
                    string DocTypeDesc = Convert.ToString(finalRecords.Rows[0][10]);
                    string DocType = Convert.ToString(finalRecords.Rows[0][11]);
                    string OverdueAmt = Convert.ToString(finalRecords.Rows[0][12]);

                    if (Div_Cd == "Div_Cd" && CustomerCode == "CustomerCode" && CustomerName == "CustomerName" && City == "City" && DocName == "DocName" && DocDate == "DocDate" && DueDate == "DueDate" && OpenAmt == "OpenAmt" && ChqNo == "ChqNo" && DistrChannel == "DistrChannel" && DocTypeDesc == "DocTypeDesc" && DocType == "DocType" && OverdueAmt == "OverdueAmt")
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
                BusinessCont.SaveLog(0, 0, 0, "IsColumnValidationForStockistOutStanding", "Is Column Validation For Stockist OutStanding", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return IsValid;
        }
        #endregion
         
        #region Import Stockist OutStanding - BranchId, CompanyId, OSData and Addedby New
        [HttpPost]
        [Route("ChequeAccounting/ImportStockistOutStandingNew/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportStockistOutStandingNew(int BranchId, int CompanyId, string Addedby)
        {
            ImportStockistOutStandingData message = new ImportStockistOutStandingData();
            bool IsColumnFlag = false, InsertFlag = false, columnErrorFlag_DivCd = false,
                            columnErrorFlag_CustomerCode = false, columnErrorFlag_CustomerName = false,
                            columnErrorFlag_City = false, columnErrorFlag_DocName = false,
                            columnErrorFlag_DocDate = false, columnErrorFlag_DueDate = false,
                            columnErrorFlag_OpenAmt = false, columnErrorFlag_ChqNo = false,
                            columnErrorFlag_DistrChannel = false, columnErrorFlag_DocTypeDesc = false,
                            columnErrorFlag_DocType = false, columnErrorFlag_OverdueAmt = false,
                            columnErrorFlag_isAlphanumeric = false;

            string columnErrorMessage_DivCd = string.Empty, columnErrorMessage_CustomerCode = string.Empty,
                   columnErrorMessage_CustomerName = string.Empty, columnErrorMessage_City = string.Empty,
                   columnErrorMessage_DocName = string.Empty, columnErrorMessage_DocDate = string.Empty,
                   columnErrorMessage_DueDate = string.Empty, columnErrorMessage_OpenAmt = string.Empty,
                   columnErrorMessage_ChqNo = string.Empty, columnErrorMessage_DistrChannel = string.Empty,
                   columnErrorMessage_DocTypeDesc = string.Empty, columnErrorMessage_DocType = string.Empty,
                   columnErrorMessage_OverdueAmt = string.Empty, columnErrorMessage_isAlphanumeric = string.Empty;
            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var Importfor = "Import stkOutstanding";
                    var columnHeaderList = _unitOfWork.OrderDispatchRepository.GetColumnHeaderList(BranchId, CompanyId, Importfor);
                    if (columnHeaderList.Count == 0)
                    {
                        message.RetResult = "Master data is not configured, Please contact to system admin";
                        return message.RetResult;
                    }
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportStockistOutStandingTemplate.xls" || file.FileName == "ImportStockistOutStandingTemplate.XLS" || file.FileName == "ImportStockistOutStandingTemplate.xlsx" || file.FileName == "ImportStockistOutStandingTemplate.XLSX")
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
                            List<ImportStockistOutStandingData> modelList = new List<ImportStockistOutStandingData>();
                            ImportStockistOutStandingData model;

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
                                model = new ImportStockistOutStandingData();
                                model.pkId = j;
                                InsertFlag = true;
                                // Iterate through the columns dynamically using the columnIndexMapping
                                foreach (var columnIndex in columnIndexMapping.Keys)
                                {
                                    string dbColumnName = columnIndexMapping[columnIndex].Item1;
                                    string columnDataType = columnIndexMapping[columnIndex].Item2;
                                    string cellValue = finalRecords.Rows[j][columnIndex].ToString();
                                    // Div_Cd
                                    if (dbColumnName == "Div_Cd")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DivCd, ref columnErrorMessage_DivCd, columnErrorFlag_isAlphanumeric);
                                    }
                                    // CustomerCode
                                    if (dbColumnName == "CustomerCode")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_CustomerCode, ref columnErrorMessage_CustomerCode, columnErrorFlag_isAlphanumeric);
                                    }
                                    // CustomerName
                                    if (dbColumnName == "CustomerName")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_CustomerName, ref columnErrorMessage_CustomerName, columnErrorFlag_isAlphanumeric);
                                    }
                                    // City
                                    if (dbColumnName == "City")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_City, ref columnErrorMessage_City, columnErrorFlag_isAlphanumeric);
                                    }
                                    // DocName
                                    if (dbColumnName == "DocName")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DocName, ref columnErrorMessage_DocName, columnErrorFlag_isAlphanumeric);
                                    }
                                    // DocDate
                                    if (dbColumnName == "DocDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DocDate, ref columnErrorMessage_DocDate, columnErrorFlag_isAlphanumeric);
                                    }
                                    // DueDate
                                    if (dbColumnName == "DueDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DueDate, ref columnErrorMessage_DueDate, columnErrorFlag_isAlphanumeric);
                                    }
                                    // OpenAmt
                                    if (dbColumnName == "OpenAmt")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_OpenAmt, ref columnErrorMessage_OpenAmt, columnErrorFlag_isAlphanumeric);
                                    }
                                    // ChqNo
                                    if (dbColumnName == "ChqNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_ChqNo, ref columnErrorMessage_ChqNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // DistrChannel
                                    if (dbColumnName == "DistrChannel")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DistrChannel, ref columnErrorMessage_DistrChannel, columnErrorFlag_isAlphanumeric);
                                    }
                                    // DocTypeDesc
                                    if (dbColumnName == "DocTypeDesc")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DocTypeDesc, ref columnErrorMessage_DocTypeDesc, columnErrorFlag_isAlphanumeric);
                                    }
                                    // DocType
                                    if (dbColumnName == "DocType")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DocType, ref columnErrorMessage_DocType, columnErrorFlag_isAlphanumeric);
                                    }
                                    // OverdueAmt
                                    if (dbColumnName == "OverdueAmt")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_OverdueAmt, ref columnErrorMessage_OverdueAmt, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Assign values to model properties dynamically based on dbColumnName
                                    switch (dbColumnName)
                                    {
                                        case "Div_Cd":
                                            model.Div_Cd = Convert.ToInt64(cellValue);
                                            break;
                                        case "CustomerCode":
                                            model.CustomerCode = cellValue;
                                            break;
                                        case "CustomerName":
                                            model.CustomerName = cellValue;
                                            break;
                                        case "City":
                                            model.City = cellValue;
                                            break;
                                        case "DocName":
                                            model.DocName = cellValue;
                                            break;
                                        case "DocDate":
                                            model.DocDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "DueDate":
                                            model.DueDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd"); ;
                                            break;
                                        case "OpenAmt":
                                            model.OpenAmt = Convert.ToDecimal(cellValue);
                                            break;
                                        case "ChqNo":
                                            model.ChqNo = cellValue;
                                            break;
                                        case "DistrChannel":
                                            model.DistrChannel = cellValue;
                                            break;
                                        case "DocTypeDesc":
                                            model.DocTypeDesc = cellValue;
                                            break;
                                        case "DocType":
                                            model.DocType = cellValue;
                                            break;
                                        case "OverdueAmt":
                                            model.OverdueAmt = Convert.ToDecimal(cellValue);
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
                            if (columnErrorFlag_DivCd || columnErrorFlag_CustomerCode || columnErrorFlag_CustomerName || columnErrorFlag_City ||
                                columnErrorFlag_DocName || columnErrorFlag_DocDate || columnErrorFlag_DueDate || columnErrorFlag_OpenAmt ||
                                columnErrorFlag_ChqNo || columnErrorFlag_DistrChannel || columnErrorFlag_DocTypeDesc || columnErrorFlag_DocType ||
                                columnErrorFlag_OverdueAmt)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_DivCd)
                                {
                                    message.RetResult += "\n Div_Cd ---- \n " + columnErrorMessage_DivCd;
                                }
                                if (columnErrorFlag_CustomerCode)
                                {
                                    message.RetResult += "\n CustomerCode ---- \n " + columnErrorMessage_CustomerCode;
                                }
                                if (columnErrorFlag_CustomerName)
                                {
                                    message.RetResult += "\n CustomerName ---- \n " + columnErrorMessage_CustomerName;
                                }
                                if (columnErrorFlag_City)
                                {
                                    message.RetResult += "\n City ---- \n " + columnErrorMessage_City;
                                }
                                if (columnErrorFlag_DocName)
                                {
                                    message.RetResult += "\n DocName ---- \n " + columnErrorMessage_DocName;
                                }
                                if (columnErrorFlag_DocDate)
                                {
                                    message.RetResult += "\n DocDate ---- \n " + columnErrorMessage_DocDate;
                                }
                                if (columnErrorFlag_DueDate)
                                {
                                    message.RetResult += "\n DueDate ---- \n " + columnErrorMessage_DueDate;
                                }
                                if (columnErrorFlag_OpenAmt)
                                {
                                    message.RetResult += "\n OpenAmt ---- \n" + columnErrorMessage_OpenAmt;
                                }
                                if (columnErrorFlag_ChqNo)
                                {
                                    message.RetResult += "\n ChqNo ---- \n " + columnErrorMessage_ChqNo;
                                }
                                if (columnErrorFlag_DistrChannel)
                                {
                                    message.RetResult += "\n DistrChannel ---- \n " + columnErrorMessage_DistrChannel;
                                }
                                if (columnErrorFlag_DocTypeDesc)
                                {
                                    message.RetResult += "\n DocTypeDesc ---- \n " + columnErrorMessage_DocTypeDesc;
                                }
                                if (columnErrorFlag_DocType)
                                {
                                    message.RetResult += "\n DocType ---- \n " + columnErrorMessage_DocType;
                                }
                                if (columnErrorFlag_OverdueAmt)
                                {
                                    message.RetResult += "\n OverdueAmt ---- \n " + columnErrorMessage_OverdueAmt;
                                }
                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("Div_Cd");
                                dt.Columns.Add("CustomerCode");
                                dt.Columns.Add("CustomerName");
                                dt.Columns.Add("City");
                                dt.Columns.Add("DocName");
                                dt.Columns.Add("DocDate");
                                dt.Columns.Add("DueDate");
                                dt.Columns.Add("OpenAmt");
                                dt.Columns.Add("ChqNo");
                                dt.Columns.Add("DistrChannel");
                                dt.Columns.Add("DocTypeDesc");
                                dt.Columns.Add("DocType");
                                dt.Columns.Add("OverdueAmt");

                                foreach (var itemList in modelList)
                                {
                                    // Add Rows DataTable
                                    dt.Rows.Add(itemList.pkId, itemList.Div_Cd, itemList.CustomerCode, itemList.CustomerName, itemList.City, itemList.DocName, itemList.DocDate, itemList.DueDate, itemList.OpenAmt, itemList.ChqNo, itemList.DistrChannel, itemList.DocTypeDesc, itemList.DocType, itemList.OverdueAmt);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    using (var db = new CFADBEntities())
                                     {
                                        SqlConnection connection = (SqlConnection)db.Database.Connection;
                                        SqlCommand cmd = new SqlCommand("CFA.usp_ImportStockistOSData", connection);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                        BranchIdParameter.SqlDbType = SqlDbType.Int;
                                        SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                        CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                        SqlParameter OSDateParameter = cmd.Parameters.AddWithValue("@OSDate", DateTime.Now);
                                        OSDateParameter.SqlDbType = SqlDbType.DateTime;
                                        SqlParameter ImportSODataParameter = cmd.Parameters.AddWithValue("@OSData", dt);
                                        ImportSODataParameter.SqlDbType = SqlDbType.Structured;
                                        SqlParameter AddedbyParameter = cmd.Parameters.AddWithValue("@Addedby", Addedby);
                                        AddedbyParameter.SqlDbType = SqlDbType.NVarChar;
                                        SqlParameter RetValParameter = cmd.Parameters.AddWithValue("@RetVal", 0);
                                        RetValParameter.Direction = ParameterDirection.Output;
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportStockistOutStandingNew", "Import Stockist Out Standing New:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
             return message.RetResult;
        }
        #endregion

        #region Get Stockist OutStanding List
        [HttpGet]
        [Route("ChequeAccounting/StockistOutStandingList/{BranchId}/{CompId}")]
        public List<ImportStockistOutStandingModel> GetStockistOSList(int BranchId, int CompId)
        {
            List<ImportStockistOutStandingModel> StockistOSLst = new List<ImportStockistOutStandingModel>();
            try
            {
                StockistOSLst = _unitOfWork.chequeAccountingRepository.GeStockistOSLst(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistOSList", "Get Stockist OutStanding List " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockistOSLst;
        }
        #endregion

        #region Get Admin Details
        [HttpGet]
        [Route("ChequeAccounting/GetAdminDetails/{EmailFor}")]
        public List<DetailsForEmail> GetAdminDetails(int EmailFor)
        {

            List<DetailsForEmail> EmailPurposes = new List<DetailsForEmail>();
            try
            {
                EmailPurposes = _unitOfWork.chequeAccountingRepository.GetAdminDetails(EmailFor);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetAdminDetails", "", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return EmailPurposes;
        }
        #endregion

        #region Get CCEmail and Purpose Details
        [HttpGet]
        [Route("ChequeAccounting/GetCCEmailandPurposeDetails")]
        public List<DetailsForEmail> GetCCEmailandPurposeDetails()
        {
            List<DetailsForEmail> EmailPurposes = new List<DetailsForEmail>();
            try
            {
                string flag = "";
                EmailPurposes = _unitOfWork.chequeAccountingRepository.GetCCEmailandPurposeDetails(flag);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCCEmailandPurposeDetails", "", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return EmailPurposes;
        }
        #endregion

        #region Email Configuration Add
        [HttpPost]
        [Route("ChequeAccounting/EmailConfigurationAdd")]
        public string EmailConfigurationAdd([FromBody] EmailModel model)
        {

            string result = string.Empty;
            try
            {
                result = _unitOfWork.chequeAccountingRepository.EmailConfigurationAdd(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "EmailConfigurationAdd", "", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Email Configuration List
        [HttpGet]
        [Route("ChequeAccounting/GetEmailConfigList/{BranchId}/{CompanyId}")]
        public List<EmailConfigModel> GetEmailConfigList(int BranchId, int CompanyId)
        {
            List<EmailConfigModel> Result = new List<EmailConfigModel>();
            try
            {
                Result = _unitOfWork.chequeAccountingRepository.GetEmailConfigList(BranchId, CompanyId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetEmailConfigList", "Get Email Configuration List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Update Cheque Status
        [HttpPost]
        [Route("ChequeAccounting/UpdateChequeStatus")]
        public string UpdateChequeStatus([FromBody] UpdateChequeSts model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.chequeAccountingRepository.UpdateChequeStatus(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "UpdateChequeStatus", "Update Cheque Status", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Invoice For Chq Block List 
        [HttpGet]
        [Route("ChequeAccounting/GetInvoiceForChqBlockLst/{stockistId}/{CompId}/{FromDate}/{ToDate}")]
        public List<InvoiceForChqBlockModel> InvoiceForChqBlockList(int stockistId,int CompId,DateTime FromDate, DateTime ToDate)
        {
            List<InvoiceForChqBlockModel> ChequeRegisterList = new List<InvoiceForChqBlockModel>();
            try
            {
                ChequeRegisterList = _unitOfWork.chequeAccountingRepository.InvoiceForChqBlockList(stockistId ,CompId, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "Cheque Register List", "Cheque Register List " + "stockistId:  " + stockistId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ChequeRegisterList;
        }
        #endregion

        #region Import Deposited Cheque Receipt - BranchId, CompanyId, OSData and Addedby
        [HttpPost]
        [Route("ChequeAccounting/ImportDepositedCheque/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportDepositedCheque(int BranchId, int CompanyId, string Addedby)
        {
            ImportDepositedChequeData message = new ImportDepositedChequeData();

            bool IsColumnFlag = false;
            bool InsertFlag = false;

            string columnErrorMessage_StockistName = string.Empty, columnErrorMessage_StockistCode = string.Empty,
                   columnErrorMessage_DepositeDate = string.Empty, columnErrorMessage_BankName = string.Empty, columnErrorMessage_AccountNo = string.Empty,
                   columnErrorMessage_ChequeNo = string.Empty, columnErrorMessage_Amount = string.Empty;

            bool columnErrorFlag_StockistName = false, columnErrorFlag_StockistCode = false, columnErrorFlag_DepositeDate = false,
                 columnErrorFlag_BankName = false, columnErrorFlag_AccountNo = false, columnErrorFlag_ChequeNo = false, columnErrorFlag_Amount = false,
                 columnErrorFlag_isAlphanumeric = false;

            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;

                    if (file.FileName == "ImportDepositedChequeDataTemplate.xls" || file.FileName == "ImportDepositedChequeDataTemplate.XLS" || file.FileName == "ImportDepositedChequeDataTemplate.xlsx" || file.FileName == "ImportDepositedChequeDataTemplate.XLSX")
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
                            List<ImportDepositedChequeData> modelList = new List<ImportDepositedChequeData>();

                            IsColumnFlag = IsColumnValidationForDepositedCheque(finalRecords); // To Check Column Name
                            if (IsColumnFlag)
                            {
                                for (int j = 1; j < finalRecords.Rows.Count; j++)
                                {
                                    ImportDepositedChequeData model = new ImportDepositedChequeData();
                                    model.pkId = j;

                                    InsertFlag = true;

                                    // StockistName
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][0].ToString(), j, ref InsertFlag, ref columnErrorFlag_StockistName, ref columnErrorMessage_StockistName, columnErrorFlag_isAlphanumeric);
                                    // StockistCode
                                    BusinessCont.TypeCheckWithData("Alphanumeric", finalRecords.Rows[j][1].ToString(), j, ref InsertFlag, ref columnErrorFlag_StockistCode, ref columnErrorMessage_StockistCode, columnErrorFlag_isAlphanumeric);
                                    // DepositeDate
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][2].ToString(), j, ref InsertFlag, ref columnErrorFlag_DepositeDate, ref columnErrorMessage_DepositeDate, columnErrorFlag_isAlphanumeric);
                                    // BankName
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][3].ToString(), j, ref InsertFlag, ref columnErrorFlag_BankName, ref columnErrorMessage_BankName, columnErrorFlag_isAlphanumeric);
                                    // AccountNo
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][4].ToString(), j, ref InsertFlag, ref columnErrorFlag_AccountNo, ref columnErrorMessage_AccountNo, columnErrorFlag_isAlphanumeric);
                                    // ChequeNo
                                    BusinessCont.TypeCheckWithData("Long", finalRecords.Rows[j][5].ToString(), j, ref InsertFlag, ref columnErrorFlag_ChequeNo, ref columnErrorMessage_ChequeNo, columnErrorFlag_isAlphanumeric);
                                    // Amount
                                    BusinessCont.TypeCheckWithData("Decimal", finalRecords.Rows[j][6].ToString(), j, ref InsertFlag, ref columnErrorFlag_Amount, ref columnErrorMessage_Amount, columnErrorFlag_isAlphanumeric);

                                    // Valid Data - Insert Flag = true
                                    if (InsertFlag)
                                    {
                                        model.StockistName = Convert.ToString(finalRecords.Rows[j][0]);
                                        model.StockistCode = Convert.ToString(finalRecords.Rows[j][1]);
                                        model.DepositeDate = Convert.ToDateTime(finalRecords.Rows[j][2]).ToString("yyyy/MM/dd hh:mm:ss"); 
                                        model.BankName = Convert.ToString(finalRecords.Rows[j][3]);
                                        model.AccountNo = Convert.ToInt64(finalRecords.Rows[j][4]);
                                        model.ChequeNo = Convert.ToInt64(finalRecords.Rows[j][5]);
                                        model.Amount = Convert.ToDecimal(finalRecords.Rows[j][6]);
                                        modelList.Add(model);
                                    }
                                }
                            }

                            // To Check Column Error Flag Maintain Column Wise
                            if (columnErrorFlag_StockistName || columnErrorFlag_StockistCode || columnErrorFlag_DepositeDate || columnErrorFlag_BankName ||
                                columnErrorFlag_AccountNo || columnErrorFlag_ChequeNo || columnErrorFlag_Amount)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_StockistName)
                                {
                                    message.RetResult += "\n StockistName ---- \n " + columnErrorMessage_StockistName;
                                }
                                if (columnErrorFlag_StockistCode)
                                {
                                    message.RetResult += "\n StockistCode ---- \n " + columnErrorMessage_StockistCode;
                                }
                                if (columnErrorFlag_DepositeDate)
                                {
                                    message.RetResult += "\n DepositeDate ---- \n " + columnErrorMessage_DepositeDate;
                                }
                                if (columnErrorFlag_BankName)
                                {
                                    message.RetResult += "\n BankName ---- \n " + columnErrorMessage_BankName;
                                }
                                if (columnErrorFlag_AccountNo)
                                {
                                    message.RetResult += "\n AccountNo ---- \n " + columnErrorMessage_AccountNo;
                                }
                                if (columnErrorFlag_ChequeNo)
                                {
                                    message.RetResult += "\n ChequeNo ---- \n " + columnErrorMessage_ChequeNo;
                                }
                                if (columnErrorFlag_Amount)
                                {
                                    message.RetResult += "\n Amount ---- \n " + columnErrorMessage_Amount;
                                }
                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("StockistName");
                                dt.Columns.Add("StockistCode");
                                dt.Columns.Add("DepositeDate");
                                dt.Columns.Add("BankName");
                                dt.Columns.Add("AccountNo");
                                dt.Columns.Add("ChequeNo");
                                dt.Columns.Add("Amount");

                                foreach (var itemList in modelList)
                                {
                                    // Add Rows DataTable
                                    dt.Rows.Add(itemList.pkId, itemList.StockistName, itemList.StockistCode, itemList.DepositeDate, itemList.BankName, itemList.AccountNo, itemList.ChequeNo, itemList.Amount);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    using (var db = new CFADBEntities())
                                    {
                                        SqlConnection connection = (SqlConnection)db.Database.Connection;
                                        SqlCommand cmd = new SqlCommand("CFA.usp_ImportChqDepoReceiptData", connection);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                        BranchIdParameter.SqlDbType = SqlDbType.Int;
                                        SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                        CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                        SqlParameter DepoDateParameter = cmd.Parameters.AddWithValue("@DepoDate", DateTime.Now);
                                        DepoDateParameter.SqlDbType = SqlDbType.DateTime;
                                        SqlParameter ImportDepoDataParameter = cmd.Parameters.AddWithValue("@DepoData", dt);
                                        ImportDepoDataParameter.SqlDbType = SqlDbType.Structured;
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportDepositedCheque", "Import Deposited Cheque:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        /// <summary>
        /// Is Column Validation For Deposited Cheque
        /// </summary>
        /// <param name="finalRecords"></param>
        /// <returns></returns>
        private bool IsColumnValidationForDepositedCheque(DataTable finalRecords)
        {
            bool IsValid = true;

            try
            {
                if (finalRecords.Rows.Count > 0)
                {
                    string StockistName = Convert.ToString(finalRecords.Rows[0][0]);
                    string StockistCode = Convert.ToString(finalRecords.Rows[0][1]);
                    string DepositeDate = Convert.ToString(finalRecords.Rows[0][2]);
                    string BankName = Convert.ToString(finalRecords.Rows[0][3]);
                    string AccountNo = Convert.ToString(finalRecords.Rows[0][4]);
                    string ChequeNo = Convert.ToString(finalRecords.Rows[0][5]);
                    string Amount = Convert.ToString(finalRecords.Rows[0][6]);

                    if (StockistName == "StockistName" && StockistCode == "StockistCode" && DepositeDate == "DepositeDate" && BankName == "BankName" &&
                        AccountNo == "AccountNo" && ChequeNo == "ChequeNo" && Amount == "Amount")
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
                BusinessCont.SaveLog(0, 0, 0, "IsColumnValidationForDepositedCheque", "Is Column Validation For Deposited Cheque", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }

            return IsValid;
        }
        #endregion

        #region Import Deposited Cheque Receipt - BranchId, CompanyId, OSData and Addedby New
        [HttpPost]
        [Route("ChequeAccounting/ImportDepositedChequeNew/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportLrDataNew(int BranchId, int CompanyId, string Addedby)
        {
            ImportDepositedChequeData message = new ImportDepositedChequeData();

            bool IsColumnFlag = false;
            bool InsertFlag = false;

            string columnErrorMessage_StockistName = string.Empty, columnErrorMessage_StockistCode = string.Empty,
                   columnErrorMessage_DepositeDate = string.Empty, columnErrorMessage_BankName = string.Empty, columnErrorMessage_AccountNo = string.Empty,
                   columnErrorMessage_ChequeNo = string.Empty, columnErrorMessage_Amount = string.Empty;

            bool columnErrorFlag_StockistName = false, columnErrorFlag_StockistCode = false, columnErrorFlag_DepositeDate = false,
                 columnErrorFlag_BankName = false, columnErrorFlag_AccountNo = false, columnErrorFlag_ChequeNo = false, columnErrorFlag_Amount = false,
                 columnErrorFlag_isAlphanumeric = false;
            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var ImportFor = "Import Deposite";
                    var columnHeaderList = _unitOfWork.OrderDispatchRepository.GetColumnHeaderList(BranchId, CompanyId, ImportFor);
                    if (columnHeaderList.Count == 0)
                    {
                        message.RetResult = "Master data is not configured, Please contact to system admin";
                        return message.RetResult;
                    }
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportDepositedChequeDataTemplate.xls" || file.FileName == "ImportDepositedChequeDataTemplate.XLS" || file.FileName == "ImportDepositedChequeDataTemplate.xlsx" || file.FileName == "ImportDepositedChequeDataTemplate.XLSX")
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
                            List<ImportDepositedChequeData> modelList = new List<ImportDepositedChequeData>();
                            ImportDepositedChequeData model;

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
                                model = new ImportDepositedChequeData();
                                model.pkId = j;
                                InsertFlag = true;
                                // Iterate through the columns dynamically using the columnIndexMapping
                                foreach (var columnIndex in columnIndexMapping.Keys)
                                {
                                    string dbColumnName = columnIndexMapping[columnIndex].Item1;
                                    string columnDataType = columnIndexMapping[columnIndex].Item2;
                                    string cellValue = finalRecords.Rows[j][columnIndex].ToString();
                                    // StockistName
                                    if (dbColumnName == "StockistName")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_StockistName, ref columnErrorMessage_StockistName, columnErrorFlag_isAlphanumeric);
                                    }
                                    // StockistCode
                                    if (dbColumnName == "StockistCode")
                                    {
                                        BusinessCont.TypeCheckWithData("String", cellValue, j, ref InsertFlag, ref columnErrorFlag_StockistCode, ref columnErrorMessage_StockistCode, columnErrorFlag_isAlphanumeric);
                                    }
                                    // DepositeDate
                                    if (dbColumnName == "DepositeDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DepositeDate, ref columnErrorMessage_DepositeDate, columnErrorFlag_isAlphanumeric);
                                    }
                                    // BankName
                                    if (dbColumnName == "BankName")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_BankName, ref columnErrorMessage_BankName, columnErrorFlag_isAlphanumeric);
                                    }
                                    // AccountNo
                                    if (dbColumnName == "AccountNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_AccountNo, ref columnErrorMessage_AccountNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // ChequeNo
                                    if (dbColumnName == "ChequeNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_ChequeNo, ref columnErrorMessage_ChequeNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Amount
                                    if (dbColumnName == "Amount")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_Amount, ref columnErrorMessage_Amount, columnErrorFlag_isAlphanumeric);
                                    }

                                    // Assign values to model properties dynamically based on dbColumnName
                                    switch (dbColumnName)
                                    {
                                        case "StockistName":
                                            model.StockistName = cellValue;
                                            break;
                                        case "StockistCode":
                                            model.StockistCode = cellValue;
                                            break;
                                        case "DepositeDate":
                                            model.DepositeDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "BankName":
                                            model.BankName = cellValue;
                                            break;
                                        case "AccountNo":
                                            model.AccountNo = Convert.ToInt64(cellValue);
                                            break;
                                        case "ChequeNo":
                                            model.ChequeNo = Convert.ToInt64(cellValue);
                                            break;
                                        case "Amount":
                                            model.Amount = Convert.ToDecimal(cellValue);
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
                            if (columnErrorFlag_StockistName || columnErrorFlag_StockistCode || columnErrorFlag_DepositeDate || columnErrorFlag_BankName ||
                                columnErrorFlag_AccountNo || columnErrorFlag_ChequeNo || columnErrorFlag_Amount)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_StockistName)
                                {
                                    message.RetResult += "\n StockistName ---- \n " + columnErrorMessage_StockistName;
                                }
                                if (columnErrorFlag_StockistCode)
                                {
                                    message.RetResult += "\n StockistCode ---- \n " + columnErrorMessage_StockistCode;
                                }
                                if (columnErrorFlag_DepositeDate)
                                {
                                    message.RetResult += "\n DepositeDate ---- \n " + columnErrorMessage_DepositeDate;
                                }
                                if (columnErrorFlag_BankName)
                                {
                                    message.RetResult += "\n BankName ---- \n " + columnErrorMessage_BankName;
                                }
                                if (columnErrorFlag_AccountNo)
                                {
                                    message.RetResult += "\n AccountNo ---- \n " + columnErrorMessage_AccountNo;
                                }
                                if (columnErrorFlag_ChequeNo)
                                {
                                    message.RetResult += "\n ChequeNo ---- \n " + columnErrorMessage_ChequeNo;
                                }
                                if (columnErrorFlag_Amount)
                                {
                                    message.RetResult += "\n Amount ---- \n " + columnErrorMessage_Amount;
                                }
                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("StockistName");
                                dt.Columns.Add("StockistCode");
                                dt.Columns.Add("DepositeDate");
                                dt.Columns.Add("BankName");
                                dt.Columns.Add("AccountNo");
                                dt.Columns.Add("ChequeNo");
                                dt.Columns.Add("Amount");

                                foreach (var itemList in modelList)
                                {
                                    // Add Rows DataTable
                                    dt.Rows.Add(itemList.pkId, itemList.StockistName, itemList.StockistCode, itemList.DepositeDate, itemList.BankName,
                                       itemList.AccountNo, itemList.ChequeNo, itemList.Amount);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    using (var db = new CFADBEntities())
                                    {
                                        SqlConnection connection = (SqlConnection)db.Database.Connection;
                                        SqlCommand cmd = new SqlCommand("CFA.usp_ImportChqDepoReceiptData", connection);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                        BranchIdParameter.SqlDbType = SqlDbType.Int;
                                        SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                        CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                        SqlParameter DepoDateParameter = cmd.Parameters.AddWithValue("@DepoDate", DateTime.Now);
                                        DepoDateParameter.SqlDbType = SqlDbType.DateTime;
                                        SqlParameter ImportDepoDataParameter = cmd.Parameters.AddWithValue("@DepoData", dt);
                                        ImportDepoDataParameter.SqlDbType = SqlDbType.Structured;
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportDepositedChequeReceipt", "Import Deposited Cheque Receipt:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        #endregion

        #region Get Import Deposited Cheque Receipt List
        [HttpGet]
        [Route("ChequeAccounting/DepoChequeReceiptList/{BranchId}/{CompId}")]
        public List<ImportDepositedChequeModel> GetChequeReceiptList(int BranchId, int CompId)
        {
            List<ImportDepositedChequeModel> ChequeReceiptLst = new List<ImportDepositedChequeModel>();
            try
            {
                ChequeReceiptLst = _unitOfWork.chequeAccountingRepository.GetChequeReceiptLst(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetChequeReceiptList", "Get Import Deposited Cheque Receipt List " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ChequeReceiptLst;
        }
        #endregion

        #region Get Cheque Register Summary Reports List
        [HttpGet]
        [Route("ChequeAccounting/RpchequeSmmryRepoList/{BranchId}/{CompId}")]
        public List<ChequeRegstrSmmryRptModel> ChequeRegisterList(int BranchId, int CompId)
        {
            List<ChequeRegstrSmmryRptModel> ChequeRegisterLst = new List<ChequeRegstrSmmryRptModel>();
            try
            {
                ChequeRegisterLst = _unitOfWork.chequeAccountingRepository.ChequeRegisterLst(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ChequeRegisterList", "Get Cheque Register Summary Reports List " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ChequeRegisterLst;
        }
        #endregion

        #region Send Email For Outstanding Update Alert
        [HttpGet]
        [Route("ChequeAccounting/GetStkOutstandingDtlsForEmail/{BranchId}/{CompId}")]
        public List<OutStandingDtls> GetStkOutstandingDtlsForEmail(int BranchId, int CompId)
        {
            string emailSent = string.Empty;
            List<OutStandingDtls> EmailDtls = new List<OutStandingDtls>();
            try
            {
                EmailDtls = _unitOfWork.chequeAccountingRepository.GetStkOutstandingDtlsForEmail(BranchId, CompId);
                if (EmailDtls.Count > 0)
                {
                    foreach (var i in EmailDtls)
                    {
                        emailSent = _unitOfWork.chequeAccountingRepository.sendEmailForOutstanding(i.Emailid, i.BranchId, i.CompId, i.TotOverdueAmt, i.StockistName, i.OSDate);
                    }
                }
                else
                {
                    BusinessCont.SaveLog(0, 0, 0, "EmailDtls", "No Records Found", "", "");
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStkOutstandingDtlsForEmail", "Get StkOutstanding Dtls For Email " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return EmailDtls;
        }
        #endregion

        #region Get Stockist OS Doc Types Reports List
        [HttpGet]
        [Route("ChequeAccounting/OsdocTypesReport/{BranchId}/{CompId}")]
        public List<StockistOsReportModel> OsdocTypesReportList(int BranchId, int CompId)
        {
            List<StockistOsReportModel> OsdocTypesReportLst = new List<StockistOsReportModel>();
            try
            {
                OsdocTypesReportLst = _unitOfWork.chequeAccountingRepository.OsdocTypesReportLst(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "OsdocTypesReportList", "Get Stockist OS Doc Types Reports List " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OsdocTypesReportLst;
        }
        #endregion

        #region Get Cheque Summary of previous month/Week
        [HttpPost]
        [Route("ChequeAccounting/GetChqSummaryForMonthlyList")]
        public List<ChqSummaryForMonthlyModel> GetChqSummaryForMonthlyList([FromBody] ChqSummaryForMonthly model)
        {
            List<ChqSummaryForMonthlyModel> ChqSummaryForMonthlyList = new List<ChqSummaryForMonthlyModel>();
            try
            {
                ChqSummaryForMonthlyList = _unitOfWork.chequeAccountingRepository.GetChqSummaryForMonthlyList(model.CompId, model.BranchId, model.FromDate, model.ToDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetChqSummaryForMonthlyList", "Get Cheque Summary of previous month/Week List " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ChqSummaryForMonthlyList;
        }
        #endregion

        #region Get Dashboard Cheque Accounting Count
        [HttpPost]
        [Route("ChequeAccounting/GetDashbordCnt")]
        public chqaccountDashcnt GetDashbordCnt(DashBoardCommonModelNew model)
        {
            chqaccountDashcnt chqcnt = new chqaccountDashcnt();
            try
            {
                chqcnt = _unitOfWork.chequeAccountingRepository.GetDashbordCnt(model.BranchId, model.CompanyId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AllInvoiceCounts", "Get Invoice All Summary Counts", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return chqcnt;
        }
        #endregion

        #region Cheque Accounting List For Fillter
        [HttpGet]
        [Route("ChequeAccounting/DashbordChequeRegList/{BranchId}/{CompId}")]
        public List<DashbordChequeRegListModel> ChequeAccountingListForFilter(int BranchId, int CompId)
        {
            List<DashbordChequeRegListModel> chqList = new List<DashbordChequeRegListModel>();
            try
            {
                chqList = _unitOfWork.chequeAccountingRepository.CheqAccountingList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ChequeAccountingListForFilter", " Cheque Accounting List For Filter :  " + BranchId + " CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return chqList;
        }

        #endregion

        #region Cheque Accounting Over Due Stockist List For Fillter
        [HttpGet]
        [Route("ChequeAccounting/OverDueStockistList/{BranchId}/{CompId}")]
        public List<StockistOutStkList> OverDueStockistList(int BranchId, int CompId)
        {
            List<StockistOutStkList> chqList = new List<StockistOutStkList>();
            try
            {
                chqList = _unitOfWork.chequeAccountingRepository.OverDueStockistList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "OverDueStockistList", " Over Due Stockist List :  " + BranchId + " CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return chqList;
        }

        #endregion

        #region Get Cheque Status Report month
        [HttpPost]
        [Route("ChequeAccounting/GetChequeStatusReportList")]
        public List<ChequeStatusReportModel> GetChequeStatusReportList ([FromBody] ChqStatusReport model)
        {
            List<ChequeStatusReportModel> chqStatusRep = new List<ChequeStatusReportModel>();
            try
            {
                chqStatusRep = _unitOfWork.chequeAccountingRepository.GetChequeStatusReportList(model.BranchId, model.CompId, model.FromDate, model.ToDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetChequeStatusReportList", "Get Cheque Status Report List " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return chqStatusRep;
        }
        #endregion

        #region Chq Reg Cumm Deposited List
        [HttpGet]
        [Route("ChequeAccounting/ChqRegCummDepositedList/{BranchId}/{CompId}")]
        public List<DashbordChequeRegListModel> ChqRegCummDepositedList(int BranchId, int CompId)
        {
            List<DashbordChequeRegListModel> chqList = new List<DashbordChequeRegListModel>();
            try
            {
                chqList = _unitOfWork.chequeAccountingRepository.ChqRegCummDepositedList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ChqRegCummDepositedList", " Chq Reg Cumm Deposited List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return chqList;
        }
        #endregion

        #region Get Owner Chq Acc Dash Smmry List
        [HttpGet]
        [Route("ChequeAccounting/GetOwnerChqAccDashSmmryList")]
        public List<OwnChqAccDashSmmryList> GetOwnerChqAccDashSmmryList()
        {
            List<OwnChqAccDashSmmryList> chqList = new List<OwnChqAccDashSmmryList>();
            try
            {
                chqList = _unitOfWork.chequeAccountingRepository.GetOwnerChqAccDashSmmryList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerChqAccDashSmmryList", " Get Owner Chq Acc Dash Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return chqList;
        }

        #endregion

    }
}
