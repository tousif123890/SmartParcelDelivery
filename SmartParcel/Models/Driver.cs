using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParcel.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        public string UserId { get; set; }

        public int AddressId { get; set; }

        [Required]
        [Display(Name ="Driver Licence Number")]
        public string LicenceNumber { get; set; }

        [Required]
        [Display(Name = "Driver Licence Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
