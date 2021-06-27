using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iznajmljivanje_bicikala.Models
{
    public class UserBicycle
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int BicycleId { get; set; }
        public Bicycle Bicycle { get; set; }
        public bool IsBooked { get; set; }
    }
}