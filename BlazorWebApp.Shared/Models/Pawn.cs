using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using BlazorWebApp.Shared.Auth;

namespace BlazorWebApp.Shared.Models
{
    public class Pawn
    { 
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }


        [IgnoreDataMember]
        public string FullName { get { return $"{Surname} {Name} {Patronymic}";}}

    }

    public enum Sex
    {
        Male=1,
        Female=2
    }
}
