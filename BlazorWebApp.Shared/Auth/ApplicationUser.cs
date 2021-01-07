using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebApp.Shared.Models;

namespace BlazorWebApp.Shared.Auth
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime RegisterDateTime { get; set; }

        public List<Pawn> Pawns { get; set; }

        public ApplicationUser()
        {
            Pawns = new List<Pawn>();
        }
    }
}
