using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Base.Tests
{
    [TestClass()]
    public class ArgumentTests
    {
        [TestMethod()]
        public void GetPermutationsTest()
        {
            var output = Argument.GetPermutations(2);

            bool[][] expected = new bool[][]
            {
                new bool[]{ true, true },
                new bool[]{ true, false},
                new bool[]{false, true},
                new bool[]{false, false}


            };

            for (int i = 0; i < output.Length; i++)
            {
                for (int j = 0; j < output[i].Length; j++)
                {
                    Assert.IsTrue(expected[i][j].Equals(output[i][j]));
                }
            }
        }

        [TestMethod()]
        public void IsValidTest()
        {
            List<string> prem = new List<string>();

            prem.Add("p>q");
            prem.Add("p");
            string conc = "q";

            Argument a = new Argument(prem, conc);

            Assert.IsTrue(a.IsValid());
        }
    }
}