using System;
using Fifteen.Solvers;
using Fifteen.Solvers.Astar;
using Fifteen.Solvers.Astar.Heuristics;
using Fifteen.Solvers.Bfs;
using Fifteen.Solvers.Dfs;

namespace Fifteen
{
    static class Program
    {
        static void Main(string[] args)
        {
            var solverName = args[0];
            var solverParameter = args[1];
            var puzzleState = FileUtils.ReadPuzzleFromFile(args[2]);
            var solutionOutputFilename = args[3];
            var statsOutputFilename = args[4];
            
            Console.WriteLine(string.Join(' ', args));

            Solver solver;
            if (solverName == "astr")
            {
                Heuristic heuristic = solverParameter switch
                {
                    "hamm" => new HammingHeuristic(),
                    "manh" => new ManhattanHeuristic(),
                    _ => throw new ArgumentException($"Unknown heuristic type {solverParameter}")
                };
                solver = new AstarSolver(puzzleState, heuristic);
            }
            else
            {
                var order = new OperatorOrder(solverParameter);
                solver = solverName switch
                {
                    "dfs" => new DFSSolver(puzzleState, order),
                    "bfs" => new BFSSolver(puzzleState, order),
                    _ => throw new ArgumentException($"Unknown solver {solverName}")
                };
            }

            try
            {
                var solution = solver.Solve();
                FileUtils.WriteSolutionToFile(solution, solutionOutputFilename);
                FileUtils.WriteStatsToFile(solution, statsOutputFilename);
            }
            catch (UnsolvableError)
            {
                Console.WriteLine("Unsolvable");
                FileUtils.WriteSolutionToFile(null, solutionOutputFilename);
                FileUtils.WriteStatsToFile(null, statsOutputFilename);
            }
            
        }
    }
}