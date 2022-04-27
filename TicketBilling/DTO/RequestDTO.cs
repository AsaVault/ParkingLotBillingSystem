using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBilling.DTO
{
    public class RequestDTO
    {
        [Required]
        public string EntryTime { get; set; }

        [Required]
        public string ExitTime { get; set; }
    }
}
