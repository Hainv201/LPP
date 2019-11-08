﻿using System;

namespace LPP
{
    [Serializable]
    public class Variable : Logic, IComparable<Variable>
    {
        public string Letter { get; set; }

        public Variable(string letter)
        {
            Letter = letter;
        }

        public override string GetLabel()
        {
            return Letter.ToString();
        }

        public override string ToString()
        {
            return Letter.ToString();
        }

        public override Logic Nandify()
        {
            return this;
        }
        public int CompareTo(Variable other)
        {
            return Letter.CompareTo(other.Letter);
        }

        public override Logic ConvertToCNF()
        {
            return this;
        }
    }
}