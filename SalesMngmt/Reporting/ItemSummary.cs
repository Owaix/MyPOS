using Lib.Entity;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Reporting
{
    public partial class ItemSummary : Form
    {
        //public CustomerLedgerSummary()
        //{
        //    InitializeComponent();
        //}

        SaleManagerEntities db = null;
        int compID = 0;
        int obj = 0;
        int AcCode = 0;
        double Amt = 0;
        DateTime dt = DateTime.Now;
 
        public ItemSummary()
        {
            InitializeComponent();
            db = new SaleManagerEntities();
        }

        private void Category_Load(object sender, EventArgs e)
        {
            var lstItem = db.Items.ToList();
            lstItem.Add(new Item { IID = 0, IName = "Select Item" });
            lstItem = lstItem.OrderBy(x => x.IID).ToList();
            FillCombo(cmbxItem, lstItem, "IName", "IID", 0);
        }

        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected)
        {
            comboBox.DataSource = obj;
            comboBox.DisplayMember = Name; // Column Name
            comboBox.ValueMember = ID;  // Column Name
            comboBox.SelectedValue = selected;
        }


        private void lblEdit_Click(object sender, EventArgs e)
        {
            if (obj == 0)
            {
                MessageBox.Show("Select any row first");
            }
            else
            {

                //var tbl = db.Articles.Where(x => x.ProductID == obj).FirstOrDefault();

                ////txtArticalNo.Text = tbl.ArticleNo;
                //txtChkNo.Text = tbl.ProductName;
                //cmbxAccount.SelectedValue = tbl.ArticleTypeID;
                //cmbxCustomer.SelectedValue = tbl.StyleID;
                //chkIsActive.Checked = (bool)tbl.IsDelete;



            }



        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


  

        private void catDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CategorysDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #region -- Helper Method Start --
        public void GetDocCode()
        {
            //catList obj = new catList(new cat { }.Select());
            //docCode = "DOC-" + (obj.Count + 1);
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (toolStripTextBoxFind.Text.Trim().Length == 0) { CategorysDataGridView.DataSource = list; }
                //else
                //{
                //    // CategorysDataGridView.DataSource = list.FindAll(x => x.ArticleType1.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                //}
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //
        #endregion -- Helper Method End --

        private void CategorysDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //  var abc=   CategorysDataGridView.Rows[e.RowIndex].Cells[0].Value;
        }

 

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CategorysDataGridView.Rows.Clear();
            int Vendorcode = Convert.ToInt32(cmbxItem.SelectedValue);
            SaleManagerEntities db1 = new SaleManagerEntities();

            //var getdata = db.itemSummaryReport(dtFrom.Value.Date, dtTOdate.Value, Vendorcode).ToList();//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;
            //var count = getdata.Count();
            //for (int b = 0; b < count; b++)
            //{
            //    CategorysDataGridView.Rows.Add(b + 1, getdata[b].Name, getdata[b].Quantity, getdata[b].Amount);
            //}
            //if (getdata.Count > 0)
            //{
            //    CategorysDataGridView.Rows.Add("", "", getdata.Sum(x => x.Quantity), getdata.Sum(x => x.Amount));
            //}
        }   

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
