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
        /// finds the difference.
        /// </summary>
        /// <returns>te difference of two nodes.</returns>
        public override double Evaluate()
        {
            return this.Right.Evaluate() - this.Left.Evaluate();
        }
    }
}
