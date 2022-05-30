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
            int i1, i2=0 ;
            try
            {

                for (i1 = 0; i1 < 5; i1++)
            {
                    if (((CheckBox)groupBox1.Controls[i1]).Checked == false)
                    {
                    }
                    else
                    {
                        if (i2==0)
                        {
                            this.Hide();
                            Form3 f3 = new Form3();
                            i2 = i1;
                        }
                        else
                        {
                            Form3 f3 = new Form3();
                            f3.setImage1(i1);
                            f3.setImage2(i2);
                            f3.ShowDialog();

                            return;

                        }
                        
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
    }
}
