using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    class Disjunction : Logic
    {
        public Disjunction():base()
        {   
        }

        public override string GetLabel()
        {
            return "|";
        }

        public override int TruthValue
        {
            get
            {
                if (LeftOperand.TruthValue == 0 && RightOperand.TruthValue == 0)
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
            return $"({LeftOperand.ToString()} | {RightOperand.ToString()})";
        }

        public override Logic Nandify()
        {
            Logic nand = new NotAnd();
            nand.LeftOperand = new NotAnd();
            nand.LeftOperand.LeftOperand = this.LeftOperand.Nandify();
            nand.LeftOperand.RightOperand = this.LeftOperand.Nandify();
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = this.RightOperand.Nandify();
            nand.RightOperand.RightOperand = this.RightOperand.Nandify();
            return nand;
        }

        public override Logic ConvertToCNF()
        {
            if (this.LeftOperand is Conjunction c1 && this.RightOperand is Conjunction c2)
            {
                Conjunction root = new Conjunction();

                Conjunction left = new Conjunction();
                Disjunction d1 = new Disjunction();
                d1.LeftOperand = c1.LeftOperand;
                d1.RightOperand = c2.LeftOperand;
                Disjunction d2 = new Disjunction();
                d2.LeftOperand = c1.RightOperand;
                d2.RightOperand = c2.LeftOperand;

                left.LeftOperand = d1;
                left.RightOperand = d2;

                Conjunction right = new Conjunction();
                Disjunction d3 = new Disjunction();
                d3.LeftOperand = c1.LeftOperand;
                d3.RightOperand = c2.RightOperand;
                Disjunction d4 = new Disjunction();
                d4.LeftOperand = c1.RightOperand;
                d4.RightOperand = c2.RightOperand;

                right.LeftOperand = d3;
                right.RightOperand = d4;

                root.LeftOperand = left;
                root.RightOperand = right;

                return root.ConvertToCNF();
            }
            else if (this.LeftOperand is Conjunction conj && !(this.RightOperand is Conjunction))
            {
                Conjunction root = new Conjunction();
                Disjunction d1 = new Disjunction();
                d1.LeftOperand = conj.LeftOperand;
                d1.RightOperand = this.RightOperand;

                Disjunction d2 = new Disjunction();
                d2.LeftOperand = conj.RightOperand;
                d2.RightOperand = this.RightOperand;

                root.LeftOperand = d1;
                root.RightOperand = d2;
                return root.ConvertToCNF();
            }
            else if (!(this.LeftOperand is Conjunction) && this.RightOperand is Conjunction conj1)
            {
                Conjunction root = new Conjunction();
                Disjunction d1 = new Disjunction();
                d1.LeftOperand = this.LeftOperand;
                d1.RightOperand = conj1.LeftOperand;

                Disjunction d2 = new Disjunction();
                d2.LeftOperand = this.LeftOperand;
                d2.RightOperand = conj1.RightOperand;

                root.LeftOperand = d1;
                root.RightOperand = d2;
                return root.ConvertToCNF();
            }
            else if (this.LeftOperand is False)
            {
                return this.RightOperand.ConvertToCNF();
            }
            else if (this.RightOperand is False)
            {
                return this.LeftOperand.ConvertToCNF();
            }
            else if (this.LeftOperand is True || this.RightOperand is True)
            {
                return new True();
            }
            else if (new LogicComparer().Equals(this.LeftOperand, this.RightOperand))
            {
                return this.LeftOperand.ConvertToCNF();
            }
            else if (this.LeftOperand is Negation && new LogicComparer().Equals(this.LeftOperand.LeftOperand, this.RightOperand))
            {
                return new True();
            }
            else if (this.RightOperand is Negation && new LogicComparer().Equals(this.RightOperand.LeftOperand,this.LeftOperand))
            {
                return new True();
            }
            this.LeftOperand = LeftOperand.ConvertToCNF();
            this.RightOperand = RightOperand.ConvertToCNF();
            if ((this.LeftOperand is Negation || this.LeftOperand is Variable || this.LeftOperand is Disjunction)
               && (this.RightOperand is Negation || this.RightOperand is Variable || this.RightOperand is Disjunction))
            {
                return this;
            }
            return this.ConvertToCNF();
        }
    }
}
