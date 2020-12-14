using System;
using System.ComponentModel.DataAnnotations;

namespace VolvoTrucks.WebApp.Validators
{
    public class YearRangeAttribute : RangeAttribute
    {
        public YearRangeAttribute() : base(DateTime.Now.Year, (DateTime.Now.Year + 1)) { }
    }
}
