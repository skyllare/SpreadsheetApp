using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeEngine
{
    /// <summary>
    /// builds operator nodes.
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// list of accepted operators.
        /// </summary>
        public static List<char> TypesOfOperators = new List<char> { '+', '-', '/', '*' };

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
        }

        //creates operator nodes.
        public static ExpressionTreeNode CreateOperatorNode(char op)
        {
            if (TypesOfOperators.Contains(op))
            {
                if (op == '+')
                {
                    return new AdditionNode(null, null);
                }
                else if (op == '-')
                {
                    return new SubtractionNode(null, null);
                }
                else if (op == '/')
                {
                    return new DivisionNode(null, null);
                }
                else if (op == '*')
                {
                    return new MultiplicationNode(null, null);
                }

               // return new ExpressionTreeNode(op);
            }

            return null;
        }
    }
}
