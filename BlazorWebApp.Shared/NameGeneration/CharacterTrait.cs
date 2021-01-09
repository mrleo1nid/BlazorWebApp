using System;
using System.Collections.Generic;
using System.Text;
using BlazorWebApp.Shared.Models;

namespace BlazorWebApp.Shared.NameGeneration
{
    public class CharacterTrait
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pawn> Pawns { get; set; }
        public CharacterTrait()
        {
            Pawns = new List<Pawn>();
        }
    }
}
