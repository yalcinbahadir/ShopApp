using Microsoft.AspNetCore.Identity;
using ShopAppTekrar.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopAppTekrar.Models
{
    public class RoleDetails
    {
        public IdentityRole Role { get; set; }
        public List<User> Members { get; set; }
        public List<User> NonMembers { get; set; }
        public RoleDetails()
        {
            Members = new List<User>();
            NonMembers = new List<User>();
        }
    }
}
