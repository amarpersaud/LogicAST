using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicAST
{
    public static class StringExtensions
    {
        public static string ReplaceAll(this string str, string[] OldString, string NewString)
        {
            if(str is null)
            {
                return null;
            }
            foreach(string s in OldString)
            {
                str = str.Replace(s, NewString);
            }
            return str;
        }

        public static string RemoveWhitespace(this string str)
        {
            string output = "";

            foreach(char c in str)
            {
                if (!string.IsNullOrWhiteSpace(new string(c, 1)))
                {
                    output += c;
                }
            }

            return output;
        }

        // from https://www.codeproject.com/Articles/1014073/Fastest-method-to-remove-all-whitespace-from-Strin
        static Regex whitespace = new Regex(@"\s+", RegexOptions.Compiled);

        public static string TrimAllWhitespace(this string str)
        {
            return whitespace.Replace(str, "");
        }

    }
}
