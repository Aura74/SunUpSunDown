﻿using GetAPISunset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GetAPISunset.Services
{
    public class SunriseClient
    {
        public async Task<SunriseItem> GetDayAsync(string SunDate, double lat, double lon)
        {
            var _client = new HttpClient();
            var isDST = DateTime.Parse(SunDate).IsDaylightSavingTime();

            string uri = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}&date={SunDate}";
            var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            Root wd = await response.Content.ReadFromJsonAsync<Root>();

            string dateInputUp = wd.results.sunrise;//tid
            string dateInputDown = wd.results.sunset;//tid
            DateTime Up = DateTime.Parse(dateInputUp);//tid
            DateTime Down = DateTime.Parse(dateInputDown);//tid
            DateTime changedTimeUp;//tid
            DateTime changedTimeDown;//tid

            if (!isDST)
            {
                changedTimeUp = Up.AddHours(1);//tid
                changedTimeDown = Down.AddHours(1);//tid
            }
            else
            {
                changedTimeUp = Up.AddHours(2);//tid
                changedTimeDown = Down.AddHours(2);//tid
            }

            var sunUpOrDownTime = new SunriseItem
            {
                sunrise = $"{changedTimeUp.ToString("HH:mm")}",
                sunset = $"{changedTimeDown.ToString("HH:mm")}",
                DagenDetGaller = SunDate,
                OriginalSunrise = Up.ToString("HH:mm"),
                OriginalSunset = Down.ToString("HH:mm"),
                SummerWinter = isDST,
                Latitude = lat,
                Longitude = lon
            };

            return sunUpOrDownTime;
        }
    }
}