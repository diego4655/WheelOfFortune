using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    //model to first point that create a new value for bet
    public class WheelModel
    {   
        [Key]
        public int Id { get; set; }
        public string status { get; set; }
    }
}
