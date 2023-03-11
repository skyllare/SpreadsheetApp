// <copyright file="ETreeTests.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>
using ExpressionTreeEngine;
using System.Linq.Expressions;

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
            double evaluation = tTree.Evaluate();
            Assert.That(evaluation, Is.EqualTo(9));
        }

        /// <summary>
        /// tests an expression that it just subtraction.
        /// </summary>
        [Test]
        public void TestBasicSubtraction()
        {
            ExpressionTree tTree = new ExpressionTree("9-1-2");
            double evaluation = tTree.Evaluate();
            Assert.That(evaluation, Is.EqualTo(6));
        }

        /// <summary>
        /// tests an expression that it just multiplication.
        /// </summary>
        [Test]
        public void TestBasicMultiplication()
        {
            ExpressionTree tTree = new ExpressionTree("3*4*1");
            double evaluation = tTree.Evaluate();
            Assert.That(evaluation, Is.EqualTo(12));
        }

        /// <summary>
        /// tests an expression that it just division.
        /// </summary>
        [Test]
        public void TestBasicDivision()
        {
            ExpressionTree tTree = new ExpressionTree("4/2");
            double evaluation = tTree.Evaluate();
            Assert.That(evaluation, Is.EqualTo(2));
        }
    }
}
