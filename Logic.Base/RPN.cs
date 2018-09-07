using Logic.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Base
{
    public static class RPN
    {

        public static bool IsInfix(this string str)
        {
            str = str.FormatInfixString();
            string[] split = str.Split(' ');
            if(split.Length == 0)
            {
                return false;
            }
            if(split.Length == 1)
            {
                return true;
            }
            if (LegalCharacters.IsSymbol(split[split.Length - 1]) && LegalCharacters.IsOperator(split[split.Length - 2]))
            {
                return true;
            }
            return false;
        }
        public static bool IsPostfix(this string str)
        {
            return IsRPN(str);
        }
        public static bool IsRPN(this string str)
        {
            string[] split = str.Split(' ');
            if (LegalCharacters.IsOperator(split[split.Length - 1])) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Formats an infix string with proper whitespace
        /// </summary>
        /// <param name="infix">Infix string to formatt</param>
        /// <returns>Formatted infix string</returns>
        public static string FormatInfixString(this string infix)
        {
            infix = infix.TrimAllWhitespace() + "  ";

            //TODO: implement
            string output = "";

            for(int i = 0; i < infix.Length - 1; i++)
            {
                if(i < infix.Length - 2)
                {
                    string sub = infix.Substring(i, 1);
                    string op2 = infix.Substring(i, 2);
                    if (LegalCharacters.IsSymbol(sub))
                    {
                        if (LegalCharacters.IsSymbol(op2))
                        {
                            output += sub;
                        }
                        else
                        {
                            output += sub + " ";
                        }
                    }
                    else if (LegalCharacters.IsOperator(op2))
                    {
                        output += op2 + " ";
                    }
                    else if (LegalCharacters.IsOperator(sub))
                    {
                        output += sub + " ";
                    }
                }


            }

            return output.Trim();
        }

        /// <summary>
        /// Convert an infix formatted string to Reverse Polish notation
        /// </summary>
        /// <param name="infixString"></param>
        /// <returns></returns>
        public static string InfixToRPN(this string infixString)
        {
            infixString = FormatInfixString(infixString);

            string output = "";

            //Create a stack for the operators
            Stack<Operator> operatorStack = new Stack<Operator>();

            //Create a stack to represent the incoming infix string and populate
            Stack<string> infixStack = new Stack<string>();
            string[] infixSplit = infixString.Split(' ');
            for(int i = infixSplit.Length - 1; i >= 0; i--)
            {
                infixStack.Push(infixSplit[i].Trim());
            }

            while(infixStack.Count > 0)
            {
                string currentSymbol = infixStack.Pop();

                if (LegalCharacters.IsOperator(currentSymbol))
                {
                    Operator currentOperator = new Operator(currentSymbol);

                    if (currentOperator.Type == OperatorType.ClosingParenthesis)
                    {
                        while(operatorStack.Peek().Type != OperatorType.OpeningParenthesis)
                        {
                            output += operatorStack.Pop().OperatorString + " ";
                        }
                        operatorStack.Pop();
                    }
                    else if(currentOperator.Type == OperatorType.OpeningParenthesis)
                    {
                        operatorStack.Push(currentOperator);
                    }
                    else
                    {
                        while(operatorStack.Count > 0 && operatorStack.Peek().Type != OperatorType.OpeningParenthesis && (operatorStack.Peek().Precedence > currentOperator.Precedence || (operatorStack.Peek().Precedence == currentOperator.Precedence && operatorStack.Peek().Associativity == Associativity.Left) && operatorStack.Peek().Type != OperatorType.OpeningParenthesis))
                        {
                            output += operatorStack.Pop().OperatorString + " ";
                        }
                        operatorStack.Push(currentOperator);
                    }

                    //Check it against the stack
                }
                else if (LegalCharacters.IsSymbol(currentSymbol))
                {
                    output += currentSymbol + " ";
                }
                else
                {
                    throw new UnexpectedSymbolException($"Unexpected Symbol \"{currentSymbol}\" while parsing infix to RPN");
                }
            }

            while(operatorStack.Count > 0)
            {
                output += operatorStack.Pop().OperatorString + " ";
            }

            //TRIM trailing space
            return output.Trim();
        }
    }



}
