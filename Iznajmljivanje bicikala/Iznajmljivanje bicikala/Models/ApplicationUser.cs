using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Iznajmljivanje_bicikala.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<UserBicycle> UserBicycles { get; set; }

        [Display(Name = "Korisnik")]
        public override string UserName { get => base.UserName; set => base.UserName = value; }
    }
}