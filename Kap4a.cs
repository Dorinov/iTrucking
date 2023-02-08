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
        int num;
        int tryCounter = 0;

        public Kap4a()
        {
            InitializeComponent();
            kap4a_gen();
        }

        private void kap4a_gen()
        {
            Random random = new Random();
            num = random.Next(1, 10000000);

            var image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            var grapfic = Graphics.FromImage(image);
            grapfic.DrawString(num.ToString(), Font, Brushes.Green, new Point(0, 0));
            pictureBox1.Image = image;
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
            tryCounter += 1;
            if (num.ToString() == textBox1.Text)
            {
                Form1 form1 = new Form1();
                form1.Show();
                Hide();

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

        private void Kap4a_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
