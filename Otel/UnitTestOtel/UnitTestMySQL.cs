using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otel;

namespace UnitTestOtel
{
    [TestClass]
    public class UnitTestMySQL
    {
        [TestMethod]
        public void TestMySQLConnect()
        {
           // MySQL sql;
            Assert.AreEqual(2,3);
        }
    }
}
