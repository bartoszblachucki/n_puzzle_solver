using System;
using System.Collections.Generic;

namespace Fifteen.Solvers.Dfs
{
    // ReSharper disable once InconsistentNaming
    public class DFSSolver : Solver
    {
        private const int MaxDepth = 20;
        
        private readonly HashSet<PuzzleState> _explored = new(new PuzzleStateComparer());
        private readonly Stack<Node> _frontier = new();
        private readonly OperatorOrder _order;
        
        private Node _currentNode;
        
        public DFSSolver(PuzzleState initialPuzzleState, OperatorOrder order) : base(initialPuzzleState)
        {
            _currentNode = RootNode;
            _frontier.Push(_currentNode);
            _order = order;
        }

        public override Solution Solve()
        {
            StartStopwatch();
            
            while (_frontier.Count != 0)
            {
                _currentNode = _frontier.Pop();
                _explored.Add(_currentNode.PuzzleState);
                
                if (IsSolution(_currentNode))
                {
                    var timeElapsed = StopStopwatch();
                    Solution.ElapsedMilliseconds = timeElapsed;
                    Solution.SolutionNode = _currentNode;
                    return Solution;
                }
                
                Explore(_currentNode);
            }

            throw new UnsolvableError();
        }

        private void Explore(Node node)
        {
            Solution.MaxDepth = Math.Max(Solution.MaxDepth, node.Depth);
            
            var depth = node.Depth;
            if (depth >= MaxDepth)
            {
                _explored.Clear();
                return;
            }
            
            foreach (var op in _order.Operators)
            {
                if (!op.CanBePerformedOn(node.PuzzleState)) 
                    continue;
                
                Solution.StatesVisited++;
                var newState = op.Perform(node.PuzzleState);
                if (_explored.Contains(newState))
                    continue;

                var newNode = new Node(newState, node);
                _frontier.Push(newNode);
            }
            Solution.StatesProcessed++;
        }
    }

    internal class PuzzleStateComparer : IEqualityComparer<PuzzleState>
    {
        public bool Equals(PuzzleState x, PuzzleState y)
        {
            return x.GetHashCode() == y.GetHashCode();
        }

        public int GetHashCode(PuzzleState obj)
        {
            return obj.GetHashCode();
        }
    }
}