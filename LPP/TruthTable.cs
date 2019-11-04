using System;
using System.Collections.Generic;
using System.Linq;

namespace LPP
{
    public class TruthTable
    {
        public List<Variable> ListVariables { get; private set; }
        public List<string[]> Data { get; private set; }
        public TruthTable(List<Variable> variables, List<string[]> data)
        {
            ListVariables = variables;
            Data = data;
        }
        //Hash Code
        public string GetTruthTableHashCode()
        {
            string result = "";
            for (int i = Data.Count - 1; i >= 0; i--)
            {
                result += Data[i][Data[0].Length - 1];
            }
            string hexCode = Convert.ToInt64(result, 2).ToString("X");
            return hexCode;
        }
        // Simplify
        public TruthTable Simplify()
        {
            List<string[]> zerotable = new List<string[]>();
            List<string[]> onetable = new List<string[]>();
            foreach (string[] row in Data)
            {
                switch (row[row.Length - 1])
                {
                    case "0":
                        zerotable.Add(row);
                        break;
                    case "1":
                        onetable.Add(row);
                        break;
                }
            }
            List<string[]> datatable = SimplifyData(zerotable).Concat(SimplifyData(onetable)).ToList();
            return new TruthTable(ListVariables, datatable);
        }
        private List<string[]> SimplifyData(List<string[]> data, int count = 0)
        {
            count++;
            List<string[]> simplifiedData = new List<string[]>();
            for (int i = 0; i < data.Count; i++)
            {
                bool isSimplifiable = false;
                for (int j = 0; j < data.Count; j++)
                {
                    List<int> different_Indexes = new List<int>();
                    for (int k = 0; k < data[i].Length; k++)
                    {
                        if (data[i][k] != data[j][k])
                        {
                            different_Indexes.Add(k);
                        }
                    }
                    if (different_Indexes.Count == 1)
                    {
                        string[] simplifiedRow = (string[])data[i].Clone();
                        simplifiedRow[different_Indexes[0]] = "*";
                        if (!simplifiedData.Any(simplifiedRow.SequenceEqual))
                        {
                            simplifiedData.Add(simplifiedRow);
                        }
                        isSimplifiable = true;
                    }
                }
                if (!isSimplifiable && !simplifiedData.Any(data[i].SequenceEqual))
                {
                    simplifiedData.Add(data[i]);
                }
            }
            if (count < ListVariables.Count)
            {
                return SimplifyData(simplifiedData, count);
            }
            return simplifiedData;
        }

        //Create Disjunctive Formula
        public Logic CreateDisjunctiveFormula()
        {
            Logic root = null;
            for (int i = 0; i< Data.Count; i++)
            {
                Logic temporary = null;
                if (Data[i][Data[i].Length - 1] == "1")
                {
                    for (int j =0; j< Data[i].Length-1; j++)
                    {
                        if(temporary == null)
                        {
                            if (Data[i][j] == "0")
                            {
                                temporary = new Negation();
                                temporary.LeftOperand = ListVariables[j];
                            }
                            else if (Data[i][j] == "1")
                            {
                                temporary = ListVariables[j];
                            }
                        }
                        else
                        {
                            Logic left_Operand = temporary;
                            Logic right_Operand = null;
                            if (Data[i][j] == "0")
                            {
                                right_Operand = new Negation();
                                right_Operand.LeftOperand = ListVariables[j];
                            }
                            else if (Data[i][j] == "1")
                            {
                                right_Operand = ListVariables[j];
                            }
                            if(right_Operand != null)
                            {
                                temporary = new Conjunction();
                                temporary.LeftOperand = left_Operand;
                                temporary.RightOperand = right_Operand;
                            }
                        }
                    }
                }
                if(temporary!= null)
                {
                    if(root == null)
                    {
                        root = temporary;
                    }
                    else
                    {
                        Logic Left = root;
                        Logic Right = temporary;
                        root = new Disjunction();
                        root.LeftOperand = Left;
                        root.RightOperand = Right;
                    }
                }
            }
            return root;
        }
    }
}