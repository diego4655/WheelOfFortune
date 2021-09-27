using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using WebApplication.Context;
using WebApplication.Interfaces;
using WebApplication.Methods;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewWheelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NewWheelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<WheelModel> Get()
        {
            //get list of wheels
            return _context.Wheel.ToList();
        }

        [HttpGet("{last}")]
        public WheelModel Get(string last) {
            //get the last wheel
            return _context.Wheel.OrderBy(w => w.Id).Last();
        }

            // POST api/<NewWheelOfFortune>
        [HttpPost]
        public ActionResult Post(WheelModel data)
        {
            //create new wheel
            try
            {
                _context.Wheel.Add(data);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception e) {
                return BadRequest();
            }   
        }

        // PUT api/<BetValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] WheelModel data)
        {
            WheelValidate val = new WheelValidate();
            //calculate the win color
            string calculateColor = val.CalculateColor();
            //calculate the win number
            int calculateNumber = val.CalculateNumber(calculateColor);
            //fill the object
            data = new WheelModel { Id = id, status =data.status , WinNumber = calculateNumber, WinColor = calculateColor };
            //update the wheel values
            if (data.Id == id)
            {
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
            else {
                return BadRequest();
            }
        }
    }
}
