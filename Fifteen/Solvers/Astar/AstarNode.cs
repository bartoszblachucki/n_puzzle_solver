#nullable enable
namespace Fifteen.Solvers.Astar
{
    public class AstarNode : Node
    {
        public int GCost;
        public int HCost;
        public int FCost => GCost + HCost;
        
        public AstarNode(PuzzleState puzzleState, Node? parent) : base(puzzleState, parent) { }
        
        public AstarNode(Node node) : base(node.PuzzleState, node.Parent) { }

        public override int GetHashCode()
        {
            return PuzzleState.GetHashCode();
        }
    }
}