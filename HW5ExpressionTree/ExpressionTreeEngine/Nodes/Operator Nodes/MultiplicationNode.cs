using ExpressionTreeEngine.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeEngine
{
    internal class MultiplicationNode : ExpressionTreeOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
        /// </summary>
        /// <param name="left">left node.</param>
        /// <param name="right">right node.</param>
        public MultiplicationNode(ExpressionTreeNode left, ExpressionTreeNode right)
        : base(left, right)
        {
        }

        /// <summary>
        /// multiples the operands.
        /// </summary>
        protected override void PerformOperation()
        {
            // Multiply the values of the left and right operands
            double result = ((ExpressionTreeConstNode)this.leftOperand).Data * ((ExpressionTreeConstNode)this.rightOperand).Data;
            Console.WriteLine($"Result of addition: {result}");
        }
    }
}
