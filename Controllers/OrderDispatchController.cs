using CNF.Business.BusinessConstant;
using CNF.Business.Model.ChequeAccounting;
using CNF.Business.Model.Context;
using CNF.Business.Model.Login;
using CNF.Business.Model.OrderDispatch;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CNF.API.Controllers
{
    public class OrderDispatchController : BaseApiController
    {
        #region PickListHeader list & AddEdit
        [HttpPost]
        [Route("OrderDispatch/PickListHeaderAddEdit")]
        public string PickListHeaderAddEdit([FromBody] PickListModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.PickListHeaderAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PickListHeaderAddEdit", "Picklist AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Picklist By BranchId and CompanyId and PicklistDate
        [HttpPost]
        [Route("OrderDispatch/GetPickList")]
        public List<PickListModel> GetPickList(PickListModel model)
        {
            List<PickListModel> pickList = new List<PickListModel>();
            try
            {
                pickList = _unitOfWork.OrderDispatchRepository.GetPickLst(model.BranchId, model.CompId, model.PicklistDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickList", "Get Pick List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return pickList;
        }
        #endregion

        #region Send Notification To Stockist
        [HttpPost]
        [Route("OrderDispatch/sendEmail")]
        public string sendEmail(UserModel model)
        {
            string emailsts = string.Empty;
            try
            {
                emailsts = _unitOfWork.OrderDispatchRepository.sendEmail(model.Email, model.PicklistNo);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "sendEmail", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return emailsts;
        }
        #endregion

        #region Send Notification To Picker
        [HttpPost]
        [Route("OrderDispatch/sendEmailtopicker")]
        public string sendEmailtopicker(UserModel model)
        {
            string emailsts = string.Empty;
            try
            {
                emailsts = _unitOfWork.OrderDispatchRepository.sendEmailToPicker(model.Email, model.PicklistNo);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "sendEmailtopicker", DateTime.Now.ToString(), BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return emailsts;
        }
        #endregion

        #region Picklist Allotment Add
        [HttpPost]
        [Route("OrderDispatch/PicklistAllotmentAdd")]
        public string PicklistAllotmentAdd([FromBody] PicklstAllotReallotModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.PicklistAllotmentAdd(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PicklistAllotmentAdd", "Picklist Allotment Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Picklist ReAllotment Add
        [HttpPost]
        [Route("OrderDispatch/PicklistReAllotmentAdd")]
        public string PicklistReAllotmentAdd([FromBody] PicklstAllotReallotModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.PicklistReAllotmentAdd(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PicklistAllotmentAdd", "Picklist ReAllotment Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Picklist Allotment Status
        [HttpPost]
        [Route("OrderDispatch/PicklistAllotmentStatus")]
        public string PicklistAllotmentStatus([FromBody] PicklistAllotmentStatusModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.PicklistAllotmentStatus(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PicklistAllotmentStatus", "Picklist Allotment Status", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Invoice Header List
        [HttpPost]
        [Route("OrderDispatch/GetInvoiceHeaderList")]
        public List<InvoiceLstModel> GetInvoiceHeaderList(InvoiceLstModel model)
        {
            List<InvoiceLstModel> InvoiceLst = new List<InvoiceLstModel>();
            try
            {
                InvoiceLst = _unitOfWork.OrderDispatchRepository.GetInvoiceHeaderLst(model.BranchId, model.CompId, model.FromDate, model.ToDate, model.BillDrawerId, model.InvStatus);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceHeaderList", "Get Invoice Header List " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Get Invoice Header List
        [HttpPost]
        [Route("OrderDispatch/GetInvoiceHeaderListForPriority")]
        public List<InvoiceLstModel> GetInvoiceHeaderListForPriority(InvoiceLstModel model)
        {
            List<InvoiceLstModel> InvoiceLst = new List<InvoiceLstModel>();
            try
            {
                InvoiceLst = _unitOfWork.OrderDispatchRepository.GetInvoiceHeaderListForPriority(model.BranchId, model.CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceHeaderList", "Get Invoice Header List " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Get Invoice Header List for Mob
        [HttpPost]
        [Route("OrderDispatch/GetInvoiceHeaderListForMob")]
        public List<InvoiceLstModel> GetInvoiceHeaderListForMob(InvoiceLstModel model)
        {
            List<InvoiceLstModel> InvoiceLst = new List<InvoiceLstModel>();
            try
            {
                InvoiceLst = _unitOfWork.OrderDispatchRepository.GetInvoiceHeaderLstForMob(model.BranchId, model.CompId, model.FromDate, model.ToDate, model.BillDrawerId, model.InvStatus);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceHeaderListForMob", "Get Invoice Header List For Mob " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Import Invoice Data - BranchId, CompanyId and Addedby
        [HttpPost]
        [Route("OrderDispatch/ImportInvoiceData/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportInvoiceData(int BranchId, int CompanyId, string Addedby)
        {
            ImportReturnModel message = new ImportReturnModel();

            bool IsColumnFlag = false;
            bool InsertFlag = false;

            string columnErrorMessage_InvoiceNo = string.Empty, columnErrorMessage_InvoiceCreateDate = string.Empty,
                   columnErrorMessage_InvoiceAmount = string.Empty, columnErrorMessage_SoldToCode = string.Empty,
                   columnErrorMessage_SoldToName = string.Empty, columnErrorMessage_SoldToCity = string.Empty;

            bool columnErrorFlag_InvoiceNo = false, columnErrorFlag_InvoiceCreateDate = false,
                 columnErrorFlag_InvoiceAmount = false, columnErrorFlag_SoldToCode = false,
                 columnErrorFlag_SoldToName = false, columnErrorFlag_SoldToCity = false, columnErrorFlag_isAlphanumeric = false;

            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportInvoiceDataTemplate.xls" || file.FileName == "ImportInvoiceDataTemplate.XLS" || file.FileName == "ImportInvoiceDataTemplate.xlsx" || file.FileName == "ImportInvoiceDataTemplate.XLSX")
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
                            List<ImportInvoiceDataModel> modelList = new List<ImportInvoiceDataModel>();
                            ImportInvoiceDataModel model;
                            IsColumnFlag = IsColumnValidationForInvoice(finalRecords); // To Check Column Name
                            if (IsColumnFlag)
                            {
                                for (int j = 1; j < finalRecords.Rows.Count; j++)
                                {
                                    model = new ImportInvoiceDataModel();
                                    model.pkId = j;

                                    InsertFlag = true;

                                    // InvoiceNo
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][0].ToString(), j, ref InsertFlag, ref columnErrorFlag_InvoiceNo, ref columnErrorMessage_InvoiceNo, columnErrorFlag_isAlphanumeric);
                                    // InvoiceCreateDate
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][1].ToString(), j, ref InsertFlag, ref columnErrorFlag_InvoiceCreateDate, ref columnErrorMessage_InvoiceCreateDate, columnErrorFlag_isAlphanumeric);
                                    // InvoiceAmount
                                    BusinessCont.TypeCheckWithData("Decimal", finalRecords.Rows[j][2].ToString(), j, ref InsertFlag, ref columnErrorFlag_InvoiceAmount, ref columnErrorMessage_InvoiceAmount, columnErrorFlag_isAlphanumeric);
                                    // SoldToCode
                                    BusinessCont.TypeCheckWithData("Alphanumeric", finalRecords.Rows[j][3].ToString(), j, ref InsertFlag, ref columnErrorFlag_SoldToCode, ref columnErrorMessage_SoldToCode, columnErrorFlag_isAlphanumeric);
                                    // SoldToName
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][4].ToString(), j, ref InsertFlag, ref columnErrorFlag_SoldToName, ref columnErrorMessage_SoldToName, columnErrorFlag_isAlphanumeric);
                                    // SoldToCity
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][5].ToString(), j, ref InsertFlag, ref columnErrorFlag_SoldToCity, ref columnErrorMessage_SoldToCity, columnErrorFlag_isAlphanumeric);

                                    // Valid Data - Insert Flag = true
                                    if (InsertFlag)
                                    {
                                        model.InvoiceNo = Convert.ToString(finalRecords.Rows[j][0]);
                                        model.InvoiceCreateDate = Convert.ToDateTime(finalRecords.Rows[j][1]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.InvoiceAmount = Convert.ToDecimal(finalRecords.Rows[j][2]);
                                        model.SoldToCode = Convert.ToString(finalRecords.Rows[j][3]);
                                        model.SoldToName = Convert.ToString(finalRecords.Rows[j][4]);
                                        model.SoldToCity = Convert.ToString(finalRecords.Rows[j][5]);
                                        modelList.Add(model);
                                    }
                                }

                                // To Check Column Error Flag Maintain Column Wise
                                if (columnErrorFlag_InvoiceNo || columnErrorFlag_InvoiceCreateDate ||
                                    columnErrorFlag_InvoiceAmount || columnErrorFlag_SoldToCode ||
                                    columnErrorFlag_SoldToName || columnErrorFlag_SoldToCity)
                                {
                                    message.RetResult = "\n Below Columns has invalid data:  ";
                                    if (columnErrorFlag_InvoiceNo)
                                    {
                                        message.RetResult += "\n InvoiceNo ---- \n " + columnErrorMessage_InvoiceNo;
                                    }
                                    if (columnErrorFlag_InvoiceCreateDate)
                                    {
                                        message.RetResult += "\n InvoiceCreateDate ---- \n " + columnErrorMessage_InvoiceCreateDate;
                                    }
                                    if (columnErrorFlag_InvoiceAmount)
                                    {
                                        message.RetResult += "\n InvoiceAmount ---- \n" + columnErrorMessage_InvoiceAmount;
                                    }
                                    if (columnErrorFlag_SoldToCode)
                                    {
                                        message.RetResult += "\n SoldToCode ---- \n " + columnErrorMessage_SoldToCode;
                                    }
                                    if (columnErrorFlag_SoldToName)
                                    {
                                        message.RetResult += "\n SoldToName ---- \n " + columnErrorMessage_SoldToName;
                                    }
                                    if (columnErrorFlag_SoldToCity)
                                    {
                                        message.RetResult += "\n SoldToCity ---- \n " + columnErrorMessage_SoldToCity;
                                    }
                                }

                                if (modelList.Count > 0)
                                {
                                    // Create DataTable
                                    DataTable dt = new DataTable();
                                    dt.Columns.Add("pkId");
                                    dt.Columns.Add("InvoiceNo");
                                    dt.Columns.Add("InvoiceCreateDate");
                                    dt.Columns.Add("InvoiceAmount");
                                    dt.Columns.Add("SoldToCode");
                                    dt.Columns.Add("SoldToName");
                                    dt.Columns.Add("SoldToCity");

                                    foreach (var itemList in modelList)
                                    {
                                        // Add Rows DataTable
                                        dt.Rows.Add(itemList.pkId, itemList.InvoiceNo, itemList.InvoiceCreateDate, itemList.InvoiceAmount, itemList.SoldToCode,
                                           itemList.SoldToName, itemList.SoldToCity);
                                    }

                                    if (dt.Rows.Count > 0)
                                    {
                                        using (var db = new CFADBEntities())
                                        {
                                            {
                                                SqlConnection connection = (SqlConnection)db.Database.Connection;
                                                SqlCommand cmd = new SqlCommand("CFA.usp_ImportInvoiceData", connection);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                                BranchIdParameter.SqlDbType = SqlDbType.Int;
                                                SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                                CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                                SqlParameter ImportInvDataParameter = cmd.Parameters.AddWithValue("@ImportInvData", dt);
                                                ImportInvDataParameter.SqlDbType = SqlDbType.Structured;
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportInvoiceData", "Import Invoice Upload Data:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        /// <summary>
        /// Is Column Validation For Invoice
        /// </summary>
        /// <param name="finalRecords"></param>
        /// <returns></returns>
        private bool IsColumnValidationForInvoice(DataTable finalRecords)
        {
            bool IsValid = false;

            try
            {
                string InvoiceNo = Convert.ToString(finalRecords.Rows[0][0]);
                string InvoiceCreateDate = Convert.ToString(finalRecords.Rows[0][1]);
                string InvoiceAmount = Convert.ToString(finalRecords.Rows[0][2]);
                string SoldToCode = Convert.ToString(finalRecords.Rows[0][3]);
                string SoldToName = Convert.ToString(finalRecords.Rows[0][4]);
                string SoldToCity = Convert.ToString(finalRecords.Rows[0][5]);

                if (InvoiceNo == "InvoiceNo" && InvoiceCreateDate == "InvoiceCreateDate" &&
                    InvoiceAmount == "InvoiceAmount" && SoldToCode == "SoldToCode" &&
                    SoldToName == "SoldToName" && SoldToCity == "SoldToCity"
                    )
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "IsColumnValidationForInvoice", "Is Column Validation For Invoice", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return IsValid;
        }
        #endregion

        #region New Import Invoice Data - BranchId, CompanyId and Addedby
        [HttpPost]
        [Route("OrderDispatch/ImportInvoiceDataNew/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportInvoiceDataNew(int BranchId, int CompanyId, string Addedby)
        {
            ImportReturnModel message = new ImportReturnModel();

            bool IsColumnFlag = false;
            bool InsertFlag = false;

            string columnErrorMessage_InvoiceNo = string.Empty, columnErrorMessage_InvoiceCreateDate = string.Empty,
                   columnErrorMessage_InvoiceAmount = string.Empty, columnErrorMessage_SoldToCode = string.Empty,
                   columnErrorMessage_SoldToName = string.Empty, columnErrorMessage_SoldToCity = string.Empty;

            bool columnErrorFlag_InvoiceNo = false, columnErrorFlag_InvoiceCreateDate = false,
                 columnErrorFlag_InvoiceAmount = false, columnErrorFlag_SoldToCode = false,
                 columnErrorFlag_SoldToName = false, columnErrorFlag_SoldToCity = false, columnErrorFlag_isAlphanumeric = false;

            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var Importfor = "Import Invoice";
                    var columnHeaderList = _unitOfWork.OrderDispatchRepository.GetColumnHeaderList(BranchId, CompanyId, Importfor);
                    if (columnHeaderList.Count == 0)
                    {
                        message.RetResult = "Master data is not configured, Please contact to system admin";
                        return message.RetResult;
                    }
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportInvoiceDataTemplate.xls" || file.FileName == "ImportInvoiceDataTemplate.XLS" || file.FileName == "ImportInvoiceDataTemplate.xlsx" || file.FileName == "ImportInvoiceDataTemplate.XLSX")
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
                            List<ImportInvoiceDataModel> modelList = new List<ImportInvoiceDataModel>();
                            ImportInvoiceDataModel model;

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
                                model = new ImportInvoiceDataModel();
                                model.pkId = j;
                                InsertFlag = true;
                                // Iterate through the columns dynamically using the columnIndexMapping
                                foreach (var columnIndex in columnIndexMapping.Keys)
                                {
                                    string dbColumnName = columnIndexMapping[columnIndex].Item1;
                                    string columnDataType = columnIndexMapping[columnIndex].Item2;
                                    string cellValue = finalRecords.Rows[j][columnIndex].ToString();
                                    // InvoiceNo
                                    if (dbColumnName == "InvoiceNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_InvoiceNo, ref columnErrorMessage_InvoiceNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // InvoiceCreateDate
                                    if (dbColumnName == "InvoiceCreateDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_InvoiceCreateDate, ref columnErrorMessage_InvoiceCreateDate, columnErrorFlag_isAlphanumeric);
                                    }
                                    // InvoiceAmount
                                    if (dbColumnName == "InvoiceAmount")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_InvoiceAmount, ref columnErrorMessage_InvoiceAmount, columnErrorFlag_isAlphanumeric);
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

                                    // Assign values to model properties dynamically based on dbColumnName
                                    switch (dbColumnName)
                                    {
                                        case "InvoiceNo":
                                            model.InvoiceNo = cellValue;
                                            break;
                                        case "InvoiceCreateDate":
                                            model.InvoiceCreateDate = cellValue;
                                            break;
                                        case "InvoiceAmount":
                                            model.InvoiceAmount = Convert.ToDecimal(cellValue);
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
                                    }
                                }

                                // Valid Data -Insert Flag = true
                                if (InsertFlag)
                                {
                                    modelList.Add(model);
                                }
                            }

                            // To Check Column Error Flag Maintain Column Wise
                            if (columnErrorFlag_InvoiceNo || columnErrorFlag_InvoiceCreateDate ||
                                columnErrorFlag_InvoiceAmount || columnErrorFlag_SoldToCode ||
                                columnErrorFlag_SoldToName || columnErrorFlag_SoldToCity)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_InvoiceNo)
                                {
                                    message.RetResult += "\n InvoiceNo ---- \n " + columnErrorMessage_InvoiceNo;
                                }
                                if (columnErrorFlag_InvoiceCreateDate)
                                {
                                    message.RetResult += "\n InvoiceCreateDate ---- \n " + columnErrorMessage_InvoiceCreateDate;
                                }
                                if (columnErrorFlag_InvoiceAmount)
                                {
                                    message.RetResult += "\n InvoiceAmount ---- \n" + columnErrorMessage_InvoiceAmount;
                                }
                                if (columnErrorFlag_SoldToCode)
                                {
                                    message.RetResult += "\n SoldToCode ---- \n " + columnErrorMessage_SoldToCode;
                                }
                                if (columnErrorFlag_SoldToName)
                                {
                                    message.RetResult += "\n SoldToName ---- \n " + columnErrorMessage_SoldToName;
                                }
                                if (columnErrorFlag_SoldToCity)
                                {
                                    message.RetResult += "\n SoldToCity ---- \n " + columnErrorMessage_SoldToCity;
                                }
                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("InvoiceNo");
                                dt.Columns.Add("InvoiceCreateDate");
                                dt.Columns.Add("InvoiceAmount");
                                dt.Columns.Add("SoldToCode");
                                dt.Columns.Add("SoldToName");
                                dt.Columns.Add("SoldToCity");

                                foreach (var itemList in modelList)
                                {
                                    // Add Rows DataTable
                                    dt.Rows.Add(itemList.pkId, itemList.InvoiceNo, itemList.InvoiceCreateDate, itemList.InvoiceAmount, itemList.SoldToCode,
                                       itemList.SoldToName, itemList.SoldToCity);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    using (var db = new CFADBEntities())
                                    {
                                        {
                                            SqlConnection connection = (SqlConnection)db.Database.Connection;
                                            SqlCommand cmd = new SqlCommand("CFA.usp_ImportInvoiceData", connection);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                            BranchIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                            CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter ImportInvDataParameter = cmd.Parameters.AddWithValue("@ImportInvData", dt);
                                            ImportInvDataParameter.SqlDbType = SqlDbType.Structured;
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportInvoiceData", "Import Invoice Upload Data:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        #endregion

        #region Get Alloted Picklist For Picker
        [HttpPost]
        [Route("OrderDispatch/GetAllotedPickListForPicker")]
        public List<Picklstmodel> GetAllotedPickListForPicker(Picklstmodel model)
        {
            List<Picklstmodel> picklstmodels = new List<Picklstmodel>();
            try
            {
                picklstmodels = _unitOfWork.OrderDispatchRepository.GetAllotedPickListForPicker(model.BranchId, model.CompId, model.PickerId, model.PicklistDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetAllotedPickListForPicker", "Get Alloted Picklist For Picker", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return picklstmodels;
        }
        #endregion

        #region Invoice Header Status Update
        [HttpPost]
        [Route("OrderDispatch/InvoiceHeaderStatusUpdate")]
        public string InvoiceHeaderStatusUpdate([FromBody] InvoiceHeaderStatusUpdateModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.InvoiceHeaderStatusUpdate(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "InvoiceHeaderStatusUpdate", "Invoice Header Status Update " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Assign Transport Mode
        [HttpPost]
        [Route("OrderDispatch/AssignTransportMode")]
        public string AssignTransportMode([FromBody] AssignTransportModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.AssignTransportMode(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AssignTransportMode", "Assign Transport Mode", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get PickList Summary Data
        [HttpPost]
        [Route("OrderDispatch/GetPickListSummaryData")]
        public PickLstSummaryData GetPickListSummaryData(PickLstSummaryData model)
        {
            PickLstSummaryData PicklistData = new PickLstSummaryData();
            try
            {
                PicklistData = _unitOfWork.OrderDispatchRepository.GetPickListSummaryData(model.BranchId, model.CompId, model.PickerId, model.PicklistDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListSummaryData", "Get Picklist Summary Data  ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PicklistData;
        }
        #endregion

        #region Get PickList Summary Data
        [HttpPost]
        [Route("OrderDispatch/GetPickListSummaryCounts")]
        public PickLstSummaryData GetPickListSummaryCounts(PickLstSummaryData model)
        {
            PickLstSummaryData PicklistData = new PickLstSummaryData();
            try
            {
                PicklistData = _unitOfWork.OrderDispatchRepository.GetPickListSummaryCounts(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListSummaryData", "Get Picklist Summary Data  ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PicklistData;
        }
        #endregion

        #region Invoice Header List For Assign Trans Mode
        [HttpPost]
        [Route("OrderDispatch/InvoiceHeaderListForAssignTransMode")]
        public List<InvoiceLstModel> InvoiceListForAssignTransMode(InvoiceLstModel model)
        {
            List<InvoiceLstModel> InvoiceLst = new List<InvoiceLstModel>();
            try
            {
                InvoiceLst = _unitOfWork.OrderDispatchRepository.InvoiceListForAssignTransMode(model.BranchId, model.CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "InvoiceListForAssignTransMode", "Invoice Header List For Assign Trans Mode " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Import LR Data - Addedby
        [HttpPost]
        [Route("OrderDispatch/ImportLrData/{Addedby}")]
        public string ImportLrData(string Addedby)
        {
            ImportLRNewModel message = new ImportLRNewModel();
            bool IsColumnFlag = false, InsertFlag = false, columnErrorFlag_InvoiceNo = false,
                 columnErrorFlag_InvoiceDate = false, columnErrorFlag_LRNo = false,
                 columnErrorFlag_LRDate = false, columnErrorFlag_LRBox = false;
            string columnErrorMessage_InvoiceNo = string.Empty, columnErrorMessage_InvoiceDate = string.Empty,
                   columnErrorMessage_LRNo = string.Empty, columnErrorMessage_LRDate = string.Empty, 
                   columnErrorMessage_LRBox = string.Empty;
            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;

                    if (file.FileName == "ImportLRDataTemplate.xls" || file.FileName == "ImportLRDataTemplate.XLS" || file.FileName == "ImportLRDataTemplate.xlsx" || file.FileName == "ImportLRDataTemplate.XLSX")
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
                            List<ImportLRNewModel> modelList = new List<ImportLRNewModel>();
                            ImportLRNewModel model;
                            IsColumnFlag = IsColumnValidation(finalRecords); // To Check Column Name
                            if (IsColumnFlag)
                            {
                                for (int j = 1; j < finalRecords.Rows.Count; j++)  // To Row Values Append
                                {
                                    model = new ImportLRNewModel();
                                    model.pkId = j;

                                    InsertFlag = true;
                                    // Invoice No
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][0].ToString(), j, ref InsertFlag, ref columnErrorFlag_InvoiceNo, ref columnErrorMessage_InvoiceNo);
                                    // Invoice Date
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][1].ToString(), j, ref InsertFlag, ref columnErrorFlag_InvoiceDate, ref columnErrorMessage_InvoiceDate);
                                    // LRNo
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][2].ToString(), j, ref InsertFlag, ref columnErrorFlag_LRNo, ref columnErrorMessage_LRNo);
                                    // LR Date
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][3].ToString(), j, ref InsertFlag, ref columnErrorFlag_LRDate, ref columnErrorMessage_LRDate);
                                    // LR Box
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][4].ToString(), j, ref InsertFlag, ref columnErrorFlag_LRBox, ref columnErrorMessage_LRBox);

                                    // Valid Data - Insert Flag = true
                                    if (InsertFlag)
                                    {
                                        model.InvoiceNo = Convert.ToString(finalRecords.Rows[j][0]);
                                        model.InvoiceDate = Convert.ToDateTime(finalRecords.Rows[j][1]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.LRNo = Convert.ToString(finalRecords.Rows[j][2]);
                                        model.LRDate = Convert.ToDateTime(finalRecords.Rows[j][3]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.LRBox = Convert.ToString(finalRecords.Rows[j][4]);
                                        modelList.Add(model);
                                    }
                                }

                                // To Check Column Error Flag Maintain Column Wise
                                if (columnErrorFlag_InvoiceNo || columnErrorFlag_InvoiceDate || columnErrorFlag_LRNo ||
                                    columnErrorFlag_LRDate || columnErrorFlag_LRBox)
                                {
                                    message.RetResult = "\n Below Columns has invalid data:  ";
                                    if (columnErrorFlag_InvoiceNo)
                                    {
                                        message.RetResult += "\n Invoice No ---- \n " + columnErrorMessage_InvoiceNo;
                                    }
                                    if (columnErrorFlag_InvoiceDate)
                                    {
                                        message.RetResult += "\n Invoice Date ---- \n " + columnErrorMessage_InvoiceDate;
                                    }
                                    if (columnErrorFlag_LRNo)
                                    {
                                        message.RetResult += "\n LR No ---- \n " + columnErrorMessage_LRNo;
                                    }
                                    if (columnErrorFlag_LRDate)
                                    {
                                        message.RetResult += "\n LR Date ---- \n " + columnErrorMessage_LRDate;
                                    }
                                    if (columnErrorFlag_LRBox)
                                    {
                                        message.RetResult += "\n LR Box ---- \n " + columnErrorMessage_LRBox;
                                    }
                                }

                                if (modelList.Count > 0)
                                {
                                    // Create DataTable
                                    DataTable dt = new DataTable();
                                    dt.Columns.Add("pkId");
                                    dt.Columns.Add("InvoiceNo");
                                    dt.Columns.Add("InvoiceDate");
                                    dt.Columns.Add("LRNo");
                                    dt.Columns.Add("LRDate");
                                    dt.Columns.Add("LRBox");

                                    foreach (var itemList in modelList)
                                    {
                                        // Add Rows DataTable
                                        dt.Rows.Add(itemList.pkId, itemList.InvoiceNo, itemList.InvoiceDate, itemList.LRNo, itemList.LRDate, itemList.LRBox);
                                    }

                                    if (dt.Rows.Count > 0)
                                    {
                                        using (var db = new CFADBEntities())
                                        {
                                            {
                                                SqlConnection connection = (SqlConnection)db.Database.Connection;
                                                SqlCommand cmd = new SqlCommand("CFA.usp_ImportLRData", connection);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                SqlParameter LRDataParameter = cmd.Parameters.AddWithValue("@LRData", dt);
                                                LRDataParameter.SqlDbType = SqlDbType.Structured;
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
                BusinessCont.SaveLog(0, 0, 0, "ImportLRData", "Import LR Upload Data:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        /// <summary>
        /// Is Column Validation
        /// </summary>
        /// <param name="finalRecords"></param>
        /// <returns></returns>
        private bool IsColumnValidation(DataTable finalRecords)
        {
            bool IsValid = false;
            try
            {
                if (finalRecords.Rows.Count > 0)
                {
                    string InvoiceNo = Convert.ToString(finalRecords.Rows[0][0]);
                    string InvoiceDate = Convert.ToString(finalRecords.Rows[0][1]);
                    string LRNo = Convert.ToString(finalRecords.Rows[0][2]);
                    string LRDate = Convert.ToString(finalRecords.Rows[0][3]);
                    string LRBox = Convert.ToString(finalRecords.Rows[0][4]);

                    if (InvoiceNo == "InvoiceNo" && InvoiceDate == "InvoiceDate" && LRNo == "LRNo" &&
                        LRDate == "LRDate" && LRBox == "LRBox")
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
                BusinessCont.SaveLog(0, 0, 0, "IsColumnValidation", "Is Column Validation", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return IsValid;
        }

        #endregion

        #region New Import LR Data - BranchId, CompanyId and Addedby
        [HttpPost]
        [Route("OrderDispatch/ImportLrDataNew/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportLrDataNew(int BranchId, int CompanyId, string Addedby)
        {
            ImportReturnModel message = new ImportReturnModel();

            bool IsColumnFlag = false, InsertFlag = false, columnErrorFlag_InvoiceNo = false,
                 columnErrorFlag_InvoiceDate = false, columnErrorFlag_LRNo = false,
                 columnErrorFlag_LRDate = false, columnErrorFlag_LRBox = false, columnErrorFlag_isAlphanumeric = false;
            string columnErrorMessage_InvoiceNo = string.Empty, columnErrorMessage_InvoiceDate = string.Empty,
                   columnErrorMessage_LRNo = string.Empty, columnErrorMessage_LRDate = string.Empty,
                   columnErrorMessage_LRBox = string.Empty;
            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var ImportFor = "Import LR";
                    var columnHeaderList = _unitOfWork.OrderDispatchRepository.GetColumnHeaderList(BranchId, CompanyId, ImportFor);
                    if (columnHeaderList.Count == 0)
                    {
                        message.RetResult = "Master data is not configured, Please contact to system admin";
                        return message.RetResult;
                    }
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportLRDataTemplate.xls" || file.FileName == "ImportLRDataTemplate.XLS" || file.FileName == "ImportLRDataTemplate.xlsx" || file.FileName == "ImportLRDataTemplate.XLSX")
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
                            List<ImportLRNewModel> modelList = new List<ImportLRNewModel>();
                            ImportLRNewModel model;

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
                                model = new ImportLRNewModel();
                                model.pkId = j;
                                InsertFlag = true;
                                // Iterate through the columns dynamically using the columnIndexMapping
                                foreach (var columnIndex in columnIndexMapping.Keys)
                                {
                                    string dbColumnName = columnIndexMapping[columnIndex].Item1;
                                    string columnDataType = columnIndexMapping[columnIndex].Item2;
                                    string cellValue = finalRecords.Rows[j][columnIndex].ToString();
                                    // InvoiceNo
                                    if (dbColumnName == "InvoiceNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_InvoiceNo, ref columnErrorMessage_InvoiceNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // InvoiceDate
                                    if (dbColumnName == "InvoiceDate")
                                    {
                                        BusinessCont.TypeCheckWithData("String", cellValue, j, ref InsertFlag, ref columnErrorFlag_InvoiceDate, ref columnErrorMessage_InvoiceDate, columnErrorFlag_isAlphanumeric);
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
                                    // LRBox
                                    if (dbColumnName == "LRBox")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_LRBox, ref columnErrorMessage_LRBox, columnErrorFlag_isAlphanumeric);
                                    }

                                    // Assign values to model properties dynamically based on dbColumnName
                                    switch (dbColumnName)
                                    {
                                        case "InvoiceNo":
                                            model.InvoiceNo = cellValue;
                                            break;
                                        case "InvoiceDate":
                                            model.InvoiceDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "LRNo":
                                            model.LRNo = cellValue;
                                            break;
                                        case "LRDate":
                                            model.LRDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "LRBox":
                                            model.LRBox = cellValue;
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
                            if (columnErrorFlag_InvoiceNo || columnErrorFlag_InvoiceDate || columnErrorFlag_LRNo ||
                                columnErrorFlag_LRDate || columnErrorFlag_LRBox)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_InvoiceNo)
                                {
                                    message.RetResult += "\n Invoice No ---- \n " + columnErrorMessage_InvoiceNo;
                                }
                                if (columnErrorFlag_InvoiceDate)
                                {
                                    message.RetResult += "\n Invoice Date ---- \n " + columnErrorMessage_InvoiceDate;
                                }
                                if (columnErrorFlag_LRNo)
                                {
                                    message.RetResult += "\n LR No ---- \n " + columnErrorMessage_LRNo;
                                }
                                if (columnErrorFlag_LRDate)
                                {
                                    message.RetResult += "\n LR Date ---- \n " + columnErrorMessage_LRDate;
                                }
                                if (columnErrorFlag_LRBox)
                                {
                                    message.RetResult += "\n LR Box ---- \n " + columnErrorMessage_LRBox;
                                }
                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("InvoiceNo");
                                dt.Columns.Add("InvoiceDate");
                                dt.Columns.Add("LRNo");
                                dt.Columns.Add("LRDate");
                                dt.Columns.Add("LRBox");

                                foreach (var itemList in modelList)
                                {
                                    // Add Rows DataTable
                                    dt.Rows.Add(itemList.pkId, itemList.InvoiceNo, itemList.InvoiceDate, itemList.LRNo, itemList.LRDate,
                                       itemList.LRBox);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    using (var db = new CFADBEntities())
                                    {
                                        {
                                            SqlConnection connection = (SqlConnection)db.Database.Connection;
                                            SqlCommand cmd = new SqlCommand("CFA.usp_ImportLRDataNew", connection);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                            BranchIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                            CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter LRDataParameter = cmd.Parameters.AddWithValue("@LRData", dt);
                                            LRDataParameter.SqlDbType = SqlDbType.Structured;
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportInvoiceData", "Import Invoice Upload Data:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        #endregion

        #region Generate PDF, QR Coode
        [HttpPost]
        [Route("OrderDispatch/GeneratePDF")]
        public string GeneratePDF(GeneratePDFModel model)
        {
            string msgPDF = string.Empty;
            PrintPDFDataModel modelPdf = new PrintPDFDataModel();  // new instance created
            try
            {
                modelPdf.BranchId = model.BranchId;
                modelPdf.CompId = model.CompId;
                modelPdf.InvId = Convert.ToInt32(model.InvId);
                modelPdf.GpId = 0;
                modelPdf.Type = model.Type;
                modelPdf.BoxNo = model.BoxNo;
                modelPdf.Action = BusinessCont.ADDRecord; // ADD
                modelPdf.Flag = BusinessCont.PendingPrinterMsg; // After PDF Saved -> Flag to set default - Pending
                modelPdf.AddedBy = model.AddedBy;
                msgPDF = _unitOfWork.OrderDispatchRepository.PrinterPDFData(modelPdf);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GeneratePDF", "Generate PDF and QR Coode", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return msgPDF;
        }
        #endregion

        #region Get PickList Generate New No
        [HttpPost]
        [Route("OrderDispatch/GetPickListGenerateNewNo")]
        public string GetPickListGenerateNewNo(PickListModel model)
        {
            string pickListNo = string.Empty;

            try
            {
                pickListNo = _unitOfWork.OrderDispatchRepository.GetPickListGenerateNewNo(model.BranchId, model.CompId, model.PicklistDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListGenerateNewNo", "Get PickList Generate New No " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return pickListNo;
        }
        #endregion

        #region Get Invoice Details For Sticker - Web
        [HttpGet]
        [Route("OrderDispatch/GetInvoiceDetailsForSticker/{BranchId}/{CompId}/{InvId}")]
        public List<GetInvoiceDetailsForStickerModel> GetInvoiceDetailsForSticker(int BranchId, int CompId, int InvId)
        {
            List<GetInvoiceDetailsForStickerModel> modelList = new List<GetInvoiceDetailsForStickerModel>();
            try
            {
                modelList = _unitOfWork.OrderDispatchRepository.GetInvoiceDetailsForSticker(BranchId, CompId, InvId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceDetailsForSticker", "Get Invoice Details For Sticker - Web" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Invoice Details For Sticker - Print
        [HttpGet]
        [Route("OrderDispatch/GetInvoiceDetailsForPrintSticker/{BranchId}/{CompId}/{InvId}")]
        [AllowAnonymous]
        public GetInvoiceDetailsForStickerModel GetInvoiceDetailsForPrintSticker(int BranchId, int CompId, int InvId)
        {
            GetInvoiceDetailsForStickerModel model = new GetInvoiceDetailsForStickerModel();
            try
            {
                model = _unitOfWork.OrderDispatchRepository.GetInvoiceDetailsForPrintSticker(BranchId, CompId, InvId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceDetailsForPrintSticker", "Get Invoice Details For Sticker - Print" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get LR Details List
        [HttpPost]
        [Route("OrderDispatch/GetLRDataList")]
        public List<ImportLrDataModel> GetLRDataList(ImportLrDataModel model)
        {
            List<ImportLrDataModel> LRLst = new List<ImportLrDataModel>();
            try
            {
                LRLst = _unitOfWork.OrderDispatchRepository.GetLRDataLst(model.BranchId, model.CompId, model.LRDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRDataList", "Get LR Data List " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId + "LRDate:  " + model.LRDate, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return LRLst;
        }
        #endregion

        #region Get PickList For Re-Allotment
        [HttpPost]
        [Route("OrderDispatch/GetPickForReAllotment")]
        public List<PickListModel> GetPickListForReAllotment(PickListModel model)
        {
            List<PickListModel> pickListList = new List<PickListModel>();

            try
            {
                pickListList = _unitOfWork.OrderDispatchRepository.GetPickListForReAllotment(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListGenerateNewNo", "Get PickList Generate New No " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return pickListList;
        }
        #endregion

        #region Get Invoice Summary Counts
        [HttpPost]
        [Route("OrderDispatch/GetInvoiceSummaryCounts")]
        public InvCntModel InvoiceSummaryCounts(InvCntModel model)
        {
            InvCntModel InvCnts = new InvCntModel();

            try
            {
                InvCnts = _unitOfWork.OrderDispatchRepository.InvoiceSummaryCounts(model.BranchId, model.CompId, model.InvDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceSummaryCounts", "Get Invoice Summary Counts" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvCnts;
        }
        #endregion

        #region PickList Header Delete
        [HttpPost]
        [Route("OrderDispatch/PickListHeaderDelete")]
        public string PickListHeaderDelete([FromBody] PickListModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.PickListHeaderDelete(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PickListHeaderDelete", "PickLis tHeader Delete", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Invoice Status List
        [HttpGet]
        [Route("OrderDispatch/InvoiceStatusList")]
        public List<InvSts> InvoiceStatusListForMob()
        {
            List<InvSts> Result = new List<InvSts>();
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.InvoiceStatusForMob();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "InvoiceStatusListForMob", "Invoice Status List For Mob", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Printer Details
        [HttpGet]
        [Route("OrderDispatch/GetPrinterDetails/{BranchId}/{CompId}")]
        [AllowAnonymous]
        public List<PrinterDtls> GetPrinterDetails(int BranchId, int CompId)
        {
            List<PrinterDtls> Result = new List<PrinterDtls>();
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.GetPrinterDetails(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrinterDetails", "Get Printer Details - BranchId: " + BranchId + "  CompanyId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Printer Log Add/Edit
        [HttpPost]
        [Route("OrderDispatch/PrinterLogAddEdit")]
        [AllowAnonymous]
        public string PrinterLogAddEdit([FromBody] PrinterLogAddEditModel model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.OrderDispatchRepository.PrinterLogAddEdit(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PrinterLogAddEdit", "Printer Log Add/Edit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Printer PDF Data
        [HttpPost]
        [Route("OrderDispatch/PrinterPDFData")]
        [AllowAnonymous]
        public string PrinterPDFData([FromBody] PrintPDFDataModel model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.OrderDispatchRepository.PrinterPDFData(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PrinterPDFData", "Printer PDF Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get Print PDF Data
        [HttpGet]
        [Route("OrderDispatch/GetPrintPDFData/{BranchId}/{CompId}/{PrinterType}")]
        [AllowAnonymous]
        public List<PrintPDFDataModel> GetPrintPDFData(int BranchId, int CompId, string PrinterType)
        {
            List<PrintPDFDataModel> Result = new List<PrintPDFDataModel>();
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.GetPrintPDFData(BranchId, CompId, PrinterType);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrintPDFData", "Get Print PDF Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Generate Gatepass Add Edit 
        [HttpPost]
        [Route("OrderDispatch/GatePassAddEdit")]
        public Responce GatePassAddEdit([FromBody] GatePassModel model)
        {
            string result = string.Empty, emailDtls = string.Empty;
            Responce res = new Responce();
            List<InvDtlsForEmail> invaGatepassDtls = new List<InvDtlsForEmail>();
            try
            {
                res = _unitOfWork.OrderDispatchRepository.GenerateGatepasAddEdit(model);
                if (result != "")
                {
                    invaGatepassDtls = _unitOfWork.OrderDispatchRepository.GetInvDtlsForEmail(model.BranchId, model.CompId, Convert.ToInt32(result));
                    if (invaGatepassDtls.Count > 0)
                    {
                        foreach (var i in invaGatepassDtls)
                        {
                            BusinessCont.SaveLog(0, 0, 0, "sendEmailForDispatchDone", " ", "START", "");
                            emailDtls = _unitOfWork.OrderDispatchRepository.sendEmailForDispatchDone(i.Emailid, i.StockistName, i.TransporterName, i.CompanyName, model.BranchId, model.CompId);
                            BusinessCont.SaveLog(0, 0, 0, "sendEmailForDispatchDone", " ", "END", "");
                        }
                    }
                    else
                    {
                        BusinessCont.SaveLog(0, 0, 0, "invaGatepassDtls", "No Records Found", "", "");
                    }
                }
                else
                {
                    BusinessCont.SaveLog(0, 0, 0, "invaGatepassDtls", "result;  " + result, "", "");
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GatePassAddEdit", "Generate Gatepass Add Edit ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return res;
        }
        #endregion

        #region Gatepass List Generate New No
        [HttpPost]
        [Route("OrderDispatch/GatepassListGenerateNewNo")]
        public string GatepassListGenerateNewNo(GatePassModel model)
        {
            string GatepassNo = string.Empty;

            try
            {
                GatepassNo = _unitOfWork.OrderDispatchRepository.GatepassListGenerateNewNo(model.BranchId, model.CompId, model.GatepassDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GatepassListGenerateNewNo", "Gatepass Generate New No", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GatepassNo;
        }
        #endregion

        #region Get Gatepass Dtls For PDF
        [HttpGet]
        [Route("OrderDispatch/GetGatepassDtlsForPDF/{BranchId}/{CompId}/{GPid}")]
        [AllowAnonymous]
        public List<InvGatpassDtls> GetGatepassDtlsForPDF(int BranchId, int CompId, int GPid)
        {
            List<InvGatpassDtls> modelList = new List<InvGatpassDtls>();
            try
            {
                modelList = _unitOfWork.OrderDispatchRepository.GetGatepassDtlsForPDF(BranchId, CompId, GPid);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGatepassDtlsForPDF", "Get Gatepass Dtls For PDF", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Generate Gatepass PDF
        /// <summary>
        /// Generate Gatepass PDF
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OrderDispatch/GenerateGatepassPDF")]
        public string GenerateGatepassPDF(GenerateGatepassPDFModel model)
        {
            string msgPDF = string.Empty;
            PrintPDFDataModel modelPdf = new PrintPDFDataModel();
            try
            {
                BusinessCont.SaveLog(0, 0, 0, "GenerateGatepassPDF", "Generate Gatepass PDF", "START", "");
                modelPdf.pkId = 0;
                modelPdf.BranchId = model.BranchId;
                modelPdf.CompId = model.CompId;
                modelPdf.InvId = 0;
                modelPdf.GpId = model.GPid;
                modelPdf.Type = model.Type;
                modelPdf.Action = BusinessCont.ADDRecord; // ADD
                modelPdf.Flag = BusinessCont.PendingPrinterMsg; // After PDF Saved -> Flag to set default - Pending
                modelPdf.AddedBy = model.AddedBy;
                msgPDF = _unitOfWork.OrderDispatchRepository.PrinterPDFData(modelPdf);
                BusinessCont.SaveLog(0, 0, 0, "GenerateGatepassPDF", "Generate Gatepass PDF", "END", "");
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GenerateGatepassPDF", "Generate Gatepass PDF", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return msgPDF;
        }
        #endregion

        #region Get Gatepass Dtls For Mobile
        [HttpGet]
        [Route("OrderDispatch/GetGatepassDtlsForMob/{BranchId}/{CompId}")]
        public List<GatepassDtls> GetGatepassDtlsForMobile(int BranchId, int CompId)
        {
            List<GatepassDtls> modelList = new List<GatepassDtls>();
            try
            {
                modelList = _unitOfWork.OrderDispatchRepository.GetGatepassDtlsForMobile(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGatepassDtlsForMobile", "Get Gatepass Dtls For Mobile", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Invoice Dtls For Mobile
        [HttpGet]
        [Route("OrderDispatch/GetInvoiceDtlsForMobile/{BranchId}/{CompId}/{InvStatus}")]
        public List<InvDtlsForMob> GetInvoiceDtlsForMobile(int BranchId, int CompId, int InvStatus)
        {
            List<InvDtlsForMob> modelList = new List<InvDtlsForMob>();
            try
            {
                modelList = _unitOfWork.OrderDispatchRepository.GetInvoiceDtlsForMobile(BranchId, CompId, InvStatus);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceDtlsForMobile", "Get Invoice Dtls For Mobile", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Gatepass Dtls For DeleteBy Id
        [HttpGet]
        [Route("OrderDispatch/GatepassDtlsForDeleteById/{GatepassId}")]
        public string GatepassDtlsForDeleteById(int GatepassId)
        {
            string msg = string.Empty;
            try
            {
                msg = _unitOfWork.OrderDispatchRepository.GatepassDtlsForDeleteById(GatepassId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GatepassDtlsForDeleteById", "Gatepass Dtls For DeleteBy Id", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return msg;
        }
        #endregion

        #region Print Details Add
        [HttpPost]
        [Route("OrderDispatch/PrintDetailsAdd")]
        [AllowAnonymous]
        public string PrintDetailsAdd(PrinterDtls model)
        {
            string msg = string.Empty;
            try
            {
                msg = _unitOfWork.OrderDispatchRepository.PrintDetailsAdd(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PrintDetailsAdd", "Print Details Add:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return msg;
        }
        #endregion

        #region Get Picklist By Picker Status
        [HttpPost]
        [Route("OrderDispatch/GetPicklistByPickerStatus")]
        public List<PickListModel> GetPicklistByPickerStatus(PickListModel model)
        {
            List<PickListModel> pickList = new List<PickListModel>();
            try
            {
                pickList = _unitOfWork.OrderDispatchRepository.GetPicklistByPickerStatus(model.BranchId, model.CompId, model.PicklistDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPicklistByPickerStatus", "Get Picklist By Picker Status", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return pickList;
        }
        #endregion

        #region Priority Invoice Flag Update
        [HttpPost]
        [Route("OrderDispatch/PriorityInvoiceFlagUpdate")]
        public string PriorityInvoiceFlagUpdate(PriorityFlagUpdtModel model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.OrderDispatchRepository.PriorityInvoiceFlagUpdate(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PriorityInvoiceFlagUpdate", "Priority Invoice Flag Update: " + model.InvId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return result;
        }
        #endregion

        #region Picklist Resolve Concern Add
        [HttpPost]
        [Route("OrderDispatch/ResolveConcernAdd")]
        public string ResolveConcernAdd([FromBody] PickListModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.ResolveConcernAdd(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveConcernAdd", "Resolve Concern Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Resolve Concern List 
        [HttpPost]
        [Route("OrderDispatch/ResolveConcernList")]
        public List<PickListModel> ResolveConcernList(PickListModel model)
        {
            List<PickListModel> pickList = new List<PickListModel>();
            try
            {
                pickList = _unitOfWork.OrderDispatchRepository.ResolveConcernLst(model.BranchId, model.CompId, model.PicklistDate);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickList", "Get Pick List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return pickList;
        }
        #endregion

        #region Get Invoice Header List for Resolve Convern
        [HttpPost]
        [Route("OrderDispatch/GetInvoiceHeaderLstResolveCnrn")]
        public List<InvoiceLstModel> GetInvoiceHeaderListResolveCnrn(InvoiceLstModel model)
        {
            List<InvoiceLstModel> InvoiceLst = new List<InvoiceLstModel>();
            try
            {
                InvoiceLst = _unitOfWork.OrderDispatchRepository.GetInvoiceHeaderLstResolveCnrn(model.BranchId, model.CompId, model.BillDrawerId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceHeaderListResolveCnrn", "Get Invoice Header List for Resolve Convern " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Invoice Resolve Convern Add
        [HttpPost]
        [Route("OrderDispatch/ResolveInvConcernAdd")]
        public string ResolveInvConcernAdd([FromBody] InvoiceLstModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.ResolveInvConcernAdd(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveInvConcernAdd", "Resolve Concern Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Assigned Transporter List
        [HttpPost]
        [Route("OrderDispatch/GetAssignedTransporterList")]
        public List<AssignedTransportModel> GetAssignedTransporterList(AssignedTransportModel model)
        {
            List<AssignedTransportModel> AssignTransList = new List<AssignedTransportModel>();
            try
            {
                AssignTransList = _unitOfWork.OrderDispatchRepository.GetAssignedTransporterList(model.BranchId, model.CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetAssignedTransporterList", "Get Assigned Transporter List ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return AssignTransList;
        }
        #endregion

        #region Edit Assigned Transport Mode
        [HttpPost]
        [Route("OrderDispatch/EditAssignedTransportMode")]
        public string EditAssignedTransportMode([FromBody] AssignedTransportModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.EditAssignedTransportMode(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "EditAssignedTransportMode", "Edit Assigned Transport Mode", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Print PDF Data With Printer
        [HttpGet]
        [Route("OrderDispatch/GetPrintPDFDataWithPrinter/{BranchId}/{CompId}/{UtilityNo}")]
        [AllowAnonymous]
        public List<PrintPDFDataModel> GetPrintPDFDataWithPrinter(int BranchId, int CompId, int UtilityNo)
        {
            List<PrintPDFDataModel> Result = new List<PrintPDFDataModel>();
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.GetPrintPDFDataWithPrinter(BranchId, CompId, UtilityNo);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrintPDFDataWithPrinter", "Get Print PDF Data With Printer", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Check Invoice No Already Exist for mobile
        [HttpPost]
        [Route("OrderDispatch/CheckInvNoExistMob")]
        public string CheckInvNoExistMob([FromBody] CheckInvNoExitModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.CheckInvNoExistMob(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "CheckInvNoExistMob", "Check Inv No Exist for Mob" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Save Assign Transport And Print Data
        [HttpPost]
        [Route("OrderDispatch/SaveAndPrint")]
        public string SaveAndPrint([FromBody] SaveAndPrintModel model)
        {
            string Result = string.Empty;
            try
            {
                Result = _unitOfWork.OrderDispatchRepository.SaveAndPrint(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveAndPrint", "Save And Print", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Invoice Counts
        [HttpPost]
        [Route("OrderDispatch/AllInvoiceCounts")]
        public InvoiceCountsModel AllInvoiceCounts(InvoiceCountsModel model)
        {
            InvoiceCountsModel allinvcnt = new InvoiceCountsModel();
            try
            {
                allinvcnt = _unitOfWork.OrderDispatchRepository.AllInvoiceCounts(model.BranchId, model.CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AllInvoiceCounts", "Get Invoice All Summary Counts" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return allinvcnt;
        }
        #endregion

        #region Save Scanned Inv Data
        [HttpPost]
        [Route("OrderDispatch/SaveScannedInvData")]
        public int SaveScannedInvData()
        {
            var httpRequest = HttpContext.Current.Request;
            SaveScannedInvData model = new SaveScannedInvData();
            int Result = 0;
            try
            {
                    model.BranchId = Convert.ToInt32(httpRequest.Params.Get(0));
                    model.CompId = Convert.ToInt32(httpRequest.Params.Get(1));
                    model.AddedBy = Convert.ToInt32(httpRequest.Params.Get(2));
                    model.ScannedData = httpRequest.Params.Get(3);
                    Result = _unitOfWork.OrderDispatchRepository.SaveScannedInvData(model);
            }           
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveScannedInvData", "Save Scanned Inv Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Dashboard Count List
        [HttpPost]
        [Route("OrderDispatch/AllOrderDispatchCountNew")]
        public OrderDispatchCountFordashModel OrderDispatchCounts(DashBoardCommonModel model)
        {
            OrderDispatchCountFordashModel ordiscount = new OrderDispatchCountFordashModel();
            try
            {
                ordiscount = _unitOfWork.OrderDispatchRepository.OrderDispatchCounts(model.BranchId, model.CompanyId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "OrderDispatchCounts", "Get Dashbord Count of Order Dispatch For All Login" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompanyId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ordiscount;
        }

        #endregion

        #region Get Order Dispatch Filter List New
        [HttpGet]
        [Route("OrderDispatch/DashboardFilterListforOrderdispatch/{BranchId}/{CompId}")]
        public List<DashOrderDispatchList> GetOrderDispatchFilterListNew(int BranchId, int CompId)
        {
            List<DashOrderDispatchList> DasInvoiceLst = new List<DashOrderDispatchList>();
            try
            {
                DasInvoiceLst = _unitOfWork.OrderDispatchRepository.GetOrderDispatchFilterListNew(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispatchFilterList", "GetOrderDispatchFilterList:  " + BranchId + " CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return DasInvoiceLst;
        }
        #endregion

        #region Get Order Disp LR Filter List New
        [HttpGet]
        [Route("OrderDispatch/GetOrderDispPLFilterListNew/{BranchId}/{CompId}")]
        public List<OrderDispPLModelList> GetOrderDispLRFilterListNew(int BranchId, int CompId)
        {
            List<OrderDispPLModelList> OrderDispLRLst = new List<OrderDispPLModelList>();
            try
            {
                OrderDispLRLst = _unitOfWork.OrderDispatchRepository.GetOrderDispLRFilterListNew(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispLRFilterListNew", "Get Order Disp LR Filter List New", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OrderDispLRLst;
        }
        #endregion

        #region Get Order Dispatch Cumm Inv Filter List New
        [HttpGet]
        [Route("OrderDispatch/GetOrderDispatchCummInvList/{BranchId}/{CompId}")]
        public List<DashOrderDispatchList> GetOrderDispatchCummInvList(int BranchId, int CompId)
        {
            List<DashOrderDispatchList> OrderDispCummInv = new List<DashOrderDispatchList>();
            try
            {
                OrderDispCummInv = _unitOfWork.OrderDispatchRepository.GetOrderDispatchCummInvList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispPLFilterListNew", "Get Order Disp PL Filter List New" , BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OrderDispCummInv;
        }
        #endregion

        #region Get Order Disp Cumm PL List New
        [HttpGet]
        [Route("OrderDispatch/GetOrderDispCummPLListNew/{BranchId}/{CompId}")]
        public List<OrderDispPLModelList> GetOrderDispCummPLListNew(int BranchId, int CompId)
        {
            List<OrderDispPLModelList> OrderDispPLLst = new List<OrderDispPLModelList>();
            try
            {
                OrderDispPLLst = _unitOfWork.OrderDispatchRepository.GetOrderDispCummPLListNew(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispCummPLListNew", "Get Order Disp Cumm PL List New", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OrderDispPLLst;
        }
        #endregion

        #region Get Order Dispatch Summary Count  List
        [HttpGet]
        [Route("OrderDispatch/GetOrderDispatchSummaryCount/{BranchId}/{CompId}")]
        public List<OrderDispatchSmmryCnt> GetOrderDispatchSummaryCount(int BranchId, int CompId)
        {
            List<OrderDispatchSmmryCnt> OrderDispLst = new List<OrderDispatchSmmryCnt>();
            try
            {
                OrderDispLst = _unitOfWork.OrderDispatchRepository.GetOrderDispatchSummaryCount(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispatchSummaryCount", "Get Order Dispatch Summary Count", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OrderDispLst;
        }
        #endregion

        #region Get UtilityNo New
        [HttpGet]
        [Route("OrderDispatch/GetUtilityNoNew")]
        [AllowAnonymous]
        public List<GetUtilityNoNewModel> GetUtilityNoNew()
        {
            List<GetUtilityNoNewModel> modelList = new List<GetUtilityNoNewModel>();
            try
            {
                modelList = _unitOfWork.OrderDispatchRepository.GetUtilityNoNew();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetUtilityNoNew", "Get UtilityNo New", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Owner Order Disp Dash Inv Smmry List
        [HttpGet]
        [Route("OrderDispatch/GetOwnerOrderDispDashInvSmmryList")]
        public List<OwnOrdrDispInvSmmryList> GetOwnerOrderDispDashInvSmmryList()
        {
            List<OwnOrdrDispInvSmmryList> modelList = new List<OwnOrdrDispInvSmmryList>();
            try
            {
                modelList = _unitOfWork.OrderDispatchRepository.GetOwnerOrderDispDashInvSmmryList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerOrderDispDashInvSmmryList", "Get Owner Order Disp Dash Inv Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get Owner Order Disp Dash Boxes Smmry List
        [HttpGet]
        [Route("OrderDispatch/GetOwnerOrderDispDashBoxesSmmryList")]
        public List<OwnOrdrDispBoxesSmmryList> GetOwnerOrderDispDashBoxesSmmryList()
        {
            List<OwnOrdrDispBoxesSmmryList> modelList = new List<OwnOrdrDispBoxesSmmryList>();
            try
            {
                modelList = _unitOfWork.OrderDispatchRepository.GetOwnerOrderDispDashBoxesSmmryList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerOrderDispDashBoxesSmmryList", "Get Owner Order Disp Dash Boxes Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return modelList;
        }
        #endregion

        #region Get PickList Summary Data For Stock Transfere
        [HttpPost]
        [Route("OrderDispatch/GetPickListSummaryCountsForStockTrans")]
        public PickLstSummaryData GetPickListSummaryCountsForStockTrans(PickLstSummaryData model)
        {
            PickLstSummaryData PicklistData = new PickLstSummaryData();
            try
            {
                PicklistData = _unitOfWork.OrderDispatchRepository.GetPickListSummaryCountsForStockTrans(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPickListSummaryCountsForStockTrans", "Get PickList Summary Counts For Stock Trans  ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return PicklistData;
        }
        #endregion

        #region Get Invoice Counts for Stock Transfer
        [HttpPost]
        [Route("OrderDispatch/AllInvoiceCountsStkCount")]
        public InvoiceCountsModel AllInvoiceCountsStkCount(InvoiceCountsModel model)
        {
            InvoiceCountsModel allinvcnt = new InvoiceCountsModel();
            try
            {
                allinvcnt = _unitOfWork.OrderDispatchRepository.AllInvoiceCountsStkCount(model.BranchId, model.CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AllInvoiceCounts", "Get Invoice All Summary Counts" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return allinvcnt;
        }
        #endregion

        #region Get LR Details List For Dash New
        [HttpGet]
        [Route("OrderDispatch/GetLRDetailsListForDashNew/{BranchId}/{CompId}")]
        public List<LRDetailsListForDash> GetLRDataList(int BranchId, int CompId)
        {
            List<LRDetailsListForDash> LRLst = new List<LRDetailsListForDash>();
            try
            {
                LRLst = _unitOfWork.OrderDispatchRepository.GetLRDetailsListForDashNew(BranchId,CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLRDetailsListForDashNew", "Get LR Details List For Dash New", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return LRLst;
        }
        #endregion

        #region Get Prio Pending Inv For Dash New
        [HttpGet]
        [Route("OrderDispatch/GetPrioPendingInvForDashNew/{BranchId}/{CompId}")]
        public List<DashOrderDispatchList> GetPrioPendingInvForDashNew(int BranchId, int CompId)
        {
            List<DashOrderDispatchList> prioInvList = new List<DashOrderDispatchList>();
            try
            {
                prioInvList = _unitOfWork.OrderDispatchRepository.GetPrioPendingInvForDashNew(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetPrioPendingInvForDashNew", "Get Prio Pending Inv For Dash New", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return prioInvList;
        }
        #endregion

        #region transporterName list Details
        [HttpPost]
        [Route("OrderDispatch/GettransporterForMonthlyModelList")]
        [AllowAnonymous]
        public List<transporterForMonthlyModel> GettransporterForMonthlyModelList([FromBody] transporterForMonthlyModel model)
        {
            List<transporterForMonthlyModel> TransMonthlyList = new List<transporterForMonthlyModel>();
            try
            {
                TransMonthlyList = _unitOfWork.OrderDispatchRepository.GettransporterForMonthlyModelList(model.BranchId, model.CompId, model.FromDate, model.ToDate, model.TransporterId, model.CourierId,model.TransportModeId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GettransporterForMonthlyModelList", "Get Cheque Summary of previous month/Week List " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransMonthlyList;
        }
        #endregion

        #region transporterName list Summary
        [HttpPost]
        [Route("OrderDispatch/GetTransporterForMonthlyModelSummary")]
        public List<TransporterForMonthlyModels> GetTransporterForMonthlyModelSummary([FromBody] TransporterForMonthlyModels model)
        {
            List<TransporterForMonthlyModels> TransMonthlyList = new List<TransporterForMonthlyModels>();
            try
            {
                TransMonthlyList = _unitOfWork.OrderDispatchRepository.GetTransporterForMonthlyModelSummary(model.BranchId, model.CompId, model.FromDate1, model.ToDate1);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransporterForMonthlyModelSummary", "Get Cheque Summary of previous month/Week List " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransMonthlyList;
        }
        #endregion

        #region Get Order Dispatch Prev Month Data
        [HttpGet]
        [Route("OrderDispatch/GetOrderDispatchPrevMonthData/{BranchId}/{CompId}")]
        public List<DashOrderDispatchListPrevMonth> GetOrderDispatchPrevMonthData(int BranchId, int CompId)
        {
            List<DashOrderDispatchListPrevMonth> OrderDispPrevInv = new List<DashOrderDispatchListPrevMonth>();
            try
            {
                OrderDispPrevInv = _unitOfWork.OrderDispatchRepository.GetOrderDispatchPrevMonthData(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispatchPreMonthData", "Get Order Dispatch Prev Month Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return OrderDispPrevInv;
        }
        #endregion

        #region Get Order Dispatch Filter List For Sticker
        [HttpGet]
        [Route("OrderDispatch/GetOrderDispatchFilterListForSticker/{BranchId}/{CompId}")]
        public List<DashOrderDispatchList> GetOrderDispatchFilterListForSticker(int BranchId, int CompId)
        {
            List<DashOrderDispatchList> DasInvoiceLst = new List<DashOrderDispatchList>();
            try
            {
                DasInvoiceLst = _unitOfWork.OrderDispatchRepository.GetOrderDispatchFilterListForSticker(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOrderDispatchFilterListForSticker", "GetOrderDispatchFilterListForSticker:  " + BranchId + " CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return DasInvoiceLst;
        }
        #endregion

        #region Get Gatepass List for Print
        [HttpPost]
        [Route("OrderDispatch/GetGatepassPrint")]
        public List<GatepassListModal> GetGatepassPrint([FromBody] GatepassDetailsModal modal)
        {
            List<GatepassListModal> list = new List<GatepassListModal>();
            try
            {
                list = _unitOfWork.OrderDispatchRepository.GetGatepassList(modal.BranchId, modal.CompanyId, modal.FromDate, modal.ToDate);
            }
            catch(Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetGatepassPrint", "Get Gatepass Print" + "BranchId:  " + modal.BranchId + "CompanyId:  " + modal.CompanyId,
                 BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return list;
        }
        #endregion

        #region Print Gatepass
        [HttpPost]
        [Route("OrderDispatch/PrintGatepass")]
        public int PrintGatepass(GatepassPrintModal model)
        {
            int RetVal = 0;
            try
            {
                RetVal = _unitOfWork.OrderDispatchRepository.PrinterGatepassData(model);
            }
            catch(Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "PrintGatepass", "Print Gatepass" + "BranchId:  " + model.BranchId + "CompanyId:  " + model.CompanyId,
                 BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetVal;

        }
        #endregion

        #region Get Invoice List For Delete
        [HttpPost]
        [Route("OrderDispatch/GetInvoiceHeaderListForDelete")]
        public List<InvoiceLstDltModel> GetInvoiceHeaderListForDelete(InvoiceLstDltModel model)
        {
            List<InvoiceLstDltModel> InvoiceLst = new List<InvoiceLstDltModel>();
            try
            {
                InvoiceLst = _unitOfWork.OrderDispatchRepository.GetInvoiceLstForDlt(model.BranchId, model.CompId, model.InvNo);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceHeaderListForDelete", "Get Invoice List For Delete " + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvoiceLst;
        }
        #endregion

        #region Delete Invoice Details 
        [HttpGet]
        [Route("OrderDispatch/DeleteInvoiceDetails/{InvId}/{BranchId}")]
        public int DeleteInvoiceDetails(int InvId, int BranchId)
        {
            int lst = 0;
            try
            {
                lst = _unitOfWork.OrderDispatchRepository.DeleteInvoiceDetails(InvId, BranchId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "DeleteVersionDetails", "Delete Version Details (Mobile & Web Application) - BranchId:  ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return lst;
        }

        #endregion

    }
}
