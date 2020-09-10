using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitaryTestsVideoLessonsTest
{
    [TestClass]
    public class AssertClassTest
    {
        [TestMethod]
        [Owner("tatianex")]
        public void ContainsTest()
        {
            string str1 = "Tatiane Pedrelli";
            string str2 = "Pedrelli";

            StringAssert.Contains(str1, str2);
        }

        [TestMethod]
        [Owner("tatianex")]
        public void StartsWithTest()
        {
            string str1 = "Todos Caixa Alta";
            string str2 = "Todos Caixa";

            StringAssert.StartsWith(str1, str2);
        }

        [TestMethod]
        [Owner("tatianex")]
        public void IsAllLowerCaseTest()
        {
            Regex reg = new Regex(@"^([^A-Z])+$");

            StringAssert.Matches("todos caixa", reg);
        }

        [TestMethod]
        [Owner("tatianex")]
        public void IsNotAllLowerCaseTest()
        {
            Regex reg = new Regex(@"^([^A-Z])+$");

            StringAssert.DoesNotMatch("Todos Caixa", reg);
        }
    }
}
