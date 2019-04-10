using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Otel
{
    public class MySQL  //для демонстрации работы теста
    {
        string host;
        string user;
        string pass;
        string database;
        string connectionString;
        string error="";
        string query;
        MySqlConnection myConn;
        MySqlCommand cmd;

        public MySQL(string host, string user, string pass, string database)
        {
            this.host = host;
            this.user = user;
            this.pass = pass;
            this.database = database;
            this.connectionString = "SERVER=" + host +
                ";DATABASE=" + database +
                ";UID=" + user +
                ";PASSWORD=" + pass +
                ";CHARSET=utf8";
        }

        protected bool Open()
        {
            error = "";
            try
            {
                myConn = new MySqlConnection(connectionString);
                myConn.Open();
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                query = "CONNECTION TO MYSQL " + user + "@" + host;
                return false;
            }
        }

        protected bool Close()
        {
            try
            {
                myConn.Close();
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                query = "DISCONNECTION FROM MYSQL " + user + "@" + host;
                return false;   
            }
        }

        public string Scalar(string query)
        {
            string result = "";
            this.query = query;
            if (!Open()) return null;
            try
            {
                cmd = new MySqlCommand(query, myConn);
                result=cmd.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
            Close();
            return result;
        }

        internal string DateToString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public DataTable Select(string query)
        {
            DataTable table = null;
            this.query = query;
            if (!Open()) return table;
            try
            {
                cmd = new MySqlCommand(query, myConn);
                MySqlDataReader reader=cmd.ExecuteReader();//возвращает запросы в таблицу
                table = new DataTable("table");//временная таблица в памяти
                table.Load(reader);//считываем в таблицу все что можно
                return table;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }  
            
             
        }

        public long Insert(string query) // return last inserting id
        {
            int rows = Update(query);
            if (rows > 0) return cmd.LastInsertedId; 
            return 0;
        }

        public int Update(string query) //update or delete
        {
            int rows = 0; //сколько строк обработано
            this.query = query;
            if (!Open()) return -1;
            try
            {
                cmd = new MySqlCommand(query, myConn);
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return -1;                
            }
            return rows;
        }

        public string addslashes(string text)
        {
            return text.Replace("'", "\\");
        }

        public bool SqlError()
        {
            if (error == "") return false;
            DialogResult dr=
            MessageBox.Show(error+"\n"+
                "Запрос: \n"+query+
                "\nAbort - закрыть программу"+
                "\nRetry - повторить запрос"+
                "\nIgnore- пропустить ошибку",
                "Ошибка БД"+user+"@"+host,
                MessageBoxButtons.AbortRetryIgnore);
            if (dr == DialogResult.Abort)
            {
                //Environment.Exit(0);
                Environment.FailFast("Error MySQL");
                //Application.Exit();
                return true;
            }
            if (dr == DialogResult.Abort) return true;
            return false;
        }
    }
}
