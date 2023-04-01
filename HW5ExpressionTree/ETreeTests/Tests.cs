// <copyright file="Tests.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>
using System.Linq.Expressions;
using System.Reflection;
using ExpressionTreeEngine;

namespace ExpressionTreeTests
{
    /// <summary>
    /// class for unit tests.
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// base dictionary for test cases.
        /// </summary>
        private readonly Dictionary<string, double> variables = new ();

        /// <summary>
        /// base dictionary for variables.
        /// </summary>
        private readonly Dictionary<string, double> vVariables = new Dictionary<string, double>()
            {
                { "A1", 6.0 },
                { "B1", 3.0 },
            };

        /// <summary>
        /// tests all operations.
        /// </summary>
        /// <param name="n">experssion.</param>
        /// <param name="m">expected result.</param>
        [TestCase("3.7*2.4*1.2", 10.656)]
        [TestCase("10.2-2.5-1.8", 5.9)]
        [TestCase("3.4+6.7+4.8", 14.9)]
        [TestCase("3*4*1", 12)]
        [TestCase("10-1-2", 7)]
        [TestCase("2+2+5", 9)]
        [TestCase("12/2/4", 1.5)]
        [TestCase("12/6/1", 2)]
        [TestCase("4+4*2/(1-5)", 2)]
        [TestCase("4/(8-6)*(7*3)", 42)]
        [TestCase("8*2/4", 4)]
        [TestCase("(A1+B1)/B1", 3)]
        [TestCase("A1/B1", 2.0)]
        [TestCase("A1*B1", 18.0)]
        [TestCase("A1-B1", 3.0)]
        [TestCase("A1+B1", 9.0)]
        [TestCase("X+Y+Z", null)]
        public void TestVariableDivision(string n, double? m)
        {
            ExpressionTree tTree = new ExpressionTree(n, this.vVariables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(m).Within(0.00001));
        }
    }
}