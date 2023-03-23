﻿// <copyright file="MultiplicationNode.cs" company="Skyllar Estil">
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
    /// class for mulitplication.
    /// </summary>
    internal class MultiplicationNode : ExpressionTreeOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
        /// </summary>
        public MultiplicationNode()
        {
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// returns the precendence of the multiplication operator.
        /// </summary>
        /// <param name="op">operator.</param>
        /// <returns>int 2.</returns>
        public override int OperatorPrecedence(string op)
        {
            return 2;
        }

        /// <summary>
        /// find the product of nodes.
        /// </summary>
        /// <returns>the product.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() * this.Right.Evaluate();
        }
    }
}
