using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChageStateBetController : ControllerBase
    {
        private readonly IDatabase _database;

        public ChageStateBetController(IDatabase database)
        {
            //database redis connection
            _database = database;
        }

        [HttpGet]
        public string Get([FromQuery]int id)
        {
            string idBet = _database.StringGet("Id");
            if (Int32.Parse(idBet) == id) {
                return "Exitosa";
            }

            return "Denegada";

        }

       
    }
}
