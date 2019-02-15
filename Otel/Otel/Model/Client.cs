using System.Data;

namespace Otel.Model
{
    class Client
    {
        public long id { get; private set; }
        public string client { get; private set; }
        public string email { get; private set; }
        public string phone { get; private set; }
        public string address { get; private set; }
        public string info { get; private set; }
        private MySQL sql;

        public Client(MySQL sql)
        {
            id = 0;
            client = "";
            email = "";
            phone = "";
            address = "";
            info = "";
            this.sql = sql;
        }

        /// <summary>
        /// Установка параметров клиента
        /// </summary>
        /// <param name="client">Установить пераметр</param>
        public void SetClient(string client)
        {
            this.client = client;
        }

        public void SetEmail(string email)
        {
            this.email = email;
        }

        public void SetPhone(string phone)
        {
            this.phone = phone;
        }

        public void SetAddress(string address)
        {
            this.address = address;
        }

        /// <summary>
        ///                  Дополнительные комменты
        /// </summary>
        /// <param name="info">Установите дополнительные комменты</param>
        public void SetInfo(string info)
        {
            this.info = info;
        }

        /// <summary>
        /// Регистрация нового клиента
        /// </summary>
        public void InsertClient()
        {
            string query = "INSERT INTO Client (client, email, phone, address, info) " +
                 "VALUES ('" + sql.addslashes(client) +
                     "', '" + sql.addslashes(email) +
                     "', '" + sql.addslashes(phone) +
                     "', '" + sql.addslashes(address) +
                     "', '" + sql.addslashes(info) + "')";
            do this.id = this.sql.Insert(query);
            while (this.sql.SqlError());
        }

        /// <summary>
        /// получение списка клиентов
        /// </summary>
        public DataTable SelectClients()
        {
            DataTable client;
            string query = "SELECT * FROM Client";
            do client = sql.Select(query);
            while (sql.SqlError());
            return client;
        }

        /// <summary>
        /// получение списка клиентов по фильтру
        /// </summary>
        public DataTable SelectClients(string find)
        {
            DataTable client;
            find = sql.addslashes(find);
            string query = "SELECT id, client, email, phone, address, info FROM Client" +
                " WHERE client LIKE '%" + find + "%'" +
                " OR email     LIKE '%" + find + "%'" +
                " OR phone     LIKE '%" + find + "%'" +
                " OR address   LIKE '%" + find + "%'" +
                " OR info      LIKE '%" + find + "%'" +
                " OR id=             '" + find + "'";
            do client = sql.Select(query);
            while (sql.SqlError());
            return client;
        }

        /// <summary>
        /// Получение всей информации о клиенте
        /// </summary>
        /// <param name="client_id">№ клиента</param>
        /// <returns>True-успешно, False - не так успешно как хотелось бы</returns>
        public bool SelectClient(long client_id)
        {
            DataTable client;
            string query = "SELECT * FROM Client WHERE id = '" + sql.addslashes(client_id.ToString()) + "'";
            do client = sql.Select(query);
            while (sql.SqlError());
            if (client.Rows.Count == 0) return false;
            this.id = long.Parse(client.Rows[0]["id"].ToString());
            this.client = client.Rows[0]["client"].ToString();
            this.email = client.Rows[0]["email"].ToString();
            this.phone = client.Rows[0]["phone"].ToString();
            this.address = client.Rows[0]["address"].ToString();
            this.info = client.Rows[0]["info"].ToString();
            return true;
        }

        /// <summary>
        /// Изменение данных клиента
        /// </summary>
        /// <param name="client_id">№ клиента</param>
        /// <returns>True-успешно, False - не так успешно как хотелось бы</returns>
        public bool UpdateClient(long client_id)
        {
            if (SelectClient(client_id))
            {
                int result = 0;
                string query = "UPDATE Client " +
                " SET client  = '" + sql.addslashes(this.client) + "'," +
                "     email   = '" + sql.addslashes(this.email) + "'," +
                "     phone   = '" + sql.addslashes(this.phone) + "'," +
                "     address = '" + sql.addslashes(this.address) + "'," +
                "     info    = '" + sql.addslashes(this.client) + "' " +
                " WHERE id    = '" + sql.addslashes(client_id.ToString()) + "'";
                do result = sql.Update(query);
                while (sql.SqlError());
                return (result == 0) ? false : true;
            }
            else return false;
        }
    }
}