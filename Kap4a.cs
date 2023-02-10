using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTrucking
{
    public partial class Kap4a : Form
    {
        string text;
        int tryCounter = 0;

        public Kap4a()
        {
            InitializeComponent();
            kap4a_gen();
        }
        private void kap4a_gen()
        {
            Random rnd = new Random();
            Bitmap result = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            int Xpos = rnd.Next(0, pictureBox1.Width - 50);
            int Ypos = rnd.Next(0, pictureBox1.Height - 25);

            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

            Graphics g = Graphics.FromImage((Image)result);

            g.Clear(Color.Gray);

            text = "";
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            g.DrawString(text,
                         new Font("Arial", 15),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));

            g.DrawLine(Pens.Black,
                       new Point(0, 0),
                       new Point(pictureBox1.Width - 1, pictureBox1.Height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, pictureBox1.Height - 1),
                       new Point(pictureBox1.Width - 1, 0));

            for (int i = 0; i < pictureBox1.Width; ++i)
                for (int j = 0; j < pictureBox1.Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            pictureBox1.Image = result;
        }

        private void pictureBox1_Click(object sender, EventArgs e) 
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // upd
        {
            kap4a_gen();
        }

        private void button1_Click(object sender, EventArgs e) // check
        {
            trySignIn();
        }

        private void Kap4a_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void trySignIn()
        {
            tryCounter += 1;
            string t = textBox1.Text;
            if (text.ToLower() == t.ToLower())
            {
                this.Hide();
            }
            else if (tryCounter == 3)
            {

                MessageBox.Show(
                    "Слишком много попыток!",
                    "Проверка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();

            }
            else
            {
                MessageBox.Show(
                    "Неправильный ввод, попробуйте снова!",
                    "Проверка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                kap4a_gen();
                textBox1.Text = "";
            }
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                trySignIn();
            }
        }
    }
}
