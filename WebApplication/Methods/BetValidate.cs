using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Methods
{
    public class BetValidate : BetValidateInterface
    {
        public WheelModel MakeWheelModel(List<WheelModel> data)
        {
            //create new object
            WheelModel returnModel = null;
            //Fill the object
            for (int i = 0; i < data.Count; i++) { 
            returnModel = new WheelModel
            {
                Id = data[i].Id,
                status = data[i].status,
                WinColor = data[i].WinColor,
                WinNumber = data[i].WinNumber
                };
                return returnModel;
            }
            //return the object empty if no have data entry
            return returnModel;
        }
        public BetModel MakeBetModel(int i,List<BetModel> data)
        {
            //Create and fill the model
            BetModel returnModel = new BetModel { 
                    IdBet = data[i].IdBet, 
                    IdWheel = data[i].IdWheel, 
                    Type = data[i].Type, 
                    Value = data[i].Value, 
                    Color = data[i].Color, 
                    BetCash = data[i].BetCash, 
                    BetWin = data[i].BetWin };
            return returnModel;
        }
        public bool ValidateBetValue(BetModel data)
        {
            //verify if the value of the bet
            if (data.BetCash <= 10000)
            {
                return true;
            }
            return false;
        }
        public bool ValidateColor(BetModel data)
        {
            //verify the number and classify 
            string verifyNumber = (data.Value % 2) == 0 ? "Par" : "Impar";
            //verify the rules of the bet
            if (data.Color.Equals("Rojo") && verifyNumber.Equals("Par"))
            {
                return true;
            }
            else if (data.Color.Equals("Negro") && verifyNumber.Equals("Impar"))
            {
                return true;
            }
            return false;
        }
        public bool ValidateNumber(BetModel data)
        {
            //verify range of the numbers
            if (data.Value >= 0 && data.Value <= 36)
            {
                return true;
            }
            return false;
        }

        public int ValidateWinColor(BetModel data, WheelModel wheel)
        {
            //calculate the bet win if is for color
            double price = 0;
            if (data.Color.Equals(wheel.WinColor)) {
                price = data.BetCash * 1.8;
            }
            return data.BetWin = (int)price;
        }

        public int ValidateWinNumber(BetModel data, WheelModel wheel)
        {
            //calculate the bet win if is for number
            data.BetWin = 0;
            if (data.Value == wheel.WinNumber)
            {
                data.BetWin = (data.BetCash * 5);
            }
            return data.BetWin;
        }
    }
}
