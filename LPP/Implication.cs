using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class Implication : Proposition
    {
        public Implication():base()
        {
        }

        public override string GetLabel()
        {
            return ">";
        }

        public override int TruthValue
        {
            get
            {
                if (LeftOperand.TruthValue == 1 && RightOperand.TruthValue == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public override string ToString()
        {
           return $"({LeftOperand.ToString()} > {RightOperand.ToString()})";
        }

        public override Proposition Nandify()
        {
            Proposition nand = new NotAnd();
            nand.LeftOperand = this.LeftOperand.Nandify();
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = this.RightOperand.Nandify();
            nand.RightOperand.RightOperand = this.RightOperand.Nandify();
            return nand;
        }
    }
}
