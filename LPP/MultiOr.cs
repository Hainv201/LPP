using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class MultiOr
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
            InterpretInputLogic();
            MultiOr_ListLogics = MultiOr_ListLogics.Distinct(new LogicComparer()).ToList();
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
    }
}
