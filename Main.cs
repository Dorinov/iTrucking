using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Security.Cryptography;

namespace iTrucking
{
    public partial class Main : Form
    {
        Timer timer = new Timer();

        int tid = 0;
        int id = 0;

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        static string cstr = "Host=localhost;Port=5432;Username=postgres;Password=65adf4gs65d4fb4s6dfg4;Database=gruzoperevozki;";
        NpgsqlConnection db_con = new NpgsqlConnection(cstr);

        public Main()
        {
            InitializeComponent();
            timer.Enabled = true;
            timer.Tick += new EventHandler(utimer);
        }
        private void utimer(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        private void button4_Click(object sender, EventArgs e) // ПОКАЗАТЬ ДАННЫЕ
        {

        }

        private void button5_Click(object sender, EventArgs e) // ОЧИСТИТЬ ДАННЫЕ
        {
            dataGridView1.Columns.Clear();
            tid = 0;
            id = 0;
            label1.Text = "";
            lockButtons();
        }

        private void button1_Click(object sender, EventArgs e) // РЕДАКТИРОВАТЬ
        {
            int y = dataGridView1.CurrentCellAddress.Y;
            id = Convert.ToInt32(dataGridView1[0, y].Value);
            AddEdit ae = new AddEdit();
            ae.Show();
            ae.setData(tid, id);
            id = 0;
        }

        private void button2_Click(object sender, EventArgs e) // ДОБАВИТЬ
        {
            AddEdit ae = new AddEdit();
            ae.Show();
            ae.setData(tid, id);
        }

        private void button3_Click(object sender, EventArgs e) // УДАЛИТЬ
        {
            string[] n1 = new string[] { "должность", "заказ", "клиент", "сотрудник", "услуги" };
            string[] n2 = new string[] { "Ид_должности", "Ид_заказа", "Ид_клиента", "Ид_сотрудника", "Ид_услуги" };
            int y = dataGridView1.CurrentCellAddress.Y;
            id = Convert.ToInt32(dataGridView1[0, y].Value);

            DialogResult dr = MessageBox.Show("Вы уверены, что хотите удалить строку с '" + n2[tid-1] + " = " + Convert.ToString(id) + "' в таблице '" + n1[tid-1] + "'?", "Удаление", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                db_con.Open();
                NpgsqlCommand cmdDel = new NpgsqlCommand($"delete from {n1[tid-1]} where {n2[tid-1]} = {Convert.ToString(id)}", db_con);
                cmdDel.ExecuteNonQuery();
                db_con.Close();
                refr(tid);
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e) // печать
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) // выход
        {
            Application.Exit();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }



        public void refr(int idd)
        {
            string[] nms = new string[] { "должность", "заказ", "клиент", "сотрудник", "услуги" };
            db_con.Open();
            dataGridView1.Columns.Clear();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from " + nms[idd - 1], db_con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            db_con.Close();
        }

        private void selTab(string arg)
        {
            db_con.Open();
            dataGridView1.Columns.Clear();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from " + arg, db_con);
            label1.Text = "Активная таблица: " + arg;
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            db_con.Close();
        }

        private void unlockButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
        }
        private void lockButtons()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
        }

        private void должностьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selTab("должность");
            tid = 1;
            unlockButtons();
        }

        private void заказToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selTab("заказ");
            tid = 2;
            unlockButtons();
        }

        private void клиентToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selTab("клиент");
            tid = 3;
            unlockButtons();
        }

        private void сотрудникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selTab("сотрудник");
            tid = 4;
            unlockButtons();
        }

        private void услугиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selTab("услуги");
            tid = 5;
            unlockButtons();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            refr(tid);
        }

        /*
        private static string stolen_method(string q, bool cript)
        {
            byte[] s1 = (cript) ? System.Text.UTF8Encoding.UTF8.GetBytes(q) : Convert.FromBase64String(q);
            SymmetricAlgorithm cripto = new RC2CryptoServiceProvider();
            cripto.Key = new byte[] { 0x10, 0x24, 0x76, 0x45, 0x84, 0x64, 0x25, 0x34 };
            cripto.IV = new byte[] { 0x14, 0x24, 0x76, 0x48, 0x84, 0x64, 0x25, 0x34 };
            s1 = ((ICryptoTransform)((cript) ? cripto.CreateEncryptor() : cripto.CreateDecryptor())).TransformFinalBlock(s1, 0, s1.Length);
            return (cript) ? Convert.ToBase64String(s1) : System.Text.UTF8Encoding.UTF8.GetString(s1).Trim();
        }*/
    }
}
