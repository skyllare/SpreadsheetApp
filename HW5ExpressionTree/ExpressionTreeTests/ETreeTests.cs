// <copyright file="ETreeTests.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>
using ExpressionTreeEngine;

namespace ExpressionTreeTests
{
    public class Tests
    {
        /// <summary>
        /// tests an expression that it just addition.
        /// </summary>
        [Test]
        public void TestBasicAddition()
        {
            ExpressionTree tTree = new ExpressionTree("2+2+5");
            Assert.That(tTree.Evaluate(), Is.EqualTo(9));
        }

        /// <summary>
        /// tests an expression that it just subtraction.
        /// </summary>
        [Test]
        public void TestBasicSubtraction()
        {
            ExpressionTree tTree = new ExpressionTree("10-1-2");
            Assert.That(tTree.Evaluate(), Is.EqualTo(7));
        }

        /// <summary>
        /// tests an expression that it just multiplication.
        /// </summary>
        [Test]
        public void TestBasicMultiplication()
        {
            ExpressionTree tTree = new ExpressionTree("3*4*1");
            Assert.That(tTree.Evaluate(), Is.EqualTo(12));
        }

        /// <summary>
        /// tests an expression that it just division.
        /// </summary>
        [Test]
        public void TestBasicDivision()
        {
            ExpressionTree tTree = new ExpressionTree("12/6");
            Assert.That(tTree.Evaluate(), Is.EqualTo(2));
        }

        /// <summary>
        /// tests an expression that it just addition.
        /// </summary>
        [Test]
        public void TestDecimalAddition()
        {
            ExpressionTree tTree = new ExpressionTree("3.4+6.7+4.8");
            Assert.That(tTree.Evaluate(), Is.EqualTo(14.9));
        }

        /// <summary>
        /// tests an expression that it just subtraction.
        /// </summary>
        [Test]
        public void TestDecimalSubtraction()
        {
            ExpressionTree tTree = new ExpressionTree("10.2-2.5-1.8");
            Assert.That(tTree.Evaluate(), Is.EqualTo(5.9));
        }

        /// <summary>
        /// tests an expression that it just multiplication.
        /// </summary>
        [Test]
        public void TestDecimalMultiplication()
        {
            ExpressionTree tTree = new ExpressionTree("3.7*2.4*1.2");
            Assert.That(tTree.Evaluate(), Is.EqualTo(10.656));
        }

        /// <summary>
        /// tests an expression that it just division.
        /// </summary>
        [Test]
        public void TestDecimalDivision()
        {
            ExpressionTree tTree = new ExpressionTree("11/2/4");
            Assert.That(tTree.Evaluate(), Is.EqualTo(1.375));
        }
    }
}