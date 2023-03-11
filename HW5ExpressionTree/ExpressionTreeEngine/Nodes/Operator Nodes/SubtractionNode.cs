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
        public SubtractionNode()
        {
            Left = null;
            Right = null;
        }


        public override double Evaluate()
        {

            return 0;
        }

    }
}
