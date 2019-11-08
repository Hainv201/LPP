using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    class BiImplication : Logic
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
            Logic nand = new NotAnd();
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

        public override Logic ConvertToCNF()
        {
            Conjunction root = new Conjunction();
            Disjunction left_disjunction = new Disjunction();
            Negation left_negation = new Negation();
            left_negation.LeftOperand = this.RightOperand.ConvertToCNF();

            left_disjunction.LeftOperand = this.LeftOperand.ConvertToCNF();
            left_disjunction.RightOperand = left_negation.ConvertToCNF();

            Disjunction right_disjunction = new Disjunction();
            Negation right_negation = new Negation();
            right_negation.LeftOperand = this.LeftOperand.ConvertToCNF();

            right_disjunction.LeftOperand = right_negation.ConvertToCNF();
            right_disjunction.RightOperand = this.RightOperand.ConvertToCNF();

            root.LeftOperand = left_disjunction.ConvertToCNF();
            root.RightOperand = right_disjunction.ConvertToCNF();
            return root;
        }
    }
}
