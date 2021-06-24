using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SmartParcel.Models
{
    public class ApplicationUser : IdentityUser
    {
       [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
       
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }

        [Display(Name = "PostCode")]
        [Required(ErrorMessage = "Please Enter The PostCode")]
        public string PostCode { get; set; }

        [Display(Name = "House Number")]
        [Required(ErrorMessage = "Please Enter The House Number")]
        public string HouseNumber { get; set; }
   
        public bool RememberMe { get; set; }


        [Display(Name = "Driver Licence")]
        public string DriverLicence { get; set; }

        [Display(Name = "Licence Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        public bool IsDriver { get; set; }

        public string PhotoPath { get; set; }

        public bool IsVerified { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
