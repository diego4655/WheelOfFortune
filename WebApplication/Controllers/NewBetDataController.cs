using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewBetDataController : ControllerBase
    {
        
        private readonly IDatabase _database;
        

        public NewBetDataController(IDatabase database)
        {
            //database redis connection
            _database = database;
        }

        [HttpGet]
        public string Get()
        {
            string key = _database.StringGet("Id");
            newBet data;
            if (key.Equals("") || key == null) {
                data = new newBet{ keyBet = "Id", valueBet = "1"  };
            }
            else { 
            int newKey = Int32.Parse(key) + 1;
                data = new newBet { keyBet = "Id", valueBet = newKey.ToString()}; 
            }
            string selectKey = Post(data);
            return selectKey;
        }

        // POST api/<NewWheelOfFortune>
        [HttpPost]
        public string Post(newBet data)
        {
            _database.StringSet(data.keyBet, data.valueBet);
            string actualKey = _database.StringGet("Id");
            return actualKey;
        }
               
    }
}
