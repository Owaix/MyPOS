﻿namespace LabExpressDesktop.Reporting
{
    partial class OrderItemsSummary
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
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.ddlItems = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.fromDate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rptBookingSummary = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ItemReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlDetails.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemReportModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Controls.Add(this.panel3);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(1008, 95);
            this.pnlDetails.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel3.Controls.Add(this.btnRun);
            this.panel3.Controls.Add(this.lblStartDate);
            this.panel3.Controls.Add(this.ddlItems);
            this.panel3.Controls.Add(this.lblUser);
            this.panel3.Controls.Add(this.lblEndDate);
            this.panel3.Controls.Add(this.endDate);
            this.panel3.Controls.Add(this.fromDate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1008, 94);
            this.panel3.TabIndex = 6;
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(887, 31);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(105, 35);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Run Report";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.BackColor = System.Drawing.Color.Transparent;
            this.lblStartDate.Location = new System.Drawing.Point(9, 40);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(84, 18);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "StartDate:";
            // 
            // ddlItems
            // 
            this.ddlItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlItems.FormattingEnabled = true;
            this.ddlItems.Location = new System.Drawing.Point(672, 36);
            this.ddlItems.Name = "ddlItems";
            this.ddlItems.Size = new System.Drawing.Size(209, 26);
            this.ddlItems.TabIndex = 3;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Location = new System.Drawing.Point(616, 40);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(54, 18);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Items:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.BackColor = System.Drawing.Color.Transparent;
            this.lblEndDate.Location = new System.Drawing.Point(310, 40);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(77, 18);
            this.lblEndDate.TabIndex = 0;
            this.lblEndDate.Text = "EndDate:";
            // 
            // endDate
            // 
            this.endDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDate.Location = new System.Drawing.Point(386, 37);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(216, 24);
            this.endDate.TabIndex = 2;
            this.endDate.Value = new System.DateTime(2018, 3, 8, 23, 59, 0, 0);
            // 
            // fromDate
            // 
            this.fromDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            this.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate.Location = new System.Drawing.Point(92, 37);
            this.fromDate.Name = "fromDate";
            this.fromDate.Size = new System.Drawing.Size(216, 24);
            this.fromDate.TabIndex = 1;
            this.fromDate.Value = new System.DateTime(2018, 3, 8, 8, 0, 0, 0);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rptBookingSummary);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 547);
            this.panel1.TabIndex = 1;
            // 
            // rptBookingSummary
            // 
            this.rptBookingSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Ds";
            reportDataSource1.Value = this.ItemReportModelBindingSource;
            this.rptBookingSummary.LocalReport.DataSources.Add(reportDataSource1);
            this.rptBookingSummary.LocalReport.ReportEmbeddedResource = "SalesMngmt.Reporting.Definition.ItemSummary.rdlc";
            this.rptBookingSummary.Location = new System.Drawing.Point(0, 0);
            this.rptBookingSummary.Name = "rptBookingSummary";
            this.rptBookingSummary.ShowExportButton = false;
            this.rptBookingSummary.Size = new System.Drawing.Size(1008, 547);
            this.rptBookingSummary.TabIndex = 0;
            // 
            // OrderItemsSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 642);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OrderItemsSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Items Summary";
            this.Load += new System.EventHandler(this.BookingSummary_Load);
            this.pnlDetails.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ItemReportModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.Reporting.WinForms.ReportViewer rptBookingSummary;
        private System.Windows.Forms.BindingSource ItemReportModelBindingSource;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.ComboBox ddlItems;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblEndDate;
        public System.Windows.Forms.DateTimePicker endDate;
        public System.Windows.Forms.DateTimePicker fromDate;
    }
}