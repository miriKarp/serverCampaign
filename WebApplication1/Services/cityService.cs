
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Server.Models;


namespace Server.Services
{
    public class CityService
    {

        List<City> cities { get; }



        private IWebHostEnvironment webHost;
        private string filePath;
        public CityService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "city.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                cities = JsonSerializer.Deserialize<List<City>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(cities));
        }

        public List<City> GetAll()
        {
            return cities;
        }

        public City Get(string name)
        {
            return cities.FirstOrDefault(c => c.name == name);
        }

        public void Add(City city)
        {
            cities.Add(city);
            saveToFile();

        }
        public void Delete(string name)
        {
            City city = Get(name);
            if (city is null)
                return;
            cities.Remove(city);
            saveToFile();

        }
        public void Update(City city)
        {
            int index = cities.FindIndex(c => c.name == city.name);
            if (index == -1)
                return;
            cities[index].sum += city.sum;
            System.Console.WriteLine((DateTime)city.time);
            cities[index].time = (DateTime)city.time;
            saveToFile();
        }

        // public int Count(long userId)
        // {
        //     return GetAll().Count();
        // }
    }
}
