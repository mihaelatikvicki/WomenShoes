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
    public partial class Magacin : Form
    {
        public Magacin()
        {
            InitializeComponent();
        }

        private void Stikle_Click(object sender, EventArgs e)
        {
            showProductPage("Stikle");
        }

        private void Sandale_Click(object sender, EventArgs e)
        {
            showProductPage("Sandale");
        }

        private void Papuce_Click(object sender, EventArgs e)
        {
            showProductPage("Papuce");
        }

        private void Espadrile_Click(object sender, EventArgs e)
        {
            showProductPage("Espadrile");
        }

        private void Patike_Click(object sender, EventArgs e)
        {
            showProductPage("Patike");
        }

        private void Cizme_Click(object sender, EventArgs e)
        {
            showProductPage("Cizme");
        }

        private void Baletanke_Click(object sender, EventArgs e)
        {
            showProductPage("Baletanke");
        }

        private void LetnjeCizme_Click(object sender, EventArgs e)
        {
            showProductPage("Letnje cizme");
        }

        // Handles product page showing logic
        private void showProductPage (string product)
        {
            Proizvodi proizvodi = new Proizvodi(product);
            proizvodi.ShowDialog();
        }
    }
}
