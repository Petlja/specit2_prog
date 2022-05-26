namespace SkakacevaTuraOpt
{
    partial class Form1
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
            this.numBrojVrsta = new System.Windows.Forms.NumericUpDown();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.trackBarBrzina = new System.Windows.Forms.TrackBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslBrzina = new System.Windows.Forms.ToolStripStatusLabel();
            this.numBrojKolona = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numBrojVrsta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrzina)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBrojKolona)).BeginInit();
            this.SuspendLayout();
            // 
            // numBrojVrsta
            // 
            this.numBrojVrsta.Location = new System.Drawing.Point(12, 15);
            this.numBrojVrsta.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numBrojVrsta.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numBrojVrsta.Name = "numBrojVrsta";
            this.numBrojVrsta.Size = new System.Drawing.Size(59, 20);
            this.numBrojVrsta.TabIndex = 0;
            this.numBrojVrsta.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numBrojVrsta.ValueChanged += new System.EventHandler(this.numVelTable_ValueChanged);
            // 
            // btnTrazi
            // 
            this.btnTrazi.Location = new System.Drawing.Point(186, 12);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(43, 23);
            this.btnTrazi.TabIndex = 1;
            this.btnTrazi.Text = "Trazi";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click);
            // 
            // trackBarBrzina
            // 
            this.trackBarBrzina.Location = new System.Drawing.Point(235, 12);
            this.trackBarBrzina.Maximum = 101;
            this.trackBarBrzina.Name = "trackBarBrzina";
            this.trackBarBrzina.Size = new System.Drawing.Size(137, 45);
            this.trackBarBrzina.TabIndex = 2;
            this.trackBarBrzina.Scroll += new System.EventHandler(this.trackBarBrzina_Scroll);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslBrzina});
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(384, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslBrzina
            // 
            this.tslBrzina.Name = "tslBrzina";
            this.tslBrzina.Size = new System.Drawing.Size(0, 17);
            // 
            // numBrojKolona
            // 
            this.numBrojKolona.Location = new System.Drawing.Point(97, 15);
            this.numBrojKolona.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numBrojKolona.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numBrojKolona.Name = "numBrojKolona";
            this.numBrojKolona.Size = new System.Drawing.Size(59, 20);
            this.numBrojKolona.TabIndex = 0;
            this.numBrojKolona.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numBrojKolona.ValueChanged += new System.EventHandler(this.numVelTable_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 441);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.trackBarBrzina);
            this.Controls.Add(this.btnTrazi);
            this.Controls.Add(this.numBrojKolona);
            this.Controls.Add(this.numBrojVrsta);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(400, 480);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numBrojVrsta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrzina)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBrojKolona)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numBrojVrsta;
        private System.Windows.Forms.Button btnTrazi;
        private System.Windows.Forms.TrackBar trackBarBrzina;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslBrzina;
        private System.Windows.Forms.NumericUpDown numBrojKolona;
    }
}

