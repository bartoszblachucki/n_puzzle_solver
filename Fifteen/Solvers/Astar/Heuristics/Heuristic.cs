namespace Fifteen.Solvers.Astar.Heuristics
{
    public abstract class Heuristic
    {
        public virtual int CalculateHeuristicScore(Node node, PuzzleState goalState)
        {
            return node.Depth;
        }
    }
}