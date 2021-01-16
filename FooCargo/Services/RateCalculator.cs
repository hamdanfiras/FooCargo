using FooCargo.CoreModels;
using FooCargo.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FooCargo.Services
{
    public class RateCalculator
    {
        private readonly CargoDb db;

        public RateCalculator(CargoDb db)
        {
            this.db = db;
        }

        public async Task<decimal> CalculateRate(MailType mailType, string origin, string destination, decimal weight, int numberOfItems)
        {
            var rate = await db.Rates.AsNoTracking().FirstOrDefaultAsync(x => x.MailType == mailType && x.Origin == origin && x.Destination == destination);
            if (rate == null) return 0;
            return rate.Amount * weight * numberOfItems;
        }
    }
}
