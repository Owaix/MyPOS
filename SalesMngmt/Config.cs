using Lib.Entity;
using SalesMngmt.Configs;
using System;

namespace SalesMngmt
{
    public partial class Config : MetroFramework.Forms.MetroForm
    {
        int CompanyID = 0;
        AspNetUser Usr = null;
        public Config(int cmpID, AspNetUser user)
        {
            InitializeComponent();
            Shown += Config_Shown;
            CompanyID = cmpID;
            Usr = user;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Customer cst = new Customer(CompanyID);
            //cst.MdiParent = this;
            //cst.Show();
        }

        private void itemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Config_Shown(object sender, EventArgs e)
        {
            Main main = new Main(CompanyID, Usr);
            main.Close();
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Company company = new Company();
            //company.MdiParent = this;
            //company.Show();
        }

        private void vendorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FVendor vandor = new FVendor(CompanyID);
            //vandor.MdiParent = this;
            //vandor.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Unit unit = new Unit(CompanyID);
            //unit.MdiParent = this;
            //unit.Show();
        }

        private void itemsToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void itemCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void itemsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Catgory category = new Catgory(CompanyID);
            category.MdiParent = this;
            category.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Maker maker = new Maker(CompanyID);
            //maker.MdiParent = this;
            //maker.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //Products products = new Products(CompanyID);
            //products.MdiParent = this;
            //products.Show();
            Prod products = new Prod(CompanyID);
            products.MdiParent = this;
            products.Show();
        }

        private void cOAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Coa coa = new Coa(CompanyID);
            //coa.MdiParent = this;
            //coa.Show();
            //PInv inv = new PInv(CompanyID);
            //inv.MdiParent = this;
            //inv.Show();
        }

        private void Config_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.Dispose();
            Main form = new Main(CompanyID, Usr);
            form.Show();
        }
    }
}