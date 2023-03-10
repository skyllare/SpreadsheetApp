using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeEngine
{
    /// <summary>
    /// abstract bade class for all mode types.
    /// </summary>
    internal abstract class ExpressionTreeNode
    {
        /// <summary>
        /// node data.
        /// </summary>
        private char data;

        /// <summary>
        /// left node.
        /// </summary>
        private ExpressionTreeNode left;

        /// <summary>
        /// right node.
        /// </summary>
        private ExpressionTreeNode right;

        public ExpressionTreeNode Right
        {
            get { return this; } 
            set { this.right = value; }
        }

        public ExpressionTreeNode Left
        {
            get { return this; }
            set { this.left = value; }
        }

        public char Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTreeNode"/> class.
        /// </summary>
        public ExpressionTreeNode()
        {
        }

        /// <summary>
        /// evaluates the node and it's children.
        /// </summary>
        public void Evaluate()
        {
        }
    }
}
