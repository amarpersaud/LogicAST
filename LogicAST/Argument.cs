﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LogicAST
{
    public class Argument
    {
        public Dictionary<string, bool> PropositionValues = new Dictionary<string, bool>();

        public List<Expression> Premises { get; private set; }
        public Expression Conclusion { get; private set; }

        public Argument(List<string> Premises, Dictionary<string, bool> PropositionValues = null)
        {
            SetPremises(Premises);
            if(!(PropositionValues is null))
            {
                this.PropositionValues = PropositionValues;
            }
        }

        public void SetPremises(List<string> Premises)
        {
            foreach (string exp in Premises)
            {
                if (exp.IsInfix())
                {
                    Expression newExp = new Expression(exp.InfixToRPN(), this.PropositionValues);
                    this.Premises.Add(newExp);
                }
                else if (exp.IsPostfix())
                {
                    Expression newExp = new Expression(exp, this.PropositionValues);
                    this.Premises.Add(newExp);
                }
                else
                {
                    throw new InvalidExpressionException("Expression invalid. Expected valid infix or postfix expression");
                }
            }
        }

        public void SetValue(string Proposition, bool value)
        {
            PropositionValues[Proposition] = value;
        }

        public bool Evaluate()
        {

            bool props = true;

            foreach (Expression p in Premises)
            {
                props &= p.Evaluate();
            }
            
            return Operator.Implies(props, Conclusion.Evaluate());
        }

        public bool IsValid()
        {
            //Todo : implement

            return false;
        }
        public static bool[][] GetPermutations(int Propositions)
        {
            //There are n number of T/F values in each row,
            //and 2^n number of rows, where n is the number of propositions
            bool[][] output = new bool[(int)Math.Pow(2, Propositions)][];

            output[0] = new bool[Propositions];
            for(int i = 0; i < Propositions; i++) {
                output[0][i] = true;
            }

            for(int i = 1; i < output.Length; i++)
            {
                output[i] = NextPermutation(output[i - 1]);
            }

            return output;
        }
        public static bool[] NextPermutation(bool[] prev)
        {
            bool[] newPerm = (bool[])prev.Clone();

            bool carry = true;
            for(int i = newPerm.Length - 1; i >= 0; i--)
            {
                if (carry)
                {
                    carry = !newPerm[i];
                    newPerm[i] = !newPerm[i];
                }
            }
            return newPerm;
        }
    }
}
