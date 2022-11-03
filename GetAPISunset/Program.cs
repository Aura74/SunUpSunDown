using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

using GetAPISunset.Services;
using GetAPISunset.Models;

namespace GetAPISunset
{
    class Program
    {
        static async Task Main(string[] args)
        {
            double latitude = 60.67452;
            double longitude = 17.14174;

            var _client = new SunriseClient();
            var _db = new ApplicationDbContext();

            //var dateStart = new DateTime(2022, 10, 29);
            //var dateEnd = new DateTime(2022, 10, 31);
            //var dayCount = dateEnd.Subtract(dateStart).TotalDays + 1;

            DateOnly wert = new DateOnly(2022, 10, 29);//Datum
            DateOnly dateOnly = wert;//Datum

            DateTime testDateTime = dateOnly.ToDateTime(TimeOnly.Parse("00:00 AM"));//Datum
            DateTime dayStart = testDateTime;//Datum
            DateTime dayEnd = new DateTime(2022, 10, 31);//Datum

            double daysUntil = dayEnd.Subtract(dayStart).TotalDays;

            Console.WriteLine($"Från {dayStart.ToString("D")} fram till {dayEnd.ToString("D")} är det {daysUntil+1} dagar\n");
            Console.WriteLine($"valt Latitude: {latitude} och Valt Longitude: {longitude}\n");

            for (int i = 0; i < daysUntil+1; i++)
            {
                var result = await _client.GetDayAsync(wert.ToString(), latitude, longitude);
                
                if (!result.SummerWinter)
                    Console.WriteLine($"Vid datumet {result.DagenDetGaller} är det vintertid");
                else
                    Console.WriteLine($"Vid datumet {result.DagenDetGaller} är det sommartid");

                _db.SunTime.Add(result);

                wert = wert.AddDays(1);
            }

            _db.SaveChanges();
        }
    }
}