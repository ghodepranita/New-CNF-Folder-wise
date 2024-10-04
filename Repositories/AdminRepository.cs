using CNF.Business.BusinessConstant;
using CNF.Business.Model.Context;
using CNF.Business.Model.User;
using CNF.Business.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net.Mail;
using System.Web.Configuration;

namespace CNF.Business.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private CFADBEntities _contextManager;
        //Inject DB Context instance from Unit of Work to avoid unwanted memory allocations.
        public AdminRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }


        //creating Login For the Selected Users(Distributors)
        public long CreateLogin(List<UserModelForActivation> _distributorModel)
        {
            return CreateDistributorLoginPvt(_distributorModel);
        }

        private long CreateDistributorLoginPvt(List<UserModelForActivation> _dM)
        {
            using (_contextManager)
            {
                long LoginId = 0;
                long LoginCount = 0;
                try
                {
                    ObjectParameter _objectParameter = null;
                    _objectParameter = new ObjectParameter("RtnResult", typeof(long));

                    //string Password = BusinessCont.GetAppConfiguration(BusinessCont.AppConfigPassword);
                    //string EncryptedPassword = BusinessCont.EncryptM(Password);
                    for (int i = 0; i < _dM.Count(); i++)
                    {
                        string keyData = _dM[i].Action == "DELETE" || _dM[i].RoleId == 7 ? null : "1,2,3,4";
                        string valueData =  null;
                        //_contextManager.usp_CreateLogin(_dM[i].UserName, Password, EncryptedPassword, _dM[i].RoleId, _dM[i].RefNo, _dM[i].DisplayName, _dM[i].Status, keyData, valueData, _dM[i].Action, _objectParameter);
                        // Sending mail to distributor
                        //string status = sendEmailViaWebApi(_dM[i]);
                        if (_objectParameter != null && _objectParameter.Value != DBNull.Value)
                        {
                            LoginId = Convert.ToInt64(_objectParameter.Value);
                            if (LoginId > 0)
                            {
                                LoginCount = LoginCount + 1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return LoginCount;
            }
        }

        public long CreateLoginForSDSIssueTracker(List<UserModelForActivation> _distributorModel)
        {
            return CreateLoginForSDSIssueTrackerPvt(_distributorModel);
        }

        private long CreateLoginForSDSIssueTrackerPvt(List<UserModelForActivation> _dM)
        {
            using (_contextManager)
            {            
                long LoginCount = 0;
                try
                {            
                    for (int i = 0; i < _dM.Count(); i++)
                    {
                        //var list = _contextManager.usp_GetDistributorInformation(_dM[i].RefNo.ToString()).Select(x => new DistributorModel
                        //{
                        //    DistributorId = Convert.ToInt32(x.DistributorId),
                        //    DistributorName = x.DistributorName,
                        //    JDEDistributorCode = x.JDEDistributorCode,
                        //    Email = x.Email,
                        //    SACode = x.SACode

                        //}).FirstOrDefault();
                
                          //int result = _contextManager.usp_CreateLoginforSDSIssueTracker(list.DistributorId.ToString(), list.JDEDistributorCode,
                          //      list.DistributorName, Int16.Parse(list.SACode), list.Email);

                          //LoginCount = LoginCount + result;                                            
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return LoginCount;
            }
        }
        private string sendEmailViaWebApi(DistributorModel dm)
        {
            string userName = WebConfigurationManager.AppSettings["PFUserName"];
            string password = WebConfigurationManager.AppSettings["PFPassWord"];

            string WebUrl = WebConfigurationManager.AppSettings["WebUrl"];
            string Url = WebUrl + "#/Activate/" + dm.DistributorName;
            string LoginUrl = WebUrl + "#/auth/login";
            string defaultPassword = "lpg@123";
            //string TinyUrl = ShortURL(Url);

            string MappedFilePath = "";
            string Subject = "";
            string Date = DateTime.Today.Date.ToString("dd-MM-yyyy");
            string message = "";
            // Replace Notification Table
            if (dm.Action.ToUpper() == "DELETE")
            {
                MappedFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~\\Mailfile\\DeactivateEmail.html");
                message = System.IO.File.OpenText(MappedFilePath).ReadToEnd().ToString();
                message = message.Replace("<!--SAName-->", dm.SAName.ToUpper());
                Subject = " Account Deactivated  " + Date + " ";
            }
            else
            {
                MappedFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~\\Mailfile\\ActivateEmail.html");
                message = System.IO.File.OpenText(MappedFilePath).ReadToEnd().ToString();
                message = message.Replace("<!--SchedulerTableString-->", Url);
                message = message.Replace("<!--SentName-->", dm.JDEDistributorCode);
                message = message.Replace("<!--SentPassword-->", defaultPassword);
                message = message.Replace("<!--LoginUrl-->", LoginUrl);
                message = message.Replace("<!--SAName-->", dm.SAName.ToUpper());
                Subject = " Account Activation " + Date + " ";
            }
            string FromMail = userName;
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(FromMail);
            mail.To.Add(dm.Email);
            mail.Subject = Subject;
            mail.Body = message;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(userName, password);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            return "Success";
        }

        public int QueryBuilder(string Query)
        {
            return QueryBuilderPvt(Query);
        }

        private int QueryBuilderPvt(string Query)
        {
            int Result = 0;
            try
            {
                //using (_contextManager = new SDSDBEntities())
                //{
                //    _contextManager.Database.CommandTimeout = 2000;
                //    Result = _contextManager.Database.ExecuteSqlCommand(Query.Trim()); 
                //}
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 28, 0, "QueryBuilder", Query, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
                throw ex;
            }
            return Result;
        }

        //public List<DistributorConsumerModel> GetDistributorConsumerList()
        //{
        //    return GetDistributorConsumerListPvt();
        //}

        //private List<DistributorConsumerModel> GetDistributorConsumerListPvt()
        //{
        //    try
        //    {
        //        return _contextManager.usp_AdmDistributorConsumerdata().Select(x => new DistributorConsumerModel
        //        {
        //            DistributorName = x.DistributorName,
        //            DistributorId = x.DistributorId,
        //            ConsumerHolding = x.ConsumerHolding,
        //            AvgDelivery = x.AvgDelivery,
        //            MI_Nos = x.MI_Nos,
        //            MI_Percernt = x.MI_Percernt,
        //            EZYGAS_Nos = x.EZYGAS_Nos,
        //            EZYGas_Percernt = x.EZYGas_Percernt,
        //            SARVEKSHAN_Nos = x.SARVEKSHAN_Nos,
        //            Sarvekshan_Percernt = x.Sarvekshan_Percernt,
        //            Total_Nos = x.Total_Nos,
        //            Total_Percernt = x.Total_Percernt,
        //            PendingForRevgeo = x.PendingForRevgeo,
        //            PendingForRevGeo_Percernt = x.PendingForRevGeo_Percernt,
        //            NoOfVehicles = x.NoOfVehicles,
        //            NoOfTrips = x.NoOfTrips
        //        }).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<DistributorClusterModel> GetDistributorClusterList(int DistributorId)
        //{
        //    return GetDistributorClusterListPvt(DistributorId);
        //}

        //private List<DistributorClusterModel> GetDistributorClusterListPvt(int DistributorId)
        //{
        //    try
        //    {
        //        return _contextManager.usp_AdmDistributorClusterdata(DistributorId).Select(x => new DistributorClusterModel
        //        {
        //            DistributorName = x.DistributorName,
        //            DistributorId = x.DistributorId,
        //            ClusterId = x.ClusterId,
        //            ClusterName = x.ClusterName,
        //            VehicleNo = x.VehicleNo,
        //            VehicleType = x.VehicleType,
        //            NumberOfWheels = x.NumberOfWheels,
        //            RTOCapacity = x.RTOCapacity,
        //            NoOfTrip = x.NoOfTrip,
        //            PossibleDel = x.PossibleDel
        //        }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}






