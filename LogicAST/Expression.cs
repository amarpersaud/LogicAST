using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LogicAST.Exceptions;

namespace LogicAST
{
    public class Expression
    {
        public Dictionary<string, bool> PropositionValues = new Dictionary<string, bool>();

        public string Value { get; private set; }

        public Expression(string Expression, Dictionary<string, bool> PropositionValues = null)
        {
            SetExpression(Expression);
            if (!(PropositionValues is null))
            {
                this.PropositionValues = PropositionValues;
            }
        }

        public void SetExpression(string Expression)
        {
            if (Expression.IsInfix())
            {
                this.Value = Expression.InfixToRPN();
            }
            else if (Expression.IsPostfix())
            {
                this.Value = Expression;
            }
            else
            {
                throw new InvalidExpressionException("Expression invalid. Expected valid infix or postfix expression");
            }
        }

        public void SetValue(string Proposition, bool value)
        {
            PropositionValues[Proposition] = value;
        }

        public bool Evaluate()
        {
            Stack<string> stk = new Stack<string>();

            string[] split = Value.Split(' ');

            foreach(string s in PropositionValues.Keys)
            {
                for(int i = 0; i < split.Length; i++)
                {
                    if(split[i] == s)
                    {
                        split[i] = PropositionValues[s].ToString();
                    }
                }
            }

            for(int i = 0; i < split.Length; i++)
            {
                if (split[i].IsOperator())
                {
                    Operator op = new Operator(split[i].Trim());

                    if(op.Type == OperatorType.Negation)
                    {
                        string val = stk.Pop();

                        bool b = bool.Parse(val);

                        stk.Push(b.ToString());

                    }
                    else
                    {
                        bool a = bool.Parse(stk.Pop());
                        bool b = bool.Parse(stk.Pop());
                        bool r = false;
                        switch (op.Type)
                        {
                            case OperatorType.Conjunction:
                                r = Operator.And(a, b);
                                break;
                            case OperatorType.Disjunction:
                                r = Operator.Or(a, b);
                                break;
                            case OperatorType.Equivalence:
                                r = Operator.Equivalent(a, b);
                                break;
                            case OperatorType.Implication:
                                r = Operator.Implies(a, b);
                                break;
                            case OperatorType.Xor:
                                r = Operator.XOr(a, b);
                                break;
                            default:
                                throw new UnexpectedSymbolException($"Unexpected symbol while evaluating \"{op.OperatorString}\"");
                        }
                        stk.Push(r.ToString());
                    }
                }
                else
                {
                    stk.Push(split[i]);
                }

            }

            if(stk.Count != 1)
            {
                throw new InvalidExpressionException($"Invalid expression while parsing \"{Value}\"");
            }
            else
            {
                return bool.Parse(stk.Pop());
            }
        }

    }
}
