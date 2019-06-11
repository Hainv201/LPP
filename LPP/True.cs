namespace LPP
{
    class True : Proposition
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

        public override Proposition Nandify()
        {
            return this;
        }
    }
}