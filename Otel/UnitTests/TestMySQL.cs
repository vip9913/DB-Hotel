using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otel;	

namespace TestOtel
{
    [TestClass]
    public class TestMySQL
    {
        [TestMethod]
        public void TestMySQLConnection()
        {
            MySQL sql=new MySQL("localhost", "root", "7f651m", "hotel");
            string result = sql.Scalar("SELECT 5+10");
            Assert.AreEqual(result,"15");
            Assert.Equals(1, 1);
            Assert.IsFalse(1==0);
        }
    }
}
