using System;
using System.Collections.Generic;
using System.Linq;
namespace LogicAST.Expressions
{
    public class Argument : IExpression
    {
        const string PROPOSITIONS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string EQUIVALENCE = "=";
        const string NEGATION = "~!";
        const string DISJUNCTION = "v|";
        const string CONJUNCTION = "&^";
        const string IMPLICATION = ">";
        const string LEFTPARENTHESES = "([{";
        const string RIGHTPARENTHESES = ")]}";

        public IExpression tree;
        public List<char> treePropositions = new List<char>();
        public Argument(string argument)
        {
            treePropositions = new List<char>();
            if (!TryParse(argument, ref treePropositions, out tree))
            {
                Console.WriteLine("Failed to parse argument, '" + argument + "'");
                Console.ReadLine();
            }
        }

        public bool Evaluate()
        {
            List<List<bool>> inputs = new List<List<bool>>();

            void Permute(List<bool> perm, int c, int n)
            {
                if (c == n)
                {
                    inputs.Add(perm);
                }
                else
                {
                    for(int i = 0; i < 2; i++)
                    {
                        perm[c] = (i==0);
                        Permute(perm, c+1, n);
                    }
                }
            }
            List<bool> f = new List<bool>();
            for (int i = 0; i < treePropositions.Count; i++)
            {
                f.Add(false);
            }
            //Find boolean permutations
            Permute(f, 0, treePropositions.Count);
            
            bool valid = true;
            for(int i = 0; i < inputs.Count; i++)
            {
                for(int j = 0; j < inputs[i].Count; j++)
                {
                    tree.Set(treePropositions[j], inputs[i][j]);
                }
                if (!tree.Evaluate())
                {
                    valid = false;
                }
            }
            return valid;
        }

        public bool PrintTable()
        {
            Console.WriteLine();

            //Generate combinations
            int size = treePropositions.Count;
            int numRows = (int)Math.Pow(2, size);
            bool[][] inputs = new bool[numRows][];
            int rightMove(int value, int pos)
            {
                if (pos != 0)
                {
                    int mask = 0x7fffffff;
                    value >>= 1;
                    value &= mask;
                    value >>= pos - 1;
                }
                return value;
            }
            for (int i = 0; i < numRows;i++)
            {
                inputs[i] = new bool[size];
                for(int j = 0; j < size; j++)
                {
                    int val = (numRows * (size - j - 1)) + i;
                    int ret = (1 & rightMove(val, (size - j - 1)));
                    inputs[i][j] = ret == 0;
                }
            }

            //Test combinations
            bool valid = true;
            Console.Write(" ");
            for(int i = 0; i < size; i++)
            {
                Console.Write(treePropositions[i] + " | ");
            }
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                Console.Write("---|");
            }
            Console.Write("---\n");
            for (int i = 0; i < numRows; i++)
            {
                Console.Write(" ");
                for (int j = 0; j < size; j++)
                {
                    tree.Set(treePropositions[j], inputs[i][j]);
                    Console.Write(inputs[i][j] ? "T | " : "F | ");
                }
                bool res = tree.Evaluate();
                if (!res)
                {
                    valid = false;
                }
                Console.Write(res ? "T" : "F");
                Console.WriteLine();
            }
            Console.WriteLine();
            return valid;
        }

        public void Set(char Letter, bool Value)
        {
            tree.Set(Letter, Value);
        }

        public override string ToString()
        {
            return tree.ToString();
        }

        public static bool TryParse(string argument, ref List<char> Propositions, out IExpression tree)
        {
            //Left side.
            IExpression left;
            //Right side.
            IExpression right;
            //Operator
            Operator op = Operator.None;

            //Make uppercase for easier handling of upper/lowercase
            //Propositions. Remove spaces as well.
            argument = argument.ToUpper().Replace(" ", String.Empty);


            int index = 0;
            if(argument == "")
            {
                tree = null;
                Propositions = null;
                return false;
            }
            if(NEGATION.Contains(argument[0]))
            {
                //Negating proposition
                op = Operator.Negation;
                if(PROPOSITIONS.IndexOf(argument[1]) != -1)
                {
                    //Proposition, insert this immediately to left.
                    left = new Negation(new Proposition(argument[1], false));
                    index += 2;
                    if (index == argument.Length)
                    {
                        tree = left;
                        if (!Propositions.Contains(argument[1]))
                        {
                            Propositions.Add(argument[1]);
                        }
                        return true;
                    }
                    else
                    {
                        //We have a right side
                        string rightProp = argument.Substring(index + 1, (argument.Length - index) - 1);
                        TryParse(rightProp, ref Propositions, out right);
                        op = GetOperator(argument[index]);

                        return GetTree(op, left, right, out tree);
                    }
                }
                else if(LEFTPARENTHESES.IndexOf(argument[1]) != -1)
                {
                    string leftProp = FindClosing(argument, 1);
                    TryParse(leftProp, ref Propositions, out left);
                    left = new Negation(left);
                    //+ negation
                    //+ 2 parens
                    index += leftProp.Length + 2 + 1;
                    if (index == argument.Length)
                    {
                        tree = left;
                        return true;
                    }
                    else
                    {
                        //We have a right side
                        string rightProp = argument.Substring(index + 1, (argument.Length - index) - 1);
                        TryParse(rightProp, ref Propositions, out right);
                        op = GetOperator(argument[index]);
                        return GetTree(op, left, right, out tree);
                    }
                }

            }
            //Left side is within parenthesis
            else if (LEFTPARENTHESES.IndexOf(argument[0]) != -1)
            {
                string leftProp = FindClosing(argument, 0);
                TryParse(leftProp, ref Propositions, out left);
                index += leftProp.Length + 2;
                if(index == argument.Length)
                {
                    tree = left;
                    return true;
                }
                else
                {
                    //We have a right side
                    string rightProp = argument.Substring(index + 1, (argument.Length - index) - 1);
                    TryParse(rightProp, ref Propositions, out right);
                    op = GetOperator(argument[index]);

                    return GetTree(op, left, right, out tree);

                }
            }
            else if(PROPOSITIONS.IndexOf(argument[0]) != -1)
            {

                if (!Propositions.Contains(argument[0]))
                {
                    Propositions.Add(argument[0]);
                }
                //The propositions string contains the symbol
                //Therefore it is a proposition
                left = new Proposition(argument[0], false);
                index++;
                if(index == argument.Length)
                {
                    tree = left;
                    return true;
                }
                else
                {
                    //We have a right side
                    string rightProp = argument.Substring(index + 1, (argument.Length - index) - 1);
                    TryParse(rightProp, ref Propositions, out right);
                    op = GetOperator(argument[index]);
                    return GetTree(op, left, right, out tree);
                }
            }
            tree = null;
            Propositions = null;
            return false;
        }

        /// <summary>
        /// Converts char operator to Operator enum
        /// </summary>
        /// <param name="c">Char</param>
        /// <returns>Enum value</returns>
        public static Operator GetOperator(char c)
        {
                if(CONJUNCTION.Contains(c))
                    return Operator.Conjunction;
                if (DISJUNCTION.Contains(c))
                    return Operator.Disjunction;
                if(NEGATION.Contains(c))
                    return Operator.Negation;
                if(EQUIVALENCE.Contains(c))
                    return Operator.Equivalence;
                if(IMPLICATION.Contains(c))
                    return Operator.Implication;

                return Operator.None;
        }
        public static bool GetTree(Operator op, IExpression left, IExpression right, out IExpression tree)
        {
            switch (op)
            {
                case Operator.Disjunction:
                    tree = new Disjunction(left, right);
                    return true;
                case Operator.Conjunction:
                    tree = new Conjunction(left, right);
                    return true;
                case Operator.Equivalence:
                    tree = new Equivalence(left, right);
                    return true;
                case Operator.Implication:
                    tree = new Implication(left, right);
                    return true;
                default:
                    tree = null;
                    return false;
            }
        }
        /// <summary>
        /// From the left side, it will find the text within the closing parenthesis.
        /// For example, FindClosing("((abc) a)", 0); willl return "(abc) a".
        /// </summary>
        /// <param name="input">Input string, including with the first parenthesis</param>
        /// <param name="left">Start index (of the parenthesis)</param>
        /// <returns>The text within the parenthesis</returns>
        public static string FindClosing(string input, int left)
        {
            int right = left;
            int depth = 0;
            while (true)
            {
                if (right < input.Length)
                {
                    if (LEFTPARENTHESES.IndexOf(input[right]) != -1)
                    {
                        depth++;
                    }
                    else if (RIGHTPARENTHESES.IndexOf(input[right]) != -1)
                    {
                        depth--;
                    }
                    if (depth == 0)
                    {
                        break;
                    }
                    right++;
                }
                else
                {
                    throw new Exception("Mismatched parenthesis");
                }
            }
            return input.Substring(left+1, right - left - 1);
        }
    }
}
