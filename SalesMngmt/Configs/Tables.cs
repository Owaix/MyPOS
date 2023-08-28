using Lib.Entity;
using Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Tables : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<tbl_Table> list = null;
        int compID = 0;

        public Tables(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;
        }

        private void Catgory_Load(object sender, EventArgs e)
        {
            pnlMain.Hide();
            list = db.tbl_Table.ToList();
            itemBindingSource.DataSource = list;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            itemBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtTable.Focus();
            label3.Text = "ADD";
            string path = Application.StartupPath + "\\Img\\124444444.png";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            tbl_Table obj = (tbl_Table)itemBindingSource.Current;
            pnlMain.Show();
            txtTable.Focus();
            label3.Text = "EDIT";
            //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Img\\" + obj.BarCode_ID;
        }


        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            tbl_Table us = (tbl_Table)itemBindingSource.Current;
            if (us.ID == 0)
            {
                itemBindingSource.RemoveCurrent();
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTable.Text == "")
            { MessageBox.Show("Please Provide Table Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                tbl_Table obj = (tbl_Table)itemBindingSource.Current;

                var Currentobj = db.tbl_Table.Where(x => x.TableName == txtTable.Text.Trim()).FirstOrDefault();
                if (obj.ID == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("Table Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.TableName == obj.TableName &&
                                         record.ID != obj.ID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Table Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                string path = Application.StartupPath;
                // string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);

                obj.TableName = txtTable.Text.Trim();
                obj.isDeleted = chkIsActive.Checked;
                obj.companyID = compID;
                if (obj.ID == 0)
                {
                    db.tbl_Table.Add(obj);
                }
                else
                {
                    var result = db.tbl_Table.SingleOrDefault(b => b.ID == obj.ID);
                    if (result != null)
                    {
                        result.TableName = txtTable.Text.Trim();
                        result.isDeleted = chkIsActive.Checked;
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();
            }
        }
        private void TableDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TableDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label10.Text = path;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload image.");
                }
            }
            catch (Exception ex)
            {
                //it will give if file is already exits..
                MessageBox.Show(ex.Message);
            }
        }

        #region -- Helper Method Start --
        public void GetDocCode()
        {
            //TableList obj = new TableList(new Table { }.Select());
            //docCode = "DOC-" + (obj.Count + 1);
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { TableDataGridView.DataSource = list; }
                else
                {
                    List<tbl_Table> filteredList = new List<tbl_Table>();
                    foreach (var item in list)
                    {
                        if (item.TableName.Contains(toolStripTextBoxFind.Text))
                        {
                            filteredList.Add(item);
                        }
                    }
                    TableDataGridView.DataSource = filteredList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //
        #endregion -- Helper Method End --
    }
}
