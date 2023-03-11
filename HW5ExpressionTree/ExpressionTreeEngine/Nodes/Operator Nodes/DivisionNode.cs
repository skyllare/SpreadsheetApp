using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionTreeEngine.Nodes;

namespace ExpressionTreeEngine
{
    /// <summary>
    /// used for division operations.
    /// </summary>
    internal class DivisionNode : ExpressionTreeOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionNode"/> class.
        /// </summary>
        /// <param name="left">left node.</param>
        /// <param name="right">right node.</param>
        public DivisionNode()
        {
            this.Left = null;
            this.Right = null;
        }
        public override double Evaluate()
        {
            return (this.Left.Data / this.Right.Data);
        }
    
    }
}
