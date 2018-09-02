using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAST.Expressions
{
    public class Proposition : IExpression
    {
        public char Letter;
        public bool Value;
        public Proposition(char Letter, bool Value)
        {
            this.Letter = Letter;
            this.Value = Value;
        }
        public bool Evaluate()
        {
            return Value;
        }
        public void Set(char Letter, bool Value)
        {
            if (this.Letter == Char.ToUpper(Letter))
            {
                this.Value = Value;
            }
        }
        public override string ToString()
        {
            return Letter.ToString();
        }
    }
}
