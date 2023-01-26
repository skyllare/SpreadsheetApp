using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cpts321HW1
{
    public class Node
    {
       
        //data integer with get and set
        private int data;
        public int Data
        {
            get { return data; }
            set { data = value; }
        }

        //right node with get and set
        private Node rightNode;
        public Node RightNode
        {
            get { return rightNode; }
            set { rightNode = value; }
        }

        //left node with get and set
        private Node leftNode;
        public Node LeftNode
        {
            get { return leftNode; }
            set { leftNode = value; }
            
        }
        //Node constructor 
        public Node(int value)
        {
            data = value;

        }

        //METHOD: Insert()
        //Accepts an integer value and inserts in the proper order following BST rules
        public void Insert (int value)
        {
            //if the value of the new node is greater than or equal to that of the current node
            //progress into the right side of the tree
            if (value > data)
            {
                //if the right node of the current node is null, insert new node here
                if (rightNode == null )
                {
                    rightNode = new Node(value);
                }
                //if that node isn't empty, set the current node to the right node and go through loop again
                else
                {
                    rightNode.Insert(value);
                }
            }
            //if the value of the new node is less than or equal to that of the current node
            //progress into the left side of the tree
            if (value < data)
            {
                //if the left node of the current node is null, insert new node here
                if (leftNode == null)
                {
                    leftNode = new Node(value);
                }
                //if that node isn't empty, set the current node to the right node and go through loop again
                else
                {
                    leftNode.Insert(value);
                }
            }
            //no duplicates
            if (value == data) 
            {
                return;
            }
        }

        //METHOD: InOrderTraversal
        //printing the tree in order means from least to greatest number. This is a left node then
        //right node printing method. That means the left is always visted before the right 
        //as we know that in a BST if there is a left node it is less than the right node (if there
        //is one. 
        public void InOrderTraversal()
        {
           
            //If there are left nodes in the tree, they will be printed
            if (leftNode != null)
            {
                leftNode.InOrderTraversal();
            }

            //prints left nodes, the root node, then right nodes
            Console.Write(data + " ");

            if (rightNode != null)
            {
                rightNode.InOrderTraversal();
            }

            
                

        }
        
        //METHOD: Count Nodes
        //accepts a ref int to update through each recursive cycle of the tree. Since this is a ref int
        //the value is updated as it changes in the main program
        public void CountNodes(ref int i)
        {

            //similar to the process of InOrderTraversal
            
            if (leftNode != null)
            {
                i++;
                leftNode.CountNodes(ref i);
            }
       
           

            if (rightNode != null)
            {
                i++;
                rightNode.CountNodes(ref i);
            }

           
        }


        //METHOD: CountLevels
        //Finds the height of the left and right side individually (not counting the root)
        //once each side has been traversed, the two values are compared and the max value
        //is returned to the main program (+1 for the root node)
        public int CountLevels()
        {
            int leftHeight = 0, rightHeight = 0;
            if (this.leftNode != null)
            {
                leftHeight = this.LeftNode.CountLevels();
            }

            if (this.rightNode != null)
            {
                rightHeight = this.RightNode.CountLevels();
            }

            return Math.Max(rightHeight, leftHeight) + 1;

        }

      
    }
}
