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
            DateOnly wert = new DateOnly(2022, 10, 29);

            DateOnly dateOnly = wert;
            DateTime testDateTime = dateOnly.ToDateTime(TimeOnly.Parse("00:00 AM"));
            
            DateTime dayStart = testDateTime;
            DateTime dayEnd = new DateTime(2022, 10, 31);
            double daysUntil = dayEnd.Subtract(dayStart).TotalDays;
            Console.WriteLine($"Från {dayStart.ToString("D")} fram till {dayEnd.ToString("D")} är det {daysUntil + 1} dagar\n");

            for (int i = 0; i < daysUntil+1; i++)
            //for (int i = 0; i < 1; i++)
            {
                var IsDaylightSavingTime = DateTime.Parse(wert.ToString()).IsDaylightSavingTime();
                
                var result = await GetWebApiLongLatAsync(wert.AddDays(0).ToString());
                
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
                    Console.WriteLine($"Datumet {wert} är det vintertid");
                else
                    Console.WriteLine($"Datumet {wert} är det sommartid");


                //string Time = dt.ToString("HH:mm:ss:tt"); – 

                //string t1 = dt.ToString("H:mm");

                //dt.ToString("HH:mm"); // 07:00 // 24 hour clock // hour is always 2 digits
                //dt.ToString("hh:mm tt"); // 07:00 AM // 12 hour clock // hour is always 2 digits
                //dt.ToString("H:mm"); // 7:00 // 24 hour clock
                //dt.ToString("h:mm tt"); // 7:00 AM // 12 hour clock

                Console.WriteLine($"Original tid från API:et för soluppgång är {Up.ToString("HH:mm")}");
                Console.WriteLine($"Original tid från API:et för Solnedgång är {Down.ToString("HH:mm")}\n");

                Console.WriteLine($"Ändrad tid för soluppgång är {changedTimeUp.ToString("HH:mm")}");
                Console.WriteLine($"Ändrad tid för Solnedgång är {changedTimeDown.ToString("HH:mm")}\n");

                ApplicationDbContext db = new ApplicationDbContext();
                Results[] sunUpOrDownTime = new Results[]
                {
                    new Results(){sunrise = $"{changedTimeUp.ToString("HH:mm")}", sunset = $"{changedTimeDown.ToString("HH:mm")}", DagenDetGaller = wert.ToString(), OriginalSunrise = Up.ToString("HH:mm"), OriginalSunset = Down.ToString("HH:mm"), SummerWinter = IsDaylightSavingTime},
                };
                db.SunTime.AddRange(sunUpOrDownTime);
                db.SaveChanges();
                wert = wert.AddDays(1);
            }//for
        }//Main
    }//Program
}//namespace