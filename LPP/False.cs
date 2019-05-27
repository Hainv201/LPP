using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class False : Proposition
    {
        public False()
        {
        }

        public override string GetLabel()
        {
           return "False";
        }

        public override int TruthValue
        {
            get
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return "False";
        }
    }
}
