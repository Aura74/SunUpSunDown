using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAPISunset.Models
{
    public class SunriseItem
    {
        public int Id { get; set; }
        public string? sunrise { get; set; }//tid
        public string? sunset { get; set; }//tid
        public string? OriginalSunrise { get; set; }//tid
        public string? OriginalSunset { get; set; }//tid
        public bool SummerWinter { get; set; }//bool
        public double Latitude { get; set; }//double
        public double Longitude { get; set; }//double
        public string Day { get; set; }//Datum

    }
}





