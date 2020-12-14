using System;
using System.Collections.Generic;
using System.Text;
using VolvoTrucks.Domain;

namespace VolvoTrucks.Services
{
    public interface ITruckService
    {
        List<TruckModel> ListModels();
        TruckModel FindModelById(int id);
        List<TruckModel> ListAvailableModels();
        List<Truck> ListAllTrucks();
        Truck FindTruckById(int id);
        void SaveOrUpdateTruck(Truck truck);
        void DeleteTruck(int id);
    }
}
