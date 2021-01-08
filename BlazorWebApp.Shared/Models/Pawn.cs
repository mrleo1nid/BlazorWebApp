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

        [IgnoreDataMember]
        public string SexToString { get
        {
            if (Sex == Sex.Female) return "Ж";
            else return "М";
        } }

    }

    public enum Sex
    {
        Male=1,
        Female=2
    }

    public enum Resides
    {
        Sity =1,
        Village =2,
    }
}
