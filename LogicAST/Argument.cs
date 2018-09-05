using System;
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

        public List<Expression> Propositions { get; private set; }
        public string Conclusion { get; private set; }

        public Argument(List<string> Propositions, Dictionary<string, bool> PropositionValues = null)
        {
            SetPropositions(Propositions);
            if(!(PropositionValues is null))
            {
                this.PropositionValues = PropositionValues;
            }
        }

        public void SetPropositions(List<string>Propositions)
        {
            foreach (string exp in Propositions)
            {
                if (exp.IsInfix())
                {
                    Expression newExp = new Expression(exp.InfixToRPN(), this.PropositionValues);
                    this.Propositions.Add(newExp);
                }
                else if (exp.IsPostfix())
                {
                    Expression newExp = new Expression(exp, this.PropositionValues);
                    this.Propositions.Add(newExp);
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
            //Todo : Implement
            return false;
        }

        public bool IsValid()
        {
            //Todo : implement
            return false;
        }

    }
}
