using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Iznajmljivanje_bicikala.Models
{
    public class Bicycle
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Marka")]
        public string Brand { get; set; }

        [Required]
        [Display(Name = "Godina proizvodnje")]
        public int ProductionYear { get; set; }

        public List<UserBicycle> UserBicycles { get; set; }
    }
}