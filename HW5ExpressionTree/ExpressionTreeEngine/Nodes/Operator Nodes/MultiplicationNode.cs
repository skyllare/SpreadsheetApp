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
        public MultiplicationNode(ExpressionTreeNode left, ExpressionTreeNode right)
        : base(left, right)
        {
        }

        protected override void PerformOperation()
        {
            // Multiply the values of the left and right operands
            double result = ((ExpressionTreeConstNode)this.leftOperand).Value * ((ExpressionTreeConstNode)this.rightOperand).Value;
            Console.WriteLine($"Result of addition: {result}");
        }
    }
}
