using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class Negation : Logic
    {
        public Negation():base()
        {
        }

        public override string GetLabel()
        {
            return "~";
        }

        public override int TruthValue
        {
            get
            {
                if (LeftOperand.TruthValue == 0)
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
            return $"(~{LeftOperand.ToString()})";
        }

        public override Logic Nandify()
        {
            Logic nand = new NotAnd();
            nand.LeftOperand = this.LeftOperand.Nandify();
            nand.RightOperand = this.LeftOperand.Nandify();
            return nand;
        }
    }
}
