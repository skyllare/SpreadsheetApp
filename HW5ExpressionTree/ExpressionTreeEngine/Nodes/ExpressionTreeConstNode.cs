// <copyright file="ExpressionTreeConstNode.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeEngine.Nodes
{
    /// <summary>
    /// class for expressions/ operands.
    /// </summary>
    internal class ExpressionTreeConstNode : ExpressionTreeNode
    {
        /// <summary>
        /// the data/ the operand.
        /// </summary>
        private double data;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTreeConstNode"/> class.
        /// </summary>
        /// <param name="eData">input data.</param>
        public ExpressionTreeConstNode(double eData)
        {
            this.data = eData;
        }

        /// <summary>
        /// Gets or sets the operand / the value.
        /// </summary>
        public double Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        /// <summary>
        /// evaluate for const nodes
        /// </summary>
        /// <returns>data of node.</returns>
        public override double Evaluate()
        {
            return this.data;
        }
    }
}
