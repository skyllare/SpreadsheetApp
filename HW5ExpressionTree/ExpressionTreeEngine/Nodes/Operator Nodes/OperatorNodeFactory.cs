// <copyright file="OperatorNodeFactory.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public static List<string> TypesOfOperators = new List<string> { "+", "-", "/", "*", ")", "(" };

        private static readonly Dictionary<char, Type> Operators = new Dictionary<char, Type>
        {
            { '+', typeof(AdditionNode) },
            { '-', typeof(SubtractionNode) },
            { '/', typeof(DivisionNode) },
            { '*', typeof(MultiplicationNode) },
        };

        private delegate void OnOperator(char op, Type type);


        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
            TraverseAvailableOperators((op, type) => Operators.Add(op, type));
        }

        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            // get the type declaration of OperatorNode
            Type operatorNodeType = typeof(ExpressionTreeOperatorNode);
            // Iterate over all loaded assemblies:
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get all types that inherit from our OperatorNode class using LINQ
                IEnumerable<Type> operatorTypes =
                assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));
                // Iterate over those subclasses of OperatorNode
                foreach (var type in operatorTypes)
                {
                    // for each subclass, retrieve the Operator property
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        object value = operatorField.GetValue(Activator.CreateInstance(type, new ExpressionTreeConstNode(0.0), new ExpressionTreeConstNode(0.0)));
                        if (value is char)
                        {
                            char operatorSymbol = (char)value;
                            // And invoke the function passed as parameter
                            // with the operator symbol and the operator class
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }

        public ExpressionTreeOperatorNode CreateOperatorNode(char op)
        {
            if (Operators.ContainsKey(op))
            {
                object operatorNodeObject = System.Activator.CreateInstance(Operators[op]);
                if (operatorNodeObject is ExpressionTreeOperatorNode)
                {
                    return (ExpressionTreeOperatorNode)operatorNodeObject;
                }
            }
            throw new Exception("Unhandled operator");
        }

        /*
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
                }*/
    }
}
