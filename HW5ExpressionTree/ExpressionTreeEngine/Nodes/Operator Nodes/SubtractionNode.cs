// <copyright file="SubtractionNode.cs" company="Skyllar Estil">
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
    /// class for subtractor nodes.
    /// </summary>
    internal class SubtractionNode : ExpressionTreeOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionNode"/> class.
        /// </summary>
        public SubtractionNode()
        {
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// returns the precendence of the subtraction operator.
        /// </summary>
        /// <param name="op">operator.</param>
        /// <returns>int 1.</returns>
        public override int OperatorPrecedence(string op)
        {
            return 2;
        }


        /// <summary>
        /// finds the difference.
        /// </summary>
        /// <returns>te difference of two nodes.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() - this.Right.Evaluate();
        }
    }
}
