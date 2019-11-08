using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    class Conjunction : Logic
    {
        public Conjunction():base()
        {
        }

        public override string GetLabel()
        {
            return "&";
        }

        public override int TruthValue
        {
            get
            {
                if (LeftOperand.TruthValue == 1 && RightOperand.TruthValue == 1)
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
            return $"({LeftOperand.ToString()} & {RightOperand.ToString()})";
        }

        public override Logic Nandify()
        {
            Logic nand = new NotAnd();
            //Left
            nand.LeftOperand = new NotAnd();
            nand.LeftOperand.LeftOperand = this.LeftOperand.Nandify();
            nand.LeftOperand.RightOperand = this.RightOperand.Nandify();
            //Right
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = this.LeftOperand.Nandify();
            nand.RightOperand.RightOperand = this.RightOperand.Nandify();   
            return nand;
        }

        public override Logic ConvertToCNF()
        {
            if (this.LeftOperand is True)
            {
                return this.RightOperand.ConvertToCNF();
            }
            else if (this.RightOperand is True)
            {
                return this.LeftOperand.ConvertToCNF();
            }
            else if (this.LeftOperand is False || this.RightOperand is False)
            {
                return new False();
            }
            else if (new LogicComparer().Equals(this.LeftOperand,this.RightOperand))
            {
                return this.LeftOperand.ConvertToCNF();
            }
            else if (this.LeftOperand is Negation && new LogicComparer().Equals(this.LeftOperand.LeftOperand, this.RightOperand))
            {
                return new False();
            }
            else if (this.RightOperand is Negation && new LogicComparer().Equals(this.RightOperand.LeftOperand,this.LeftOperand))
            {
                return new False();
            }
            this.LeftOperand = LeftOperand.ConvertToCNF();
            this.RightOperand = RightOperand.ConvertToCNF();
            return this;
        }
    }
}
