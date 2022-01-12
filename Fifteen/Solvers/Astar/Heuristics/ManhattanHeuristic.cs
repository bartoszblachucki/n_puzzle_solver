using System;

namespace Fifteen.Solvers.Astar.Heuristics
{
    public class ManhattanHeuristic : Heuristic
    {
        public override int CalculateHeuristicScore(Node node, PuzzleState goalState)
        {
            var h = base.CalculateHeuristicScore(node, goalState);

            for (var row = 0; row < goalState.Rows; row++)
            {
                for (var column = 0; column < goalState.Columns; column++)
                {
                    var flatIndex = goalState.ConvertToFlatIndex(row, column);
                    var dataX = flatIndex % goalState.Columns;
                    var dataY = flatIndex / goalState.Columns;

                    var goalPosition = GetGoalPosition(node.PuzzleState.Get(row, column), goalState);
                    var goalFlatIndex = goalState.ConvertToFlatIndex(goalPosition);
                    var goalX = goalFlatIndex % goalState.Columns;
                    var goalY = goalFlatIndex / goalState.Columns;

                    h += Math.Abs(dataX - goalX) + Math.Abs(dataY - goalY);
                }
            }

            return h;
        }

        private Coords GetGoalPosition(int value, PuzzleState goalState)
        {
            for (var row = 0; row < goalState.Rows; row++)
            {
                for (var column = 0; column < goalState.Columns; column++)
                {
                    if (goalState.Get(row, column) == value)
                        return new Coords(row, column);
                }
            }

            throw new ArgumentException($"Desired position of {value} cannot be found!");
        }
    }
}