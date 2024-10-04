using CNF.Business.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories.Repository
{
    public interface IUserRepository
    {
        //Get token from credentials
        LoginDetails GetLoginDetails(string UserName, string Password, int company, int Branch, int role);

        LoginDetails GetLoginDetailsByUserNameUId(string UserName, int UserId, int company, int Branch, int role);

        //Create After Deleting if exists Refresh Token
        long CreateAfetrDeletingRefreshToken(RefreshToken model);       

        //Get RToken By Id
        RefreshToken GetRefreshTokenByUserId(long UserId);
        long UpdateFeedbackSystemStatus(UpdateFeedbackSystem updateFeedback);
        string OfficerValidate(string UserId, string Password);
        List<EmployeeDtls> GetUserByRole(int BranchId, int CompId, int RoleId);
    }
}
