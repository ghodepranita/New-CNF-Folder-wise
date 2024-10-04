using CNF.Business.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories.Repository
{
    public interface ILoginRepository
    {
        LoginDetails SendOTPSMS(UserModel user);
        string sendEmailViaWebApi(string EmailId);
        string ActiveUser(ActiveUser model);
        //string GetCurrentVersion(string ApplicationName);
        string ChangePassword(UserModel model);
        void TestExceptionFilter();
        LoginDetails GetUserDetailsByUsername(string UserName);
        //int SaveResetPasswordLog(EmailSend emailSend,int Flag);
        //ResetPasswordModel GetResetPasswordLog(int ResetId);
        int ChangeforgotPassword(ResetPasswordModel model);
        void SendNotification(int BranchId, int UserId, string Title, string Message, string PayloadStr);
        string SendSMS(UserModel user);
        LoginDetails LoginDetails(string UserName, string Password);
        //List<AppConfiguration> GetAppConfiguration(string Key);
        int AddApiconfigurationMaster(AppConfiguration _aMM);
        //string GetMobileLogs();
        SaveLogModel SaveLog(string LogDtls);
        string CreateIssueTrackerLoginSession(IssueTrackerLogin model);
        EmployeeDtls EmpCompDtls(int EmpId);
        //Forgot password
        string ForgotPassword(string EmailId);
        UserPermissions UserPermissions(int RoleId);
    }
}
