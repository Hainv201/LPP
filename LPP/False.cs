using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class False : Logic
    {
        public False():base()
        {
        }
        public override bool IsLeaf
        {
            get { return true; }
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

        public override Logic Nandify()
        {
            return this;
        }

        public override Logic ConvertToCNF()
        {
            return this;
        }

        public override string GetRandomPrefix()
        {
            return "0";
        }

        public override string GetCNFForm()
        {
            return "False";
        }
    }
}
