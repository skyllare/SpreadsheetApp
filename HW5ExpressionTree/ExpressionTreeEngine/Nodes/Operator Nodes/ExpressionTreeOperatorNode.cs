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

    

        public ExpressionTreeOperatorNode()
        {
        }

        public override double Evaluate()
        {
            
            return 0.0;
        }
    }
}
