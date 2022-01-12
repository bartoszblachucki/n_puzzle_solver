using System;
using System.IO;
using System.Linq;
using Fifteen.Solvers;

namespace Fifteen
{
    public static class FileUtils
    {
        public static PuzzleState ReadPuzzleFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename);
            
            var sizeString = lines[0].Split(' ');
            var rows = int.Parse(sizeString[0]);
            var columns = int.Parse(sizeString[1]);

            var puzzleItems = new int[rows, columns];
            for (int row = 1; row < lines.Length; row++)
            {
                var rowInts = lines[row].Split(' ').Select(int.Parse).ToArray();
                for (int column = 0; column < rowInts.Length; column++)
                {
                    puzzleItems[row - 1, column] = rowInts[column];
                }
            }

            var puzzleState = new PuzzleState(puzzleItems);
            return puzzleState;
        }

        public static void WriteStatsToFile(Solution solution, string statsOutputFilename)
        {
            using StreamWriter file = new StreamWriter(statsOutputFilename);
            
            if (solution == null)
            {
                file.WriteLine("-1");
                file.WriteLine("-1");
                file.WriteLine("-1");
                file.WriteLine("-1");
                file.WriteLine("-1");
            }
            else
            {
                file.WriteLine(solution.PathLength);
                file.WriteLine(solution.StatesVisited);
                file.WriteLine(solution.StatesProcessed);
                file.WriteLine(solution.MaxDepth);
                file.WriteLine(solution.ElapsedMilliseconds);
            }
        }

        public static void WriteSolutionToFile(Solution solution, string solutionOutputFilename)
        {
            using StreamWriter file = new StreamWriter(solutionOutputFilename);

            if (solution == null)
            {
                file.WriteLine("-1");
                file.WriteLine("-1");    
            }
            else
            {
                file.WriteLine(solution.PathLength);
                file.WriteLine(solution.ToOperatorString());
            }
        }
    }
}