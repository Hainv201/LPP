using System;
using System.Collections.Generic;

namespace LPP
{
    class Universal:Quantifier
    {
        public Universal() :base()
        {
        }

        public override string GetLabel()
        {
            return $"@{String.Join(",", BoundVariables)}";
        }

        public override string ToString()
        {
            return $"@{String.Join(",", BoundVariables)}.{LeftOperand.ToString()}";
        }
    }
}