using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetValuesController : ControllerBase
    {
        private readonly IDatabase _database;

        public BetValuesController(IDatabase database)
        {
            _database = database;
        }

    
        // GET api/<BetValuesController>/
        [HttpGet]
        public IEnumerable<string> Get([FromQuery] string number, string color, string value)
        {
            string Id = _database.StringGet("Id");
            BetValuesUser data = new BetValuesUser { id = Id, number = number, color = color, value = value };
            string colorCompare = data.color;
            //check the maximun number to wheel
            if (Int32.Parse(data.number) > 36) {
                yield return "Revisar Numero";
                yield break;
            }            
            //check the maximun value to bet
            if (Int32.Parse(data.value) > 10000) {
                yield return "Supera el monto maximo a apostar";
                yield break;
            }
            //check the colors of the wheel
            if (colorCompare.Equals("Negro") || colorCompare.Equals("Rojo")){
                Post(data);
            }else{
                yield return "Revise el color";
                yield break;
            }
            
            HashEntry[] showReturn = _database.HashGetAll(data.id);
            string seeValues;

            for (int i = 0; i < showReturn.Length; i++) {
                seeValues = showReturn[i].ToString();
                yield return seeValues;
            }            
        }

        // POST api/<BetValuesController>
        [HttpPost]
        public void Post(BetValuesUser data)
        {
            _database.HashSet(data.id, new HashEntry[] { new HashEntry("Numero", data.number), new HashEntry("Color",data.color), new HashEntry("Valor",data.value) });
        }
    
    }
}
