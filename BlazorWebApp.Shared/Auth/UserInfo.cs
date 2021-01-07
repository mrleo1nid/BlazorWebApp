using System;
using System.Collections.Generic;
using System.Text;
using BlazorWebApp.Shared.Models;

namespace BlazorWebApp.Shared.Auth
{
    public class UserInfo
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public DateTime RegisterDateTime;
        public Dictionary<string, string> ExposedClaims { get; set; }
    }
}
