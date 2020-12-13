using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VolvoTrucks.DataAccess;
using VolvoTrucks.Domain;

namespace VolvoTrucks.Repositories
{
    public class TruckModelRepository : ITruckModelRepository
    {
        private readonly VolvoTruckContext _ctx;
        public TruckModelRepository(VolvoTruckContext ctx) 
        {
            _ctx = ctx;
        }

        public List<TruckModel> FindAllModels()
        {
            return _ctx.TruckModels.ToList();
        }

        public List<TruckModel> FindAllAvailableModels()
        {
            return _ctx.TruckModels.Where(m => m.Available == true).ToList();
        }

        public TruckModel FindById(int id)
        {
            return _ctx.TruckModels.Where(m => m.TruckModelId == id).FirstOrDefault();
        }

        public TruckModel FindByModelName(string modelName)
        {
            return _ctx.TruckModels.Where(m => m.Model.Equals(modelName)).FirstOrDefault();
        }
    }
}
