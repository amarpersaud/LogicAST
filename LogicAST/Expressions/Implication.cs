namespace LogicAST.Expressions
{
    public class Implication : IExpression
    {
        public IExpression a;
        public IExpression b;
        public Implication(IExpression a, IExpression b)
        {
            this.a = a;
            this.b = b;
        }
        public bool Evaluate()
        {
            return ((!a.Evaluate()) || b.Evaluate());
        }
        public void Set(char Letter, bool Value)
        {
            a.Set(Letter, Value);
            b.Set(Letter, Value);
        }
        public override string ToString()
        {
            return "( " + a.ToString() + " > " + b.ToString() + " )";
        }
    }
}
