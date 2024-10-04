using CNF.Business.BusinessConstant;
using CNF.Business.Model.Configuration;
using CNF.Business.Model.Context;
using CNF.Business.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private CFADBEntities _contextManager;

        //Inject DB Context instance from Unit of Work to avoid unwanted memory allocations.
        public ConfigurationRepository(CFADBEntities contextManager)
        {
            _contextManager = contextManager;
        }

        #region App Configuration List
        /// <summary>
        ///  App Configuration List
        /// </summary>
        /// <returns></returns>
        public AppConfigurationList GetAppConfiguration()
        {
            return GetAppConfigurationPvt();
        }
        private AppConfigurationList GetAppConfigurationPvt()
        {
            AppConfigurationList appconfigurationlist = new AppConfigurationList();
            try
            {
                using (CFADBEntities ContextManager = new CFADBEntities())
                {
                    appconfigurationlist.AppConfiParameter = ContextManager.usp_AppConfigurationList().Select(x => new AppConfigurationDetail()
                    {
                        Id = Convert.ToInt32(x.Id),
                        Key = x.Key,
                        Value = x.Value,
                        Info = x.Info
                    }).OrderByDescending(x => x.Id).ToList();

                }
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "GetAppConfigurationPvt", "Get App Configuration", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            return appconfigurationlist;
        }
        #endregion

        #region Add Edit App Configuration 
        /// <summary>
        /// Add Edit App Configuration 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddEditAppConfiguration(AppConfigurationLst model)
        {
            return AddEditAppConfigurationPvt(model);
        }
        private string AddEditAppConfigurationPvt(AppConfigurationLst model)
        {
            int RetValue = 0;
            ObjectParameter obj = new ObjectParameter("RetValue", typeof(int));
           
            try
            {
                RetValue = _contextManager.usp_AppConfigurationAddEdit(model.Id, model.Key, model.Value, model.Info, model.Action, obj);
            }
            catch (Exception ex)
            {
                BusinessCont.SaveLog(0, 0, 0, "AddEditAppConfiguration", "Add Edit App Configuration", BusinessCont.FailStatus, BusinessCont.ExceptionMsg(ex.InnerException));
            }
            if (RetValue > 0)
            {
                return BusinessCont.SuccessStatus;
            }
            else
            {
                return BusinessCont.msg_exist;
            }

        }
        #endregion
    }
}
