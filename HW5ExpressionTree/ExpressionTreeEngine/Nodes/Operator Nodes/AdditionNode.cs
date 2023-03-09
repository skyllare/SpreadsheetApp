using ExpressionTreeEngine.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpressionTreeEngine
{
    internal class AdditionNode : ExpressionTreeOperatorNode
    {
        public AdditionNode(ExpressionTreeNode left, ExpressionTreeNode right)
            : base(left, right)
        {
        }

        protected override void PerformOperation()
        {
            // Add the values of the left and right operands
            double result = ((ExpressionTreeConstNode)this.leftOperand).Value + ((ExpressionTreeConstNode)this.rightOperand).Value;
            Console.WriteLine($"Result of addition: {result}");
        }
    }
}
