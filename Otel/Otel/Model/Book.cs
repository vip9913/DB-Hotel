using System;
using System.Data;

namespace Otel.Model
{
    public class Book
    {
        private MySQL sql;
        public long id { get; private set; }
        public long client_id { get; private set; }
        public DateTime book_date { get; private set; }
        public DateTime from_day { get; private set; }
        public DateTime till_day { get; private set; }
        public int adults { get; private set; } //update
        public int childs { get; private set; } //update
        public string status { get; private set; }
        public string info { get; private set; } //update

        /// <summary>
        /// конструктор класса
        /// </summary>
        /// <param name="sql"></param>
        public Book(MySQL sql)
        {
            id = 0;           
            this.sql = sql;
        }

        public void SetAdults(int adults)
        {
            this.adults = adults;
        }

        public void SetChilds(int childs)
        {
            this.childs = childs;
        }

        public void SetInfo(string info)
        {
            this.info = info;
        }

        /// <summary>
        /// Добавление новой резервации комнаты
        /// </summary>
        /// <param name="client_id">Для какого клиента</param>
        /// <param name="from_day">С какой даты</param>
        /// <param name="till_day">По какую дату (за день до выезда)</param>
        /// <returns>True-успешно</returns>
        public bool InsertBook(long client_id, DateTime from_day, DateTime till_day)
        {
            // создание новой регистрации
            this.sql.Insert(
            "INSERT INTO Book " +
            "SET client_id ='" + client_id + "', " +
            "book_date = now(), "+    
            "    from_day = '" + sql.DateToString(from_day) + "', " +
            "till_day = '" + sql.DateToString(till_day) + "', " +
            "adults = '" + this.adults + "' ," +
            "childs = '" + this.childs + "', " +
            "status = 'wait', "+    
            "info = '" + sql.addslashes(this.info) + "'");
            while (sql.SqlError()) ;
            return (this.id > 0);
        }

        /// <summary>
        /// Получение данных для заданной резервации (бронирования)
        /// </summary>
        /// <param name="book_id"></param>
        /// <returns></returns>
        public bool SelectBook(long book_id)
        {
            DataTable book;
            this.id = book_id;
            do book = sql.Select(
                "SELECT client_id, book_date, from_day, till_day, adults, childs, status, b.info " +
                " FROM Book= " +
                " WHERE id='" + sql.addslashes(this.id.ToString()) + "'");
            while (sql.SqlError());
            if (book.Rows.Count == 0)
                return false;
            this.id = long.Parse(book.Rows[0]["id"].ToString());
            this.client_id = long.Parse(book.Rows[0]["client_id"].ToString());
            this.book_date = DateTime.Parse(book.Rows[0]["book_date"].ToString());
            this.from_day = DateTime.Parse(book.Rows[0]["from_day"].ToString());
            this.till_day = DateTime.Parse(book.Rows[0]["till_day"].ToString());
            this.adults = int.Parse(book.Rows[0]["adults"].ToString());
            this.childs = int.Parse(book.Rows[0]["childs"].ToString());
            this.status = book.Rows[0]["status"].ToString();
            this.info = book.Rows[0]["info"].ToString();
            return true;
        }

        /// <summary>
        /// Обновление данных ()
        /// </summary>
        /// <param name="book_id"></param>
        /// <returns>True- успешно</returns>
        public bool UpdateBook(long book_id)
        {
            if (this.id <= 0) return false;

            int result = 0;
            string query = "UPDATE Book " +
             " SET adults  = '" + sql.addslashes(this.adults.ToString()) + "'," +
        "     childs   = '" + sql.addslashes(this.childs.ToString()) + "'," +        
        "     info    = '" + sql.addslashes(this.info) + "' " +
        " WHERE id    = '" + sql.addslashes(this.id.ToString()) + "' LIMIT 1";
            do result = sql.Update(query);
            while (sql.SqlError());
            return (result == 0) ? false : true;
        }

        /// <summary>
        /// изменение статуса брони
        /// </summary>
        /// <param name="status">waiting/configrm/deleded</param>
        /// <returns></returns>
        private bool UpdateStatus(string status)
        {
            if (status != "waiting" &&
                status != "confirm" &&
                status != "deleted" || this.id<=0) return false;

            int result = 0;
            string query = "UPDATE Book " +
            " SET status  = '" + status +"' "+       
            " WHERE id    = '" + sql.addslashes(this.id.ToString()) + "' LIMIT 1";
            do result = sql.Update(query);
            while (sql.SqlError());
            return (result == 1);
        }

        /// <summary>
        /// Ожидание подтверждения брони
        /// </summary>
        /// <returns></returns>
        public bool SetStatusWaiting()
        {
            return UpdateStatus("waiting");
        }

        /// <summary>
        /// Бронь подтверждена
        /// </summary>
        /// <returns></returns>
        public bool SetStatusConfirm()
        {
            return UpdateStatus("confirm");
        }

        /// <summary>
        /// Бронь снята
        /// </summary>
        /// <returns></returns>
        public bool SetStatusDeleted()
        {
            return UpdateStatus("deleted");
        }

        /// <summary>
        /// Смена даты въезда
        /// </summary>
        /// <param name="from_day">С какой даты</param>
        /// <returns></returns>
        public bool UpdateFromDay(DateTime from_day)
        {
            if (this.id <= 0) return false;
            this.from_day = from_day;
            int result;
            do result = sql.Update(
              "UPDATE Book " +
            " SET from_day  = '" + sql.DateToString(from_day) + "' " +
            " WHERE id    = '" + sql.addslashes(this.id.ToString()) + "' LIMIT 1");
            while (sql.SqlError());
            return (result==1);
        }

        /// <summary>
        /// Смена даты выезда
        /// </summary>
        /// <param name="till_day">Дата выезда</param>
        /// <returns></returns>
        public bool UpdateTillDay(DateTime till_day)
        {
            if (this.id <= 0) return false;
            this.till_day = till_day;
            int result;
            do result = sql.Update(
              "UPDATE Book " +
            " SET from_day  = '" + sql.DateToString(till_day) + "' " +
            " WHERE id    = '" + sql.addslashes(this.id.ToString()) + "' LIMIT 1");
            while (sql.SqlError());
            return (result == 1);
        }

        /// <summary>
        /// получение списка броней
        /// </summary>
        public DataTable SelectBooks()
        {
            DataTable book;
            string query = @"SELECT client_id, client, book_date, from_day, till_day,
            adults, childs, status, info 
            FROM Book b
            LEFT JOIN Client c
                ON b.client_id=c.id
            ORDER BY book_date";
            do book = sql.Select(query);
            while (sql.SqlError());
            return book;
        }

        /// <summary>
        /// поиск броней по фильтру
        /// </summary>
        public DataTable SelectBooks(string find)
        {
            find = sql.addslashes(find);
            DataTable book;
            string query = @"SELECT client_id, client, book_date, from_day, till_day,
            adults, childs, status, info 
            FROM Book b
            LEFT JOIN Client c
                ON b.client_id=c.id
WHERE Client LIKE '%"+find+"%'"+
	" OR book_date LIKE '"+find+"%'"+
	" OR from_day  LIKE '"+find+"%'"+
    " OR till_day  LIKE '"+find+"%'"+
    " OR adults=  '"+find+"'"+
    " OR childs=  '"+find+"'"+
    " OR status = '"+find+"'"+
    " OR info LIKE '%"+find+"%'"+
     "       ORDER BY book_date;";
            do book = sql.Select(query);
            while (sql.SqlError());
            return book;
        }
    }
}
