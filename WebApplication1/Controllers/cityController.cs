using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {

        CityService cityService;
        public CityController(CityService cityService, IHttpContextAccessor httpContextAccessor)
        {
            this.cityService = cityService;
        }
        City c = new City();

        [HttpGet]
        public ActionResult<List<City>> GetAll() =>
            cityService.GetAll();

        [HttpGet("{name}")]
        public ActionResult<City> Get(string name)
        {
            City city = cityService.Get(name);
            if (city == null)
                return NotFound();
            return city;
        }
        [HttpPost]
        public City Create(City city)
        {
            cityService.Add(city);
            return c;
            // return CreatedAtAction(nameof(Create), new { name = city.name }, city);
        }
        // [HttpPost("{city}")]
        // public string Create(List<City>city)
        // {
        //     cityService.Add(city);
        //     return "added";
        //     // return CreatedAtAction(nameof(Create), new { name = city.name }, city);
        // }

        // PUT api/<BookController>/5
        [HttpPut("{name}")]
        public City Update(string name, City city)
        {
            if (name != city.name)
                // return BadRequest();
                return c;
            City existingCity = cityService.Get(name);
            if (existingCity is null)
                return c;
            cityService.Update(city);
            return c;
        }

        [HttpDelete("{name}")]
        public City Delete(string name)
        {
            City city = cityService.Get(name);
            if (city is null)
                return c;
            cityService.Delete(name);
            return c;
        }
    }
}
