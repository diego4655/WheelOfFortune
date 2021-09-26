using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class BetModel
    {
        [Key]
        public int Id { get; set; }
        public int IdWheel { get; set; }
        public int user { get; set; }
        public string Type { get; set; }
        public double value { get; set; }
        public double Win { get; set; }
    }
}
