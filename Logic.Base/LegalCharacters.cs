using Logic.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Base
{
    public static class LegalCharacters
    {
        public const string SYMBOLS = "1234567890QWERTYUIOPASDFGHJKLZXCBNMqwertyuiopasdfghjklzxcbnm_";
        public static readonly string[] EQUIVALENCE = { "=" };
        public static readonly string[] NEGATION = { "~" , "!"};
        public static readonly string[] DISJUNCTION = { "v", "|", "OR" };
        public static readonly string[] CONJUNCTION = { "&", "^", "AND" };
        public static readonly string[] IMPLICATION = { ">" , "=>" , "->"};
        public static readonly string[] XOR = { "XOR" };
           
        public static readonly string[] LEFTPARENTHESES = { "(", "[", "{" };
        public static readonly string[] RIGHTPARENTHESES = { ")", "]", "}" };

        public static Dictionary<OperatorType, int> Precedence = new Dictionary<OperatorType, int>() {

            { OperatorType.ClosingParenthesis, 3 },
            { OperatorType.OpeningParenthesis, 3 },
            { OperatorType.Negation, 2 },
            { OperatorType.Disjunction, 1 },
            { OperatorType.Conjunction, 1 },
            { OperatorType.Equivalence, 0 },
            { OperatorType.Implication, 0 }
        };

        public static Dictionary<OperatorType, Associativity> Associativities = new Dictionary<OperatorType, Associativity>() {

            { OperatorType.Negation, Associativity.Right },
            { OperatorType.Disjunction, Associativity.Left },
            { OperatorType.Conjunction, Associativity.Left },
            { OperatorType.Equivalence, Associativity.Left },
            { OperatorType.Implication, Associativity.Left }
        };

        public static List<string> OPERATORS = EQUIVALENCE  .Concat(NEGATION)
                                                            .Concat(DISJUNCTION)
                                                            .Concat(CONJUNCTION)
                                                            .Concat(IMPLICATION)
                                                            .Concat(LEFTPARENTHESES)
                                                            .Concat(RIGHTPARENTHESES)
                                                            .Concat(XOR).ToList();

        public static bool IsSymbol(this string str)
        {
            if (!OPERATORS.Contains(str))
            {
                foreach (char c in str)
                {
                    if (!SYMBOLS.Contains(c))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public static bool IsOperator(this string str)
        {
            return OPERATORS.Contains(str);
        }

        public static OperatorType GetOperator(string c)
        {
            if (CONJUNCTION.Contains(c))
                return OperatorType.Conjunction;
            if (DISJUNCTION.Contains(c))
                return OperatorType.Disjunction;
            if (NEGATION.Contains(c))
                return OperatorType.Negation;
            if (EQUIVALENCE.Contains(c))
                return OperatorType.Equivalence;
            if (IMPLICATION.Contains(c))
                return OperatorType.Implication;
            if (LEFTPARENTHESES.Contains(c))
                return OperatorType.OpeningParenthesis;
            if (RIGHTPARENTHESES.Contains(c))
                return OperatorType.ClosingParenthesis;

            return OperatorType.None;
        }

    }
}
