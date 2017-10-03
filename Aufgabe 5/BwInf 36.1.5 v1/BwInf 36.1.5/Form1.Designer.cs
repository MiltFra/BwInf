namespace BwInf_36._1._5
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
            this.pb_Grid = new System.Windows.Forms.PictureBox();
            this.tb_setPawn_x = new System.Windows.Forms.TextBox();
            this.tb_setPawn_y = new System.Windows.Forms.TextBox();
            this.lbl_setPawn = new System.Windows.Forms.Label();
            this.lbl_setRook = new System.Windows.Forms.Label();
            this.tb_setRook_y = new System.Windows.Forms.TextBox();
            this.tb_setRook_x = new System.Windows.Forms.TextBox();
            this.bt_setPawn = new System.Windows.Forms.Button();
            this.bt_setRook = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_Grid
            // 
            this.pb_Grid.Location = new System.Drawing.Point(13, 13);
            this.pb_Grid.Name = "pb_Grid";
            this.pb_Grid.Size = new System.Drawing.Size(650, 650);
            this.pb_Grid.TabIndex = 0;
            this.pb_Grid.TabStop = false;
            // 
            // tb_setPawn_x
            // 
            this.tb_setPawn_x.Location = new System.Drawing.Point(669, 37);
            this.tb_setPawn_x.Name = "tb_setPawn_x";
            this.tb_setPawn_x.Size = new System.Drawing.Size(95, 20);
            this.tb_setPawn_x.TabIndex = 1;
            // 
            // tb_setPawn_y
            // 
            this.tb_setPawn_y.Location = new System.Drawing.Point(773, 37);
            this.tb_setPawn_y.Name = "tb_setPawn_y";
            this.tb_setPawn_y.Size = new System.Drawing.Size(95, 20);
            this.tb_setPawn_y.TabIndex = 2;
            // 
            // lbl_setPawn
            // 
            this.lbl_setPawn.AutoSize = true;
            this.lbl_setPawn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_setPawn.Location = new System.Drawing.Point(708, 13);
            this.lbl_setPawn.Name = "lbl_setPawn";
            this.lbl_setPawn.Size = new System.Drawing.Size(123, 21);
            this.lbl_setPawn.TabIndex = 3;
            this.lbl_setPawn.Text = "X           -           Y";
            // 
            // lbl_setRook
            // 
            this.lbl_setRook.AutoSize = true;
            this.lbl_setRook.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_setRook.Location = new System.Drawing.Point(708, 108);
            this.lbl_setRook.Name = "lbl_setRook";
            this.lbl_setRook.Size = new System.Drawing.Size(123, 21);
            this.lbl_setRook.TabIndex = 6;
            this.lbl_setRook.Text = "X           -           Y";
            // 
            // tb_setRook_y
            // 
            this.tb_setRook_y.Location = new System.Drawing.Point(773, 132);
            this.tb_setRook_y.Name = "tb_setRook_y";
            this.tb_setRook_y.Size = new System.Drawing.Size(95, 20);
            this.tb_setRook_y.TabIndex = 5;
            // 
            // tb_setRook_x
            // 
            this.tb_setRook_x.Location = new System.Drawing.Point(669, 132);
            this.tb_setRook_x.Name = "tb_setRook_x";
            this.tb_setRook_x.Size = new System.Drawing.Size(95, 20);
            this.tb_setRook_x.TabIndex = 4;
            // 
            // bt_setPawn
            // 
            this.bt_setPawn.Location = new System.Drawing.Point(669, 63);
            this.bt_setPawn.Name = "bt_setPawn";
            this.bt_setPawn.Size = new System.Drawing.Size(199, 30);
            this.bt_setPawn.TabIndex = 7;
            this.bt_setPawn.Text = "Set Pawn";
            this.bt_setPawn.UseVisualStyleBackColor = true;
            this.bt_setPawn.Click += new System.EventHandler(this.bt_setPawn_Click);
            // 
            // bt_setRook
            // 
            this.bt_setRook.Location = new System.Drawing.Point(669, 158);
            this.bt_setRook.Name = "bt_setRook";
            this.bt_setRook.Size = new System.Drawing.Size(199, 30);
            this.bt_setRook.TabIndex = 8;
            this.bt_setRook.Text = "Set Rook";
            this.bt_setRook.UseVisualStyleBackColor = true;
            this.bt_setRook.Click += new System.EventHandler(this.bt_setRook_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(880, 665);
            this.Controls.Add(this.bt_setRook);
            this.Controls.Add(this.bt_setPawn);
            this.Controls.Add(this.lbl_setRook);
            this.Controls.Add(this.tb_setRook_y);
            this.Controls.Add(this.tb_setRook_x);
            this.Controls.Add(this.lbl_setPawn);
            this.Controls.Add(this.tb_setPawn_y);
            this.Controls.Add(this.tb_setPawn_x);
            this.Controls.Add(this.pb_Grid);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_Grid;
        private System.Windows.Forms.TextBox tb_setPawn_x;
        private System.Windows.Forms.TextBox tb_setPawn_y;
        private System.Windows.Forms.Label lbl_setPawn;
        private System.Windows.Forms.Label lbl_setRook;
        private System.Windows.Forms.TextBox tb_setRook_y;
        private System.Windows.Forms.TextBox tb_setRook_x;
        private System.Windows.Forms.Button bt_setPawn;
        private System.Windows.Forms.Button bt_setRook;
    }
}

