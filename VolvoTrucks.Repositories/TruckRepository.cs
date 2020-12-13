using System;
using System.Collections.Generic;
using System.Linq;
using VolvoTrucks.DataAccess;
using VolvoTrucks.Domain;

namespace VolvoTrucks.Repositories
{
    public class TruckRepository : ITruckRepository
    {
        private readonly VolvoTruckContext _ctx;
        public TruckRepository(VolvoTruckContext ctx)
        {
            _ctx = ctx;
        }

        public List<Truck> FindAllTrucks()
        {
            return _ctx.Trucks.ToList();
        }

        public Truck FindById(int id)
        {
            return _ctx.Trucks.Where(t => t.TruckId == id).FirstOrDefault();
        }

        public List<Truck> FindByModelId(int modelId)
        {
            return _ctx.Trucks.Where(t => t.Model.TruckModelId == modelId).ToList();
        }

        public List<Truck> FindByManufacturingYear(int year)
        {
            return _ctx.Trucks.Where(t => t.ManufacturingYear == year).ToList();
        }

        public List<Truck> FindByModelYear(int year)
        {
            return _ctx.Trucks.Where(t => t.ModelYear == year).ToList();
        }

        public void Save(Truck truck)
        {
            _ctx.Trucks.Add(truck);
            _ctx.SaveChanges();
        }

        public void Update(Truck truck)
        {
            var t = FindById(truck.TruckId);
            if (t != null)
            {
                t.Description = truck.Description;
                t.ManufacturingYear = truck.ManufacturingYear;
                t.Model = truck.Model;
                t.ModelYear = truck.ModelYear;
                _ctx.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var t = FindById(id);
            if (t != null)
            {
                _ctx.Trucks.Remove(t);
                _ctx.SaveChanges();
            }
        }
    }
}
