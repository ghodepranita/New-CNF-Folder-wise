using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Model.User
{
    public class DistributorItems
    {
        public Nullable<long> DistItemID { get; set; }
        public int ItemID { get; set; }
        public string ItemDescription { get; set; }
        public string ItemShortDescription { get; set; }

        public string ActiveFlag { get; set; }
        public string PackageCode { get; set; }
    }
    public class PostModel
    {
        public int DistributorId { get; set; }
        public int AreaClusterId { get; set; }
        public decimal AreaRefNo { get; set; }
        public string OrderStatus { get; set; }
        public decimal StaffRefNo { get; set; }
        public string TripDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string VehicleNo { get; set; }
        public int PriceCode { get; set; }
        public string TripNo { get; set; }
        public string BookingDate { get; set; }

        public string Flag { get; set; }
    }

    public class DistributorModel
    {
        public int DistributorId { get; set; }
        public string DistributorName { get; set; }
        public string JDEDistributorCode { get; set; }
        public string DistributorAddress { get; set; }
        public string Email { get; set; }
        public Nullable<decimal> MobileNo { get; set; }
        public string GSTNo { get; set; }
        public string SACode { get; set; }
        public string SAName { get; set; }
        public string ActiveFlag { get; set; }
        public string Action { get; set; }
        public long UserId { get; set; }
        public string Status { get; set; }
        public string Keydata { get; set; }
        public string ValueData { get; set; }
        public DistributorFlags Flags { get; set; }
    }
    public class DistributorFlags
    {
        public string MultipleGodown { get; set; }
        public string MultipleHelper { get; set; }
        public string MobileGodown { get; set; }
        public string AllowCycle { get; set; }
    }
    public class DistributorConfig
    {
        public int DistributorId { get; set; }
        public int ConfigId { get; set; }
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
    }
    public class DistributorDetails 
    {
        public List<DistributorModel> DistributorList { get; set; }
        public List<ConsumerGeocodingModel> ConsumerGeocodingList { get; set; }
        public long LoginId { get; set; }
    }

    public class DistributorConsumerModel
    {
        public int? DistributorId { get; set; }
        public string DistributorName { get; set; }
        public int? ConsumerHolding { get; set; }
        public int? AvgDelivery { get; set; }
        public int? MI_Nos { get; set; }
        public Nullable<decimal> MI_Percernt { get; set; }
        public int? EZYGAS_Nos { get; set; }
        public Nullable<decimal> EZYGas_Percernt { get; set; }
        public int? SARVEKSHAN_Nos { get; set; }
        public Nullable<decimal> Sarvekshan_Percernt { get; set; }
        public int? Total_Nos { get; set; }
        public Nullable<decimal> Total_Percernt { get; set; }
        public int? PendingForRevgeo { get; set; }
        public Nullable<decimal> PendingForRevGeo_Percernt { get; set; }
        public int? NoOfVehicles { get; set; }
        public int? NoOfTrips { get; set; }
    }

    public class DistributorConsumerDetails 
    {
        public List<DistributorConsumerModel> DistributorConsumerList { get; set; }
    }


    public class DistributorClusterModel
    {
        public int? DistributorId { get; set; }
        public string DistributorName { get; set; }
        public int ClusterId { get; set; }
        public string ClusterName { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string NumberOfWheels { get; set; }
        public int? RTOCapacity { get; set; }
        public int? NoOfTrip { get; set; }
        public int? PossibleDel { get; set; }
    }

    public class DistributorClusterDetails 
    {
        public List<DistributorClusterModel> DistributorClusterList { get; set; }
    }

    public class TaxDetails
    {
        public string TaxType { get; set; }
        public int Tax { get; set; }
    }
    public class DistributorLocationModel
    {
        public int DistributorId { get; set; }
        public string JDEDistributorCode { get; set; }
        public string DistributorName { get; set; }
        public string DAddress { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<decimal> MobileNo { get; set; }
        public string Email { get; set; }
        public string CityName { get; set; }
        public string DLat { get; set; }
        public string DLong { get; set; }
        public string DistrictCode { get; set; }
        public string StateCode { get; set; }
    }
    public class ConsumerGeocodingModel
    {
        public int DistributorId { get; set; }
        public string DistributorName { get; set; }
        public string JDEDistributorCode { get; set; }

        public int?TotalConsumer { get; set; }
        public int? LocationFound { get; set; }
        public int? SavedInSDS { get; set; }
        public int? RequiredGeocoding { get; set; }
        public string Flags { get; set; }
    }
    public class UserModelForActivation
    {
        public string UserName { get; set; }
        public int RefNo { get; set; }
        public int RoleId { get; set; }
        public string DisplayName { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public DistributorFlags Flags { get; set; }
    }
}
