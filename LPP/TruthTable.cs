using System;
using System.Collections.Generic;

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
        //
    }
}