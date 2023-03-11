// <copyright file="DivisionNode.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

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

        /// <summary>
        /// finds the quotient.
        /// </summary>
        /// <returns>the quotient.</returns>
        public override double Evaluate()
        {
            return this.Right.Evaluate() / this.Left.Evaluate();
        }
    }
}
