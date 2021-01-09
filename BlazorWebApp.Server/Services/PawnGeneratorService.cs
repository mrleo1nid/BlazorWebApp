using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebApp.Server.Data;
using BlazorWebApp.Shared.Helpers;
using BlazorWebApp.Shared.Models;
using BlazorWebApp.Shared.NameGeneration;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApp.Server.Services
{
    public class PawnGeneratorService
    {
        private Random random;
        private NamesDbContext _context;
        private RandomDateTime randomDateTime;
        private ApplicationDbContext _applicationDbContext;
        public PawnGeneratorService(NamesDbContext context, ApplicationDbContext appcontext)
        {
            random = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
            randomDateTime = new RandomDateTime();
            _context = context;
            _applicationDbContext = appcontext;
        }

        public async Task<Pawn> GenerateRandomPawn()
        {
            Pawn pawn = new Pawn();
            pawn.Sex = GenerateSex();
            pawn = await CreatePawnName(pawn);
            pawn = await CreatePawnSurName(pawn);
            pawn = await CreatePawnPatronim(pawn);
            pawn.Resides = GenerateResides();
            pawn.Orientation = GenerateSexualOrientation(pawn.Sex);
            pawn.Age = GenerateAge();
            pawn.DateofBirth = GenerateBirth(pawn.Age);
            pawn.Traits = await GenerateTraits();
            pawn.BloodType = GenerateBloodType();
            return pawn;
        }

        public async Task LowOldName(string name)
        {
          var namestr = await  _context.NameStrings.Where(x => x.Name == name).FirstOrDefaultAsync();
          namestr.PeoplesCount = random.Next(50, 300);
          _context.NameStrings.Update(namestr);
          await _context.SaveChangesAsync();
        }
        public async Task LowOldSurName(string surname)
        {
            var surnamestr = await _context.SurnameStrings.Where(x => x.Surname == surname).FirstOrDefaultAsync();
            surnamestr.PeoplesCount = random.Next(50, 300);
            _context.SurnameStrings.Update(surnamestr);
            await _context.SaveChangesAsync();
        }
        public async Task<Pawn> CreatePawnName(Pawn pawn)
        {
            var randListName = await _context.NameStrings
                .Where(x => x.Sex == pawn.SexToString)
                .ToArrayAsync();
            double[] doublearr = new double[randListName.Count()];
            int[] intarr = new int[randListName.Count()];
            for (int i = 0; i < randListName.Count(); i++)
            {
                intarr[i] = randListName[i].ID;
                if (randListName[i].PeoplesCount <= 10)
                {
                    doublearr[i] = random.Next(30, 150);
                }
                else
                {
                    doublearr[i] = randListName[i].PeoplesCount;
                }
            }
            var nameid = RandomHelper.GetRandomNumberFromArrayWithProbabilities(intarr, doublearr, random);
            pawn.Name = randListName.Where(x => x.ID == nameid).FirstOrDefault().Name;
            return pawn;
        }
        public async Task<Pawn> CreatePawnSurName(Pawn pawn)
        {
            var randListName = await _context.SurnameStrings
                .Where(x => x.Sex == pawn.SexToString)
                .ToArrayAsync();
            double[] doublearr = new double[randListName.Count()];
            int[] intarr = new int[randListName.Count()];
            for (int i = 0; i < randListName.Count(); i++)
            {
                intarr[i] = randListName[i].ID;
                if (randListName[i].PeoplesCount <= 10)
                {
                    doublearr[i] = random.Next(30, 150);
                }
                else
                {
                    doublearr[i] = randListName[i].PeoplesCount;
                }
            }
            var nameid = RandomHelper.GetRandomNumberFromArrayWithProbabilities(intarr, doublearr, random);
            pawn.Surname = randListName.Where(x => x.ID == nameid).FirstOrDefault().Surname;
            return pawn;
        }

        private async Task<Pawn> CreatePawnPatronim(Pawn pawn)
        {
            var randListName = await _context.PatronimicStrings
                .Where(x => x.Sex == pawn.Sex)
                .Include(x=>x.NameString)
                .ToArrayAsync();
            double[] doublearr = new double[randListName.Count()];
            int[] intarr = new int[randListName.Count()];
            for (int i = 0; i < randListName.Count(); i++)
            {
                intarr[i] = randListName[i].Id;
                if (randListName[i].NameString.PeoplesCount <= 10)
                {
                    doublearr[i] = random.Next(30, 150);
                }
                else
                {
                    doublearr[i] = randListName[i].NameString.PeoplesCount;
                }
            }
            var nameid = RandomHelper.GetRandomNumberFromArrayWithProbabilities(intarr, doublearr, random);
            pawn.Patronymic = randListName.Where(x => x.Id == nameid).FirstOrDefault().Patronimic;
            return pawn;
        }

        private int GenerateAge()
        {
            double[] doubles = new double[109];
            int[] ints = new int[109];
            for (int i = 1; i < 14; i++)
            {
                ints[i - 1] = i;
                doubles[i - 1] = 20;
            }
            for (int i = 14; i < 30; i++)
            {
                ints[i - 1] = i;
                doubles[i - 1] = 30;
            }
            for (int i = 30; i < 45; i++)
            {
                ints[i - 1] = i;
                doubles[i - 1] = 35;
            }
            for (int i = 45; i < 60; i++)
            {
                ints[i - 1] = i;
                doubles[i - 1] = 30;
            }
            for (int i = 60; i < 70; i++)
            {
                ints[i - 1] = i;
                doubles[i - 1] = 10;
            }
            for (int i = 70; i < 110; i++)
            {
                ints[i - 1] = i;
                doubles[i - 1] = 6;
            }
            return RandomHelper.GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
        }

        private Sex GenerateSex()
        {
            int[] ints = new int[2];
            double[] doubles = new double[2];
            ints[0] = 0;
            ints[1] = 1;
            doubles[0] = 46.3;
            doubles[1] = 53.7;
            var res = RandomHelper.GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
            if (res==0)
            {
                return Sex.Male;
            }
            else
            {
                return Sex.Female;
            }
        }
        private SexualOrientation GenerateSexualOrientation(Sex sex)
        {
            int[] ints = new int[4];
            double[] doubles = new double[4];
            if (sex == Sex.Female)
            {
                ints[0] = (int)SexualOrientation.Heterosexuality;
                ints[1] = (int)SexualOrientation.Homosexuality;
                ints[2] = (int)SexualOrientation.Bisexuality;
                ints[3] = (int)SexualOrientation.Asexuality;
                doubles[0] = 95.7;
                doubles[1] = 5;
                doubles[2] = 6;
                doubles[3] = 1;
            }
            else
            {
                ints[0] = (int)SexualOrientation.Heterosexuality;
                ints[1] = (int)SexualOrientation.Homosexuality;
                ints[2] = (int)SexualOrientation.Bisexuality;
                ints[3] = (int)SexualOrientation.Asexuality;
                doubles[0] = 95.4;
                doubles[1] = 6;
                doubles[2] = 4;
                doubles[3] = 1;
            }
           
            var res = RandomHelper.GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
            return (SexualOrientation) res;
        }
        private Resides GenerateResides()
        {
            int[] ints = new int[2];
            double[] doubles = new double[2];
            ints[0] = 0;
            ints[1] = 1;
            doubles[0] = 1187;
            doubles[1] = 1077;
            var res = RandomHelper.GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
            if (res == 0)
            {
                return Resides.City;
            }
            else
            {
                return Resides.Village;
            }
        }
        private DateTime GenerateBirth(int pawnears)
        {
            var start = randomDateTime.Next();
            var nowmon = DateTime.Now.Month;
            var currentear = DateTime.Now.AddYears(-pawnears).Year;
            if (start.Month>nowmon)
            {
                currentear = currentear - 1;
            }
            
            return new DateTime(currentear,start.Month,start.Day, start.Hour,start.Minute,start.Second);
        }
        private BloodType GenerateBloodType()
        {
           int[] ints = new int[8];
           double[] doubles = new double[8];
           ints[0] = (int) BloodType.OPositive;
           doubles[0] = 36.44;
           ints[1] = (int)BloodType.APositive;
           doubles[1] = 28.27;
           ints[2] = (int)BloodType.BPositive;
           doubles[2] = 20.59;
           ints[3] = (int)BloodType.ABPositive;
           doubles[3] = 5.09;
           ints[4] = (int)BloodType.ONegative;
           doubles[4] = 4.33;
           ints[5] = (int)BloodType.ANegative;
           doubles[5] = 3.52;
           ints[6] = (int)BloodType.BNegative;
           doubles[6] = 1.39;
           ints[7] = (int)BloodType.ABNegative;
           doubles[7] = 0.40;
           return (BloodType) RandomHelper.GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
        }
        private async Task<List<CharacterTrait>> GenerateTraits()
        {
            List<CharacterTrait> traits = new List<CharacterTrait>();
            int[] ints = new int[3];
            double[] doubles = new double[3];
            ints[0] = 2;
            ints[1] = 3;
            ints[2] = 5;
            doubles[0] = 60;
            doubles[1] = 30;
            doubles[2] = 10;
            var res = RandomHelper.GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
            var dbTraits = await _applicationDbContext.Traits.ToListAsync();
            ints = new int[dbTraits.Count()];
            doubles = new double[dbTraits.Count()];
            for (int j = 0; j < dbTraits.Count; j++)
            {
                ints[j] = dbTraits[j].Id;
                doubles[j] = 10;
            }
            for (int i = 0; i <= res; i++)
            {
                var restrid = RandomHelper.GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
                traits.Add( await _applicationDbContext.Traits.FindAsync(restrid));
            }
            return traits;
        }
    }
    
}
