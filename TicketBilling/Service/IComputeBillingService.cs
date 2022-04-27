using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBilling.Models;

namespace TicketBilling.Service
{
    public interface IComputeBillingService
    {
        Task ComputeBill(string entryTime, string exitTime);
        Task<IEnumerable<PackingTicket>> GetAllPackingTickets();
    }
}
