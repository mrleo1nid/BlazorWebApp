using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using BlazorWebApp.Shared.Auth;
using BlazorWebApp.Shared.NameGeneration;

namespace BlazorWebApp.Shared.Models
{
    public class Pawn
    { 
        public Guid Id { get; set; }
        public bool IsLive { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FirstSurname { get; set; }
        public string Patronymic { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public Resides Resides { get; set; }
        public SexualOrientation Orientation { get; set; }
        public BloodType BloodType { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime DateOfDeath { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        [IgnoreDataMember]
        public string FullName { get { return $"{Name} {Patronymic} {Surname}";}}

        [IgnoreDataMember]
        public string SexToString { get
        {
            if (Sex == Sex.Female) return "Ж";
            else return "М";
        } }

        public List<Relation> Relations { get; set; }
        public List<Relation> OthenRelations { get; set; }
        public List<CharacterTrait> Traits { get; set; }
        public Pawn()
        {
            Traits = new List<CharacterTrait>();
            IsLive = true;
            Relations = new List<Relation>();
            OthenRelations = new List<Relation>();
        }

    }

    public enum Sex
    {
        [Description("Мужчина")]
        Male =1,
        [Description("Женщина")]
        Female =2
    }

    public enum Resides
    {
        [Description("Город")]
        City =1,
        [Description("Деревня")]
        Village =2,
    }

    public enum BloodType
    {
        [Description("O+")]
        OPositive = 0,
        [Description("A+")]
        APositive = 1,
        [Description("B+")]
        BPositive = 2,
        [Description("AB+")]
        ABPositive = 3,
        [Description("O-")]
        ONegative = 4,
        [Description("A-")]
        ANegative = 5,
        [Description("B-")]
        BNegative = 6,
        [Description("AB-")]
        ABNegative = 7
    }
    public enum SexualOrientation
    {
        [Description("Гетеро")]
        Heterosexuality = 1,
        [Description("Би")]
        Bisexuality = 2,
        [Description("Гомо")]
        Homosexuality = 3,
        [Description("Асексуал")]
        Asexuality = 4
    }
}
