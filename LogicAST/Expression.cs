using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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
            //Todo : Implement
            return false;
        }

    }
}
