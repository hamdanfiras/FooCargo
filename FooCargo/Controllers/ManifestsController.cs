using FooCargo.CoreModels;
using FooCargo.Models;
using FooCargo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooCargo.Controllers
{
    public class ManifestsController : BaseController
    {
        private readonly RateCalculator rateCalculator;

        public ManifestsController(CargoDb context, RateCalculator rateCalculator) : base(context)
        {
            this.rateCalculator = rateCalculator;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Manifest>>> Get()
        {
            return await Db.Manifests.ToListAsync();
        }

        [HttpGet("{flightNumber}/{flightDate}")]
        public async Task<ActionResult<Manifest>> Details(string flightNumber, DateTime flightDate)
        {
            var manifest = await Db.Manifests.Include(x => x.Shipments).FirstOrDefaultAsync(x => x.FlightNumber == flightNumber && x.Date == flightDate);

            return manifest;
        }

        [HttpPost("")]
        public async Task<ActionResult> Post(Manifest manifest)
        {
            // validation must be done and return badrequest if not

            manifest.Date = manifest.Date.Date;
            await Db.Manifests.AddAsync(manifest);
            await Db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("Shipment")]
        public async Task<ActionResult> PostShipment(Shipment shipment)
        {
            // validation must be done and return badrequest if not

            if (shipment?.Manifest?.FlightNumber == null)
            {
                return BadRequest();
            }

            Db.Entry(shipment.Manifest).State = EntityState.Unchanged;

            shipment.Fee = await rateCalculator.CalculateRate(shipment.MailType, shipment.Origin, shipment.Destination, shipment.Weight, shipment.NumberOfItems);
            
            await Db.Shipments.AddAsync(shipment);
            await Db.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("Shipment/{awbnumber}")]
        public async Task<ActionResult> DeleteShipment(string awbnumber)
        {
            var shipment = Db.Shipments.FirstOrDefault(x => x.AWBNumber == awbnumber);
            if (shipment != null)
            {
                Db.Shipments.Remove(shipment);
                await Db.SaveChangesAsync();
            }

            return NoContent();
        }

    }

    //public class ShipmentsController : BaseController
    //{
    //    public ShipmentsController(CargoDb context) : base(context)
    //    {
    //    }

    //    public async Task<ActionResult<List<Shipment>>> Get(Guid manifestId)
    //    {
    //        return Db.Shipments.Where()
    //    }
    //}
}
