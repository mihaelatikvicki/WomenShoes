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
    public partial class Proizvodi : Form
    {
        public static int height = 50;
        public static int count = 1;
        string productType;

        public Proizvodi(string productType)
        {
            InitializeComponent();
            this.productType = productType;
        }

        // Initial content updates / product creation
        private void Proizvodi_Load(object sender, EventArgs e)
        {
            height = 50;
            ProductType.Text = productType;
            ProductType.ForeColor = Color.FromArgb(00, 00, 00);
            displayProducts();
        }

        // Handles product display
        public void displayProducts()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand cmd = new SqlCommand("SELECT * FROM products WHERE category=@category", conn);
                cmd.Parameters.AddWithValue("@category", productType);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader[0].ToString());
                    GroupBox gb = new GroupBox();
                    gb.Name = "GroupBox" + reader[0];
                    gb.Location = new Point(30, height);
                    gb.Size = new Size(750, 150);
                    gb.BackColor = Color.FromArgb(244, 172, 65);
                    height += 150;
                    this.Controls.Add(gb);

                    // DELETE ITEM
                    PictureBox btnDelete = new PictureBox();
                    btnDelete.Location = new Point(20, 70);
                    btnDelete.Load("../../images/xxx.png"); 
                    btnDelete.SizeMode = PictureBoxSizeMode.StretchImage;
                    btnDelete.Size = new Size(30, 30);
                    btnDelete.Name = reader[0].ToString();
                    btnDelete.Click += (sender, args) =>
                    {
                        DialogResult dr = MessageBox.Show("Da li ste sigurni da želite da obrišete?", "Brisanje proizvoda", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                        if (dr == DialogResult.Yes)
                        {
                            deleteProduct(id);
                        }

                    };
                    gb.Controls.Add(btnDelete);

                    // UPDATE ITEM
                    PictureBox btnUpdate = new PictureBox();
                    btnUpdate.Location = new Point(50, 70);
                    btnUpdate.Load("../../images/update.png"); // Promeniti sliku
                    btnUpdate.SizeMode = PictureBoxSizeMode.StretchImage;
                    btnUpdate.Size = new Size(30, 30);
                    btnUpdate.Name = reader[0].ToString();
                    btnUpdate.Click += (sender, args) =>
                    {
                        AdministratorAzuriranje administratorAzuriranje = new AdministratorAzuriranje(id);
                        DialogResult updateForm = administratorAzuriranje.ShowDialog();
                    };
                    gb.Controls.Add(btnUpdate);

                    // ITEM IMAGE
                    PictureBox img = new PictureBox();
                    img.Load("../../images/" + reader[6].ToString());
                    img.Location = new Point(100, 25);
                    img.Size = new Size(100, 110);
                    img.SizeMode = PictureBoxSizeMode.StretchImage;
                    gb.Controls.Add(img);


                    // LABEL ZA NAZIV PROIZVODA
                    Label lb = new Label();
                    lb.Location = new Point(230, 40);
                    lb.Text = "Naziv: " + reader[2].ToString();
                    lb.AutoSize = true;
                    lb.Name = reader[0].ToString();
                    lb.Font = new Font("Georgia", 10);
                    lb.ForeColor = Color.FromArgb(00, 00, 00);
                    gb.Controls.Add(lb);


                    // LABEL ZA KATEGORIJU PROIZVODA
                    Label lb1 = new Label();
                    lb1.Location = new Point(230, 75);
                    lb1.Text = "Kategorija: " + reader[3].ToString();
                    lb1.AutoSize = true;
                    lb1.Name = reader[0].ToString();
                    lb1.Font = new Font("Georgia", 10);
                    lb1.ForeColor = Color.FromArgb(00, 00, 00);
                    gb.Controls.Add(lb1);


                    // LABEL ZA PROIZVODJACA PROIZVODA
                    Label lb2 = new Label();
                    lb2.Location = new Point(230, 105);
                    lb2.Text = "Proizvodjac: " + reader[5].ToString();
                    lb2.AutoSize = true;
                    lb2.Name = reader[0].ToString();
                    lb2.Font = new Font("Georgia", 10);
                    lb2.ForeColor = Color.FromArgb(00, 00, 00);
                    gb.Controls.Add(lb2);


                    // LABEL ZA CENU PROIZVODA
                    Label lb3 = new Label();
                    lb3.Location = new Point(550, 105);
                    lb3.Text = "Cena: " + reader[4].ToString() + "RSD";
                    lb3.AutoSize = true;
                    lb3.Name = reader[0].ToString();
                    lb3.Font = new Font("Georgia", 16);
                    lb3.ForeColor = Color.FromArgb(00, 00, 00);
                    gb.Controls.Add(lb3);

                    count++;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handle product deletion
        public void deleteProduct(int id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand cmd = new SqlCommand("DELETE FROM products WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Uspesno obrisan proizvod");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
