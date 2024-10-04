using System;
using System.Collections.Generic;

namespace CNF.Business.Model.Configuration
{
    #region Start - Class App Configuration 
    public class AppConfigurationList
    {
        public List<AppConfigurationDetail> AppConfiParameter { get; set; }
    }
    public class AppConfigurationDetail
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Info { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
    public class AppConfigurationLst
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Info { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string Action { get; set; }
    }
    #endregion  End - Class App Configuration 
}
