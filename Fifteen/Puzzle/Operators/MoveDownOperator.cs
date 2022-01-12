namespace Fifteen.Puzzle.Operators
{
    public class MoveDownOperator: MoveZeroOperator
    {
        protected override Coords CalculateNewZeroPosition(Coords currentZeroPos)
        {
            return new Coords(currentZeroPos.Row + 1, currentZeroPos.Column);
        }
    }
}