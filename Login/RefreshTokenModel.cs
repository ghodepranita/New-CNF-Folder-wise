using System;

namespace CNF.Business.Model.Login
{
    public class RefreshTokenModel
    {      
        public string ClientId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public DateTime ExpiryTime { get; set; }
    }

    public class RefreshToken
    { 
        public int TokenId { get; set; }
        public Nullable<long> RtnValue { get; set; }
        public string ClientKey { get; set; }
        public string RefreshValue { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long UserId { get; set; }
        public System.DateTime LastUpdateDate { get; set; }
        public System.DateTime ExpiryTime { get; set; }
        
        public string Action { get; set; }     
        public long AddUpdateResult { get; set; }
    }
}
