using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    public class Formula
    {
        public Logic RootProposition { get; set; }
        public bool IsPredicate = false;
        List<string> listNotations;
        public List<Variable> Variables;
        public List<Variable> BoundVariables;
        List<string> listPredicate;
        public Formula(string inputtedfunction)
        {
            listPredicate = new List<string>();
            Variables = new List<Variable>();
            BoundVariables = new List<Variable>();
            Parsing(inputtedfunction);
            RootProposition = CreateTree();
            Variables.Sort();
            BoundVariables.Sort();
        }
        public Formula()
        {

        }
        private void Parsing(string inputtedfunction)
        {
            listNotations = new List<string>();
            int predicateIndex = -1;
            bool IsAfterPredicate = false;
            string token = "";
            bool Read = true;
            for (int i = 0; i < inputtedfunction.Length; i++)
            {
                if (inputtedfunction[i] != '(' && inputtedfunction[i] != ')' && inputtedfunction[i] != ',' && inputtedfunction[i] != '.')
                {
                    token += inputtedfunction[i];
                }
                else if (inputtedfunction[i] == ')' && IsAfterPredicate)
                {
                    IsAfterPredicate = false;
                    
                }
                if (inputtedfunction[i] == '(' || inputtedfunction[i] != ')' || inputtedfunction[i] != ',' || inputtedfunction[i] != '.'|| i == inputtedfunction.Length -1)
                {
                    Read = false;
                }
                if (predicateIndex != -1 && !IsAfterPredicate)
                {
                    int nrPredicateVariables = listNotations.Count - predicateIndex - 1;
                    listNotations[predicateIndex] = listNotations[predicateIndex] + nrPredicateVariables;
                    predicateIndex = -1;
                }
                if (token != "" && !Read)
                {
                    if (token.Any(char.IsUpper) && inputtedfunction[i + 1] == '(')
                    {
                        predicateIndex = listNotations.Count;
                        IsAfterPredicate = true;
                    }
                    listNotations.Add(token);
                    Read = true;
                    token = "";
                }
            }
        }
        private Logic CreateTree()
        {
            Logic currentNode = null;
            string token = listNotations.First();
            // 2 operands
            if (token == "=")
            {
                currentNode = new BiImplication();
                listNotations.Remove(token);
                currentNode.LeftOperand = CreateTree();
                currentNode.RightOperand = CreateTree();
            }
            else if (token == "&")
            {
                currentNode = new Conjunction();
                listNotations.Remove(token);
                currentNode.LeftOperand = CreateTree();
                currentNode.RightOperand = CreateTree();
            }
            else if (token == "|")
            {
                currentNode = new Disjunction();
                listNotations.Remove(token);
                currentNode.LeftOperand = CreateTree();
                currentNode.RightOperand = CreateTree();
            }
            else if (token == ">")
            {
                currentNode = new Implication();
                listNotations.Remove(token);
                currentNode.LeftOperand = CreateTree();
                currentNode.RightOperand = CreateTree();
            }
            else if (token == "%")
            {
                currentNode = new NotAnd();
                listNotations.Remove(token);
                currentNode.LeftOperand = CreateTree();
                currentNode.RightOperand = CreateTree();
            }
            // 1 operand
            else if (token == "~")
            {
                currentNode = new Negation();
                listNotations.Remove(token);
                currentNode.LeftOperand = CreateTree();
            }
            // No operand
            else if (token == "1")
            {
                currentNode = new True();
                listNotations.Remove(token);
            }
            else if (token == "0")
            {
                currentNode = new False();
                listNotations.Remove(token);
            }
            else if (token.Any(char.IsUpper) && token.Length !=1)
            {
                if (!CheckValidPredicate(token))
                {
                    throw new Exception("Invalid Formula!!!");
                }
                listPredicate.Add(token);
                IsPredicate = true;
                currentNode = new Predicate(token[0].ToString());
                int nrofvariables = Convert.ToInt32(token[1].ToString());
                listNotations.Remove(token);
                for (int i = 0; i < nrofvariables; i++)
                {
                    Variable variable = CheckExistVariable(listNotations.First(), Variables);
                    if (variable == null)
                    {
                        variable = new Variable(listNotations.First());
                        Variables.Add(variable);
                    }
                    ((Predicate)currentNode).ObjectVariables.Add(variable);
                    listNotations.RemoveAt(0);
                }
            }
            //1 operand
            else if (token.Contains("@"))
            {
                IsPredicate = true;
                currentNode = new Universal();
                listNotations.Remove(token);
                Variable variable = CheckExistVariable(listNotations.First(), Variables);
                if (variable == null)
                {
                    variable = new Variable(listNotations.First());
                    Variables.Add(variable);
                    BoundVariables.Add(variable);
                }
                ((Universal)currentNode).BoundVariables.Add(variable);
                listNotations.RemoveAt(0);
                currentNode.LeftOperand = CreateTree();
            }
            else if (token.Contains("!"))
            {
                IsPredicate = true;
                currentNode = new Existential();
                listNotations.Remove(token);
                Variable variable = CheckExistVariable(listNotations.First(), Variables);
                if (variable == null)
                {
                    variable = new Variable(listNotations.First());
                    Variables.Add(variable);
                    BoundVariables.Add(variable);
                }
                ((Existential)currentNode).BoundVariables.Add(variable);
                listNotations.RemoveAt(0);
                currentNode.LeftOperand = CreateTree();
            }
            // no operand
            else
            {
                Variable variable = CheckExistVariable(token,Variables);
                if (variable == null)
                {
                    variable = new Variable(token);
                    Variables.Add(variable);
                }
                currentNode = variable;
                listNotations.Remove(token);
            }
            return currentNode;
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

        private bool CheckValidPredicate(string predicate)
        {
            if (listPredicate.Any())
            {
                foreach (string item in listPredicate)
                {
                    if(item[0] == predicate[0])
                    {
                        if(item[1] != predicate[1])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
