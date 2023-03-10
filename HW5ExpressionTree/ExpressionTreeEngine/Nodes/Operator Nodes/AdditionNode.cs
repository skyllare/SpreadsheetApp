using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExpressionTreeEngine.Nodes;

namespace ExpressionTreeEngine
{
    /// <summary>
    /// used for addition operations.
    /// </summary>
    internal class AdditionNode : ExpressionTreeOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode"/> class.
        /// </summary>
        /// <param name="left"> expression node left.</param>
        /// <param name="right"> expression node right.</param>
        public AdditionNode(ExpressionTreeNode left, ExpressionTreeNode right)
            : base(left, right)
        {
        }

        /// <summary>
        /// adds the left and right node together.
        /// </summary>
        protected override void PerformOperation()
        {
            // Add the values of the left and right operands
            double result = ((ExpressionTreeConstNode)this.leftOperand).Data + ((ExpressionTreeConstNode)this.rightOperand).Data;
            Console.WriteLine($"Result of addition: {result}");
        }
    }
}
