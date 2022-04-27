using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBilling.Abstract;
using TicketBilling.Models;

namespace TicketBilling.Concrete
{
    public class PackingTicketsRepository : GenericRepository<PackingTicket>, IPackingTicketsRepository
    {
        private readonly db_a483f5_usertestContext _context;

        public PackingTicketsRepository(db_a483f5_usertestContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
