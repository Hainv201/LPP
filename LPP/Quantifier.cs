using System;
using System.Collections.Generic;

namespace LPP
{
    [Serializable]
    abstract class Quantifier : Logic
    {
        public List<Variable> BoundVariables;

        protected Quantifier():base()
        {
            BoundVariables = new List<Variable>();
        }
    }
}