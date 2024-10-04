using CNF.Business.Model.User;
using System.Collections.Generic;

namespace CNF.Business.Repositories.Repository
{
   public interface IAdminRepository
    {
        long CreateLogin(List<UserModelForActivation> _distributorModel);
        long CreateLoginForSDSIssueTracker(List<UserModelForActivation> _distributorModel); 
       

    }
}
