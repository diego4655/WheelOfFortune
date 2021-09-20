using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    //Model to finish the bet
    public class CloseBet
    {
        public string id { get; set; }
        public string number { get; set; }
        public string color { get; set; }
        public string value { get; set; }
        public string BetState { get; set; }
        public string BetValuePay { get; set; }

    }
}
