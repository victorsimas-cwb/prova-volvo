using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using VolvoTrucks.DataAccess;
using VolvoTrucks.Domain;
using VolvoTrucks.Repositories;
using VolvoTrucks.Services;

namespace VolvoTrucks.NUnit.Tests
{
    [TestFixture]
    public class ServicesTests
    {
        private ServiceProvider serviceProvider { get; set; }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<VolvoTruckContext>(opt => opt.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=VolvoTrucks; Trusted_Connection=True; MultipleActiveResultSets=true"));
            services.AddScoped<ITruckRepository, TruckRepository>();
            services.AddScoped<ITruckModelRepository, TruckModelRepository>();
            services.AddScoped<ITruckService, TruckService>();
            serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void ServiceIsInjectableAndAvailable()
        {
            ITruckService _service = serviceProvider.GetService<ITruckService>();
            Assert.NotNull(_service);
        }

        [Test]
        public void TruckServiceModelFunctions()
        {
            ITruckService _service = serviceProvider.GetService<ITruckService>();

            // Validating pre-populated models
            List<TruckModel> models = _service.ListModels();
            Assert.NotNull(models);
            Assert.AreEqual(4, models.Count);

            TruckModel model1 = _service.FindModelById(1);
            Assert.NotNull(model1);
            Assert.AreEqual(1, model1.TruckModelId);
            Assert.AreEqual("FH", model1.Model);
            Assert.AreEqual(true, model1.Available);

            TruckModel model2 = _service.FindModelById(2);
            Assert.NotNull(model2);
            Assert.AreEqual(2, model2.TruckModelId);
            Assert.AreEqual("FM", model2.Model);
            Assert.AreEqual(true, model2.Available);

            TruckModel model3 = _service.FindModelById(3);
            Assert.NotNull(model3);
            Assert.AreEqual(3, model3.TruckModelId);
            Assert.AreEqual("FMX", model3.Model);
            Assert.AreEqual(false, model3.Available);

            TruckModel model4 = _service.FindModelById(4);
            Assert.NotNull(model4);
            Assert.AreEqual(4, model4.TruckModelId);
            Assert.AreEqual("VM", model4.Model);
            Assert.AreEqual(false, model4.Available);

            List<TruckModel> availableModels = _service.ListAvailableModels();
            Assert.NotNull(availableModels);
            availableModels.ForEach(m => {
                Assert.AreEqual(true, m.Available);
            });
        }

        [Test]
        public void TruckServiceFunctions()
        {
            ITruckService _service = serviceProvider.GetService<ITruckService>();

            List<TruckModel> models = _service.ListAvailableModels();
            Assert.NotNull(models);
            Assert.AreEqual(2, models.Count);
            // Get first available model
            var model = models[0];

            // Saving new Truck with specific information
            Truck truck = new Truck()
            {
                Description = "TEST",
                ManufacturingYear = 1944,
                ModelYear = 1944,
                Model = model,
                TruckModelId = model.TruckModelId
            };

            List<Truck> receivedTrucks = _service.ListAllTrucks();
            List<Truck> filtered = receivedTrucks.Where(t => t.ManufacturingYear == 1944 || t.ModelYear == 1944).ToList();
            Assert.AreEqual(0, filtered.Count);

            _service.SaveOrUpdateTruck(truck);

            receivedTrucks = _service.ListAllTrucks();
            filtered = receivedTrucks.Where(t => t.ManufacturingYear == 1944 || t.ModelYear == 1944).ToList();
            
            Assert.AreEqual(1, filtered.Count);
            
            Truck saved = filtered[0];

            Assert.AreEqual(1944, saved.ManufacturingYear);
            Assert.AreEqual(1944, saved.ModelYear);
            Assert.AreEqual("TEST", saved.Description);

            int id = saved.TruckId;

            Truck toUpdate = _service.FindTruckById(id);
            
            Assert.NotNull(toUpdate);
            
            toUpdate.ModelYear = 1950;
            _service.SaveOrUpdateTruck(toUpdate);

            receivedTrucks = _service.ListAllTrucks();
            filtered = receivedTrucks.Where(t => t.ManufacturingYear == 1944 && t.ModelYear == 1950).ToList();

            Assert.AreEqual(1, filtered.Count);

            Truck updated = filtered[0];

            Assert.AreEqual(1944, updated.ManufacturingYear);
            Assert.AreEqual(1950, updated.ModelYear);
            Assert.AreEqual("TEST", updated.Description);

            _service.DeleteTruck(id);

            receivedTrucks = _service.ListAllTrucks();
            filtered = receivedTrucks.Where(t => t.ManufacturingYear == 1944 || t.ModelYear == 1950).ToList();

            Assert.AreEqual(0, filtered.Count);
        }
    }
}

