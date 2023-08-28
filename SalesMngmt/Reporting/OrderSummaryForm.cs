using Lib.Entity;
using Lib.Model;
using Lib.Utilities;
using Microsoft.Reporting.WinForms;
using SalesMngmt;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LabExpressDesktop.Reporting
{
    public partial class OrderSummaryForm : Form
    {
        #region -- Global variables start --
        bool isNight { get; set; } //= false;
        SaleManagerEntities db = null;
        #endregion -- Global variable end --
        AspNetUser user = null;

        public OrderSummaryForm(AspNetUser Usr)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            user = Usr;
        }

        private IEnumerable<Dict> GetPaymentType()
        {
            List<Dict> dict = new List<Dict>();
            dict.Add(new Dict { key = 0, Value = "All" });
            dict.Add(new Dict { key = 1, Value = "Cash" });
            dict.Add(new Dict { key = 3, Value = "Card" });
            dict.Add(new Dict { key = 2, Value = "Void" });
            return dict;
        }
        private void BookingSummary_Load(object sender, EventArgs e)
        {
            fromDate.Value = DateTime.Today;
            endDate.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            PopulateUsers();
            getuserTime();

            List<Dict> dict = new List<Dict>();
            dict.Add(new Dict { key = 0, Value = "All" });
            dict.Add(new Dict { key = 1, Value = "Delivery" });
            dict.Add(new Dict { key = 2, Value = "Dine-In" });
            dict.Add(new Dict { key = 3, Value = "Take Away" });
            dict.Add(new Dict { key = 4, Value = "Express" });

            FillCombo<Dict>(cmbOrder, dict, "Value", "Key");
            FillCombo<Dict>(comboBox1, GetPaymentType(), "Value", "Key");
            //    this.rptBookingSummary.RefreshReport();
        }

        public void FillCombo<T>(ComboBox comboBox, IEnumerable<T> obj, String Name, String ID, int selected = 0)
        {
            if (obj.Count() > 0)
            {
                comboBox.DataSource = obj;
                comboBox.DisplayMember = Name; // Column Name
                comboBox.ValueMember = ID;  // Column Name
                comboBox.SelectedIndex = selected;
            }
        }
        List<OrderReportModel> list = null;
        private void btnRun_Click(object sender, EventArgs e)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now;
            dtStart = fromDate.Value;
            dtEnd = endDate.Value;

            rptBookingSummary.LocalReport.DataSources.Clear();
            list = Lib.Reporting.Reports.BookingSummary(dtStart, dtEnd, ddlUsers.Text, comboBox1.SelectedValue.ToString());

            if (cmbOrder.SelectedValue.ToString() == "0")
                list = list;
            if (cmbOrder.SelectedValue.ToString() == "1")
                list = list.Where(x => x.ordrType.StartsWith("H")).ToList();
            if (cmbOrder.SelectedValue.ToString() == "2")
                list = list.Where(x => x.ordrType.StartsWith("D")).ToList();
            if (cmbOrder.SelectedValue.ToString() == "3")
                list = list.Where(x => x.ordrType.StartsWith("T")).ToList();
            if (cmbOrder.SelectedValue.ToString() == "4")
                list = list.Where(x => x.ordrType.StartsWith("E")).ToList();

            rptBookingSummary.LocalReport.DataSources.Add(new ReportDataSource("Ds", list));
            this.rptBookingSummary.RefreshReport();
            button1.Enabled = true;
        }

        #region -- Helper Method Start --

        private void PopulateUsers()
        {
            var ul = db.AspNetUsers.ToList();
            if (ul.Count > 0)
            {
                AspNetUser objusers = new AspNetUser();
                objusers.Id = "0";
                objusers.UserName = "All";
                ul.Add(objusers);
                ddlUsers.DisplayMember = "username";
                ddlUsers.ValueMember = "userid";
                ddlUsers.DataSource = ul.OrderByDescending(x => x.UserName == "All").ThenBy(x => x.UserName).ToList();
            }
            else { ddlUsers.DataSource = null; }
        }

        public void getuserTime()
        {
        }
        #endregion -- Helper Method End --

        private void OrderSummaryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Main config = new Main(0, user);
            config.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list.ForEach(x => { x.item = x.InvoiceNo; x.userName = ddlUsers.Text; });
            Silent silent = new Silent();
            ReportViewer reportViewer1 = new ReportViewer();
            //  Printer.setDef(ConfigurationManager.AppSettings["Thermal"].ToString());
            silent.Run(reportViewer1, list, "SalesMngmt.Reporting.Definition.Summary.rdlc");
        }
    }
}
