using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Data.SqlClient;
using System.Configuration;

namespace WomenShoes
{
    public partial class Izvestaji : Form
    {
        public Izvestaji()
        {
            InitializeComponent();
        }

        private void Izvestaji_Load(object sender, EventArgs e)
        {
            PdfDocument pdf = new PdfDocument();
            pdf.AddTitle("WomenShoes");
        }

        // Generate administrator pdf
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream("C:\\pdf\\administrator.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);

                Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                var titleFont = FontFactory.GetFont("Georgia", 32, BaseColor.BLACK);
                var subtitleFont = FontFactory.GetFont("Georgia", 14, BaseColor.BLACK);
                var pfont = FontFactory.GetFont("Georgia", 11, BaseColor.BLACK);

                PdfPTable table1 = new PdfPTable(4);
                table1.WidthPercentage = 100;
                table1.SpacingAfter = 20f;

                PdfPCell cell11 = new PdfPCell();
                cell11.Colspan = 3;
                cell11.AddElement(new Paragraph("WomenShoes d.o.o", titleFont));
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

                PdfPTable table2 = new PdfPTable(1);
                table2.WidthPercentage = 100;
                table2.SpacingAfter = 20f;

                PdfPCell cell21 = new PdfPCell();
                cell21.Colspan = 1;
                cell21.AddElement(new Paragraph("WomenShoes d.o.o", pfont));
                cell21.AddElement(new Paragraph("Marka Oreskovica 16", pfont));
                cell21.AddElement(new Paragraph("Subotica, Srbija", pfont));
                cell21.AddElement(new Paragraph("24000", pfont));
                cell21.AddElement(new Paragraph("Telefon: 024/123-456", pfont));
                cell21.Border = 0;

                table2.AddCell(cell21);

                PdfPTable table3 = new PdfPTable(10);
                table3.WidthPercentage = 100;

                PdfPCell cell31 = new PdfPCell();
                cell31.Colspan = 1;
                Paragraph p31 = new Paragraph("#", pfont);
                p31.Alignment = Element.ALIGN_CENTER;
                p31.SpacingAfter = 10f;
                cell31.AddElement(p31);

                PdfPCell cell32 = new PdfPCell();
                cell32.Colspan = 2;
                Paragraph p32 = new Paragraph("Firstname", pfont);
                p32.Alignment = Element.ALIGN_CENTER;
                p32.SpacingAfter = 10f;
                cell32.AddElement(p32);

                PdfPCell cell33 = new PdfPCell();
                cell33.Colspan = 2;
                Paragraph p33 = new Paragraph("Lastname", pfont);
                p33.Alignment = Element.ALIGN_CENTER;
                p33.SpacingAfter = 10f;
                cell33.AddElement(p33);

                PdfPCell cell34 = new PdfPCell();
                cell34.Colspan = 2;
                Paragraph p34 = new Paragraph("Username", pfont);
                p34.Alignment = Element.ALIGN_CENTER;
                p34.SpacingAfter = 10f;
                cell34.AddElement(p34);

                PdfPCell cell35 = new PdfPCell();
                cell35.Colspan = 3;
                Paragraph p35 = new Paragraph("Image", pfont);
                p35.Alignment = Element.ALIGN_CENTER;
                p35.SpacingAfter = 10f;
                cell35.AddElement(p35);

                table3.AddCell(cell31);
                table3.AddCell(cell32);
                table3.AddCell(cell33);
                table3.AddCell(cell34);
                table3.AddCell(cell35);

                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand cmd = new SqlCommand("SELECT * FROM administrator", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PdfPCell cell41 = new PdfPCell();
                    PdfPCell cell42 = new PdfPCell();
                    PdfPCell cell43 = new PdfPCell();
                    PdfPCell cell44 = new PdfPCell();
                    PdfPCell cell45 = new PdfPCell();

                    cell41.Colspan = 1;
                    cell42.Colspan = 2;
                    cell43.Colspan = 2;
                    cell44.Colspan = 2;
                    cell45.Colspan = 3;

                    Paragraph p41 = new Paragraph(reader[0].ToString());
                    p41.Alignment = Element.ALIGN_CENTER;
                    p41.SpacingAfter = 10f;
                    cell41.AddElement(p41);

                    Paragraph p42 = new Paragraph(reader[1].ToString());
                    p42.Alignment = Element.ALIGN_CENTER;
                    p42.SpacingAfter = 10f;
                    cell42.AddElement(p42);

                    Paragraph p43 = new Paragraph(reader[2].ToString());
                    p43.Alignment = Element.ALIGN_CENTER;
                    p43.SpacingAfter = 10f;
                    cell43.AddElement(p43);

                    Paragraph p44 = new Paragraph(reader[3].ToString());
                    p44.Alignment = Element.ALIGN_CENTER;
                    p44.SpacingAfter = 10f;
                    cell44.AddElement(p44);

                    Paragraph p45 = new Paragraph(reader[4].ToString());
                    p45.Alignment = Element.ALIGN_CENTER;
                    p45.SpacingAfter = 10f;
                    cell45.AddElement(p45);

                    table3.AddCell(cell41);
                    table3.AddCell(cell42);
                    table3.AddCell(cell43);
                    table3.AddCell(cell44);
                    table3.AddCell(cell45);
                }
                conn.Close();

                doc.Add(table1);
                doc.Add(table2);
                doc.Add(table3);

                doc.Close();

                string filename = "C:\\pdf\\administrator.pdf";
                System.Diagnostics.Process.Start(filename);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Generate product pdf
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream("C:\\pdf\\products.pdf", FileMode.Create, FileAccess.ReadWrite);
                Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                var titleFont = FontFactory.GetFont("Georgia", 32, BaseColor.BLACK);
                var subtitleFont = FontFactory.GetFont("Georgia", 12, BaseColor.BLACK);
                var pfont = FontFactory.GetFont("Georgia", 7, BaseColor.BLACK);

                PdfPTable table1 = new PdfPTable(4);
                table1.WidthPercentage = 100;
                table1.SpacingAfter = 20f;

                PdfPCell cell11 = new PdfPCell();
                cell11.Colspan = 3;
                cell11.AddElement(new Paragraph("WomenShoes d.o.o", titleFont));
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

                PdfPTable table2 = new PdfPTable(1);
                table2.WidthPercentage = 100;
                table2.SpacingAfter = 20f;

                PdfPCell cell21 = new PdfPCell();
                cell21.Colspan = 1;
                cell21.AddElement(new Paragraph("WomenShoes d.o.o", subtitleFont));
                cell21.AddElement(new Paragraph("Marka Oreskovica 16", subtitleFont));
                cell21.AddElement(new Paragraph("Subotica, Srbija", subtitleFont));
                cell21.AddElement(new Paragraph("24000", subtitleFont));
                cell21.AddElement(new Paragraph("Telefon: 024/123-456", subtitleFont));
                cell21.Border = 0;

                table2.AddCell(cell21);

                PdfPTable table3 = new PdfPTable(20);
                table3.WidthPercentage = 100;

                PdfPCell cell31 = new PdfPCell();
                cell31.Colspan = 1;
                Paragraph p31 = new Paragraph("#", pfont);
                p31.Alignment = Element.ALIGN_CENTER;
                p31.SpacingAfter = 10f;
                cell31.AddElement(p31);

                PdfPCell cell32 = new PdfPCell();
                cell32.Colspan = 3;
                Paragraph p32 = new Paragraph("Category", pfont);
                p32.Alignment = Element.ALIGN_CENTER;
                p32.SpacingAfter = 10f;
                cell32.AddElement(p32);

                PdfPCell cell33 = new PdfPCell();
                cell33.Colspan = 9;
                Paragraph p33 = new Paragraph("Name", pfont);
                p33.Alignment = Element.ALIGN_CENTER;
                p33.SpacingAfter = 10f;
                cell33.AddElement(p33);

                PdfPCell cell34 = new PdfPCell();
                cell34.Colspan = 4;
                Paragraph p34 = new Paragraph("Type", pfont);
                p34.Alignment = Element.ALIGN_CENTER;
                p34.SpacingAfter = 10f;
                cell34.AddElement(p34);

                PdfPCell cell35 = new PdfPCell();
                cell35.Colspan = 3;
                Paragraph p35 = new Paragraph("Price", pfont);
                p35.Alignment = Element.ALIGN_CENTER;
                p35.SpacingAfter = 10f;
                cell35.AddElement(p35);

                table3.AddCell(cell31);
                table3.AddCell(cell32);
                table3.AddCell(cell33);
                table3.AddCell(cell34);
                table3.AddCell(cell35);

                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand komanda = new SqlCommand("SELECT * FROM products", conn);
                conn.Open();
                SqlDataReader citac = komanda.ExecuteReader();
                while (citac.Read())
                {
                    PdfPCell cell41 = new PdfPCell();
                    PdfPCell cell42 = new PdfPCell();
                    PdfPCell cell43 = new PdfPCell();
                    PdfPCell cell44 = new PdfPCell();
                    PdfPCell cell45 = new PdfPCell();

                    cell41.Colspan = 1;
                    cell42.Colspan = 3;
                    cell43.Colspan = 9;
                    cell44.Colspan = 4;
                    cell45.Colspan = 3;

                    Paragraph p41 = new Paragraph(citac[0].ToString(), pfont);
                    p41.Alignment = Element.ALIGN_CENTER;
                    p41.SpacingAfter = 10f;
                    cell41.AddElement(p41);

                    Paragraph p42 = new Paragraph(citac[1].ToString(), pfont);
                    p42.Alignment = Element.ALIGN_CENTER;
                    p42.SpacingAfter = 10f;
                    cell42.AddElement(p42);

                    Paragraph p43 = new Paragraph(citac[2].ToString(), pfont);
                    p43.Alignment = Element.ALIGN_CENTER;
                    p43.SpacingAfter = 10f;
                    cell43.AddElement(p43);

                    Paragraph p44 = new Paragraph(citac[3].ToString(), pfont);
                    p44.Alignment = Element.ALIGN_CENTER;
                    p44.SpacingAfter = 10f;
                    cell44.AddElement(p44);

                    Paragraph p45 = new Paragraph(citac[4].ToString(), pfont);
                    p45.Alignment = Element.ALIGN_CENTER;
                    p45.SpacingAfter = 10f;
                    cell45.AddElement(p45);

                    table3.AddCell(cell41);
                    table3.AddCell(cell42);
                    table3.AddCell(cell43);
                    table3.AddCell(cell44);
                    table3.AddCell(cell45);
                }
                conn.Close();

                doc.Add(table1);
                doc.Add(table2);
                doc.Add(table3);

                doc.Close();

                string filename = "C:\\pdf\\products.pdf";
                System.Diagnostics.Process.Start(filename);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Generate order pdf
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream("C:\\pdf\\orders.pdf", FileMode.Create, FileAccess.ReadWrite);
                Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                var titleFont = FontFactory.GetFont("Georgia", 32, BaseColor.BLACK);
                var subtitleFont = FontFactory.GetFont("Georgia", 12, BaseColor.BLACK);
                var pfont = FontFactory.GetFont("Georgia", 7, BaseColor.BLACK);

                PdfPTable table1 = new PdfPTable(4);
                table1.WidthPercentage = 100;
                table1.SpacingAfter = 20f;

                PdfPCell cell11 = new PdfPCell();
                cell11.Colspan = 3;
                cell11.AddElement(new Paragraph("WomenShoes d.o.o", titleFont));
                cell11.VerticalAlignment = Element.ALIGN_LEFT;
                cell11.Border = 0;

                PdfPCell cell12 = new PdfPCell();
                var logo = iTextSharp.text.Image.GetInstance("C:\\pdf\\logo.jpg");
                logo.ScaleAbsoluteHeight(70);
                logo.ScaleAbsoluteWidth(70);
                logo.Alignment = Element.ALIGN_RIGHT;
                cell12.AddElement(logo);
                cell12.Border = 0;

                table1.AddCell(cell11);
                table1.AddCell(cell12);

                PdfPTable table2 = new PdfPTable(1);
                table2.WidthPercentage = 100;
                table2.SpacingAfter = 20f;

                PdfPCell cell21 = new PdfPCell();
                cell21.Colspan = 1;
                cell21.AddElement(new Paragraph("WomenShoes d.o.o", subtitleFont));
                cell21.AddElement(new Paragraph("Marka Oreskovica 16", subtitleFont));
                cell21.AddElement(new Paragraph("Subotica, Srbija", subtitleFont));
                cell21.AddElement(new Paragraph("24000", subtitleFont));
                cell21.AddElement(new Paragraph("Telefon: 024/123-456", subtitleFont));
                cell21.Border = 0;

                table2.AddCell(cell21);

                PdfPTable table3 = new PdfPTable(20);
                table3.WidthPercentage = 100;

                PdfPCell cell31 = new PdfPCell();
                cell31.Colspan = 1;
                Paragraph p31 = new Paragraph("#", pfont);
                p31.Alignment = Element.ALIGN_CENTER;
                p31.SpacingAfter = 10f;
                cell31.AddElement(p31);

                PdfPCell cell32 = new PdfPCell();
                cell32.Colspan = 3;
                Paragraph p32 = new Paragraph("Category", pfont);
                p32.Alignment = Element.ALIGN_CENTER;
                p32.SpacingAfter = 10f;
                cell32.AddElement(p32);

                PdfPCell cell33 = new PdfPCell();
                cell33.Colspan = 9;
                Paragraph p33 = new Paragraph("Name", pfont);
                p33.Alignment = Element.ALIGN_CENTER;
                p33.SpacingAfter = 10f;
                cell33.AddElement(p33);

                PdfPCell cell34 = new PdfPCell();
                cell34.Colspan = 4;
                Paragraph p34 = new Paragraph("Type", pfont);
                p34.Alignment = Element.ALIGN_CENTER;
                p34.SpacingAfter = 10f;
                cell34.AddElement(p34);

                PdfPCell cell35 = new PdfPCell();
                cell35.Colspan = 3;
                Paragraph p35 = new Paragraph("Price", pfont);
                p35.Alignment = Element.ALIGN_CENTER;
                p35.SpacingAfter = 10f;
                cell35.AddElement(p35);

                table3.AddCell(cell31);
                table3.AddCell(cell32);
                table3.AddCell(cell33);
                table3.AddCell(cell34);
                table3.AddCell(cell35);

                SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"]);

                SqlCommand cmd = new SqlCommand("SELECT * FROM orders", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PdfPCell cell41 = new PdfPCell();
                    PdfPCell cell42 = new PdfPCell();
                    PdfPCell cell43 = new PdfPCell();
                    PdfPCell cell44 = new PdfPCell();
                    PdfPCell cell45 = new PdfPCell();

                    cell41.Colspan = 1;
                    cell42.Colspan = 3;
                    cell43.Colspan = 9;
                    cell44.Colspan = 4;
                    cell45.Colspan = 3;

                    Paragraph p41 = new Paragraph(reader[0].ToString(), pfont);
                    p41.Alignment = Element.ALIGN_CENTER;
                    p41.SpacingAfter = 10f;
                    cell41.AddElement(p41);

                    Paragraph p42 = new Paragraph(reader[1].ToString(), pfont);
                    p42.Alignment = Element.ALIGN_CENTER;
                    p42.SpacingAfter = 10f;
                    cell42.AddElement(p42);

                    Paragraph p43 = new Paragraph(reader[2].ToString(), pfont);
                    p43.Alignment = Element.ALIGN_CENTER;
                    p43.SpacingAfter = 10f;
                    cell43.AddElement(p43);

                    Paragraph p44 = new Paragraph(reader[3].ToString(), pfont);
                    p44.Alignment = Element.ALIGN_CENTER;
                    p44.SpacingAfter = 10f;
                    cell44.AddElement(p44);

                    Paragraph p45 = new Paragraph(reader[4].ToString(), pfont);
                    p45.Alignment = Element.ALIGN_CENTER;
                    p45.SpacingAfter = 10f;
                    cell45.AddElement(p45);

                    table3.AddCell(cell41);
                    table3.AddCell(cell42);
                    table3.AddCell(cell43);
                    table3.AddCell(cell44);
                    table3.AddCell(cell45);
                }
                conn.Close();

                doc.Add(table1);
                doc.Add(table2);
                doc.Add(table3);

                doc.Close();

                string filename = "C:\\pdf\\orders.pdf";
                System.Diagnostics.Process.Start(filename);

            }
            catch (Exception ex)
            {
               MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
