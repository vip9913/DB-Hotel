using System;
using System.Data;
using System.Windows.Forms;

namespace Otel
{
    public partial class Otel : Form
    {
        MySQL sql;
        Model.Client mClient;

        public Otel()
        {
            InitializeComponent();
            timer1.Enabled = true;
            sql = new MySQL("localhost", "root", "7f651m", "hotel");

            mClient = new Model.Client(sql);
            //mClient.SetInfo("test Insert");
            //mClient.SetClient("Маша");
            //mClient.SetAddress("Луна");
            //mClient.InsertClient();
            //MessageBox.Show(mClient.id.ToString());          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DataTable client=sql.Select("SELECT * FROM Client");
            // MessageBox.Show(sql.Scalar("SELECT NOW()"));
            dataGridView1.DataSource=mClient.SelectClients();
            dataGridView1.Columns[0].HeaderText = "№";
           // MessageBox.Show(client.Rows[0][1].ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            do sql.Insert("INSERT INTO Client VALUES(3, 'Тестор', 'test6@mail.ru', '06', 'addr6', 'программный');");
            while (sql.SqlError());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.Text = sql.Scalar("SELECT COUNT(*) FROM Client");
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //при вводе текста в строку
        {
            dataGridView1.DataSource = mClient.SelectClients(textBox1.Text);
            dataGridView1.Columns[0].HeaderText = "№";

        }

        //при щелкании на строчку выдает id
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//при щелчке по столбцу
        {
            Random rnd = new Random();
            long id = long.Parse(dataGridView1[0, e.RowIndex].Value.ToString());

            //mClient.SelectClient(long.Parse(dataGridView1[0, e.RowIndex].Value.ToString()));           

            mClient.SelectClient(id);
            string new_phone = rnd.Next(10000, 99999).ToString();
            mClient.SetPhone(new_phone);
            mClient.UpdateClient();
            dataGridView1.Refresh();         

            //MessageBox.Show(mClient.client);
            //MessageBox.Show(long.Parse(dataGridView1[0, e.RowIndex].Value.ToString()).ToString());

        }
    }
}
