namespace LPP
{
    public class Variable : Proposition
    {
        public char Letter { get; set; }

        public Variable(char letter)
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