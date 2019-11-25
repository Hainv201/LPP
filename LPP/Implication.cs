using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class Implication : Logic
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

        public override Logic Nandify()
        {
            Logic nand = new NotAnd();
            nand.LeftOperand = this.LeftOperand.Nandify();
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = this.RightOperand.Nandify();
            nand.RightOperand.RightOperand = this.RightOperand.Nandify();
            return nand;
        }

        public override Logic ConvertToCNF()
        {
            Disjunction root = new Disjunction();

            Negation left_negation = new Negation();
            left_negation.LeftOperand = this.LeftOperand;

            root.LeftOperand = left_negation;
            root.RightOperand = RightOperand;
            return root.ConvertToCNF();
        }
    }
}
