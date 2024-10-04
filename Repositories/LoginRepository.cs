using CNF.Business.BusinessConstant;
using CNF.Business.Model.Context;
using CNF.Business.Model.Login;
using CNF.Business.Repositories.Repository;
using com.toml.dp.util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace CNF.Business.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private CFADBEntities _contextManager;
        public LoginRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }
        /// <summary>
        /// Web user login details check
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public LoginDetails LoginDetails(string UserName, string Password)
        {
            return LoginDetailsPvt(UserName, Password);
        }

        /// <summary>
        /// Web user login details check
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        private LoginDetails LoginDetailsPvt(string UserName, string Password)
        {
            LoginDetails loginDetails = new LoginDetails();
            try
            {
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                {
                    //using (SDSDBEntities contextManager = new SDSDBEntities())
                    //{
                    //    loginDetails.UserInfo = contextManager.usp_UserLoginCheck(UserName, Password, 0, "UNamePWD").Select(a => new LoginAuth
                    //    {
                    //        RoleId = a.RoleId,
                    //        RefNo = a.RefNo,
                    //        RoleName = a.RoleName,
                    //        UserName = a.UserName,
                    //        ActiveStatus = a.Status,
                    //        UserId = a.UserId,
                    //        DisplayName = a.DisplayName,
                    //        LastUpdatedDate = BusinessCont.CheckNullandConvertDate(a.LastUpdatedDate),
                    //        LoginFrom = BusinessCont.LoginFromSDS,
                    //        IsFeedbackSystem = a.IsFeedbackSystem
                    //    }).FirstOrDefault();
                    //}
                    loginDetails.Status = BusinessCont.SuccessStatus;
                }
            }
            catch (Exception ex)
            {
                loginDetails.Status = BusinessCont.FailStatus;
                loginDetails.ExMsg = BusinessCont.SomethngWntWrng;
                throw ex;
            }
            return loginDetails;
        }

        /// <summary>
        /// Send OTP SMS for activate user/Login user first time only
        /// </summary>
        /// <param name="MobileNo">User Mobile No.</param>
        /// <returns></returns>
        public LoginDetails SendOTPSMS(UserModel user)
        {
            return SendOTPSMSPvt(user);
        }

        /// <summary>
        /// Send OTP SMS for activate user/Login user first time only
        /// </summary>
        /// <param name="MobileNo">User Mobile No.</param>
        /// <returns></returns>
        private LoginDetails SendOTPSMSPvt(UserModel user)
        {
            LoginDetails loginDetails = new LoginDetails();
            List<LoginAuth> UserDetails = new List<LoginAuth>();
            string FinalMessage = "", MessageId = string.Empty;
            DateTime ReqstDate = DateTime.Now; decimal StaffRefNo = 0;
            LoginAuth objLoginAuth = new LoginAuth();
            int value = 0;
            try
            {
                //using (SDSDBEntities contextManager = new SDSDBEntities())
                //{
                //    //ContextManager contextManager = new ContextManager();
                //    if (!string.IsNullOrEmpty(user.MobileNo))
                //    {
                //        var UserInfo = contextManager.usp_UserDetailsByMobileNo(user.MobileNo).FirstOrDefault();
                //        if (UserInfo != null)
                //        {
                //            if (user.UserId == 0 && user.StaffRefNo == 0)
                //            {
                //                Random random = new Random();
                //                value = random.Next(1000, 5000);
                //                FinalMessage = Convert.ToString(value) + " is OTP for " + user.MobileNo;
                //                MessageId = "1";// SMSAlertBox(user.MobileNo, FinalMessage);
                //                contextManager.usp_AddSendOTPLog(objLoginAuth.DistributorId, StaffRefNo, objLoginAuth.RoleId, user.MobileNo, value, loginDetails.Status, DateTime.Now, MessageId, loginDetails.ExMsg, ReqstDate);
                //            }
                //            objLoginAuth.DisplayName = UserInfo.StaffName;
                //            objLoginAuth.RoleId = (int)UserInfo.RoleId;

                //            objLoginAuth.MobileNo = user.MobileNo;
                //            objLoginAuth.OTP = value;
                //            objLoginAuth.DistributorId = UserInfo.DistributorID;
                //            objLoginAuth.JDEDistributorCode = UserInfo.JDEDistributorCode;
                //            objLoginAuth.DistributorName = UserInfo.DistributorName;
                //            objLoginAuth.IsDeliveryBoy = UserInfo.WhetherDeliveryBoy;
                //            objLoginAuth.IsGodownKeeper = UserInfo.IsGodownKeeper;
                //            objLoginAuth.GodownKeeperId = UserInfo.GodownKeeperId.ToString();
                //            objLoginAuth.StaffRefNo = UserInfo.StaffRefNo.ToString();
                //            objLoginAuth.RefNo = UserInfo.StaffRefNo.ToString();
                //            objLoginAuth.UserName = UserInfo.StaffName;
                //            objLoginAuth.UserAddress = UserInfo.StaffAddress;

                //            objLoginAuth.DistributorAddress = UserInfo.DistributorAddress;
                //            objLoginAuth.GSTNO = UserInfo.GSTNO;
                //            objLoginAuth.Email = UserInfo.Email;
                //            objLoginAuth.ContactDetails = UserInfo.ContactDetails;

                //            loginDetails.UserInfo = objLoginAuth;
                //            loginDetails.Status = BusinessCont.SuccessStatus;
                //            loginDetails.ExMsg = BusinessCont.ValidUser;
                //        }
                //        else
                //        {
                //            loginDetails.Status = BusinessCont.FailStatus;
                //            loginDetails.ExMsg = BusinessCont.MobNoNotFound;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                loginDetails.Status = BusinessCont.FailStatus;
                loginDetails.ExMsg = BusinessCont.SomethngWntWrng;
                throw ex;
            }
            return loginDetails;
        }

        public string ShortURL(string myurl)
        {
            string FinalMessage = string.Empty;
            try
            {
                if (myurl != null)
                {
                    string proxyURL = ConfigurationManager.AppSettings["proxyURL"];
                    int proxyPort = Convert.ToInt32(ConfigurationManager.AppSettings["proxyPort"]);
                    WebProxy proxy = new WebProxy(proxyURL, proxyPort);
                    string apiurl = ConfigurationManager.AppSettings["GoogleShortURL"];
                    WebClient client = new WebClient
                    {
                        Proxy = proxy
                    };
                    client.Headers["Content-Type"] = "application/json";
                    var response1 = client.UploadString(apiurl, JsonConvert.SerializeObject(new { longUrl = myurl }));
                    var shortUrl = (string)JObject.Parse(response1)["id"];
                    FinalMessage = Convert.ToString(shortUrl);
                }
            }
            catch (Exception)
            {
            }
            return FinalMessage;
        }

        /// <summary>
        /// Send email to user with default password
        /// </summary>
        /// <returns></returns>
        /// 
        public string sendEmailViaWebApi(string EmailId)
        {
            EmailSend Email = new EmailSend();
            string DefaultPwd = ConfigurationManager.AppSettings["DefaultPwd"];
            // MailMessage mailMessage = null;
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mailfile\\ForgetPassword.html");
            string messageformat = System.IO.File.OpenText(filePath).ReadToEnd().ToString();
            // Replace Notification Table
            messageformat = messageformat.Replace("<!--SchedulerTableString-->", DefaultPwd);
            string Date = DateTime.Today.Date.ToString("dd-MM-yyyy");
            string Subject = " Forgot password " + Date + " ";

            SmtpClient mailClient = null;
            // Create the mail message
            MailMessage mailMessage = null;

            mailClient = new SmtpClient();
            mailMessage = new MailMessage();

            if (!string.IsNullOrWhiteSpace(EmailId))//Recipient Email
            {
                string ToEmailId = EmailId.Trim();
                if (ToEmailId.Contains(";"))
                {
                    string[] emails = ToEmailId.Trim().Split(';');
                    foreach (string email in emails)
                    {
                        mailMessage.To.Add(email.Trim());
                    }
                }
                else
                {
                    mailMessage.To.Add(ToEmailId);
                }
            }
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = messageformat;
            mailClient.Send(mailMessage);

            Email.Status = "Success";
            return Email.Status;
        }

        #region User Details
        public LoginDetails GetUserDetailsByUsername(string UserName)
        {
            return GetUserDetailsByUsernamePvt(UserName);
        }

        /// <summary>
        /// Send OTP SMS for activate user/Login user first time only
        /// </summary>
        /// <param name="MobileNo">User Mobile No.</param>
        /// <returns></returns>
        private LoginDetails GetUserDetailsByUsernamePvt(string UserName)
        {
            LoginDetails loginDetails = new LoginDetails();
            List<LoginAuth> UserDetails = new List<LoginAuth>();
            string MessageId = string.Empty;
            DateTime ReqstDate = DateTime.Now;
            LoginAuth objLoginAuth = new LoginAuth();
            int value = 0;
            try
            {
                //using (SDSDBEntities contextManager = new SDSDBEntities())
                //{
                //    //ContextManager contextManager = new ContextManager();
                //    if (!string.IsNullOrEmpty(UserName))
                //    {
                //        var UserInfo = contextManager.usp_UserDetailsByUsername(UserName).FirstOrDefault();
                //        if (UserInfo != null)
                //        {
                //            objLoginAuth.DisplayName = UserInfo.StaffName;
                //            objLoginAuth.RoleId = (int)UserInfo.RoleId;
                //            objLoginAuth.OTP = value;
                //            objLoginAuth.DistributorId = UserInfo.DistributorID;
                //            objLoginAuth.JDEDistributorCode = UserInfo.JDEDistributorCode;
                //            objLoginAuth.DistributorName = UserInfo.DistributorName;
                //            objLoginAuth.IsDeliveryBoy = UserInfo.WhetherDeliveryBoy;
                //            objLoginAuth.IsGodownKeeper = UserInfo.IsGodownKeeper;
                //            objLoginAuth.GodownKeeperId = UserInfo.GodownKeeperId.ToString();
                //            objLoginAuth.StaffRefNo = UserInfo.StaffRefNo.ToString();
                //            objLoginAuth.RefNo = UserInfo.StaffRefNo.ToString();
                //            objLoginAuth.UserName = UserInfo.StaffName;
                //            objLoginAuth.UserAddress = UserInfo.StaffAddress;

                //            objLoginAuth.DistributorAddress = UserInfo.DistributorAddress;
                //            objLoginAuth.GSTNO = UserInfo.GSTNO;
                //            objLoginAuth.Email = UserInfo.Email;


                //            loginDetails.UserInfo = objLoginAuth;
                //            loginDetails.Status = BusinessCont.SuccessStatus;
                //            loginDetails.ExMsg = BusinessCont.ValidUser;
                //        }
                //        else
                //        {
                //            loginDetails.Status = BusinessCont.FailStatus;
                //            loginDetails.ExMsg = BusinessCont.MobNoNotFound;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                loginDetails.Status = BusinessCont.FailStatus;
                loginDetails.ExMsg = BusinessCont.SomethngWntWrng;
                throw ex;
            }
            return loginDetails;
        }
        #endregion

        /// <summary>
        /// SMSAlertBox method used for Send SMS using SMS Alert Box
        /// </summary>
        /// <param name="MobileNo"></param>
        /// <param name="FinalMessage"></param>
        /// <returns></returns>
        public string SMSAlertBox(string MobileNo, string FinalMessage)
        {
            try
            {
                string senderusername = "yoy-sanchitnew";
                string senderpassword = "909090";
                string senderid = "HPGASP";
                string sURL = "";
                sURL = "http://Vtsms.co.in:8080/bulksms/bulksms?username=" + senderusername + "&password=" + senderpassword + "&type=0&dlr=0&destination=91" + MobileNo + "&source=" + senderid + "&message=" + FinalMessage + "";
                string proxyURL = ConfigurationManager.AppSettings["proxyURL"];
                int proxyPort = Convert.ToInt32(ConfigurationManager.AppSettings["proxyPort"]);
                WebProxy proxy = new WebProxy(proxyURL, proxyPort);
                WebRequest request = WebRequest.Create(sURL);
                request.Method = "POST";
                //request.Proxy = proxy;
                string postData = FinalMessage;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader readerst = new StreamReader(dataStream);
                string responseFromServer = readerst.ReadToEnd();
                readerst.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Save last seen of user as active user
        /// </summary>
        /// <param name="DistId"></param>
        /// <param name="UserId"></param>
        /// <param name="UserTypeId"></param>
        /// <param name="MobileNo"></param>
        /// <param name="VersionNo"></param>
        /// <returns></returns>
        public string ActiveUser(ActiveUser model)
        {
            return ActiveUserPvt(model);
        }

        /// <summary>
        /// Save last seen of user as active user
        /// </summary>
        /// <param name="DistId"></param>
        /// <param name="UserId"></param>
        /// <param name="UserTypeId"></param>
        /// <param name="MobileNo"></param>
        /// <param name="VersionNo"></param>
        /// <returns></returns>
        private string ActiveUserPvt(ActiveUser model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetVal", typeof(int));
            string Result = BusinessCont.FailStatus;
            try
            {
                using (CFADBEntities contextManager = new CFADBEntities())
                {
                    RetValue = contextManager.usp_AddActiveUser(model.BranchId, model.CompanyId, model.UserId, model.EmpId, model.Username, model.MobileNo, model.RoleId, model.VersionNo, model.DeviceId, obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (RetValue > 0)
            {
                Result = BusinessCont.SuccessStatus;
            }
            return Result;
        }

        /// <summary>
        /// return current version
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        //public string GetCurrentVersion(string ApplicationName)
        //{
        //    return GetCurrentVersionPvt(ApplicationName);
        //}

        /// <summary>
        /// return current version
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        //private string GetCurrentVersionPvt(string ApplicationName)
        //{
        //    try
        //    {
        //        //return _contextManager.usp_GetVersionDetails(ApplicationName).Select(a => a.VersionNumber).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        /// <summary>
        /// return Mobile Logs
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        //public string GetMobileLogs()
        //{
        //    return GetMobileLogsPvt();
        //}

        /// <summary>
        /// return MobileLogs
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        //private string GetMobileLogsPvt()
        //{
        //    try
        //    {
        //        //return _contextManager.usp_GetAppConfiguration("SaveMobileLogs").Select(a => a.Value).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public void ExecuteTableParamedProcedure<T>(string storedProcedureName, string parameterName, string tableParamTypeName, IEnumerable<T> sprocParamObjects, SqlConnection connection = null)
        {
            //using (SDSDBEntities contextManager = new SDSDBEntities())
            //{
            //    using (SqlCommand command = connection.CreateCommand())
            //    {
            //        command.CommandText = storedProcedureName;
            //        command.CommandType = CommandType.StoredProcedure;

            //        SqlParameter parameter = command.Parameters.AddWithValue(parameterName, CreateDataTable(sprocParamObjects));
            //        parameter.SqlDbType = SqlDbType.Structured;
            //        parameter.TypeName = tableParamTypeName;

            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        private static DataTable CreateDataTable<T>(IEnumerable<T> sprocParamObjects)
        {
            DataTable table = new DataTable();

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var sprocParamObject in sprocParamObjects)
            {
                var propertyValues = new List<object>();
                foreach (PropertyInfo property in properties)
                {
                    propertyValues.Add(property.GetValue(sprocParamObject, null));
                }
                table.Rows.Add(propertyValues.ToArray());

                Console.WriteLine(table);
            }
            return table;
        }

        //public List<AppConfiguration> GetAppConfiguration(string Key)
        //{
        //    return GetAppConfigurationPvt(Key);
        //}

        //public List<AppConfiguration> GetAppConfigurationPvt(string Key)
        //{
        //    try
        //    {
        //        //return _contextManager.usp_GetAppConfiguration(Key).Select(a => new AppConfiguration
        //        //{
        //        //    Id = a.Id,
        //        //    Key = a.Key,
        //        //    Value = a.Value,
        //        //    Info = a.Info,
        //        //}).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        BusinessCont.SaveLog(0, 0, 0, 0, 0, "GetAppConfigurationPvt", "Key= " + Key, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Save log from mobile app
        /// </summary>
        /// <param name="LogID"></param>
        /// <param name="ServiceId"></param>
        /// <param name="DistributorId"></param>
        /// <param name="GodownkeeperId"></param>
        /// <param name="DeliveryBoyId"></param>
        /// <param name="LogFor"></param>
        /// <param name="LogData"></param>
        /// <param name="LogStatus"></param>
        /// <param name="LogDateTime"></param>
        /// <param name="LogException"></param>
        /// <returns></returns>
        public SaveLogModel SaveLog(string LogDtls)
        {
            return SaveLogPvt(LogDtls);
        }
        private SaveLogModel SaveLogPvt(string LogDtls)
        {
            SaveLogModel saveLogModel = new SaveLogModel();
            saveLogModel.Status = BusinessCont.FailStatus;
            try
            {
                string SaveLog = ConfigurationManager.AppSettings["SaveLog"];
                if (SaveLog == "Yes")
                {
                    //using (SDSDBEntities contextManager = new SDSDBEntities())
                    //{
                    //    var LogData = JsonConvert.DeserializeObject<List<LogDetails>>(LogDtls);
                    //    foreach (var item in LogData)
                    //    {
                    //        DateTime LogDT = BusinessCont.StrConvertIntoDatetime(item.LogDT);
                    //        contextManager.usp_AddEditAuditLog(item.LogID, item.ServiceId, item.DistId, item.GId, item.DId, item.LFor, item.LData, item.LStatus, LogDT, item.LEx).FirstOrDefault();
                    //    }
                    //}
                }
                saveLogModel.Status = BusinessCont.SuccessStatus;
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SaveLog", null, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
                saveLogModel.ExMsg = BusinessCont.ExceptionMsg(ex);
                saveLogModel.Status = BusinessCont.FailStatus;
            }
            return saveLogModel;
        }

        public int AddApiconfigurationMaster(AppConfiguration _aMM)
        {
            return AddApiconfigurationMasterPvt(_aMM);
        }

        private int AddApiconfigurationMasterPvt(AppConfiguration _aMM)
        {
            using (_contextManager)
            {
                ObjectParameter _objectParameter = null;
                _objectParameter = new ObjectParameter("RtnValue", typeof(int));
                int PkId = 0;
                try
                {
                    //_contextManager.usp_AddEditApiconfigurationDetails(_aMM.Id, _aMM.Key, _aMM.Value, _aMM.Info);
                    //if (_objectParameter != null && _objectParameter.Value != DBNull.Value)
                    //    PkId = Convert.ToInt32(_objectParameter.Value);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return PkId;
            }
        }

        #region Create Issue Tracker Login Session

        public string CreateIssueTrackerLoginSession(IssueTrackerLogin model)
        {
            return CreateIssueTrackerLoginSessionPvt(model);
        }

        private string CreateIssueTrackerLoginSessionPvt(IssueTrackerLogin model)
        {
            using (_contextManager)
            {
                ObjectParameter objectParameter = null;
                string ProfileId = "";
                try
                {


                    objectParameter = new ObjectParameter("ResultId", typeof(string));
                    //_contextManager.usp_InsertIssueTrackerLogin(model.DealerCode, model.ProfileId, objectParameter);

                    if (objectParameter != null && objectParameter.Value != DBNull.Value)
                        ProfileId = Convert.ToString(objectParameter.Value);


                }
                catch (Exception)
                {
                    throw;
                }
                return ProfileId;
            }
        }
        #endregion

        #region Get Employee Details
        public EmployeeDtls EmpCompDtls(int EmpId)
        {
            return EmpCompDtlsPvt(EmpId);
        }

        private EmployeeDtls EmpCompDtlsPvt(int EmpId)
        {
            using (_contextManager)
            {
                try
                {
                    return _contextManager.usp_EmployeeCompanyDetails(EmpId).Select(a => new EmployeeDtls
                    {
                        EmpId = Convert.ToInt32(a.EmpId),
                        CompanyId = Convert.ToInt32(a.CompanyId),
                        CompanyName = a.CompanyName,
                        BranchCode = a.BranchCode,
                        BranchName = a.BranchName,
                        CityCode = a.CityCode,
                        CityName = a.CityName
                    }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region ChangePassword
        public string ChangePassword(UserModel model)
        {
            return ChangePasswordPvt(model);
        }
        private string ChangePasswordPvt(UserModel model)
        {
            try
            {

                ObjectParameter obj = new ObjectParameter("Rtnval", typeof(int));
                _contextManager.usp_ChangePassword(model.unm, model.EmpId, model.upwd, model.Encryptpwd, model.AddedBy, obj);
                if (obj != null)
                    return obj.Value.ToString();
                else
                    return "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region ForgotPassword
        public string ForgotPassword(string EmailId)
        {
            return ForgotPasswordPvt(EmailId);
        }
        private string ForgotPasswordPvt(string EmailId)
        {
            try
            {
                ObjectParameter obj = null;
                obj = new ObjectParameter("RetValue", typeof(int));
                _contextManager.usp_CheckUser(EmailId, obj);
                if (obj != null)
                    return obj.Value.ToString();
                else
                    return "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Reset Password 
        public int ChangeforgotPassword(ResetPasswordModel model)
        {
            return ChangeforgotPasswordPvt(model);
        }
        private int ChangeforgotPasswordPvt(ResetPasswordModel model)
        {
            try
            {
                int UpdatePasswordValue = 0;
                if (model != null)
                {
                    //Encrypt Current and New Password to be checked in SP
                    model.CurrentEncryptPassword = model.Password;
                    model.Password = BusinessCont.DecryptM(model.Password);
                    ObjectParameter RtnValue = new ObjectParameter("Rtnval", typeof(int));
                    //_contextManager.usp_ChangePassword(model.Username, model.UserId,
                    //                                        model.Password,
                    //                                        model.CurrentEncryptPassword,
                    //                                        model.ResetID, "Y", RtnValue);
                    if (RtnValue.Value != DBNull.Value)
                    {
                        UpdatePasswordValue = Convert.ToInt32(RtnValue.Value);
                    }
                }
                return UpdatePasswordValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int SaveResetPasswordLog(EmailSend emailSend, int Flag)
        {
            return SaveResetPasswordLogPvt(emailSend, Flag);
        }
        private int SaveResetPasswordLogPvt(EmailSend emailSend, int Flag)
        {
            int ResetId = 0;
            ObjectParameter objParam = null;
            if (emailSend != null)
            {
                objParam = new ObjectParameter("ResetId", typeof(int));
                //_contextManager.usp_SDS_AddResetPasswordDetails(emailSend.UserName, emailSend.DistributorId, Flag, objParam);
                if (objParam.Value != DBNull.Value)
                {
                    ResetId = Convert.ToInt32(objParam.Value);
                }
            }
            return ResetId;
        }

        //public ResetPasswordModel GetResetPasswordLog(int ResetId)
        //{
        //    return GetResetPasswordLogPvt(ResetId);
        //}
        //private ResetPasswordModel GetResetPasswordLogPvt(int ResetId)
        //{
        //    //return _contextManager.usp_GetResetPasswordDetails(ResetId).Select(x => new ResetPasswordModel()
        //    //{
        //    //    DistributorId = Convert.ToInt32(x.DistributorId),
        //    //    DistributorCode = x.DistributorCode,
        //    //    ResetID = x.ResetID,
        //    //    ResetStatus = x.ResetStatus
        //    //}).FirstOrDefault();
        //}
        #endregion

        public void TestExceptionFilter()
        {
            {
                try
                {
                    int a = 0;
                    int b = 10;
                    int c = b / a;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Firebase push Notification For Customer Surver

        /// <summary>
        /// Method for send notification to android user
        /// </summary>
        /// <param name="DistributorId">Distributor Id for send notification</param>
        /// <param name="StaffRefNo">StaffRefNo for send notification</param>
        /// <param name="Title">Notification title</param>
        /// <param name="Message">Notification message</param>
        /// <returns></returns>
        public void SendNotification(int BranchId, int UserId, string Title, string Message, string PayloadStr)
        {
            try
            {
                var DeviceDetails = _contextManager.usp_GetUserDeviceDetails(BranchId, UserId).FirstOrDefault();
                if (DeviceDetails != null)
                {
                    AndroidNotification(DeviceDetails.DeviceId, Title, Message, PayloadStr);
                }

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "Send Notification", "User Id= " + UserId + ", Title=" + Title + ", Message=" + Message, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
        }

        /// <summary>
        /// Private method for send notification to android user
        /// </summary>
        /// <param name="Token">send notification to Mobile token using FCM</param>
        /// <param name="NotiTitle">Notification title</param>
        /// <param name="NotificationMsg">Notification message</param>
        /// <returns></returns>
        private string AndroidNotification(string Token, string NotiTitle, string NotificationMsg, string PayloadStr)
        {
            string ResponseStr = string.Empty;
            try
            {
                string ApplicationKey = "AAAAVz-avoI:APA91bEWw0hh659cece4v1kyYwmSQlnzIwofn5roHdl3DQT21srSspJwQfaqhy8vscmKnDfKj_8xeMn5P2I08H2CioFUFUOFbf-GadaN2sF99jlKmZYIXb9jNkM2dWANLU1-agTB9spC";

                string senderId = "374729260674"; // senderId is sender key of web project in fcm

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send") as HttpWebRequest;

                tRequest.Method = "Post";
                tRequest.ContentType = "application/json";
                tRequest.UseDefaultCredentials = true;
                tRequest.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                var data = new
                {
                    to = Token,
                    notification = new
                    {
                        title = NotiTitle,
                        body = NotificationMsg
                    },
                    data = new
                    {
                        payload = PayloadStr
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", ApplicationKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (HttpWebResponse tResponse = tRequest.GetResponse() as HttpWebResponse)
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                ResponseStr = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ResponseStr;
        }

        #endregion

        /// <summary>
        /// Send test SMS
        /// </summary>
        /// <returns></returns>
        public string SendSMS(UserModel user)
        {
            return SendSMSPvt(user);
        }

        /// <summary>
        /// Send test SMS
        /// </summary>
        /// <returns></returns>
        private string SendSMSPvt(UserModel user)
        {
            try
            {
                return SMSAlertBox(user.MobileNo, user.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region User Permissions
        public UserPermissions UserPermissions(int RoleId)
        {
            return UserPermissionsPvt(RoleId);
        }

        private UserPermissions UserPermissionsPvt(int RoleId)
        {
            UserPermissions result = new UserPermissions();
            try
            {
                var r = _contextManager.usp_GetMenuListRoleWise(RoleId).SingleOrDefault();
                if (r != null)
                {
                    result = new UserPermissions
                    {
                        RoleId = Convert.ToInt32(r.RoleId),
                        Dashboard = Convert.ToInt32(r.Dashboard),
                        EmployeeMaster = Convert.ToInt32(r.EmployeeMaster),
                        StockistMaster = Convert.ToInt32(r.StockistMaster),
                        CartingMaster = Convert.ToInt32(r.CartingMaster),
                        CourierMaster = Convert.ToInt32(r.CourierMaster),
                        BranchMaster = Convert.ToInt32(r.BranchMaster),
                        CompanyMaster = Convert.ToInt32(r.CompanyMaster),
                        StockistTransporterMapping = Convert.ToInt32(r.StockistTransporterMapping),
                        TransporterMaster = Convert.ToInt32(r.TransporterMaster),
                        StockistBranchRelation = Convert.ToInt32(r.StockistBranchRelation),
                        StockistCompanyRelation = Convert.ToInt32(r.StockistCompanyRelation),
                        GeneralMaster = Convert.ToInt32(r.GeneralMaster),
                        EmailConfig = Convert.ToInt32(r.EmailConfig),
                        PicklistOperation = Convert.ToInt32(r.PicklistOperation),
                        PicklistAdd = Convert.ToInt32(r.PicklistAdd),
                        PicklistVerify = Convert.ToInt32(r.PicklistVerify),
                        PicklistAllot = Convert.ToInt32(r.PicklistAllot),
                        ReAllotPicklist = Convert.ToInt32(r.ReAllotPicklist),
                        ImportInvData = Convert.ToInt32(r.ImportInvData),
                        InvCancelList = Convert.ToInt32(r.InvCancelList),
                        ReadyToDispatch = Convert.ToInt32(r.ReadyToDispatch),
                        AssignTransportMode = Convert.ToInt32(r.AssignTransportMode),
                        PrintSticker = Convert.ToInt32(r.PrintSticker),
                        ImportLRData = Convert.ToInt32(r.ImportLRData),
                        ChqRegister = Convert.ToInt32(r.ChqRegister),
                        ImportOS = Convert.ToInt32(r.ImportOS),
                        ImportChqDeposit = Convert.ToInt32(r.ImportChqDeposit),
                        ChqSummRpt = Convert.ToInt32(r.ChqSummRpt),
                        ChqSummMonthlyRpt = Convert.ToInt32(r.ChqSummMonthlyRpt),
                        ResolveConcernPL = Convert.ToInt32(r.ResolveConcernPL),
                        ResolveConcernINV = Convert.ToInt32(r.ResolveConcernINV),
                        PriotiseINV = Convert.ToInt32(r.PriotiseINV),
                        AssignTransportModeEdit = Convert.ToInt32(r.AssignTransportModeEdit),
                        AppConfig = Convert.ToInt32(r.AppConfig),
                        ImportTransitReport = Convert.ToInt32(r.ImportTransitReport),
                        ApproveVehicleIssue = Convert.ToInt32(r.ApproveVehicleIssue),
                        InsuranceClaim = Convert.ToInt32(r.InsuranceClaim),
                        ApprovalClaim = Convert.ToInt32(r.ApprovalClaim),
                        LRReceivedList = Convert.ToInt32(r.LRReceivedList),
                        ResolveClaimConcern = Convert.ToInt32(r.ResolveClaimConcern),
                        ImportSRS = Convert.ToInt32(r.ImportSRS),
                        CorrectionRequiredList = Convert.ToInt32(r.CorrectionRequiredList),
                        SRSPendingCNList = Convert.ToInt32(r.SRSPendingCNList),
                        ImportCreditNote = Convert.ToInt32(r.ImportCreditNote),
                        UploadDestructionCertificate = Convert.ToInt32(r.UploadDestructionCertificate),
                        DestructionCertificateList = Convert.ToInt32(r.DestructionCertificateList),
                        AppConfiguration = Convert.ToInt32(r.AppConfiguration),
                        StockTransferAdd = Convert.ToInt32(r.StockTransferAdd),
                        CityMaster = Convert.ToInt32(r.CityMaster),
                        ThresholdValueMaster = Convert.ToInt32(r.ThresholdValueMaster),
                        ChecklistMaster = Convert.ToInt32(r.ChecklistMaster),
                        OtherCNFMaster = Convert.ToInt32(r.OtherCNFMaster),
                        ImportDepositedCheque = Convert.ToInt32(r.ImportDepositedCheque),
                        ChequeRegisterSummaryReport = Convert.ToInt32(r.ChequeRegisterSummaryReport),
                        RaisedConcernList = Convert.ToInt32(r.RaisedConcernList),
                        VersionDetails = Convert.ToInt32(r.VersionDetails),
                        BranchCompanyRelationMaster = Convert.ToInt32(r.BranchCompanyRelationMaster),
                        DashBoardOrderReturn = Convert.ToInt32(r.DashBoardOrderReturn),
                        DashBoardOrderDispatch = Convert.ToInt32(r.DashBoardOrderDispatch),
                        DashBoradChequeAcc = Convert.ToInt32(r.DashBoradChequeAcc),
                        DashBoardInventoyInward = Convert.ToInt32(r.DashBoardInventoyInward),
                        DashBoradStockTrans = Convert.ToInt32(r.DashBoradStockTrans),
                        DashMiniORPendingSRS = Convert.ToInt32(r.DashMiniORPendingSRS),
                        DashORFOROperator = Convert.ToInt32(r.DashORFOROperator),
                        DashCheackAccForOprator = Convert.ToInt32(r.DashCheackAccForOprator),
                        DashOrdDisForOperator = Convert.ToInt32(r.DashOrdDisForOperator),
                        DashOrdDisForSupervisor = Convert.ToInt32(r.DashOrdDisForSupervisor),
                        DashBoardORForSupervisor = Convert.ToInt32(r.DashBoardORForSupervisor),
                        DshBranchDRP = Convert.ToInt32(r.DshBranchDRP),
                        DshCompanyDRP = Convert.ToInt32(r.DshCompanyDRP),
                        ExpenseRegister = Convert.ToInt32(r.ExpenseRegister),
                        ReimbursmentInvoice = Convert.ToInt32(r.ReimbursmentInvoice),
                        ComissionInvoice = Convert.ToInt32(r.ComissionInvoice),
                        GatepassbillSummary = Convert.ToInt32(r.GatepassbillSummary),
                        CheckInvoice = Convert.ToInt32(r.CheckInvoice),
                        VendorMaster = Convert.ToInt32(r.VendorMaster),
                        TaxMaster = Convert.ToInt32(r.TaxMaster),
                        HeadMaster = Convert.ToInt32(r.HeadMaster),
                        TransportParentMaster = Convert.ToInt32(r.TransportParentMaster),
                        TransportParentMapping = Convert.ToInt32(r.TransportParentMapping),
                        CourierParentMaster = Convert.ToInt32(r.CourierParentMaster),
                        CourierParentMapping = Convert.ToInt32(r.CourierParentMapping),
                        CompanyVendorMapping = Convert.ToInt32(r.CompanyVendorMapping),
                        ChequeStatusReport = Convert.ToInt32(r.ChequeStatusReport),
                        VendorBranchMapping = Convert.ToInt32(r.VendorBranchMapping),
                        VehicleChecklistForImg = Convert.ToInt32(r.VehicleChecklistForImg),
                        VerifyStockistData = Convert.ToInt32(r.VerifyStockistData),
                        ImportDynamically = Convert.ToInt32(r.ImportDynamically),
                        OCRImportProduct = Convert.ToInt32(r.OCRImportProduct),
                        OCRIntegration = Convert.ToInt32(r.OCRIntegration),
                        OCRSummaryReport = Convert.ToInt32(r.OCRSummaryReport),
                        OCRDetailReport = Convert.ToInt32(r.OCRDetailReport),
                        TransporterReport = Convert.ToInt32(r.TransporterReport),
                        ImportSRSList = Convert.ToInt32(r.ImportSRSList),
                        PrinterDetailsMaster = Convert.ToInt32(r.PrinterDetailsMaster),
                        PrinterHistoryReport = Convert.ToInt32(r.PrinterHistoryReport),
                        PrintGatepass = Convert.ToInt32(r.PrintGatepass),
                        DeleteInvoiceDetails=Convert.ToInt32(r.DeleteInvoiceDetails)
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "UserPermissionsPvt", "User Permissions Pvt", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return result;
        }
        #endregion

    }
}
