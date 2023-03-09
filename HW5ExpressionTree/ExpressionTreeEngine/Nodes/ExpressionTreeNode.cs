using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeEngine
{
    internal abstract class ExpressionTreeNode
    {
        private char data;
        private ExpressionTreeNode left;
        private ExpressionTreeNode right;

        public ExpressionTreeNode()
        {
        }

        public void Evaluate()
        {
        }
    }
}
