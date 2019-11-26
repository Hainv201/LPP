using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class CNF
    {
        static bool Has_Janus;
        List<string> listValues;
        MultiAnd topLayer;
        public List<Variable> Cnf_List_Variables;
        static Dictionary<string, bool> appropriate_Values;
        static int count;
        static string step;
        static string sub_step;

        //Constructor for the string input [Abc]
        public CNF(string input)
        {
            count = 1;
            step = "";
            sub_step = "";
            Has_Janus = false;
            Cnf_List_Variables = new List<Variable>();
            List<MultiOr> multiOrs_Toplayer = new List<MultiOr>();
            appropriate_Values = new Dictionary<string, bool>();
            ParseInput(input);
            CreateCNFTree(listValues, multiOrs_Toplayer);
            topLayer = new MultiAnd(multiOrs_Toplayer);
            Cnf_List_Variables.Sort();
        }

        // Constructor for a logic
        public CNF(Logic logic)
        {
            count = 1;
            step = "";
            sub_step = "";
            Has_Janus = false;
            Cnf_List_Variables = new List<Variable>();
            appropriate_Values = new Dictionary<string, bool>();
            topLayer = new MultiAnd(logic);
        }

        //Parse Input in [aB,c] form
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

        private void CreateCNFTree(List<string> listValues_, List<MultiOr> multiOrs_Toplayer)
        {
            if (listValues_ != null)
            {
                foreach (string token in listValues_)
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
        
        //Convert CNF to Logic
        public Logic ConvertToLogic()
        {
            Logic logic = this.topLayer.CovertToLogic();
            return logic;
        }

        public void DavisPutnan(CNF clone_cnf)
        {
            step += $"Davis-Putnan [{count}] {clone_cnf}" + " - Var: " + String.Join("", clone_cnf.Cnf_List_Variables) + Environment.NewLine;
            clone_cnf = clone_cnf.RemoveUseless(clone_cnf);
            step += $"Remove Useless [{count}]: {clone_cnf}" + Environment.NewLine;
            Variable variable = clone_cnf.Cnf_List_Variables.First();
            clone_cnf = clone_cnf.SolveNonJanus(clone_cnf, variable);
            clone_cnf.Cnf_List_Variables.Remove(variable);
            clone_cnf = Resolution(clone_cnf, variable);
            count++;
            if (clone_cnf.Cnf_List_Variables.Count != 0 && !Has_Janus)
            {
                clone_cnf.DavisPutnan(clone_cnf);
            }
        }

        public CNF RemoveUseless(CNF cnf)
        {
            cnf.topLayer.ListMultiOrs.RemoveAll(x => x.CheckOpposite());
            return cnf;
        }

        public CNF SolveNonJanus(CNF cnf, Variable v)
        {
            string multiorString = String.Join("", cnf.topLayer.ListMultiOrs);
            if (multiorString.Contains(v.Letter) && multiorString.Contains(v.Letter.ToLower()))
            {
                if (!appropriate_Values.ContainsKey(v.Letter) && !appropriate_Values.ContainsKey(v.Letter.ToLower()))
                {
                    appropriate_Values.Add(v.Letter, false);
                    step += $"Solve Non Janus [{count}] on {v}: {cnf}" + Environment.NewLine;
                    sub_step += $"Substitute Solution [{count}] [{v.Letter.ToLower()}]" + Environment.NewLine;
                    sub_step += $"Solve Non Janus [{count}]: {v} = False" + Environment.NewLine;
                }
            }
            else if (multiorString.ToString().Contains(v.Letter) && !multiorString.ToString().Contains(v.Letter.ToLower()))
            {
                if (!appropriate_Values.ContainsKey(v.Letter))
                {
                    appropriate_Values.Add(v.Letter, true);
                    cnf.topLayer.ListMultiOrs.RemoveAll(x => x.ToString().Contains(v.Letter));
                    step += $"Solve Non Janus [{count}] on {v}: {v} = True, {cnf}" + Environment.NewLine;
                }
            }
            else if (!multiorString.ToString().Contains(v.Letter) && multiorString.ToString().Contains(v.Letter.ToLower()))
            {
                if (!appropriate_Values.ContainsKey(v.Letter.ToLower()))
                {
                    appropriate_Values.Add(v.Letter, false);
                    cnf.topLayer.ListMultiOrs.RemoveAll(x => x.ToString().Contains(v.Letter.ToLower()));
                    step += $"Solve Non Janus [{count}] on {v}: {v} = False, {cnf}" + Environment.NewLine;
                }
            }
            else if ((cnf.ToString() == "[]" || cnf.ToString() == "[True]") && !Has_Janus)
            {
                step += $"Solve Non Janus [{count}] on {v}: {cnf}" + Environment.NewLine;
            }
            return cnf;
        }

        public CNF Resolution(CNF cnf, Variable v)
        {
            List<string> lower = new List<string>();
            List<string> upper = new List<string>();
            //Get All MultiOrs contain the Variable
            foreach (MultiOr multiOr in cnf.topLayer.ListMultiOrs)
            {
                if (multiOr.ToString().Contains(v.Letter))
                {
                    upper.Add(multiOr.ToString().Replace(v.Letter, ""));
                }
                if (multiOr.ToString().Contains(v.Letter.ToLower()))
                {
                    lower.Add(multiOr.ToString().Replace(v.Letter.ToLower(), ""));
                }
            }
            cnf.topLayer.ListMultiOrs.RemoveAll(x => x.ToString().Contains(v.Letter));
            cnf.topLayer.ListMultiOrs.RemoveAll(x => x.ToString().Contains(v.Letter.ToLower()));
            //Resolution
            List<string> newMultiAnd_String = new List<string>();
            foreach (string a in upper)
            {
                foreach (string b in lower)
                {
                    newMultiAnd_String.Add(new String((a + b).Distinct().ToArray()));
                }
            }
            if (newMultiAnd_String.Contains(""))
            {
                Has_Janus = true;
                step += $"Has Janus: UNSAT" + Environment.NewLine;
                return cnf;
            }
            cnf.CreateCNFTree(newMultiAnd_String, cnf.topLayer.ListMultiOrs);
            cnf.topLayer.ListMultiOrs = cnf.topLayer.ListMultiOrs.Distinct(new MultiOrComparer()).ToList();
            if (cnf.Cnf_List_Variables.Count >= 1)
            {
                step += $"Resolution [{count}] on {v}: {cnf}" + Environment.NewLine;
            }
            if (cnf.ToString() == "[]" || cnf.ToString() == "[True]" )
            {
                if (!appropriate_Values.ContainsKey(v.Letter))
                {
                    step += $"Resolution [{count}] on {v}: {cnf}" + Environment.NewLine;
                    appropriate_Values.Add(v.Letter, true);
                    step += $"Choose {v} = True" + Environment.NewLine;
                }
            }
            return cnf;
        }

        public Dictionary<string,bool> GetAppropriateValue()
        {
            return appropriate_Values;
        }

        public string ShowStep()
        {
            string final_step = step + sub_step;
            if (Has_Janus)
            {
                string result = step;
                step = "";
                sub_step = "";
                count = 1;
                appropriate_Values = new Dictionary<string, bool>();
                Has_Janus = false;
                return result;
            }
            step = "";
            sub_step = "";
            count = 1;
            appropriate_Values = new Dictionary<string, bool>();
            Has_Janus = false;
            return final_step;
        }

        public bool GetHasJanusValue()
        {
            return Has_Janus;
        }
    }
}
