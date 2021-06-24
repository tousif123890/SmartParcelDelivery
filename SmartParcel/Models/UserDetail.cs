using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SmartParcel.Models
{
    public class UserDetail
    {
        [Key]
        public string UserId { get; set; }

        public int AddressId { get; set; }

  
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
    
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
     
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

      
        public int Gender { get; set; }

   
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = " There must be atleast one Number, one Uppercase , one lowercase and one special character." ,MinimumLength = 7)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password does not match. Please Try Again.")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }

        public bool RememberMe { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}