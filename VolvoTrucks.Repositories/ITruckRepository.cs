using System;
using System.Collections.Generic;
using System.Text;
using VolvoTrucks.Domain;

namespace VolvoTrucks.Repositories
{
    public interface ITruckRepository
    {
        List<Truck> FindAllTrucks();
        Truck FindById(int id);
        List<Truck> FindByModelId(int modelId);
        List<Truck> FindByManufacturingYear(int year);
        List<Truck> FindByModelYear(int year);
        void Save(Truck truck);
        void Update(Truck truck);
        void Delete(int id);

    }
}
