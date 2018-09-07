using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Logic.Base
{
    public class Argument
    {
        public Dictionary<string, bool> PropositionValues = new Dictionary<string, bool>();

        public List<Expression> Premises { get; private set; }
        public Expression Conclusion { get; private set; }

        public Argument(List<string> Premises, string Conclusion, Dictionary<string, bool> PropositionValues = null)
        {
            if(!(PropositionValues is null))
            {
                this.PropositionValues = PropositionValues;
            }
            SetPremises(Premises);
            this.Conclusion = new Expression(Conclusion, this.PropositionValues);
        }

        public void SetPremises(List<string> Premises)
        {
            this.Premises = new List<Expression>();
            foreach (string exp in Premises)
            {
                if (exp.IsInfix())
                {
                    Expression newExp = new Expression(exp.InfixToRPN(), this.PropositionValues);
                    this.Premises.Add(newExp);
                }
                else if (exp.IsPostfix() || exp.IsSymbol())
                {
                    Expression newExp = new Expression(exp, this.PropositionValues);
                    this.Premises.Add(newExp);
                }
                else
                {
                    throw new InvalidExpressionException("Expression invalid. Expected valid infix or postfix expression");
                }
            }

            foreach(Expression e in this.Premises)
            {
                List<string> strs = e.GetPropositions();
                foreach(string s in strs)
                {
                    if (!PropositionValues.ContainsKey(s))
                    {
                        PropositionValues.Add(s, false);
                    }
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
            var permutations = GetPermutations(this.PropositionValues.Count);
            Dictionary<string, bool> PropVals = PropositionValues.ToDictionary(x=> x.Key, x=> x.Value);
            //For each row in the table
            for(int i = 0; i < permutations.Length; i++)
            {
                //
                for(int j = 0; j < PropositionValues.Count; j++)
                {
                    PropositionValues[PropositionValues.Keys.ElementAt(j)] = permutations[i][j]; 
                }

                bool allTrue = true;
                foreach(Expression e in Premises)
                {
                    allTrue &= e.Evaluate();
                }
                if (allTrue)
                {
                    if (!Conclusion.Evaluate())
                    {
                        PropositionValues = PropVals;
                        return false;
                    }
                }
            }
            PropositionValues = PropVals;
            return true;
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
