using EntityFramework.BulkInsert.Extensions;
using Newtonsoft.Json;
using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using PhoneBookMVC.Services.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Transactions;

namespace PhoneBookMVC.App_Start
{
    internal class LocationConfiguration
    {
        internal static void Configure()
        {
            Dictionary<string, string[]> locations = new Dictionary<string, string[]>();

            using (WebClient webClient = new WebClient())
            {
                string jsonString = webClient.DownloadString("https://raw.githubusercontent.com/David-Haim/CountriesToCitiesJSON/master/countriesToCities.json");
                locations = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(jsonString);
            }

            CountriesService countriesService = new CountriesService();
            List<Country> countries = new List<Country>();

            foreach (var item in locations)
            {
                Country country = new Country();
                country.Name = item.Key;
                countries.Add(country);
            }

            countriesService.InsertCollection(countries);

            List<Country> allCountries = countriesService.GetAll();
            List<City> cities = new List<City>();

            foreach (var item in locations)
            {
                foreach (Country c in allCountries)
                {
                    if (c.Name == item.Key)
                    {
                        cities.AddRange(item.Value.Select(city => new City() { Name = city, CountryID = c.ID }));
                    }
                }
            }

            CitiesService citieService = new CitiesService();
            citieService.InsertCollection(cities);
        }
    }
}