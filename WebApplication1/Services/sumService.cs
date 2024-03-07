
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
    public class SumService
    {
        // double allSum { get; }



        private Sum allSum = new Sum();
        // private double allSum;
        // public double _allSum
        // {
        //     get { return allSum; }
        //     private set { allSum = value; }
        // }

        private IWebHostEnvironment webHost;
        private string filePath;
        public SumService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "sum.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                allSum = JsonSerializer.Deserialize<Sum>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(allSum));
        }

        public double Get()
        {
            return allSum.sum;
        }


        public void Add(Sum sum)
        {
            System.Console.WriteLine(sum + " " + allSum);
            allSum.sum += sum.sum;
            saveToFile();

        }
        public void Delete(double sum)
        {
            allSum.sum -= sum;
            saveToFile();

        }
    }
}
