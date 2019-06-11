using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class BiImplication : Proposition
    {
        public BiImplication():base()
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

        public override Proposition Nandify()
        {
            Proposition nand = new NotAnd();
            //Left
            nand.LeftOperand = new NotAnd();
            nand.LeftOperand.LeftOperand = new NotAnd();
            nand.LeftOperand.LeftOperand.LeftOperand = this.LeftOperand.Nandify();
            nand.LeftOperand.LeftOperand.RightOperand = this.LeftOperand.Nandify();
            nand.LeftOperand.RightOperand = new NotAnd();
            nand.LeftOperand.RightOperand.LeftOperand = this.RightOperand.Nandify();
            nand.LeftOperand.RightOperand.RightOperand = this.RightOperand.Nandify();
            //Right
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = this.LeftOperand.Nandify();
            nand.RightOperand.RightOperand = this.RightOperand.Nandify();
            return nand;
        }
    }
}
