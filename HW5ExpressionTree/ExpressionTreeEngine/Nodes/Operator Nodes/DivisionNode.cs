using ExpressionTreeEngine.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeEngine
{
    internal class DivisionNode : ExpressionTreeOperatorNode
    {
        public DivisionNode(ExpressionTreeNode left, ExpressionTreeNode right)
          : base(left, right)
        {
        }

        protected override void PerformOperation()
        {
            // Divide the values of the left and right operands
            double result = ((ExpressionTreeConstNode)this.leftOperand).Value / ((ExpressionTreeConstNode)this.rightOperand).Value;
            Console.WriteLine($"Result of addition: {result}");
        }
    }
}
