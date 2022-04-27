using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBilling.DTO;
using TicketBilling.Service;

namespace TicketBilling.Controllers
{
    [Route("api/billing")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IComputeBillingService _service;

        public BillingController(IComputeBillingService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetBilling(RequestDTO payload)
        {
            try
            {
                if (payload == null)
                {
                    return BadRequest("Invalid payload");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                await _service.ComputeBill(payload.EntryTime, payload.ExitTime);
                var result = await _service.GetAllPackingTickets();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest("Billing failed " + ex.Message);
            }
        }
    }
}
