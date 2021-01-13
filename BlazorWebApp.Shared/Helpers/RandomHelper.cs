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
        /// <summary>
        /// Возвращает случайное число из массива vals с вероятностью из массива probs
        /// </summary>
        /// <param name="vals"></param>
        /// <param name="probs"></param>
        /// <returns></returns>
        public static int GetRandomNumberFromArrayWithProbabilities(int[] vals, double[] probs, Random randomgen)
        {
            var vers = new double[probs.Length];
            double sum = probs.Sum();

            vers[0] = probs[0] / sum;
            for (int i = 1; i < vers.Length - 1; i++)
            {
                vers[i] = probs[i] / sum + vers[i - 1];
            }
            vers[vers.Length - 1] = 1.0;

            double rndval = randomgen.NextDouble();
            for (int i = 0; i < vers.Length; i++)
                if (vers[i] >= rndval)
                    return vals[i];
            return vals.Last();
        }

        public static int GetDiminishingChanceParentAge (int start, int size, Random random)
        {
           int[] ints = new int[size * 2 -1];
           double[] doubles = new double[size*2 -1];
           for (int i = 0; i < size; i++)
           {
               ints[i] = start + i;
               doubles[i] = size - i;
           }
           for (int i = 1; i < size; i++)
           {
               ints[9+i] = start - i;
               doubles[9 + i] = size - i;
           }

           return GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
        }
        public static Tuple<bool, int> GetDeathChance(int start, int age, Random random)
        {
            int[] ints = new int[age-start];
            double[] doubles = new double[age - start];
            int counter = 0;
            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = start+i;
                doubles[i] = counter;
                counter++;
            }
            var deathage = GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
            if (deathage<=age)
            {
                return new Tuple<bool, int>(true, deathage);
            }
            else
            {
                return new Tuple<bool, int>(false, deathage);
            }
        }
    }
}
