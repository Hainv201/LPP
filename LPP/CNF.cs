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
        static Dictionary<string, string> appropriate_Values;
        static string step;

        //Constructor for the string input [Abc]
        public CNF(string input)
        {
            step = "";
            Has_Janus = false;
            Cnf_List_Variables = new List<Variable>();
            List<MultiOr> multiOrs_Toplayer = new List<MultiOr>();
            appropriate_Values = new Dictionary<string, string>();
            ParseInput(input);
            CreateCNFTree(listValues, multiOrs_Toplayer);
            topLayer = new MultiAnd(multiOrs_Toplayer);
            Cnf_List_Variables.Sort();
        }

        // Constructor for a logic
        public CNF(Logic logic)
        {
            step = "";
            Has_Janus = false;
            Cnf_List_Variables = new List<Variable>();
            appropriate_Values = new Dictionary<string, string>();
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
            CNF processedCNF = ObjectExtension.CopyObject<CNF>(clone_cnf);
            step += $"Davis-Putnan {clone_cnf}" + " - Var: " + String.Join("", clone_cnf.Cnf_List_Variables) + Environment.NewLine;
            CNF afterRemoveUseLess = RemoveUseless(clone_cnf);
            step += $"Remove Useless: {afterRemoveUseLess}" + Environment.NewLine;
            Variable variable = clone_cnf.Cnf_List_Variables.First();
            CNF afterSolveNonJanus = SolveNonJanus(afterRemoveUseLess, variable);
            clone_cnf.Cnf_List_Variables.Remove(variable);
            CNF afterResolution = Resolution(afterSolveNonJanus, variable);
            if (clone_cnf.Cnf_List_Variables.Count != 0 && !Has_Janus)
            {
                DavisPutnan(afterResolution);
            }
            if (!Has_Janus)
            {
                SubtituteSolution(processedCNF);
            }
        }

        public CNF RemoveUseless(CNF cnf)
        {
            cnf.topLayer.ListMultiOrs.RemoveAll(x => x.CheckOpposite());
            return cnf;
        }

        public CNF SolveNonJanus(CNF cnf, Variable v)
        {
            if (cnf.ToString().Contains(v.Letter) && !cnf.ToString().Contains(v.Letter.ToLower()))
            {
                if (!appropriate_Values.ContainsKey(v.Letter))
                {
                    appropriate_Values.Add(v.Letter, "True");
                    cnf.topLayer.ListMultiOrs.RemoveAll(x => x.ToString().Contains(v.Letter));
                    step += $"Solve Non Janus on {v}: {v} = True, {cnf}" + Environment.NewLine;
                }
            }
            else if (!cnf.ToString().Contains(v.Letter) && cnf.ToString().Contains(v.Letter.ToLower()))
            {
                if (!appropriate_Values.ContainsKey(v.Letter))
                {
                    appropriate_Values.Add(v.Letter, "False");
                    cnf.topLayer.ListMultiOrs.RemoveAll(x => x.ToString().Contains(v.Letter.ToLower()));
                    step += $"Solve Non Janus on {v}: {v} = False, {cnf}" + Environment.NewLine;
                }
            }
            else
            {
                step += $"Solve Non Janus on {v}: {cnf}" + Environment.NewLine;
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
            step += $"Resolution on {v}: {cnf}" + Environment.NewLine;
            if (cnf.ToString() == "[]" || cnf.ToString() == "[True]")
            {
                if (!appropriate_Values.ContainsKey(v.Letter))
                {
                    appropriate_Values.Add(v.Letter, "True");
                    step += $"Choose {v} = True" + Environment.NewLine;
                }
            }
            return cnf;
        }

        public CNF SubtituteSolution(CNF cNF)
        {
            cNF.topLayer.ListMultiOrs.ForEach(x => { x.GetMultiOrAfterDavisPutnam(appropriate_Values); });
            cNF.topLayer.ListMultiOrs.RemoveAll(x => x.ToString() == "");
            cNF = RemoveUseless(cNF);
            step += $"Substitute Solution [{cNF}]" + Environment.NewLine;
            if (cNF.ToString() != "[]")
            {
                Variable variable = cNF.Cnf_List_Variables.First();
                cNF.Cnf_List_Variables.Remove(variable);
                cNF = SolveNonJanus(cNF, variable);
            }
            return cNF;
        }
        public Dictionary<string,string> GetAppropriateValue()
        {
            return appropriate_Values;
        }

        public string ShowStep()
        {
            string result = step;
            step = "";
            appropriate_Values = new Dictionary<string, string>();
            Has_Janus = false;
            return result;
        }

        public bool GetHasJanusValue()
        {
            return Has_Janus;
        }
    }
}
