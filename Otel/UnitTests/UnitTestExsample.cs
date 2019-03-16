using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otel;
using Otel.Model;

namespace UnitTests
{
    [TestClass]
    public class UnitTestExsample
    {
        MySQL sql;
        const int testYear = 2004;

        [TestMethod]
        public void Пример()
        {
            Assert.Equals(1, 1);
        }

        [TestMethod]
        public void TestCalendarAddDays()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");
            Calendar calendar = new Calendar(sql);
            calendar.DeleteDays(testYear);          
            calendar.InsertDays(testYear);
            string days = sql.Scalar("SELECT COUNT(*) FROM Calendar WHERE YEAR(day)="+testYear);
            Assert.AreEqual(days, DateTime.IsLeapYear(testYear)? "366" : "365");
        }

        [TestMethod]
        public void TestCalendarAddHoliday()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");            
            Calendar calendar = new Calendar(sql);
            calendar.AddHoliday(new DateTime(testYear, 9, 22));
            string days = sql.Scalar("SELECT COUNT(*) FROM Calendar WHERE day='2004-09-22'"+
                " AND holiday = 1 ");           
            Assert.AreEqual(days, "1");          
        }

        [TestMethod]
        public void TestCalendarDelHoliday()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");
            Calendar calendar = new Calendar(sql);
            calendar.DelHoliday(new DateTime(testYear, 9, 22));
            string days = sql.Scalar("SELECT COUNT(*) FROM Calendar WHERE day='2004-09-22'" +
                " AND holiday = 0 ");
            Assert.AreEqual(days, "1");
        }

        [TestMethod]
        public void TestCalendarDeleteDays()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");
            Calendar calendar = new Calendar(sql);
            calendar.DeleteDays(testYear);
                        
            string days = sql.Scalar("SELECT COUNT(*) FROM Calendar WHERE YEAR(day)=" + testYear);
            Assert.AreEqual(days, "0");
        }
    }
}
