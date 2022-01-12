using System;

namespace Fifteen.Solvers.Astar.Heuristics
{
    public class HammingHeuristic : Heuristic
    {
        public override int CalculateHeuristicScore(Node node, PuzzleState goalState)
        {
            int h = base.CalculateHeuristicScore(node, goalState);

            for (int row = 0; row < goalState.Rows; row++)
            {
                for (int column = 0; column < goalState.Columns; column++)
                {
                    if (node.PuzzleState.Get(row, column) != goalState.Get(row, column))
                    {
                        h++;
                    }
                }    
            }

            return h;
        }
    }
}