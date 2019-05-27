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
        List<char> listNotations;
        private bool isPredicate;
        public List<Variable> Variables { get; private set; }
        public List<Variable> BoundVariables { get; private set; }
        public Formula(string inputtedfunction)
        {
            Parsing(inputtedfunction);
            RootProposition = CreateTree();
        }
        private void Parsing(string inputtedfunction)
        {
            listNotations = new List<char>();
            inputtedfunction = inputtedfunction.Replace(" ", "");
            for (int i = 0; i < inputtedfunction.Length; i++)
            {
                while (inputtedfunction[i] != '(' && inputtedfunction[i] != ')' && inputtedfunction[i] != ',')
                {
                    listNotations.Add(inputtedfunction[i]);
                    i++;
                }
            }
        }
        private Proposition CreateTree()
        {
            Variables = new List<Variable>();
            BoundVariables = new List<Variable>();
            isPredicate = false;
            List<Proposition> WaitingProp = new List<Proposition>();
            Proposition currentNode = null;
            char token = listNotations.First();
            switch (token)
            {
                // 2 operands
                case '=':
                    currentNode = new BiImplication();
                    listNotations.Remove(token);
                    currentNode.LeftOperand = CreateTree();
                    currentNode.RightOperand = CreateTree();
                    break;
                case '&':
                    currentNode = new Conjunction();
                    listNotations.Remove(token);
                    currentNode.LeftOperand = CreateTree();
                    currentNode.RightOperand = CreateTree();
                    break;
                case '|':
                    currentNode = new Disjunction();
                    listNotations.Remove(token);
                    currentNode.LeftOperand = CreateTree();
                    currentNode.RightOperand = CreateTree();
                    break;
                case '>':
                    currentNode = new Implication();
                    listNotations.Remove(token);
                    currentNode.LeftOperand = CreateTree();
                    currentNode.RightOperand = CreateTree();
                    break;
                case '%':
                    currentNode = new NotAnd();
                    listNotations.Remove(token);
                    currentNode.LeftOperand = CreateTree();
                    currentNode.RightOperand = CreateTree();
                    break;
                // 1 operand
                case '~':
                    currentNode = new Negation();
                    listNotations.Remove(token);
                    currentNode.LeftOperand = CreateTree();
                    break;
                // No operand
                case '1':
                    currentNode = new True();
                    listNotations.Remove(token);
                    break;
                case '0':
                    currentNode = new False();
                    listNotations.Remove(token);
                    break;
                case char letter when (letter >= 'A' && letter <= 'Z'):
                    if (isPredicate)
                    {
                        currentNode = new Predicate(letter);
                        while (WaitingProp.Count > 0 && WaitingProp.First() is Variable v)
                        {
                            WaitingProp.Remove(WaitingProp.First());
                            ((Predicate)currentNode).ObjectVariables.Add(v);
                        }
                    }
                    else
                    {
                        var variable = GetVariable(letter);
                        if (variable == null)
                        {
                            variable = new Variable(letter);
                            Variables.Add(variable);
                        }
                        stack.Push(variable);
                    }
                    break;
                case char letter when (letter >= 'a' && letter <= 'z'):
                    isPredicate = true;
                    var objectVariable = GetVariable(letter);
                    if (objectVariable == null)
                    {
                        objectVariable = new Variable(letter);
                        Variables.Add(objectVariable);
                    }
                    stack.Push(objectVariable);
                    break;
                case '@':
                case '!':
                    var boundVariables = new List<Variable>();
                    while (stack.Count > 0 && stack.Peek() is Variable v)
                    {
                        stack.Pop();
                        boundVariables.Add(v);
                    }
                    operand = stack.Pop();
                    if (token == '@')
                    {
                        stack.Push(new Universal(operand, boundVariables));
                    }
                    else
                    {
                        stack.Push(new Existential(operand, boundVariables));
                    }
                    BoundVariables = BoundVariables.Union(boundVariables).ToList();
                    break;
                default:
                    throw new InvalidLogicalNotationException("The provided logical notation is invalid.");
            }
            return currentNode;
        }
    }
}
