using System;
using System.Collections.Generic;
using System.Text;
using BlazorWebApp.Shared.Models;

namespace BlazorWebApp.Shared.Auth
{
    public class UserInfoExtended : UserInfo
    {
        public List<Pawn> Pawns;
        public UserInfoExtended()
        {
            Pawns = new List<Pawn>();
        }
    }
}
