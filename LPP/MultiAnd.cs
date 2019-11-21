﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class MultiAnd
    {
        public List<Logic> MultiAnd_ListLogics;
        public List<MultiOr> ListMultiOrs;
        public MultiAnd(List<Logic> logics, List<MultiOr> multiOrs)
        {
            if (logics == null)
            {
                MultiAnd_ListLogics = new List<Logic>();
            }
            MultiAnd_ListLogics = new List<Logic>(logics);
            if (multiOrs == null)
            {
                ListMultiOrs = new List<MultiOr>();
            }
            ListMultiOrs = new List<MultiOr>(multiOrs);
        }
        public MultiAnd(Logic logic, List<Variable> variables)
        {
            MultiAnd_ListLogics = new List<Logic>();
            MultiAnd_ListLogics.Add(logic);
            ListMultiOrs = new List<MultiOr>();
            InterpretLogic();
            ListMultiOrs = ListMultiOrs.Distinct(new MultiOrComparer()).ToList();
            MultiAnd_ListLogics = MultiAnd_ListLogics.Distinct(new LogicComparer()).ToList();
            AddVariable(variables);
        }
        public override string ToString()
        {
            string toString = "[";
            foreach (Logic logic in MultiAnd_ListLogics)
            {
                if (logic is Negation && logic.LeftOperand is Variable v)
                {
                    toString += v.Letter.ToLower();
                }
                else if (logic is Variable v1)
                {
                    toString += v1.Letter;
                }
                toString += ",";
            }
            if (ListMultiOrs.Count == 0 && MultiAnd_ListLogics.Count!=0)
            {
                toString = toString.Remove(toString.Length - 1);
            }
            return toString+ String.Join(",", ListMultiOrs) + "]";
        }
        public Logic CovertToLogic()
        {
            Logic logic = null;
            List<Logic> logics = new List<Logic>(MultiAnd_ListLogics);
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

        public bool IsExist_A_and_NotA()
        {
            foreach (Logic A in MultiAnd_ListLogics)
            {
                if (A is Variable a)
                {
                    foreach (Logic B in MultiAnd_ListLogics)
                    {
                        if (B is Negation && B.LeftOperand is Variable b)
                        {
                            if (a.Letter == b.Letter)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void InterpretLogic()
        {
            ListMultiOrs = ListMultiOrs.Distinct(new MultiOrComparer()).ToList();
            MultiAnd_ListLogics = MultiAnd_ListLogics.Distinct(new LogicComparer()).ToList();
            for (int i = 0; i < MultiAnd_ListLogics.Count; i++)
            {
                Logic logic = MultiAnd_ListLogics[i];
                if (logic is Conjunction conjunction)
                {
                    MultiAnd_ListLogics.Add(conjunction.LeftOperand);
                    MultiAnd_ListLogics.Add(conjunction.RightOperand);
                    MultiAnd_ListLogics.Remove(logic);
                    InterpretLogic();
                }
                if (logic is Disjunction disjunction)
                {
                    MultiOr multiOr = new MultiOr(logic);
                    ListMultiOrs.Add(multiOr);
                    MultiAnd_ListLogics.Remove(logic);
                    InterpretLogic();
                }
            }
        }

        private void AddVariable(List<Variable> variables)
        {
            foreach (Logic logic in MultiAnd_ListLogics)
            {
                if (logic is Variable v1)
                {
                    Variable variable = CheckExistVariable(v1.Letter, variables);
                    if (variable == null)
                    {
                        variable = v1;
                        variables.Add(variable);
                    }
                }
                if (logic is Negation && logic.LeftOperand is Variable v2)
                {
                    Variable variable = CheckExistVariable(v2.Letter, variables);
                    if (variable == null)
                    {
                        variable = v2;
                        variables.Add(variable);
                    }
                }
            }
            foreach (MultiOr multiOr in ListMultiOrs)
            {
                foreach (Logic logic in multiOr.MultiOr_ListLogics)
                {
                    if (logic is Variable v3)
                    {
                        Variable variable = CheckExistVariable(v3.Letter, variables);
                        if (variable == null)
                        {
                            variable = v3;
                            variables.Add(variable);
                        }
                    }
                    if (logic is Negation && logic.LeftOperand is Variable v4)
                    {
                        Variable variable = CheckExistVariable(v4.Letter, variables);
                        if (variable == null)
                        {
                            variable = v4;
                            variables.Add(variable);
                        }
                    }
                }
            }
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
    }
}
