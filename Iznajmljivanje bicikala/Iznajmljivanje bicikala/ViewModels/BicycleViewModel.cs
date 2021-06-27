using Iznajmljivanje_bicikala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iznajmljivanje_bicikala.ViewModels
{
    public class BicycleViewModel
    {
        public List <Bicycle> Bicycles { get; set; }

        public Bicycle Bike { get; set; }
    }
}
