using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class BiImplication : Logic
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

        public override Logic Nandify()
        {
            Logic left_nandify = this.LeftOperand.Nandify();
            Logic right_nandify = this.RightOperand.Nandify();
            Logic nand = new NotAnd();
            //Left
            nand.LeftOperand = new NotAnd();
            nand.LeftOperand.LeftOperand = new NotAnd();
            nand.LeftOperand.LeftOperand.LeftOperand = left_nandify;
            nand.LeftOperand.LeftOperand.RightOperand = left_nandify;
            nand.LeftOperand.RightOperand = new NotAnd();
            nand.LeftOperand.RightOperand.LeftOperand = right_nandify;
            nand.LeftOperand.RightOperand.RightOperand = right_nandify;
            //Right
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = left_nandify;
            nand.RightOperand.RightOperand = right_nandify;
            return nand;
        }

        public override Logic ConvertToCNF()
        {
            Logic left_convert = this.LeftOperand.ConvertToCNF();
            Logic right_convert = this.RightOperand.ConvertToCNF();
            Conjunction root = new Conjunction();
            Disjunction left_disjunction = new Disjunction();
            Negation left_negation = new Negation();
            left_negation.LeftOperand = right_convert;

            left_disjunction.LeftOperand = left_convert;
            left_disjunction.RightOperand = left_negation.ConvertToCNF();

            Disjunction right_disjunction = new Disjunction();
            Negation right_negation = new Negation();
            right_negation.LeftOperand = left_convert;

            right_disjunction.LeftOperand = right_negation.ConvertToCNF();
            right_disjunction.RightOperand = right_convert;

            root.LeftOperand = left_disjunction;
            root.RightOperand = right_disjunction;
            return root.Simplify();
        }

        public override string GetRandomPrefix()
        {
            return $"=({LeftOperand.GetRandomPrefix()},{RightOperand.GetRandomPrefix()})";
        }
    }
}
