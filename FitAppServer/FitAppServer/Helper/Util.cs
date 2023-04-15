using System;
using System.Collections.Generic;
using System.IO;


namespace FitAppServer.Helper
{
    public static class Util
    {
        public static List<string> GetRandom()
        {
            List<string> entries = new List<string>();

            // Read all lines from the CSV file into an array
            string[] lines = File.ReadAllLines("Helper\\recipe.csv");

            // Generate 10 random indices
            Random random = new Random();
            List<int> randomIndices = new List<int>();
            while (randomIndices.Count < 4)
            {
                int randomIndex = random.Next(lines.Length);
                if (!randomIndices.Contains(randomIndex))
                {
                    randomIndices.Add(randomIndex);
                }
            }

            // Add the entries at the random indices to the list
            foreach (int index in randomIndices)
            {
                entries.Add(lines[index]);
            }

            return entries;
        }
    }

}
