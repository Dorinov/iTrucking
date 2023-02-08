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
    public partial class AddEdit : Form
    {
        // location: 12, 12
        // 1 = должность
        // 2 = заказ
        // 3 = клиент
        // 4 = сотрудник
        // 5 = услуги
        string[] nms = new string[] { "должность", "заказ", "клиент", "сотрудник", "услуги" };

        int tid = 0;
        int id = 0;

        static string cstr = "Host=localhost;Port=5432;Username=postgres;Password=65adf4gs65d4fb4s6dfg4;Database=gruzoperevozki;";
        NpgsqlConnection db_con = new NpgsqlConnection(cstr);

        public AddEdit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // ОТМЕНА
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) // ПОДТВЕРДИТЬ
        {
            if (checkBoxs(tid))
            {
                db_con.Open();
                if (id == 0)
                {
                    int maxId = 0;
                    string zapr = "";
                    NpgsqlCommand cmd2 = new NpgsqlCommand("select * from " + nms[tid - 1], db_con);
                    using (var reader = cmd2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maxId = Convert.ToInt32(reader[0]);
                        }
                    }
                    maxId += 1;

                    if (tid == 1)
                    {
                        zapr = $"insert into должность values({Convert.ToString(maxId)}, '{textBox15.Text}', {textBox12.Text})";
                    }
                    else if (tid == 2)
                    {
                        zapr = $"insert into заказ values({Convert.ToString(maxId)}, {textBox4.Text}, {textBox2.Text}, {textBox7.Text}, '{textBox5.Text}', '{textBox8.Text}', '{textBox3.Text}', '{textBox10.Text}', '{textBox6.Text}', {Convert.ToString(checkBox1.Checked)});";
                    }
                    else if (tid == 3)
                    {
                        zapr = $"insert into клиент values({Convert.ToString(maxId)}, '{textBox17.Text}', '{textBox13.Text}', '{textBox16.Text}', '{textBox20.Text}', '{textBox11.Text}');";
                    }
                    else if (tid == 4)
                    {
                        zapr = $"insert into сотрудник values({Convert.ToString(maxId)}, '{textBox24.Text}', '{textBox19.Text}', '{textBox26.Text}', '{textBox14.Text}', {textBox9.Text}, '{textBox25.Text}');";
                    }
                    else if (tid == 5)
                    {
                        zapr = $"insert into услуги values({Convert.ToString(maxId)}, {textBox31.Text}, {textBox22.Text}, '{textBox33.Text}');";
                    }
                    try
                    {
                        NpgsqlCommand cmd3 = new NpgsqlCommand(zapr, db_con);
                        cmd3.ExecuteNonQuery();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Convert.ToString(ex),
                            "Ошибка добавления",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    string zapr = "";

                    if (tid == 1)
                    {
                        zapr = $"update должность set наименование = '{textBox15.Text}', оклад = {textBox12.Text} where Ид_должности = {Convert.ToString(id)};";
                    }
                    else if (tid == 2)
                    {
                        zapr = $"update заказ set ид_сотрудника = {textBox4.Text}, ид_клиента = {textBox2.Text}, ид_услуги = {textBox7.Text}, пункт_а = '{textBox5.Text}', пункт_б = '{textBox8.Text}', дата = '{textBox3.Text}', время = '{textBox10.Text}', комментарий_заказчика = '{textBox6.Text}', выполнен = {Convert.ToString(checkBox1.Checked)} where Ид_заказа = {Convert.ToString(id)};";
                    }
                    else if (tid == 3)
                    {
                        zapr = $"update клиент set фамилия = '{textBox17.Text}', имя = '{textBox13.Text}', отчество = '{textBox16.Text}', номер_телефона = '{textBox20.Text}', пароль = '{textBox11.Text}' where Ид_клиента = {Convert.ToString(id)};";
                    }
                    else if (tid == 4)
                    {
                        zapr = $"update сотрудник set фамилия = '{textBox24.Text}', имя = '{textBox19.Text}', отчество = '{textBox26.Text}', номер_телефона = '{textBox14.Text}', ид_должности = {textBox9.Text}, пароль = '{textBox25.Text}' where Ид_сотрудника = {Convert.ToString(id)};";
                    }
                    else if (tid == 5)
                    {
                        zapr = $"update услуги set макс_вес = {textBox31.Text}, макс_объем = {textBox22.Text}, наименование = '{textBox33.Text}' where Ид_услуги = {Convert.ToString(id)};";
                    }

                    try
                    {
                        NpgsqlCommand cmd4 = new NpgsqlCommand(zapr, db_con);
                        cmd4.ExecuteNonQuery();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Convert.ToString(ex),
                            "Ошибка обновления",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                db_con.Close();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }



        private bool checkBoxs(int idd)
        {
            bool ret = false;
            if (idd == 1)
            {
                if (textBox15.Text != "" || textBox12.Text != "")
                    ret = true;
            }
            else if (idd == 2)
            {
                if (textBox4.Text != "" || textBox2.Text != "" || textBox7.Text != "" || textBox5.Text != "" || textBox8.Text != "" || textBox3.Text != "" || textBox10.Text != "")
                    ret = true;
            }
            else if (idd == 3)
            {
                if (textBox17.Text != "" || textBox13.Text != "" || textBox16.Text != "" || textBox20.Text != "" || textBox11.Text != "")
                    ret = true;
            }
            else if (idd == 4)
            {
                if (textBox24.Text != "" || textBox19.Text != "" || textBox26.Text != "" || textBox14.Text != "" || textBox9.Text != "" || textBox25.Text != "")
                    ret = true;
            }
            else if (idd == 5)
            {
                if (textBox31.Text != "" || textBox22.Text != "" || textBox33.Text != "")
                    ret = true;
            }
            return ret;
        }




        public void setData(int a, int b)
        {
            tid = a;
            id = b;

            if (id == 0)
                button2.Text = "Добавить";
            else
                button2.Text = "Обновить";

            switch (tid)
            {
                case 1:
                    panel_dolzhnost.Location = new Point(12, 12);
                    break;
                case 2:
                    panel_zakaz.Location = new Point(12, 12);
                    break;
                case 3:
                    panel_client.Location = new Point(12, 12);
                    break;
                case 4:
                    panel_sotrudnik.Location = new Point(12, 12);
                    break;
                case 5:
                    panel_uslugi.Location = new Point(12, 12);
                    break;
                default:
                    break;
            }

            if (id != 0)
            {
                db_con.Open();
                if (tid == 1)
                {
                    // должность
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select * from должность where Ид_должности = " + id, db_con);
                    NpgsqlDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox18.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ид_должности")));
                        textBox15.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("наименование")));
                        textBox12.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("оклад")));
                    }
                }
                else if (tid == 2)
                {
                    // заказ
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select * from заказ where Ид_заказа = " + id, db_con);
                    NpgsqlDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ид_заказа")));
                        textBox4.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("ид_сотрудника")));
                        textBox2.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("ид_клиента")));
                        textBox7.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("ид_услуги")));
                        textBox5.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("пункт_а")));
                        textBox8.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("пункт_б")));
                        textBox3.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("дата")));
                        textBox10.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("время")));
                        textBox6.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("комментарий_заказчика")));
                        checkBox1.Checked = reader.GetBoolean(reader.GetOrdinal("выполнен"));
                    }
                }
                else if (tid == 3)
                {
                    // клиент
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select * from клиент where Ид_клиента = " + id, db_con);
                    NpgsqlDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox21.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ид_клиента")));
                        textBox17.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("фамилия")));
                        textBox13.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("имя")));
                        textBox16.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("отчество")));
                        textBox20.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("номер_телефона")));
                        textBox11.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("пароль")));
                    }
                }
                else if (tid == 4)
                {
                    // сотрудник
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select * from сотрудник where Ид_сотрудника = " + id, db_con);
                    NpgsqlDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox27.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ид_сотрудника")));
                        textBox9.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("ид_должности")));
                        textBox24.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("фамилия")));
                        textBox19.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("имя")));
                        textBox26.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("отчество")));
                        textBox14.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("номер_телефона")));
                        textBox25.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("пароль")));
                    }
                }
                else if (tid == 5)
                {
                    // услуги
                    NpgsqlCommand cmd1 = new NpgsqlCommand("select * from услуги where Ид_услуги = " + id, db_con);
                    NpgsqlDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox34.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("Ид_услуги")));
                        textBox31.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("макс_вес")));
                        textBox22.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("макс_объем")));
                        textBox33.Text = Convert.ToString(reader.GetValue(reader.GetOrdinal("наименование")));
                    }
                }
                db_con.Close();
            }
        }





        private void panel_zakaz_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // ид
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // сотрудник
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // клиент
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            // услуга
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // пункт а
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            // пункт б
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // дата
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            // время
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            // комм заказчика
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // выполнен
        }




        private void panel_dolzhnost_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            // ид
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            // наименование
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            // оклад
        }




        private void panel_client_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            // ид
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            // фамилия
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            // имя
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            // отчество
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            // номер телефона
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            // пароль
        }




        private void panel_sotrudnik_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            // ид
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            // фамилия
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            // имя
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            // отчество
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            // ид должн
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            // номер тел
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            // пароль
        }




        private void panel_uslugi_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {
            // ид
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            // макс вес
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            // макс объем
        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {
            // наименование
        }
    }
}
