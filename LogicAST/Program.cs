using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicAST.Expressions;

namespace LogicAST
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter Argument. \nOperators: \n\tOr: |\t\t\tAnd: &\n\tEquivalence: =\t\tImplication: >\n\tNegation: ~\n");
                string inp = Console.ReadLine();
                switch (inp)
                {
                    case "exit":
                        Environment.Exit(0);
                        break;
                    case "":
                        break;
                    default:
                        List<string> eps = new List<string>();
                        eps.Add(inp);
                        Argument arg = new Argument(eps);
                        Console.WriteLine(arg.ToString() + "\n");
                        Console.WriteLine("Press Enter to Continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
