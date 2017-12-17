namespace BwInf
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
            this.pbGrid = new System.Windows.Forms.PictureBox();
            this.bt_Task1 = new System.Windows.Forms.Button();
            this.bt_Task4 = new System.Windows.Forms.Button();
            this.bt_Task3 = new System.Windows.Forms.Button();
            this.bt_Task2 = new System.Windows.Forms.Button();
            this.tb_delay = new System.Windows.Forms.TextBox();
            this.lbl_delay = new System.Windows.Forms.Label();
            this.tb_K = new System.Windows.Forms.TextBox();
            this.tb_L = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_count = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pbGrid
            // 
            this.pbGrid.Location = new System.Drawing.Point(13, 13);
            this.pbGrid.Name = "pbGrid";
            this.pbGrid.Size = new System.Drawing.Size(640, 640);
            this.pbGrid.TabIndex = 0;
            this.pbGrid.TabStop = false;
            // 
            // bt_Task1
            // 
            this.bt_Task1.Location = new System.Drawing.Point(659, 115);
            this.bt_Task1.Name = "bt_Task1";
            this.bt_Task1.Size = new System.Drawing.Size(100, 33);
            this.bt_Task1.TabIndex = 1;
            this.bt_Task1.Text = "Aufgabe 1";
            this.bt_Task1.UseVisualStyleBackColor = true;
            this.bt_Task1.Click += new System.EventHandler(this.bt_Task1_Click);
            // 
            // bt_Task4
            // 
            this.bt_Task4.Location = new System.Drawing.Point(659, 281);
            this.bt_Task4.Name = "bt_Task4";
            this.bt_Task4.Size = new System.Drawing.Size(100, 33);
            this.bt_Task4.TabIndex = 3;
            this.bt_Task4.Text = "Aufgabe 4";
            this.bt_Task4.UseVisualStyleBackColor = true;
            // 
            // bt_Task3
            // 
            this.bt_Task3.Location = new System.Drawing.Point(659, 193);
            this.bt_Task3.Name = "bt_Task3";
            this.bt_Task3.Size = new System.Drawing.Size(100, 33);
            this.bt_Task3.TabIndex = 4;
            this.bt_Task3.Text = "Aufgabe 3";
            this.bt_Task3.UseVisualStyleBackColor = true;
            this.bt_Task3.Click += new System.EventHandler(this.bt_Task3_Click);
            // 
            // bt_Task2
            // 
            this.bt_Task2.Location = new System.Drawing.Point(659, 154);
            this.bt_Task2.Name = "bt_Task2";
            this.bt_Task2.Size = new System.Drawing.Size(100, 33);
            this.bt_Task2.TabIndex = 5;
            this.bt_Task2.Text = "Aufgabe 2";
            this.bt_Task2.UseVisualStyleBackColor = true;
            this.bt_Task2.Click += new System.EventHandler(this.bt_Task2_Click);
            // 
            // tb_delay
            // 
            this.tb_delay.Location = new System.Drawing.Point(659, 633);
            this.tb_delay.Name = "tb_delay";
            this.tb_delay.Size = new System.Drawing.Size(100, 20);
            this.tb_delay.TabIndex = 6;
            // 
            // lbl_delay
            // 
            this.lbl_delay.AutoSize = true;
            this.lbl_delay.Location = new System.Drawing.Point(656, 617);
            this.lbl_delay.Name = "lbl_delay";
            this.lbl_delay.Size = new System.Drawing.Size(70, 13);
            this.lbl_delay.TabIndex = 7;
            this.lbl_delay.Text = "Verzögerung:";
            // 
            // tb_K
            // 
            this.tb_K.Location = new System.Drawing.Point(686, 233);
            this.tb_K.Name = "tb_K";
            this.tb_K.Size = new System.Drawing.Size(73, 20);
            this.tb_K.TabIndex = 9;
            this.tb_K.Text = "7";
            this.tb_K.TextChanged += new System.EventHandler(this.tb_K_TextChanged);
            // 
            // tb_L
            // 
            this.tb_L.Location = new System.Drawing.Point(686, 259);
            this.tb_L.Name = "tb_L";
            this.tb_L.Size = new System.Drawing.Size(73, 20);
            this.tb_L.TabIndex = 10;
            this.tb_L.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(656, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "k";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(656, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(9, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "l";
            // 
            // lbl_count
            // 
            this.lbl_count.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.lbl_count.AutoSize = true;
            this.lbl_count.Font = new System.Drawing.Font("Lucida Console", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_count.Location = new System.Drawing.Point(665, 13);
            this.lbl_count.Name = "lbl_count";
            this.lbl_count.Size = new System.Drawing.Size(24, 21);
            this.lbl_count.TabIndex = 13;
            this.lbl_count.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 661);
            this.Controls.Add(this.lbl_count);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_L);
            this.Controls.Add(this.tb_K);
            this.Controls.Add(this.lbl_delay);
            this.Controls.Add(this.tb_delay);
            this.Controls.Add(this.bt_Task2);
            this.Controls.Add(this.bt_Task3);
            this.Controls.Add(this.bt_Task4);
            this.Controls.Add(this.bt_Task1);
            this.Controls.Add(this.pbGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pbGrid;
        private System.Windows.Forms.Button bt_Task1;
        private System.Windows.Forms.Button bt_Task4;
        private System.Windows.Forms.Button bt_Task3;
        private System.Windows.Forms.Button bt_Task2;
        private System.Windows.Forms.TextBox tb_delay;
        private System.Windows.Forms.Label lbl_delay;
        private System.Windows.Forms.TextBox tb_K;
        private System.Windows.Forms.TextBox tb_L;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lbl_count;
    }
}

