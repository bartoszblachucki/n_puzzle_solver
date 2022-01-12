using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Fifteen.Solvers
{
    public class Solution
    {
        private List<Node> _path;
        public List<Node> Path => _path ??= FindSolutionRecursively(SolutionNode);

        public int PathLength => Path.Count;
        public Node RootNode;
        public Node SolutionNode;
        public long ElapsedMilliseconds;
        public int MaxDepth;
        public int StatesVisited;
        public int StatesProcessed;

        private List<Node> FindSolutionRecursively(Node solutionNode)
        {
            List<Node> solutionPath = new List<Node>();

            for (var node = solutionNode; node.Parent != null; node = node.Parent)
            {
                solutionPath.Add(node);
            }

            solutionPath.Reverse();
            return solutionPath;
        }

        public override string ToString()
        {
            return $"Osiągnięta głębokość: {MaxDepth}\n" +
                   $"Czas wykonania (ms): {ElapsedMilliseconds}\n" +
                   $"Stanów odwiedzonych: {StatesVisited}\n" +
                   $"Stanów przetworzonych: {StatesProcessed}\n";
        }

        public string ToOperatorString()
        {
            string solutionStringBuilder = "";
            solutionStringBuilder += DetermineOperatorCharBetween(RootNode.PuzzleState, Path[0].PuzzleState);

            for (int i = 1; i < Path.Count; i++)
            {
                solutionStringBuilder += DetermineOperatorCharBetween(Path[i - 1].PuzzleState, Path[i].PuzzleState);
            }

            return solutionStringBuilder;
        }

        private static char DetermineOperatorCharBetween(PuzzleState one, PuzzleState two)
        {
            var posDelta = two.ZeroPosition - one.ZeroPosition;

            var left = new Coords(0, -1);
            var right = new Coords(0, 1);
            var up = new Coords(-1, 0);
            var down = new Coords(1, 0);

            if (posDelta.Equals(left))
                return 'L';
            if (posDelta.Equals(right))
                return 'R';
            if (posDelta.Equals(up))
                return 'U';
            if (posDelta.Equals(down))
                return 'D';
            throw new ArgumentException($"Unknown operator for zero position delta {posDelta}");
        }
    }
}