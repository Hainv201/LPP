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
                d1.LeftOperand = c1.LeftOperand.ConvertToCNF();
                d1.RightOperand = c2.LeftOperand.ConvertToCNF();
                Disjunction d2 = new Disjunction();
                d2.LeftOperand = c1.RightOperand.ConvertToCNF();
                d2.RightOperand = c2.LeftOperand.ConvertToCNF();

                left.LeftOperand = d1.ConvertToCNF();
                left.RightOperand = d2.ConvertToCNF();

                Conjunction right = new Conjunction();
                Disjunction d3 = new Disjunction();
                d3.LeftOperand = c1.LeftOperand.ConvertToCNF();
                d3.RightOperand = c2.RightOperand.ConvertToCNF();
                Disjunction d4 = new Disjunction();
                d4.LeftOperand = c1.RightOperand.ConvertToCNF();
                d4.RightOperand = c2.RightOperand.ConvertToCNF();

                right.LeftOperand = d3.ConvertToCNF();
                right.RightOperand = d4.ConvertToCNF();

                root.LeftOperand = left.ConvertToCNF();
                root.RightOperand = right.ConvertToCNF();

                return root;
            }
            else if (this.LeftOperand is Conjunction conj && !(this.RightOperand is Conjunction))
            {
                Conjunction root = new Conjunction();
                Disjunction d1 = new Disjunction();
                d1.LeftOperand = conj.LeftOperand.ConvertToCNF();
                d1.RightOperand = this.RightOperand.ConvertToCNF();

                Disjunction d2 = new Disjunction();
                d2.LeftOperand = conj.RightOperand.ConvertToCNF();
                d2.RightOperand = this.RightOperand.ConvertToCNF();

                root.LeftOperand = d1.ConvertToCNF();
                root.RightOperand = d2.ConvertToCNF();
                return root;
            }
            else if (!(this.LeftOperand is Conjunction) && this.RightOperand is Conjunction conj1)
            {
                Conjunction root = new Conjunction();
                Disjunction d1 = new Disjunction();
                d1.LeftOperand = this.LeftOperand.ConvertToCNF();
                d1.RightOperand = conj1.LeftOperand.ConvertToCNF();

                Disjunction d2 = new Disjunction();
                d2.LeftOperand = this.LeftOperand.ConvertToCNF();
                d2.RightOperand = conj1.RightOperand.ConvertToCNF();

                root.LeftOperand = d1.ConvertToCNF();
                root.RightOperand = d2.ConvertToCNF();
                return root;
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
            return this;
        }
    }
}
