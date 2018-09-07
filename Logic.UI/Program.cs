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
            Console.WriteLine(helpText);
            while (true)
            {
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
                    case "valid":
                        if(arguments.Count == 0)
                        {
                            Console.WriteLine("No premises!");
                            break;
                        }
                        if(conclusion == "")
                        {
                            Console.WriteLine("No conclusion!");
                        }

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
                    case "help":
                    case "/?":
                    case "/h":
                    case "-h":
                    case "--h":
                    case "-help":
                    case "--help":
                        Console.WriteLine(helpText);
                        break;
                    case "clear":
                        Console.Clear();
                        Console.WriteLine(helpText);
                        break;
                    case "":
                        break;
                    default:
                        Console.WriteLine("Unrecognized input");
                        break;
                }
            }
        }

        public const string helpText = "\n ==========\nCommands: \n\thelp: \t\t\t\tShows this page\n\texit:\t\t\t\tExits the program\n\tpremise [premise]\t\tAdds the premise\n\tconclusion [conclusion]: \tSets the conclusion\n\tvalid\t\t\t\tShows if the argument is valid\n\tclear premises\t\t\tclears the list of premises\n\tclear\t\t\t\tclears the console";
    }
}
