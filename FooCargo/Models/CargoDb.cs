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
        public CargoDb(DbContextOptions options) : base(options)
        {
        }
    }
}
