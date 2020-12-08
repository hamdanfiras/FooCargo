﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FooCargo.CoreModels;
using FooCargo.Models;

namespace FooCargo.Controllers
{
    public class RatesController : BaseController
    {
        public RatesController(CargoDb context) : base(context)
        {
        }

        // GET: api/Rates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rate>>> GetRates()
        {
            return await Db.Rates.ToListAsync();
        }

        // GET: api/Rates/5
        [HttpGet("{mailType}/{origin}/{destination}")]
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

            return CreatedAtAction("GetRate", new { id = rate.MailType }, rate);
        }

        // DELETE: api/Rates/5
        [HttpDelete("{id}")]
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