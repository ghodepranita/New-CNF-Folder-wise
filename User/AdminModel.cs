using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Business.Model.User
{
    public class ZOModel
    {
        public string ZOCode { get; set; }
        public string ZOName { get; set; }
        public string ActiveFlag { get; set; }
        public string LastUpdateBy { get; set; }
        public System.DateTime LastUpdateDateTime { get; set; }
    }
    public class ROModel
    {
        public string ROCode { get; set; }
        public string ROName { get; set; }
        public string ZOCode { get; set; }
        public string ActiveFlag { get; set; }
        public string LastUpdateBy { get; set; }
        public System.DateTime LastUpdateDateTime { get; set; }
        public string ZOName { get; set; }
    }
    public class SAModel
    {
        public string SACode { get; set; }
        public string SAName { get; set; }
        public string ROCode { get; set; }
        public string ROName { get; set; }
        public string ZOCode { get; set; }
        public string ZOName { get; set; }
        public string ActiveFlag { get; set; }
        public string LastUpdateBy { get; set; }
        public System.DateTime LastUpdateDateTime { get; set; }
    }
    public class SlotAllotmentPostModel
    {
        public string DistributorIds { get; set; }
        public string DistributorIdsForRemoval { get; set; }
        public DateTime SlotDateTime { get; set; }
        public int ClassRoomId { get; set; }
        public string Flag { get; set; }
    }
    public class SlotAllotment
    {
        public string ZOCode { get; set; }
        public string ZOName { get; set; }
        public string ROCode { get; set; }
        public string ROName { get; set; }
        public string SACode { get; set; }
        public string SAName { get; set; }
        public int DistributorId { get; set; }
        public string DistributorName { get; set; }
        public string JDEDistributorCode { get; set; }
        public Nullable<System.DateTime> AllotedDateTime { get; set; }
        public string AllotedDateTimeStr { get; set; }
        public Nullable<System.DateTime> StartDateTime { get; set; }
        public string StartDateTimeStr { get; set; }
        public Nullable<System.DateTime> EndDateTime { get; set; }
        public string EndDateTimeStr { get; set; }
        public Nullable<decimal> Duration { get; set; }
        public string ActiveForOnBoarding { get; set; }
        public Nullable<decimal> Id { get; set; }
        public string SessionName { get; set; }
    }

    public class ClassRoom
    {
        public decimal Id { get; set; }
        public Nullable<int> DistributorId { get; set; }
        public string SessionName { get; set; }
        public string VideoTitle { get; set; }
        public string VideoName { get; set; }
        public string PosterImageName { get; set; }
        public string Language { get; set; }
        public string IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateDateTime { get; set; }
        public string Flag { get; set; }
    }
}
