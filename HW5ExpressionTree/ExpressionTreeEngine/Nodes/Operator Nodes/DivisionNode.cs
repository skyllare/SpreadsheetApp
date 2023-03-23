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
        public DivisionNode()
        {
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// returns the precendence of the division operator.
        /// </summary>
        /// <param name="op">operator.</param>
        /// <returns>int 1.</returns>
        public override int OperatorPrecedence(string op)
        {
            return 2;
        }


        /// <summary>
        /// finds the quotient.
        /// </summary>
        /// <returns>the quotient.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() / this.Right.Evaluate();
        }
    }
}
