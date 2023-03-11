using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExpressionTreeEngine.Nodes;

namespace ExpressionTreeEngine
{
    /// <summary>
    /// used for addition operations.
    /// </summary>
    internal class AdditionNode : ExpressionTreeOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode"/> class.
        /// </summary>
        /// <param name="left"> expression node left.</param>
        /// <param name="right"> expression node right.</param>
        public AdditionNode()
        {
            this.Left = null;
            this.Right = null;
        }

        public override double Evaluate()
        {
            return 0;
        }

    }



}

