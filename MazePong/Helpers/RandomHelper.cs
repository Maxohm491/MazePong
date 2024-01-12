using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazePong.Helpers {
    public static class RandomHelper {

        public static List<int> ListOfUniqueRandoms(int count, int min, int max) {
            if (count > max - min || max < min) {
                throw new InvalidOperationException("Generating bad random list");
            }


            Random rand = new();
            HashSet<int> randomNumbers = new();

            for (int i = 0; i < count; i++)
                while (!randomNumbers.Add(rand.Next(min, max))) ;

            return randomNumbers.ToList<int>();
        }
    }
}
