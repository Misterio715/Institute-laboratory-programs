namespace WindowsApplication1
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
            this.tb1 = new System.Windows.Forms.TextBox();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.tb3 = new System.Windows.Forms.TextBox();
            this.tb4 = new System.Windows.Forms.TextBox();
            this.tb5 = new System.Windows.Forms.TextBox();
            this.tb6 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(417, 106);
            this.tb1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(148, 26);
            this.tb1.TabIndex = 0;
            // 
            // tb2
            // 
            this.tb2.Location = new System.Drawing.Point(417, 177);
            this.tb2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(148, 26);
            this.tb2.TabIndex = 1;
            // 
            // tb3
            // 
            this.tb3.Location = new System.Drawing.Point(417, 245);
            this.tb3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb3.Name = "tb3";
            this.tb3.Size = new System.Drawing.Size(148, 26);
            this.tb3.TabIndex = 2;
            // 
            // tb4
            // 
            this.tb4.BackColor = System.Drawing.Color.OrangeRed;
            this.tb4.Location = new System.Drawing.Point(44, 106);
            this.tb4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb4.Name = "tb4";
            this.tb4.Size = new System.Drawing.Size(148, 26);
            this.tb4.TabIndex = 3;
            // 
            // tb5
            // 
            this.tb5.BackColor = System.Drawing.Color.Yellow;
            this.tb5.Location = new System.Drawing.Point(44, 177);
            this.tb5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb5.Name = "tb5";
            this.tb5.Size = new System.Drawing.Size(148, 26);
            this.tb5.TabIndex = 4;
            // 
            // tb6
            // 
            this.tb6.BackColor = System.Drawing.Color.Chartreuse;
            this.tb6.Location = new System.Drawing.Point(44, 245);
            this.tb6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb6.Name = "tb6";
            this.tb6.Size = new System.Drawing.Size(148, 26);
            this.tb6.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 335);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(207, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "Установить интервалы";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(417, 335);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 7;
            this.button2.Text = "Включить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(417, 402);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 35);
            this.button3.TabIndex = 8;
            this.button3.Text = "Выключить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(446, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "СВЕТОФОР";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 498);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb6);
            this.Controls.Add(this.tb5);
            this.Controls.Add(this.tb4);
            this.Controls.Add(this.tb3);
            this.Controls.Add(this.tb2);
            this.Controls.Add(this.tb1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "СВЕТОФОР";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.TextBox tb3;
        private System.Windows.Forms.TextBox tb4;
        private System.Windows.Forms.TextBox tb5;
        private System.Windows.Forms.TextBox tb6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
    }
}