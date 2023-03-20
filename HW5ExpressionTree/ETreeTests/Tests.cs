// <copyright file="Tests.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>
using System.Linq.Expressions;
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
        /// tests an expression that it just addition.
        /// </summary>
        [Test]
        public void TestBasicAddition()
        {
            ExpressionTree tTree = new ExpressionTree("2+2+5", this.variables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(9));
        }

        /// <summary>
        /// tests an expression that it just subtraction.
        /// </summary>
        [Test]
        public void TestBasicSubtraction()
        {
            ExpressionTree tTree = new ExpressionTree("10-1-2", this.variables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(7));
        }

        /// <summary>
        /// tests an expression that it just multiplication.
        /// </summary>
        [Test]
        public void TestBasicMultiplication()
        {
            ExpressionTree tTree = new ExpressionTree("3*4*1", this.variables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(12));
        }

        /// <summary>
        /// tests an expression that it just division.
        /// </summary>
        [Test]
        public void TestBasicDivision()
        {
            ExpressionTree tTree = new ExpressionTree("12/6", this.variables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(2));
        }

        /// <summary>
        /// tests an expression that it just addition.
        /// </summary>
        [Test]
        public void TestDecimalAddition()
        {
            ExpressionTree tTree = new ExpressionTree("3.4+6.7+4.8", this.variables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(14.9).Within(0.00001));
        }

        /// <summary>
        /// tests an expression that it just subtraction.
        /// </summary>
        [Test]
        public void TestDecimalSubtraction()
        {
            ExpressionTree tTree = new ExpressionTree("10.2-2.5-1.8", this.variables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(5.9).Within(0.00001));
        }

        /// <summary>
        /// tests an expression that it just multiplication.
        /// </summary>
        [Test]
        public void TestDecimalMultiplication()
        {
            ExpressionTree tTree = new ExpressionTree("3.7*2.4*1.2", this.variables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(10.656));
        }

        /// <summary>
        /// tests an expression that it just division.
        /// </summary>
        [Test]
        public void TestDecimalDivision()
        {
            ExpressionTree tTree = new ExpressionTree("11/2/4", this.variables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(1.375));
        }

        /// <summary>
        /// Tests that when variables have no value, return is 0.
        /// </summary>
        [Test]
        public void TestVariableBaseCase()
        {
            ExpressionTree tTree = new ExpressionTree("A1+B1+C2", this.variables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(0));
        }

        /// <summary>
        /// tests addition using variables.
        /// </summary>
        [Test]
        public void TestVariableAddition()
        {
            Dictionary<string, double> vVariables = new Dictionary<string, double>()
            {
                { "A1", 3.0 },
                { "B1", 3.0 },
            };

            ExpressionTree tTree = new ExpressionTree("A1+B1", vVariables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(6.0));
        }

        /// <summary>
        /// tests subtraction using variables.
        /// </summary>
        [Test]
        public void TestVariableSutraction()
        {
            Dictionary<string, double> vVariables = new Dictionary<string, double>()
            {
                { "A1", 6.0 },
                { "B1", 3.0 },
            };

            ExpressionTree tTree = new ExpressionTree("A1-B1", vVariables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(3.0));
        }

        /// <summary>
        /// tests multiplication using variables.
        /// </summary>
        [Test]
        public void TestVariableMultiplicationtion()
        {
            Dictionary<string, double> vVariables = new Dictionary<string, double>()
            {
                { "A1", 6.0 },
                { "B1", 3.0 },
            };

            ExpressionTree tTree = new ExpressionTree("A1*B1", vVariables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(18.0));
        }

        /// <summary>
        /// tests division using variables.
        /// </summary>
        [Test]
        public void TestVariableDivision()
        {
            Dictionary<string, double> vVariables = new Dictionary<string, double>()
            {
                { "A1", 6.0 },
                { "B1", 3.0 },
            };

            ExpressionTree tTree = new ExpressionTree("A1/B1", vVariables);
            Assert.That(tTree.Evaluate(), Is.EqualTo(2.0));
        }
    }
}