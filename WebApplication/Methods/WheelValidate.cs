using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Interfaces;

namespace WebApplication.Methods
{
    public class WheelValidate : WheelInterface
    {
        private Random r = new Random();
        public string CalculateColor()
        {
            //calculate the color with random number
            int i = r.Next(0, 1);
            string[] colorArray = { "Negro", "Rojo" };
            string colorReturn;
            //get the color of the array to return
            colorReturn = colorArray[i];
            return colorReturn;
        }
        public int CalculateNumber(string calculateColor)
        {
            int returnNumber;
            int resultNumber = r.Next(0, 36);
            //verify if is even or odd
            string numberType = (resultNumber % 2) == 0 ? "Par" : "Impar";
            //check the rule for color and type and substract 1 to reach the rule
            if (calculateColor.Equals("Rojo") && numberType.Equals("Par")){
                returnNumber = resultNumber;
            }
            else { 
                returnNumber = (resultNumber - 1); 
            }
            //make the similar function of the another if but with the black color
            if (calculateColor.Equals("Negro") && numberType.Equals("Impar"))
            {
                returnNumber = resultNumber;
            }
            else
            {
                returnNumber = (resultNumber - 1);
            }

            return returnNumber;
        }
    }
}
