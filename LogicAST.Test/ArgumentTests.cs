using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAST.Tests
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

            for(int i = 0; i < output.Length; i++)
            {
                for (int j = 0; j < output[i].Length; j++)
                {
                    Assert.IsTrue(expected[i][j].Equals(output[i][j]));
                }
            }
        }
    }
}