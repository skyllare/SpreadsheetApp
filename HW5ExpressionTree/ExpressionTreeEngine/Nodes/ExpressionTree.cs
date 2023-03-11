// <copyright file="ExpressionTree.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using ExpressionTreeEngine.Nodes;
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
        private ExpressionTreeNode? root = null;
        private OperatorNodeFactory opFactory = new OperatorNodeFactory();
        private Dictionary<string, double> variables = new();
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
        /// <param name="variableName"></param>
        /// <param name="variableValue"></param>
        public void SetVariable(string variableName, double variableValue)
        {
            variables[variableName] = variableValue;
        }

        /// <summary>
        /// evaluates the expression to a double value
        /// </summary>
        /// <returns>The value of the expression.</returns>
        public double Evaluate()
        {
            return root.Evaluate();
        }

        /// <summary>
        /// Makes the expression tree.
        /// </summary>
        /// <param name="expression">Expression that the tree is made after.</param>
        /// <returns>root node.</returns>
        private ExpressionTreeNode MakeExpressionTree(string expression)
        {
            expression = ShuntingYardAlgorithm(expression);
        

            for (int i = 0; i < expression.Length; i++)
            {
                if (!OperatorNodeFactory.TypesOfOperators.Contains(expression[i]))
                {
                    if (Char.IsDigit(expression[i]))
                    {
                        sOutput.Push(new ExpressionTreeConstNode(Convert.ToDouble(expression[i].ToString())));
                    }
                   
                    
                }
                else
                {
                    ExpressionTreeOperatorNode temp = OperatorNodeFactory.CreateOperatorNode(expression[i]);

                    if (sOutput.Count != 0)
                    {
                        temp.Left = sOutput.Pop();
                        temp.Right = sOutput.Pop();
                    }

                    sOutput.Push(temp);

                }                
            }            
            return sOutput.Pop();
        }


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
