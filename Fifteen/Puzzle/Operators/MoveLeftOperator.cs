namespace Fifteen.Puzzle.Operators
{
    public class MoveLeftOperator: MoveZeroOperator
    {
        protected override Coords CalculateNewZeroPosition(Coords currentZeroPos)
        {
            return new Coords(currentZeroPos.Row, currentZeroPos.Column - 1);
        }
    }
}