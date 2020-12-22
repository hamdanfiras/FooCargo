using FooCargo.CoreModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FooCargo.Models
{
    public class CargoDb : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public CargoDb(DbContextOptions<CargoDb> options) : base(options)
        {
        }


        // this is called Fluent API
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Manifest>().HasKey(x => new { x.FlightNumber, x.Date });
            builder.Entity<Manifest>().Property(x => x.FlightNumber).HasColumnName("FltNo");
     

            builder.Entity<Rate>().HasKey(x => new { x.MailType, x.Origin, x.Destination });
            builder.Entity<Rate>().Property(x => x.Amount).HasPrecision(10, 2);

            builder.Entity<Shipment>().HasKey(x => x.AWBNumber);
            builder.Entity<Shipment>().Property(x => x.Weight).HasPrecision(8, 2);
            builder.Entity<Shipment>().Property(x => x.Fee).HasPrecision(10, 2);

        }

        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

         
    }

  
}
