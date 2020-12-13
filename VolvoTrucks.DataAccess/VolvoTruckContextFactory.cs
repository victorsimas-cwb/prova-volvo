using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace VolvoTrucks.DataAccess
{
    public class VolvoTruckContextFactory : IDesignTimeDbContextFactory<VolvoTruckContext>
    {
        private const string connStr = "Server=(localdb)\\MSSQLLocalDB; Database=VolvoTrucks; Trusted_Connection=True; MultipleActiveResultSets=true";
        public VolvoTruckContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<VolvoTruckContext>();
            builder.UseSqlServer(connStr);
            return new VolvoTruckContext(builder.Options);
        }
    }
}
