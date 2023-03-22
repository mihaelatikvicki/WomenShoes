using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WomenShoes
{
    public partial class Pocetak : Form
    {
        public Pocetak()
        {
            InitializeComponent();
        }

        private void AdministratorBtn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void KupacBtn_Click(object sender, EventArgs e)
        {
            Kupac kupac = new Kupac();
            kupac.ShowDialog();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
