// <copyright file="ExpressionTree.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using System.Linq;
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
        public ExpressionTree(string expression, Dictionary<string,double> vars)
        {
            this.variables = vars;
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
            return this.root.Evaluate();
        }

        /// <summary>
        /// Makes the expression tree.
        /// </summary>
        /// <param name="expression">Expression that the tree is made after.</param>
        /// <returns>root node.</returns>
        public ExpressionTreeNode MakeExpressionTree(string expression)
        {
            List<string> sExpression = this.ShuntingYardAlgorithm(expression);
            for (int i = 0; i < sExpression.Count; i++)
            {
                if (!OperatorNodeFactory.TypesOfOperators.Contains(sExpression[i]))
                {
                    double tempValue = 0;
                    bool result = double.TryParse(sExpression[i], out tempValue);
                    if (result)
                    {
                        this.sOutput.Push(new ExpressionTreeConstNode(Convert.ToDouble(sExpression[i].ToString())));
                    }
                    else
                    {
                        ExpressionTreeVariableNode varNodeTemp = new ExpressionTreeVariableNode(sExpression[i], ref variables);
                        this.sOutput.Push(new ExpressionTreeConstNode(Convert.ToDouble(varNodeTemp.Evaluate().ToString())));
                    }
                }
                else
                {
                    ExpressionTreeOperatorNode temp = OperatorNodeFactory.CreateOperatorNode(Convert.ToChar(sExpression[i]));

                    if (this.sOutput.Count != 0)
                    {
                        temp.Right = this.sOutput.Pop();
                        temp.Left = this.sOutput.Pop();
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
        private List<string> ShuntingYardAlgorithm(string expression)
        {
            Stack<string> operatorStack = new Stack<string>();
            List<string> operatorString = new List<string>();
            string output = string.Empty;
            string var = string.Empty;
            string value = string.Empty;

            for (int i = 0; i < expression.Length; i++)
            {
                if (OperatorNodeFactory.TypesOfOperators.Contains(expression[i].ToString()))
                {
                    operatorString.Add(value);
                    value = string.Empty;
                    var = string.Empty;
                    var += expression[i];
                    if (operatorStack.Count() == 0)
                    {
                        operatorStack.Push(var);
                    }
                    else
                    {
                        operatorString.Add(operatorStack.Pop());
                        operatorStack.Push(var);
                    }
                }
                else if (char.IsDigit(expression[i]) || expression[i] == '.' || char.IsLetter(expression[i]))
                {
                    if (i != expression.Length - 1)
                    {
                        value += expression[i];
                    }
                    else
                    {
                        value += expression[i].ToString();
                        operatorString.Add(value);
                    }
                }
            }

            if (operatorStack.Count() != 0)
            {
                operatorString.Add(operatorStack.Pop());
            }

            return operatorString;
        }
    }
}
