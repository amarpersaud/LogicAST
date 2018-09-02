namespace LogicAST.Expressions
{
    public class Negation : IExpression
    {
        public IExpression a;
        public Negation(IExpression a)
        {
            this.a = a;
        }
        public bool Evaluate()
        {
            return (!a.Evaluate());
        }
        public void Set(char Letter, bool Value)
        {
            a.Set(Letter, Value);
        }
        public override string ToString()
        {
            return "( ! " + a.ToString() + " )";
        }
    }
}
