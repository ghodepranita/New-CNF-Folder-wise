using CNF.API.Filters;
using CNF.API.Models;
using CNF.Business.BusinessConstant;
using CNF.Business.Model.Login;
using CNF.Business.Model.Master;
using CNF.Business.Model.User;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using EmployeeDtls = CNF.Business.Model.Login.EmployeeDtls;

namespace CNF.API.Controllers
{
    public class LoginController : BaseApiController
    {
        [HttpPost]
        [Route("Login/CreateLogs")]
        public IHttpActionResult CreateLogs([FromBody]LoggerModel model)
        {
            LoggerModel _LoggerModel = new LoggerModel();
            _unitOfWork.GetLoggerInstance.LogException(model);
            return Ok();
        }

        [HttpPost]
        [OverrideAuthorization]
        [Route("Login/ChangePassword")]
        public IHttpActionResult ChangePassword([FromBody]UserModel model)
        {
            var result = _unitOfWork.loginRepository.ChangePassword(model);

            if (result != null)
                return Ok(result);

            return BadRequest("-1");
        }

        //forgot password reset
        [HttpPost]
        [OverrideAuthorization]
        [Route("Login/UpdateforgotPassword")]
        [AllowAnonymous]
        public IHttpActionResult UpdateforgotPassword([FromBody]ResetPasswordModel model)
        {
            int result = _unitOfWork.loginRepository.ChangeforgotPassword(model);

            if (result > 0)
                return Ok(result);

            return BadRequest("-1");
        }

        decimal LogId = 0;

        // Send OTP SMS for activate user/Login user first time only       
        [HttpPost]
        [OverrideAuthorization]
        [Route("Login/SendOTPSMS")]
        [AllowAnonymous]
        public IHttpActionResult SendOTPSMS([FromBody]UserModel user)
        {
            if (user == null)
                return BadRequest(BusinessCont.InvalidClientRqst);
            Task<LoginDetails> accessToken = null;
            LoginDetails loginDetails = null;
            try
            {
                loginDetails = new LoginDetails();
                loginDetails = _unitOfWork.loginRepository.SendOTPSMS(user);
                if (loginDetails.UserInfo != null)
                {
                    //loginDetails.UserInfo.Source = BusinessCont.MobSource;
                    if (loginDetails != null)
                    {
                        // Create & Return the access token which contains JWT and Refresh Token
                        accessToken = CreateAccessToken(loginDetails, "");
                        accessToken.Result.Status = BusinessCont.SuccessStatus;
                        return Ok(new { authToken = accessToken });
                    }
                }
                else
                {
                    accessToken = NULLObject();
                    accessToken.Result.Status = BusinessCont.FailStatus;
                    accessToken.Result.ExMsg = BusinessCont.UnauthorizedUser;
                }
            }
            catch (Exception ex)
            {
                loginDetails.Status = BusinessCont.FailStatus;
                loginDetails.ExMsg = BusinessCont.ExceptionMsg(ex);
                BusinessCont.SaveLog(LogId, 0, 0, "SendOTPSMS", "MobileNo= " + user.MobileNo, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
                //throw;
            }
            return Ok(new { authToken = accessToken });
        }

        private async Task<LoginDetails> NULLObject()
        {
            LoginDetails loginDetails = new LoginDetails();

            return new LoginDetails()
            {
                UserInfo = null,
                Token = "",
                expiration = DateTime.Now,
                refresh_token = ""
            };
        }
        [HttpGet]
        [OverrideAuthorization]
        [Route("Login/ADSLoginVerification/{Username}/{Password}")]
        [AllowAnonymous]
        public IHttpActionResult ADSLoginVerification(string UserName, string Password)
        {
            string result = null;
            try
            {
                result = _unitOfWork.UserRepository.OfficerValidate(UserName, Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(result);
        }

        #region Create New Token From Another Server 

        //[HttpPost]
        //[OverrideAuthorization]
        //[Route("Login/GenerateTokenByLogin")]
        //[AllowAnonymous]
        //public async Task<IHttpActionResult> GenerateTokenByLogin([FromBody]UserModel user)
        //{
        //    // We will return Generic 500 HTTP Server Status Error
        //    // If we receive an invalid payload
        //    if (user == null)
        //    {
        //        return InternalServerError(); // Error 500
        //    }

        //    switch (user.GrantType)
        //    {
        //        case "password":
        //            return await GenerateNewToken(user, user.UserDtls, BusinessCont.LoginFromSDS);
        //        case "refresh_token":
        //            return await RefreshToken(user, user.UserDtls, BusinessCont.LoginFromSDS);
        //        default:
        //            // not supported - return a HTTP 401 (Unauthorized)
        //            return Unauthorized();
        //    }
        //}


        #endregion

        #region New Refresh Token Based Authentication

        [HttpPost]
        [OverrideAuthorization]
        [Route("Login/LoginDetails2")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoginDetails2([FromBody]UserModel user)
        {
            try
            {
                switch (user.GrantType)
                {
                    case "password":
                        return await GenerateNewToken(user, null);
                    case "refresh_token":
                        return await RefreshToken(user, null);
                    default:
                        // not supported - return a HTTP 401 (Unauthorized)
                        return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "LoginDetails2", "UserName- " + user.Username + ", Password- " + user.Password, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
                throw ex;
            }
        }

        // Method to Create New JWT and Refresh Token
        private async Task<IHttpActionResult> GenerateNewToken(UserModel model, LoginAuth UserDtls)
        {
            // check if there's an user with the given username
            //await
            LoginDetails loginDetails = new LoginDetails();
            try
            {
                if (UserDtls == null)
                {
                    loginDetails = _unitOfWork.UserRepository.GetLoginDetails(model.Username, model.Password, model.CompanyId, model.BranchId, model.RoleId);
                }
                else
                {
                    loginDetails.UserInfo = UserDtls;
                    loginDetails.Status = BusinessCont.SuccessStatus;
                }

                // Validate credentials
                if (loginDetails.UserInfo != null)
                {
                    if (loginDetails.Status == "Success")
                    {
                        // username & password matches: create the refresh token
                        var newRtoken = CreateRefreshToken(WebConfigSettings.ClientIdSecret, loginDetails.UserInfo.UserId);

                        // Delete Previous if exists and Add new refresh token to Database
                        _unitOfWork.UserRepository.CreateAfetrDeletingRefreshToken(newRtoken);

                        // Create & Return the access token which contains JWT and Refresh Token
                        var accessToken = await CreateAccessToken(loginDetails, newRtoken.RefreshValue);
                        return Ok(new { authToken = accessToken });
                    }
                    else
                    {
                        // return Ok(new { status = "Deactivated" });
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GenerateNewToken", null, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
                throw ex;
            }
            return Unauthorized();
        }

        // Create access Tokenm
        private async Task<LoginDetails> CreateAccessToken(LoginDetails loginDetails, string refreshToken)
        {
            string UserName = "";
            int RoleId = 0;
            string DisplayName = String.IsNullOrEmpty(loginDetails.UserInfo.DisplayName) ? "" : loginDetails.UserInfo.DisplayName;
            double tokenExpiryTime = Convert.ToDouble(WebConfigSettings.WebTokenExpireTimeinHrs);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(WebConfigSettings.JwtSecretKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, UserName),
                        new Claim(ClaimTypes.Role,RoleId.ToString()),
                        new Claim("LoggedOn", DateTime.Now.ToString()),
                        new Claim("DisplayName", loginDetails.UserInfo.DisplayName!=null? loginDetails.UserInfo.DisplayName:""),
                        //new Claim("RefNo", loginDetails.UserInfo.RefNo!=null? loginDetails.UserInfo.RefNo:""),
                        new Claim("RoleId", RoleId.ToString())
                     }),

                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Issuer = WebConfigSettings.AppDomain,
                Audience = WebConfigSettings.AppDomain,
                Expires = DateTime.UtcNow.AddHours(tokenExpiryTime)
            };

            // Generate token
            var newtoken = tokenHandler.CreateToken(tokenDescriptor);
            var encodedToken = tokenHandler.WriteToken(newtoken);

            return new LoginDetails()
            {
                UserInfo = loginDetails.UserInfo,
                UserDetails = loginDetails.UserDetails,
                Token = encodedToken,
                expiration = newtoken.ValidTo,
                refresh_token = refreshToken
            };
        }
        private async Task<IHttpActionResult> MobileGenerateNewToken(LoginDetails loginDetails)
        {
            // Validate credentials
            if (loginDetails != null)
            {
                // username & password matches: create the refresh token
                var newRtoken = CreateRefreshToken(WebConfigSettings.ClientIdSecret, loginDetails.UserInfo.UserId);
                // Create & Return the access token which contains JWT and Refresh Token
                var accessToken = await CreateAccessToken(loginDetails, newRtoken.RefreshValue);
                return Ok(new { authToken = accessToken });
            }
            return Unauthorized();
        }


        private RefreshToken CreateRefreshToken(string clientId, long userId)
        {
            return new RefreshToken()
            {
                ClientKey = clientId,
                UserId = userId,
                RefreshValue = Guid.NewGuid().ToString("N"),
                CreatedDate = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddHours(WebConfigSettings.MobileTokenExpireTimeinHrs)
            };
        }

        // Method to Refresh JWT and Refresh Token
        private async Task<IHttpActionResult> RefreshToken(UserModel model, LoginAuth UserDtls)
        {
            try
            {
                LoginDetails userDetails = new LoginDetails();
                // check if the received refreshToken exists for the given clientId                
                if (UserDtls == null)
                {
                    userDetails = _unitOfWork.UserRepository.GetLoginDetails(model.Username, model.UserDtls.EncryptPassword, model.UserDtls.CompanyId, 
                        Convert.ToInt16(model.UserDtls.BranchId), model.UserDtls.RoleId);
                }
                else
                {
                    userDetails.UserInfo = UserDtls;
                    userDetails.Status = BusinessCont.SuccessStatus;
                }
                if (userDetails == null)
                {
                    //if user doesnt exists
                    return Unauthorized();
                }

                var resultUserRT = new RefreshToken();
                if (userDetails != null)
                {
                    //if user exists, check if refresh toen exists or not
                    resultUserRT = _unitOfWork.UserRepository.GetRefreshTokenByUserId(Convert.ToInt32(userDetails.UserInfo.UserId));
                }

                if (resultUserRT == null)
                {
                    // refresh token not found or invalid (or invalid clientId)
                    return Unauthorized();
                }

                // check if refresh token is expired
                if (resultUserRT.ExpiryTime < DateTime.UtcNow)
                {
                    return Unauthorized();
                }

                // generate a new refresh token 
                var rtNew = CreateRefreshToken(resultUserRT.ClientKey, resultUserRT.UserId);

                // Delete Previous if exists and Add new refresh token to Database
                _unitOfWork.UserRepository.CreateAfetrDeletingRefreshToken(rtNew);

                var response = await CreateAccessToken(userDetails, rtNew.RefreshValue);

                return Ok(new { authToken = response });

            }
            catch (Exception ex)
            {
                //throw ex;
                return Unauthorized();
            }
        }
        #endregion

        #region Test apis
        //Test       
        [Route("Login/AllowAnonymous")]
        [OverrideAuthorization]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult AllowAnonymous()
        {
            return Ok("AllowAnonymous");
        }

        [Route("Login/Authorize1")]
        [HttpGet]
        public IHttpActionResult Authorize1()
        {
            //throw new NullReferenceException();
            return Ok("Authorize1");
        }

        [Route("Login/Authorize2")]
        [HttpPost]
        public IHttpActionResult Authorize2(UserModel model)
        {
            _unitOfWork.loginRepository.TestExceptionFilter();
            //throw new NullReferenceException();
            return Ok("Authorize(Roles =2, 1");
        }

        [Route("Login/Authorize3")]
        [HttpPost]
        public IHttpActionResult Authorize3(UserModel model)
        {
            return Ok("Authorize3");
        }
        #endregion

        #region Create Login
        //creating Distributor Login
        [HttpPost]
        [Route("Login/CreateLogin")]
        public IHttpActionResult CreateLogin([FromBody]List<UserModelForActivation> _user)
        {
            if (_user == null)
                return BadRequest(BusinessCont.InvalidClientRqst);
            DistributorDetails _details = null;
            try
            {
                _details = new DistributorDetails();
                _details.LoginId = _unitOfWork.adminRepository.CreateLogin(_user);
            }
            catch (Exception ex)
            {
                //_details.Status = BusinessCont.FailStatus;
                //_details.ExMsg = BusinessCont.ExceptionMsg(ex);
            }
            return Ok(_details);
        }


        #endregion

        //to get the UserDetails by UserName
        [HttpPost]
        [OverrideAuthorization]
        [Route("Login/GetUserDetailsByUserName")]
        [AllowAnonymous]
        public IHttpActionResult GetUserDetailsByUserName([FromBody] UserModel user)
        {
            LoginDetails GetUserModel = new LoginDetails();
            try
            {
                GetUserModel = _unitOfWork.loginRepository.GetUserDetailsByUsername(user.Username);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(GetUserModel);
        }

        //[HttpPost]
        //[OverrideAuthorization]
        //[Route("Login/CDCMSLogin")]
        //[AllowAnonymous]
        //public async Task<IHttpActionResult> CDCMSLogin([FromBody]UserModel user)
        //{
        //    if (user == null)
        //        return InternalServerError();

        //    switch (user.GrantType)
        //    {
        //        case "password":
        //            return await GenerateNewToken(user, null, BusinessCont.LoginFromCDCMS);
        //        case "refresh_token":
        //            return await RefreshToken(user, null, BusinessCont.LoginFromCDCMS);
        //        default:
        //            return Unauthorized();
        //    }
        //}

        [HttpPost]
        [Route("Login/SendNotification")]
        public void SendNotification([FromBody]Notification notification)
        {
            try
            {
                _unitOfWork.loginRepository.SendNotification(notification.BranchId, notification.UserId, notification.Title, notification.Message, notification.PayloadStr);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "SendNotification", null, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
        }

        [HttpGet]
        [OverrideAuthorization]
        [Route("Login/Login1")]
        [AllowAnonymous]
        public HttpResponseMessage GetAntiForgeryToken()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            HttpCookie cookie = HttpContext.Current.Request.Cookies["X-CSRF-TOKEN"];

            string cookieToken;
            string formToken;
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
            AntiForgery.GetTokens(cookie == null ? "" : cookie.Value, out cookieToken, out formToken);

            AntiForgeryTokenModel content = new AntiForgeryTokenModel
            {
                AntiForgeryToken = formToken
            };

            response.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");


            if (!string.IsNullOrEmpty(cookieToken))
            {

                response.Headers.Add("X-CSRF-TOKEN", cookieToken);
                response.Headers.Add("withCredentials", "true");

                // response.Headers.Server.
                response.Headers.AddCookies(new[]
                {
                    new CookieHeaderValue("X-CSRF-TOKEN", cookieToken)
                    {
                        Expires = DateTimeOffset.Now.AddMinutes(10),
                        Path = "/"
                    }
                });
            }
            return response;
        }

        [HttpPost]
        [Route("Login/AddUpdateApiconfigurationMaster")]
        public IHttpActionResult AddUpdateAppconfigurationMaster([FromBody]AppConfiguration Appconfiguration)
        {
            if (Appconfiguration == null)
                return BadRequest(BusinessCont.InvalidClientRqst);
            try
            {
                Appconfiguration.Id = _unitOfWork.loginRepository.AddApiconfigurationMaster(Appconfiguration);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(LogId, 0, 0, "AddUpdateApiconfigurationMaster", null, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
                //throw;
            }
            return Ok(Appconfiguration);
        }

        [HttpPost]
        [OverrideAuthorization]
        [Route("Login/SaveLog")]
        [AllowAnonymous]
        public SaveLogModel SaveLog([FromBody]SaveLogModel LogDtls)
        {
            return _unitOfWork.loginRepository.SaveLog(LogDtls.saveLogStr);
        }

        [ValidateAntiForgeryTokenFilter]
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(UserModel model)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [OverrideAuthorization]
        [Route("Login/SendSMS")]
        [AllowAnonymous]
        public string SendSMS([FromBody]UserModel user)
        {
            return _unitOfWork.loginRepository.SendSMS(user);
        }

        #region Issue Tracker Login Session
        [HttpPost]
        [Route("Login/CreateIssueTrackerSession")]
        public IssueTrackerLogin CreateIssueTrackerSession([FromBody]IssueTrackerLogin _model)
        {
            IssueTrackerLogin model = new IssueTrackerLogin();
            try
            {
                model.ProfileId = "";
                model.ProfileId = _unitOfWork.loginRepository.CreateIssueTrackerLoginSession(_model);

                //model.Status= BusinessCont.SuccessStatus;
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(LogId, 0, 0, "CreateIssueTrackerSession", null, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
                //model.Status = BusinessCont.FailStatus;
                //throw;
            }
            return model;
        }

        #endregion

        #region Login Session
        [HttpPost]
        [Route("Login/EmpCompDtls")]
        [AllowAnonymous]
        public EmployeeDtls EmpCompDtls([FromBody]EmployeeDtls model)
        {
            EmployeeDtls Result = new EmployeeDtls();
            try
            {
                Result = _unitOfWork.loginRepository.EmpCompDtls(model.EmpId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(LogId, 0, 0, "LoginSession", null, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return Result;
        }
        #endregion

        #region ForgotPassword
        [HttpPost]
        [Route("Login/ForgotPassword")]
        [AllowAnonymous]
        public string ForgotPassword(UserModel model)
        {
            string Result = string.Empty;
            string emailsts = string.Empty;
            try
            {
                Result = _unitOfWork.loginRepository.ForgotPassword(model.Email); //check and update current password into default password

                if (Result == "1")
                {
                    emailsts = _unitOfWork.loginRepository.sendEmailViaWebApi(model.Email); //send email to user with default password
                    if (emailsts == "Success")
                    {
                        emailsts = BusinessCont.SuccessStatus;
                    }
                    else
                    {
                        emailsts = BusinessCont.FailStatus;
                    }
                }
                else
                {
                    emailsts = BusinessCont.FailStatus;
                }

            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(LogId, 0, 0, "ForgotPassword", null, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return emailsts;
        }
        #endregion

        #region User List By Role
        [HttpGet]
        [Route("Login/UserListByRole/{BranchId}/{CompId}/{RoleId}")]
        public List<EmployeeDtls> UserListByRole(int BranchId, int CompId, int RoleId)
        {
            List<EmployeeDtls> UserList = new List<EmployeeDtls>();
            try
            {
                UserList = _unitOfWork.UserRepository.GetUserByRole(BranchId, CompId, RoleId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(LogId, 0, 0, "UserListByRole", null, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return UserList;
        }
        #endregion

        #region Save Active user from Mobile
        [HttpPost]
        [Route("Login/SaveActiveUser")]
        public string SaveActiveUser(ActiveUser model)
        {
            string result = string.Empty;
            try
            {
                result = _unitOfWork.loginRepository.ActiveUser(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(LogId, 0, 0, "Save Active User", null, BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region Get User Permissions
        [HttpGet]
        [Route("Login/GetUserPermissions/{RoleId}")]
        public UserPermissions GetUserPermissions(int RoleId)
        {
            UserPermissions result = new UserPermissions();
            try
            {
                result = _unitOfWork.loginRepository.UserPermissions(RoleId);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(LogId, 0, RoleId, "GetUserPermissions", "Get User Permissions", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex));
            }
            return result;
        }
        #endregion

        #region User Registration
        [HttpPost]
        [Route("Login/UserRegistration")]
        [AllowAnonymous]
        public string UserRegistration(AddEmployeeModel model)
        {
            string Result = string.Empty;
            try
            {
                // Result = _unitOfWork.MastersRepository.AddEmployeeDtls(model);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, (model.EmpId != 0 ? model.EmpId : 0), "UserRegistration", "User Registration", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return Result;
        }
        #endregion

        #region Get Check Username Available
        [HttpGet]
        [Route("login/GetCheckUsernameAvailable/{Username}")]
        [AllowAnonymous]
        public CheckUsernameAvailableModel GetCheckUsernameAvailables(string Username)
        {
            CheckUsernameAvailableModel model = new CheckUsernameAvailableModel();
            try
            {
                model = _unitOfWork.MastersRepository.GetCheckUsernameAvailable(Username);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetCheckUsernameAvailables", "Get Check Username Available", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return model;
        }
        #endregion
    }
}
