using LogicAST.Exceptions;

namespace LogicAST
{

    public class Operator
    {
        public Operator(string OperatorString)
        {
            if (LegalCharacters.IsOperator(OperatorString))
            {
                this.Type = LegalCharacters.GetOperator(OperatorString);
                this.OperatorString = OperatorString;
            }
            else
            {
                throw new UnexpectedSymbolException($"Unexpected Symbol while creating Operator; \"{OperatorString}\"");
            }

        }
        public OperatorType Type;
        public string OperatorString;
        public int Precedence { get { return LegalCharacters.Precedence[this.Type]; } }
        public Associativity Associativity { get { return LegalCharacters.Associativities[this.Type]; } }
    }

    public enum OperatorType
    {
        Conjunction,
        Disjunction,
        Equivalence,
        Implication,
        Negation,
        OpeningParenthesis,
        ClosingParenthesis,
        None
    }
}
