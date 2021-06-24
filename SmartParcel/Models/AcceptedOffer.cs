using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SmartParcel.Models
{
    public class AcceptedOffer
    {
        [Key]
        public int AcceptedOfferId { get; set; }

        public int MakingOfferId { get; set; }
        public string DriverId { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public bool PaymentMade { get; set; }
    }
}
