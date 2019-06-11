using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    public class Formula
    {
        public Proposition RootProposition { get; set; }
        List<string> listNotations;
        public List<Variable> Variables { get; private set; }
        public List<Variable> BoundVariables { get; private set; }
        public Formula(string inputtedfunction)
        {
            Parsing(inputtedfunction);
            RootProposition = CreateTree();
        }
        private void Parsing(string inputtedfunction)
        {
            listNotations = new List<string>();
            inputtedfunction = inputtedfunction.Replace(" ", "");
            int predicateIndex = -1;
            bool IsAfterPredicate = false;
            for (int i = 0; i < inputtedfunction.Length; i++)
            {
                string token = "";
                if (inputtedfunction[i] != '(' && inputtedfunction[i] != ')' && inputtedfunction[i] != ',' && inputtedfunction[i] != '.')
                {
                    token += inputtedfunction[i];
                }
                else if (inputtedfunction[i] == ')' && IsAfterPredicate)
                {
                    IsAfterPredicate = false;
                }
                if (predicateIndex != -1 && !IsAfterPredicate)
                {
                    int nrPredicateVariables = listNotations.Count - predicateIndex - 1;
                    listNotations[predicateIndex] = listNotations[predicateIndex] + nrPredicateVariables;
                    predicateIndex = -1;
                }
                if (token !="")
                {
                    if (token.Any(char.IsUpper))
                    {
                        predicateIndex = listNotations.Count;
                        IsAfterPredicate = true;
                    }
                    listNotations.Add(token);
                }
            }
        }
        private Proposition CreateTree()
        {
            Variables = new List<Variable>();
            Proposition currentNode = null;
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
            else if (token.Any(char.IsUpper))
            {
                currentNode = new Predicate(token[0].ToString());
                int nrofvariables = Convert.ToInt32(token[1].ToString());
                listNotations.Remove(token);
                for (int i = 0; i < nrofvariables; i++)
                {
                    Variable v = new Variable(listNotations.First());
                    ((Predicate)currentNode).ObjectVariables.Add(v);
                    Variables.Add(v);
                    listNotations.RemoveAt(0);
                }
            }
            //1 operand
            else if (token.Contains("@"))
            {
                currentNode = new Universal();
                listNotations.Remove(token);
                Variable v = new Variable(listNotations.First());
                ((Universal)currentNode).BoundVariables.Add(v);
                Variables.Add(v);
                listNotations.RemoveAt(0);
                currentNode.LeftOperand = CreateTree();
            }
            else if (token.Contains("!"))
            {
                currentNode = new Existential();
                listNotations.Remove(token);
                Variable v = new Variable(listNotations.First());
                ((Existential)currentNode).BoundVariables.Add(v);
                Variables.Add(v);
                listNotations.RemoveAt(0);
                currentNode.LeftOperand = CreateTree();
            }
            // no operand
            else
            {
                currentNode = new Variable(token);
                listNotations.Remove(token);
            }
            return currentNode;
        }
    }
}
