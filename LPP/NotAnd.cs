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
            Disjunction root = new Disjunction();

            Negation left_negation = new Negation();
            left_negation.LeftOperand = this.LeftOperand;
            Negation right_negation = new Negation();
            right_negation.LeftOperand = this.RightOperand;  

            root.LeftOperand = left_negation;
            root.RightOperand = right_negation;

            return root.ConvertToCNF();
        }
    }
}
