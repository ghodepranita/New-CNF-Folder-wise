using CNF.Business.BusinessConstant;
using CNF.Business.Model.Context;
using CNF.Business.Model.InventoryInward;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ExcelDataReader;
using CNF.Business.Model.ChequeAccounting;

namespace CNF.API.Controllers
{
    public class InventoryInwardController : BaseApiController
    {
        // Web API START
        #region Import Transit Data - BranchId, CompanyId and Addedby
        [HttpPost]
        [Route("InventoryInward/ImportTransitData/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportTransitData(int BranchId, int CompanyId, string Addedby)
        {
            ImportTransitDataModel message = new ImportTransitDataModel();

            bool IsColumnFlag = false;
            bool InsertFlag = false;
            bool NextDateInvalid = false;

            string columnErrorMsg_DelNo = string.Empty, columnErrorMsg_ActGIDate = string.Empty,
                   columnErrorMsg_RecP = string.Empty, columnErrorMsg_RecPDesc = string.Empty,
                   columnErrorMsg_DispP = string.Empty, columnErrorMsg_DispPDesc = string.Empty,
                   columnErrorMsg_InvNo = string.Empty, columnErrorMsg_InvDate = string.Empty,
                   columnErrorMsg_MatNo = string.Empty, columnErrorMsg_MatDesc = string.Empty,
                   columnErrorMsg_UoM = string.Empty, columnErrorMsg_BNo = string.Empty,
                   columnErrorMsg_Qty = string.Empty, columnErrorMsg_TCode = string.Empty, columnErrorMsg_TName = string.Empty,
                   columnErrorMsg_LrNo = string.Empty, columnErrorMsg_LrDate = string.Empty,
                   columnErrorMsg_TtlCQty = string.Empty, columnErrorMsg_VehNo = string.Empty;

            bool columnErrorFlag_DelNo = false, columnErrorFlag_ActGIDate = false, columnErrorFlag_RecP = false,
                 columnErrorFlag_RecPDesc = false, columnErrorFlag_DispP = false, columnErrorFlag_DispPDesc = false,
                 columnErrorFlag_InvNo = false, columnErrorFlag_InvDate = false, columnErrorFlag_MatNo = false,
                 columnErrorFlag_MatDesc = false, columnErrorFlag_UoM = false, columnErrorFlag_BNo = false,
                 columnErrorFlag_Qty = false, columnErrorFlag_TCode = false, columnErrorFlag_TName = false,
                 columnErrorFlag_LrNo = false, columnErrorFlag_LrDate = false, columnErrorFlag_TtlCQty = false,
                 columnErrorFlag_VehNo = false;

            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;

                    if (file.FileName == "ImportTransitReportTemplate.xls" || file.FileName == "ImportTransitReportTemplate.XLS" || file.FileName == "ImportTransitReportTemplate.xlsx" || file.FileName == "ImportTransitReportTemplate.XLSX")
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
                            List<ImportTransitDataModel> modelList = new List<ImportTransitDataModel>(); // modelList

                            ImportTransitDataModel model; // model

                            IsColumnFlag = IsColumnValidationForTransitData(finalRecords); // To Check Column Name
                            if (IsColumnFlag) // Valid Column - true
                            {
                                for (int j = 1; j < finalRecords.Rows.Count; j++) // To Add rows values
                                {
                                    model = new ImportTransitDataModel(); // new instance model
                                    model.pkId = j;

                                    InsertFlag = true;

                                    // Delivery #
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][0].ToString(), j, ref InsertFlag, ref columnErrorFlag_DelNo, ref columnErrorMsg_DelNo);

                                    // Actual GI Date
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][1].ToString(), j, ref InsertFlag, ref columnErrorFlag_ActGIDate, ref columnErrorMsg_ActGIDate);

                                    // Rec. Plant
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][2].ToString(), j, ref InsertFlag, ref columnErrorFlag_RecP, ref columnErrorMsg_RecP);

                                    // Rec. Plant Description
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][3].ToString(), j, ref InsertFlag, ref columnErrorFlag_RecPDesc, ref columnErrorMsg_RecPDesc);

                                    // Disp. Plant
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][4].ToString(), j, ref InsertFlag, ref columnErrorFlag_DispP, ref columnErrorMsg_DispP);

                                    // Disp. Plant Description
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][5].ToString(), j, ref InsertFlag, ref columnErrorFlag_DispPDesc, ref columnErrorMsg_DispPDesc);

                                    // Invoice #
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][6].ToString(), j, ref InsertFlag, ref columnErrorFlag_InvNo, ref columnErrorMsg_InvNo);

                                    // Invoice Date
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][7].ToString(), j, ref InsertFlag, ref columnErrorFlag_InvDate, ref columnErrorMsg_InvDate);

                                    // Material #
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][8].ToString(), j, ref InsertFlag, ref columnErrorFlag_MatNo, ref columnErrorMsg_MatNo);

                                    // Material Description
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][9].ToString(), j, ref InsertFlag, ref columnErrorFlag_MatDesc, ref columnErrorMsg_MatDesc);

                                    // UoM
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][10].ToString(), j, ref InsertFlag, ref columnErrorFlag_UoM, ref columnErrorMsg_UoM);

                                    // Batch
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][11].ToString(), j, ref InsertFlag, ref columnErrorFlag_BNo, ref columnErrorMsg_BNo);

                                    // Quantity
                                    BusinessCont.TypeCheckWithData("Decimal", finalRecords.Rows[j][13].ToString(), j, ref InsertFlag, ref columnErrorFlag_Qty, ref columnErrorMsg_Qty);

                                    // Transporter Code
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][15].ToString(), j, ref InsertFlag, ref columnErrorFlag_TCode, ref columnErrorMsg_TCode);

                                    // Transporter Name
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][16].ToString(), j, ref InsertFlag, ref columnErrorFlag_TName, ref columnErrorMsg_TName);

                                    // LR NUMBER
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][17].ToString(), j, ref InsertFlag, ref columnErrorFlag_LrNo, ref columnErrorMsg_LrNo);

                                    // LR DATE
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][18].ToString(), j, ref InsertFlag, ref columnErrorFlag_LrDate, ref columnErrorMsg_LrDate);

                                    // Total Case Qty.
                                    BusinessCont.TypeCheckWithData("Decimal", finalRecords.Rows[j][20].ToString(), j, ref InsertFlag, ref columnErrorFlag_TtlCQty, ref columnErrorMsg_TtlCQty);

                                    // Vehicle Number
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][25].ToString(), j, ref InsertFlag, ref columnErrorFlag_VehNo, ref columnErrorMsg_VehNo);

                                    // Valid Data - Insert Flag = true
                                    if (InsertFlag)
                                    {
                                        model.DeliveryNo = Convert.ToString(finalRecords.Rows[j][0]);
                                        model.ActualGIDate = Convert.ToDateTime(finalRecords.Rows[j][1]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.RecPlant = Convert.ToString(finalRecords.Rows[j][2]);
                                        model.RecPlantDesc = Convert.ToString(finalRecords.Rows[j][3]);
                                        model.DispPlant = Convert.ToString(finalRecords.Rows[j][4]);
                                        model.DispPlantDesc = Convert.ToString(finalRecords.Rows[j][5]);
                                        model.InvoiceNo = Convert.ToString(finalRecords.Rows[j][6]);
                                        model.InvoiceDate = Convert.ToDateTime(finalRecords.Rows[j][7]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.MaterialNo = Convert.ToString(finalRecords.Rows[j][8]);
                                        model.MatDesc = Convert.ToString(finalRecords.Rows[j][9]);
                                        model.UoM = Convert.ToString(finalRecords.Rows[j][10]);
                                        model.BatchNo = Convert.ToString(finalRecords.Rows[j][11]);
                                        model.Quantity = Convert.ToDecimal(finalRecords.Rows[j][13]);
                                        model.TransporterCode = Convert.ToString(finalRecords.Rows[j][15]);
                                        model.TransporterName = Convert.ToString(finalRecords.Rows[j][16]);
                                        model.LrNo = Convert.ToString(finalRecords.Rows[j][17]);
                                        model.LrDate = Convert.ToDateTime(finalRecords.Rows[j][18]).ToString("yyyy/MM/dd hh:mm:ss");
                                        model.TotalCaseQty = Convert.ToDecimal(finalRecords.Rows[j][20]);
                                        model.VehicleNo = Convert.ToString(finalRecords.Rows[j][25]);
                                        modelList.Add(model);
                                    }
                                }
                            }

                            // To Check Column Error Flag Maintain Column Wise
                            if (columnErrorFlag_DelNo || columnErrorFlag_ActGIDate || columnErrorFlag_RecP ||
                                columnErrorFlag_RecPDesc || columnErrorFlag_DispP || columnErrorFlag_DispPDesc ||
                                columnErrorFlag_InvNo || columnErrorFlag_InvDate || columnErrorFlag_MatNo || columnErrorFlag_MatDesc || columnErrorFlag_UoM || columnErrorFlag_BNo || columnErrorFlag_Qty || columnErrorFlag_TCode || columnErrorFlag_TName || columnErrorFlag_LrNo ||
                                columnErrorFlag_LrDate || columnErrorFlag_TtlCQty || columnErrorFlag_VehNo)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_DelNo)
                                {
                                    message.RetResult += "\n Delivery # ---- \n " + columnErrorMsg_DelNo;
                                }
                                if (columnErrorFlag_ActGIDate)
                                {
                                    message.RetResult += "\n Actual GI Date ---- \n " + columnErrorMsg_ActGIDate;
                                }
                                if (columnErrorFlag_RecP)
                                {
                                    message.RetResult += "\n Rec. Plant ---- \n " + columnErrorMsg_RecP;
                                }
                                if (columnErrorFlag_RecPDesc)
                                {
                                    message.RetResult += "\n Rec. Plant Description ---- \n " + columnErrorMsg_RecPDesc;
                                }
                                if (columnErrorFlag_DispP)
                                {
                                    message.RetResult += "\n Disp. Plant ---- \n " + columnErrorMsg_DispP;
                                }
                                if (columnErrorFlag_DispPDesc)
                                {
                                    message.RetResult += "\n Disp. Plant Description ---- \n " + columnErrorMsg_DispPDesc;
                                }
                                if (columnErrorFlag_InvNo)
                                {
                                    message.RetResult += "\n Invoice # ---- \n" + columnErrorMsg_InvNo;
                                }
                                if (columnErrorFlag_InvDate)
                                {
                                    message.RetResult += "\n Invoice Date ---- \n " + columnErrorMsg_InvDate;
                                }
                                if (columnErrorFlag_MatNo)
                                {
                                    message.RetResult += "\n Material # ---- \n " + columnErrorMsg_MatNo;
                                }
                                if (columnErrorFlag_MatDesc)
                                {
                                    message.RetResult += "\n Material Description ---- \n " + columnErrorMsg_MatDesc;
                                }
                                if (columnErrorFlag_UoM)
                                {
                                    message.RetResult += "\n UoM ---- \n " + columnErrorMsg_UoM;
                                }
                                if (columnErrorFlag_BNo)
                                {
                                    message.RetResult += "\n Batch ---- \n " + columnErrorMsg_BNo;
                                }
                                if (columnErrorFlag_Qty)
                                {
                                    message.RetResult += "\n Quantity ---- \n " + columnErrorMsg_Qty;
                                }
                                if (columnErrorFlag_TCode)
                                {
                                    message.RetResult += "\n Transporter Code ---- \n " + columnErrorMsg_TCode;
                                }
                                if (columnErrorFlag_TName)
                                {
                                    message.RetResult += "\n Transporter Name ---- \n " + columnErrorMsg_TName;
                                }
                                if (columnErrorFlag_LrNo)
                                {
                                    message.RetResult += "\n LR NUMBER ---- \n " + columnErrorMsg_LrNo;
                                }
                                if (columnErrorFlag_LrDate)
                                {
                                    message.RetResult += "\n LR DATE ---- \n " + columnErrorMsg_LrDate;
                                }
                                if (columnErrorFlag_TtlCQty)
                                {
                                    message.RetResult += "\n Total Case Qty. ---- \n " + columnErrorMsg_TtlCQty;
                                }
                                if (columnErrorFlag_VehNo)
                                {
                                    message.RetResult += "\n Vehicle Number ---- \n " + columnErrorMsg_VehNo;
                                }
                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("DeliveryNo");
                                dt.Columns.Add("ActualGIDate");
                                dt.Columns.Add("RecPlant");
                                dt.Columns.Add("RecPlantDesc");
                                dt.Columns.Add("DispPlant");
                                dt.Columns.Add("DispPlantDesc");
                                dt.Columns.Add("InvoiceNo");
                                dt.Columns.Add("InvoiceDate");
                                dt.Columns.Add("MaterialNo");
                                dt.Columns.Add("MatDesc");
                                dt.Columns.Add("UOM");
                                dt.Columns.Add("BatchNo");
                                dt.Columns.Add("Quantity");
                                dt.Columns.Add("TransporterCode");
                                dt.Columns.Add("TransporterName");
                                dt.Columns.Add("LrNo");
                                dt.Columns.Add("LrDate");
                                dt.Columns.Add("TotalCaseQty");
                                dt.Columns.Add("VehicleNo");

                                foreach (var itemList in modelList)
                                {
                                    // Previous & Current Date Validation
                                    if (Convert.ToDateTime(itemList.InvoiceDate).Date <= Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")) &&
                                         Convert.ToDateTime(itemList.LrDate).Date <= Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")))
                                    {
                                        // Add Rows DataTable
                                        dt.Rows.Add(itemList.pkId, itemList.DeliveryNo, itemList.ActualGIDate, itemList.RecPlant,
                                                itemList.RecPlantDesc, itemList.DispPlant, itemList.DispPlantDesc,
                                                itemList.InvoiceNo, itemList.InvoiceDate, itemList.MaterialNo, itemList.MatDesc, itemList.UoM, itemList.BatchNo, itemList.Quantity,
                                                itemList.TransporterCode, itemList.TransporterName, itemList.LrNo,
                                                itemList.LrDate, itemList.TotalCaseQty, itemList.VehicleNo);
                                    }                                  
                                    else
                                    {
                                        NextDateInvalid = true;
                                    }
                                }

                                // Next Date Validation
                                if (NextDateInvalid)
                                {
                                    message.RetResult = BusinessCont.msg_InvalidNextDate;
                                }
                                else
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        using (var db = new CFADBEntities())
                                        {
                                            {
                                                SqlConnection connection = (SqlConnection)db.Database.Connection;
                                                SqlCommand cmd = new SqlCommand("CFA.usp_ImportTransitData", connection);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                                BranchIdParameter.SqlDbType = SqlDbType.Int;
                                                SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                                CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                                SqlParameter ImportTransDataParameter = cmd.Parameters.AddWithValue("@ImpTrDt", dt);
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportTransitData", "Import Transit Upload Data:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        /// <summary>
        /// Is Column Validation For Transit Data
        /// </summary>
        /// <param name="finalRecords"></param>
        /// <returns></returns>
        private bool IsColumnValidationForTransitData(DataTable finalRecords)
        {
            bool IsValid = false;
            string DelNo = string.Empty, ActGIDate = string.Empty, RecP = string.Empty, RecPDesc = string.Empty,
                   DispP = string.Empty, DispPDesc = string.Empty, InvNo = string.Empty, InvDate = string.Empty,
                   MatNo = string.Empty, MatDesc = string.Empty, UoM = string.Empty, BNo = string.Empty, Qty = string.Empty,
                   TCode = string.Empty, TName = string.Empty, LrNo = string.Empty, LrDate = string.Empty,
                   TtlCQty = string.Empty, VehNo = string.Empty;
            try
            {
                if (finalRecords.Rows.Count > 0)
                {
                    DelNo = Convert.ToString(finalRecords.Rows[0][0]);
                    ActGIDate = Convert.ToString(finalRecords.Rows[0][1]);
                    RecP = Convert.ToString(finalRecords.Rows[0][2]);
                    RecPDesc = Convert.ToString(finalRecords.Rows[0][3]);
                    DispP = Convert.ToString(finalRecords.Rows[0][4]);
                    DispPDesc = Convert.ToString(finalRecords.Rows[0][5]);
                    InvNo = Convert.ToString(finalRecords.Rows[0][6]);
                    InvDate = Convert.ToString(finalRecords.Rows[0][7]);
                    MatNo = Convert.ToString(finalRecords.Rows[0][8]);
                    MatDesc = Convert.ToString(finalRecords.Rows[0][9]);
                    UoM = Convert.ToString(finalRecords.Rows[0][10]);
                    BNo = Convert.ToString(finalRecords.Rows[0][11]);
                    Qty = Convert.ToString(finalRecords.Rows[0][13]);
                    TCode = Convert.ToString(finalRecords.Rows[0][15]);
                    TName = Convert.ToString(finalRecords.Rows[0][16]);
                    LrNo = Convert.ToString(finalRecords.Rows[0][17]);
                    LrDate = Convert.ToString(finalRecords.Rows[0][18]);
                    TtlCQty = Convert.ToString(finalRecords.Rows[0][20]);
                    VehNo = Convert.ToString(finalRecords.Rows[0][25]);
                    if (DelNo == "Delivery #" && ActGIDate == "Actual GI Date" && RecP == "Rec. Plant" &&
                        RecPDesc == "Rec. Plant Description" && DispP == "Disp. Plant" &&
                        DispPDesc == "Disp. Plant Description" && InvNo == "Invoice #" && InvDate == "Invoice Date" &&
                        MatNo == "Material #" && MatDesc == "Material Description" && UoM == "UoM" && BNo == "Batch" &&
                        Qty == "Quantity" && TCode == "Transporter Code" && TName == "Transporter Name" &&
                        LrNo == "LR NUMBER" && LrDate == "LR DATE" && TtlCQty == "Total Case Qty." &&
                        VehNo == "Vehicle Number")
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
                BusinessCont.SaveLog(0, 0, 0, "IsColumnValidationForTransitData", "Is Column Validation For Transit Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return IsValid;
        }
        #endregion

        #region Import Transit Data - BranchId, CompanyId and Addedby New
        [HttpPost]
        [Route("InventoryInward/ImportTransitDataNew/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportTransitDataNew(int BranchId, int CompanyId, string Addedby)
        {
            ImportTransitDataModel message = new ImportTransitDataModel();

            bool IsColumnFlag = false;
            bool InsertFlag = false;
            bool NextDateInvalid = false;

            string columnErrorMsg_DelNo = string.Empty, columnErrorMsg_ActGIDate = string.Empty,
                   columnErrorMsg_RecP = string.Empty, columnErrorMsg_RecPDesc = string.Empty,
                   columnErrorMsg_DispP = string.Empty, columnErrorMsg_DispPDesc = string.Empty,
                   columnErrorMsg_InvNo = string.Empty, columnErrorMsg_InvDate = string.Empty,
                   columnErrorMsg_MatNo = string.Empty, columnErrorMsg_MatDesc = string.Empty,
                   columnErrorMsg_UoM = string.Empty, columnErrorMsg_BNo = string.Empty,
                   columnErrorMsg_Qty = string.Empty, columnErrorMsg_TCode = string.Empty, columnErrorMsg_TName = string.Empty,
                   columnErrorMsg_LrNo = string.Empty, columnErrorMsg_LrDate = string.Empty,
                   columnErrorMsg_TtlCQty = string.Empty, columnErrorMsg_VehNo = string.Empty;

            bool columnErrorFlag_DelNo = false, columnErrorFlag_ActGIDate = false, columnErrorFlag_RecP = false,
                 columnErrorFlag_RecPDesc = false, columnErrorFlag_DispP = false, columnErrorFlag_DispPDesc = false,
                 columnErrorFlag_InvNo = false, columnErrorFlag_InvDate = false, columnErrorFlag_MatNo = false,
                 columnErrorFlag_MatDesc = false, columnErrorFlag_UoM = false, columnErrorFlag_BNo = false,
                 columnErrorFlag_Qty = false, columnErrorFlag_TCode = false, columnErrorFlag_TName = false,
                 columnErrorFlag_LrNo = false, columnErrorFlag_LrDate = false, columnErrorFlag_TtlCQty = false,
                 columnErrorFlag_VehNo = false, columnErrorFlag_isAlphanumeric = false;
            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var ImportFor = "Import Tranist Data";
                    var columnHeaderList = _unitOfWork.OrderDispatchRepository.GetColumnHeaderList(BranchId, CompanyId, ImportFor);
                    if (columnHeaderList.Count == 0)
                    {
                        message.RetResult = "Master data is not configured, Please contact to system admin";
                        return message.RetResult;
                    }
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportTransitReportTemplate.xls" || file.FileName == "ImportTransitReportTemplate.XLS" || file.FileName == "ImportTransitReportTemplate.xlsx" || file.FileName == "ImportTransitReportTemplate.XLSX")
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
                            List<ImportTransitDataModel> modelList = new List<ImportTransitDataModel>();
                            ImportTransitDataModel model;

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
                                model = new ImportTransitDataModel();
                                model.pkId = j;
                                InsertFlag = true;
                                // Iterate through the columns dynamically using the columnIndexMapping
                                foreach (var columnIndex in columnIndexMapping.Keys)
                                {
                                    string dbColumnName = columnIndexMapping[columnIndex].Item1;
                                    string columnDataType = columnIndexMapping[columnIndex].Item2;
                                    string cellValue = finalRecords.Rows[j][columnIndex].ToString();
                                    // DeliveryNo
                                    if (dbColumnName == "DeliveryNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DelNo, ref columnErrorMsg_DelNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // ActualGIDate
                                    if (dbColumnName == "ActualGIDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_ActGIDate, ref columnErrorMsg_ActGIDate, columnErrorFlag_isAlphanumeric);
                                    }
                                    // RecPlant
                                    if (dbColumnName == "RecPlant")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_RecP, ref columnErrorMsg_RecP, columnErrorFlag_isAlphanumeric);
                                    }
                                    // RecPlantDesc
                                    if (dbColumnName == "RecPlantDesc")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_RecPDesc, ref columnErrorMsg_RecPDesc, columnErrorFlag_isAlphanumeric);
                                    }
                                    // DispPlant
                                    if (dbColumnName == "DispPlant")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DispP, ref columnErrorMsg_DispP, columnErrorFlag_isAlphanumeric);
                                    }
                                    // DispPlantDesc
                                    if (dbColumnName == "DispPlantDesc")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_DispPDesc, ref columnErrorMsg_DispPDesc, columnErrorFlag_isAlphanumeric);
                                    }
                                    // InvoiceNo
                                    if (dbColumnName == "InvoiceNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_InvNo, ref columnErrorMsg_InvNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // InvoiceDate
                                    if (dbColumnName == "InvoiceDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_InvDate, ref columnErrorMsg_InvDate, columnErrorFlag_isAlphanumeric);
                                    }

                                    // MaterialNo
                                    if (dbColumnName == "MaterialNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_MatNo, ref columnErrorMsg_MatNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // MatDesc
                                    if (dbColumnName == "MatDesc")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_MatDesc, ref columnErrorMsg_MatDesc, columnErrorFlag_isAlphanumeric);
                                    }
                                    // UoM
                                    if (dbColumnName == "UoM")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_UoM, ref columnErrorMsg_UoM, columnErrorFlag_isAlphanumeric);
                                    }

                                    // BatchNo
                                    if (dbColumnName == "BatchNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_BNo, ref columnErrorMsg_BNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Quantity
                                    if (dbColumnName == "Quantity")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_Qty, ref columnErrorMsg_Qty, columnErrorFlag_isAlphanumeric);
                                    }
                                    // TransporterCode
                                    if (dbColumnName == "TransporterCode")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_TCode, ref columnErrorMsg_TCode, columnErrorFlag_isAlphanumeric);
                                    }
                                    // TransporterName
                                    if (dbColumnName == "TransporterName")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_TName, ref columnErrorMsg_TName, columnErrorFlag_isAlphanumeric);
                                    }
                                    // LrNo
                                    if (dbColumnName == "LrNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_LrNo, ref columnErrorMsg_LrNo, columnErrorFlag_isAlphanumeric);
                                    }
                                    // LrDate
                                    if (dbColumnName == "LrDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_LrDate, ref columnErrorMsg_LrDate, columnErrorFlag_isAlphanumeric);
                                    }
                                    // TotalCaseQty
                                    if (dbColumnName == "TotalCaseQty")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_TtlCQty, ref columnErrorMsg_TtlCQty, columnErrorFlag_isAlphanumeric);
                                    }
                                    // VehicleNo
                                    if (dbColumnName == "VehicleNo")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_VehNo, ref columnErrorMsg_VehNo, columnErrorFlag_isAlphanumeric);
                                    }

                                    // Assign values to model properties dynamically based on dbColumnName
                                    switch (dbColumnName)
                                    {
                                        case "DeliveryNo":
                                            model.DeliveryNo = cellValue;
                                            break;
                                        case "ActualGIDate":
                                            model.ActualGIDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "RecPlant":
                                            model.RecPlant = cellValue;
                                            break;
                                        case "RecPlantDesc":
                                            model.RecPlantDesc = cellValue;
                                            break;
                                        case "DispPlant":
                                            model.DispPlant = cellValue;
                                            break;
                                        case "DispPlantDesc":
                                            model.DispPlantDesc = cellValue;
                                            break;
                                        case "InvoiceNo":
                                            model.InvoiceNo = cellValue;
                                            break;
                                        case "InvoiceDate":
                                            model.InvoiceNo = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "MaterialNo":
                                            model.MaterialNo = cellValue;
                                            break;
                                        case "MatDesc":
                                            model.MatDesc = cellValue;
                                            break;
                                        case "UoM":
                                            model.UoM = cellValue;
                                            break;
                                        case "BatchNo":
                                            model.BatchNo = cellValue;
                                            break;
                                        case "Quantity":
                                            model.Quantity = Convert.ToDecimal(cellValue);
                                            break;
                                        case "TransporterCode":
                                            model.TransporterCode = cellValue;
                                            break;
                                        case "TransporterName":
                                            model.TransporterName = cellValue;
                                            break;
                                        case "LrNo":
                                            model.LrNo = cellValue;
                                            break;
                                        case "LrDate":
                                            model.LrDate = Convert.ToDateTime(cellValue).ToString("yyyy/MM/dd");
                                            break;
                                        case "TotalCaseQty":
                                            model.TotalCaseQty = Convert.ToDecimal(cellValue);
                                            break;
                                        case "VehicleNo":
                                            model.VehicleNo = cellValue;
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
                            if (columnErrorFlag_DelNo || columnErrorFlag_ActGIDate || columnErrorFlag_RecP ||
                                columnErrorFlag_RecPDesc || columnErrorFlag_DispP || columnErrorFlag_DispPDesc ||
                                columnErrorFlag_InvNo || columnErrorFlag_InvDate || columnErrorFlag_MatNo || columnErrorFlag_MatDesc || columnErrorFlag_UoM || columnErrorFlag_BNo || columnErrorFlag_Qty || columnErrorFlag_TCode || columnErrorFlag_TName || columnErrorFlag_LrNo ||
                                columnErrorFlag_LrDate || columnErrorFlag_TtlCQty || columnErrorFlag_VehNo)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_DelNo)
                                {
                                    message.RetResult += "\n Delivery # ---- \n " + columnErrorMsg_DelNo;
                                }
                                if (columnErrorFlag_ActGIDate)
                                {
                                    message.RetResult += "\n Actual GI Date ---- \n " + columnErrorMsg_ActGIDate;
                                }
                                if (columnErrorFlag_RecP)
                                {
                                    message.RetResult += "\n Rec. Plant ---- \n " + columnErrorMsg_RecP;
                                }
                                if (columnErrorFlag_RecPDesc)
                                {
                                    message.RetResult += "\n Rec. Plant Description ---- \n " + columnErrorMsg_RecPDesc;
                                }
                                if (columnErrorFlag_DispP)
                                {
                                    message.RetResult += "\n Disp. Plant ---- \n " + columnErrorMsg_DispP;
                                }
                                if (columnErrorFlag_DispPDesc)
                                {
                                    message.RetResult += "\n Disp. Plant Description ---- \n " + columnErrorMsg_DispPDesc;
                                }
                                if (columnErrorFlag_InvNo)
                                {
                                    message.RetResult += "\n Invoice # ---- \n" + columnErrorMsg_InvNo;
                                }
                                if (columnErrorFlag_InvDate)
                                {
                                    message.RetResult += "\n Invoice Date ---- \n " + columnErrorMsg_InvDate;
                                }
                                if (columnErrorFlag_MatNo)
                                {
                                    message.RetResult += "\n Material # ---- \n " + columnErrorMsg_MatNo;
                                }
                                if (columnErrorFlag_MatDesc)
                                {
                                    message.RetResult += "\n Material Description ---- \n " + columnErrorMsg_MatDesc;
                                }
                                if (columnErrorFlag_UoM)
                                {
                                    message.RetResult += "\n UoM ---- \n " + columnErrorMsg_UoM;
                                }
                                if (columnErrorFlag_BNo)
                                {
                                    message.RetResult += "\n Batch ---- \n " + columnErrorMsg_BNo;
                                }
                                if (columnErrorFlag_Qty)
                                {
                                    message.RetResult += "\n Quantity ---- \n " + columnErrorMsg_Qty;
                                }
                                if (columnErrorFlag_TCode)
                                {
                                    message.RetResult += "\n Transporter Code ---- \n " + columnErrorMsg_TCode;
                                }
                                if (columnErrorFlag_TName)
                                {
                                    message.RetResult += "\n Transporter Name ---- \n " + columnErrorMsg_TName;
                                }
                                if (columnErrorFlag_LrNo)
                                {
                                    message.RetResult += "\n LR NUMBER ---- \n " + columnErrorMsg_LrNo;
                                }
                                if (columnErrorFlag_LrDate)
                                {
                                    message.RetResult += "\n LR DATE ---- \n " + columnErrorMsg_LrDate;
                                }
                                if (columnErrorFlag_TtlCQty)
                                {
                                    message.RetResult += "\n Total Case Qty. ---- \n " + columnErrorMsg_TtlCQty;
                                }
                                if (columnErrorFlag_VehNo)
                                {
                                    message.RetResult += "\n Vehicle Number ---- \n " + columnErrorMsg_VehNo;
                                }
                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("DeliveryNo");
                                dt.Columns.Add("ActualGIDate");
                                dt.Columns.Add("RecPlant");
                                dt.Columns.Add("RecPlantDesc");
                                dt.Columns.Add("DispPlant");
                                dt.Columns.Add("DispPlantDesc");
                                dt.Columns.Add("InvoiceNo");
                                dt.Columns.Add("InvoiceDate");
                                dt.Columns.Add("MaterialNo");
                                dt.Columns.Add("MatDesc");
                                dt.Columns.Add("UOM");
                                dt.Columns.Add("BatchNo");
                                dt.Columns.Add("Quantity");
                                dt.Columns.Add("TransporterCode");
                                dt.Columns.Add("TransporterName");
                                dt.Columns.Add("LrNo");
                                dt.Columns.Add("LrDate");
                                dt.Columns.Add("TotalCaseQty");
                                dt.Columns.Add("VehicleNo");
                                foreach (var itemList in modelList)
                                {
                                    // Previous & Current Date Validation
                                    if (Convert.ToDateTime(itemList.InvoiceDate).Date <= Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")) &&
                                         Convert.ToDateTime(itemList.LrDate).Date <= Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")))
                                    {
                                        // Add Rows DataTable
                                        dt.Rows.Add(itemList.pkId, itemList.DeliveryNo, itemList.ActualGIDate, itemList.RecPlant,
                                                itemList.RecPlantDesc, itemList.DispPlant, itemList.DispPlantDesc,
                                                itemList.InvoiceNo, itemList.InvoiceDate, itemList.MaterialNo, itemList.MatDesc, itemList.UoM, itemList.BatchNo, itemList.Quantity,
                                                itemList.TransporterCode, itemList.TransporterName, itemList.LrNo,
                                                itemList.LrDate, itemList.TotalCaseQty, itemList.VehicleNo);
                                    }
                                    else
                                    {
                                        NextDateInvalid = true;
                                    }
                                }

                                // Next Date Validation
                                if (NextDateInvalid)
                                {
                                    message.RetResult = BusinessCont.msg_InvalidNextDate;
                                }
                                else
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        using (var db = new CFADBEntities())
                                        {
                                                SqlConnection connection = (SqlConnection)db.Database.Connection;
                                                SqlCommand cmd = new SqlCommand("CFA.usp_ImportTransitData", connection);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                                BranchIdParameter.SqlDbType = SqlDbType.Int;
                                                SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                                CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                                SqlParameter ImportTransDataParameter = cmd.Parameters.AddWithValue("@ImpTrDt", dt);
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportTransitData", "Import Transit Upload Data:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        #endregion

        #region Get Transit Data List
        [HttpGet]
        [Route("InventoryInward/GetTransitDataList/{BranchId}/{CompId}")]
        public List<GetTransitListModel> GetTransitDataList(int BranchId, int CompId)
        {
            List<GetTransitListModel> TransitLst = new List<GetTransitListModel>();
            try
            {
                TransitLst = _unitOfWork.inventoryInwardRepository.GetTransitDataLst(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransitDataList", "Get Transit Data List " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransitLst;
        }
        #endregion

        #region Raise Insurance Claim Add Edit And Delete
        [HttpPost]
        [Route("InventoryInward/RaiseInsuranceClaim")]
        public int RaiseInsuranceClaim([FromBody]RaiseInsuranceClaimModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.inventoryInwardRepository.RaiseInsuranceClaim(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "RaiseInsuranceClaim", "Raise Insurance Claim", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Raise Insurance Claim List
        [HttpGet]
        [Route("InventoryInward/GetRaiseInsuranceClaimList/{BranchId}/{CompId}")]
        public List<GetRaiseInsuranceClaimListModel> GetRaiseInsuranceClaimList(int BranchId, int CompId)
        {
            List<GetRaiseInsuranceClaimListModel> ClaimList = new List<GetRaiseInsuranceClaimListModel>();
            try
            {
                ClaimList = _unitOfWork.inventoryInwardRepository.GetRaiseInsuranceClaimList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetRaiseInsuranceClaimList", "Get Raise Insurance Claim List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ClaimList;
        }
        #endregion

        #region Get Map Inward Vehicle Raise Cncrn List
        [HttpGet]
        [Route("InventoryInward/GetMapInwardVehicleRaiseCncrnList/{BranchId}/{CompId}")]
        public List<MapInwardVehicleForMobModel> GetMapInwardVehicleRaiseCncrnList(int BranchId, int CompId)
        {
            List<MapInwardVehicleForMobModel> concernList = new List<MapInwardVehicleForMobModel>();
            try
            {
                concernList = _unitOfWork.inventoryInwardRepository.GetMapInwardVehicleRaiseCncrnList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetMapInwardVehicleRaiseCncrnList", "Get Map Inward Vehicle Raise Cncrn List (BranchId: " + BranchId + "  CompanyId: " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return concernList;
        }
        #endregion

        #region Resolve Vehicle Issue
        [HttpPost]
        [Route("InventoryInward/ResolveVehicleIssue")]
        public int ResolveVehicleIssue([FromBody] RaiseInsuranceClaimModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.inventoryInwardRepository.ResolveVehicleIssue(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveVehicleIssue", "Resolve Vehicle Issue", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Approve SAN 
        [HttpPost]
        [Route("InventoryInward/ApproveSANAdd")]
        public int ApproveSANAdd([FromBody] ApproveSAN model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.inventoryInwardRepository.ApproveSANAdd(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ApproveSANAdd", "Approve SAN Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Approve Claim and Email Send
        [HttpPost]
        [Route("InventoryInward/ApproveClaimAdd")]
        public int ApproveClaimAdd([FromBody] ApproveClaim model)
        {
            int result = 0;
            string emailDtls = string.Empty;
            List<ApproveClaim> approvalclaim = new List<ApproveClaim>();
            try
            {
                result = _unitOfWork.inventoryInwardRepository.ApproveClaimAdd(model);
                //if (result > 0)
                //{
                //    BusinessCont.SaveLog(0, 0, 0, "SendEmailForClaimApprove", " ", "START", "");
                //    emailDtls = _unitOfWork.inventoryInwardRepository.SendEmailForClaimApprove(model.ClaimNo, model.ApproveClaimDate, model.BranchId, model.CompId);
                //    BusinessCont.SaveLog(0, 0, 0, "SendEmailForClaimApprove", " ", "END", "");
                //}
                //else
                //{
                //    BusinessCont.SaveLog(0, 0, 0, "ApproveClaimAdd", "No Records Found", "", "");
                //}
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ApproveClaimAdd", "Approve Claim Add", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return result;
        }
        #endregion

        // Mobile API START
        #region Get Transit Data List For Mobile
        [HttpGet]
        [Route("InventoryInward/GetTransitDataListForMob/{BranchId}/{CompId}")]
        public List<ImportTransitListModel> GetTransitDataListForMob(int BranchId, int CompId)
        {
            List<ImportTransitListModel> TransitLst = new List<ImportTransitListModel>();
            try
            {
                TransitLst = _unitOfWork.inventoryInwardRepository.GetTransitDataListForMob(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetTransitDataListForMob", "Get Transit Data List For Mob " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TransitLst;
        }
        #endregion

        #region Raise Concern For Transit Data
        [HttpPost]
        [Route("InventoryInward/RaiseConcernForTransitData")]
        public int RaiseConcernForTransitData([FromBody]RaiseConcernForTransitDataModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.inventoryInwardRepository.RaiseConcernForTransitData(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "RaiseConcernForTransitData", "Raise Concern For Transit Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Add Edit Map Inward Vehicle with Transit LR Details
        [HttpPost]
        [Route("InventoryInward/MapInwardVehicleWithTransitLR")]
        public int MapInwardVehicleWithTransitLR([FromBody]MapInwardVehicleForMobModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.inventoryInwardRepository.MapInwardVehicleWithTransitLR(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "MapInwardVehicleWithTransitLR", "Map Inward Vehicle With Transit LR", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Map Inward Vehicle With Transit LR For Mob
        [HttpGet]
        [Route("InventoryInward/GetMapInwardVehicleWithTransitLR/{BranchId}/{CompId}")]
        public List<MapInwardVehicleForMobModel> GetMapInwardVehicleWithTransitLR(int BranchId, int CompId)
        {
            List<MapInwardVehicleForMobModel> vehicleList = new List<MapInwardVehicleForMobModel>();
            try
            {
                vehicleList = _unitOfWork.inventoryInwardRepository.GetMapInwardVehicleWithTransitLR(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetMapInwardVehicleWithTransitLR", "Get Map Inward Vehicle With Transit LR", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return vehicleList;
        }
        #endregion

        #region Update Vehicle CheckList And Upload Img
        [HttpPost]
        [Route("InventoryInward/UpdateVehicleCheckListAndUploadImg")]
        public int UpdateVehicleCheckListAndUploadImg()
        {
            var httpRequest = HttpContext.Current.Request;
            VehicleChecklistDtlsModel model = new VehicleChecklistDtlsModel();
            int retValue = 0;
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    for (int i = 0; i < httpRequest.Files.Count; i++)
                    {
                        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VhcleInspImg\\") + httpRequest.Params.Get(2) + "-" + httpRequest.Files[i].FileName;
                        httpRequest.Files[i].SaveAs(filePath);
                        if (model.Img1 == null)
                        {
                            model.Img1 = httpRequest.Params.Get(2) + "-" + httpRequest.Files[i].FileName;
                        }
                        else if (model.Img2 == null)
                        {
                            model.Img2 = httpRequest.Params.Get(2) + "-" + httpRequest.Files[i].FileName;
                        }
                        else if (model.Img3 == null)
                        {
                            model.Img3 = httpRequest.Params.Get(2) + "-" + httpRequest.Files[i].FileName;
                        }
                        else if (model.Img4 == null)
                        {
                            model.Img4 = httpRequest.Params.Get(2) + "-" + httpRequest.Files[i].FileName;
                        }
                    }
                    model.BranchId = Convert.ToInt32(httpRequest.Params.Get(0));
                    model.CompId = Convert.ToInt32(httpRequest.Params.Get(1));
                    model.LREntryId = Convert.ToInt32(httpRequest.Params.Get(2));
                    model.AddedBy = httpRequest.Params.Get(3);
                    model.CLQueId = httpRequest.Params.Get(4);
                    model.TransitId = Convert.ToInt32(httpRequest.Params.Get(5));
                    retValue = _unitOfWork.inventoryInwardRepository.VehicleChecklistMstAdd(model);
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "UpdateVehicleCheckListAndUploadImg", "Update Vehicle CheckList And Upload Img", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return retValue;
        }
        #endregion

        #region Get Vehicle Checklist Details For Mobile
        [HttpGet]
        [Route("InventoryInward/GetVehicleChecklistDetailsForMob/{BranchId}/{CompId}")]
        public List<ChckelistDtls> GetVehicleChecklistDetailsForMob(int BranchId, int CompId)
        {
            List<ChckelistDtls> listcount = new List<ChckelistDtls>();
            try
            {
                listcount = _unitOfWork.inventoryInwardRepository.GetVehicleChecklistDetailsForMob(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetVehicleChecklistDetailsForMob", "Get Vehicle Checklist Details For Mobile ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return listcount;
        }
        #endregion

        #region Delete Vehicle Checklist Details For Mob
        [HttpPost]
        [Route("InventoryInward/DeleteVehicleChecklistDetailsForMob")]
        public int DeleteVehicleChecklistDetailsForMob([FromBody] VehicleChecklistDtlsModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.inventoryInwardRepository.DeleteVehicleChecklistDetailsForMob(model);
            }
            catch(Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "DeleteVehicleChecklistDetailsForMob", "Delete Vehicle Checklist Details For Mob", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get Raise Concern Of Transit Data for mobile
        [HttpGet]
        [Route("InventoryInward/GetRaiseConcernListForMob/{BranchId}/{CompId}")]
        public List<RaiseConcernForTransitData> GetRaiseConcernListForMob(int BranchId, int CompId)
        {
            List<RaiseConcernForTransitData> listcount = new List<RaiseConcernForTransitData>();
            try
            {
                listcount = _unitOfWork.inventoryInwardRepository.GetRaiseConcernListForMob(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetRaiseConcernforMob", "Get Raise Concern for Mob ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }

            return listcount;
        }
        #endregion

        #region Get Inv Inward Pages All Count
        [HttpPost]
        [Route("InventoryInward/GetInvInwardPagesAllCount")]
        public InvInwardAllCountModel GetInvInwardPagesAllCount(InvInwardAllCountModel model)
        {
            InvInwardAllCountModel TotalCount = new InvInwardAllCountModel();
            try
            {
                TotalCount = _unitOfWork.inventoryInwardRepository.GetInvInwardPagesAllCount(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvInwardPagesAllCount", "Get Inv Inward Pages All Count ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return TotalCount;
        }
        #endregion

        #region Resolve Raised Concern At Op Level
        [HttpPost]
        [Route("InventoryInward/ResolveRaisedConcernAtOpLevel")]
        public int ResolveRaisedConcernAtOpLevel([FromBody]RaiseConcernForTransitData model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.inventoryInwardRepository.ResolveRaisedConcernAtOpLevel(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "ResolveRaisedConcernAtOpLevel", "Resolve Raised Concern At Op Level ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Inventory Inward Dashbord Count For All Login
        [HttpPost]
        [Route("InventoryInward/InventoryCountForallLogin")]
        public dashbordcountmodel GetInventoryCountForSupervisor(DashBoardCommonModel model)
        {
            dashbordcountmodel dashcount = new dashbordcountmodel();
            try
            {
                dashcount = _unitOfWork.inventoryInwardRepository.GetInventoryCountForSupervisor(model.BranchId, model.CompanyId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInventoryCountForSupervisor", "Get Inventory Count For Supervisor" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompanyId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return dashcount;
        }
        #endregion

        #region Get Inventory Inwrd dashboard Filtered List
        [HttpGet]
        [Route("InventoryInward/GetListForDashboardInventoryInwrd/{BranchId}/{CompId}")]
        public List<DashboardInventoryInwrdMdal> GetListForDashboardInventoryInwrdList(int BranchId, int CompId)
        {
            List<DashboardInventoryInwrdMdal> GetListForDashboardInventoryInwrd = new List<DashboardInventoryInwrdMdal>();
            try
            {
                GetListForDashboardInventoryInwrd = _unitOfWork.inventoryInwardRepository.GetListForDashboardInventoryInwrdList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetListForDashboardInventoryInwrd", "GetListForDashboardInventoryInwrd", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return GetListForDashboardInventoryInwrd;
        }
        #endregion

        #region Start - Inward Supervisor Dashboard Mob
        [HttpGet]
        [Route("InventoryInward/InwardSupervisorDashboardMob/{BranchId}/{CompId}")]
        public List<InwardSupervisorDashboardMobModel> InwardSupervisorDashboardMob(int BranchId, int CompId)
        {
            List<InwardSupervisorDashboardMobModel> modelList = new List<InwardSupervisorDashboardMobModel>();
            try
            {
                modelList = _unitOfWork.inventoryInwardRepository.InwardSupervisorDashboardMob(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "InwardSupervisorDashboardMob", "Inward Supervisor Dashboard Mob", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return modelList;
        }
        #endregion End - Inward Supervisor Dashboard Mob

        #region Get Inv Inward Cumm Vehicle List
        [HttpGet]
        [Route("InventoryInward/GetInvInwardCummVehicleList/{BranchId}/{CompId}")]
        public List<DashboardInventoryInwrdMdal> GetInvInwardCummVehicleList(int BranchId, int CompId)
        {
            List<DashboardInventoryInwrdMdal> CummVehicleLst= new List<DashboardInventoryInwrdMdal>();
            try
            {
                CummVehicleLst = _unitOfWork.inventoryInwardRepository.GetInvInwardCummVehicleList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvInwardCummVehicleList", "Get Inv Inward Cumm Vehicle List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return CummVehicleLst;
        }
        #endregion

        #region  Get List Transit Vehicle Checlist For View Image
        [HttpPost]
        [Route("InventoryInward/TransitVhcleChkLstForViewImg")]
        public List<VhcleChkLstForViewImgModel> TransitVhcleChkLstForViewImg ([FromBody] VehicleChkLstReport model)
        {
            List<VhcleChkLstForViewImgModel> VhcleChkLst = new List<VhcleChkLstForViewImgModel>();
            try
            {
                VhcleChkLst = _unitOfWork.inventoryInwardRepository.TransitVhcleChkLstForViewImg(model.BranchId,model.CompId,model.FromDate,model.ToDate);
            }
            catch(Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "TransitVhcleChkLstForViewImg", "Transit Vhcle Chk Lst Fo rView Img", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return VhcleChkLst;
        }

        #endregion 

        #region Get Owner Inv Inward Dash Smmry List
        [HttpGet]
        [Route("InventoryInward/GetOwnerInvInwardDashSmmryList")]
        public List<OwnInvInwardDashSmmryList> GetOwnerInvInwardDashSmmryList()
        {
            List<OwnInvInwardDashSmmryList> modelList = new List<OwnInvInwardDashSmmryList>();
            try
            {
                modelList = _unitOfWork.inventoryInwardRepository.GetOwnerInvInwardDashSmmryList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOwnerInvInwardDashSmmryList", "Get Owner Inv Inward Dash Smmry List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return modelList;
        }
        #endregion

        // Mobile API END

        // Not Used API
        //#region InsuranceClaim Add Edit
        //[HttpPost]
        //[Route("InventoryInward/InsuranceClaimAddEdit")]
        //public int InsuranceClaimAddEdit([FromBody] InsuranceClaimModel model)
        //{
        //    int RetValue = 0;
        //    try
        //    {
        //        RetValue = _unitOfWork.inventoryInwardRepository.InsuranceClaimAddEdit(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "InsuranceClaimAddEdit", "InsuranceClaim AddEdit", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return RetValue;
        //}
        //#endregion

        //#region Insurance Claim List
        //[HttpGet]
        //[Route("InventoryInward/InsuranceClaimList/{BranchId}/{CompId}")]
        //public List<InsuranceClaimModel> InsuranceClaimList(int BranchId, int CompId)
        //{
        //    List<InsuranceClaimModel> InsClaimList = new List<InsuranceClaimModel>();
        //    try
        //    {
        //        InsClaimList = _unitOfWork.inventoryInwardRepository.GetInsuranceClaimList(BranchId, CompId);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClaimList", "Insurance Claim List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
        //    }
        //    return InsClaimList;
        //}
        //#endregion   

        //#region Get Invoice List
        //[HttpGet]
        //[Route("InventoryInward/InsuranceClaimInvoiceList/{BranchId}/{CompId}")]
        //public List<InvoiceListModel> GetInvoiceList(int BranchId, int CompId)
        //{
        //    List<InvoiceListModel> getInvoicelist = new List<InvoiceListModel>();
        //    try
        //    {
        //        getInvoicelist = _unitOfWork.inventoryInwardRepository.GetInvoiceList(BranchId, CompId);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetBranchList", "Get Invoice List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
        //    }
        //    return getInvoicelist;
        //}
        //#endregion

        //#region Get Insurance Claim Invoice By Id List
        //[HttpGet]
        //[Route("InventoryInward/GetInsuranceClaimInvById/{BranchId}/{CompanyId}/{InvoiceId}")]
        //public List<InsuranceClaimModel> GetInsuranceClaimInvById(int BranchId, int CompanyId, string InvoiceId)
        //{
        //    List<InsuranceClaimModel> GetClaimInvByIdTypeList = new List<InsuranceClaimModel>();
        //    try
        //    {
        //        GetClaimInvByIdTypeList = _unitOfWork.inventoryInwardRepository.GetInsuranceClaimInvByIdList(BranchId, CompanyId, InvoiceId);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClaimInvById", "Get Insurance Claim Inv By Id", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
        //    }
        //    return GetClaimInvByIdTypeList;
        //}
        //#endregion

        //#region Send Email For approval Update Alert
        //[HttpGet]
        //[Route("InventoryInward/GetInsuranceClmByIdForEmail/{BranchId}/{CompId}/{ClaimId}")]
        //public string GetInsuranceClmByIdForEmail(int BranchId, int CompId, int ClaimId)
        //{
        //    bool IsEmailSend = false;
        //    string MailFilePath = string.Empty, EmailCC = string.Empty,
        //    Subject = string.Empty, Date = string.Empty, CCEmail = string.Empty, isEmailFlag = string.Empty;
        //    string msgHTMLOutput = string.Empty;
        //    Date = DateTime.Today.Date.ToString("dd-MM-yyyy");
        //    Subject = ConfigurationManager.AppSettings["InsuranceClaimAppr"] + Date + " ";
        //    List<InsuranceClaimModel> EmailDtls = new List<InsuranceClaimModel>();
        //    List<CCEmailDtls> CCEmailList = new List<CCEmailDtls>();
        //    try
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "START", "");

        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 1", "");

        //        MailFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailFile\\Approval_Update.html");

        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 2", "");

        //        EmailDtls = _unitOfWork.inventoryInwardRepository.GetInsuranceClmDtlsForEmail(BranchId, CompId, ClaimId); // create table for claimid wise (ClaimNo,ClaimDate and ClaimAmt) against claimid records

        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 3", "");

        //        msgHTMLOutput = _unitOfWork.inventoryInwardRepository.InsuranceClaimforApproval(EmailDtls, MailFilePath);

        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 4", "");

        //        using (CFADBEntities contextManager = new CFADBEntities())
        //        {
        //            BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 5", "");

        //            CCEmailList = _unitOfWork.chequeAccountingRepository.GetCCEmailDtlsPvt(1, 1, 8);
        //            if (CCEmailList.Count > 0)
        //            {
        //                BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 6", "");

        //                for (int i = 0; i < CCEmailList.Count; i++)
        //                {
        //                    CCEmail += ";" + CCEmailList[i].Email;
        //                }
        //                EmailCC = CCEmail.TrimStart(';');
        //            }

        //            BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 7", "");

        //            if (EmailDtls.Count > 0)
        //            {
        //                BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 8", "");

        //                foreach (var item in EmailDtls)
        //                {
        //                    BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 9", "");

        //                    if (item.IsEmail == false) // email send process only not sent
        //                    {
        //                        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 10", "");

        //                        BusinessCont.SaveLog(0, 0, 0, "item.EmailId:  " + item.EmailId, " EmailCC:   " + EmailCC + " item.IsEmail:  " + item.IsEmail + " Subject:  " + Subject + " IsEmailSend:  " + IsEmailSend, " msgHTMLOutput:  " + msgHTMLOutput, "");

        //                        //IsEmailSend = EmailNotification.sendEmailForApproval(item.EmailId, EmailCC, Subject, msgHTMLOutput);

        //                        BusinessCont.SaveLog(0, 0, 0, "EmailNotification", "emailNotification", "Start", "");
        //                        EmailNotification emailNotification = new EmailNotification();
        //                        IsEmailSend = emailNotification.sendEmailForApproval(item.EmailId, EmailCC, Subject, msgHTMLOutput);
        //                        BusinessCont.SaveLog(0, 0, 0, "EmailNotification", "emailNotification", "End", "");

        //                        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 11", "");
        //                    }

        //                    BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 12", "");
        //                }

        //                BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 13", "");

        //                if (IsEmailSend == true) //email sent
        //                {
        //                    BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 14", "");

        //                    isEmailFlag = _unitOfWork.inventoryInwardRepository.UpdateMailForApproval(BranchId, CompId, ClaimId, IsEmailSend);

        //                    BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 15", "");
        //                }
        //            }
        //            else
        //            {
        //                BusinessCont.SaveLog(0, 0, 0, "EmailDtls", "No Records Found", "", "");
        //            }
        //        }

        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "END", "");
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 16", BusinessCont.ExceptionMsg(ex.InnerException));

        //        BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmDtlsForEmail", "Get Insurance Clm Dtls For Email " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }

        //    BusinessCont.SaveLog(0, 0, 0, "GetInsuranceClmByIdForEmail", "Get InsuranceClm By Id For Email", "Step 17", "");

        //    return isEmailFlag;
        //}
        //#endregion

        //#region Invoice Inward Raise Request By Id For Mobile
        //[HttpPost]
        //[Route("InventoryInward/updateInvInwardRaiseRequestById")]
        //public string updateInvInwardRaiseRequestById([FromBody] InvInwardRaiseRequestByIdForModel model)
        //{
        //    string Result = string.Empty;
        //    try
        //    {
        //        Result = _unitOfWork.inventoryInwardRepository.UpdateInvInwardRaiseRequestById(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "updateInvInwardRaiseRequestById", "Invoice Inward Raise Request By Id", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return Result;
        //}
        //#endregion

        //#region  Add/Edit and Delete LR Details
        //[HttpPost]
        //[Route("InventoryInward/AddEditDeleteLrDetails")]
        //public string AddEditDeleteLrDetails([FromBody] LRDetailsModel model)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        result = _unitOfWork.inventoryInwardRepository.AddEditDeleteLrDetails(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "AddEditDeleteLrDetails", "Add Edit Delete Lr Deatils", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
        //    }
        //    return result;
        //}
        //#endregion

        //#region Get LR List Details
        //[HttpGet]
        //[Route("InventoryInward/GetLRDetailsList/{BranchId}/{CompId}")]
        //public List<LRDetailsModel> GetLRDetailsList(int BranchId, int CompId)
        //{
        //    List<LRDetailsModel> LRLst = new List<LRDetailsModel>();
        //    try
        //    {
        //        LRLst = _unitOfWork.inventoryInwardRepository.GetLRDetailsList(BranchId, CompId);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetLRDetailsList", "Get LR Deatils List " + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return LRLst;
        //}
        //#endregion

        //#region  Get Invoice In Vehicle Check List Master
        //[HttpGet]
        //[Route("InventoryInward/GetInvInVehicleCheckListMaster/{BranchId}/{CompId}/{ChecklistType}")]
        //[AllowAnonymous]
        //public List<InvInVehicleChecklistMaster> GetInvInVehicleCheckListMaster(int BranchId, int CompId, string ChecklistType)

        //{
        //    List<InvInVehicleChecklistMaster> InvInVehicleCheckList = new List<InvInVehicleChecklistMaster>();
        //    try
        //    {
        //        InvInVehicleCheckList = _unitOfWork.inventoryInwardRepository.GetInvInVehicleCheckListMaster(BranchId, CompId, ChecklistType);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetInvInVehicleCheckListMaster", "Get Invoice In Vehicle Check List Master" + "BranchId:  " + BranchId + "CompId:  " + CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return InvInVehicleCheckList;
        //}
        //#endregion

        //#region Get Claim Types List
        //[HttpGet]
        //[Route("InventoryInward/InsuranceClaimTypeList/{BranchId}/{CompanyId}")]
        //public List<InsuranceClaimModel> GetClaimTypeList(int BranchId, int CompanyId)
        //{
        //    List<InsuranceClaimModel> GetClaimTypeList = new List<InsuranceClaimModel>();
        //    try
        //    {
        //        GetClaimTypeList = _unitOfWork.inventoryInwardRepository.GetClaimTypeList(BranchId, CompanyId);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetClaimTypeList", "Get Claim Type List (BranchId: " + BranchId + "  CompanyId: " + CompanyId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
        //    }
        //    return GetClaimTypeList;
        //}
        //#endregion
    }
}
