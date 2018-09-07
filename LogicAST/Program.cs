using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Base;

namespace Logic.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> arguments = new List<string>();
            string conclusion = "";
            while (true)
            {
                Console.WriteLine("");
                string inp = Console.ReadLine();
                string[] split = inp.Split(' ');
                switch (split[0])
                {
                    case "prem":
                    case "premise":
                        string premise = inp.Remove(0, split[0].Length);
                        arguments.Add(premise);
                        Console.WriteLine("Added premise: " + premise);
                        break;
                    case "clear premises":
                        arguments.Clear();
                        Console.WriteLine("Premises cleared");
                        break;
                    case "conc":
                    case "conclusion":
                        string conc = inp.Remove(0, split[0].Length);
                        conclusion = conc;
                        Console.WriteLine("Set conclusion: " + conclusion);
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    case "":
                        break;
                    case "valid":

                        Console.WriteLine("\n ==========\n");
                        foreach (string s in arguments)
                        {
                            Console.WriteLine(s);
                        }

                        Console.WriteLine(".`.");
                        Console.WriteLine(conclusion);
                        Console.WriteLine();
                        Argument arg = new Argument(arguments, conclusion);
                        Console.WriteLine("Argument is valid:" + arg.IsValid() + "\n");
                        break;
                }
            }
        }
    }
}
