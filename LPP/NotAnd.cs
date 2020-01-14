using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class NotAnd : Logic
    {
        public NotAnd():base()
        {
        }

        public override string GetLabel()
        {
            return "%";
        }

        public override int TruthValue
        {
            get
            {
                if (LeftOperand.TruthValue == 1 && RightOperand.TruthValue == 1)
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
            return $"%({LeftOperand.ToString()}, {RightOperand.ToString()})";
        }

        public override Logic Nandify()
        {
            Logic nand = new NotAnd();
            nand.LeftOperand = this.LeftOperand.Nandify();
            nand.RightOperand = this.RightOperand.Nandify();
            return nand;
        }

        public override Logic ConvertToCNF()
        {
            Logic left_convert = this.LeftOperand.ConvertToCNF();
            Logic right_convert = this.RightOperand.ConvertToCNF();
            Disjunction root = new Disjunction();

            Negation left_negation = new Negation();
            left_negation.LeftOperand = left_convert;
            Negation right_negation = new Negation();
            right_negation.LeftOperand = right_convert;

            root.LeftOperand = left_negation.ConvertToCNF();
            root.RightOperand = right_negation.ConvertToCNF();

            return root.Simplify();
        }

        public override string GetRandomPrefix()
        {
            return $"%({LeftOperand.GetRandomPrefix()},{RightOperand.GetRandomPrefix()})";
        }
    }
}
