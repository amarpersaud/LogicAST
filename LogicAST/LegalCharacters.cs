using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAST
{
    public class LegalCharacters
    {
        public const string SYMBOLS = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm-_";
        public const string EQUIVALENCE = "=";
        public const string NEGATION = "~!";
        public const string DISJUNCTION = "v|";
        public const string CONJUNCTION = "&^";
        public const string IMPLICATION = ">";
        public const string LEFTPARENTHESES = "([{";
        public const string RIGHTPARENTHESES = ")]}";
    }
}
