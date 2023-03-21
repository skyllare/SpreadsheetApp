// <copyright file="OperatorNodeFactory.cs" company="Skyllar Estil">
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
    /// builds operator nodes.
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// list of accepted operators.
        /// </summary>
        public static List<string> TypesOfOperators = new List<string> { "+", "-", "/", "*" };

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
        }

        /// <summary>
        /// creates operator nodes.
        /// </summary>
        /// <param name="op">operator.</param>
        /// <returns>type of operator node.</returns>
        public static ExpressionTreeOperatorNode CreateOperatorNode(char op)
        {
            if (TypesOfOperators.Contains(op.ToString()))
            {
                if (op == '+')
                {
                    return new AdditionNode();
                }
                else if (op == '-')
                {
                    return new SubtractionNode();
                }
                else if (op == '/')
                {
                    return new DivisionNode();
                }
                else if (op == '*')
                {
                    return new MultiplicationNode();
                }

               // return new ExpressionTreeNode(op);
            }

            return null;
        }
    }
}
