using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FooCargo.CoreModels;
using FooCargo.Models;
using Microsoft.AspNetCore.Authorization;
using FooCargo.Authorization;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using FooCargo.Services;
using System.Threading;

namespace FooCargo.Controllers
{
#if DEBUG
    [AllowAnonymous] 
#endif

    public class RatesController : BaseController
    {
        private readonly DapperCargoDb dapperCargoDb;
        private readonly IConfiguration configuration;

        public RatesController(CargoDb context, DapperCargoDb dapperCargoDb, IConfiguration configuration) : base(context)
        {
            this.dapperCargoDb = dapperCargoDb;
            this.configuration = configuration;
        }

        // GET: api/Rates
        [HttpGet]
        [Authorize(Policy = Policies.VIEW_RATE)]
        public async Task<ActionResult<IEnumerable<Rate>>> GetRates()
        {
            return await Db.Rates.ToListAsync();
        }

        // GET: api/Rates
        [HttpGet()]
        [Route("RatesView")]
        [Authorize(Policy = Policies.VIEW_RATE)]
        public  ActionResult<IEnumerable<RateView>> GetRatesFromView()
        {
            var res = dapperCargoDb.Connnection().Query<RateView>("Select * From RatesView").ToList();
            return res;
        }


        // GET: api/Rates/5
        [HttpGet("{mailType}/{origin}/{destination}")]
        [Authorize(Policy = Policies.VIEW_RATE)]
        public async Task<ActionResult<Rate>> GetRate(MailType mailType, string origin, string destination)
        {
            var rate = await Db.Rates.FirstOrDefaultAsync(x => x.MailType == mailType && x.Origin == origin && x.Destination == destination);

            if (rate == null)
            {
                return NotFound();
            }

            return rate;
        }

        // PUT: api/Rates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{mailType}/{origin}/{destination}")]
        [Authorize(Policy = Policies.MANAGE_RATE)]

        public async Task<IActionResult> PutRate(MailType mailType, string origin, string destination, Rate rate)
        {
            if (mailType != rate.MailType || origin != rate.Origin || destination != rate.Destination)
            {
                return BadRequest();
            }

            Db.Entry(rate).State = EntityState.Modified;

            try
            {
                await Db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateExists(mailType, origin, destination))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.MANAGE_RATE)]
        public async Task<ActionResult<Rate>> PostRate(Rate rate)
        {
            Db.Rates.Add(rate);
            try
            {
                await Db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RateExists(rate.MailType, rate.Origin, rate.Destination))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRate", new { mailType = rate.MailType, origin = rate.Origin, destination = rate.Destination }, rate);
        }

        // DELETE: api/Rates/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.MANAGE_RATE)]
        public async Task<IActionResult> DeleteRate(MailType id)
        {
            var rate = await Db.Rates.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

            Db.Rates.Remove(rate);
            await Db.SaveChangesAsync();

            return NoContent();
        }

        private bool RateExists(MailType id, string origin, string destination)
        {
            return Db.Rates.Any(e => e.MailType == id && e.Origin == origin && e.Destination == destination);
        }
    }

}
