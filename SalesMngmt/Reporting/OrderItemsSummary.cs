using Lib.Entity;
using Microsoft.Reporting.WinForms;
using SalesMngmt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LabExpressDesktop.Reporting
{
    public partial class OrderItemsSummary : Form
    {
        #region -- Global variables start --
        bool isNight { get; set; } //= false;
        SaleManagerEntities db = null;
        #endregion -- Global variable end --
        AspNetUser user = null;

        public OrderItemsSummary(AspNetUser Usr)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            user = Usr;
        }

        private void BookingSummary_Load(object sender, EventArgs e)
        {
            fromDate.Value = DateTime.Today;
            endDate.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            PopulateUsers();
            getuserTime();
            List<Dict> dictList = new List<Dict>();
            dictList.Add(new Dict { key = 0, Value = "All" });
            dictList.AddRange(GetItems());
            FillCombo<Dict>(ddlItems, dictList, "Value", "Key");
            //    this.rptBookingSummary.RefreshReport();
        }

        private List<Dict> GetItems()
        {
            var orders = (from c in db.tbl_OrderDetails
                          join Items in db.Items on c.itemID equals Items.IID
                          group c by new
                          {
                              Items.IID,
                              Items.IName,
                          } into gcs
                          select new Dict()
                          {
                              key = gcs.Select(x => x.itemID).FirstOrDefault() ?? 0,
                              Value = gcs.Key.IName
                          }).ToList();
            return orders;
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
        private void btnRun_Click(object sender, EventArgs e)
        {


            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now;

            dtStart = fromDate.Value;
            dtEnd = endDate.Value;

            rptBookingSummary.LocalReport.DataSources.Clear();
            List<Lib.Model.ItemReportModel> obj = Lib.Reporting.Reports.ItemsSummary(dtStart, dtEnd, ddlItems.SelectedValue.ToString());
            rptBookingSummary.LocalReport.DataSources.Add(new ReportDataSource("Ds", obj));
            this.rptBookingSummary.RefreshReport();
        }


        #region -- Helper Method Start --
        //public void getRole(string roleID) { lblRoleID.Text = roleID; }

        //public void passUser(String userID)
        //{
        //    lbluserID.Text = userID;
        //}

        private void PopulateUsers()
        {
            var ul = db.AspNetUsers.ToList();
            if (ul.Count > 0)
            {
                AspNetUser objusers = new AspNetUser();
                objusers.Id = "0";
                objusers.UserName = "all";
                ul.Add(objusers);
                ddlItems.DisplayMember = "username";
                ddlItems.ValueMember = "userid";
                ddlItems.DataSource = ul.OrderByDescending(x => x.UserName == "all").ThenBy(x => x.UserName).ToList();
            }
            else { ddlItems.DataSource = null; }
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
    }
}
