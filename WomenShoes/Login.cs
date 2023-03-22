using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WomenShoes
{
    public partial class Login : Form
    {
        public static string firstname = "";
        public static string lastname = "";

        public Login()
        {
            InitializeComponent();
        }

        // Handle login button click
        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }

        // Handle login logic / DB connection
        public void login()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand cmd = new SqlCommand("SELECT * FROM administrator WHERE username=@username AND password=@password", conn);
                cmd.Parameters.AddWithValue("@username", textBox1.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    DataRow row = dt.Rows[0];
                    firstname = row["first_name"].ToString();
                    lastname = row["last_name"].ToString();
                    Administrator administrator = new Administrator();
                    administrator.ShowDialog();
                    this.Close();
                }
                else
                {
                    label2.Text = "Pogrešna lozinka i/ili korisničko ime!";
                    textBox1.Clear();
                    textBox2.Clear();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handle back button click
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
