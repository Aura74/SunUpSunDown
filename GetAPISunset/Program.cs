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
            //DateTime dateEnd = new DateTime(2022, 10, 31);
            DateTime dateEnd = dateStart.AddDays(totalDays);

            Console.WriteLine($"Start: {dateStart.ToShortDateString()}, Slut: {dateEnd.ToShortDateString()}.");

            double daysUntil = dateEnd.Subtract(dateStart).TotalDays + 1;

            Console.WriteLine($"Från {dateStart:D} fram till {dateEnd:D} är det {daysUntil} dagar\n");
            Console.WriteLine($"valt Latitude: {latitude} och Valt Longitude: {longitude}\n");

            for (int i = 0; i < daysUntil; i++)
            {
                // Kolla om datum vid lat/long existerar redan
                if (_client.CheckExistingDate(currentDay.ToString(), latitude, longitude))
                {
                    Console.WriteLine($"Datumet {currentDay} för {latitude}, {longitude} finns redan i databasen.");
                }

                else
                {
                    var result = await _client.GetDayAsync(currentDay.ToString(), latitude, longitude);
                    _db.SunTime.Add(result);
                    Console.WriteLine($"{result.DagenDetGaller} sparas till databas.");

                    // Temporär print för att ge lite detaljer
                    _client.PrintDayDetails(result);
                }

                currentDay = currentDay.AddDays(1);
            }

            _db.SaveChanges();
        }
    }
}