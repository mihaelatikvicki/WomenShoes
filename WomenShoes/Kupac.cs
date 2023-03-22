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
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Configuration;

namespace WomenShoes
{
    public partial class Kupac : Form
    {
        public static int height = 300;
        public static int count = 1;

        CheckedListBox chklist = new CheckedListBox();
        int id_narudzbe;
        int count_order = 0;

        iTextSharp.text.BaseColor cellBackground = new BaseColor(78, 184, 206);
        public Kupac()
        {
            InitializeComponent();
            Random rnd = new Random();
            id_narudzbe = rnd.Next(10000);
        }

        // Clear controls, populate category data and create product table
        private void Kupac_Load(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
                categoryComboBox.SelectedItem = null;

                populateCategories();

                ColumnHeader headerProducts = new ColumnHeader();
                headerProducts.Text = "Proizvodi";
                headerProducts.Width = 100;
                headerProducts.TextAlign = HorizontalAlignment.Center;
                listView1.Columns.Add(headerProducts);

                ColumnHeader headerAmount = new ColumnHeader();
                headerAmount.Text = "Kolicina";
                headerAmount.Width = 100;
                headerAmount.TextAlign = HorizontalAlignment.Center;
                listView1.Columns.Add(headerAmount);

                ColumnHeader headerPrice = new ColumnHeader();
                headerPrice.Text = "Cena";
                headerPrice.Width = 100;
                headerPrice.TextAlign = HorizontalAlignment.Center;
                listView1.Columns.Add(headerPrice);

                listView1.View = View.Details;

                label2.Text = id_narudzbe.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Retrieve all categories from the database
        public void populateCategories()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand komanda = new SqlCommand("SELECT DISTINCT Category FROM products", conn);

                conn.Open();
                SqlDataReader citac = komanda.ExecuteReader();
                while (citac.Read())
                {
                    categoryComboBox.Items.Add(citac[0].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Clears products on the left and created shopping basket
        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            height = 0;
            count = 0;
            createProductSelection();
            panel1.VerticalScroll.Value = VerticalScroll.Minimum;
        }

        // Creates product controls in the list view so users can pick their products based on the category selected
        public void createProductSelection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand cmd = new SqlCommand("SELECT * FROM products WHERE category=@category", conn);
                cmd.Parameters.AddWithValue("@category", categoryComboBox.SelectedItem.ToString());

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Create product checkbox
                    CheckBox checkBox = new CheckBox();
                    checkBox.Name = reader[2].ToString();
                    checkBox.Location = new Point(10, height);
                    checkBox.Text = reader[4].ToString();

                    chklist.Items.Add("checkBox");
                    panel1.Controls.Add(checkBox);

                    // Create product label
                    Label lb = new Label();
                    lb.Location = new Point(135, height);
                    lb.Text = reader[2].ToString();
                    lb.AutoSize = true;
                    lb.Name = reader[0].ToString();
                    lb.Font = new System.Drawing.Font("Arial", 10);
                    panel1.Controls.Add(lb);

                    height += 50;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Adds products to basket
        private void addToBasket_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();

                foreach (CheckBox box in panel1.Controls.OfType<CheckBox>())
                {
                    if (box.Checked == true)
                    {
                        ListViewItem item = new ListViewItem(box.Name);
                        item.SubItems.Add("1");
                        item.SubItems.Add(box.Text);
                        listView1.Items.Add(item);
                    }
                }

                double total = 0;

                foreach (ListViewItem item in listView1.Items)
                {
                    total += Convert.ToDouble(item.SubItems[2].Text);
                }

                double pdv = total * 0.13;
                double totaldue = pdv + total;

                textBox2.Text = total.ToString();
                textBox3.Text = pdv.ToString();
                textBox4.Text = totaldue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Handles order inset and invoice creation
        private void buyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                insertOrder();
                createInvoice();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Insert order
        public void insertOrder()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                conn.Open();
                foreach (ListViewItem ls in listView1.Items)
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO orders(firstname,lastname,address,city,ZIP,phone,email,date,product_name,price,sum,id_order) VALUES " +
                        "(@firstName,@lastName,@address,@city,@zip,@phone,@email,@date,@product_name,@price,@sum,@id_order)", conn);

                    cmd.Parameters.AddWithValue("@firstName", textBox1.Text);
                    cmd.Parameters.AddWithValue("@lastName", textBox5.Text);
                    cmd.Parameters.AddWithValue("@address", textBox6.Text);
                    cmd.Parameters.AddWithValue("@city", textBox7.Text);
                    cmd.Parameters.AddWithValue("@zip", textBox9.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox10.Text);
                    cmd.Parameters.AddWithValue("@email", textBox11.Text);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@product_name", ls.SubItems[1].Text);
                    cmd.Parameters.AddWithValue("@price", ls.SubItems[2].Text);
                    cmd.Parameters.AddWithValue("@sum", textBox4.Text);
                    cmd.Parameters.AddWithValue("@id_order", label2.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Uspesno ste narucili vase delove");

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Create invoice pdf
        public void createInvoice()
        {
            FileStream fs = new FileStream("C:\\pdf\\" + id_narudzbe.ToString() +".pdf", FileMode.Create, FileAccess.ReadWrite);
            Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();

            var titleFont = FontFactory.GetFont("Georgia", 32, BaseColor.BLACK);
            var subtitleFont = FontFactory.GetFont("Georgia", 14, BaseColor.BLACK);
            var pfont = FontFactory.GetFont("Georgia", 11, BaseColor.BLACK);

            PdfPTable table1 = new PdfPTable(4);
            //table1.DefaultCellBorder = Rectangle.NO_BORDER;
            table1.WidthPercentage = 100;
            table1.SpacingAfter = 20f;
            
            PdfPCell cell11 = new PdfPCell();
            cell11.Colspan = 3;
            cell11.AddElement(new Paragraph("Shoe Heaven d.o.o", titleFont));
            cell11.AddElement(new Paragraph("Hvala Vam što kupujete kod nas. Detalje pogledajte ispod",subtitleFont));
            cell11.VerticalAlignment = Element.ALIGN_LEFT;
            cell11.Border = 0;

            PdfPCell cell12 = new PdfPCell();
            var logo = iTextSharp.text.Image.GetInstance("C:\\pdf\\logo.jpg");
            logo.ScaleAbsoluteHeight(100);
            logo.ScaleAbsoluteWidth(100);
            logo.Alignment = Element.ALIGN_RIGHT;
            cell12.AddElement(logo);
            cell12.Border = 0;

            table1.AddCell(cell11);
            table1.AddCell(cell12);

            PdfPTable table2 = new PdfPTable(2);
            table2.WidthPercentage = 100;
            table2.SpacingAfter = 20f;

            PdfPCell cell21 = new PdfPCell();
            cell21.Colspan = 1;
            cell21.AddElement(new Paragraph("Shoe Heaven d.o.o", pfont));
            cell21.AddElement(new Paragraph("Marka Oreskovica 16", pfont));
            cell21.AddElement(new Paragraph("Subotica, Srbija", pfont));
            cell21.AddElement(new Paragraph("24000", pfont));
            cell21.AddElement(new Paragraph("Telefon: 024/123-456", pfont));
            cell21.AddElement(new Paragraph("Racun: #" + id_narudzbe.ToString(), pfont));
            cell21.AddElement(new Paragraph("Datum i vreme:" + DateTime.Now, pfont));
            cell21.Border = 0;

            PdfPCell cell22 = new PdfPCell();
            cell22.Colspan = 1;
            cell22.AddElement(new Paragraph(textBox1.Text + " " + textBox5.Text, pfont));
            cell22.AddElement(new Paragraph(textBox6.Text, pfont));
            cell22.AddElement(new Paragraph(textBox7.Text, pfont));
            cell22.AddElement(new Paragraph(textBox9.Text, pfont));
            cell22.AddElement(new Paragraph("Telefon: "+ textBox10.Text, pfont));
            cell22.AddElement(new Paragraph("Email: " + textBox11.Text, pfont));
            cell22.Border = 0;

            table2.AddCell(cell21);
            table2.AddCell(cell22);

            PdfPTable table3 = new PdfPTable(11);
            table3.WidthPercentage = 100;

            PdfPCell cell31 = new PdfPCell();
            cell31.Colspan = 1;
            Paragraph p31 = new Paragraph("#",pfont);
            p31.Alignment = Element.ALIGN_CENTER;
            p31.SpacingAfter = 10f;
            cell31.AddElement(p31);

            PdfPCell cell32 = new PdfPCell();
            cell32.Colspan = 5;
            Paragraph p32 = new Paragraph("Nazivi proizvoda", pfont);
            p32.Alignment = Element.ALIGN_CENTER;
            p32.SpacingAfter = 10f;
            cell32.AddElement(p32);

            PdfPCell cell33 = new PdfPCell();
            cell33.Colspan = 2;
            Paragraph p33 = new Paragraph("Kolicina", pfont);
            p33.Alignment = Element.ALIGN_CENTER;
            p33.SpacingAfter = 10f;
            cell33.AddElement(p33);

            PdfPCell cell34 = new PdfPCell();
            cell34.Colspan = 3;
            Paragraph p34 = new Paragraph("Kolicina", pfont);
            p34.Alignment = Element.ALIGN_CENTER;
            p34.SpacingAfter = 10f;
            cell34.AddElement(p34);

            cell31.BackgroundColor = cellBackground;
            cell32.BackgroundColor = cellBackground;
            cell33.BackgroundColor = cellBackground;
            cell34.BackgroundColor = cellBackground;

            table3.AddCell(cell31);
            table3.AddCell(cell32);
            table3.AddCell(cell33);
            table3.AddCell(cell34);

            count_order = listView1.Items.Count - 1;

            for(int i = 0; i <= count_order; i++)
            {
                PdfPCell cell41 = new PdfPCell();
                PdfPCell cell42 = new PdfPCell();
                PdfPCell cell43 = new PdfPCell();
                PdfPCell cell44 = new PdfPCell();

                cell41.Colspan = 1;
                cell42.Colspan = 5;
                cell43.Colspan = 2;
                cell44.Colspan = 3;
                
                ListViewItem item = listView1.Items[i];
                Paragraph p41 = new Paragraph((i + 1).ToString(),pfont);
                p41.Alignment = Element.ALIGN_CENTER;
                p41.SpacingAfter = 10f;
                cell41.AddElement(p41);

                Paragraph p42 = new Paragraph(item.SubItems[0].Text, pfont);
                p42.Alignment = Element.ALIGN_CENTER;
                p42.SpacingAfter = 10f;
                cell42.AddElement(p42);

                Paragraph p43 = new Paragraph(item.SubItems[1].Text, pfont);
                p43.Alignment = Element.ALIGN_CENTER;
                p43.SpacingAfter = 10f;
                cell43.AddElement(p43);

                Paragraph p44 = new Paragraph(item.SubItems[2].Text, pfont);
                p44.Alignment = Element.ALIGN_CENTER;
                p44.SpacingAfter = 10f;
                cell44.AddElement(p44);

                table3.AddCell(cell41);
                table3.AddCell(cell42);
                table3.AddCell(cell43);
                table3.AddCell(cell44);
            }

            PdfPCell cell51 = new PdfPCell();
            cell51.Colspan = 8;
            Paragraph p51 = new Paragraph("Total: ", pfont);
            p51.Alignment = Element.ALIGN_RIGHT;
            p51.SpacingAfter = 10f;
            cell51.AddElement(p51);

            PdfPCell cell52 = new PdfPCell();
            cell52.Colspan = 3;
            Paragraph p52 = new Paragraph(textBox4.Text, pfont);
            p52.Alignment = Element.ALIGN_CENTER;
            p52.SpacingAfter = 10f;
            cell52.AddElement(p52);

            cell51.BackgroundColor = cellBackground;
            cell52.BackgroundColor = cellBackground;

            table3.AddCell(cell51);
            table3.AddCell(cell52);

            doc.Add(table1);
            doc.Add(table2);
            doc.Add(table3);

            doc.Close();

            string filename = "C:\\pdf\\" + id_narudzbe.ToString() + ".pdf";
            System.Diagnostics.Process.Start(filename);
        }
    }
}
