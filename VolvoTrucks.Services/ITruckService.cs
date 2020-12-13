using System;
using System.Collections.Generic;
using System.Text;
using VolvoTrucks.Domain;

namespace VolvoTrucks.Services
{
    public interface ITruckService
    {
        List<TruckModel> ListModels();
        List<TruckModel> ListAvailableModels();
        List<Truck> ListAllTrucks();
        void SaveOrUpdateTruck(Truck truck);
        List<Truck> ListTrucks(string field, string param);
    }
}
