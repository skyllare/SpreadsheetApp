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

        public ExpressionTreeNode? Left { get; set; }
        public ExpressionTreeNode? Right { get; set; }

        public abstract double Evaluate();





    }
}
