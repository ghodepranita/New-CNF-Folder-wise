using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Model.Login
{
    public class LoggerModel
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Method { get; set; }
        public string ExecutionType { get; set; }        
        public string ControllerActionName { get; set; }        
        public string URL { get; set; }
        public string DisplayName { get; set; }
        public string RefNo { get; set; }
        public string RoleId { get; set; }
        public string Parameters { get; set; }
        public string LogStatus { get; set; }
        public string Exception { get; set; }
        public DateTime LogDateTime { get; set; }

        public string LogDateTimestr { get; set; }

        //Input Paramaters
        public DateTime FromOnDate { get; set; }
        public DateTime ToDate { get; set; }

    }

    public class LogDetails
    {
        public decimal LogID { get; set; }
        public int ServiceId { get; set; }
        public int DistId { get; set; }
        public decimal GId { get; set; }
        public decimal DId { get; set; }
        public string LFor { get; set; }
        public string LData { get; set; }
        public string LStatus { get; set; }
        public string LEx { get; set; }
        public string LogDT { get; set; }
        public string LogDtls { get; set; }
    }
    public class AuditLog
    {
        public decimal LogID { get; set; }
        public Nullable<int> DistributorId { get; set; }
        public string LogFor { get; set; }
        public string LogData { get; set; }
        public string LogStatus { get; set; }
        public Nullable<System.DateTime> LogDatetime { get; set; }
        public string LogException { get; set; }
        public string LogDatetimeStr { get; set; }
    }
    public class TimeModel
    {
        public string StateCode { get; set; }
        public string DistrictCode { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
    }
}
