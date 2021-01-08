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
        public PawnGeneratorService(NamesDbContext context)
        {
            random = new Random(Convert.ToInt32(DateTime.Now.Millisecond));
            _context = context;
        }

        public async Task<Pawn> GenerateRandomPawn()
        {
            Pawn pawn = new Pawn();
            pawn.Sex = GenerateSex();
            pawn = await CreatePawnName(pawn);
            pawn = await CreatePawnSurName(pawn);
            pawn = await CreatePawnPatronim(pawn);
            pawn.Resides = GenerateResides();
            pawn.Age = GenerateAge();
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
                return Resides.Sity;
            }
            else
            {
                return Resides.Village;
            }
        }
    }
    
}
