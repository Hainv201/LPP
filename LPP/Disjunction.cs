using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class Disjunction : Logic
    {
        public Disjunction():base()
        {   
        }

        public override string GetLabel()
        {
            return "|";
        }

        public override int TruthValue
        {
            get
            {
                if (LeftOperand.TruthValue == 0 && RightOperand.TruthValue == 0)
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
            return $"({LeftOperand.ToString()} | {RightOperand.ToString()})";
        }

        public override Logic Nandify()
        {
            Logic nand = new NotAnd();
            nand.LeftOperand = new NotAnd();
            nand.LeftOperand.LeftOperand = this.LeftOperand.Nandify();
            nand.LeftOperand.RightOperand = this.LeftOperand.Nandify();
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = this.RightOperand.Nandify();
            nand.RightOperand.RightOperand = this.RightOperand.Nandify();
            return nand;
        }
    }
}
