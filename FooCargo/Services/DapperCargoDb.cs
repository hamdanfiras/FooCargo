using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FooCargo.Services
{
    public class DapperCargoDb
    {
        private readonly IConfiguration configuration;

        public DapperCargoDb(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IDbConnection Connnection()
        {
            var connString = configuration.GetConnectionString("CargoDbConnection");
            return new SqlConnection(connString);
        } 
    }
}
