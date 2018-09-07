using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Base.Tests
{
    [TestClass()]
    public class RPNTests
    {
        [TestMethod()]
        public void InfixToRPNTest()
        {
            string res = RPN.InfixToRPN("pv(~Q)");


            Assert.AreEqual("p Q ~ v", res);
        }

        [TestMethod()]
        public void FormatInfixStringTest()
        {
            string[] props = {
                "a&b>c",
                "c^d=f",
                "PropA &PropB^PropC>PropD",
                "pv~Q"
            };

            string[] results =
            {
                "a & b > c",
                "c ^ d = f",
                "PropA & PropB ^ PropC > PropD",
                "p v ~ Q"
            };
            for(int i = 0; i < props.Length; i++)
            {
                Assert.AreEqual(RPN.FormatInfixString(props[i]), results[i]);
            }
        }

        [TestMethod()]
        public void IsOperatorTest()
        {
            string[] operators = { "=>", "->", "!", "~", "=", "&" };
            string[] nonOperators = { "#", "@", "%", "*", "a", "sin", "cos"};

            foreach(string s in operators)
            {
                Assert.IsTrue(LogicAST.LegalCharacters.IsOperator(s));
            }
            foreach(string s in nonOperators)
            {
                Assert.IsFalse(LogicAST.LegalCharacters.IsOperator(s));
            }
        }
    }
}