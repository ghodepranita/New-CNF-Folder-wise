using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CNF.Business.BusinessConstant;
using CNF.Business.Model.Context;
using CNF.Business.Model.Master;
using CNF.Business.Model.OCR;
using ExcelDataReader;
using Google.Cloud.Vision.V1;
using Newtonsoft.Json;
using Image = Google.Cloud.Vision.V1.Image;

namespace CNF.API.Controllers
{
    public class OCRController : BaseApiController
    {

        #region Import Product Master Data
        [HttpPost]
        [Route("OCR/ImportProductData/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportInvoiceData(int BranchId, int CompanyId, string Addedby)
        {
            ImportReturnModel message = new ImportReturnModel();

            bool IsColumnFlag = false;
            bool InsertFlag = false;

            string columnErrorMessage_div = string.Empty, columnErrorMessage_Batch = string.Empty,
                   columnErrorMessage_MaterialDescription = string.Empty, columnErrorMessage_Code = string.Empty,
                   columnErrorMessage_ExpiryDate = string.Empty;

            bool columnErrorFlag_div = false, columnErrorFlag_Batch = false,
                 columnErrorFlag_MaterialDescription = false, columnErrorFlag_Code = false,
                 columnErrorFlag_ExpiryDate = false, columnErrorFlag_isAlphanumeric = false;

            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportProductTemplate.xls" || file.FileName == "ImportProductTemplate.XLS" || file.FileName == "ImportProductTemplate.xlsx" || file.FileName == "ImportProductTemplate.XLSX")
                    {
                        if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".XLS"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream)
;
                        }
                        else if (file.FileName.EndsWith(".xlsx") || file.FileName.EndsWith(".XLSX"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream)
;
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
                            List<ImportProductDataModel> modelList = new List<ImportProductDataModel>();
                            ImportProductDataModel model;
                            IsColumnFlag = IsColumnValidationForProduct(finalRecords); // To Check Column Name
                            if (IsColumnFlag)
                            {
                                for (int j = 1; j < finalRecords.Rows.Count; j++)
                                {
                                    model = new ImportProductDataModel();
                                    model.pkId = j;

                                    InsertFlag = true;

                                    // Division
                                    BusinessCont.TypeCheckWithData("Int32", finalRecords.Rows[j][0].ToString(), j, ref InsertFlag, ref columnErrorFlag_div, ref columnErrorMessage_div, columnErrorFlag_isAlphanumeric);
                                    // Batch No
                                    BusinessCont.TypeCheckWithData("Alphanumeric", finalRecords.Rows[j][1].ToString(), j, ref InsertFlag, ref columnErrorFlag_Batch, ref columnErrorMessage_Batch, columnErrorFlag_isAlphanumeric);
                                    // Material Description
                                    BusinessCont.TypeCheckWithData("String", finalRecords.Rows[j][2].ToString(), j, ref InsertFlag, ref columnErrorFlag_MaterialDescription, ref columnErrorMessage_MaterialDescription, columnErrorFlag_isAlphanumeric);
                                    // Code
                                    BusinessCont.TypeCheckWithData("Int32", finalRecords.Rows[j][3].ToString(), j, ref InsertFlag, ref columnErrorFlag_Code, ref columnErrorMessage_Code, columnErrorFlag_isAlphanumeric);
                                    // Expiry Date
                                    BusinessCont.TypeCheckWithData("DateTime", finalRecords.Rows[j][4].ToString(), j, ref InsertFlag, ref columnErrorFlag_ExpiryDate, ref columnErrorMessage_ExpiryDate, columnErrorFlag_isAlphanumeric);


                                    // Valid Data - Insert Flag = true
                                    if (InsertFlag)
                                    {
                                        model.Division = Convert.ToInt32(finalRecords.Rows[j][0]);
                                        model.BatchNo = Convert.ToString(finalRecords.Rows[j][1]);
                                        model.ProductName = Convert.ToString(finalRecords.Rows[j][2]);
                                        model.Code = Convert.ToString(finalRecords.Rows[j][3]);
                                        model.EXP_Date = Convert.ToDateTime(finalRecords.Rows[j][4]).ToString("yyyy/MM/dd hh:mm:ss");
                                        modelList.Add(model);
                                    }
                                }
                            }
                            // To Check Column Error Flag Maintain Column Wise
                            if (columnErrorFlag_div || columnErrorFlag_Batch ||
                               columnErrorFlag_MaterialDescription || columnErrorFlag_Code ||
                                columnErrorFlag_ExpiryDate)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_div)
                                {
                                    message.RetResult += "\n div ---- \n " + columnErrorMessage_div;
                                }
                                if (columnErrorFlag_Batch)
                                {
                                    message.RetResult += "\n Batch ---- \n " + columnErrorMessage_Batch;
                                }
                                if (columnErrorFlag_MaterialDescription)
                                {
                                    message.RetResult += "\n MaterialDescription ---- \n" + columnErrorMessage_MaterialDescription;
                                }
                                if (columnErrorFlag_Code)
                                {
                                    message.RetResult += "\n Code ---- \n " + columnErrorMessage_Code;
                                }
                                if (columnErrorFlag_ExpiryDate)
                                {
                                    message.RetResult += "\n ExpiryDate ---- \n " + columnErrorMessage_ExpiryDate;
                                }
                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("Division");
                                dt.Columns.Add("BatchNo");
                                dt.Columns.Add("ProductName");
                                dt.Columns.Add("Code");
                                dt.Columns.Add("ExpiryDate");
                                foreach (var itemList in modelList)
                                {
                                    // Add Rows DataTable
                                    dt.Rows.Add(itemList.pkId, itemList.Division, itemList.BatchNo, itemList.ProductName, itemList.Code,
                                       itemList.EXP_Date);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    using (var db = new CFADBEntities())
                                    {
                                        {
                                            SqlConnection connection = (SqlConnection)db.Database.Connection;
                                            SqlCommand cmd = new SqlCommand("CFA.usp_ImportProductData", connection);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                            BranchIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                            CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter ImportInvDataParameter = cmd.Parameters.AddWithValue("@ProdMst", dt);
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportProductData", "Import Product Upload Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        /// <summary>
        /// Is Column Validation For Invoice
        /// </summary>
        /// <param name="finalRecords"></param>
        /// <returns></returns>
        private bool IsColumnValidationForProduct(DataTable finalRecords)
        {
            bool IsValid = false;

            try
            {
                string div = Convert.ToString(finalRecords.Rows[0][0]);
                string Batch = Convert.ToString(finalRecords.Rows[0][1]);
                string MaterialDescription = Convert.ToString(finalRecords.Rows[0][2]);
                string Code = Convert.ToString(finalRecords.Rows[0][3]);
                string ExpiryDate = Convert.ToString(finalRecords.Rows[0][4]);

                if (div == "div" && Batch == "Batch" &&
                    MaterialDescription == "MaterialDescription" && Code == "Code" &&
                    ExpiryDate == "ExpiryDate")
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
                BusinessCont.SaveLog(0, 0, 0, "IsColumnValidationForProduct", "Is Column Validation For Product", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return IsValid;
        }
        #endregion

        #region  New Import Product Master Data
        [HttpPost]
        [Route("OCR/ImportProductDataNew/{BranchId}/{CompanyId}/{Addedby}")]
        public string ImportProductDataNew(int BranchId, int CompanyId, string Addedby)
        {
            ImportReturnModel message = new ImportReturnModel();

            bool IsColumnFlag = false;
            bool InsertFlag = false;
            string columnErrorMessage_div = string.Empty, columnErrorMessage_Batch = string.Empty,
                              columnErrorMessage_MaterialDescription = string.Empty, columnErrorMessage_Code = string.Empty,
                              columnErrorMessage_ExpiryDate = string.Empty;

            bool columnErrorFlag_div = false, columnErrorFlag_Batch = false,
                 columnErrorFlag_MaterialDescription = false, columnErrorFlag_Code = false,
                 columnErrorFlag_ExpiryDate = false, columnErrorFlag_isAlphanumeric = false;

            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var Importfor = "Import Product";
                    var columnHeaderList = _unitOfWork.OrderDispatchRepository.GetColumnHeaderList(BranchId, CompanyId, Importfor);
                    if (columnHeaderList.Count == 0)
                    {
                        message.RetResult = "Master data is not configured, Please contact to system admin";
                        return message.RetResult;
                    }
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;
                    IExcelDataReader reader = null;
                    if (file.FileName == "ImportProductTemplate.xls" || file.FileName == "ImportProductTemplate.XLS" || file.FileName == "ImportProductTemplate.xlsx" || file.FileName == "ImportProductTemplate.XLSX")
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
                            List<ImportProductDataModel> modelList = new List<ImportProductDataModel>();
                            ImportProductDataModel model;

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
                                model = new ImportProductDataModel();
                                model.pkId = j;
                                InsertFlag = true;
                                // Iterate through the columns dynamically using the columnIndexMapping
                                foreach (var columnIndex in columnIndexMapping.Keys)
                                {
                                    string dbColumnName = columnIndexMapping[columnIndex].Item1;
                                    string columnDataType = columnIndexMapping[columnIndex].Item2;
                                    string cellValue = finalRecords.Rows[j][columnIndex].ToString();
                                    // div
                                    if (dbColumnName == "div")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_div, ref columnErrorMessage_div, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Batch
                                    if (dbColumnName == "Batch")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_Batch, ref columnErrorMessage_Batch, columnErrorFlag_isAlphanumeric);
                                    }
                                    // MaterialDescription
                                    if (dbColumnName == "MaterialDescription")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_MaterialDescription, ref columnErrorMessage_MaterialDescription, columnErrorFlag_isAlphanumeric);
                                    }
                                    // Code
                                    if (dbColumnName == "Code")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_Code, ref columnErrorMessage_Code, columnErrorFlag_isAlphanumeric);
                                    }
                                    // ExpiryDate
                                    if (dbColumnName == "ExpiryDate")
                                    {
                                        BusinessCont.TypeCheckWithData(columnDataType, cellValue, j, ref InsertFlag, ref columnErrorFlag_ExpiryDate, ref columnErrorMessage_ExpiryDate, columnErrorFlag_isAlphanumeric);
                                    }

                                    // Assign values to model properties dynamically based on dbColumnName
                                    switch (dbColumnName)
                                    {
                                        case "Division":
                                            model.Division = Convert.ToInt32(cellValue);
                                            break;
                                        case "BatchNo":
                                            model.BatchNo = cellValue;
                                            break;
                                        case "ProductName":
                                            model.ProductName = cellValue;
                                            break;
                                        case "Code":
                                            model.Code = cellValue;
                                            break;
                                        case "EXP_Date":
                                            model.EXP_Date = cellValue;
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
                            if (columnErrorFlag_div || columnErrorFlag_Batch ||
                               columnErrorFlag_MaterialDescription || columnErrorFlag_Code ||
                                columnErrorFlag_ExpiryDate)
                            {
                                message.RetResult = "\n Below Columns has invalid data:  ";
                                if (columnErrorFlag_div)
                                {
                                    message.RetResult += "\n div ---- \n " + columnErrorMessage_div;
                                }
                                if (columnErrorFlag_Batch)
                                {
                                    message.RetResult += "\n Batch ---- \n " + columnErrorMessage_Batch;
                                }
                                if (columnErrorFlag_MaterialDescription)
                                {
                                    message.RetResult += "\n MaterialDescription ---- \n" + columnErrorMessage_MaterialDescription;
                                }
                                if (columnErrorFlag_Code)
                                {
                                    message.RetResult += "\n Code ---- \n " + columnErrorMessage_Code;
                                }
                                if (columnErrorFlag_ExpiryDate)
                                {
                                    message.RetResult += "\n ExpiryDate ---- \n " + columnErrorMessage_ExpiryDate;
                                }
                            }

                            if (modelList.Count > 0)
                            {
                                // Create DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pkId");
                                dt.Columns.Add("Division");
                                dt.Columns.Add("BatchNo");
                                dt.Columns.Add("ProductName");
                                dt.Columns.Add("Code");
                                dt.Columns.Add("ExpiryDate");

                                foreach (var itemList in modelList)
                                {
                                    // Add Rows DataTable
                                    dt.Rows.Add(itemList.pkId, itemList.Division, itemList.BatchNo, itemList.ProductName, itemList.Code,
                                       itemList.EXP_Date);
                                }

                                if (dt.Rows.Count > 0)
                                {
                                    using (var db = new CFADBEntities())
                                    {
                                        {
                                            SqlConnection connection = (SqlConnection)db.Database.Connection;
                                            SqlCommand cmd = new SqlCommand("CFA.usp_ImportProductData", connection);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            SqlParameter BranchIdParameter = cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                            BranchIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter CompanyIdParameter = cmd.Parameters.AddWithValue("@CompId", CompanyId);
                                            CompanyIdParameter.SqlDbType = SqlDbType.Int;
                                            SqlParameter ImportInvDataParameter = cmd.Parameters.AddWithValue("@ProdMst", dt);
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
                BusinessCont.SaveLog(0, BranchId, CompanyId, "ImportProductData", "Import Product Data:  " + Addedby, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return message.RetResult;
        }

        #endregion

        #region Get Product Master List 
        [HttpGet]
        [Route("OCR/GetProductDataList/{BranchId}/{CompId}")]
        public List<ProductDataListModel> GetProductDataList(int BranchId, int CompId)
        {
            List<ProductDataListModel> ProductLst = new List<ProductDataListModel>();
            try
            {
                ProductLst = _unitOfWork.OCRRepository.GetProductDataList(BranchId, CompId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetProductDataList", "Get Product Data List ", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return ProductLst;
        }

        #endregion

        //#region Get OCR Image Text Data
        //[HttpPost]
        //[Route("OCR/GetOCRImageTextData")]
        //public OCRTextModel GetOCRImageTextData()
        // {
        //    var httpRequest = HttpContext.Current.Request;
        //    OCRTextModel model = new OCRTextModel();
        //    string FileReadPath = string.Empty, AuthJsonFilepath = string.Empty, retValue = string.Empty;
        //    string[] SplitedData = null;
        //    try
        //    {
        //        if (httpRequest.Files.Count > 0)
        //        {
        //            var SaveFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OCRImages\\") + httpRequest.Files[0].FileName;
        //            httpRequest.Files[0].SaveAs(SaveFilePath);
        //            model.ImageName = httpRequest.Files[0].FileName;

        //            FileReadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OCRImages\\" + model.ImageName);
        //            AuthJsonFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AuthenticationData.json");
        //            var imageAnnotatorClientBuilder = new ImageAnnotatorClientBuilder();
        //            if (!string.IsNullOrEmpty(AuthJsonFilepath))
        //            {
        //                imageAnnotatorClientBuilder.CredentialsPath = AuthJsonFilepath;
        //            }
        //            var client = imageAnnotatorClientBuilder.Build();
        //            using (var imageStream = File.OpenRead(FileReadPath))
        //            {
        //                var image = Image.FromStream(imageStream);
        //                var response = client.DetectText(image);
        //                if (response != null)
        //                {
        //                    var DeserializeObj = JsonConvert.DeserializeObject<List<OCRDetectedText>>(Convert.ToString(response));

        //                    foreach (var data in DeserializeObj)
        //                    {
        //                        model.DetectedText = data.description;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            BusinessCont.SaveLog(0, 0, 0, "No file found to save", "No file found to save", BusinessCont.FailStatus, "File Not Found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetOCRImageTextData", "Get OCR Image Text Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
        //    }
        //    return model;
        //}
        //#endregion

        #region Get OCR Image Text Data
        [HttpPost]
        [Route("OCR/GetOCRImageTextData")]
        public OCRTextModel GetOCRImageTextData()
        {
            var httpRequest = HttpContext.Current.Request;
            OCRTextModel model = new OCRTextModel();
            string FileReadPath = string.Empty, AuthJsonFilepath = string.Empty, retValue = string.Empty;
            string[] SplitedData = null;
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    var SaveFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OCRImages\\") + httpRequest.Files[0].FileName;
                    httpRequest.Files[0].SaveAs(SaveFilePath);
                    model.ImageName = httpRequest.Files[0].FileName;

                    FileReadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OCRImages\\" + model.ImageName);
                    AuthJsonFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AuthenticationData.json");
                    var imageAnnotatorClientBuilder = new ImageAnnotatorClientBuilder();
                    if (!string.IsNullOrEmpty(AuthJsonFilepath))
                    {
                        imageAnnotatorClientBuilder.CredentialsPath = AuthJsonFilepath;
                    }
                    var client = imageAnnotatorClientBuilder.Build();

                    // Use a lock to synchronize access to the file
                    lock (FileReadPath)
                    {
                        using (var imageStream = File.OpenRead(FileReadPath))
                        {
                            var image = Image.FromStream(imageStream);
                            var response = client.DetectText(image);
                            if (response != null)
                            {
                                var DeserializeObj = JsonConvert.DeserializeObject<List<OCRDetectedText>>(Convert.ToString(response));

                                foreach (var data in DeserializeObj)
                                {
                                    model.DetectedText = data.description;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    BusinessCont.SaveLog(0, 0, 0, "No file found to save", "No file found to save", BusinessCont.FailStatus, "File Not Found");
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetOCRImageTextData", "Get OCR Image Text Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion

        #region Get Product Details By Batch No
        [HttpGet]
        [Route("OCR/GetProductDetailsByBatchNo/{BatchNo}/{BranchId}/{CompId}/{AddedBy}")]
        public ProductBatchModel GetProductDetailsByBatchNo(string BatchNo, int BranchId, int CompId, int AddedBy)
        {
            ProductBatchModel prodModel = new ProductBatchModel();
            try
            {
                prodModel = _unitOfWork.OCRRepository.GetProductDetailsByBatchNo(BatchNo, BranchId, CompId, AddedBy);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetProductDetailsByBatchNo", "Get Product Details By Batch No", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return prodModel;
        }
        #endregion

        #region Save OCR Text Data
        [HttpPost]
        [Route("OCR/SaveOCRTextData")]
        public int SaveOCRTextData([FromBody] OCRModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.OCRRepository.SaveOCRTextData(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveOCRTextData", "Save OCR Text Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        //#region Get OCR Text Data
        //[HttpGet]
        //[Route("OCR/GetOCRTextData/{BranchId}/{CompId}")]
        //public List<OCRModel> GetOCRTextData(int BranchId, int CompId)
        //{
        //    List<OCRModel> ocrList = new List<OCRModel>();
        //    try
        //    {
        //        ocrList = _unitOfWork.OCRRepository.GetOCRTextData(BranchId, CompId);
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, "GetOCRTextData", "Get OCR Text Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
        //    }
        //    return ocrList;
        //}
        //#endregion

        #region Get OCR Text Data
        [HttpPost]
        [Route("OCR/GetOCRTextData")]
        public List<OCRModel> GetOCRTextData(OCRModel model)
        {
            List<OCRModel> GetOCRTextDataList = new List<OCRModel>();
            try
            {
                GetOCRTextDataList = _unitOfWork.OCRRepository.GetOCRTextData(model.BranchId, model.CompId, model.StockistId, model.LREntryId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "v", "GetOCRTextData", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GetOCRTextDataList;
        }
        #endregion

        #region Save RGB Data
        [HttpPost]
        [Route("OCR/SaveRGBData")]
        public int SaveRGBData([FromBody] RGBColorModel model)
        {
            int RetValue = 0;
            try
            {
                RetValue = _unitOfWork.OCRRepository.SaveRGBData(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveRGBData", "Save RGB Data", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return RetValue;
        }
        #endregion

        #region Get LR List
        [HttpGet]
        [Route("Masters/GetStockistLrList/{BranchId}/{CompanyId}/{StockistId}")]
        public List<StockistLrModel> GetStockistList(int BranchId, int CompanyId, int StockistId)
        {
            List<StockistLrModel> StockLst = new List<StockistLrModel>();
            try
            {
                StockLst = _unitOfWork.OCRRepository.GetLRList(BranchId, CompanyId, StockistId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistList", "Get Stockist List", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }
        #endregion

        #region Get Stokcist List
        [HttpGet]
        [Route("Masters/GetStockistLstForOCR/{BranchId}/{CompanyId}")]
        public List<StockistLrModel> GetStockistLstForOCR(int BranchId, int CompanyId)
        {
            List<StockistLrModel> StockLst = new List<StockistLrModel>();
            try
            {
                StockLst = _unitOfWork.OCRRepository.GetStockistLstForOCR(BranchId, CompanyId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetStockistLstForOCR", "Get Stockist List For OCR", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return StockLst;
        }
        #endregion

        #region Get Summary Report List
        [HttpPost]
        [Route("OCR/GetSummaryReportList")]
        public List<RptOCRDataSummaryListModel> GetSummaryReportList([FromBody] RptOCRDataSummaryListModel model)
        {
            List<RptOCRDataSummaryListModel> SummaryReportList = new List<RptOCRDataSummaryListModel>();
            try
            {
                SummaryReportList = _unitOfWork.OCRRepository.GetSummaryReportList(model.BranchId, model.CompId, model.FromDate, model.ToDate, model.StockistId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetSummaryReportList", "GetSummaryReportList" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return SummaryReportList;
        }
        #endregion

        #region Get Summary Report List
        [HttpPost]
        [Route("OCR/GetDetailsReportList")]
        public List<OCRDetailsRept> GetDetailsReportList([FromBody] OCRDetailsRept model)
        {
            List<OCRDetailsRept> GetDetailsReportList = new List<OCRDetailsRept>();
            try
            {
                GetDetailsReportList = _unitOfWork.OCRRepository.GetDetailsReportList(model.BranchId, model.CompId, model.FromDate, model.ToDate, Convert.ToInt32(model.StockistId));
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetDetailsReportList", "GetDetailsReportList" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return GetDetailsReportList;
        }
        #endregion

        #region Get OCR Count data
        [Route("OCR/OCRCountData")]
        public OCRCountModel OCrSummaryCounts(OCRCountModel model)
        {
            OCRCountModel InvCnts = new OCRCountModel();

            try
            {
                InvCnts = _unitOfWork.OCRRepository.OCrSummaryCounts(model.BranchId, model.CompId, model.StockistId, model.LREntryId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetInvoiceSummaryCounts", "Get Invoice Summary Counts" + "BranchId:  " + model.BranchId + "CompId:  " + model.CompId, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return InvCnts;
        }
        #endregion

    }
}
