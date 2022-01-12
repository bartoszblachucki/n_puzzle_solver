#nullable enable

namespace Fifteen.Solvers
{
    public class Node
    {
        public readonly PuzzleState PuzzleState;
        public readonly int Depth;
        public Node? Parent;
        
        public Node(PuzzleState puzzleState, Node? parent)
        {
            PuzzleState = puzzleState;
            Parent = parent;
            Depth = CalculateDepth();
        }

        public void SetParent(Node parent)
        {
            Parent = parent;
        } 
        
        private int CalculateDepth()
        {
            var depth = 0;
            for (Node node = this; node.Parent != null; node = node.Parent)
                depth++;

            return depth;
        }
    }
}