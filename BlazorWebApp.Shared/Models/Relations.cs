using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWebApp.Shared.Models
{
    public class Relation
    {
        public Guid Id { get; set; }

        public Guid PawnId { get; set; }
        public Pawn Pawn { get; set; }
        public RelationType RelationType { get; set; }

        public Guid RelationPawnId { get; set; }
        public Pawn RelationPawn { get; set; }

        public Relation()
        {

        }
    }

    public enum RelationType
    {
        Father = 0,
        Mother = 1,
        Child =2,
        Frend = 3,
        Lover = 4,
        Brother = 5,
        Sister = 6,
        FosterFather = 7,
        BestFrend =8,
        Ex = 9,
        Spouse = 10
    }
}
