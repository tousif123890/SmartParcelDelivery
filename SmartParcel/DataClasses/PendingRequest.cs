using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParcel.DataClasses
{
    public class PendingRequest
    {
        [Required]
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string PostCode { get; set; }

        public string HouseNumber { get; set; }

        public string DriverLicence { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string Photo { get; set; }
    }
}
