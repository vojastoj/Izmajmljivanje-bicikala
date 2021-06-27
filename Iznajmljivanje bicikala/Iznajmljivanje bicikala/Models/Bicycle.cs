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
        public string Brand { get; set; }
        
        [Required]
        public int ProductionYear { get; set; }

        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
