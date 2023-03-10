using ExpressionTreeEngine.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeEngine
{
    internal class SubtractionNode : ExpressionTreeOperatorNode
    {
        public SubtractionNode(ExpressionTreeNode left, ExpressionTreeNode right)
      : base(left, right)
        {
        }

        protected override void PerformOperation()
        {
            // Subtract the values of the left and right operands
            double result = ((ExpressionTreeConstNode)this.Left).Data - ((ExpressionTreeConstNode)this.Right).Data;
            Console.WriteLine($"Result of addition: {result}");
        }

    }
}
