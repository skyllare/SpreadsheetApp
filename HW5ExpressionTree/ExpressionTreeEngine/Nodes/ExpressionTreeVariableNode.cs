// <copyright file="ExpressionTreeVariableNode.cs" company="Skyllar Estil">
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
    /// class for variable nodes.
    /// </summary>
    public class ExpressionTreeVariableNode : ExpressionTreeNode
    {
        /// <summary>
        /// name of variable.
        /// </summary>
        private string vName;

        /// <summary>
        /// dictionary of names and values.
        /// </summary>
        private Dictionary<string, double> vValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTreeVariableNode"/> class.
        /// </summary>
        /// <param name="name">name of variables.</param>
        /// <param name="variables">dictionary.</param>
        public ExpressionTreeVariableNode(string name, ref Dictionary<string, double> variables)
        {
            this.vName = name;
            this.vValue = variables;
        }

        /// <summary>
        /// Gets or sets the varibale name.
        /// </summary>
        public string VName
        {
            get { return this.vName; }
            set { this.vName = value; }
        }

        /// <summary>
        /// Gets or sets the variable dictionary.
        /// </summary>
        public Dictionary<string, double> VValue
        {
            get { return this.vValue; }
            set { this.vValue = value; }
        }

        /// <summary>
        /// evaluation for varibale nodes.
        /// </summary>
        /// <returns>the value of the variable.</returns>
        public override double Evaluate()
        {
            double rValue = 0.0;
            if (this.vValue.ContainsKey(this.vName))
            {
                rValue = this.vValue[this.vName];
            }

            return rValue;
        }
    }
}