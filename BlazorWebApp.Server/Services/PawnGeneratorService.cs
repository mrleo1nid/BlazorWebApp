using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebApp.Server.Data;
using BlazorWebApp.Shared.Helpers;
using BlazorWebApp.Shared.Models;

namespace BlazorWebApp.Server.Services
{
    public class PawnGeneratorService
    {
        private Random random;
        private NamesDbContext _context;
        public PawnGeneratorService(NamesDbContext context)
        {
            random = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
            _context = context;
        }

        public async Task<Pawn> GenerateRandomPawn()
        {
            Pawn pawn = new Pawn();
            pawn.Sex = (Sex) random.Next(1);
            pawn = CreatePawnName(pawn);
            pawn.Age = random.Next(14,70);
            return pawn;
        }

        private Pawn CreatePawnName(Pawn pawn)
        {
            string name = string.Empty;
            if (pawn.Sex==Sex.Male)
            {
                var namestr = _context.NameStrings.Where(x => x.Sex.Trim() == "М")
                    .OrderBy(s => random.NextDouble())
                    .First();
                pawn.Name = namestr.Name;
                var surnamestr = _context.SurnameStrings.Where(x => x.Sex.Trim() == "М")
                    .OrderBy(s => random.NextDouble())
                        .First();
                pawn.Surname = surnamestr.Surname;
                var patrstr = _context.PatronimicStrings.Where(x => x.Sex == Sex.Male)
                    .OrderBy(s => random.NextDouble())
                    .First();
                pawn.Patronymic = patrstr.Patronimic;

            }
            else
            {
                var namestr = _context.NameStrings.Where(x => x.Sex.Trim() == "Ж")
                        .OrderBy(s => random.NextDouble())
                        .First();
                pawn.Name = namestr.Name;
                var surnamestr = _context.SurnameStrings.Where(x => x.Sex.Trim() == "Ж")
                        .OrderBy(s => random.NextDouble())
                        .First();
                pawn.Surname = surnamestr.Surname;
                var patrstr = _context.PatronimicStrings.Where(x => x.Sex == Sex.Female)
                    .OrderBy(s => random.NextDouble())
                    .First();
                pawn.Patronymic = patrstr.Patronimic;
            }
            return pawn;
        }
    }
}
