using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAPISunset
{
    public class Results
    {
        public int Id { get; set; }
        public string? sunrise { get; set; }//tid
        public string? sunset { get; set; }//tid
        public string? OriginalSunrise { get; set; }//tid
        public string? OriginalSunset { get; set; }//tid
        public bool SummerWinter { get; set; }//bool
        public double Latitude { get; set; }//double
        public double Longitude { get; set; }//double
        public string DagenDetGaller { get; set; }//Datum

        //public int Id { get; set; }
        //public TimeOnly? sunrise { get; set; }
        //public TimeOnly? sunset { get; set; }
        //public TimeOnly? OriginalSunrise { get; set; }
        //public TimeOnly? OriginalSunset { get; set; }
        //public bool SummerWinter { get; set; }
        //public double Latitude { get; set; }
        //public double Longitude { get; set; }
        //public DateOnly Date { get; set; }
    }
}





