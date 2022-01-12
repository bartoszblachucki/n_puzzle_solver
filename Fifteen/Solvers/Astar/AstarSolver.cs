using System;
using System.Collections.Generic;
using Fifteen.Solvers.Astar.Heuristics;

namespace Fifteen.Solvers.Astar
{
    public class AstarSolver : Solver
    {
        private readonly HashSet<AstarNode> _openSet = new();
        private readonly HashSet<AstarNode> _closedSet = new();

        private readonly Heuristic _heuristic;
        private readonly OperatorOrder _order;

        private AstarNode _currentNode;

        public AstarSolver(PuzzleState initialPuzzleState, Heuristic heuristic) : base(initialPuzzleState)
        {
            _heuristic = heuristic;
            _order = OperatorOrder.Default();
            _currentNode = new AstarNode(RootNode);
        }

        public override Solution Solve()
        {
            StartStopwatch();
            CalculateScoresAndClose(_currentNode);

            while (_openSet.Count > 0)
            {
                _currentNode = GetNodeWithMinFCost();

                if (IsSolution(_currentNode))
                {
                    var timeElapsed = StopStopwatch();
                    Solution.ElapsedMilliseconds = timeElapsed;
                    Solution.SolutionNode = _currentNode;
                    return Solution;
                }

                _openSet.Remove(_currentNode);
                ExploreNode(_currentNode);
            }

            throw new UnsolvableError();
        }

        private void ExploreNode(AstarNode node)
        {
            Solution.MaxDepth = Math.Max(Solution.MaxDepth, node.Depth);

            foreach (var neighbour in GetNeighbours(node, _order))
            {
                CalculateScoresAndClose(neighbour);
            }

            Solution.StatesProcessed++;
        }

        private void CalculateScoresAndClose(AstarNode node)
        {
            if (_closedSet.Add(node))
            {
                node.GCost = node.Depth;
                node.HCost = CalculateHeuristicScore(node, GoalPuzzleState);
                _openSet.Add(node);
            }
        }

        private List<AstarNode> GetNeighbours(AstarNode node, OperatorOrder order)
        {
            List<AstarNode> neighbours = new List<AstarNode>();
            foreach (var op in order.Operators)
            {
                if (!op.CanBePerformedOn(node.PuzzleState))
                    continue;

                var newState = op.Perform(node.PuzzleState);
                var newNode = new AstarNode(newState, node);
                neighbours.Add(newNode);

                Solution.StatesVisited += neighbours.Count;
            }

            return neighbours;
        }

        private AstarNode GetNodeWithMinFCost()
        {
            var minF = int.MaxValue;
            AstarNode minNode = null;

            foreach (var node in _openSet)
            {
                if (node.FCost < minF)
                {
                    minF = node.FCost;
                    minNode = node;
                }
            }

            return minNode;
        }

        private int CalculateHeuristicScore(Node node, PuzzleState goalState)
        {
            return _heuristic.CalculateHeuristicScore(node, goalState);
        }
    }
}