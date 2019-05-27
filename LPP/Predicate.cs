using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class Predicate : Proposition
    {
        public char Letter { get; set; }
        public List<Variable> ObjectVariables { get; set; }

        public Predicate(char letter)
        {
            Letter = letter;
        }

        public override string GetLabel()
        {
            return ToString();
        }

        public override string ToString()
        {
            return $"{Letter.ToString()}({String.Join(",", ObjectVariables)})";
        }
    }
}
