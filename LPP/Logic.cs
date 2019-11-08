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
            string graph = Environment.NewLine+$"node{index} [label = \"{GetLabel()}\"]";
            if (preIndex != 0)
            {
                graph += Environment.NewLine+$"node{preIndex} -- node{index}";
            }
            //2 operands
            if (this is BiImplication || this is Conjunction || this is Disjunction || this is Implication || this is NotAnd)
            {
                int pre = index;
                index++;
                graph += LeftOperand.CreateGraph(ref index, pre);
                graph += RightOperand.CreateGraph(ref index, pre);
                return graph;
            }
            // 1 operand
            else if (this is Negation || this is Universal || this is Existential)
            {
                int pre = index;
                index++;
                graph += LeftOperand.CreateGraph(ref index, pre);
                return graph;
            }
            else
            {
                index++;
                return graph;
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
    }
}
