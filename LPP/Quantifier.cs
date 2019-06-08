using System.Collections.Generic;

namespace LPP
{
    abstract class Quantifier : Proposition
    {
        public List<Variable> BoundVariables;

        public Quantifier():base()
        {
            BoundVariables = new List<Variable>();
        }
    }
}