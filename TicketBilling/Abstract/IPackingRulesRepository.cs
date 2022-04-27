using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBilling.Models;

namespace TicketBilling.Abstract
{
    public interface IPackingRulesRepository: IGenericRepository<PackingRule>
    {
    }
}
