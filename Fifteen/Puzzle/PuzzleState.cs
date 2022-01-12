using System;
using System.Text;

namespace Fifteen
{
    public readonly struct PuzzleState
    {
        private readonly int[,] _items;
        private readonly Coords _zeroPos;
        private readonly string _string;
        private readonly int _hash;
        
        public int Rows { get; }
        public int Columns { get; }

        public Coords ZeroPosition => _zeroPos;

        public PuzzleState(int[,] items, Coords zeroPos)
        {
            _items = items;
            _zeroPos = zeroPos;
            _string = CreateStringRepresentation(_items);
            _hash = _string.GetHashCode();
            
            Rows = _items.GetLength(0);
            Columns = _items.GetLength(1);
        }

        public PuzzleState(int[,] items) : this(items, FindZeroPos(items)) { }
        
        public int Get(int row, int column)
        {
            return _items[row, column];
        }

        public bool IsPositionValid(Coords position)
        {
            if (position.Row < 0)
                return false;

            if (position.Column < 0)
                return false;

            if (position.Row >= Rows)
                return false;

            if (position.Column >= Columns)
                return false;

            return true;
        }

        public PuzzleState MoveZeroTo(Coords newPosition)
        {
            return SwapPositions(_zeroPos, newPosition);
        }
        
        /// <summary>
        /// Na potrzeby zadania możemy założyć, że przy każdym wywołaniu metody
        /// w argumencie second znajduje się nowa pozycja zera. Przekazując ją do konstruktora
        /// nowego stanu pomijamy ponowne szukanie jego pozycji. 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public PuzzleState SwapPositions(Coords first, Coords second)
        {
            var alteredItems = _items.Clone() as int[,];
            if (alteredItems == null)
                throw new ArgumentException("Items could not be cloned");

            var temp = alteredItems[first.Row, first.Column];
            alteredItems[first.Row, first.Column] = alteredItems[second.Row, second.Column];
            alteredItems[second.Row, second.Column] = temp;

            return new PuzzleState(alteredItems, second);
        }

        public bool Equals(PuzzleState other)
        {
            if (other.Rows != Rows || Columns != other.Columns)
                return false;

            if (!ZeroPosition.Equals(other.ZeroPosition))
                return false;

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    if (_items[row, column] != other._items[row, column])
                        return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// PuzzleState jest niemutowalny, więc jako hasz zwracam hasz reprezentacji tekstowej
        /// (celem późniejszego użycia HashSetów)
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _hash;
        }

        private static Coords FindZeroPos(int[,] items)
        {
            var rows = items.GetLength(0);
            var columns = items.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (items[row, column] == 0)
                    {
                        return new Coords(row, column);
                    }
                }
            }

            throw new ArgumentException("Zero tile not found");
        }

        public int ConvertToFlatIndex(Coords coords)
        {
            return ConvertToFlatIndex(coords.Row, coords.Column);
        }

        public int ConvertToFlatIndex(int row, int column)
        {
            return row * Columns + column;
        }
        
        public override string ToString()
        {
            return _string;
        }

        private static string CreateStringRepresentation(int[,] items)
        {
            StringBuilder repr = new StringBuilder();
            //string repr = "";
            var rows = items.GetLength(0);
            var columns = items.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    repr.Append($"{items[i, j]} ");
                }

                repr.Append("\n");
            }

            return repr.ToString();

            // Wersja linQ jest zbyt wolna przy milionach operacji
            /*return string.Join(Environment.NewLine, items.OfType<int>()
                .Select((value, index) => new {value, index})
                .GroupBy(x => x.index / items.GetLength(1))
                .Select(x => $"{{{string.Join(",", x.Select(y => y.value))}}}"));*/
        }
    }
}