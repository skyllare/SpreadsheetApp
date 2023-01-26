using Cpts321HW1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cpts321HW1
{
    class Program
    {
        static void Main()
        {
            Tree tree = new Tree();

            //get user input
            Console.WriteLine("Enter a collection of numbers in the range [0, 100], seperated by spaces:");
            string userInputIntList = Console.ReadLine();

            //puts the user entered integers into an interger array to then insert into BST
            string[] stringNumbersForInsert = userInputIntList.Split(' ');
            int[] intNumbersForInsert = Array.ConvertAll(stringNumbersForInsert, s => int.Parse(s));
            for (int i = 0; i < intNumbersForInsert.Length; i++)
            {
                tree.Insert(intNumbersForInsert[i]);
            }
     
            //all statements to be printed to the terminal
            Console.Write("Tree Contents: ");
            tree.InOrderTraversal();

            Console.WriteLine("\nTree statistics: ");

            int nodeCount = 0;
            tree.CountNodes(ref nodeCount);
            Console.WriteLine("   Number of nodes: " + nodeCount);
  
            Console.WriteLine("   Number of levels: " + tree.CountLevels());

            int LevelEquationResult = (int)Math.Ceiling(Math.Log(Convert.ToDouble(nodeCount))+1);

            Console.WriteLine("   Minimum number of levels that a tree with " + nodeCount + " nodes could have = " + LevelEquationResult);
            Console.WriteLine("Done");
        }
    }
}