using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class Conjunction : Logic
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
            Logic left_nandify = this.LeftOperand.Nandify();
            Logic right_nandify = this.RightOperand.Nandify();
            Logic nand = new NotAnd();
            //Left
            nand.LeftOperand = new NotAnd();
            nand.LeftOperand.LeftOperand = left_nandify;
            nand.LeftOperand.RightOperand = right_nandify;
            //Right
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = left_nandify;
            nand.RightOperand.RightOperand = right_nandify;
            return nand;
        }

        public override Logic Simplify()
        {
            LogicComparer logicComparer = new LogicComparer();
            if (this.LeftOperand is True)
            {
                return this.RightOperand.Simplify();
            }
            else if (this.RightOperand is True)
            {
                return this.LeftOperand.Simplify();
            }
            else if (this.LeftOperand is False || this.RightOperand is False)
            {
                return new False();
            }
            else if (logicComparer.Equals(this.LeftOperand, this.RightOperand))
            {
                return this.LeftOperand.Simplify();
            }
            return this;
        }
        public override Logic ConvertToCNF()
        {
            this.LeftOperand = this.LeftOperand.ConvertToCNF();
            this.RightOperand = this.RightOperand.ConvertToCNF();
            return this.Simplify();
        }

        public override Logic ApplyDistributiveLaw()
        {
            this.LeftOperand = this.LeftOperand.ApplyDistributiveLaw();
            this.RightOperand = this.RightOperand.ApplyDistributiveLaw();
            return this.Simplify();
        }

        public override string GetRandomPrefix()
        {
            return $"&({LeftOperand.GetRandomPrefix()},{RightOperand.GetRandomPrefix()})";
        }
        public override string GetCNFForm()
        {
            return LeftOperand.GetCNFForm() + "," + RightOperand.GetCNFForm();
        }
    }
}
