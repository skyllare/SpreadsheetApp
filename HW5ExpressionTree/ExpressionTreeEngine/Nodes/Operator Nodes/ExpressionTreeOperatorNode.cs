// <copyright file="ExpressionTreeOperatorNode.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpressionTreeEngine.Nodes
{
    /// <summary>
    /// base calss for all operator nodes.
    /// </summary>
    internal abstract class ExpressionTreeOperatorNode : ExpressionTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTreeOperatorNode"/> class.
        /// </summary>
        public ExpressionTreeOperatorNode()
        {
        }

        /// <summary>
        /// Gets or sets the Left node.
        /// </summary>
        public ExpressionTreeNode Left { get; set; }

        /// <summary>
        /// Gets or sets the Right node.
        /// </summary>
        public ExpressionTreeNode Right { get; set; }

        /// <summary>
        /// Evaluate method.
        /// </summary>
        /// <returns> zero.</returns>
        public override double Evaluate()
        {
            return 0.0;
        }
    }
}
