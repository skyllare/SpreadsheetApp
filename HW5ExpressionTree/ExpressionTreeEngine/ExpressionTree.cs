// <copyright file="ExpressionTree.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

namespace ExpressionTreeEngine
{
    /// <summary>
    /// class for the entire expression tree.
    /// </summary>
    public class ExpressionTree
    {
        private ExpressionTreeNode root;

        private Dictionary<string, double> variables = new Dictionary<string, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">Expression tree is made for.</param>
        public ExpressionTree(string expression)
        {
            root = MakeExpressionTree(expression);
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

        private ExpressionTreeNode MakeExpressionTree(string expression)
        {
            Stack<int> expressionStack = new Stack<int>();

            if (expression == null)
            {
                return null;
            }

            for (int i = 0; i < expression.Length; i++)
            {

                int j = i;
                string temp = string.Empty;
                while (!this.isOperator(expression.Substring(j)))
                {
                    temp += expression.Substring(j);
                    j++;
                }

                int sTemp = Convert.ToInt32(temp);
                expressionStack.Push(sTemp);

                if (this.isOperator(expression.Substring(i)))
                {

                }
            }

            return null;
        }

        private bool isOperator(string x)
        {
            if (x == "+" || x == "-" || x == "/" || x == "*")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}