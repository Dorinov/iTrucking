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
    public partial class Form1 : Form
    {
        int click = 0;
        string login;
        string password;
        bool b_stop = true;
        bool b_stopClick = false;

        static string cstr = "Host=localhost;Port=5432;Username=postgres;Password=65adf4gs65d4fb4s6dfg4;Database=gruzoperevozki;";
        NpgsqlConnection db_con = new NpgsqlConnection(cstr);

        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e) // вход
        {
            trySignIn();
        }

        private void button2_Click(object sender, EventArgs e) // регистрация
        {
            Reg reg = new Reg();
            reg.Show();
            Hide();
        }

        private void trySignIn()
        {
            click += 1;
            b_stopClick = true;
            db_con.Open();

            NpgsqlCommand cmd1 = new NpgsqlCommand("select номер_телефона from сотрудник", db_con);
            using (var reader = cmd1.ExecuteReader())
            {
                while (reader.Read() && b_stop)
                {
                    login = reader["номер_телефона"].ToString();
                    if (login == textBox1.Text)
                    {
                        b_stop = false;
                    }
                }
            }

            if (click == 4 && b_stopClick)
            {
                b_stopClick = false;
                Kap4a kap4a = new Kap4a();
                kap4a.ShowDialog();
            }
            else if (b_stop)
            {
                MessageBox.Show(
                    "Такого логина не существует!",
                    "Ошибка входа",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                b_stop = true;

                NpgsqlCommand cmd2 = new NpgsqlCommand("select пароль from сотрудник", db_con);
                using (var reader = cmd2.ExecuteReader())
                {
                    while (reader.Read() && b_stop)
                    {
                        password = reader["пароль"].ToString();
                        if (password == textBox2.Text)
                        {
                            b_stop = false;
                        }
                    }
                }

                if (b_stop)
                {
                    MessageBox.Show(
                        "Неправильно набран пароль!",
                        "Ошибка входа",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else if (!(b_stop))
                {
                    Main main = new Main();
                    main.Show();
                    Hide();
                }
            }
            b_stop = true;
            db_con.Close();
        }

        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                trySignIn();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
