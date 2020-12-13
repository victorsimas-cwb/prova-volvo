using Microsoft.EntityFrameworkCore;
using System;
using VolvoTrucks.Domain;

namespace VolvoTrucks.DataAccess
{
    public class VolvoTruckContext : DbContext
    {

        //private const string connStr = "Server =(localdb)\\MSSQLLocalDB; Database=VolvoTrucks; Trusted_Connection=True; MultipleActiveResultSets=true";
        public VolvoTruckContext()
        { }

        public VolvoTruckContext(DbContextOptions<VolvoTruckContext> options) : base(options)
        { }

        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckModel> TruckModels { get; set; }

        private void ConfigTruckModels(ModelBuilder builder)
        {
            builder.Entity<TruckModel>(ent =>
            {
                ent.ToTable("TRUCK_MODELS");
                ent.HasKey(p => p.TruckModelId);
                ent.HasData(
                    new TruckModel
                    {
                        TruckModelId = 1,
                        Model = "FH",
                        Available = true
                    }, 
                    new TruckModel
                    {
                        TruckModelId = 2,
                        Model = "FM",
                        Available = true
                    },
                    new TruckModel
                    {
                        TruckModelId = 3,
                        Model = "FMX",
                        Available = false
                    },
                    new TruckModel
                    {
                        TruckModelId = 4,
                        Model = "VM",
                        Available = false
                    }
                    );
            });
        }
        private void ConfigTrucks(ModelBuilder builder)
        {
            builder.Entity<Truck>(ent => 
            {
                ent.ToTable("TRUCKS");
                ent.HasKey(p => p.TruckId);
                ent.HasOne(p => p.Model).WithMany(m => m.Trucks);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("VOLVOTRUCKS");
            ConfigTruckModels(modelBuilder);
            ConfigTrucks(modelBuilder);
        }

    }
}