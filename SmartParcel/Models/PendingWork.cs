using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartParcel.Models
{
    public class PendingWork
    {
        [Key]
        public int PendingWorkId { get; set; }

        public string UserId { get; set; }

        public int AcceptedOfferId { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
