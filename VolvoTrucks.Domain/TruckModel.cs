using System;
using System.Collections.Generic;
using System.Text;

namespace VolvoTrucks.Domain
{
    public class TruckModel
    {
        public int TruckModelId { get; set; }
        public string Model { get; set; }
        public bool Available { get; set; }
        public virtual ICollection<Truck> Trucks { get; set; }
    }
}
