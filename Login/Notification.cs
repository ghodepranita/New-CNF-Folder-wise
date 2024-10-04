using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Model.Login
{
    public class Notification
    {
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string PayloadStr { get; set; }
    }
    public class NotificationDtls
    {
        [DataMember]
        public string multicast_id { get; set; }
        [DataMember]
        public int success { get; set; }
        [DataMember]
        public int failure { get; set; }
        [DataMember]
        public string canonical_ids { get; set; }
    }
}
