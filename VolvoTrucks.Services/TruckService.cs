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

        public List<TruckModel> ListAvailableModels()
        {
            return _modelRepo.FindAllAvailableModels();
        }

        public List<Truck> ListAllTrucks()
        {
            return _truckRepo.FindAllTrucks();
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

        public List<Truck> ListTrucks(string field, string param)
        {
            if (field == "id" || field == "ma_year" || field == "mo_year" || field == "model_id")
            {
                string param_s = Regex.Replace(param, "[^0-9]", "");
                int param_i = Int32.Parse(param_s);
                switch(field)
                {
                    case "id":
                        List<Truck> res = new List<Truck>();
                        res.Add(_truckRepo.FindById(param_i));
                        return res;
                    case "ma_year":
                        return _truckRepo.FindByManufacturingYear(param_i);
                    case "mo_year":
                        return _truckRepo.FindByModelYear(param_i);
                    case "model_id":
                        return _truckRepo.FindByModelId(param_i);
                    default:
                        return _truckRepo.FindAllTrucks();
                }
            }
            else
            {
                return null;
            }

        }

    }
}
