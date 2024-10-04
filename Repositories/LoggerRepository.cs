using CNF.Business.Model.Context;
using CNF.Business.Model.Login;
using CNF.Business.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private CFADBEntities _contextManager;
        public LoggerRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }

        #region Logger Repo : to get logged records by passing parameters : from date, to date, role id, status etc
        /// <summary>
        /// Logger Repo : to get logged records by passing parameters : from date, to date, role id, status etc
        /// </summary>
        /// <param name="contextManager"></param>

        //public List<AuditLog> GetLoggerDetails()
        //{
        //    return GetLoggerDetailsPvt();
        //}
        //private List<AuditLog> GetLoggerDetailsPvt()
        //{
        //    try
        //    {
        //        return _contextManager.usp_GetLogDetails()
        //                                   .Select(x => new AuditLog()
        //                                   {
        //                                       LogID = x.LogID,
        //                                       DistributorId = x.DistributorId,
        //                                       LogData = x.LogData,
        //                                       LogDatetime = x.LogDatetime,
        //                                       LogStatus = x.LogStatus,
        //                                       LogException = x.LogException,
        //                                       LogFor = x.LogFor,
        //                                       LogDatetimeStr = BusinessConstant.BusinessCont.CheckNullandConvertDateWithTime(x.LogDatetime)
        //                                   }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion
    }
}
