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

        private void button2_Click(object sender, EventArgs e) // reg
        {
            trySignUp();
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



        private void trySignUp()
        {
            db_con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select номер_телефона from сотрудник", db_con);
            bool found = false;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string s = Convert.ToString(reader["номер_телефона"]);
                    if (s == textBox1.Text)
                    {
                        found = true;
                        break;
                    }
                }

            }

            if (found)
            {
                MessageBox.Show(
                    "Аккаунт с таким логином уже существует, вернитесь ко входу!",
                    "Ошибка регистрации",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
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

                Main main = new Main();
                main.Show();
                Hide();
            }
            db_con.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                trySignUp();
            }
        }
    }
}
