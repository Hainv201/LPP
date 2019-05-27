using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class BiImplication : Proposition
    {
        public BiImplication()
        {
        }

        public override string GetLabel()
        {
            return "=";
        }

        public override int TruthValue
        {
            get
            {
                if (LeftOperand.TruthValue == RightOperand.TruthValue)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override string ToString()
        {
            return $"({LeftOperand.ToString()} = {RightOperand.ToString()})";
        }
    }
}
