using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class LogicComparer : IEqualityComparer<Logic>
    {
        public bool Equals(Logic x, Logic y)
        {
            return x.ToString() == y.ToString();
        }

        public int GetHashCode(Logic obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
