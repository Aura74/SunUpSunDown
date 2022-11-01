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
        static double valtLatitude = 60.67452;
        static double valtLongitude = 17.14174;
        
        public static async Task<Root> GetWebApiLongLatAsync(string SunDate)
        {
            HttpClient httpClient = new HttpClient();
            double latitude = valtLatitude;
            double longitude = valtLongitude;
            string uri = $"https://api.sunrise-sunset.org/json?lat={latitude}&lng={longitude}&date={SunDate}";
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            Root wd = await response.Content.ReadFromJsonAsync<Root>();
            return wd;
        }

        static async Task Main(string[] args)
        {
            DateOnly wert = new DateOnly(2022, 10, 29);//Datum
            DateOnly dateOnly = wert;//Datum
            DateTime testDateTime = dateOnly.ToDateTime(TimeOnly.Parse("00:00 AM"));//Datum
            DateTime dayStart = testDateTime;//Datum
            DateTime dayEnd = new DateTime(2022, 10, 31);//Datum
            double daysUntil = dayEnd.Subtract(dayStart).TotalDays;//doubble
            Console.WriteLine($"Från {dayStart.ToString("D")} fram till {dayEnd.ToString("D")} är det {daysUntil + 1} dagar\n");
            Console.WriteLine($"valt Latitude: {valtLatitude} och Valt Longitude: {valtLongitude}\n");

            for (int i = 0; i < daysUntil+1; i++)
            {
                var IsDaylightSavingTime = DateTime.Parse(wert.ToString()).IsDaylightSavingTime();
                var ärDetSkottAr = DateTime.IsLeapYear(wert.Year);
                if (ärDetSkottAr == false)
                    Console.WriteLine($"Året {wert.Year} är inte skottår");
                else
                    Console.WriteLine($"Året {wert.Year} är ett skottår");

                var result = await GetWebApiLongLatAsync(wert.AddDays(0).ToString());
                
                string dateInputUp = result.results.sunrise;//tid
                string dateInputDown = result.results.sunset;//tid
                DateTime Up = DateTime.Parse(dateInputUp);//tid
                DateTime Down = DateTime.Parse(dateInputDown);//tid
                DateTime changedTimeUp;//tid
                DateTime changedTimeDown;//tid

                if (IsDaylightSavingTime == false)
                { 
                    changedTimeUp = Up.AddHours(1);//tid
                    changedTimeDown = Down.AddHours(1);//tid
                }
                else {
                    changedTimeUp = Up.AddHours(2);//tid
                    changedTimeDown = Down.AddHours(2);//tid
                }

                if (IsDaylightSavingTime == false)
                    Console.WriteLine($"Vid datumet {wert} är det vintertid");
                else
                    Console.WriteLine($"Vid datumet {wert} är det sommartid");

                Console.WriteLine($"Original tid från API:et för soluppgång är {Up.ToString("HH:mm")}");
                Console.WriteLine($"Original tid från API:et för Solnedgång är {Down.ToString("HH:mm")}\n");

                Console.WriteLine($"Ändrad tid för soluppgång är {changedTimeUp.ToString("HH:mm")}");
                Console.WriteLine($"Ändrad tid för Solnedgång är {changedTimeDown.ToString("HH:mm")}\n");

                ApplicationDbContext db = new ApplicationDbContext();
                Results[] sunUpOrDownTime = new Results[]
                {
                    new Results()
                    {
                        sunrise = $"{changedTimeUp.ToString("HH:mm")}", 
                        sunset = $"{changedTimeDown.ToString("HH:mm")}", 
                        DagenDetGaller = wert.ToString(), 
                        OriginalSunrise = Up.ToString("HH:mm"), 
                        OriginalSunset = Down.ToString("HH:mm"), 
                        SummerWinter = IsDaylightSavingTime, 
                        Latitude = valtLatitude, 
                        Longitude = valtLongitude},
                };
                db.SunTime.AddRange(sunUpOrDownTime);
                
                db.SaveChanges();
                wert = wert.AddDays(1);

                Console.WriteLine($"valt Latitude: {valtLatitude} och Valt Longitude: {valtLongitude}\n");
            }//for
        }//Main
    }//Program
}//namespace