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
    public partial class Administrator : Form
    {
        public Administrator()
        {
            InitializeComponent();
        }

        // Set administrator name
        private void Administrator_Load(object sender, EventArgs e)
        {
            string firstname = Login.firstname;
            string lastname = Login.lastname;
            AdministratorTitle.Text = firstname.ToString() + " " + lastname.ToString();
        }

        // Handle back button click
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Magacin_Click(object sender, EventArgs e)
        {
            Magacin magacin = new Magacin();
            magacin.TopLevel = false;
            AdministratorMain.Controls.Add(magacin);
            magacin.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            magacin.Dock = DockStyle.Fill;
            magacin.Show();
            AdministratorTitle.Text = "Magacin";
        }

        private void Izvestaji_Click(object sender, EventArgs e)
        {
            Izvestaji izvestaji = new Izvestaji();
            izvestaji.ShowDialog();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.ShowDialog();
        }

        private void AdministratorCloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
