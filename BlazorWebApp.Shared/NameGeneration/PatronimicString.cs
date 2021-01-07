using System;
using System.Collections.Generic;
using System.Text;
using BlazorWebApp.Shared.Models;

namespace BlazorWebApp.Shared.NameGeneration
{
    public class PatronimicString
    {
        public int Id { get; set; }
        public Sex Sex { get; set; }
        public string Patronimic { get; set; }
        public int NameStringId { get; set; }
        public NameString NameString { get; set; }
    }
}
