using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class Negation : Logic
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
            Logic left_nandify = this.LeftOperand.Nandify();
            Logic nand = new NotAnd();
            nand.LeftOperand = left_nandify;
            nand.RightOperand = left_nandify;
            return nand;
        }

        public override Logic ConvertToCNF()
        {
            this.LeftOperand = this.LeftOperand.ConvertToCNF();
            if (this.LeftOperand is Disjunction d)
            {
                Conjunction root = new Conjunction();
                Negation left_negation = new Negation();
                left_negation.LeftOperand = d.LeftOperand;
                Negation right_negation = new Negation();
                right_negation.LeftOperand = d.RightOperand;

                root.LeftOperand = left_negation.ConvertToCNF();
                root.RightOperand = right_negation.ConvertToCNF();
                return root.Simplify();
            }
            else if (this.LeftOperand is Conjunction c)
            {
                Disjunction root = new Disjunction();

                Negation left_negation = new Negation();
                left_negation.LeftOperand = c.LeftOperand;
                Negation right_negation = new Negation();
                right_negation.LeftOperand = c.RightOperand;

                root.LeftOperand = left_negation.ConvertToCNF();
                root.RightOperand = right_negation.ConvertToCNF();

                return root.Simplify();
            }
            else if (this.LeftOperand is NotAnd notAnd)
            {
                Conjunction root = new Conjunction();
                root.LeftOperand = notAnd.LeftOperand.ConvertToCNF();
                root.RightOperand = notAnd.RightOperand.ConvertToCNF();
                return root.Simplify();
            }
            else if (this.LeftOperand is Negation negation)
            {
                return negation.LeftOperand.ConvertToCNF();
            }
            return this.Simplify();
        }

        public override Logic Simplify()
        {
            if (this.LeftOperand is True)
            {
                return new False();
            }
            else if (this.LeftOperand is False)
            {
                return new True();
            }
            return this;
        }

        public override string GetRandomPrefix()
        {
            return $"~({LeftOperand.GetRandomPrefix()})";
        }
        public override string GetCNFForm()
        {
            return LeftOperand.ToString().ToLower();
        }
    }
}
