namespace WomenShoes
{
    partial class Pocetak
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pocetak));
            this.AdministratorBtn = new System.Windows.Forms.Button();
            this.KupacBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // AdministratorBtn
            // 
            this.AdministratorBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(172)))), ((int)(((byte)(65)))));
            this.AdministratorBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AdministratorBtn.BackgroundImage")));
            this.AdministratorBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AdministratorBtn.FlatAppearance.BorderSize = 0;
            this.AdministratorBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AdministratorBtn.Font = new System.Drawing.Font("Georgia", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdministratorBtn.ForeColor = System.Drawing.Color.Black;
            this.AdministratorBtn.Location = new System.Drawing.Point(64, 92);
            this.AdministratorBtn.Margin = new System.Windows.Forms.Padding(2);
            this.AdministratorBtn.Name = "AdministratorBtn";
            this.AdministratorBtn.Padding = new System.Windows.Forms.Padding(8);
            this.AdministratorBtn.Size = new System.Drawing.Size(206, 325);
            this.AdministratorBtn.TabIndex = 0;
            this.AdministratorBtn.TabStop = false;
            this.AdministratorBtn.Text = "Administrator";
            this.AdministratorBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AdministratorBtn.UseVisualStyleBackColor = false;
            this.AdministratorBtn.Click += new System.EventHandler(this.AdministratorBtn_Click);
            // 
            // KupacBtn
            // 
            this.KupacBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(172)))), ((int)(((byte)(65)))));
            this.KupacBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("KupacBtn.BackgroundImage")));
            this.KupacBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.KupacBtn.FlatAppearance.BorderSize = 0;
            this.KupacBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KupacBtn.Font = new System.Drawing.Font("Georgia", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KupacBtn.ForeColor = System.Drawing.Color.Black;
            this.KupacBtn.Location = new System.Drawing.Point(293, 92);
            this.KupacBtn.Margin = new System.Windows.Forms.Padding(2);
            this.KupacBtn.Name = "KupacBtn";
            this.KupacBtn.Padding = new System.Windows.Forms.Padding(8);
            this.KupacBtn.Size = new System.Drawing.Size(206, 325);
            this.KupacBtn.TabIndex = 0;
            this.KupacBtn.TabStop = false;
            this.KupacBtn.Text = "Kupac";
            this.KupacBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.KupacBtn.UseVisualStyleBackColor = false;
            this.KupacBtn.Click += new System.EventHandler(this.KupacBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Georgia", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(194, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Shoe Heaven";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(11, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(46, 41);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Pocetak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(549, 455);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KupacBtn);
            this.Controls.Add(this.AdministratorBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Pocetak";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prodavnica obuće";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AdministratorBtn;
        private System.Windows.Forms.Button KupacBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

