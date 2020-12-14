using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using VolvoTrucks.DataAccess;
using VolvoTrucks.Domain;
using VolvoTrucks.Repositories;

namespace VolvoTrucks.NUnit.Tests
{
    [TestFixture]
    public class RepositoriesTest
    {
        private ServiceProvider serviceProvider { get; set; }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<VolvoTruckContext>(opt => opt.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=VolvoTrucks; Trusted_Connection=True; MultipleActiveResultSets=true"));
            services.AddScoped<ITruckRepository, TruckRepository>();
            services.AddScoped<ITruckModelRepository, TruckModelRepository>();
            serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void RepositoriesAvailable()
        {
            ITruckModelRepository _modelRepo = serviceProvider.GetService<ITruckModelRepository>();
            ITruckRepository _truckRepo = serviceProvider.GetService<ITruckRepository>();

            Assert.NotNull(_modelRepo);
            Assert.NotNull(_truckRepo);
        }

        [Test]
        public void TruckModelRepositoryFetchingValidResults()
        {
            ITruckModelRepository _modelRepo = serviceProvider.GetService<ITruckModelRepository>();

            // Fetch pre-registered data from migration
            Assert.AreEqual(4, _modelRepo.FindAllModels().Count);
            Assert.AreEqual(2, _modelRepo.FindAllAvailableModels().Count);
        }

        [Test]
        public void TruckRepositoryCRUD()
        {
            ITruckModelRepository _modelRepo = serviceProvider.GetService<ITruckModelRepository>();
            ITruckRepository _truckRepo = serviceProvider.GetService<ITruckRepository>();

            Assert.NotNull(_modelRepo.FindAllAvailableModels());
            Assert.AreEqual(2, _modelRepo.FindAllAvailableModels().Count);

            // Get first available model
            var model = _modelRepo.FindAllAvailableModels()[0];

            // Saving new Truck with specific information
            Truck truck = new Truck()
            {
                Description = "TEST",
                ManufacturingYear = 1929,
                ModelYear = 1929,
                Model = model,
                TruckModelId = model.TruckModelId
            };

            _truckRepo.Save(truck);

            // List existing trucks
            List<Truck> trucks = _truckRepo.FindAllTrucks();

            // Checking for id for that specific truck saved above
            Truck savedTruck = trucks.AsQueryable().Where(el => el.ManufacturingYear == 1929).First();
            Assert.NotNull(savedTruck);
            Assert.AreEqual(1929, savedTruck.ManufacturingYear);
            Assert.AreEqual(1929, savedTruck.ModelYear);
            Assert.AreEqual("TEST", savedTruck.Description);

            int id = savedTruck.TruckId;

            // Updating truck
            Truck toUpdate = _truckRepo.FindById(id);
            toUpdate.ModelYear = 1930;
            _truckRepo.Update(toUpdate);

            // Refetching truck
            Truck updated = _truckRepo.FindById(id);
            Assert.NotNull(updated);
            Assert.AreEqual(1929, updated.ManufacturingYear);
            Assert.AreEqual(1930, updated.ModelYear);
            Assert.AreEqual("TEST", updated.Description);

            // Deleting truck
            _truckRepo.Delete(id);

            Truck deleted = _truckRepo.FindById(id);
            Assert.Null(deleted);
        }
    }
}
