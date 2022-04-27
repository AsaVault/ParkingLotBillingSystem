using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBilling.Abstract;
using TicketBilling.Models;

namespace TicketBilling.Service
{
    public class ComputeBillService : IComputeBillingService
    {
        private readonly IPackingRulesRepository _prRepo;
        private readonly IPackingTicketsRepository _ptRepo;

        public ComputeBillService(IPackingRulesRepository prRepo, IPackingTicketsRepository ptRepo)
        {
            _prRepo = prRepo ?? throw new ArgumentNullException(nameof(prRepo));
            _ptRepo = ptRepo ?? throw new ArgumentNullException(nameof(ptRepo));
        }

        public async Task ComputeBill(string entryTime, string exitTime)
        {
            //Convert string to DateTime
            var entry = Convert.ToDateTime(entryTime);
            var exit = Convert.ToDateTime(exitTime);

            //Set all default values
            var vehicleName = "Packing Car";

            var amount = 0.00m;
            var diffTime = exit - entry;
            var totalExcessTime = (Convert.ToDecimal(diffTime.TotalHours - 1));
            var timeSpent = 0;

            //get packing charge
            var parkCharge =  _prRepo.GetAsQuerable().Where(x => x.RuleDescription == "Entrance fee").FirstOrDefault();
            var parkChargeAmount = parkCharge.Amount;
            amount += parkChargeAmount;

            //Get first charge
            if (diffTime.TotalHours >= 1 )
            {
               var firstCharge = _prRepo.GetAsQuerable().Where(x => x.RuleDescription == "First Full or partial hour").FirstOrDefault();
               var firstChargeAmount = firstCharge.Amount;
                amount += firstChargeAmount;
            }

            //Get second charge
            if(diffTime.TotalHours >= 2)
            {
                var excessCharge =  _prRepo.GetAsQuerable().Where(x => x.RuleDescription == "Each successive full or partial hour").FirstOrDefault();
                var excessChargeAmount = excessCharge.Amount;
                

                if(diffTime.TotalMinutes > 1 || diffTime.TotalSeconds > 1)
                {
                    totalExcessTime += 1;
                }

                //Remove all decimal palces
                totalExcessTime = Math.Truncate(totalExcessTime);

                //Calculate total excess charge
                var calculateTotalExcessCharge = totalExcessTime *  excessChargeAmount;
                amount += calculateTotalExcessCharge;
            }

            //Calculate total time spent at the parking lot
            timeSpent = Convert.ToInt32(totalExcessTime) + 1;


            //Save Packing Tickets
           await  _ptRepo.AddAsync(new PackingTicket 
            {
               Name = vehicleName,
               HoursSpent = timeSpent,
               EntryTime = entryTime,
               ExitTime = exitTime,
               AmountToPay = amount,
               Date = DateTime.Now
            });

        }

        public async Task<IEnumerable<PackingTicket>> GetAllPackingTickets()
        {
            return await _ptRepo.GetAllAsync();
        }
    }
}

