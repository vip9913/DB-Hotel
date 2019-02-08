using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otel
{
    public partial class Form1 : Form
    {
        MySQL sql;

        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            sql = new MySQL("localhost", "root", "7f651m", "hotel");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable client=sql.Select("SELECT * FROM Client");
           // MessageBox.Show(sql.Scalar("SELECT NOW()"));
            MessageBox.Show(client.Rows[0][1].ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sql.Insert("INSERT INTO Client VALUES(0, 'Тестор', 'test6@mail.ru', '06', 'addr6', 'программный');");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.Text = sql.Scalar("SELECT COUNT(*) FROM Client");
        }
    }
}
