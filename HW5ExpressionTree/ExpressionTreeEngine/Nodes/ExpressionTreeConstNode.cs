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
        public ExpressionTreeConstNode(double eData) 
        { 
            this.data = eData;
        }
        /// <summary>
        /// the data/ the operand.
        /// </summary>
        private double data;

        /// <summary>
        /// Gets the operand / the value.
        /// </summary>
        public double Data
        {
            get { return data; }
            set { data = value; }
        }

        public override double Evaluate()
        {
            return this.data;
        }
    }
}
