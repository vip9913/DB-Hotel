using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Otel;
using Otel.Model;
using System.Data;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        MySQL sql;
        const int testYear = 2004;

        [TestMethod]
        public void Пример()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void TestCalendarAddDays()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");
            Calendar calendar = new Calendar(sql);
            calendar.DeleteDays(testYear);
            calendar.InsertDays(testYear);
            string days = sql.Scalar("SELECT COUNT(*) FROM Calendar WHERE YEAR(day)=" + testYear);
            Assert.AreEqual(days, DateTime.IsLeapYear(testYear) ? "366" : "365");
        }

        [TestMethod]
        public void TestCalendarAddHoliday()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");
            Calendar calendar = new Calendar(sql);
            calendar.AddHoliday(new DateTime(testYear, 9, 22));
            string days = sql.Scalar("SELECT COUNT(*) FROM Calendar WHERE day='2004-09-22'" +
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

        [TestMethod]
        public void TestInsertRoom()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");
            Room room = new Room(sql);
            //room.InsertRoom();

            string rooms = sql.Scalar("INSERT INTO Room (room='apartament', beds=1, info='supper nomer for test sexy')");
            //string rooms = sql.Scalar("INSERT INTO room SET room = 'Double 103', beds = 1, floor = '1', info = 'No Nice bed'");
            // Assert.AreEqual(rooms, "1");
           // Assert.IsTrue(room.InsertRoom());
        }

        [TestMethod]
        private void TestUpdateRoom()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");
            Room room = new Room(sql);
            room.UpdateRoom();
        }

        [TestMethod]
        private void TestDeleteRoom()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");
            Room room = new Room(sql);
            room.DeleteRoom(room.id);
        }

        [TestMethod]
        public void TestRoom()
        {
            MySQL sql = new MySQL("localhost", "root", "7f651m", "hotel");
            Room room = new Room(sql);
            long RoomID;
            string RoomInfo1 = "Info 123456789";
            string RoomInfo2 = "Info 1234567890";

            room.SetBeds(1);
            room.SetRoom("My room");
            room.SetFloor("99");
            room.SetInfo(RoomInfo1);

            Assert.IsTrue(room.InsertRoom());//проверка на выполнение функции
            RoomID = room.id;

            DataTable table= room.SelectRooms();
            bool found = false;
            foreach (DataRow row in table.Rows)
            {
                if (row["info"].ToString() == RoomInfo1 && row["id"].ToString() == RoomID.ToString())
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);

            Assert.IsTrue(room.SelectRoom(RoomID));
            room.SetInfo(RoomInfo2);
            Assert.IsTrue(room.UpdateRoom());
            room.SetInfo("");//Просто забиваем данные если предудущая функция пустая или не сработала
            Assert.IsTrue(room.SelectRoom(RoomID));

            Assert.AreEqual(RoomInfo2, room.info);

            Assert.IsTrue(room.DeleteRoom(RoomID));
            Assert.IsFalse(room.SelectRoom(RoomID));
            //TestInsertRoom();
            //TestUpdateRoom();
            //TestDeleteRoom();
        }
    }
}
