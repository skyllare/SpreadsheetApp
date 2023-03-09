using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeEngine
{
    internal class OperatorNodeFactory
    {
        private static List<char> TypesOfOperators = new List<char> { '+', '-', '/', '*' };

        public OperatorNodeFactory() 
        {
        }

        public static ExpressionTreeNode CreateOperatorNode(char op)
        {
            if (TypesOfOperators.Contains(op))
            {
                return new ExpressionTreeNode(op);
            }

            return null;
        }
    }
}
