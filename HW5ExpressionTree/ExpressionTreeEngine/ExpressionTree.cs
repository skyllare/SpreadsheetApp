// <copyright file="ExpressionTree.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;

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
        private ExpressionTreeNode root;

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
        /// <param name="variableName"></param>
        /// <param name="variableValue"></param>
        public void SetVariable(string variableName, double variableValue)
        {

        }

        /// <summary>
        /// evaluates the expression to a double value
        /// </summary>
        /// <returns>The value of the expression.</returns>
        public double Evaluate()
        {
            return 0;
        }

        /// <summary>
        /// Makes the expression tree.
        /// </summary>
        /// <param name="expression">Expression that the tree is made after.</param>
        /// <returns>root node.</returns>
        private ExpressionTreeNode MakeExpressionTree(string expression)
        {
            Stack<ExpressionTreeNode> sTree = new Stack<ExpressionTreeNode>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (OperatorNodeFactory.TypesOfOperators.Contains(expression[i]))
                {
                    ExpressionTreeNode opNode = OperatorNodeFactory.CreateOperatorNode(expression[i]);
                    opNode = this.root;
                    opNode.Left = sTree.Pop();
                    if (sTree.Count != 0)
                    {
                        opNode.Right = sTree.Pop();
                    }
                    sTree.Push(opNode);
                }
                else
                {
                    ExpressionTreeConstNode tempNode = new ExpressionTreeConstNode();
                    tempNode.Data = expression[i];
                    sTree.Push(tempNode);
                }

            }
            return this.root;

        }


        private string ShuntingYardAlgorithm(string expression)
        {
            Stack<char> operatorStack = new Stack<char>();
            string output = string.Empty;
            for (int i = 0; i < expression.Length; i++)
            {
                if (OperatorNodeFactory.TypesOfOperators.Contains(expression[i]))
                {
                    operatorStack.Push(expression[i]);
                }
                else
                {
                    output += expression[i];
                }
            }

            for (int i = 0; i < operatorStack.Count; i++)
            {
                output += operatorStack.Pop();
            }

            return output;
        }
    }
}