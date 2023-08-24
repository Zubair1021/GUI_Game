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
    public partial class MainForm2 : Form
    {
        public MainForm2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm page = new MainForm();
            page.Show();
            Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            start1 page = new start1();
            page.Show();
            Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Help page = new Help();
            page.Show();
            Visible = false;
        }
    }
}
