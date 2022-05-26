namespace HilbertovaKriva
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
            this.numRedKrive = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numRedKrive)).BeginInit();
            this.SuspendLayout();
            // 
            // numRedKrive
            // 
            this.numRedKrive.Location = new System.Drawing.Point(18, 16);
            this.numRedKrive.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numRedKrive.Name = "numRedKrive";
            this.numRedKrive.Size = new System.Drawing.Size(67, 20);
            this.numRedKrive.TabIndex = 0;
            this.numRedKrive.ValueChanged += new System.EventHandler(this.numRedKrive_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numRedKrive);
            this.Name = "Form1";
            this.Text = "Hilbertova kriva";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.numRedKrive)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numRedKrive;
    }
}

