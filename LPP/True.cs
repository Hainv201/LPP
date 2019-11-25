using System;

namespace LPP
{
    [Serializable]
    public class True : Logic
    {
        public True()
        {
        }

        public override string GetLabel()
        {
            return "True";
        }

        public override int TruthValue
        {
            get
            {
                return 1;
            }
        }

        public override string ToString()
        {
            return "True";
        }

        public override Logic Nandify()
        {
            return this;
        }

        public override Logic ConvertToCNF()
        {
            return this;
        }
    }
}