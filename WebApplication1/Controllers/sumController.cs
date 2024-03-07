using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SumController : ControllerBase
    {

        SumService sumService;
        Sum s=new Sum();
        public SumController(SumService sumService, IHttpContextAccessor httpContextAccessor)
        {
            this.sumService = sumService;
        }

        [HttpGet]
        public double GetAll() =>
            sumService.Get();


        [HttpPost]
        public Sum Create([FromBody] Sum sum)
        {
            System.Console.WriteLine(sum);
            sumService.Add(sum);
            return s;
        }


        [HttpDelete("{sum}")]
        public Sum Delete(double sum)
        {
            sumService.Delete(sum);
            return s;
        }
    }
}
