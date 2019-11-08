using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class CNF
    {
        public static bool Has_Janus;
        List<string> listValues;
        MultiAnd topLayer;
        public List<Variable> Cnf_List_Variables;
        Dictionary<string, bool> appropriate_Values;
        public CNF(string input)
        {
            Has_Janus = false;
            Cnf_List_Variables = new List<Variable>();
            ParseInput(input);
            List<MultiOr> multiOrs_Toplayer = new List<MultiOr>();
            List<Logic> logics_TopLayer = new List<Logic>();
            CreateCNFTree(listValues, multiOrs_Toplayer,logics_TopLayer);
            topLayer = new MultiAnd(logics_TopLayer, multiOrs_Toplayer);
            appropriate_Values = new Dictionary<string, bool>();
        }

        public CNF(Logic logic)
        {
            Has_Janus = false;
            Cnf_List_Variables = new List<Variable>();
            appropriate_Values = new Dictionary<string, bool>();
            topLayer = new MultiAnd(logic,Cnf_List_Variables);
        }

        private void ParseInput(string input)
        {
            bool Read = false;
            listValues = new List<string>();
            string token = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '[')
                {
                    Read = true;
                }
                if (input[i] == ',' || input[i] == ']')
                {
                    Read = false;
                }
                if (input[i] != '[' && input[i] != ',' && input[i] != ']')
                {
                    if (Read)
                    {
                        token += input[i];
                    }
                }
                if (token != "" && !Read)
                {
                    listValues.Add(token);
                    token = "";
                    Read = true;
                }
            }
        }

        private void CreateCNFTree(List<string> listValues_, List<MultiOr> multiOrs_Toplayer, List<Logic> logics_TopLayer)
        {
            if (listValues_ != null)
            {
                foreach (string token in listValues_)
                {
                    if (token.Length > 1)
                    {
                        List<Logic> children_MultiOr = new List<Logic>();
                        for (int i = 0; i < token.Length; i++)
                        {
                            Logic logic = CreateLogic(token[i]);
                            children_MultiOr.Add(logic);
                        }
                        MultiOr multiOr = new MultiOr(children_MultiOr);
                        multiOrs_Toplayer.Add(multiOr);
                    }
                    else
                    {
                        Logic logic = CreateLogic(token[0]);
                        logics_TopLayer.Add(logic);
                    }
                }
            }
        }

        private Logic CreateLogic(char value)
        {
            Logic logic = null;
            if (char.IsUpper(value))
            {
                Variable variable = CheckExistVariable(value.ToString(), Cnf_List_Variables);
                if (variable == null)
                {
                    variable = new Variable(value.ToString());
                    Cnf_List_Variables.Add(variable);
                }
                logic = variable;
            }
            else if (char.IsLower(value))
            {
                logic = new Negation();
                Variable variable = CheckExistVariable(char.ToUpper(value).ToString(), Cnf_List_Variables);
                if (variable == null)
                {
                    variable = new Variable(char.ToUpper(value).ToString());
                    Cnf_List_Variables.Add(variable);
                }
                logic.LeftOperand = variable;
            }
            return logic;
        }

        private Variable CheckExistVariable(string letter, List<Variable> variables)
        {
            if (variables.Any())
            {
                foreach (Variable variable in variables)
                {
                    if (variable.Letter == letter)
                    {
                        return variable;
                    }
                }
            }
            return null;
        }

        public string CreateCNFGraph(ref int index, int preIndex = 0)
        {
            string graph = Environment.NewLine + $"node{index} [label = \"&\"]";
            preIndex = index;
            index++;
            foreach (Logic logic in topLayer.MultiAnd_ListLogics)
            {
                graph += Environment.NewLine + $"node{index} [label = \"{logic.ToString()}\"]";
                graph += Environment.NewLine + $"node{preIndex} -- node{index}";
                index++;
            }
            foreach (MultiOr multiOr in topLayer.ListMultiOrs)
            {
                graph += Environment.NewLine + $"node{index} [label = \"|\"]";
                graph += Environment.NewLine + $"node{preIndex} -- node{index}";
                int pre = index;
                foreach (Logic logic in multiOr.MultiOr_ListLogics)
                {
                    index++;
                    graph += Environment.NewLine + $"node{index} [label = \"{logic.ToString()}\"]";
                    graph += Environment.NewLine + $"node{pre} -- node{index}";
                }
                index++;
            }
            return graph;
        }
        public override string ToString()
        {
            return topLayer.ToString();
        }
        public Logic ConvertToLogic()
        {
            Logic logic = this.topLayer.CovertToLogic();
            return logic;
        }

        public void DavisPutnan(CNF cnf)
        {
            Cnf_List_Variables.Sort();
            cnf = cnf.RemoveUseless(cnf);
            Variable variable = cnf.Cnf_List_Variables.First();
            SolveNonJanus(cnf, variable);
            if (cnf.ToString().Contains(variable.Letter))
            {
                cnf = Resolution(cnf, variable.Letter);
            }
            if (cnf.ToString().Contains(variable.Letter.ToLower()))
            {
                cnf = Resolution(cnf, variable.Letter.ToLower());
            }
            cnf.Cnf_List_Variables.Remove(variable);
            if (cnf.Cnf_List_Variables.Count != 0 && !Has_Janus)
            {
                DavisPutnan(cnf);
            }
        }

        private CNF RemoveUseless(CNF cnf)
        {
            cnf.topLayer.ListMultiOrs.RemoveAll(x => x.CheckOpposite());
            return cnf;
        }

        private void SolveNonJanus(CNF cnf,Variable v)
        {
            if (!cnf.IsSatisfiable() && cnf.topLayer.ListMultiOrs.Count == 0)
            {
                Has_Janus = true;
            }
            if (CheckVaribleExistInsideNegation(cnf.topLayer.MultiAnd_ListLogics,v) == 1)
            {
                if (!appropriate_Values.ContainsKey(v.Letter))
                {
                    appropriate_Values.Add(v.Letter, true);
                }
            }
            if (CheckVaribleExistInsideNegation(cnf.topLayer.MultiAnd_ListLogics, v) == 2)
            {
                if (!appropriate_Values.ContainsKey(v.Letter.ToLower()))
                {
                    appropriate_Values.Add(v.Letter.ToLower(), false);
                }
            }
            string multiorString = String.Join("",cnf.topLayer.ListMultiOrs);
            if (multiorString.Contains(v.Letter) && multiorString.Contains(v.Letter.ToLower()))
            {
                if (!appropriate_Values.ContainsKey(v.Letter) && !appropriate_Values.ContainsKey(v.Letter.ToLower()))
                {
                    appropriate_Values.Add(v.Letter, true);
                    appropriate_Values.Add(v.Letter.ToLower(), false);
                }
            }
            if (multiorString.ToString().Contains(v.Letter) && !multiorString.ToString().Contains(v.Letter.ToLower()))
            {
                if (!appropriate_Values.ContainsKey(v.Letter))
                {
                    appropriate_Values.Add(v.Letter, true);
                }
            }
            if (!multiorString.ToString().Contains(v.Letter) && multiorString.ToString().Contains(v.Letter.ToLower()))
            {
                if (!appropriate_Values.ContainsKey(v.Letter.ToLower()))
                {
                    appropriate_Values.Add(v.Letter.ToLower(), false);
                }
            }
            if (cnf.ToString() == "[]" && !Has_Janus)
            {
                if (!appropriate_Values.ContainsKey(v.Letter))
                {
                    appropriate_Values.Add(v.Letter, true);
                }
            }
        }

        private CNF Resolution(CNF cnf, string v)
        {
            CNF clone_cnf = cnf;
            List<string> lower = new List<string>();
            List<string> upper = new List<string>();
            foreach (MultiOr multiOr in clone_cnf.topLayer.ListMultiOrs)
            {
                if (v.All(char.IsUpper))
                {
                    if (multiOr.ToString().Contains(v))
                    {
                        upper.Add(multiOr.ToString().Replace(v, ""));
                    }
                    if (multiOr.ToString().Contains(v.ToLower()))
                    {
                        lower.Add(multiOr.ToString().Replace(v.ToLower(), ""));
                    }
                }
                else
                {
                    if (multiOr.ToString().Contains(v))
                    {
                        lower.Add(multiOr.ToString().Replace(v, ""));
                    }
                    if (multiOr.ToString().Contains(v.ToUpper()))
                    {
                        upper.Add(multiOr.ToString().Replace(v.ToUpper(), ""));
                    }
                }
            }
            foreach (Logic logic in clone_cnf.topLayer.MultiAnd_ListLogics)
            {
                if (v.All(char.IsUpper))
                {
                    if (logic is Variable v1)
                    {
                        if (v1.Letter == v)
                        {
                            upper.Add("");
                        }
                    }
                }
                else
                {
                    if (logic is Negation && logic.LeftOperand is Variable v2)
                    {
                        if (v2.Letter == v.ToUpper())
                        {
                            lower.Add("");
                        }
                    }
                }
            }
            if (v.All(char.IsUpper))
            {
                clone_cnf.topLayer.MultiAnd_ListLogics.RemoveAll(x => x.ToString().Contains(v));
            }
            else
            {
                clone_cnf.topLayer.MultiAnd_ListLogics.RemoveAll(x => x.ToString().Contains(v.ToUpper()));
            }
            clone_cnf.topLayer.ListMultiOrs.RemoveAll(x => x.ToString().Contains(v.ToLower()));
            clone_cnf.topLayer.ListMultiOrs.RemoveAll(x => x.ToString().Contains(v));
            List<string> newMultiAnd_String = new List<string>();
            foreach (string a in upper)
            {
                foreach (string b in lower)
                {
                    newMultiAnd_String.Add(new String((a + b).Distinct().ToArray()));
                }
            }
            newMultiAnd_String.RemoveAll(x=>x =="");
            for (int i = 0; i < newMultiAnd_String.Count; i++)
            {
                for (int j = i + 1; j < newMultiAnd_String.Count; j++)
                {
                    if (SameChar(newMultiAnd_String[i], newMultiAnd_String[j]))
                    {
                        newMultiAnd_String.Remove(newMultiAnd_String[j]);
                    }
                }
            }
            CreateCNFTree(newMultiAnd_String, clone_cnf.topLayer.ListMultiOrs,clone_cnf.topLayer.MultiAnd_ListLogics);
            clone_cnf.topLayer.MultiAnd_ListLogics = clone_cnf.topLayer.MultiAnd_ListLogics.Distinct(new LogicComparer()).ToList();
            return clone_cnf;
        }

        public bool IsSatisfiable()
        {
            return !topLayer.IsExist_A_and_NotA();
        }

        public Dictionary<string,bool> GetAppropriateValue()
        {
            return appropriate_Values;
        }

        private bool SameChar(string firstString, string secondString)
        {
            char[] first = firstString.ToCharArray();
            char[] second = secondString.ToCharArray();
            Array.Sort(first);
            Array.Sort(second);
            return first.SequenceEqual(second);
        }

        private int CheckVaribleExistInsideNegation(List<Logic> logics, Variable variable)
        {
            int value = 0;
            if (logics.Contains(variable))
            {
                value = 1;
            }
            if (logics.Any())
            {
                foreach (Logic logic in logics)
                {
                    if (logic is Negation && logic.LeftOperand == variable)
                    {
                        value = 2;
                    }
                }
            }
            return value;
        }
    }
}
