using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAST
{
    public class RPN
    {

        /// <summary>
        /// Formats an infix string with proper whitespace
        /// </summary>
        /// <param name="infix"></param>
        /// <returns></returns>
        public string FormatInfixString(string infix)
        {
            infix = infix.TrimAllWhitespace();

            //TODO: implement

            return infix;
        }

        public string InfixToRPN(string infixString)
        {
            Stack<string> output = new Stack<string>();
            Stack<string> operatorStack = new Stack<string>();

            //TODO: implement

            //Concatenate into a single string with space as delimeter
            return output.Aggregate((x,c) => x + " " +c);
        }

    }
}
