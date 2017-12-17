namespace BwInf_36._1._2__GUI_
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
            this.tb_SchoolWeek = new System.Windows.Forms.TextBox();
            this.tb_HolidayWE = new System.Windows.Forms.TextBox();
            this.tb_HolidayWeek = new System.Windows.Forms.TextBox();
            this.tb_SchoolWE = new System.Windows.Forms.TextBox();
            this.lb_GroupMembers = new System.Windows.Forms.ListBox();
            this.bt_addAdult = new System.Windows.Forms.Button();
            this.bt_AddSmallChild = new System.Windows.Forms.Button();
            this.bt_addChild = new System.Windows.Forms.Button();
            this.tb_Age = new System.Windows.Forms.TextBox();
            this.bt_addPerson = new System.Windows.Forms.Button();
            this.lbl_Age = new System.Windows.Forms.Label();
            this.bt_Calculate = new System.Windows.Forms.Button();
            this.bt_undo = new System.Windows.Forms.Button();
            this.lbl_AdultCount = new System.Windows.Forms.Label();
            this.lbl_SmallChildCount = new System.Windows.Forms.Label();
            this.lbl_ChildCount = new System.Windows.Forms.Label();
            this.lb_output = new System.Windows.Forms.ListBox();
            this.lbl_Total = new System.Windows.Forms.Label();
            this.lbl_Coupons = new System.Windows.Forms.Label();
            this.tb_Coupons = new System.Windows.Forms.TextBox();
            this.lbl_SchoolWeek = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_SchoolWeek
            // 
            this.tb_SchoolWeek.Location = new System.Drawing.Point(240, 34);
            this.tb_SchoolWeek.Name = "tb_SchoolWeek";
            this.tb_SchoolWeek.Size = new System.Drawing.Size(100, 20);
            this.tb_SchoolWeek.TabIndex = 0;
            this.tb_SchoolWeek.TextChanged += new System.EventHandler(this.tb_SchoolWeek_TextChanged);
            // 
            // tb_HolidayWE
            // 
            this.tb_HolidayWE.Location = new System.Drawing.Point(240, 131);
            this.tb_HolidayWE.Name = "tb_HolidayWE";
            this.tb_HolidayWE.Size = new System.Drawing.Size(100, 20);
            this.tb_HolidayWE.TabIndex = 3;
            this.tb_HolidayWE.TextChanged += new System.EventHandler(this.tb_HolidayWE_TextChanged);
            // 
            // tb_HolidayWeek
            // 
            this.tb_HolidayWeek.Location = new System.Drawing.Point(240, 99);
            this.tb_HolidayWeek.Name = "tb_HolidayWeek";
            this.tb_HolidayWeek.Size = new System.Drawing.Size(100, 20);
            this.tb_HolidayWeek.TabIndex = 2;
            this.tb_HolidayWeek.TextChanged += new System.EventHandler(this.tb_HolidayWeek_TextChanged);
            // 
            // tb_SchoolWE
            // 
            this.tb_SchoolWE.Location = new System.Drawing.Point(240, 67);
            this.tb_SchoolWE.Name = "tb_SchoolWE";
            this.tb_SchoolWE.Size = new System.Drawing.Size(100, 20);
            this.tb_SchoolWE.TabIndex = 1;
            this.tb_SchoolWE.TextChanged += new System.EventHandler(this.tb_SchoolWE_TextChanged);
            // 
            // lb_GroupMembers
            // 
            this.lb_GroupMembers.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_GroupMembers.FormattingEnabled = true;
            this.lb_GroupMembers.Location = new System.Drawing.Point(12, 157);
            this.lb_GroupMembers.Name = "lb_GroupMembers";
            this.lb_GroupMembers.Size = new System.Drawing.Size(129, 290);
            this.lb_GroupMembers.TabIndex = 12;
            this.lb_GroupMembers.TabStop = false;
            this.lb_GroupMembers.UseTabStops = false;
            // 
            // bt_addAdult
            // 
            this.bt_addAdult.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_addAdult.Location = new System.Drawing.Point(147, 158);
            this.bt_addAdult.Name = "bt_addAdult";
            this.bt_addAdult.Size = new System.Drawing.Size(140, 33);
            this.bt_addAdult.TabIndex = 4;
            this.bt_addAdult.Text = "Erwachsener";
            this.bt_addAdult.UseVisualStyleBackColor = true;
            this.bt_addAdult.Click += new System.EventHandler(this.bt_addAdult_Click);
            // 
            // bt_AddSmallChild
            // 
            this.bt_AddSmallChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_AddSmallChild.Location = new System.Drawing.Point(147, 236);
            this.bt_AddSmallChild.Name = "bt_AddSmallChild";
            this.bt_AddSmallChild.Size = new System.Drawing.Size(140, 33);
            this.bt_AddSmallChild.TabIndex = 6;
            this.bt_AddSmallChild.Text = "Kleinkind";
            this.bt_AddSmallChild.UseVisualStyleBackColor = true;
            this.bt_AddSmallChild.Click += new System.EventHandler(this.bt_AddSmallChild_Click);
            // 
            // bt_addChild
            // 
            this.bt_addChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_addChild.Location = new System.Drawing.Point(147, 197);
            this.bt_addChild.Name = "bt_addChild";
            this.bt_addChild.Size = new System.Drawing.Size(140, 33);
            this.bt_addChild.TabIndex = 5;
            this.bt_addChild.Text = "Kind";
            this.bt_addChild.UseVisualStyleBackColor = true;
            this.bt_addChild.Click += new System.EventHandler(this.bt_addChild_Click);
            // 
            // tb_Age
            // 
            this.tb_Age.Location = new System.Drawing.Point(211, 274);
            this.tb_Age.Name = "tb_Age";
            this.tb_Age.Size = new System.Drawing.Size(129, 20);
            this.tb_Age.TabIndex = 7;
            this.tb_Age.TextChanged += new System.EventHandler(this.tb_Age_TextChanged);
            // 
            // bt_addPerson
            // 
            this.bt_addPerson.Enabled = false;
            this.bt_addPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_addPerson.Location = new System.Drawing.Point(147, 297);
            this.bt_addPerson.Name = "bt_addPerson";
            this.bt_addPerson.Size = new System.Drawing.Size(193, 33);
            this.bt_addPerson.TabIndex = 8;
            this.bt_addPerson.Text = "Person hinzufügen";
            this.bt_addPerson.UseVisualStyleBackColor = true;
            this.bt_addPerson.Click += new System.EventHandler(this.bt_addPerson_Click);
            // 
            // lbl_Age
            // 
            this.lbl_Age.AutoSize = true;
            this.lbl_Age.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Age.Location = new System.Drawing.Point(147, 272);
            this.lbl_Age.Name = "lbl_Age";
            this.lbl_Age.Size = new System.Drawing.Size(53, 24);
            this.lbl_Age.TabIndex = 18;
            this.lbl_Age.Text = "Alter:";
            // 
            // bt_Calculate
            // 
            this.bt_Calculate.Enabled = false;
            this.bt_Calculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Calculate.Location = new System.Drawing.Point(147, 401);
            this.bt_Calculate.Name = "bt_Calculate";
            this.bt_Calculate.Size = new System.Drawing.Size(193, 46);
            this.bt_Calculate.TabIndex = 11;
            this.bt_Calculate.Text = "Berechnen";
            this.bt_Calculate.UseVisualStyleBackColor = true;
            this.bt_Calculate.Click += new System.EventHandler(this.bt_Calculate_Click);
            // 
            // bt_undo
            // 
            this.bt_undo.Enabled = false;
            this.bt_undo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_undo.Location = new System.Drawing.Point(147, 336);
            this.bt_undo.Name = "bt_undo";
            this.bt_undo.Size = new System.Drawing.Size(193, 33);
            this.bt_undo.TabIndex = 9;
            this.bt_undo.Text = "Schritt zurück";
            this.bt_undo.UseVisualStyleBackColor = true;
            this.bt_undo.Click += new System.EventHandler(this.bt_undo_Click);
            // 
            // lbl_AdultCount
            // 
            this.lbl_AdultCount.AutoSize = true;
            this.lbl_AdultCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AdultCount.Location = new System.Drawing.Point(293, 163);
            this.lbl_AdultCount.Name = "lbl_AdultCount";
            this.lbl_AdultCount.Size = new System.Drawing.Size(25, 24);
            this.lbl_AdultCount.TabIndex = 21;
            this.lbl_AdultCount.Text = " 0";
            // 
            // lbl_SmallChildCount
            // 
            this.lbl_SmallChildCount.AutoSize = true;
            this.lbl_SmallChildCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SmallChildCount.Location = new System.Drawing.Point(293, 236);
            this.lbl_SmallChildCount.Name = "lbl_SmallChildCount";
            this.lbl_SmallChildCount.Size = new System.Drawing.Size(25, 24);
            this.lbl_SmallChildCount.TabIndex = 22;
            this.lbl_SmallChildCount.Text = " 0";
            // 
            // lbl_ChildCount
            // 
            this.lbl_ChildCount.AutoSize = true;
            this.lbl_ChildCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ChildCount.Location = new System.Drawing.Point(293, 202);
            this.lbl_ChildCount.Name = "lbl_ChildCount";
            this.lbl_ChildCount.Size = new System.Drawing.Size(25, 24);
            this.lbl_ChildCount.TabIndex = 23;
            this.lbl_ChildCount.Text = " 0";
            // 
            // lb_output
            // 
            this.lb_output.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_output.FormattingEnabled = true;
            this.lb_output.Location = new System.Drawing.Point(372, 34);
            this.lb_output.Name = "lb_output";
            this.lb_output.Size = new System.Drawing.Size(141, 316);
            this.lb_output.TabIndex = 24;
            this.lb_output.TabStop = false;
            // 
            // lbl_Total
            // 
            this.lbl_Total.AutoSize = true;
            this.lbl_Total.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total.Location = new System.Drawing.Point(368, 353);
            this.lbl_Total.Name = "lbl_Total";
            this.lbl_Total.Size = new System.Drawing.Size(84, 24);
            this.lbl_Total.TabIndex = 25;
            this.lbl_Total.Text = "Gesamt: ";
            // 
            // lbl_Coupons
            // 
            this.lbl_Coupons.AutoSize = true;
            this.lbl_Coupons.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Coupons.Location = new System.Drawing.Point(147, 373);
            this.lbl_Coupons.Name = "lbl_Coupons";
            this.lbl_Coupons.Size = new System.Drawing.Size(116, 24);
            this.lbl_Coupons.TabIndex = 27;
            this.lbl_Coupons.Text = "Gutscheine: ";
            // 
            // tb_Coupons
            // 
            this.tb_Coupons.Location = new System.Drawing.Point(279, 375);
            this.tb_Coupons.Name = "tb_Coupons";
            this.tb_Coupons.Size = new System.Drawing.Size(61, 20);
            this.tb_Coupons.TabIndex = 10;
            this.tb_Coupons.Text = "0";
            // 
            // lbl_SchoolWeek
            // 
            this.lbl_SchoolWeek.AutoSize = true;
            this.lbl_SchoolWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SchoolWeek.Location = new System.Drawing.Point(12, 30);
            this.lbl_SchoolWeek.Name = "lbl_SchoolWeek";
            this.lbl_SchoolWeek.Size = new System.Drawing.Size(145, 24);
            this.lbl_SchoolWeek.TabIndex = 28;
            this.lbl_SchoolWeek.Text = "Schule, Woche:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 24);
            this.label1.TabIndex = 29;
            this.label1.Text = "Ferien, Wochenende:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 24);
            this.label2.TabIndex = 30;
            this.label2.Text = "Ferien, Woche:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 24);
            this.label3.TabIndex = 31;
            this.label3.Text = "Schule, Wochenende:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(236, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 24);
            this.label4.TabIndex = 32;
            this.label4.Text = "Bewertung:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(368, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 24);
            this.label5.TabIndex = 33;
            this.label5.Text = "Tickets:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 452);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_SchoolWeek);
            this.Controls.Add(this.lbl_Coupons);
            this.Controls.Add(this.tb_Coupons);
            this.Controls.Add(this.lbl_Total);
            this.Controls.Add(this.lb_output);
            this.Controls.Add(this.lbl_ChildCount);
            this.Controls.Add(this.lbl_SmallChildCount);
            this.Controls.Add(this.lbl_AdultCount);
            this.Controls.Add(this.bt_undo);
            this.Controls.Add(this.bt_Calculate);
            this.Controls.Add(this.lbl_Age);
            this.Controls.Add(this.bt_addPerson);
            this.Controls.Add(this.tb_Age);
            this.Controls.Add(this.bt_addChild);
            this.Controls.Add(this.bt_AddSmallChild);
            this.Controls.Add(this.bt_addAdult);
            this.Controls.Add(this.lb_GroupMembers);
            this.Controls.Add(this.tb_SchoolWE);
            this.Controls.Add(this.tb_HolidayWeek);
            this.Controls.Add(this.tb_HolidayWE);
            this.Controls.Add(this.tb_SchoolWeek);
            this.Name = "Form1";
            this.Text = "BwInf 36.2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tb_SchoolWeek;
        private System.Windows.Forms.TextBox tb_HolidayWE;
        private System.Windows.Forms.TextBox tb_HolidayWeek;
        private System.Windows.Forms.TextBox tb_SchoolWE;
        private System.Windows.Forms.ListBox lb_GroupMembers;
        private System.Windows.Forms.Button bt_addAdult;
        private System.Windows.Forms.Button bt_AddSmallChild;
        private System.Windows.Forms.Button bt_addChild;
        private System.Windows.Forms.TextBox tb_Age;
        private System.Windows.Forms.Button bt_addPerson;
        private System.Windows.Forms.Label lbl_Age;
        private System.Windows.Forms.Button bt_Calculate;
        private System.Windows.Forms.Button bt_undo;
        private System.Windows.Forms.Label lbl_AdultCount;
        private System.Windows.Forms.Label lbl_SmallChildCount;
        private System.Windows.Forms.Label lbl_ChildCount;
        private System.Windows.Forms.ListBox lb_output;
        private System.Windows.Forms.Label lbl_Total;
        private System.Windows.Forms.Label lbl_Coupons;
        private System.Windows.Forms.TextBox tb_Coupons;
        private System.Windows.Forms.Label lbl_SchoolWeek;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

