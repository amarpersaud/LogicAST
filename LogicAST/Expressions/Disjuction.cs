using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAST.Expressions
{
    public class Disjunction : IExpression
    {
        public IExpression a;
        public IExpression b;
        public Disjunction(IExpression a, IExpression b)
        {
            this.a = a;
            this.b = b;
        }
        public bool Evaluate()
        {
            return (a.Evaluate() || b.Evaluate());
        }
        public void Set(char Letter, bool Value)
        {
            a.Set(Letter, Value);
            b.Set(Letter, Value);
        }
        public override string ToString()
        {
            return "( " + a.ToString() + " | " + b.ToString() + " )";
        }
    }
}
