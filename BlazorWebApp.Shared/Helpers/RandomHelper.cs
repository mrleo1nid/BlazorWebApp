using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorWebApp.Shared.Models;

namespace BlazorWebApp.Shared.Helpers
{
    public static class RandomHelper
    {
        public static T GetRandomObj<T>(Random random,List<T> list)
        {
            return list.ElementAt(random.Next(list.Count));
        }

        public static bool GetRandomBool(Random random)
        {
            var res = random.Next(100);
            if (res > 50) return true;
            else return false;
        }

        public static Sex GetRandomSex(Random random)
        {
            if (GetRandomBool(random)) return Sex.Female;
            else return Sex.Male;
        }
    }
}
