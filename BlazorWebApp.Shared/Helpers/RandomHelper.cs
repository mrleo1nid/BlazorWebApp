using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorWebApp.Shared.Models;

namespace BlazorWebApp.Shared.Helpers
{
    public static class RandomHelper
    {
        public static RelationType GetFatherType(Random random)
        {
            int[] ints = new int[2];
            double[] doubles = new double[2];
            ints[0] = (int) RelationType.BiologicalFather;
            doubles[0] = 95;
            ints[1] = (int)RelationType.FosterFather;
            doubles[1] = 5;
            return (RelationType) GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
        }
        public static RelationType GetMotherType(Random random)
        {
            int[] ints = new int[2];
            double[] doubles = new double[2];
            ints[0] = (int)RelationType.BiologicalFather;
            doubles[0] = 95;
            ints[1] = (int)RelationType.FosterFather;
            doubles[1] = 5;
            return (RelationType)GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
        }
        public static RelationType GetSpouseOrEx(Random random)
        {
            int[] ints = new int[2];
            double[] doubles = new double[2];
            ints[0] = (int)RelationType.Spouse;
            doubles[0] = 30;
            ints[1] = (int)RelationType.Ex;
            doubles[1] = 70;
            return (RelationType)GetRandomNumberFromArrayWithProbabilities(ints, doubles, random);
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
            int[] ints = new int[150];
            double[] doubles = new double[150];
            if (age >=150)
            {
                return new Tuple<bool, int>(true,0);
            }

            for (int i = start; i < 150; i++)
            {
                if (i < 14)
                {
                    ints[i] = i;
                    doubles[i] = 3;
                }
                else if (i < 25)
                {
                    ints[i] = i;
                    doubles[i] = 5;
                }
                else if (i < 40)
                {
                    ints[i] = i;
                    doubles[i] = 8;
                }
                else if (i < 60)
                {
                    ints[i] = i;
                    doubles[i] = 10;
                }
                else if (i < 80)
                {
                    ints[i] = i;
                    doubles[i] = 15;
                }
                else if (i < 100)
                {
                    ints[i] = i;
                    doubles[i] = 20;
                }
                else if (i < 130)
                {
                    ints[i] = i;
                    doubles[i] = 50;
                }
                else if (i < 150)
                {
                    ints[i] = i;
                    doubles[i] = 80;
                }
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
