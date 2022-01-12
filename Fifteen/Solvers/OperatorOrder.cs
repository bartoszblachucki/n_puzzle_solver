using System;
using Fifteen.Puzzle.Operators;

namespace Fifteen.Solvers
{
    public class OperatorOrder
    {
        private const string DefaultOperatorOrderString = "LRUD";
        public readonly MoveZeroOperator[] Operators = new MoveZeroOperator[4];
        
        public OperatorOrder(string orderString)
        {
            if (orderString.Length != Operators.Length)
                throw new ArgumentException("Order string length is not equal to operator amount");

            for (int i = 0; i < orderString.Length; i++)
            {
                char operatorChar = orderString[i];
                Operators[i] = GetOperatorFromChar(operatorChar);
            }
        }

        public static OperatorOrder Default()
        {
            return new OperatorOrder(DefaultOperatorOrderString);
        }

        private MoveZeroOperator GetOperatorFromChar(char operatorChar)
        {
            return operatorChar switch
            {
                'L' => new MoveLeftOperator(),
                'R' => new MoveRightOperator(),
                'U' => new MoveUpOperator(),
                'D' => new MoveDownOperator(),
                _ => throw new ArgumentException($"Operator {operatorChar} doesn't exist")
            };
        }
    }
}