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
        public string? sunrise { get; set; }
        public string? sunset { get; set; }
        public string? OriginalSunrise { get; set; }
        public string? OriginalSunset { get; set; }
        public bool SummerWinter { get; set; }

        public string DagenDetGaller { get; set; }
    }
}





