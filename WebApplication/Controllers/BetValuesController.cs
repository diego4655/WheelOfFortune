using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Context;
using WebApplication.Methods;
using WebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetValuesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private BetValidate validate = new BetValidate();
       
        public BetValuesController(AppDbContext context)
        {
            _context = context;
        }   
  
        // GET api/<BetValuesController>/5
        [HttpGet("{id}")]
        public IEnumerable<BetModel> Get(int id)
        {
            //get the data from the bet table filter by idWheel 
            List<BetModel> listBet = _context.Bet.Where(b => b.IdWheel == id).ToList();
            //get the data from the wheel frilter by idWheel
            List<WheelModel> listWheel = _context.Wheel.Where(w => w.Id == id).ToList();            
            //Fill the object Wheel
            WheelModel getWheel = validate.MakeWheelModel(listWheel);            
            for (int i = 0; i < listBet.Count; i++)
            {
                //fill the object Bet
                BetModel getBet = validate.MakeBetModel(i,listBet);
                string type = getBet.Type;
                //verify the type of win and get the win value
                getBet.BetWin = type.Equals("Numero") ? validate.ValidateWinNumber(getBet,getWheel) : validate.ValidateWinColor(getBet,getWheel);
                //send the data to update
                Put(id,getBet);
            }            
            return _context.Bet.Where(b => b.IdWheel == id).ToList();
        }

        // POST api/<BetValuesController>
        [HttpPost]
        public ActionResult Post([FromBody] BetModel data)
        {            
            bool validationType;
            bool validationCash;
            //verify if the number and colour corresponds to the rules
            validationType = data.Type.Equals("Numero") ? validate.ValidateNumber(data) : validate.ValidateColor(data);            
            //verify the value of the bet
            validationCash = validate.ValidateBetValue(data);
            //insert data into the table
            if (validationType && validationCash)
            {
                try
                {
                    _context.Add(data);
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            else {
                return Content("Revise su apuesta");
            }

            
        }

        // PUT api/<BetValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] BetModel data)
        {
            //update data into the table
            if (data.IdWheel == id)
            {
                var bet = _context.Bet.Where(b => b.IdBet == data.IdBet).SingleOrDefault();
                bet.BetWin = data.BetWin;
                _context.SaveChanges();                
            }            
        }

      
    }
}
