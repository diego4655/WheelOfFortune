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
        public WheelModel Get()
        {
            return _context.Wheel.OrderBy(w => w.Id).Last();
        }

        // POST api/<NewWheelOfFortune>
        [HttpPost]
        public ActionResult Post(WheelModel data)
        {
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
    }
}
