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
    public class RPNTests
    {
        [TestMethod()]
        public void InfixToRPNTest()
        {
            string[] props = {
                "p"
            };

            string[] results =
            {
                "p"
            };
            for (int i = 0; i < props.Length; i++)
            {
                string formatted = RPN.InfixToRPN(props[i]);
                Assert.AreEqual(formatted, results[i]);
            }
        }

        [TestMethod()]
        public void FormatInfixStringTest()
        {
            string[] props = {
                "a&b>c",
                "c^d=f",
                "PropA &PropB^PropC>PropD",
                "pv~Q",
                "p"
            };

            string[] results =
            {
                "a & b > c",
                "c ^ d = f",
                "PropA & PropB ^ PropC > PropD",
                "p v ~ Q",
                "p"
            };
            for(int i = 0; i < props.Length; i++)
            {
                string formatted = RPN.FormatInfixString(props[i]);
                Assert.AreEqual(formatted, results[i]);
            }
        }

        [TestMethod()]
        public void IsOperatorTest()
        {
            string[] operators = { "=>", "->", "!", "~", "=", "&" };
            string[] nonOperators = { "#", "@", "%", "*", "a", "sin", "cos"};

            foreach(string s in operators)
            {
                Assert.IsTrue(LegalCharacters.IsOperator(s));
            }
            foreach(string s in nonOperators)
            {
                Assert.IsFalse(LegalCharacters.IsOperator(s));
            }
        }
    }
}