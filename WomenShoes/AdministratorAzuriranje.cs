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
    public partial class AdministratorAzuriranje : Form
    {
        int productId;
        public AdministratorAzuriranje(int productId)
        {
            InitializeComponent();
            this.productId = productId;
        }

        // Initial data population
        private void AdministratorAzuriranje_Load(object sender, EventArgs e)
        {
            populateCategories();
            getProductData();
        }

        // Retrieve categories from the database
        public void populateCategories()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand cmd = new SqlCommand("SELECT DISTINCT category FROM products", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[0].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Retrieves product data from the DB and displays it
        public void getProductData()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand cmd = new SqlCommand("SELECT * FROM products WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", productId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader[0].ToString();
                    textBox2.Text = reader[2].ToString();
                    textBox3.Text = reader[3].ToString(); 
                    textBox4.Text = reader[4].ToString();
                    textBox5.Text = reader[5].ToString();
                    textBox6.Text = reader[6].ToString();
                    comboBox1.SelectedItem = reader[1].ToString();
                    pictureBox1.Load("../../images/products/" + reader[6].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update button click handling
        private void button1_Click(object sender, EventArgs e)
        {
            updateValues();
            this.Close();
        }

        // Update product data in the DB
        public void updateValues()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand cmd = new SqlCommand("UPDATE products SET category=@p2, name=@p3, type=@p4, price=@p5, manufacturer=@p6, image=@p7 WHERE id=@p1", conn);
                cmd.Parameters.AddWithValue("@p1", textBox1.Text);
                cmd.Parameters.AddWithValue("@p2", comboBox1.Text);
                cmd.Parameters.AddWithValue("@p3", textBox2.Text);
                cmd.Parameters.AddWithValue("@p4", textBox3.Text);
                cmd.Parameters.AddWithValue("@p5", textBox4.Text);
                cmd.Parameters.AddWithValue("@p6", textBox5.Text);
                cmd.Parameters.AddWithValue("@p7", textBox6.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Uspesno ste azurirali podatke");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handles back button click
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
