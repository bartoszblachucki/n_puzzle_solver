namespace Fifteen.Puzzle.Operators
{
    public abstract class MoveZeroOperator
    {
        public bool CanBePerformedOn(PuzzleState puzzleState)
        {
            var resultZeroPosition = CalculateNewZeroPosition(puzzleState.ZeroPosition);
            return puzzleState.IsPositionValid(resultZeroPosition);
        }

        public PuzzleState Perform(PuzzleState source)
        {
            return source.MoveZeroTo(CalculateNewZeroPosition(source.ZeroPosition));
        }

        protected abstract Coords CalculateNewZeroPosition(Coords currentZeroPos);
    }
}