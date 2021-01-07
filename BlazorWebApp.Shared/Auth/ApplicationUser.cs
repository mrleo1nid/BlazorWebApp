using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebApp.Shared.Auth
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime RegisterDateTime { get; set; }
    }
}
