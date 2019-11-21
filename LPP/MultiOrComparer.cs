using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class MultiOrComparer: IEqualityComparer<MultiOr>
    {
        public bool Equals(MultiOr x, MultiOr y)
        {
            return SameChar(x.ToString(), y.ToString());
        }

        public int GetHashCode(MultiOr obj)
        {
            char[] characters = obj.ToString().ToCharArray();
            Array.Sort(characters);
            string value = characters.ToString();
            return value.GetHashCode();
        }

        private bool SameChar(string firstString, string secondString)
        {
            char[] first = firstString.ToCharArray();
            char[] second = secondString.ToCharArray();
            Array.Sort(first);
            Array.Sort(second);
            return first.SequenceEqual(second);
        }
    }
}
