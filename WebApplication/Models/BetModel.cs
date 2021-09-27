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
        public int IdBet { get; set; }
        public int IdWheel { get; set; }        
        public string Type { get; set; }
        public int Value { get; set; }
        public string Color { get; set; }
        public int BetCash { get; set; }
        public int BetWin { get; set; }
    }
}
