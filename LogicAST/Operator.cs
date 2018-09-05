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

        public static bool Implies(bool a, bool b)
        {
            return (!a) || b;
        }
        public static bool Equivalent(bool a, bool b)
        {
            return a == b;
        }
        public static bool And(bool a, bool b)
        {
            return a && b;
        }
        public static bool Or(bool a, bool b)
        {
            return a || b;
        }
        public static bool XOr(bool a, bool b)
        {
            return a ^ b;
        }
        public static bool Not(bool a)
        {
            return a;
        }
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
