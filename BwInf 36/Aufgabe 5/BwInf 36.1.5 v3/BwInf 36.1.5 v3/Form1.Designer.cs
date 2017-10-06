namespace BwInf_36._1._5_v3
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
            this.button2 = new System.Windows.Forms.Button();
            this.bt_ = new System.Windows.Forms.Button();
            this.bt_Task2 = new System.Windows.Forms.Button();
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(659, 130);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "Aufgabe 4";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // bt_
            // 
            this.bt_.Location = new System.Drawing.Point(659, 91);
            this.bt_.Name = "bt_";
            this.bt_.Size = new System.Drawing.Size(100, 33);
            this.bt_.TabIndex = 4;
            this.bt_.Text = "Aufgabe 3";
            this.bt_.UseVisualStyleBackColor = true;
            // 
            // bt_Task2
            // 
            this.bt_Task2.Location = new System.Drawing.Point(659, 52);
            this.bt_Task2.Name = "bt_Task2";
            this.bt_Task2.Size = new System.Drawing.Size(100, 33);
            this.bt_Task2.TabIndex = 5;
            this.bt_Task2.Text = "Aufgabe 2";
            this.bt_Task2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 661);
            this.Controls.Add(this.bt_Task2);
            this.Controls.Add(this.bt_);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.bt_Test);
            this.Controls.Add(this.bt_Task1);
            this.Controls.Add(this.pbGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pbGrid;
        private System.Windows.Forms.Button bt_Task1;
        private System.Windows.Forms.Button bt_Test;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button bt_;
        private System.Windows.Forms.Button bt_Task2;
    }
}

