namespace DataGridSampleProject
{
    partial class DetailsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtReporter = new System.Windows.Forms.TextBox();
            this.txtReportee = new System.Windows.Forms.TextBox();
            this.txtProductLineResponsibility = new System.Windows.Forms.TextBox();
            this.txtWorkExperience = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboxDesignation = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.errName = new System.Windows.Forms.ErrorProvider(this.components);
            this.errDesignation = new System.Windows.Forms.ErrorProvider(this.components);
            this.errEmail = new System.Windows.Forms.ErrorProvider(this.components);
            this.errReporter = new System.Windows.Forms.ErrorProvider(this.components);
            this.errReportee = new System.Windows.Forms.ErrorProvider(this.components);
            this.errProdLineResp = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorWorkExperience = new System.Windows.Forms.ErrorProvider(this.components);
            this.Id = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errDesignation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errReporter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errReportee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errProdLineResp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorWorkExperience)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Designation: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 147);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email Id: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 195);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Reporter: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 242);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Reportee: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 344);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(287, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Work experience at SEDEMAC (in yrs): ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 291);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(207, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Product Line Responsibility: ";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(323, 55);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(241, 26);
            this.txtName.TabIndex = 7;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(323, 147);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(241, 26);
            this.txtEmail.TabIndex = 9;
            // 
            // txtReporter
            // 
            this.txtReporter.Location = new System.Drawing.Point(323, 192);
            this.txtReporter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReporter.Name = "txtReporter";
            this.txtReporter.Size = new System.Drawing.Size(241, 26);
            this.txtReporter.TabIndex = 10;
            // 
            // txtReportee
            // 
            this.txtReportee.Location = new System.Drawing.Point(323, 239);
            this.txtReportee.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReportee.Name = "txtReportee";
            this.txtReportee.Size = new System.Drawing.Size(241, 26);
            this.txtReportee.TabIndex = 11;
            // 
            // txtProductLineResponsibility
            // 
            this.txtProductLineResponsibility.Location = new System.Drawing.Point(323, 285);
            this.txtProductLineResponsibility.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtProductLineResponsibility.Name = "txtProductLineResponsibility";
            this.txtProductLineResponsibility.Size = new System.Drawing.Size(241, 26);
            this.txtProductLineResponsibility.TabIndex = 12;
            // 
            // txtWorkExperience
            // 
            this.txtWorkExperience.Location = new System.Drawing.Point(323, 338);
            this.txtWorkExperience.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtWorkExperience.Name = "txtWorkExperience";
            this.txtWorkExperience.Size = new System.Drawing.Size(241, 26);
            this.txtWorkExperience.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtId);
            this.panel1.Controls.Add(this.Id);
            this.panel1.Controls.Add(this.comboxDesignation);
            this.panel1.Controls.Add(this.txtWorkExperience);
            this.panel1.Controls.Add(this.txtProductLineResponsibility);
            this.panel1.Controls.Add(this.txtReportee);
            this.panel1.Controls.Add(this.txtReporter);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(105, 117);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 428);
            this.panel1.TabIndex = 0;
            // 
            // comboxDesignation
            // 
            this.comboxDesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxDesignation.FormattingEnabled = true;
            this.comboxDesignation.Items.AddRange(new object[] {
            "Engineer",
            "Lead Engineer",
            "Principal Engineer",
            "Chief Engineer"});
            this.comboxDesignation.Location = new System.Drawing.Point(323, 101);
            this.comboxDesignation.Name = "comboxDesignation";
            this.comboxDesignation.Size = new System.Drawing.Size(241, 28);
            this.comboxDesignation.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label8);
            this.panel2.Location = new System.Drawing.Point(105, 37);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(592, 46);
            this.panel2.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(194, 9);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(179, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "Employee Details";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.OliveDrab;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(558, 540);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(112, 35);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.submit_Click);
            // 
            // errName
            // 
            this.errName.ContainerControl = this;
            // 
            // errDesignation
            // 
            this.errDesignation.ContainerControl = this;
            // 
            // errEmail
            // 
            this.errEmail.ContainerControl = this;
            // 
            // errReporter
            // 
            this.errReporter.ContainerControl = this;
            // 
            // errReportee
            // 
            this.errReportee.ContainerControl = this;
            // 
            // errProdLineResp
            // 
            this.errProdLineResp.ContainerControl = this;
            // 
            // errorWorkExperience
            // 
            this.errorWorkExperience.ContainerControl = this;
            // 
            // Id
            // 
            this.Id.AutoSize = true;
            this.Id.Location = new System.Drawing.Point(24, 6);
            this.Id.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Id.Name = "Id";
            this.Id.Size = new System.Drawing.Size(31, 20);
            this.Id.TabIndex = 14;
            this.Id.Text = "Id: ";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(323, 5);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(241, 26);
            this.txtId.TabIndex = 15;
            // 
            // DetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GreenYellow;
            this.ClientSize = new System.Drawing.Size(826, 611);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DetailsForm";
            this.Text = "Employee Details";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errDesignation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errReporter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errReportee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errProdLineResp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorWorkExperience)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtReporter;
        private System.Windows.Forms.TextBox txtReportee;
        private System.Windows.Forms.TextBox txtProductLineResponsibility;
        private System.Windows.Forms.TextBox txtWorkExperience;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ErrorProvider errName;
        private System.Windows.Forms.ErrorProvider errDesignation;
        private System.Windows.Forms.ErrorProvider errEmail;
        private System.Windows.Forms.ErrorProvider errReporter;
        private System.Windows.Forms.ErrorProvider errReportee;
        private System.Windows.Forms.ErrorProvider errProdLineResp;
        private System.Windows.Forms.ErrorProvider errorWorkExperience;
        private System.Windows.Forms.ComboBox comboxDesignation;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label Id;
    }
}