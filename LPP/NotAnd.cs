﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    class NotAnd : Proposition
    {
        public NotAnd()
        {
        }

        public override string GetLabel()
        {
            return "%";
        }

        public override int TruthValue
        {
            get
            {
                if (LeftOperand.TruthValue == 1 && RightOperand.TruthValue == 1)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public override string ToString()
        {
            return $"%({LeftOperand.ToString()}, {RightOperand.ToString()})";
        }
    }
}
