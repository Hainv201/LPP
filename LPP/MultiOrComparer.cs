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
            return x.ToString() == y.ToString();
        }

        public int GetHashCode(MultiOr obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
