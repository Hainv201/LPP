using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class SemanticTableaux
    {
        List<Logic> listFormulas;
        SemanticTableaux left_Child;
        SemanticTableaux right_Child;
        char variable = 'a';
        static bool result;
        List<Variable> Active_Variables;
        static List<Logic> listFormulasProcessedByGamma;
        public SemanticTableaux(List<Logic> parentFormulas, List<Variable> active_Variables)
        {
            this.listFormulas = parentFormulas.Distinct(new LogicComparer()).ToList();
            if (active_Variables == null)
            {
                Active_Variables = new List<Variable>();
            }
            Active_Variables = new List<Variable>(active_Variables);
            this.left_Child = null;
            this.right_Child = null;
        }

        public SemanticTableaux(Logic formula)
        {
            listFormulasProcessedByGamma = new List<Logic>();
            listFormulas = new List<Logic>();
            Active_Variables = new List<Variable>();
            listFormulas.Add(formula);
            this.left_Child = null;
            this.right_Child = null;
        }

        public void ApplyRule()
        {
            result = false;
            if (!IsClosed())
            {
                if (!DoubleNegation())
                {
                    if (!AlphaRule())
                    {
                        if (!DeltaRule())
                        {
                            if (!BetaRule())
                            {
                                if (!GammaRule())
                                {
                                    if (IsClosed())
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                result = true;
            }
        }
        private bool DoubleNegation()
        {
            List<Logic> child_listFormulas;
            foreach (Logic formula in listFormulas)
            {
                if (formula is Negation && formula.LeftOperand is Negation negation)
                {
                    child_listFormulas = listFormulas.ToList();
                    child_listFormulas.Remove(formula);
                    child_listFormulas.Add(negation.LeftOperand);
                    SemanticTableaux new_semanticTableaux = new SemanticTableaux(child_listFormulas,Active_Variables);
                    this.left_Child = new_semanticTableaux;
                    new_semanticTableaux.ApplyRule();
                    return true;
                }
            }
            return false;
        }
        private bool AlphaRule()
        {
            List<Logic> child_listFormulas;
            foreach (Logic formula in listFormulas)
            {
                if (formula is Conjunction conjunction)
                {
                    child_listFormulas = listFormulas.ToList();
                    child_listFormulas.Remove(formula);
                    child_listFormulas.Add(conjunction.LeftOperand);
                    child_listFormulas.Add(conjunction.RightOperand);
                    SemanticTableaux new_semanticTableaux = new SemanticTableaux(child_listFormulas,Active_Variables);
                    this.left_Child = new_semanticTableaux;
                    new_semanticTableaux.ApplyRule();
                    return true;
                }
                else if (formula is Negation && formula.LeftOperand is Disjunction disjunction)
                {
                    child_listFormulas = listFormulas.ToList();
                    child_listFormulas.Remove(formula);
                    Negation negation_Left = new Negation();
                    negation_Left.LeftOperand = disjunction.LeftOperand;
                    Negation negation_Right = new Negation();
                    negation_Right.LeftOperand = disjunction.RightOperand;
                    child_listFormulas.Add(negation_Left);
                    child_listFormulas.Add(negation_Right);
                    SemanticTableaux new_semanticTableaux = new SemanticTableaux(child_listFormulas,Active_Variables);
                    this.left_Child = new_semanticTableaux;
                    new_semanticTableaux.ApplyRule();
                    return true;
                }
                else if (formula is Negation && formula.LeftOperand is Implication implication)
                {
                    child_listFormulas = listFormulas.ToList();
                    child_listFormulas.Remove(formula);
                    child_listFormulas.Add(implication.LeftOperand);
                    Negation negation_Right = new Negation();
                    negation_Right.LeftOperand = implication.RightOperand;
                    child_listFormulas.Add(negation_Right);
                    SemanticTableaux new_semanticTableaux = new SemanticTableaux(child_listFormulas,Active_Variables);
                    this.left_Child = new_semanticTableaux;
                    new_semanticTableaux.ApplyRule();
                    return true;
                }
                else if (formula is Negation && formula.LeftOperand is NotAnd notAnd)
                {
                    child_listFormulas = listFormulas.ToList();
                    child_listFormulas.Remove(formula);
                    child_listFormulas.Add(notAnd.LeftOperand);
                    child_listFormulas.Add(notAnd.RightOperand);
                    SemanticTableaux new_semanticTableaux = new SemanticTableaux(child_listFormulas, Active_Variables);
                    this.left_Child = new_semanticTableaux;
                    new_semanticTableaux.ApplyRule();
                    return true;
                }
            }
            return false;
        }
        
        private bool BetaRule()
        {
            List<Logic> leftchild_ListFormulas;
            List<Logic> rightchild_ListFormulas;
            foreach (Logic formula in listFormulas)
            {
                if (formula is Negation && formula.LeftOperand is Conjunction conjunction)
                {
                    leftchild_ListFormulas = listFormulas.ToList();
                    rightchild_ListFormulas = listFormulas.ToList();

                    leftchild_ListFormulas.Remove(formula);
                    rightchild_ListFormulas.Remove(formula);

                    Negation negation_Left = new Negation();
                    negation_Left.LeftOperand = conjunction.LeftOperand;
                    leftchild_ListFormulas.Add(negation_Left);
                    SemanticTableaux left_child = new SemanticTableaux(leftchild_ListFormulas, Active_Variables);
                    this.left_Child = left_child;

                    Negation negation_Right = new Negation();
                    negation_Right.LeftOperand = conjunction.RightOperand;
                    rightchild_ListFormulas.Add(negation_Right);
                    SemanticTableaux right_child = new SemanticTableaux(rightchild_ListFormulas, Active_Variables);
                    this.right_Child = right_child;

                    left_child.ApplyRule();
                    right_child.ApplyRule();

                    return true;
                }
                else if (formula is Disjunction disjunction)
                {
                    leftchild_ListFormulas = listFormulas.ToList();
                    rightchild_ListFormulas = listFormulas.ToList();

                    leftchild_ListFormulas.Remove(formula);
                    rightchild_ListFormulas.Remove(formula);

                    leftchild_ListFormulas.Add(disjunction.LeftOperand);
                    SemanticTableaux left_child = new SemanticTableaux(leftchild_ListFormulas, Active_Variables);
                    this.left_Child = left_child;

                    rightchild_ListFormulas.Add(disjunction.RightOperand);
                    SemanticTableaux right_child = new SemanticTableaux(rightchild_ListFormulas, Active_Variables);
                    this.right_Child = right_child;

                    left_child.ApplyRule();
                    right_child.ApplyRule();

                    return true;
                }
                else if (formula is Implication implication)
                {
                    leftchild_ListFormulas = listFormulas.ToList();
                    rightchild_ListFormulas = listFormulas.ToList();

                    leftchild_ListFormulas.Remove(formula);
                    rightchild_ListFormulas.Remove(formula);

                    Negation negation_Left = new Negation();
                    negation_Left.LeftOperand = implication.LeftOperand;
                    leftchild_ListFormulas.Add(negation_Left);
                    SemanticTableaux left_child = new SemanticTableaux(leftchild_ListFormulas, Active_Variables);
                    this.left_Child = left_child;

                    rightchild_ListFormulas.Add(implication.RightOperand);
                    SemanticTableaux right_child = new SemanticTableaux(rightchild_ListFormulas, Active_Variables);
                    this.right_Child = right_child;

                    left_child.ApplyRule();
                    right_child.ApplyRule();
                    return true;
                }
                // BiImplication
                else if (formula is BiImplication biImplication)
                {
                    leftchild_ListFormulas = listFormulas.ToList();
                    rightchild_ListFormulas = listFormulas.ToList();

                    leftchild_ListFormulas.Remove(formula);
                    rightchild_ListFormulas.Remove(formula);

                    Negation negation_Left = new Negation();
                    negation_Left.LeftOperand = biImplication.LeftOperand;
                    Negation negation_Right = new Negation();
                    negation_Right.LeftOperand = biImplication.RightOperand;

                    leftchild_ListFormulas.Add(negation_Left);
                    leftchild_ListFormulas.Add(negation_Right);
                    SemanticTableaux left_child = new SemanticTableaux(leftchild_ListFormulas, Active_Variables);
                    this.left_Child = left_child;

                    rightchild_ListFormulas.Add(biImplication.LeftOperand);
                    rightchild_ListFormulas.Add(biImplication.RightOperand);
                    SemanticTableaux right_child = new SemanticTableaux(rightchild_ListFormulas, Active_Variables);
                    this.right_Child = right_child;

                    left_child.ApplyRule();
                    right_child.ApplyRule();

                    return true;
                }
                else if (formula is Negation && formula.LeftOperand is BiImplication bi)
                {
                    leftchild_ListFormulas = listFormulas.ToList();
                    rightchild_ListFormulas = listFormulas.ToList();

                    leftchild_ListFormulas.Remove(formula);
                    rightchild_ListFormulas.Remove(formula);

                    Negation negation_Left = new Negation();
                    negation_Left.LeftOperand = bi.LeftOperand;
                    leftchild_ListFormulas.Add(negation_Left);
                    leftchild_ListFormulas.Add(bi.RightOperand);
                    SemanticTableaux left_child = new SemanticTableaux(leftchild_ListFormulas, Active_Variables);
                    this.left_Child = left_child;

                    Negation negation_Right = new Negation();
                    negation_Right.LeftOperand = bi.RightOperand;
                    rightchild_ListFormulas.Add(bi.LeftOperand);
                    rightchild_ListFormulas.Add(negation_Right);
                    SemanticTableaux right_child = new SemanticTableaux(rightchild_ListFormulas, Active_Variables);
                    this.right_Child = right_child;

                    left_child.ApplyRule();
                    right_child.ApplyRule();

                    return true;
                }
                else if (formula is NotAnd notAnd)
                {
                    leftchild_ListFormulas = listFormulas.ToList();
                    rightchild_ListFormulas = listFormulas.ToList();

                    leftchild_ListFormulas.Remove(formula);
                    rightchild_ListFormulas.Remove(formula);

                    Negation negation_Left = new Negation();
                    negation_Left.LeftOperand = notAnd.LeftOperand;
                    leftchild_ListFormulas.Add(negation_Left);
                    SemanticTableaux left_child = new SemanticTableaux(leftchild_ListFormulas, Active_Variables);
                    this.left_Child = left_child;

                    Negation negation_Right = new Negation();
                    negation_Right.LeftOperand = notAnd.RightOperand;
                    rightchild_ListFormulas.Add(negation_Right);
                    SemanticTableaux right_child = new SemanticTableaux(rightchild_ListFormulas, Active_Variables);
                    this.right_Child = right_child;

                    left_child.ApplyRule();
                    right_child.ApplyRule();

                    return true;
                }
            }
            return false;
        }

        private bool DeltaRule()
        {
            List<Logic> child_listFormulas;
            foreach (Logic formula in listFormulas)
            {
                if (formula is Existential existential)
                {
                    child_listFormulas = listFormulas.ToList();
                    child_listFormulas.Remove(formula);
                    child_listFormulas.Add(existential.LeftOperand);
                    SemanticTableaux new_semanticTableaux = new SemanticTableaux(child_listFormulas,Active_Variables);
                    foreach (Variable bound_variable in existential.BoundVariables)
                    {
                        bound_variable.Letter = variable++.ToString();
                        new_semanticTableaux.Active_Variables.Add(bound_variable);
                    }
                    this.left_Child = new_semanticTableaux;

                    new_semanticTableaux.ApplyRule();
                    return true;
                }
                else if (formula is Negation && formula.LeftOperand is Universal universal)
                {
                    child_listFormulas = listFormulas.ToList();
                    child_listFormulas.Remove(formula);
                    Negation negation = new Negation();
                    negation.LeftOperand = universal.LeftOperand;
                    child_listFormulas.Add(negation);

                    SemanticTableaux new_semanticTableaux = new SemanticTableaux(child_listFormulas,Active_Variables);
                    foreach (Variable bound_variable in universal.BoundVariables)
                    {
                        bound_variable.Letter = variable++.ToString();
                        new_semanticTableaux.Active_Variables.Add(bound_variable);
                    }
                    this.left_Child = new_semanticTableaux;

                    new_semanticTableaux.ApplyRule();
                    return true;
                }
            }
            return false;
        }

        private bool GammaRule()
        {
            List<Logic> child_listFormulas;
            foreach (Logic formula in listFormulas)
            {
                if (!listFormulasProcessedByGamma.Contains(formula) && formula is Universal universal)
                {
                    listFormulasProcessedByGamma.Add(formula);
                    child_listFormulas = listFormulas.ToList();
                    child_listFormulas.Add(universal.LeftOperand);
                    SemanticTableaux new_semanticTableaux = new SemanticTableaux(child_listFormulas,Active_Variables);
                    foreach (Variable bound_variable in universal.BoundVariables)
                    {
                        bound_variable.Letter = variable++.ToString();
                        new_semanticTableaux.Active_Variables.Add(bound_variable);
                    }
                    this.left_Child = new_semanticTableaux;

                    new_semanticTableaux.ApplyRule();
                    return true;

                }
                else if (!listFormulasProcessedByGamma.Contains(formula) && formula is Negation && formula.LeftOperand is Existential existential)
                {
                    listFormulasProcessedByGamma.Add(formula);
                    child_listFormulas = listFormulas.ToList();
                    Negation negation = new Negation();
                    negation.LeftOperand = existential.LeftOperand;
                    child_listFormulas.Add(negation);
                    SemanticTableaux new_semanticTableaux = new SemanticTableaux(child_listFormulas,Active_Variables);
                    foreach (Variable bound_variable in existential.BoundVariables)
                    {
                        bound_variable.Letter = variable++.ToString();
                        new_semanticTableaux.Active_Variables.Add(bound_variable);
                    }
                    this.left_Child = new_semanticTableaux;

                    new_semanticTableaux.ApplyRule();
                    return true;

                }
            }
            return false;
        }

        private bool IsClosed()
        {
            foreach (Logic formula in listFormulas)
            {
                if (listFormulas.Any(existFormula => existFormula is Negation && existFormula.LeftOperand.ToString() == formula.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"{{{String.Join(", ", listFormulas)}}}";
        }

        public string CreateGraph(ref int index, int preIndex = 0)
        {
            string graph = $"\nnode{index} [ label = \"{ToString()} [{ String.Join(", ", Active_Variables)}]\"]";
            if (preIndex != 0)
            {
                graph += $"\nnode{preIndex} -- node{index}";
            }
            if (this.left_Child != null && this.right_Child != null)
            {
                int pre = index;
                index++;
                graph += this.left_Child.CreateGraph(ref index, pre);
                graph += this.right_Child.CreateGraph(ref index, pre);
                return graph;
            }
            else if (this.left_Child != null && this.right_Child == null)
            {
                int pre = index;
                index++;
                graph += this.left_Child.CreateGraph(ref index, pre);
                return graph;
            }
            else if (IsClosed())
            {
                int pre = index;
                index++;
                graph += $"\nnode{index} [ label = \"X\" ]";
                graph += $"\nnode{pre} -- node{index}";
            }

            index++;
            return graph;
        }

        public bool GetResult()
        {
            return result;
        }
    }
}
