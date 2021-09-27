using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Interfaces
{
    interface BetValidateInterface
    {
        public bool ValidateNumber(BetModel data);
        public bool ValidateColor(BetModel data);
        public bool ValidateBetValue(BetModel data);
        public int ValidateWinNumber(BetModel data,WheelModel wheel);
        public int ValidateWinColor(BetModel data, WheelModel wheel);
        public BetModel MakeBetModel(int i,List<BetModel> data);
        public WheelModel MakeWheelModel(List<WheelModel> data);
    }
}
