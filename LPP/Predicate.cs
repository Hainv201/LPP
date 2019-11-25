using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class Predicate : Logic
    {
        private string Letter;
        public List<Variable> ObjectVariables;

        public Predicate(string letter):base(letter)
        {
            Letter = letter;
            ObjectVariables = new List<Variable>();
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
