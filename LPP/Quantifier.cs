using System.Collections.Generic;

namespace LPP
{
    abstract class Quantifier : Proposition
    {
        public List<Variable> BoundVariables { get; private set; }

        protected Quantifier():base()
        {
            BoundVariables = new List<Variable>();
        }
    }
}