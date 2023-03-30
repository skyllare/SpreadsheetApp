// <copyright file="Program.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Reflection;
using ExpressionTreeEngine;
using ExpressionTreeEngine.Nodes;

/// <summary>
/// houses the demo program.
/// </summary>
public class Program
{
    /// <summary>
    /// demo class.
    /// </summary>
    private static void Main()
    {
        string expression = "A1+B1+C1";
        int userInput = 0;
        Dictionary<string, double> variables = new ();

        do
        {
            userInput = 0;
            userInput = Menu(expression);

            if (userInput == 1)
            {
                expression = Console.ReadLine();
            }
            else if (userInput == 2)
            {
                Console.WriteLine("Enter variable name");
                string curVName = Console.ReadLine();
                Console.WriteLine("Enter varibale value");
                double vValue = Convert.ToInt32(Console.ReadLine());
                variables[curVName] = vValue;
            }
            else if (userInput == 3)
            {
                ExpressionTree test = new ExpressionTree(expression, variables);
                test.MakeExpressionTree(expression);
                double evaluation = test.Evaluate();
                Console.WriteLine(evaluation);
            }
        }
        while (userInput != 4);
    }

    /// <summary>
    /// holds the menu options.
    /// </summary>
    /// <param name="expression">takes in the default expression/\.</param>
    /// <returns>the user input.</returns>
    private static int Menu(string expression)
    {
        Console.WriteLine("Menu (current expression) = " + expression);
        Console.WriteLine("1 = Set a new expression");
        Console.WriteLine("2 = Set a variable value");
        Console.WriteLine("3 = Evaluate Tree");
        Console.WriteLine("4 = Quit");
        int userInput = Convert.ToInt32(Console.ReadLine());
        return userInput;
    }
}