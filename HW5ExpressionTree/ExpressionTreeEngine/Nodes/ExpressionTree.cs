// <copyright file="ExpressionTree.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using ExpressionTreeEngine.Nodes;

namespace ExpressionTreeEngine
{
    /// <summary>
    /// class for the entire expression tree.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// root node of the tree.
        /// </summary>
        private ExpressionTreeNode? root = null;

        /// <summary>
        /// variable names and values.
        /// </summary>
        private Dictionary<string, double> variables = new ();

        /// <summary>
        /// used to hold the tree.
        /// </summary>
        private Stack<ExpressionTreeNode> sOutput = new Stack<ExpressionTreeNode>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">Expression tree is made for.</param>
        public ExpressionTree(string expression)
        {
            this.root = this.MakeExpressionTree(expression);
        }

        /// <summary>
        /// Sets the specified variable within the ExpressionTree variables dictionary.
        /// </summary>
        /// <param name="variableName">name of variable.</param>
        /// <param name="variableValue">value for variable.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables[variableName] = variableValue;
        }

        /// <summary>
        /// evaluates the expression to a double value
        /// </summary>
        /// <returns>The value of the expression.</returns>
        public double Evaluate()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return this.root.Evaluate();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        /// <summary>
        /// Makes the expression tree.
        /// </summary>
        /// <param name="expression">Expression that the tree is made after.</param>
        /// <returns>root node.</returns>
        private ExpressionTreeNode MakeExpressionTree(string expression)
        {
            expression = this.ShuntingYardAlgorithm(expression);
            for (int i = 0; i < expression.Length; i++)
            {
                if (!OperatorNodeFactory.TypesOfOperators.Contains(expression[i]))
                {
                    if (char.IsDigit(expression[i]))
                    {
                        this.sOutput.Push(new ExpressionTreeConstNode(Convert.ToDouble(expression[i].ToString())));
                    }
                }
                else
                {
                    ExpressionTreeOperatorNode temp = OperatorNodeFactory.CreateOperatorNode(expression[i]);

                    if (this.sOutput.Count != 0)
                    {
                        temp.Left = this.sOutput.Pop();
                        temp.Right = this.sOutput.Pop();
                    }

                    this.sOutput.Push(temp);
                }
            }

            return this.sOutput.Pop();
        }

        /// <summary>
        /// puts expressions in postfix.
        /// </summary>
        /// <param name="expression">og expression.</param>
        /// <returns>postfix expression.</returns>
        private string ShuntingYardAlgorithm(string expression)
        {
            Stack<char> operatorStack = new Stack<char>();
            string output = string.Empty;
            for (int i = 0; i < expression.Length; i++)
            {
                if (OperatorNodeFactory.TypesOfOperators.Contains(expression[i]))
                {
                    if (operatorStack.Count != 0)
                    {
                        output += operatorStack.Peek();
                        operatorStack.Pop();
                    }

                    operatorStack.Push(expression[i]);
                }
                else
                {
                    output += expression[i];
                }
            }

            for (int i = 0; i <= operatorStack.Count; i++)
            {
                output += operatorStack.Peek();
                operatorStack.Pop();
            }

            return output;
        }
    }
}
