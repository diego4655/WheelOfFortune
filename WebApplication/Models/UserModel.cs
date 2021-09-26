using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Login{ get; set; }
        public string password { get; set; }
        public double BetCapital { get; set; }

    }
}
