﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP
{
    [Serializable]
    public class Conjunction : Logic
    {
        public Conjunction():base()
        {
        }

        public override string GetLabel()
        {
            return "&";
        }

        public override int TruthValue
        {
            get
            {
                if (LeftOperand.TruthValue == 1 && RightOperand.TruthValue == 1)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override string ToString()
        {
            return $"({LeftOperand.ToString()} & {RightOperand.ToString()})";
        }

        public override Logic Nandify()
        {
            Logic nand = new NotAnd();
            //Left
            nand.LeftOperand = new NotAnd();
            nand.LeftOperand.LeftOperand = this.LeftOperand.Nandify();
            nand.LeftOperand.RightOperand = this.RightOperand.Nandify();
            //Right
            nand.RightOperand = new NotAnd();
            nand.RightOperand.LeftOperand = this.LeftOperand.Nandify();
            nand.RightOperand.RightOperand = this.RightOperand.Nandify();   
            return nand;
        }

        public override Logic Simplify()
        {
            LogicComparer logicComparer = new LogicComparer();
            if (this.LeftOperand is True)
            {
                return this.RightOperand.Simplify();
            }
            else if (this.RightOperand is True)
            {
                return this.LeftOperand.Simplify();
            }
            else if (this.LeftOperand is False || this.RightOperand is False)
            {
                return new False();
            }
            else if (logicComparer.Equals(this.LeftOperand, this.RightOperand))
            {
                return this.LeftOperand.Simplify();
            }
            return this;
        }
        public override Logic ConvertToCNF()
        {
            this.LeftOperand = this.LeftOperand.ConvertToCNF();
            this.RightOperand = this.RightOperand.ConvertToCNF();
            return this.Simplify();
        }

        public override Logic ApplyDistributiveLaw()
        {
            this.LeftOperand = this.LeftOperand.ApplyDistributiveLaw();
            this.RightOperand = this.RightOperand.ApplyDistributiveLaw();
            return this.Simplify();
        }
    }
}
