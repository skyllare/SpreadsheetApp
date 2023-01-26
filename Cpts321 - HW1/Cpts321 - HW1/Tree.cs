using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cpts321HW1
{
    public class Tree
    {
        //root node
        private Node root;

        //METHOD: Insert
        //If the root is null, the data is set to a root node. If it isn't
        //null, goes through Insert() method inside the node class.

        public void Insert(int data)
        {
            if (root != null)
            {
                root.Insert(data);
            }
            else
            {
                root = new Node(data);
            }
        }

        //METHOD: InOrderTraversal
        //allows the InOrderTraversal to run if the tree isn't empty.
        //if the tree is empty, return
        public void InOrderTraversal()
        {
            //tree isn't empty
            if (root != null)
            {
                root.InOrderTraversal();
            }
            //tree is empty
            else
            {
                return;
            }

        }
        //METHOD: CountNodes()
        //Will proceeed to the CountNodes inside the Node class if the tree is not NULL
        //if the tree is NULL, will be returned back to the main program
        public void CountNodes(ref int i)
        {

            //tree isn't empty
            if (root != null)
            {
                //accounts for the root node
                i++;

                root.CountNodes(ref i);

            }
            //tree is empty
            else
            {
                return;
            }

        }


        //METHOD: CountLevels()
        //Will proceeed to the CountLevels inside the Node class if the tree is not NULL
        //if the tree is NULL, a 0 will be returned (as there are 0 levels)
        public int CountLevels()
        {
            if (root != null)
            {


                return root.CountLevels();
            }
            else
            {
                return 0;
            }
        }



    }
}
