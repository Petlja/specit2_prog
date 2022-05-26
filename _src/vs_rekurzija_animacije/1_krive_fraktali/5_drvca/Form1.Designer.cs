namespace Drvca
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
            this.components = new System.ComponentModel.Container();
            this.numRedKrive = new System.Windows.Forms.NumericUpDown();
            this.lblRazvojText = new System.Windows.Forms.Label();
            this.lblRedKriveText = new System.Windows.Forms.Label();
            this.lblUgaoText = new System.Windows.Forms.Label();
            this.numUgaoDeg = new System.Windows.Forms.NumericUpDown();
            this.lblKoefSmanjenjaText = new System.Windows.Forms.Label();
            this.numKoefSmanjenja = new System.Windows.Forms.NumericUpDown();
            this.lblPocDuzinaText = new System.Windows.Forms.Label();
            this.numPocDuzina = new System.Windows.Forms.NumericUpDown();
            this.cbxRazvoj = new System.Windows.Forms.ComboBox();
            this.numNaDole = new System.Windows.Forms.NumericUpDown();
            this.lblNaDoleText = new System.Windows.Forms.Label();
            this.numMaxRandom = new System.Windows.Forms.NumericUpDown();
            this.lblMaxRandomText = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbNepomican = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numRedKrive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUgaoDeg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKoefSmanjenja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPocDuzina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNaDole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRandom)).BeginInit();
            this.SuspendLayout();
            // 
            // numRedKrive
            // 
            this.numRedKrive.Location = new System.Drawing.Point(952, 39);
            this.numRedKrive.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRedKrive.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRedKrive.Name = "numRedKrive";
            this.numRedKrive.Size = new System.Drawing.Size(52, 20);
            this.numRedKrive.TabIndex = 0;
            this.numRedKrive.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRedKrive.ValueChanged += new System.EventHandler(this.InputChanged);
            // 
            // lblRazvojText
            // 
            this.lblRazvojText.AutoSize = true;
            this.lblRazvojText.Location = new System.Drawing.Point(739, 14);
            this.lblRazvojText.Name = "lblRazvojText";
            this.lblRazvojText.Size = new System.Drawing.Size(43, 13);
            this.lblRazvojText.TabIndex = 2;
            this.lblRazvojText.Text = "Razvoj:";
            // 
            // lblRedKriveText
            // 
            this.lblRedKriveText.AutoSize = true;
            this.lblRedKriveText.Location = new System.Drawing.Point(890, 41);
            this.lblRedKriveText.Name = "lblRedKriveText";
            this.lblRedKriveText.Size = new System.Drawing.Size(56, 13);
            this.lblRedKriveText.TabIndex = 3;
            this.lblRedKriveText.Text = "Red krive:";
            // 
            // lblUgaoText
            // 
            this.lblUgaoText.AutoSize = true;
            this.lblUgaoText.Location = new System.Drawing.Point(865, 90);
            this.lblUgaoText.Name = "lblUgaoText";
            this.lblUgaoText.Size = new System.Drawing.Size(81, 13);
            this.lblUgaoText.TabIndex = 4;
            this.lblUgaoText.Text = "Jed. ugao [deg]";
            // 
            // numUgaoDeg
            // 
            this.numUgaoDeg.Location = new System.Drawing.Point(952, 88);
            this.numUgaoDeg.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numUgaoDeg.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUgaoDeg.Name = "numUgaoDeg";
            this.numUgaoDeg.Size = new System.Drawing.Size(52, 20);
            this.numUgaoDeg.TabIndex = 5;
            this.numUgaoDeg.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUgaoDeg.ValueChanged += new System.EventHandler(this.InputChanged);
            // 
            // lblKoefSmanjenjaText
            // 
            this.lblKoefSmanjenjaText.AutoSize = true;
            this.lblKoefSmanjenjaText.Location = new System.Drawing.Point(847, 116);
            this.lblKoefSmanjenjaText.Name = "lblKoefSmanjenjaText";
            this.lblKoefSmanjenjaText.Size = new System.Drawing.Size(99, 13);
            this.lblKoefSmanjenjaText.TabIndex = 6;
            this.lblKoefSmanjenjaText.Text = "Koef. smanjenja [%]";
            // 
            // numKoefSmanjenja
            // 
            this.numKoefSmanjenja.Location = new System.Drawing.Point(952, 114);
            this.numKoefSmanjenja.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numKoefSmanjenja.Name = "numKoefSmanjenja";
            this.numKoefSmanjenja.Size = new System.Drawing.Size(52, 20);
            this.numKoefSmanjenja.TabIndex = 7;
            this.numKoefSmanjenja.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numKoefSmanjenja.ValueChanged += new System.EventHandler(this.InputChanged);
            // 
            // lblPocDuzinaText
            // 
            this.lblPocDuzinaText.AutoSize = true;
            this.lblPocDuzinaText.Location = new System.Drawing.Point(862, 64);
            this.lblPocDuzinaText.Name = "lblPocDuzinaText";
            this.lblPocDuzinaText.Size = new System.Drawing.Size(84, 13);
            this.lblPocDuzinaText.TabIndex = 8;
            this.lblPocDuzinaText.Text = "Početna dužina:";
            // 
            // numPocDuzina
            // 
            this.numPocDuzina.Location = new System.Drawing.Point(952, 62);
            this.numPocDuzina.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPocDuzina.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPocDuzina.Name = "numPocDuzina";
            this.numPocDuzina.Size = new System.Drawing.Size(52, 20);
            this.numPocDuzina.TabIndex = 9;
            this.numPocDuzina.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPocDuzina.ValueChanged += new System.EventHandler(this.InputChanged);
            // 
            // cbxRazvoj
            // 
            this.cbxRazvoj.FormattingEnabled = true;
            this.cbxRazvoj.Items.AddRange(new object[] {
            "F[-X][+X]",
            "F[+X]F[-X]X[+X][-X]X",
            "F+[[X]-X]-F[-FX]+X",
            "X[+X]X[-X]X",
            "F[+X]F[-X]X",
            "XX-[-X+X+X]+[+X-X-X]",
            "X[+XX][-XX]X[+XX]-XXX",
            "X[+XX][-XX]X[+XX][-XX]X",
            "X[+X[+X][-X]X][-X[+X][-X]X]X[+][-X]X",
            "XX+[X-X]-[X[+X+X][-X-X]]-X+X",
            "XX+[X-X]-[X[+X+X][-X-X]]-[X+X]+",
            "XX+[X-X]-[-X+X+X]",
            "X[+X]X[-X]X"});
            this.cbxRazvoj.Location = new System.Drawing.Point(788, 12);
            this.cbxRazvoj.Name = "cbxRazvoj";
            this.cbxRazvoj.Size = new System.Drawing.Size(216, 21);
            this.cbxRazvoj.TabIndex = 10;
            this.cbxRazvoj.SelectedIndexChanged += new System.EventHandler(this.InputChanged);
            this.cbxRazvoj.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxRazvoj_KeyDown);
            // 
            // numNaDole
            // 
            this.numNaDole.Location = new System.Drawing.Point(952, 140);
            this.numNaDole.Name = "numNaDole";
            this.numNaDole.Size = new System.Drawing.Size(52, 20);
            this.numNaDole.TabIndex = 11;
            this.numNaDole.ValueChanged += new System.EventHandler(this.InputChanged);
            // 
            // lblNaDoleText
            // 
            this.lblNaDoleText.AutoSize = true;
            this.lblNaDoleText.Location = new System.Drawing.Point(873, 142);
            this.lblNaDoleText.Name = "lblNaDoleText";
            this.lblNaDoleText.Size = new System.Drawing.Size(71, 13);
            this.lblNaDoleText.TabIndex = 4;
            this.lblNaDoleText.Text = "Na dole [deg]";
            // 
            // numMaxRandom
            // 
            this.numMaxRandom.Location = new System.Drawing.Point(952, 166);
            this.numMaxRandom.Name = "numMaxRandom";
            this.numMaxRandom.Size = new System.Drawing.Size(52, 20);
            this.numMaxRandom.TabIndex = 11;
            this.numMaxRandom.ValueChanged += new System.EventHandler(this.InputChanged);
            // 
            // lblMaxRandomText
            // 
            this.lblMaxRandomText.AutoSize = true;
            this.lblMaxRandomText.Location = new System.Drawing.Point(847, 168);
            this.lblMaxRandomText.Name = "lblMaxRandomText";
            this.lblMaxRandomText.Size = new System.Drawing.Size(97, 13);
            this.lblMaxRandomText.TabIndex = 4;
            this.lblMaxRandomText.Text = "Max Random [deg]";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbNepomican
            // 
            this.cbNepomican.AutoSize = true;
            this.cbNepomican.Location = new System.Drawing.Point(893, 192);
            this.cbNepomican.Name = "cbNepomican";
            this.cbNepomican.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbNepomican.Size = new System.Drawing.Size(106, 17);
            this.cbNepomican.TabIndex = 12;
            this.cbNepomican.Text = "Nepomičan crtež";
            this.cbNepomican.UseVisualStyleBackColor = true;
            this.cbNepomican.CheckedChanged += new System.EventHandler(this.InputChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 475);
            this.Controls.Add(this.cbNepomican);
            this.Controls.Add(this.numMaxRandom);
            this.Controls.Add(this.numNaDole);
            this.Controls.Add(this.cbxRazvoj);
            this.Controls.Add(this.numPocDuzina);
            this.Controls.Add(this.lblPocDuzinaText);
            this.Controls.Add(this.numKoefSmanjenja);
            this.Controls.Add(this.lblKoefSmanjenjaText);
            this.Controls.Add(this.numUgaoDeg);
            this.Controls.Add(this.lblMaxRandomText);
            this.Controls.Add(this.lblNaDoleText);
            this.Controls.Add(this.lblUgaoText);
            this.Controls.Add(this.lblRedKriveText);
            this.Controls.Add(this.lblRazvojText);
            this.Controls.Add(this.numRedKrive);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numRedKrive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUgaoDeg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKoefSmanjenja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPocDuzina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNaDole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRandom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numRedKrive;
        private System.Windows.Forms.Label lblRazvojText;
        private System.Windows.Forms.Label lblRedKriveText;
        private System.Windows.Forms.Label lblUgaoText;
        private System.Windows.Forms.NumericUpDown numUgaoDeg;
        private System.Windows.Forms.Label lblKoefSmanjenjaText;
        private System.Windows.Forms.NumericUpDown numKoefSmanjenja;
        private System.Windows.Forms.Label lblPocDuzinaText;
        private System.Windows.Forms.NumericUpDown numPocDuzina;
        private System.Windows.Forms.ComboBox cbxRazvoj;
        private System.Windows.Forms.NumericUpDown numNaDole;
        private System.Windows.Forms.Label lblNaDoleText;
        private System.Windows.Forms.NumericUpDown numMaxRandom;
        private System.Windows.Forms.Label lblMaxRandomText;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbNepomican;
    }
}

