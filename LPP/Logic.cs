using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public abstract class Logic
    {
        public Logic LeftOperand { get; set; }
        public Logic RightOperand { get; set; }
        public Logic TseitinVariable { get; private set; }
        public virtual bool IsLeaf { get; private set; }
        public virtual int TruthValue { get; private set; }

        public Logic()
        {
            this.LeftOperand = null;
            this.RightOperand = null;
        }
        // For Predicate and Variable
        public Logic(string letter)
        {
            this.LeftOperand = null;
            this.RightOperand = null;
        }
        // For Graph
        public string CreateGraph(ref int index, int preIndex = 0)
        {
            string logicgraph = Environment.NewLine+$"node{index} [label = \"{GetLabel()}\"]";
            if (preIndex != 0)
            {
                logicgraph += Environment.NewLine+$"node{preIndex} -- node{index}";
            }
            //2 operands
            if (this is BiImplication || this is Conjunction || this is Disjunction || this is Implication || this is NotAnd)
            {
                int pre = index;
                index++;
                logicgraph += LeftOperand.CreateGraph(ref index, pre);
                logicgraph += RightOperand.CreateGraph(ref index, pre);
                return logicgraph;
            }
            // 1 operand
            else if (this is Negation || this is Universal || this is Existential)
            {
                int pre = index;
                index++;
                logicgraph += LeftOperand.CreateGraph(ref index, pre);
                return logicgraph;
            }
            else
            {
                index++;
                return logicgraph;
            }
        }
        public abstract string GetLabel();
        // For Truth Table
        public TruthTable CreateTruthTable(List<Variable> Variables)
        {
            var data = new List<string[]>();
            for (int i = 0; i < (int)Math.Pow(2, Variables.Count); i++)
            {
                //Get the boolean set for the truth table
                var values = Convert.ToString(i, 2).PadLeft(Variables.Count, '0').ToCharArray();
                var row = new string[Variables.Count + 1];
                for (int j = 0; j < Variables.Count; j++)
                {
                    Variables[j].TruthValue = ((int)Char.GetNumericValue(values[j]));
                    row[j] = Variables[j].TruthValue.ToString();
                }
                row[Variables.Count] = TruthValue.ToString();
                data.Add(row);
            }
            return new TruthTable(Variables, data);
        }
        //Nadify
        public virtual Logic Nandify()
        {
            return this;
        }

        public virtual Logic ConvertToCNF()
        {
            return this;
        }

        public virtual Logic ApplyDistributiveLaw()
        {
            return this;
        }

        public virtual Logic Simplify()
        {
            return this;
        }

        public virtual string GetRandomPrefix()
        {
            return this.GetRandomPrefix();
        }
        public virtual string GetCNFForm()
        {
            return this.GetCNFForm();
        }

        public virtual Logic GetTseitinSubLogic()
        {
            return this;
        }

        public void GetTseitinVariable(ref char tsetinchar, List<Variable> variables)
        {
            if (!this.IsLeaf)
            {
                var v = new Variable(tsetinchar.ToString());
                this.TseitinVariable = v;
                variables.Add(v);
                tsetinchar++;
                if (this.LeftOperand != null)
                {
                    this.LeftOperand.GetTseitinVariable(ref tsetinchar,variables);
                }
                if (this.RightOperand != null)
                {
                    this.RightOperand.GetTseitinVariable(ref tsetinchar,variables);
                }
            }
            else
            {
                this.TseitinVariable = this;
            }
        }

        public virtual Logic GetTseitinTranformation()
        {
            if(this.LeftOperand != null && this.RightOperand != null)
            {
                if (this.LeftOperand.ToString() != this.LeftOperand.TseitinVariable.ToString() && this.RightOperand.ToString() != this.RightOperand.TseitinVariable.ToString())
                {
                    Conjunction c1 = new Conjunction();
                    c1.LeftOperand = this.GetTseitinSubLogic();
                    Conjunction c2 = new Conjunction();
                    c2.LeftOperand = this.LeftOperand.GetTseitinTranformation();
                    c2.RightOperand = this.RightOperand.GetTseitinTranformation();

                    c1.RightOperand = c2;
                    return c1;
                }
                else if (this.LeftOperand.ToString() != this.LeftOperand.TseitinVariable.ToString() && this.RightOperand.ToString() == this.RightOperand.TseitinVariable.ToString())
                {
                    Conjunction c1 = new Conjunction();
                    c1.LeftOperand = this.GetTseitinSubLogic();
                    c1.RightOperand = this.LeftOperand.GetTseitinTranformation();
                    return c1;
                }
                else if (this.LeftOperand.ToString() == this.LeftOperand.TseitinVariable.ToString() && this.RightOperand.ToString() != this.RightOperand.TseitinVariable.ToString())
                {
                    Conjunction c1 = new Conjunction();
                    c1.LeftOperand = this.GetTseitinSubLogic();
                    c1.RightOperand = this.RightOperand.GetTseitinTranformation();
                    return c1;
                }
                return this.GetTseitinSubLogic();
            }
            else if (this is Negation)
            {
                if (this.LeftOperand.ToString() != this.LeftOperand.TseitinVariable.ToString())
                {
                    Conjunction c1 = new Conjunction();
                    c1.LeftOperand = this.GetTseitinSubLogic();
                    c1.RightOperand = this.LeftOperand.GetTseitinTranformation();
                    return c1;
                }
                return this.GetTseitinSubLogic();
            }
            else
            {
                return this.GetTseitinSubLogic();
            }
        }
    }
}
