using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VolvoTrucks.Domain;
using VolvoTrucks.Repositories;

namespace VolvoTrucks.Services
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _truckRepo;
        private readonly ITruckModelRepository _modelRepo;

        public TruckService(ITruckRepository truckRepository, ITruckModelRepository modelRepository) 
        {
            _truckRepo = truckRepository;
            _modelRepo = modelRepository;
        }

        public List<TruckModel> ListModels()
        {
            return _modelRepo.FindAllModels();
        }

        public TruckModel FindModelById(int id)
        {
            return _modelRepo.FindById(id);
        }

        public List<TruckModel> ListAvailableModels()
        {
            return _modelRepo.FindAllAvailableModels();
        }

        public List<Truck> ListAllTrucks()
        {
            return _truckRepo.FindAllTrucks();
        }

        public Truck FindTruckById(int id)
        {
            return _truckRepo.FindById(id);
        }

        public void SaveOrUpdateTruck(Truck truck)
        {
            if (truck == null) return;

            if (truck.TruckId > 0)
            {
                _truckRepo.Update(truck);
            }
            else
            {
                _truckRepo.Save(truck);
            }
        }

        public void DeleteTruck(int id)
        {
            if (id > 0)
            {
                _truckRepo.Delete(id);
            }
        }

    }
}
