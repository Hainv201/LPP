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
        public override bool IsLeaf
        {
            get { return false; }
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
            Logic left_nandify = this.LeftOperand.Nandify();
            Logic right_nandify = this.RightOperand.Nandify();
            Logic nand = new NotAnd();
            nand.LeftOperand = left_nandify;
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = right_nandify;
            nand.RightOperand.RightOperand = right_nandify;
            return nand;
        }

        public override Logic ConvertToCNF()
        {
            Logic left_convert = this.LeftOperand.ConvertToCNF();
            Logic right_convert = this.RightOperand.ConvertToCNF();
            Disjunction root = new Disjunction();

            Negation left_negation = new Negation();
            left_negation.LeftOperand = left_convert;

            root.LeftOperand = left_negation.ConvertToCNF();
            root.RightOperand = right_convert;
            return root.Simplify();
        }

        public override string GetRandomPrefix()
        {
            return $">({LeftOperand.GetRandomPrefix()},{RightOperand.GetRandomPrefix()})";
        }

        public override Logic GetTseitinSubLogic()
        {
            BiImplication biImplication = new BiImplication();
            biImplication.LeftOperand = this.TseitinVariable;

            Implication implication = new Implication();
            implication.LeftOperand = this.LeftOperand.TseitinVariable;
            implication.RightOperand = this.RightOperand.TseitinVariable;
            biImplication.RightOperand = implication;
            Console.WriteLine(biImplication);
            return biImplication;
        }
    }
}
