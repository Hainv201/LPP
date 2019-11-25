using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    class MultiAnd
    {
        public List<MultiOr> ListMultiOrs;
        public MultiAnd(List<MultiOr> multiOrs)
        {
            if (multiOrs == null)
            {
                ListMultiOrs = new List<MultiOr>();
            }
            ListMultiOrs = new List<MultiOr>(multiOrs);
        }
        public MultiAnd(Logic logic)
        {
            ListMultiOrs = new List<MultiOr>();
            List<Logic> Temp_List = new List<Logic>();
            Temp_List.Add(logic);
            InterpretLogic(Temp_List);
            ListMultiOrs = ListMultiOrs.Distinct(new MultiOrComparer()).ToList();
        }
        public override string ToString()
        {
            return "[" + String.Join(",", ListMultiOrs) + "]";
        }
        public Logic CovertToLogic()
        {
            Logic logic = null;
            List<Logic> logics = new List<Logic>();
            foreach (MultiOr multiOr in ListMultiOrs)
            {
                logics.Add(multiOr.CovertToLogic());
            }
            if (logics.Count >1)
            {
                logic = CreateTree(logic, logics, 0);
            }
            else
            {
                logic = logics[0];
            }
            return logic;
        }

        private Logic CreateTree(Logic logic, List<Logic> logics, int i)
        {
            if (i < logics.Count)
            {
                Logic tmp = new Conjunction();
                logic = tmp;
                logic.LeftOperand = logics[i];
                if (i == logics.Count - 2)
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

        private void InterpretLogic(List<Logic> logics)
        {
            ListMultiOrs = ListMultiOrs.Distinct(new MultiOrComparer()).ToList();
            while(logics.Count != 0)
            {
                Logic logic = logics.First();
                if (logic is Conjunction conjunction)
                {
                    logics.Add(conjunction.LeftOperand);
                    logics.Add(conjunction.RightOperand);
                    logics.Remove(logic);
                    InterpretLogic(logics);
                }
                else
                {
                    MultiOr multiOr = new MultiOr(logic);
                    multiOr.GetCNF();
                    ListMultiOrs.Add(multiOr);
                    logics.Remove(logic);
                    InterpretLogic(logics);
                }
            }
        }
    }
}
