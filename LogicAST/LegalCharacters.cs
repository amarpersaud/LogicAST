using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAST
{
    public static class LegalCharacters
    {
        public const string SYMBOLS = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm_";
        public static readonly string[] EQUIVALENCE = { "=" };
        public static readonly string[] NEGATION = { "~" , "!"};
        public static readonly string[] DISJUNCTION = { "v", "|" };
        public static readonly string[] CONJUNCTION = { "&", "^" };
        public static readonly string[] IMPLICATION = { ">" , "=>" , "->"};
           
        public static readonly string[] LEFTPARENTHESES = { "(", "[", "{" };
        public static readonly string[] RIGHTPARENTHESES = { ")", "]", "}" };
    }
}
