using System;
using System.Collections.Generic;

#nullable disable

namespace TicketBilling.Models
{
    public partial class PackingRule
    {
        public int Id { get; set; }
        public string RuleDescription { get; set; }
        public decimal Amount { get; set; }
    }
}
