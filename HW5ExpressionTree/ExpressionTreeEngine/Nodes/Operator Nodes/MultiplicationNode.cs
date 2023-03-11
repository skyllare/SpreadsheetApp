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
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
        /// </summary>
        /// <param name="left">left node.</param>
        /// <param name="right">right node.</param>
        public MultiplicationNode()
        {
            this.Left = null;
            this.Right = null;
        }

        public override double Evaluate()
        {
            return (this.Left.Data * this.Right.Data);
        }
    
    }
}
