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
            this.bt_Test = new System.Windows.Forms.Button();
            this.bt_Task4 = new System.Windows.Forms.Button();
            this.bt_Task3 = new System.Windows.Forms.Button();
            this.bt_Task2 = new System.Windows.Forms.Button();
            this.tb_delay = new System.Windows.Forms.TextBox();
            this.lbl_delay = new System.Windows.Forms.Label();
            this.bt_Stop = new System.Windows.Forms.Button();
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
            this.bt_Task1.Location = new System.Drawing.Point(659, 13);
            this.bt_Task1.Name = "bt_Task1";
            this.bt_Task1.Size = new System.Drawing.Size(100, 33);
            this.bt_Task1.TabIndex = 1;
            this.bt_Task1.Text = "Aufgabe 1";
            this.bt_Task1.UseVisualStyleBackColor = true;
            this.bt_Task1.Click += new System.EventHandler(this.bt_Task1_Click);
            // 
            // bt_Test
            // 
            this.bt_Test.Location = new System.Drawing.Point(659, 169);
            this.bt_Test.Name = "bt_Test";
            this.bt_Test.Size = new System.Drawing.Size(100, 33);
            this.bt_Test.TabIndex = 2;
            this.bt_Test.Text = "Test";
            this.bt_Test.UseVisualStyleBackColor = true;
            // 
            // bt_Task4
            // 
            this.bt_Task4.Location = new System.Drawing.Point(659, 130);
            this.bt_Task4.Name = "bt_Task4";
            this.bt_Task4.Size = new System.Drawing.Size(100, 33);
            this.bt_Task4.TabIndex = 3;
            this.bt_Task4.Text = "Aufgabe 4";
            this.bt_Task4.UseVisualStyleBackColor = true;
            // 
            // bt_Task3
            // 
            this.bt_Task3.Location = new System.Drawing.Point(659, 91);
            this.bt_Task3.Name = "bt_Task3";
            this.bt_Task3.Size = new System.Drawing.Size(100, 33);
            this.bt_Task3.TabIndex = 4;
            this.bt_Task3.Text = "Aufgabe 3";
            this.bt_Task3.UseVisualStyleBackColor = true;
            this.bt_Task3.Click += new System.EventHandler(this.bt_Task3_Click);
            // 
            // bt_Task2
            // 
            this.bt_Task2.Location = new System.Drawing.Point(659, 52);
            this.bt_Task2.Name = "bt_Task2";
            this.bt_Task2.Size = new System.Drawing.Size(100, 33);
            this.bt_Task2.TabIndex = 5;
            this.bt_Task2.Text = "Aufgabe 2";
            this.bt_Task2.UseVisualStyleBackColor = true;
            this.bt_Task2.Click += new System.EventHandler(this.bt_Task2_Click);
            // 
            // tb_delay
            // 
            this.tb_delay.Location = new System.Drawing.Point(659, 225);
            this.tb_delay.Name = "tb_delay";
            this.tb_delay.Size = new System.Drawing.Size(100, 20);
            this.tb_delay.TabIndex = 6;
            // 
            // lbl_delay
            // 
            this.lbl_delay.AutoSize = true;
            this.lbl_delay.Location = new System.Drawing.Point(660, 209);
            this.lbl_delay.Name = "lbl_delay";
            this.lbl_delay.Size = new System.Drawing.Size(70, 13);
            this.lbl_delay.TabIndex = 7;
            this.lbl_delay.Text = "Verzögerung:";
            // 
            // bt_Stop
            // 
            this.bt_Stop.Location = new System.Drawing.Point(659, 251);
            this.bt_Stop.Name = "bt_Stop";
            this.bt_Stop.Size = new System.Drawing.Size(100, 33);
            this.bt_Stop.TabIndex = 8;
            this.bt_Stop.Text = "Stop";
            this.bt_Stop.UseVisualStyleBackColor = true;
            this.bt_Stop.Click += new System.EventHandler(this.bt_Stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 661);
            this.Controls.Add(this.bt_Stop);
            this.Controls.Add(this.lbl_delay);
            this.Controls.Add(this.tb_delay);
            this.Controls.Add(this.bt_Task2);
            this.Controls.Add(this.bt_Task3);
            this.Controls.Add(this.bt_Task4);
            this.Controls.Add(this.bt_Test);
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
        private System.Windows.Forms.Button bt_Test;
        private System.Windows.Forms.Button bt_Task4;
        private System.Windows.Forms.Button bt_Task3;
        private System.Windows.Forms.Button bt_Task2;
        private System.Windows.Forms.TextBox tb_delay;
        private System.Windows.Forms.Label lbl_delay;
        private System.Windows.Forms.Button bt_Stop;
    }
}

