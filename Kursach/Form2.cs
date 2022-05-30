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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            int i;
            try
            {

                for (i = 0; i < 5; i++)
                {
                    if (((RadioButton)groupBox2.Controls[i]).Checked == false)
                    {
                    }
                    else
                    {
                        this.Hide();
                        Form3 f3 = new Form3();
                        f3.setImage(i);
                        f3.ShowDialog();

                        return;
                    }
                }
                throw new Exception();
            }

            catch (Exception)
            {
                MessageBox.Show("Выберите интеграл", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
