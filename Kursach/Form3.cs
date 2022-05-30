using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Application.EnableVisualStyles();

            Form2 f2 = new Form2();
            f2.Owner = this.Owner as Form2;
        }

        int ii = 0;

        public void setImage(int i)//помещаем в label2 интеграл, выбранный пользователем
        {
            if (i == 1)
            { this.textBox2.ReadOnly = false; } //если выбран интеграл вида x^n
            else
            {
                this.textBox2.Hide();
                this.label3.Hide();
            }
            if (i == 4)
            { this.textBox5.ReadOnly = false; } //если выбран интеграл вида a^n
            else
            {
                this.textBox5.Hide();
                this.label6.Hide();
            }

            label2.ImageIndex = i;
            ii = i;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1";
            textBox2.Text = "2";
            textBox5.Text = "2";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a, b, k;
            int n;
            try
            {
                if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Выберите метод рассчета значения интеграла", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                a = Convert.ToDouble(textBox3.Text);
                b = Convert.ToDouble(textBox4.Text);
                k = Convert.ToDouble(textBox1.Text);
                n = Convert.ToInt32(textBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (a >= b)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Нижний предел не должен превышать верхний", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Hide();

            Form1 f1 = new Form1();
            f1.Func(a, b, ii, n, k);

            if (textBox5.ReadOnly == false)
            {
                n = Convert.ToInt32(textBox5.Text);
            }

            if (radioButton1.Checked == true)
            {
                f1.DrawRect(a, b, ii, n, k);
                f1.RectangleMethod(a, b, ii, n, k);
            }

            if (radioButton2.Checked == true)
            {
                f1.DrawTrap(a, b, ii, n, k);
                f1.TrapMethod(a, b, ii, n, k);
            }

            if (radioButton3.Checked == true)
            {
                f1.DrawSimp(a, b, ii, n, k);
                f1.SimpMethod(a, b, ii, n, k);
            }
            f1.ShowDialog();
        }
    }
}
