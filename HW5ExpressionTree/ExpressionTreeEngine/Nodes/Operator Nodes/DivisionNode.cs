using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionTreeEngine.Nodes;

namespace ExpressionTreeEngine
{
    /// <summary>
    /// used for division operations.
    /// </summary>
    internal class DivisionNode : ExpressionTreeOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionNode"/> class.
        /// </summary>
        /// <param name="left">left node.</param>
        /// <param name="right">right node.</param>
        public DivisionNode(ExpressionTreeNode left, ExpressionTreeNode right)
          : base(left, right)
        {
        }

        /// <summary>
        /// divides the left and right nodes.
        /// </summary>
        protected override void PerformOperation()
        {
            // Divide the values of the left and right operands
            double result = ((ExpressionTreeConstNode)this.Left).Data / ((ExpressionTreeConstNode)this.Right).Data;
            Console.WriteLine($"Result of addition: {result}");
        }
    }
}
