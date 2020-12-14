using System;
using System.ComponentModel.DataAnnotations;

namespace VolvoTrucks.Domain
{
    public class Truck
    {
        public int TruckId { get; set; }
        public int TruckModelId { get; set; }
        public TruckModel Model { get; set; }
        public int ManufacturingYear { get; set; }
        public int ModelYear { get; set; }
        public string Description { get; set; }
    }
}
