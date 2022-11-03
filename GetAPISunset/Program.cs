using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

using GetAPISunset.Services;
using GetAPISunset.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

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

            double daysUntil = dayEnd.Subtract(dayStart).TotalDays + 1;

            Console.WriteLine($"Från {dayStart:D} fram till {dayEnd:D} är det {daysUntil} dagar\n");
            Console.WriteLine($"valt Latitude: {latitude} och Valt Longitude: {longitude}\n");

            for (int i = 0; i < daysUntil; i++)
            {
                // Kolla om datum vid lat/long existerar redan
                var existingDate = _db.SunTime.Select(d => d)
                    .Where(d => d.Latitude == latitude
                    && d.Longitude == longitude
                    && d.DagenDetGaller == wert.ToString()
                    ).FirstOrDefault();

                if (existingDate is not null)
                {
                    Console.WriteLine($"Datumet {existingDate.DagenDetGaller} för {existingDate.Latitude}, {existingDate.Longitude} finns redan i databasen.");
                }

                else
                {
                    var result = await _client.GetDayAsync(wert.ToString(), latitude, longitude);
                    _client.PrintDayDetails(result);
                    _db.SunTime.Add(result);

                    Console.WriteLine($"Datumet {result.DagenDetGaller} för {result.Latitude}, {result.Longitude} sparades i databasen.");
                }

                wert = wert.AddDays(1);
            }

            _db.SaveChanges();
        }
    }
}