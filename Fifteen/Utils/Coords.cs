using System;

namespace Fifteen
{
    public readonly struct Coords
    {
        public readonly int Row;
        public readonly int Column;

        public Coords(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Coords(Coords coords)
        {
            Row = coords.Row;
            Column = coords.Column;
        }

        public bool Equals(Coords other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            return obj is Coords other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public override string ToString()
        {
            return $"({Row}, {Column})";
        }

        public static Coords operator +(Coords one, Coords two)
        {
            return new(one.Row + two.Row, one.Column + two.Column);
        }
        
        public static Coords operator -(Coords one, Coords two)
        {
            return new(one.Row - two.Row, one.Column - two.Column);
        }
    }
}