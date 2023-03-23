// <copyright file="AdditionNode.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

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
        public AdditionNode()
        {
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// returns the precendence of the addition operator.
        /// </summary>
        /// <param name="op">operator.</param>
        /// <returns>int 1.</returns>
        public override int OperatorPrecedence(string op)
        {
            return 1;
        }

        /// <summary>
        /// adds the expression tree nodes.
        /// </summary>
        /// <returns>the sum.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() + this.Right.Evaluate();
        }
    }
}