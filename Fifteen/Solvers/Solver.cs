using System.Diagnostics;

namespace Fifteen.Solvers
{
    public abstract class Solver
    {
        protected readonly Node RootNode;
        protected readonly PuzzleState GoalPuzzleState;
        protected Solution Solution;

        private Stopwatch _stopwatch;
        
        protected Solver(PuzzleState initialPuzzleState)
        {
            RootNode = new Node(initialPuzzleState, null);
            GoalPuzzleState = GenerateGoal(initialPuzzleState.Rows, initialPuzzleState.Columns);
            Solution = new Solution {RootNode = RootNode};
        }

        private PuzzleState GenerateGoal(int rows, int columns)
        {
            int[,] goal = new int[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    goal[row, column] = row * columns + column + 1;
                }
            }

            goal[rows - 1, columns - 1] = 0;
            return new PuzzleState(goal, new Coords(rows - 1, columns - 1));
        }

        protected bool IsSolution(Node node)
        {
            return IsSolution(node.PuzzleState);
        }

        protected bool IsSolution(PuzzleState puzzleState)
        {
            return puzzleState.Equals(GoalPuzzleState);
        }

        protected void StartStopwatch()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        protected long StopStopwatch()
        {
            _stopwatch.Stop();
            return _stopwatch.ElapsedMilliseconds;
        }

        public abstract Solution Solve();
    }
}