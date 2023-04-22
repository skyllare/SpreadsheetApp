// <copyright file="ExpressionTree.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using System.Linq;
using System.Linq.Expressions;
using ExpressionTreeEngine.Nodes;

namespace ExpressionTreeEngine
{
    /// <summary>
    /// class for the entire expression tree.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// if the dictionary has values for the needed keys.
        /// </summary>
        private bool dictTest = true;

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
        /// <param name="vars">variable dictionary.</param>
        public ExpressionTree(string expression, Dictionary<string, double> vars)
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
        /// evaluates the expression to a double value.
        /// </summary>
        /// <returns>The value of the expression.</returns>
        public double? Evaluate()
        {
            if (this.dictTest)
            {
                return this.root.Evaluate();
            }
            else
            {
                return null;
            }
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
                        this.TestDictionary(sExpression);
                        ExpressionTreeVariableNode varNodeTemp = new ExpressionTreeVariableNode(sExpression[i], ref this.variables);
                        this.sOutput.Push(new ExpressionTreeConstNode(Convert.ToDouble(varNodeTemp.Evaluate().ToString())));
                    }
                }
                else
                {
                    OperatorNodeFactory factory = new OperatorNodeFactory();
                    ExpressionTreeOperatorNode temp = factory.CreateOperatorNode(Convert.ToChar(sExpression[i]));

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
        public List<string> ShuntingYardAlgorithm(string expression)
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
                    // note: make changes in here
                    operatorString.Add(value);
                    value = string.Empty;
                    var = string.Empty;
                    var += expression[i];
                    if (operatorStack.Count() == 0 || var == "(")
                    {
                        operatorStack.Push(var);
                    }
                    else if (var == ")")
                    {
                        while (operatorStack.Peek() != "(")
                        {
                            operatorString.Add(operatorStack.Pop());
                        }

                        operatorStack.Pop();
                    }
                    else
                    {
                        if (this.OperatorPrecedence(var) <= this.OperatorPrecedence(operatorStack.Peek()))
                        {
                            operatorString.Add(operatorStack.Pop());
                            operatorStack.Push(var);
                        }
                        else
                        {
                            operatorStack.Push(var);
                        }
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

            while (operatorStack.Count() != 0)
            {
                operatorString.Add(operatorStack.Pop());
            }

            operatorString.RemoveAll(string.IsNullOrEmpty);
            return operatorString;
        }

        /// <summary>
        /// returns the operator precedence.
        /// </summary>
        /// <param name="op">operator.</param>
        /// <returns>1 for +/- or 2 for //*.</returns>
        private int OperatorPrecedence(string op)
        {
            if (op == "*" || op == "/")
            {
                return 2;
            }
            else if (op == "+" || op == "-")
            {
                return 1;
            }

            return -1;
        }

        /// <summary>
        /// test if the key is in the dictionary.
        /// </summary>
        /// <param name="expression">expression.</param>
        /// <returns>true if all the keys are present, false if they aren't.</returns>
        private void TestDictionary(List<string> expression)
        {
            for (int i = 0; i < expression.Count; i++)
            {
                if (char.IsLetter(expression[i][0]))
                {
                    if (this.variables.ContainsKey(expression[i]))
                    {
                        this.dictTest = true;
                    }
                    else
                    {
                        this.dictTest = false;
                        break;
                    }
                }
            }

        }
    }
}
