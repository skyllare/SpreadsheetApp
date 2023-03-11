using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeEngine.Nodes
{
    /// <summary>
    /// abstract bade class for all mode types.
    /// </summary>
    public abstract class ExpressionTreeNode
    {

       

        public abstract double Evaluate();
    }
}
