using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZombieGUIGame
{
    public partial class start1 : Form
    {
        public start1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm2 page = new MainForm2();
            page.Show();
            Visible = false;
        }

        private void EasyMode_Click(object sender, EventArgs e)
        {
            Form1 page = new Form1();
            page.Show();
            Visible = false;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form2 page = new Form2();
            page.Show();
            Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Form3 page = new Form3();
            page.Show();
            Visible = false;
        }
    }
}
