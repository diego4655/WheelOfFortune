using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetEndingController : ControllerBase
    {
        private readonly IDatabase _database;

        public BetEndingController(IDatabase database)
        {
            _database = database;
        }


        public Dictionary<int, string> ResultBet() {
            //method to random number
            Random r = new Random();
            //Instance dictionary to return
            Dictionary<int, string> valuesReturn = new Dictionary<int, string>();
            //Calc random number
            int WinNumber = r.Next(0, 37);
            //Create and calc wincolor
            string WinColor;
            if (WinNumber % 2 == 0)
            {
                WinColor = "Rojo";
            }
            else {
                WinColor = "Negro";
            }
            //add values to dictionary
            valuesReturn.Add(WinNumber, WinColor);
            return valuesReturn;
        }


        // GET api/<BetEndingController>/5
        [HttpGet]
        public IEnumerable<String> Get()
        {
            //get last id from redis
            string Id = _database.StringGet("Id");
            //get values of result bet
            Dictionary<int, string> WinValues = ResultBet();
            //get values from database
            HashEntry[] showReturn = _database.HashGetAll(Id);
            //list to get all values of showReturn
            List<string> seeValues = new List<string>();
            for (int i = 0; i < showReturn.Length; i++)
            {
                seeValues.Add(showReturn[i].ToString());
                
            }
            //variables to get information of foreach
            int position;    
            string NumForeach =null;
            string ColForeach = null;
            string ValForeach = null;
            int PayValueNum = 0;
            double PayValueCol = 0;
            foreach (var entry in WinValues) {
                foreach (var outputs in seeValues) {
                    position = outputs.IndexOf(": ");
                    
                    if (outputs.Contains("Numero:")) {
                        NumForeach = outputs.Substring(position + 1);
                    }
                    if (outputs.Contains("Color:")) {
                        ColForeach = outputs.Substring(position + 1);
                    }
                    if (outputs.Contains("Valor:")) {
                        ValForeach = outputs.Substring(position + 1);
                    }
                }
                if (int.Parse(NumForeach) == entry.Key){
                    PayValueNum = Int32.Parse(ValForeach) * 5;                   
                }else if (ColForeach.Equals(entry.Value)) {
                    PayValueCol = double.Parse(ValForeach) * 1.8;                   
                } 
            }
            //get and fill the object
            CloseBet clBet;
            if (PayValueNum != 0) {
               clBet = new CloseBet { id = Id,number = NumForeach,color = ColForeach,value = ValForeach, BetState = "Cerrado", BetValuePay = PayValueNum.ToString() };
            } else if (PayValueCol != 0) {
                clBet = new CloseBet { id = Id, number = NumForeach, color = ColForeach, value = ValForeach, BetState = "Cerrado", BetValuePay = PayValueCol.ToString() };
            }else {
                clBet = new CloseBet { id = Id, number = NumForeach, color = ColForeach, value = ValForeach, BetState = "Cerrado", BetValuePay = "0" };
            }
            //send information
            Post(clBet);
            //get values from database to return
            HashEntry[] finalReturn = _database.HashGetAll(Id);
            string allValues;
            for (int i = 0; i < finalReturn.Length; i++)
            {
                allValues = finalReturn[i].ToString();
                yield return allValues;
            }
        }

        // POST api/<BetValuesController>
        [HttpPost]
        public void Post(CloseBet data)
        {
            _database.HashSet(data.id, new HashEntry[] { new HashEntry("Numero", data.number), new HashEntry("Color", data.color), new HashEntry("Valor", data.value), new HashEntry("Estado",data.BetState), new HashEntry("Premio",data.BetValuePay) });
        }

    }
}
