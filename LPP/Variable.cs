namespace LPP
{
    public class Variable : Proposition
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
    }
}