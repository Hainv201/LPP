using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class MultiOr
    {
        public List<Logic> MultiOr_ListLogics;
        public MultiOr(List<Logic> logics)
        {
            if (logics == null)
            {
                MultiOr_ListLogics = new List<Logic>();
            }
            MultiOr_ListLogics = new List<Logic>(logics);
        }
        public MultiOr(Logic logic)
        {
            MultiOr_ListLogics = new List<Logic>();
            MultiOr_ListLogics.Add(logic);
        }
        public override string ToString()
        {
            string toString = "";
            foreach (Logic logic in MultiOr_ListLogics)
            {
                if (logic is Negation && logic.LeftOperand is Variable v)
                {
                    toString += v.Letter.ToLower();
                }
                else if (logic is Variable v1)
                {
                    toString += v1.Letter;
                }
                else
                {
                    toString += logic.ToString();
                }
            }
            return toString;
        }

        public bool CheckOpposite()
        {
            string cnf = ToString();
            for (int i = 0; i < cnf.Length; i++)
            {
                for (int j = i+1; j < cnf.Length; j++)
                {
                    if (char.ToUpper(cnf[i]) == char.ToUpper(cnf[j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Logic CovertToLogic()
        {
            Logic logic = null;
            if (MultiOr_ListLogics.Count >1)
            {
                logic = CreateTree(logic, MultiOr_ListLogics, 0);
            }
            else
            {
                logic = MultiOr_ListLogics.First();
            }
            return logic;
        }

        
        private Logic CreateTree(Logic logic, List<Logic> logics, int i)
        {
            if (i < logics.Count)
            {
                Logic tmp = new Disjunction();
                logic = tmp;
                logic.LeftOperand = logics[i];
                if (i == logics.Count -2)
                {
                    logic.RightOperand = logics[i+1];
                }
                else
                {
                    logic.RightOperand = CreateTree(logic, logics, i + 1);

                }
            }
            return logic;
        }

        // Get CNF Form
        public void GetCNF()
        {
            InterpretInputLogic();
            MultiOr_ListLogics = MultiOr_ListLogics.Distinct(new LogicComparer()).ToList();
        }

        private void InterpretInputLogic()
        {
            MultiOr_ListLogics = MultiOr_ListLogics.Distinct(new LogicComparer()).ToList();
            for (int i = 0; i < MultiOr_ListLogics.Count; i++)
            {
                Logic logic = MultiOr_ListLogics[i];
                if (logic is Disjunction disjunction)
                {
                    MultiOr_ListLogics.Add(disjunction.LeftOperand);
                    MultiOr_ListLogics.Add(disjunction.RightOperand);
                    MultiOr_ListLogics.Remove(logic);
                    InterpretInputLogic();
                }
            }
        }

        public void GetMultiOrAfterDavisPutnam(Dictionary<string,string> appropriateValues)
        {
            for (int i = 0; i < MultiOr_ListLogics.Count; i++)
            {
                var logic = MultiOr_ListLogics[i];
                if (logic is Negation && logic.LeftOperand is Variable v)
                {
                    var value = appropriateValues.FirstOrDefault(x => x.Key == v.Letter).Value;
                    if (value == "True")
                    {
                        MultiOr_ListLogics.Remove(logic);
                        GetMultiOrAfterDavisPutnam(appropriateValues);
                    }
                    else if(value == "False")
                    {
                        MultiOr_ListLogics.Clear();
                        break;
                    }
                }
                else if (logic is Variable v1)
                {
                    var value = appropriateValues.FirstOrDefault(x => x.Key == v1.Letter).Value;
                    if (value == "True")
                    {
                        MultiOr_ListLogics.Clear();
                        break;
                    }
                    else if(value == "False")
                    {
                        MultiOr_ListLogics.Remove(logic);
                        GetMultiOrAfterDavisPutnam(appropriateValues);
                    }
                }
            }
        }
    }
}
