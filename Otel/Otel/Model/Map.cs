using System;
using System.Data;

namespace Otel.Model
{
    public class Map
    {
        private MySQL sql;

        long room_id;
        long book_id;
        DateTime calendar_day;

        public string status { get; private set; }
        public int adults { get; private set; }
        public int childs { get; private set; }

        public Map(MySQL sql)
        {
            this.sql = sql;
            room_id = -1;
            book_id = -1;
            calendar_day = DateTime.MinValue;
        }

        public void SetStatus()
        {
            this.status = status;
        }

        public void SetAdults()
        {
            this.adults = adults;
        }

        public void SetChilds()
        {
            this.childs = childs;
        }

        public void SelectMap(long root_id, long book_id, DateTime calendar_day)
        {
            this.room_id = room_id;
            this.book_id = book_id;
            this.calendar_day = calendar_day;

            DataTable map;
            string query = "SELECT status, adults, childs  FROM Map" +
                " WHERE room_id = '" + sql.addslashes(this.room_id.ToString()) + "'" +
                " AND book_id = '" + sql.addslashes(this.book_id.ToString()) + "'" +
                " AND calendar_day = '" + sql.DateToString(this.calendar_day) + "'";
            do map = sql.Select(query);
            while (sql.SqlError());

            if (map.Rows.Count == 0)
            {
                InsertMapNone();//добавление пустой комнаты
            }
            else
            {
                this.status = map.Rows[0]["status"].ToString();
                this.adults = int.Parse(map.Rows[0]["adults"].ToString());
                this.childs = int.Parse(map.Rows[0]["childs"].ToString());
            }
        }

        private void InsertMapNone()
        {
            this.status = "none";
            this.adults = 0;
            this.childs = 0;
            InsertMap(); //если нет данных значит вносим
        }

        private void InsertMap()
        {
            do this.sql.Insert(
            @"INSERT INTO Map
            SET room_id = " + this.room_id + @",
            book_id = " + this.book_id + @",
            calendar_day = '" + this.sql.DateToString(this.calendar_day) + @"',
            status = '" + this.status + @"',
            adults = " + this.adults + @",
            childs = " + this.childs);
            while (this.sql.SqlError());
        }

        public DataTable SelectMap(DateTime from_day, DateTime till_day)
        {
            DataTable map;
            do map = sql.Select(
                @"SELECT room_id, book_id, calendar_day, status, adults, childs
                FROM map
                WHERE calendar_day BETWEEN '" +
                sql.DateToString(from_day) + "' AND '" +
                sql.DateToString(till_day) + "'");
            while (this.sql.SqlError());
            return map;
        }

        public void DeleteMap()
        {
            if (room_id < 0 || book_id < 0 || calendar_day == DateTime.MinValue) return; //проверки на глупость
            string query = "DELETE FROM Map" +
               " WHERE room_id = '" + sql.addslashes(this.room_id.ToString()) + "'" +
               " AND book_id = '" + sql.addslashes(this.book_id.ToString()) + "'" +
               " AND calendar_day = '" + sql.DateToString(this.calendar_day) + "'"+
               " LIMIT 1";
            do sql.Update(query);
            while (sql.SqlError());
        }

        public void UpdateMap()
        {
            do sql.Update(
              @"UPDATE Map " +
              " SET status = '"+sql.addslashes(status)             +"',"+
              " adults = "+adults+", "+
              " childs = "+childs+" "+
              " WHERE room_id = '" + sql.addslashes(this.room_id.ToString()) + "'" +
              " AND book_id = " + sql.addslashes(this.book_id.ToString()) + "'" +
              " AND calendar_day = " + sql.DateToString(this.calendar_day) + "'" +
              " LIMIT 1");            
            while (this.sql.SqlError());
        }
    }
}
