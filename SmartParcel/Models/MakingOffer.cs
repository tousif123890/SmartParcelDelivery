using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParcel.Models
{
    public class MakingOffer
    {
        [Key]
        public int MakingOfferId { get; set; }

        public string UserId { get; set; }

        public int AcceptedOfferId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime PickUpDateTime { get; set; }
        [Required]
        public DateTime DropOffDateTime { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsCompleted { get; set; }
        public bool Payment { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
