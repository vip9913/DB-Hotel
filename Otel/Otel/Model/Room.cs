using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otel.Model
{
    public class Room
    {
        private MySQL sql;
        public long id { get; private set; }
        public string room { get; private set; }
        public int beds { get; private set; }
        public string floor { get; private set; }
        public int step { get; private set; }
        public string info { get; private set; }

        public void SetRoom(string room)
        {
            this.room = room;
        }

        public void SetBeds(int beds)
        {
            this.beds = beds;
        }

        public void SetFloor(string floor)
        {
            this.floor = floor;
        }

        public void SetInfo(string info)
        {
            this.info = info;
        }

        /// <summary>
        /// конструктор для управления комнатами
        /// </summary>
        /// <param name="sql"></param>
        public Room(MySQL sql)
        {
            this.sql = sql;
            this.id = 0;
        }

        /// <summary>
        /// Выбор всех комнат
        /// </summary>
        /// <returns></returns>
        public DataTable SelectRooms()
        {
            DataTable room;
            string query = "SELECT id, room, beds, floor, step, info FROM Room ORDER BY step";
            do room = sql.Select(query);
            while (sql.SqlError());
            return room;
        }

        /// <summary>
        /// Добавление новой комнаты
        /// </summary>
        public bool InsertRoom()
        {
            string query = "INSERT INTO Room (room, beds, floor, info) " +
                 "VALUES ('" + sql.addslashes(this.room) +
                     "', '" + sql.addslashes(this.beds.ToString()) +
                     "', '" + sql.addslashes(this.floor) + 
                     "', '" + sql.addslashes(this.info) + "')";
            do this.id = this.sql.Insert(query);
            while (this.sql.SqlError());
            if (this.id <= 0) return false;

            //query = "UPDATE Room SET SET step= "+
            //                  this.id.ToString()+
            //    " WHERE id= "+this.id.ToString();
            //do sql.Update(query);
            //while (sql.SqlError());
            return true;          
        }

        /// <summary>
        /// Поиск нужной комнаты
        /// </summary>
        /// <param name="room_id"></param>
        /// <returns></returns>
        public bool SelectRoom(long room_id)
        {
            DataTable room;
            string query = "SELECT * FROM Room WHERE id = '" + sql.addslashes(room_id.ToString()) + "'";
            do room = sql.Select(query);
            while (sql.SqlError());
            if (room.Rows.Count == 0) return false;
            this.id = long.Parse(room.Rows[0]["id"].ToString());
            this.room = room.Rows[0]["room"].ToString();
            this.beds = int.Parse(room.Rows[0]["beds"].ToString());
            this.floor = room.Rows[0]["floor"].ToString();
       //   this.step = int.Parse(room.Rows[0]["step"].ToString());
            this.info = room.Rows[0]["info"].ToString();
            return true;
        }

        /// <summary>
        /// Редактирование комнаты
        /// </summary>        
        /// <returns>True-успешно, False - не успешно</returns>
        public bool UpdateRoom()
        {
            if (this.id <= 0) return false;
                int result = 0;
                string query = "UPDATE Room " +
                 " SET room  = '" + sql.addslashes(this.room) + "'," +
            "     beds   = '" + sql.addslashes(this.beds.ToString()) + "'," +
            "     floor   = '" + sql.addslashes(this.floor) + "'," +
            "     info    = '" + sql.addslashes(this.info) + "' " +
            " WHERE id    = '" + sql.addslashes(this.id.ToString()) + "' LIMIT 1";            
                do result = sql.Update(query);
                while (sql.SqlError());
                return (result == 0) ? false : true;                      
        }

        /// <summary>
        /// Удаление комнат по ID если нигде не используется (не задействована)
        /// </summary>
        /// <param name="room_id">№ комнаты</param>
        /// <returns></returns>
        public bool DeleteRoom(long room_id)
        {
            int result = 0;
            string query = "DELETE FROM Room " +
            " WHERE id    = '" + sql.addslashes(this.id.ToString()) + "' LIMIT 1";
            do result = sql.Update(query);
            while (sql.SqlError());
            return (result == 0) ? false : true;
        }
    }
}
