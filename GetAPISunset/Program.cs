using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using GetAPISunset.Data;

namespace GetAPISunset
{
    class Program
    {
        public static async Task<Root> GetWebApiLongLatAsync(string SunDate)
        {
            HttpClient httpClient = new HttpClient();

            var latitude = 60.67452;
            var longitude = 17.14174;
            var uri = $"https://api.sunrise-sunset.org/json?lat={latitude}&lng={longitude}&date={SunDate}";

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            Root wd = await response.Content.ReadFromJsonAsync<Root>();
            return wd;
        }

        static async Task Main(string[] args)
        {
            // Skriv i Startdatum
            DateOnly wert = new DateOnly(2022, 7, 15);

            DateOnly dateOnly = wert;
            DateTime testDateTime = dateOnly.ToDateTime(TimeOnly.Parse("00:00 AM"));
            Console.WriteLine(testDateTime);

            DateTime dayStart = testDateTime;
            DateTime dayEnd = new DateTime(2022, 7, 17);
            double daysUntil = dayEnd.Subtract(dayStart).TotalDays;
            Console.WriteLine($"Det är {daysUntil} dagar");

            for (int i = 0; i < daysUntil; i++)
            //for (int i = 0; i < 1; i++)
            {
                var IsDaylightSavingTime = DateTime.Parse(wert.ToString()).IsDaylightSavingTime();
                wert = wert.AddDays(1);
                
                var result = await GetWebApiLongLatAsync(wert.ToString());
                
                string dateInputUp = result.results.sunrise;
                string dateInputDown = result.results.sunset;
                DateTime Up = DateTime.Parse(dateInputUp);
                DateTime Down = DateTime.Parse(dateInputDown);

                DateTime changedTimeUp;
                DateTime changedTimeDown;

                if (IsDaylightSavingTime == false)
                { 
                    changedTimeUp = Up.AddHours(1);
                    changedTimeDown = Down.AddHours(1);
                }
                else {
                    changedTimeUp = Up.AddHours(2);
                    changedTimeDown = Down.AddHours(2);
                }

                if (IsDaylightSavingTime == false)
                    Console.WriteLine("Det är nu vintertid");
                else
                    Console.WriteLine("Det är nu sommartid");
                
                Console.WriteLine($"Original från API Sunrise: {Up}");
                Console.WriteLine($"Original från API Sunset: {Down}\n");

                Console.WriteLine($"Ändrad tid Sunrise: {changedTimeUp}");
                Console.WriteLine($"Ändrad tid Sunset: {changedTimeDown}\n");

                ApplicationDbContext db = new ApplicationDbContext();
                Results[] sunUpOrDownTime = new Results[]
                {
                    new Results(){sunrise = $"{changedTimeUp}", sunset = $"{changedTimeDown}", DagenDetGaller = wert.ToString(), OriginalSunrise = Up.ToString(), OriginalSunset = Down.ToString(), SummerWinter = IsDaylightSavingTime},
                };
                db.SunTime.AddRange(sunUpOrDownTime);
                db.SaveChanges();
            }//for
        }//Main
    }//Program
}//namespace