using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicAST.Exceptions;

namespace LogicAST.Expressions
{
    public interface IExpression
    {
        bool Evaluate();
        void Set(char Letter, bool Value);
        string ToString();
    }
}
