using System;
using System.Data;
using System.Windows.Forms;

namespace Otel
{
    public partial class Otel : Form
    {
        MySQL sql;

        public Otel()
        {
            InitializeComponent();
            timer1.Enabled = true;
            sql = new MySQL("localhost", "root", "7f651m", "hotel");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable client=sql.Select("SELECT * FROM Client");
            // MessageBox.Show(sql.Scalar("SELECT NOW()"));
            dataGridView1.DataSource=client;
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
    }
}
