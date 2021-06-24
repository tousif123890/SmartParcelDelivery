using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParcel.Models
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }

        public int MakingOfferId { get; set; }

        public bool IsAccepted { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime PickUpTime { get; set; }
        public DateTime DropOffTime { get; set; }

        public float PickLatitude { get; set; }

        public float PickLongitude { get; set; }

        public float DropLatitude { get; set; }

        public float DropLongitude { get; set; }

    }
}
