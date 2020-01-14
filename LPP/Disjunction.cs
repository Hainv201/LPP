using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class Disjunction : Logic
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
            Logic left_nandify = this.LeftOperand.Nandify();
            Logic right_nandify = this.RightOperand.Nandify();
            Logic nand = new NotAnd();
            nand.LeftOperand = new NotAnd();
            nand.LeftOperand.LeftOperand = left_nandify;
            nand.LeftOperand.RightOperand = left_nandify;
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = right_nandify;
            nand.RightOperand.RightOperand = right_nandify;
            return nand;
        }
        public override Logic Simplify()
        {
            LogicComparer logicComparer = new LogicComparer();
            if (this.LeftOperand is False)
            {
                return this.RightOperand.Simplify();
            }
            else if (this.RightOperand is False)
            {
                return this.LeftOperand.Simplify();
            }
            else if (this.LeftOperand is True || this.RightOperand is True)
            {
                return new True();
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
            if (this.LeftOperand is Conjunction c1 && this.RightOperand is Conjunction c2)
            {
                Conjunction root = new Conjunction();

                Conjunction left_ = new Conjunction();
                Disjunction d1 = new Disjunction();
                d1.LeftOperand = c1.LeftOperand;
                d1.RightOperand = c2.LeftOperand;
                Disjunction d2 = new Disjunction();
                d2.LeftOperand = c1.LeftOperand;
                d2.RightOperand = c2.RightOperand;
                left_.LeftOperand = d1.ApplyDistributiveLaw();
                left_.RightOperand = d2.ApplyDistributiveLaw();

                Conjunction right_ = new Conjunction();
                Disjunction d3 = new Disjunction();
                d3.LeftOperand = c1.RightOperand;
                d3.RightOperand = c2.LeftOperand;
                Disjunction d4 = new Disjunction();
                d4.LeftOperand = c1.RightOperand;
                d4.RightOperand = c2.RightOperand;
                right_.LeftOperand = d3.ApplyDistributiveLaw();
                right_.RightOperand = d4.ApplyDistributiveLaw();

                root.LeftOperand = left_;
                root.RightOperand = right_;

                return root.Simplify();
            }
            else if (this.LeftOperand is Conjunction conj)
            {
                Conjunction root = new Conjunction();
                Disjunction d1 = new Disjunction();
                d1.LeftOperand = conj.LeftOperand;
                d1.RightOperand = this.RightOperand;

                Disjunction d2 = new Disjunction();
                d2.LeftOperand = conj.RightOperand;
                d2.RightOperand = this.RightOperand;

                root.LeftOperand = d1.ApplyDistributiveLaw();
                root.RightOperand = d2.ApplyDistributiveLaw();
                return root.Simplify();
            }
            else if (this.RightOperand is Conjunction conj1)
            {
                Conjunction root = new Conjunction();
                Disjunction d1 = new Disjunction();
                d1.LeftOperand = this.LeftOperand;
                d1.RightOperand = conj1.LeftOperand;

                Disjunction d2 = new Disjunction();
                d2.LeftOperand = this.LeftOperand;
                d2.RightOperand = conj1.RightOperand;

                root.LeftOperand = d1.ApplyDistributiveLaw();
                root.RightOperand = d2.ApplyDistributiveLaw();
                return root.Simplify();
            }
            return this.Simplify();
        }

        public override string GetRandomPrefix()
        {
            return $"|({LeftOperand.GetRandomPrefix()},{RightOperand.GetRandomPrefix()})";
        }

        public override string GetCNFForm()
        {
            return LeftOperand.GetCNFForm() + RightOperand.GetCNFForm();
        }
    }
}
