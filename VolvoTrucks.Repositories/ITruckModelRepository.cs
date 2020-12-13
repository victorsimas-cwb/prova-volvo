using System;
using System.Collections.Generic;
using System.Text;
using VolvoTrucks.Domain;

namespace VolvoTrucks.Repositories
{
    public interface ITruckModelRepository
    {
        List<TruckModel> FindAllModels();
        List<TruckModel> FindAllAvailableModels();
        TruckModel FindById(int id);
        TruckModel FindByModelName(string modelName);
    }
}
