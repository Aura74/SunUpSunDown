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
            int totalDays = 0;

            Console.WriteLine("Hur många dagar ska hämtas?");
            var tmp = Console.ReadLine();
            
            if (int.TryParse(tmp, out totalDays))
                totalDays -= 1;

            DateOnly currentDay = new DateOnly(2022, 10, 29);
            DateTime dateStart = DateTime.Parse(currentDay.ToString());
            DateTime dateEnd = dateStart.AddDays(totalDays);
            double daysUntil = dateEnd.Subtract(dateStart).TotalDays + 1;

            Console.WriteLine($"Från {dateStart:D} fram till {dateEnd:D} är det {daysUntil} dagar\n");

            for (int i = 0; i < daysUntil; i++)
            {
                // Kolla om datum vid lat/long existerar redan
                var existingDate = _db.SunTime.Select(d => d)
                    .Where(d => d.Latitude == latitude
                    && d.Longitude == longitude
                    && d.Datum == currentDay.ToDateTime(TimeOnly.Parse("00:00"))
                    ).FirstOrDefault();

                if (existingDate is not null)
                {
                    Console.WriteLine($"Ersätter {currentDay}..");
                    _db.SunTime.Remove(existingDate);
                }

                var result = await _client.GetDayAsync(currentDay.ToString(), latitude, longitude);
                _db.SunTime.Add(result);
                Console.WriteLine($"{result.Datum} sparas till databas.");

                currentDay = currentDay.AddDays(1);
            }

            _db.SaveChanges();
        }
    }
}