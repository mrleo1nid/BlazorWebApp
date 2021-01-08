using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebApp.Server.Data;
using BlazorWebApp.Shared.Helpers;
using BlazorWebApp.Shared.Models;
using BlazorWebApp.Shared.NameGeneration;

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
            pawn.Sex = RandomHelper.GetRandomSex(random);
            pawn = CreatePawnName(pawn);
            pawn.Age = random.Next(14,70);
            return pawn;
        }

        private Pawn CreatePawnName(Pawn pawn)
        {
            string name = string.Empty;
            if (pawn.Sex==Sex.Male)
            {
                var namestr = RandomHelper.GetRandomObj<NameString>(random,
                    _context.NameStrings.Where(x => x.Sex == "М").ToList());
                pawn.Name = namestr.Name;
                var surnamestr = RandomHelper.GetRandomObj<SurnameString>(random, _context.SurnameStrings.Where(x => x.Sex == "М").ToList());
                pawn.Surname = surnamestr.Surname;
                var patrstr = RandomHelper.GetRandomObj<PatronimicString>(random, _context.PatronimicStrings.Where(x => x.Sex == Sex.Male).ToList());
                pawn.Patronymic = patrstr.Patronimic;

            }
            else
            {
                var namestr = RandomHelper.GetRandomObj<NameString>(random,
                    _context.NameStrings.Where(x => x.Sex == "Ж").ToList());
                pawn.Name = namestr.Name;
                var surnamestr = RandomHelper.GetRandomObj<SurnameString>(random, _context.SurnameStrings.Where(x => x.Sex == "Ж").ToList());
                pawn.Surname = surnamestr.Surname;
                var patrstr = RandomHelper.GetRandomObj<PatronimicString>(random, _context.PatronimicStrings.Where(x => x.Sex == Sex.Female).ToList());
                pawn.Patronymic = patrstr.Patronimic;
            }
            return pawn;
        }
    }
}
