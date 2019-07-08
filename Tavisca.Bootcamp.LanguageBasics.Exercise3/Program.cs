using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 },
                new[] { 2, 8 },
                new[] { 5, 2 },
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" },
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 },
                new[] { 2, 8, 5, 1 },
                new[] { 5, 2, 4, 4 },
                new[] { "tFc", "tF", "Ftc" },
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 },
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 },
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 },
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" },
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {

            int[] selectedMenu = new int[dietPlans.Length];

            for (var dietIndex = 0; dietIndex < dietPlans.Length; dietIndex++)
            {
                int[] calorie = CountCalories(protein, carbs, fat);
                int[] reIndex = Enumerable.Range(0, protein.Length).ToArray();

                if (dietPlans[dietIndex].Length.Equals(0))
                {
                    selectedMenu[dietIndex] = 0;
                }
                else
                {
                    foreach (var detail in dietPlans[dietIndex])
                    {
                        reIndex = SelectDiet(protein, carbs, fat, calorie, reIndex, detail);
                    }
                    selectedMenu[dietIndex] = reIndex[0];
                }
            }

            return selectedMenu;
        }

        private static int[] SelectDiet(int[] protein, int[] carbs, int[] fat, int[] calorie,
                                             int[] reIndex, char detail)
        {
            switch (detail)
            {
                case 'C':
                    reIndex = GetHighIndex(carbs, reIndex);
                    break;
                case 'c':
                    reIndex = GetLowIndex(carbs, reIndex);
                    break;
                case 'P':
                    reIndex = GetHighIndex(protein, reIndex);
                    break;
                case 'p':
                    reIndex = GetLowIndex(protein, reIndex);
                    break;
                case 'F':
                    reIndex = GetHighIndex(fat, reIndex);
                    break;
                case 'f':
                    reIndex = GetLowIndex(fat, reIndex);
                    break;
                case 'T':
                    reIndex = GetHighIndex(calorie, reIndex);
                    break;
                case 't':
                    reIndex = GetLowIndex(calorie, reIndex);
                    break;
            }

            return reIndex;
        }

        public static int[] CountCalories(int[] protein, int[] carbs, int[] fat)
        {
            int[] calorie = new int[protein.Length];
            for (var i = 0; i < protein.Length; i++)
            {
                calorie[i] = (protein[i] + carbs[i]) * 5 + fat[i] * 9;
            }
            return calorie;
        }

        public static int[] GetLowIndex(int[] nutrition, int[] indices)
        {
            var min_value = indices.Select(index => nutrition[index]).Min();
            int[] new_indices = indices.Where(i => nutrition[i] == min_value).ToArray();

            return new_indices;
        }

        public static int[] GetHighIndex(int[] nutrition, int[] indices)
        {
            var max_value = indices.Select(index => nutrition[index]).Max();
            int[] new_indices = indices.Where(i => nutrition[i] == max_value).ToArray();

            return new_indices;
        }
    }
}
