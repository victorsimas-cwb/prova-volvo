using System.ComponentModel.DataAnnotations;
using VolvoTrucks.Domain;
using VolvoTrucks.WebApp.Validators;

namespace VolvoTrucks.WebApp.Models
{
    public class TruckViewModel
    {

        [Display(Name = "Truck Code")]
        public int? TruckId { get; set; }

        public TruckModel Model { get; set; }

        [Display(Name = "Model")]
        public int? ModelId { get; set; }

        [Display(Name = "Model")]
        public string ModelName { get; set; }

        [Display(Name = "Manufacturing Year")]
        [Required]
        [YearRange]
        public int ManufacturingYear { get; set; }

        [Display(Name = "Model Year")]
        [Required]
        [YearRange]
        public int ModelYear { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public TruckViewModel()
        { }

        public TruckViewModel(Truck truck)
        {
            TruckId = truck.TruckId;
            ModelName = truck.Model.Model;
            ModelId = truck.Model.TruckModelId;
            ManufacturingYear = truck.ManufacturingYear;
            ModelYear = truck.ModelYear;
            Description = truck.Description;
        }
    }
}
