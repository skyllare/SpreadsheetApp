using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeEngine.Nodes
{
    public class ExpressionTreeVariableNode : ExpressionTreeNode
    {

        private string vName;

        private double vValue;

        public string VName
        {
            get { return vName; }
            set { vName = value; }
        }

        public double VValue
        {
            get { return vValue; }
            set { vValue = value; }
        }

        public ExpressionTreeVariableNode(string name, double value)
        {
            vValue = value;
            vName = name;
        }

        public override double Evaluate()
        {
            double rValue = 0.0;
            return rValue;
        }
    }
}

