using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParcel.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Display(Name = "PostCode")]
        [Required(ErrorMessage = "Please Enter The PostCode")]
        public string PostCode { get; set; }

        [Display(Name = "House Number")]
        [Required(ErrorMessage = "Please Enter The House Number")]
        public string HouseNumber { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
