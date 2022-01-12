using System;
using System.Collections.Generic;

namespace Fifteen.Solvers.Bfs
{
    // ReSharper disable once InconsistentNaming
    public class BFSSolver : Solver
    {
        private readonly HashSet<PuzzleState> _explored = new();
        private readonly Queue<Node> _frontier = new();
        private readonly OperatorOrder _order;

        private Node _currentNode;
        
        public BFSSolver(PuzzleState initialPuzzleState, OperatorOrder order) : base(initialPuzzleState)
        {
            _currentNode = RootNode;
            _frontier.Enqueue(_currentNode);
            _order = order;
        }

        public override Solution Solve()
        {
            StartStopwatch();
            
            while (_frontier.Count != 0)
            {
                _currentNode = _frontier.Dequeue();

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
            
            foreach (var op in _order.Operators)
            {
                if (!op.CanBePerformedOn(node.PuzzleState)) 
                    continue;
                
                var newState = op.Perform(node.PuzzleState);
                    
                if (_explored.Contains(newState))
                {
                    continue;
                }
                
                var newNode = new Node(newState, node);
                _frontier.Enqueue(newNode);

                Solution.StatesVisited++;
            }
            _explored.Add(node.PuzzleState);
            
            Solution.StatesProcessed++;
        }
    }
}