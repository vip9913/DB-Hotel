using System;
using System.Data;


namespace Otel.Model
{
    public class Calendar
    {
        private MySQL sql;

        public Calendar(MySQL sql)
        {           
            this.sql = sql;
        }

        /// <summary>
        /// генерация календаря на заданный год
        /// </summary>
        /// <param name="year">На какой год создать календарь</param>
        public void InsertDays(int year)
        {
            DateTime day = new DateTime(year, 1, 1);
            while (day.Year == year)
            {
                int wend = 0;
                if (day.DayOfWeek == DayOfWeek.Friday || day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday) wend = 1;
                string query=
                                "INSERT IGNORE INTO Calendar"+
                                " SET day = '"+day.ToString("yyyy-MM-dd")+"',"+
                                "     wend = "+wend+","+
                                "     holiday = 0";
                do this.sql.Insert(query);
                while (sql.SqlError());
                day=day.AddDays(1);
            }

        }

        /// <summary>
        /// Удаление дней календаря на заданный год
        /// </summary>
        /// <param name="year">На какой год создать календарь</param>
        public void DeleteDays(int year)
        {           
            string query =      "DELETE FROM Calendar " +
                                " WHERE YEAR(day) = '" + year + "'";
                do this.sql.Update(query);
                while (sql.SqlError());                        
        }

        /// <summary>
        /// Установка/Сброс праздничного дня
        /// </summary>
        /// <param name="day"></param>
        /// <param name="holiday"></param>
        private void UpdateHoliday(DateTime day, bool holiday)
        {
            string query = "UPDATE Calendar " +
          " SET holiday = " + (holiday? "1" : "0")+
          " WHERE day = '" + day.ToString("yyyy-MM-dd") + "'" +
          " LIMIT 1";
            do this.sql.Update(query);
            while (sql.SqlError());
        }

        /// <summary>
        /// добавление выходного дня
        /// </summary>
        /// <param name="day">День для установки</param>
        public void AddHoliday(DateTime day)
        {
            UpdateHoliday(day, true);
        }

        /// <summary>
        /// удаление праздничного дня
        /// </summary>
        /// <param name="day">День для установки</param>
        public void DelHoliday(DateTime day)
        {
            UpdateHoliday(day, false);
        }

    }
}
