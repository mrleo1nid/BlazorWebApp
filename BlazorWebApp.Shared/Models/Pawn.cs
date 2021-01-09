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

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public Resides Resides { get; set; }
        public SexualOrientation Orientation { get; set; }
        public DateTime DateofBirth { get; set; }
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

        public List<CharacterTrait> Traits { get; set; }
        public Pawn()
        {
            Traits = new List<CharacterTrait>();
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
    public enum SexualOrientation
    {
        [Description("Гетеросексуальность")]
        Heterosexuality = 1,
        [Description("Бисексуальность")]
        Bisexuality = 2,
        [Description("Гомосексуальность")]
        Homosexuality = 3,
        [Description("Асексуальность")]
        Asexuality = 4
    }
}
