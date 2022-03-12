namespace LabExpressDesktop.Reporting
{
    partial class OrderSummaryForm
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.OrderReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.ddlUsers = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.fromDate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rptBookingSummary = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.OrderReportModelBindingSource)).BeginInit();
            this.pnlDetails.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Controls.Add(this.panel3);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlDetails.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(1344, 130);
            this.pnlDetails.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.comboBox1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnRun);
            this.panel3.Controls.Add(this.lblStartDate);
            this.panel3.Controls.Add(this.ddlUsers);
            this.panel3.Controls.Add(this.lblUser);
            this.panel3.Controls.Add(this.lblEndDate);
            this.panel3.Controls.Add(this.endDate);
            this.panel3.Controls.Add(this.fromDate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1344, 127);
            this.panel3.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SandyBrown;
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(1183, 74);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 43);
            this.button1.TabIndex = 7;
            this.button1.Text = "Print";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1095, 43);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(79, 32);
            this.comboBox1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(1032, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type:";
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.LimeGreen;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(1183, 25);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(140, 43);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Run Report";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.BackColor = System.Drawing.Color.Transparent;
            this.lblStartDate.Location = new System.Drawing.Point(12, 49);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(99, 24);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "StartDate:";
            // 
            // ddlUsers
            // 
            this.ddlUsers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlUsers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlUsers.FormattingEnabled = true;
            this.ddlUsers.Location = new System.Drawing.Point(875, 44);
            this.ddlUsers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ddlUsers.Name = "ddlUsers";
            this.ddlUsers.Size = new System.Drawing.Size(153, 32);
            this.ddlUsers.TabIndex = 3;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Location = new System.Drawing.Point(808, 49);
            this.lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(59, 24);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "User:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.BackColor = System.Drawing.Color.Transparent;
            this.lblEndDate.Location = new System.Drawing.Point(413, 49);
            this.lblEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(96, 24);
            this.lblEndDate.TabIndex = 0;
            this.lblEndDate.Text = "EndDate:";
            // 
            // endDate
            // 
            this.endDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDate.Location = new System.Drawing.Point(515, 46);
            this.endDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(287, 29);
            this.endDate.TabIndex = 2;
            this.endDate.Value = new System.DateTime(2018, 3, 8, 23, 59, 0, 0);
            // 
            // fromDate
            // 
            this.fromDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            this.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate.Location = new System.Drawing.Point(123, 46);
            this.fromDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fromDate.Name = "fromDate";
            this.fromDate.Size = new System.Drawing.Size(287, 29);
            this.fromDate.TabIndex = 1;
            this.fromDate.Value = new System.DateTime(2018, 3, 8, 8, 0, 0, 0);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rptBookingSummary);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 130);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1344, 660);
            this.panel1.TabIndex = 1;
            // 
            // rptBookingSummary
            // 
            this.rptBookingSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Ds";
            reportDataSource1.Value = this.OrderReportModelBindingSource;
            this.rptBookingSummary.LocalReport.DataSources.Add(reportDataSource1);
            this.rptBookingSummary.LocalReport.ReportEmbeddedResource = "SalesMngmt.Reporting.Definition.BookingSummary.rdlc";
            this.rptBookingSummary.Location = new System.Drawing.Point(0, 0);
            this.rptBookingSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rptBookingSummary.Name = "rptBookingSummary";
            this.rptBookingSummary.ShowExportButton = false;
            this.rptBookingSummary.Size = new System.Drawing.Size(1344, 660);
            this.rptBookingSummary.TabIndex = 0;
            // 
            // OrderSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 790);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OrderSummaryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Booking Summary";
            this.Load += new System.EventHandler(this.BookingSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OrderReportModelBindingSource)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox ddlUsers;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.Reporting.WinForms.ReportViewer rptBookingSummary;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.DateTimePicker fromDate;
        public System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource OrderReportModelBindingSource;
        private System.Windows.Forms.Button button1;
    }
}