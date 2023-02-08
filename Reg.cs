using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace iTrucking
{
    public partial class Reg : Form
    {
        static string cstr = "Host=localhost;Port=5432;Username=postgres;Password=65adf4gs65d4fb4s6dfg4;Database=gruzoperevozki;";
        NpgsqlConnection db_con = new NpgsqlConnection(cstr);

        int maxId;

        public Reg()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // login
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) // pass
        {

        }

        private void button2_Click(object sender, EventArgs e) // reg
        {
            db_con.Open();

            NpgsqlCommand cmd1 = new NpgsqlCommand("select Ид_сотрудника from сотрудник", db_con);
            using (var reader = cmd1.ExecuteReader())
            {
                while (reader.Read())
                {
                    maxId = Convert.ToInt32(reader["Ид_сотрудника"]);
                }

            }
            maxId += 1;

            NpgsqlCommand cmd2 = new NpgsqlCommand($"insert into сотрудник values({maxId},null,null,null,'{textBox1.Text}',null,'{textBox2.Text}')", db_con);
            cmd2.ExecuteNonQuery();

            db_con.Close();

            Main main = new Main();
            main.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e) // back
        {
            Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void Reg_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
