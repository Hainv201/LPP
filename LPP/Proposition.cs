﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    public abstract class Proposition
    {
        public Proposition LeftOperand { get; set; }
        public Proposition RightOperand { get; set; }
        public List<Variable> Variables { get; set; }
        public bool IsPredicate { get; set; }
        public virtual int TruthValue { get; private set; }

        public Proposition()
        {
            this.LeftOperand = null;
            this.RightOperand = null;
        }
        // For Graph
        public string CreateGraph(ref int index, int preIndex = 0)
        {
            string graph = $"\nnode{index} [ label = \"{GetLabel()}\" ]";
            if (preIndex != 0)
            {
                graph += $"\nnode{preIndex} -- node{index}";
            }
            if (this is Divide || this is Multiply || this is Plus || this is Power || this is Substract)
            {
                int pre = index;
                index++;
                graph += LeftOperand.CreateGraph(ref index, pre);
                graph += RightOperand.CreateGraph(ref index, pre);
                return graph;
            }
            else if (this is Cosine || this is Exp || this is Factorial
                || this is NaturalLogarithm || this is Sine)
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
        public TruthTable CreateTruthTable()
        {
            var data = new List<string[]>();
            for (int i = 0; i < (int)Math.Pow(2, Variables.Count); i++)
            {
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

    }
}