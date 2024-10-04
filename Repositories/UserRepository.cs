using CNF.Business.BusinessConstant;
using CNF.Business.Model.Context;
using CNF.Business.Model.Login;
using CNF.Business.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories
{
    public class UserRepository : IUserRepository
    {
        private CFADBEntities _contextManager;
        //Inject DB Context instance from Unit of Work to avoid unwanted memory allocations.
        public UserRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }

        #region Create Refresh Token Record in Refresh Token Table        
        /// <summary>
        /// Create Refresh Token Record in Refresh Token Table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long CreateAfetrDeletingRefreshToken(RefreshToken model)
        {
            model.Action = BusinessCont.ADDRecord;
            model.LastUpdateDate = DateTime.Now;
            return GetAddUpdateClearRefreshTokenPvt(model).AddUpdateResult;
        }

        #endregion

        #region Get record from refresh token table by user id, to proceed further with refresh token flow
        /// <summary>
        /// If Refresh token table row by userId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public RefreshToken GetRefreshTokenByUserId(long UserId)
        {
            RefreshToken model = new RefreshToken();
            model.UserId = UserId;
            model.Action = BusinessCont.GETRecord;
            model.CreatedDate = DateTime.Now;
            model.LastUpdateDate = DateTime.Now;
            model.ExpiryTime = DateTime.Now;
            return GetAddUpdateClearRefreshTokenPvt(model);
        }
        #endregion

        #region Common Private Function for Add, Delete, Get Refresh Token
        /// <summary>
        /// Update Refresh Token table with new values,
        ///  so new token will be updated in table which will be used to continue user authenticated session.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>      
        //Common Private Function for Add, Delete, Get Refresh Token
        private RefreshToken GetAddUpdateClearRefreshTokenPvt(RefreshToken model)
        {
            RefreshToken result = new RefreshToken();
            try
            {
                ObjectParameter _ProcOutValue = new ObjectParameter("RtnValue", typeof(int));
                result = _contextManager.usp_RefreshTokenAddEdit(model.ClientKey,
                                                    model.RefreshValue,
                                                    model.CreatedDate,
                                                    model.UserId,
                                                    model.LastUpdateDate,
                                                    model.ExpiryTime,
                                                    model.Action)
                                                    .Select(x => new RefreshToken()
                                                    {
                                                        ClientKey = x.ClientKey,
                                                        RefreshValue = x.RefreshValue,
                                                        CreatedDate = x.CreatedDate,
                                                        UserId = x.UserId,
                                                        LastUpdateDate = x.LastUpdateDate,
                                                        ExpiryTime = x.ExpiryTime,
                                                        RtnValue = x.RtnValue
                                                    }).FirstOrDefault();

            }
            catch (Exception)
            {
            }

            return result;
        }
        #endregion

        #region Get User Details if authenticated
        /// <summary>
        /// Get User Details
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public LoginDetails GetLoginDetails(string UserName, string Password, int company, int Branch, int role)
        {
            try
            {
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                {
                    string Status = string.Empty, UserFlag = string.Empty;

                    UserFlag = BusinessCont.UserByUserNamePwd;

                    if (!string.IsNullOrEmpty(UserFlag))
                    {
                        return GetLoginDetailsByUserNameIdPwdPvt(UserName, Password, 0, UserFlag, company, Branch, role);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetLoginDetails", "UserName- " + UserName + ", Password- " + Password, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
                throw ex;
            }
        }

        //private LoginDetails LoginDetailsPvt(string UserName, string Password)
        //{
        //    LoginDetails loginDetails = new LoginDetails();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
        //        {
        //            string EncrPassword = BusinessCont.EncryptM(Password);

        //                loginDetails.UserInfo = _contextManager.usp_UserLoginCheck(UserName, EncrPassword,0, BusinessCont.UserByUserNamePwd).Select(a => new LoginAuth
        //                {
        //                    RoleId = a.RoleId,
        //                    RefNo = a.RefNo,
        //                    RoleName = a.RoleName,
        //                    UserName = a.UserName,
        //                    ActiveStatus = a.Status,
        //                    UserId = a.UserId,
        //                    DisplayName = a.DisplayName,
        //                    LastUpdatedDate = a.LastUpdatedDate?.ToString(BusinessCont.DateFormat) ?? null,
        //                }).FirstOrDefault();                   
        //            loginDetails.Status = BusinessCont.SuccessStatus;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loginDetails.Status = BusinessCont.FailStatus;
        //        loginDetails.ExMsg = BusinessCont.SomethngWntWrng;
        //        //throw ex;
        //    }
        //    return loginDetails;
        //}
        #endregion

        #region GetLoginDetailsByUserNameId
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public LoginDetails GetLoginDetailsByUserNameUId(string UserName, int UserId, int company, int Branch, int role)
        {
            if (!string.IsNullOrEmpty(UserName) && UserId > 0)
            {
                return GetLoginDetailsByUserNameIdPwdPvt(UserName, "", UserId, BusinessCont.UserByUserNameUID, company, Branch, role);
            }
            return null;
        }


        private LoginDetails GetLoginDetailsByUserNameIdPwdPvt(string UserName, string Password, int UserId, string BusinessConstant, int company, int Branch, int role)
        {
            LoginDetails loginDetails = new LoginDetails();
            loginDetails.UserInfo = new LoginAuth();

            try
            {
                loginDetails.UserInfo = _contextManager.usp_UserLoginCheck(UserName, Password, company, Branch, role).Select(a => new LoginAuth
                {
                    UserId = a.UserId,
                    RoleId = Convert.ToInt16(a.RoleId),
                    UserName = a.UserName,
                    Password = a.Password,
                    EncryptPassword = a.EncryptPassword,
                    EmpId = a.EmpId,
                    DisplayName = a.DisplayName,
                    IsActive = a.IsActive,
                    BranchId = a.BranchId,
                    BranchCode = a.BranchCode,
                    BranchName = a.BranchName,
                    City = a.City,
                    RoleName = a.RoleName,
                    EmpNo = a.EmpNo,
                    EmpName = a.EmpName,
                    EmpMobNo = a.EmpMobNo,
                    CompanyId = Convert.ToInt32(a.CompanyId),
                    CompanyCode = a.CompanyCode,
                    CompanyName = a.CompanyName,
                    CompCityName = a.CompCityName,
                    BranchCity = a.BranchCity,
                    StateCode = a.StateCode
                }).FirstOrDefault();
                loginDetails.Status = BusinessCont.SuccessStatus;
            }
            catch (Exception ex)
            {
                loginDetails.Status = BusinessCont.FailStatus;
                loginDetails.ExMsg = BusinessCont.SomethngWntWrng;
                //throw ex;
            }
            return loginDetails;
        }

        public string OfficerValidate(string UserId, string Password)
        {
            string Status = string.Empty;
            try
            {
                string DecrptPassword = BusinessCont.DecryptM(Password);

                string GeneratePassword = string.Empty, AppId = "SDS";
                string Date = DateTime.Now.ToString("dd");
                string Month = DateTime.Now.ToString("MM");
                string Year = DateTime.Now.ToString("yy");
                GeneratePassword = "WsHP*" + Date[0] + Month[0] + Year + Month[1] + Date[1];
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "OfficerValidate", "UserId- " + UserId + ", Password" + Password, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
                throw ex;
            }
            return Status;
        }
        #endregion

        #region Update Feedback System 
        /// <summary>
        /// Update Feedback System 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public long UpdateFeedbackSystemStatus(UpdateFeedbackSystem updateFeedback)
        {
            return UpdateFeedbackSystemStatusPvt(updateFeedback);
        }

        /// <summary>
        /// Update Feedback System 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private long UpdateFeedbackSystemStatusPvt(UpdateFeedbackSystem updateFeedback)
        {
            ObjectParameter result = new ObjectParameter("Result", typeof(long));
            long Id = 0;

            try
            {

                //_contextManager.usp_UpdateFeedbackSystemStatus(updateFeedback.DistributorId, updateFeedback.IsFeedbackSystem, updateFeedback.UpdatedBy, result);
                if (result != null)
                {
                    Id = Convert.ToInt64(result.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Id;
        }
        #endregion

        #region Get User By Role
        public List<EmployeeDtls> GetUserByRole(int BranchId, int CompId, int RoleId)
        {
            return GetUserByRolePvt(BranchId, CompId, RoleId);
        }
        private List<EmployeeDtls> GetUserByRolePvt(int BranchId, int CompId, int RoleId)
        {
            List<EmployeeDtls> UserList = new List<EmployeeDtls>();
            try
            {
                UserList = _contextManager.usp_UserListByRole(BranchId, CompId, RoleId).Select(x => new EmployeeDtls
                {
                   EmpId = Convert.ToInt32(x.EmpId),
                   BranchId = Convert.ToInt32(x.BranchId),
                   EmpNo = x.EmpNo,
                   EmpName = x.EmpName,
                   EmpEmail = x.EmpEmail,
                   UserId = x.UserId,
                   RoleId = Convert.ToInt32(x.RoleId),
                   EmpMobNo = x.EmpMobNo
                }).ToList();
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, RoleId, "GetUserByRolePvt", "Get User By Role", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return UserList;
        }
        #endregion

    }
}
