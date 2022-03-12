using Lib.Entity;
using Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Prod : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Item> list = null;
        int compID = 0;

        public Prod(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;
        }

        private void Prod_Load(object sender, EventArgs e)
        {
            pnlMain.Hide();
            FillCombo(cmbxCat, db.Items_Cat.ToList(), "Cat", "CatID", 1);
            list = (from i in db.Items.ToList()
                    join c in db.Items_Cat on i.SCatID equals c.CatID
                    select new Item
                    {
                        IID = i.IID,
                        SCatID = i.SCatID,
                        Size = c.Cat,
                        IName = i.IName,
                        SalesPrice = i.SalesPrice,
                        isDeleted = i.isDeleted,
                        RetailPOne = i.RetailPOne,
                        RetailPTwo = i.RetailPTwo,
                        RetailPThree = i.RetailPThree,
                        BarCode_ID = i.BarCode_ID
                    }).OrderBy(x => x.Size).ThenBy(x => x.IName).ToList();

            bindingSource1.DataSource = list;
        }

        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected = 1)
        {
            try
            {
                comboBox.DataSource = obj;
                comboBox.DisplayMember = Name; // Column Name
                comboBox.ValueMember = ID;  // Column Name
                comboBox.SelectedValue = selected;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            bindingSource1.AddNew();
            pnlMain.Show();
            txtItem.Focus();
            label3.Text = "ADD";
            string path = Application.StartupPath + "\\Img\\124444444.png";
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(path);
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Item obj = (Item)bindingSource1.Current;
                var item = db.Items.Where(x => x.IID == obj.IID).FirstOrDefault();
                if (item != null)
                {
                    obj.BarCode_ID = item.BarCode_ID;
                }
                pnlMain.Show();
                txtItem.Focus();
                label3.Text = "EDIT";
                string path = Application.StartupPath + "\\Img\\" + obj.BarCode_ID;
                //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Img\\" + obj.BarCode_ID;
                openFileDialog1.FileName = path;
                label10.Text = obj.BarCode_ID;
                pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                //pictureBox1.Image = Image.FromFile(path);
                pictureBox1.Image = Utillityfunctions.LoadImage(item.Img);
            }
            catch (Exception)
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\Img\\124444444.png");
            }
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            Item us = (Item)bindingSource1.Current;
            if (us.SCatID == 0)
            {
                bindingSource1.RemoveCurrent();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (txtItem.Text == "")
                    { MessageBox.Show("Please Provide Party Type", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    else
                    {
                        Item obj = (Item)bindingSource1.Current;

                        var Currentobj = list.Where(x => x.IName == txtItem.Text.Trim()).FirstOrDefault();
                        if (obj.SCatID == 0)
                        {
                            if (Currentobj != null)
                            {
                                MessageBox.Show("Prod Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            bool isCodeExist = list.Any(record => record.IName == obj.IName && record.SCatID != obj.SCatID);
                            if (isCodeExist)
                            {
                                MessageBox.Show("Prod Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        string path = Application.StartupPath;
                        // string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                        string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);

                        string fileNameOnly = Path.GetFileNameWithoutExtension(filename);
                        string extension = Path.GetExtension(filename);

                        //string newFullPath = path + "\\Img\\" + filename;
                        //string paths = Path.GetDirectoryName(newFullPath);

                        if (filename == null)
                        {
                            MessageBox.Show("Please select a valid image.");
                            return;
                        }
                        else
                        {
                            try
                            {
                                if (!File.Exists(path + "\\Img\\" + filename))
                                {
                                    //System.IO.File.Copy(openFileDialog1.FileName, path + "\\Img\\" + filename);
                                    //Utillityfunctions.SaveJpeg(path + "\\Img\\" + filename, new Bitmap(openFileDialog1.FileName), 50);
                                }
                            }
                            catch (Exception ex)
                            {
                                //if (File.Exists(path + "\\Img\\" + filename))
                                //{
                                //    Random _rdm = new Random();
                                //    var random = _rdm.Next(1000, 9999);
                                //    string tempFileName = string.Format("{0}_{1}", fileNameOnly, random.ToString());
                                //    filename = Path.Combine(tempFileName + extension);
                                //}
                                var ds = "";
                            }
                        }

                        if (obj.IID == 0)
                        {
                            obj.IName = txtItem.Text.Trim();
                            obj.SCatID = Convert.ToInt16(cmbxCat.SelectedValue);
                            obj.SalesPrice = Convert.ToDouble(txtRate.Text);
                            obj.RetailPOne = Convert.ToDecimal(metroTextBox1.Text == "" ? "0" : metroTextBox1.Text);
                            obj.RetailPTwo = Convert.ToDecimal(metroTextBox2.Text == "" ? "0" : metroTextBox2.Text);
                            obj.RetailPThree = Convert.ToDecimal(metroTextBox3.Text == "" ? "0" : metroTextBox3.Text);
                            obj.Img = Utillityfunctions.ToBase64(openFileDialog1.FileName, path + "\\Img\\" + filename);
                            obj.isDeleted = true;
                            obj.BarCode_ID = filename;
                            obj.RetailPrice = 0;
                            db.Items.Add(obj);
                        }
                        else
                        {
                            var result = db.Items.SingleOrDefault(b => b.IID == obj.IID);
                            if (result != null)
                            {
                                result.IName = txtItem.Text.Trim();
                                result.SCatID = Convert.ToInt16(cmbxCat.SelectedValue);
                                result.isDeleted = chkIsActive.Checked;
                                result.SalesPrice = Convert.ToDouble(txtRate.Text);
                                result.RetailPOne = Convert.ToDecimal(metroTextBox1.Text == "" ? "0" : metroTextBox1.Text);
                                result.RetailPTwo = Convert.ToDecimal(metroTextBox2.Text == "" ? "0" : metroTextBox2.Text);
                                result.RetailPThree = Convert.ToDecimal(metroTextBox3.Text == "" ? "0" : metroTextBox3.Text);
                                result.Img = Utillityfunctions.ToBase64(openFileDialog1.FileName, path + "\\Img\\" + filename);
                                result.BarCode_ID = filename;//.Replace("openFileDialog1", label10.Text);
                                db.Entry(result).State = EntityState.Modified;
                                //result.BarCode_ID = filename;
                            }
                        }
                        db.SaveChanges();
                        pnlMain.Hide();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void catDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void txtCredit_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label10.Text = openFileDialog1.FileName;
                        pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
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

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { ProdsDataGridView.DataSource = list; }
                else
                {
                    ProdsDataGridView.DataSource = list.FindAll(x => x.IName.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ProdsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
