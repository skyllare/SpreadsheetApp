using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpressionTreeEngine.Nodes
{
    internal abstract class ExpressionTreeOperatorNode : ExpressionTreeNode
    {
        protected ExpressionTreeNode leftOperand;
        protected ExpressionTreeNode rightOperand;

        public ExpressionTreeOperatorNode(ExpressionTreeNode left, ExpressionTreeNode right)
        {
            leftOperand = left;
            rightOperand = right;
        }

        public void Evaluate()
        {
            leftOperand.Evaluate();
            rightOperand.Evaluate();
            PerformOperation();
        }

        protected abstract void PerformOperation();


    }
}
