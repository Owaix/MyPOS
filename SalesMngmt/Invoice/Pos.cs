using Lib.Entity;
using Lib.Model;
using Lib.Utilities;
using MetroFramework.Controls;
using Microsoft.Reporting.WinForms;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class Pos : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        bool takeAway = false;
        bool HomeDelivery = false;
        bool Express = false;
        bool DineIn = true;
        AspNetUser UserObj = null;
        String CurrentBlock = "";
        public Boolean isMax = true;

        public Pos(AspNetUser _usr)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            UserObj = _usr;
        }

        private void Pos_Load(object sender, EventArgs e)
        {
            if (isMax)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            lblBusinessName.Text = ConfigurationManager.AppSettings["BusinessName"];
            pnlTab2.Visible = false;
            generateCategory();
            pnlTotal.Hide();
            panel3.Hide();
            panel7.Hide();
            panel8.Hide();
            label35.Text = UserObj.Email;
            var list = new List<COA_M>();
            var waiter = db.tbl_Employee.ToList();
            //var Customers = db.Customers.ToList();
            list.Add(new COA_M { CAC_Code = 1, CATitle = "%" });
            list.Add(new COA_M { CAC_Code = 2, CATitle = "Rs" });
            FillCombo<COA_M>(cmbxDis1, list, "CATitle", "CAC_Code");
            FillCombo<COA_M>(cmbxGst1, list, "CATitle", "CAC_Code");
            FillCombo<tbl_Employee>(cmbxWaiter, waiter, "EmployeName", "ID");
            //FillCombo<Customer>(cmbxCus, Customers, "CusName", "CID", -1);
            FillCombo<Dict>(ddlpizzaSize, GetAllSize(), "Value", "Key", 0);
            FillCombo<Dict>(ddlpizzaFlavout, GetAllFlavor(), "Value", "Key");
            FillCombo<Dict>(comboBox1, GetPaymentType(), "Value", "Key");
            FillCombo<Dict>(comboBox2, GetPaymentType(), "Value", "Key");
            BindOrders();
            if (lblTblID.Text == "0")
            {
                btnChangeTable.Enabled = false;
            }
            else
            {
                btnChangeTable.Enabled = true;
            }
        }

        private void BindOrders()
        {
            var orders = (from order in db.tbl_Order
                          select new Orders
                          {
                              OrderId = order.OrderId,
                              OrderNo = order.OrderNo,
                          }).OrderByDescending(x => x.OrderId).Take(20).ToList();

            FillCombo<Orders>(cmbxOrders, orders, "OrderNo", "OrderId");
        }

        private IEnumerable<Dict> Getsize()
        {
            List<Dict> dict = new List<Dict>();
            dict.Add(new Dict { key = 1, Value = "Half" });
            dict.Add(new Dict { key = 2, Value = "Full" });
            return dict;
        }

        private IEnumerable<Dict> GetPaymentType()
        {
            List<Dict> dict = new List<Dict>();
            dict.Add(new Dict { key = 1, Value = "Cash" });
            dict.Add(new Dict { key = 2, Value = "Card" });
            return dict;
        }

        private IEnumerable<Dict> GetAllFlavor()
        {
            List<Dict> dict = new List<Dict>();
            dict.Add(new Dict { key = 1, Value = "Tikka" });
            dict.Add(new Dict { key = 2, Value = "Fajita" });
            dict.Add(new Dict { key = 3, Value = "Cheese Lover" });
            dict.Add(new Dict { key = 4, Value = "Peproni" });
            return dict;
        }

        private List<Dict> GetAllSize()
        {
            List<Dict> dict = new List<Dict>();
            dict.Add(new Dict { key = 0, Value = "" });
            dict.Add(new Dict { key = 1, Value = "Small" });
            dict.Add(new Dict { key = 2, Value = "Medium" });
            dict.Add(new Dict { key = 3, Value = "Large" });
            dict.Add(new Dict { key = 4, Value = "Jumbo" });
            return dict;
        }

        private void generateCategory()
        {
            var categoryList = db.Items_Cat.ToList();
            int locY = 9; // 73
            int CategoryLen = categoryList.Count;
            int Height = 67;
            int R = 20;
            int G = 40;
            int B = 100;

            for (int i = 0; i < CategoryLen; i++)
            {
                categoryList[i].img = String.IsNullOrEmpty(categoryList[i].img) ? "cartoon-roast-chicken_119631-190.jpg" : categoryList[i].img;
                string customPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Img\" + categoryList[i].img;
                if (!File.Exists(customPath))
                {
                    customPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Img\cartoon-roast-chicken_119631-190.jpg";
                }
                var btnItems = new MetroButton();
                var btnCat = new MetroButton();
                //btnCat.ActiveControl = null;
                //btnCat.BackColor = System.Drawing.Color.DeepSkyBlue;
                //btnCat.ForeColor = System.Drawing.Color.Black;
                //btnCat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(R)))), ((int)(((byte)(G)))), ((int)(((byte)(B)))));
                btnCat.BackgroundImage = Image.FromFile(customPath);
                btnCat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                btnCat.Location = new System.Drawing.Point(9, locY);
                btnCat.Name = categoryList[i].CatID.ToString();
                btnCat.Size = new System.Drawing.Size(122, Height);
                btnCat.TabIndex = 8;
                btnCat.Text = "";
                btnCat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                //btnCat.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
                //btnCat.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
                btnCat.UseCustomBackColor = true;
                btnCat.UseSelectable = true;
                btnCat.Click += new EventHandler(MetroTile_Click);
                panel5.Controls.Add(btnCat);
                R += 20;
                G += 25;
                B += 30;

                var lblTitle = new Label();
                lblTitle.AutoSize = true;
                lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblTitle.Location = new System.Drawing.Point(9, locY + 69);
                lblTitle.Name = "label23";
                lblTitle.Size = new System.Drawing.Size(129, 22);
                lblTitle.TabIndex = 27;
                lblTitle.Text = categoryList[i].Cat;
                panel5.Controls.Add(lblTitle);
                locY += 93; //73

            }
        }

        internal void EditPOS(string v, String f, DataGridView grid, Label lblBa, int val = 0)
        {
            try
            {
                int selectedInd = grid.CurrentCell.RowIndex;
                if (selectedInd > -1)
                {
                    if (f == "qty")
                    {
                        grid.Rows[selectedInd].Cells[4].Value = v;
                    }
                    else if (f == "Disc")
                    {
                        if (val == 1)
                        {
                            var Amt = Convert.ToDecimal(grid.Rows[selectedInd].Cells[4].Value) * Convert.ToDecimal(grid.Rows[selectedInd].Cells[3].Value);
                            grid.Rows[selectedInd].Cells[5].Value = CalculateDis(1, Amt.ToString(), v);
                        }
                        else
                        {
                            grid.Rows[selectedInd].Cells[5].Value = v;
                        }
                    }
                    else
                    {
                        grid.Rows[selectedInd].Cells[3].Value = v;
                    }
                    grid.Rows[selectedInd].Cells[6].Value = (Convert.ToDecimal(grid.Rows[selectedInd].Cells[4].Value) * Convert.ToDecimal(grid.Rows[selectedInd].Cells[3].Value) - Convert.ToDecimal(grid.Rows[selectedInd].Cells[5].Value));
                    LoadBal(grid, lblBa);
                }
            }
            catch (InvalidOperationException ex)
            {
                //var exceptions = ExceptionExtensions.ToMessageAndCompleteStacktrace(ex);
                //Lib.Entity.Excption Excepti = new Lib.Entity.Excption();
                //Excepti.ExcptionName = exceptions;
                //db.Excptions.Add(Excepti);
                //db.SaveChanges();
                MessageBox.Show("Unable to remove selected row at this time");
            }
        }
        int CatId = 0;
        protected void MetroTile_Click(object sender, EventArgs e)
        {
            MetroButton button = sender as MetroButton;
            CatId = Convert.ToInt32(button.Name);
            var itemList = db.Items.Where(x => x.SCatID == CatId).ToList();
            GenerateItems(itemList, true);
        }

        private void GenerateItems(List<Item> itemList, bool bind)
        {
            // System.Drawing.Point(164, 82);
            // System.Drawing.Point(274, 82);
            // System.Drawing.Point(164, 167);
            panel5.Controls.OfType<MetroButton>().ToList().ForEach(x =>
            {
                if (x.TabIndex != 8)
                {
                    x.Dispose();
                }
            });

            panel5.Controls.OfType<Label>().ToList().ForEach(x =>
            {
                if (x.TabIndex == 14)
                {
                    x.Dispose();
                }
            });

            if (bind)
            {
                FillCombo<Item>(cmbxItems, itemList, "IName", "IID");
            }
            int locX = 164;
            int locY = 40;
            int itemLen = itemList.Count;
            int R = 200;
            int G = 100;
            int B = 10;


            double firstLoop = (double)itemLen / 4;
            double y = firstLoop - Math.Truncate(firstLoop);
            if (y > 0.0001)
            {
                firstLoop += Convert.ToInt32(y);
            }
            firstLoop = firstLoop == 0 ? 1 : firstLoop;
            int len = 0;
            for (int i = 0; i < firstLoop; i++)
            {
                locX = 164;
                for (int j = 0; j < 4; j++)
                {
                    try
                    {
                        if (len == itemLen)
                        {
                            break;
                        }
                        //this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
                        //this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        //string customPath = @"E:\ada\cartoon-roast-chicken_119631-190.jpg";
                        //if (itemList[len].BarCode_ID != "")
                        //{
                        itemList[len].BarCode_ID = String.IsNullOrEmpty(itemList[len].BarCode_ID) ? "32313.png" : itemList[len].BarCode_ID;
                        string customPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Img\" + itemList[len].BarCode_ID;
                        var btnItems = new MetroButton();
                        //if (!File.Exists(customPath))
                        //{
                        //    btnItems.BackgroundImage = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + @"\Img\" + "pizza-burger.jpg");
                        //}
                        //else
                        //{
                        //    btnItems.BackgroundImage = Image.FromFile(customPath);
                        //}
                        if (string.IsNullOrEmpty(itemList[len].Img))
                        {
                            btnItems.BackgroundImage = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath) + @"\Img\" + "pizza-burger.jpg");
                        }
                        else
                        {
                            btnItems.BackgroundImage = Utillityfunctions.LoadImage(itemList[len].Img);
                        }

                        btnItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        //btnCat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(R)))), ((int)(((byte)(G)))), ((int)(((byte)(B)))));
                        btnItems.Location = new System.Drawing.Point(locX, locY);
                        btnItems.Name = itemList[len].IName + "~" + itemList[len].IID.ToString() + "~" + itemList[len].RetailPrice;
                        btnItems.Size = new System.Drawing.Size(91, 64);
                        btnItems.TabIndex = 33;
                        btnItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                        btnItems.UseCustomBackColor = true;
                        btnItems.UseSelectable = true;
                        btnItems.Click += new EventHandler(MetroTile_Clickitem);
                        panel5.Controls.Add(btnItems);
                        G += 60;
                        B += 60;

                        var lblTitle = new Label();
                        lblTitle.AutoSize = true;
                        lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblTitle.Location = new System.Drawing.Point(locX + 7, locY + 65);
                        lblTitle.Name = "label23";
                        lblTitle.Size = new System.Drawing.Size(129, 22);
                        lblTitle.TabIndex = 14;
                        String Item = String.Empty;
                        var ItemArr = itemList[len].IName.Split(' ');
                        for (int x = 0; x < ItemArr.Length; x++)
                        {
                            if (x == 2)
                            {
                                Item += Environment.NewLine + ItemArr[x];
                            }
                            else
                            {
                                Item += ItemArr[x] + " ";
                            }
                        }
                        lblTitle.Text = Item + Environment.NewLine + "Rs : " + itemList[len].SalesPrice;
                        panel5.Controls.Add(lblTitle);
                        locX += 130;

                        len += 1;
                    }
                    catch (Exception ex)
                    {
                        //var exceptions = ExceptionExtensions.ToMessageAndCompleteStacktrace(ex);
                        //Lib.Entity.Excption Excepti = new Lib.Entity.Excption();
                        //Excepti.ExcptionName = exceptions;
                        //db.Excptions.Add(Excepti);
                        //db.SaveChanges();
                        MessageBox.Show(ex.Message);
                        //    break;
                    }
                }
                locY += 120;
            }
        }
        String btnName = String.Empty;        
        protected void MetroTile_Clickitem(object sender, EventArgs e)
        {

            if (DineIn == true && lblTblID.Text == "0")
            {
                var result = MessageBox.Show("Table Is Not Selected In Dine In", "Choose Table First", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if (this.WindowState == FormWindowState.Maximized)
                        this.isMax = true;
                    else
                        this.isMax = false;

                    TablsList table = new TablsList(0, "new", lblTblID.Text, UserObj);
                    table.isMax = isMax;
                    this.Hide();
                    table.Show();
                    return;
                }
                else
                {
                    return;
                }
            }

            MetroButton button = sender as MetroButton;
            var btn = button.Name.Split('~');
            var text = btn[0];
            var name = btn[1];
            // int fullPlate = Convert.ToInt16(btn[2]);
            //if (text.ToLower().Contains("pizza"))
            //{
            //    btnName = name;
            //    ShowDialogs();
            //    return;
            //}
            //else if (fullPlate != 0)
            //{
            //    btnName = name + "~" + fullPlate.ToString();
            //    panel3.Show();
            //    return;
            //}
            int IID = Convert.ToInt32(name);
            AddSubQty(IID, 1, "inc", "", "0");

        }

        private void ShowDialogs()
        {
            ddlpizzaSize.SelectedValue = 1;
            ddlpizzaFlavout.SelectedValue = 0;
            panel7.Show();
        }
        private void AddSubQty(int IID, int inc, string Flag, String Pizza, double? price)
        {
            bool isAdd = true;
            var item = db.Items.Where(x => x.IID == IID).FirstOrDefault();
            if (item != null)
            {
                price = price == 0 ? item.SalesPrice : price;
                decimal Discount = 0;
                foreach (DataGridViewRow row in gridItems.Rows)
                {
                    //if (row.Cells[1].Value != null && Pizza == "")
                    //{
                    var itemID = Convert.ToInt32(row.Cells[1].Value.DefaultZero());
                    var SP = Convert.ToInt32(row.Cells[3].Value.DefaultZero());
                    if (IID == itemID && price == SP)
                    {
                        if (row.Cells[4].Value.ToString() == "1" && Flag == "dec")
                        {
                            gridItems.Rows.RemoveAt(row.Index);
                        }
                        else
                        {
                            row.Cells[0].Value = item.SCatID;
                            row.Cells[1].Value = item.IID;
                            row.Cells[2].Value = item.IName;
                            row.Cells[3].Value = price;
                            row.Cells[4].Value = Convert.ToDouble(row.Cells[4].Value) + inc;
                            Discount = Convert.ToDecimal(CalculateDis(1, (Convert.ToDecimal(price) * Convert.ToDecimal(row.Cells[4].Value)).ToString(), item.DisP.ToString().Replace(".00", ""))) + item.DisR ?? 0;
                            row.Cells[5].Value = Discount;
                            row.Cells[6].Value = (Convert.ToDecimal(price) * Convert.ToDecimal(row.Cells[4].Value)) - Discount;
                            row.Cells[7].Value = Pizza;
                        }
                        isAdd = false;
                        //}
                    }
                }
                if (isAdd == true)
                {
                    Discount = Convert.ToDecimal(CalculateDis(1, price.ToString(), item.DisP.ToString().Replace(".00", ""))) + item.DisR ?? 0;
                    this.gridItems.Rows.Add(item.SCatID, item.IID, item.IName, price, 1, Discount, Convert.ToDecimal(price) - Discount, Pizza);
                }
                LoadBal();
            }
        }
        private void AddSubQty(int IID, int inc, string Flag, String Pizza, String Size)
        {
            bool isAdd = true;
            var item = db.Items.Where(x => x.IID == IID).FirstOrDefault();
            var SalesPrice = item.SalesPrice;
            if (Size != "0")
            {
                if (Size == "1")
                {
                    SalesPrice = item.SalesPrice;
                }
                else if (Size == "2")
                {
                    SalesPrice = Convert.ToDouble(item.RetailPOne);
                }
                else if (Size == "3")
                {
                    SalesPrice = Convert.ToDouble(item.RetailPTwo);
                }
                else if (Size == "4")
                {
                    SalesPrice = Convert.ToDouble(item.RetailPThree);
                }
            }
            if (item != null)
            {
                decimal Discount = 0;
                foreach (DataGridViewRow row in gridItems.Rows)
                {
                    if (row.Cells[1].Value != null && Pizza == "")
                    {
                        var itemID = Convert.ToInt32(row.Cells[1].Value.ToString() == "" ? "0" : row.Cells[1].Value);
                        if (IID == itemID)
                        {
                            if (row.Cells[4].Value.ToString() == "1" && Flag == "dec")
                            {
                                gridItems.Rows.RemoveAt(row.Index);
                            }
                            else
                            {
                                row.Cells[0].Value = item.SCatID;
                                row.Cells[1].Value = item.IID;
                                row.Cells[2].Value = item.IName;
                                row.Cells[3].Value = SalesPrice;
                                row.Cells[4].Value = Convert.ToDouble(row.Cells[4].Value) + inc;
                                Discount = Convert.ToDecimal(CalculateDis(1, (Convert.ToDecimal(row.Cells[3].Value) * Convert.ToDecimal(row.Cells[4].Value)).ToString(), item.DisP.ToString().Replace(".00", ""))) + item.DisR ?? 0;
                                row.Cells[5].Value = Discount;
                                row.Cells[6].Value = (Convert.ToDecimal(row.Cells[3].Value) * Convert.ToDecimal(row.Cells[4].Value)) - Discount;
                                row.Cells[7].Value = Pizza;
                            }
                            isAdd = false;
                        }
                    }
                }
                if (isAdd == true)
                {
                    Discount = Convert.ToDecimal(CalculateDis(1, SalesPrice.ToString(), item.DisP.ToString().Replace(".00", ""))) + item.DisR ?? 0;
                    this.gridItems.Rows.Add(item.SCatID, item.IID, item.IName, SalesPrice, 1, Discount, Convert.ToDecimal(SalesPrice) - Discount, Pizza);
                }
                LoadBal();
            }
        }

        private void LoadBal()
        {
            Decimal Amount = 0;
            foreach (DataGridViewRow row in gridItems.Rows)
            {
                var SP = Convert.ToDecimal(row.Cells[3].Value);
                var Qty = Convert.ToDecimal(row.Cells[4].Value);
                var Disc = Convert.ToDecimal(row.Cells[5].Value);
                Amount += (SP * Qty) - Disc;
            }
            lblBal.Text = Amount.ToString();
        }
        private void LoadBal(DataGridView grid, Label lblBa)
        {
            Decimal Amount = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {
                var SP = Convert.ToDecimal(row.Cells[3].Value);
                var Qty = Convert.ToDecimal(row.Cells[4].Value);
                var Disc = Convert.ToDecimal(row.Cells[5].Value);
                Amount += (SP * Qty) - Disc;
            }
            lblBa.Text = Amount.ToString();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (gridItems.SelectedRows.Count > 0)
            {
                var itemID = Convert.ToInt32(gridItems.SelectedRows[0].Cells[1].Value);
                AddSubQty(itemID, 1, "inc", "", "0");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gridItems.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Items In Grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DineIn == true)
            {
                //progressBar1.Minimum = 0;
                //progressBar1.Maximum = 100;
                //progressBar1.Value = 10;
                //panel9.Visible = true;

                savePrint("print");
            }
            else
            {
                txtGrandtotal1.Text = lblBal.Text;
                btnSave.Enabled = false;
                pnlTotal.Show();
                if (Express == true)
                {
                    lblName.Text = "Date";
                    dtOrderDate.Visible = true;
                    txtName.Visible = false;
                    txtMob.Text = "FoodPanda";
                }
                else
                {
                    lblName.Text = "Name";
                    dtOrderDate.Visible = false;
                    txtName.Visible = true;
                    txtMob.Text = "";
                }

                if (HomeDelivery == true)
                {
                    txtMob.Focus();
                }
                else
                {
                    cmbxDis1.Focus();
                }
            }
        }

        private void savePrint(string type)
        {
            Orders PrintOrderList = new Orders();
            tbl_Order order = null;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    order = new tbl_Order();
                    if (lblTableID.Text == "00" && DineIn == true)
                    {
                        MessageBox.Show("Table is Not Selected");
                        return;
                    }
                    int orderID = Convert.ToInt32(lblDineInID.Text);

                    if (DineIn == true)
                    {
                        if (orderID == 0)
                        {
                            order = new tbl_Order();
                            order.Amount = 0;
                            order.OrderNo = "D" + GenerateRandomNo();
                            order.OrderDate = DateTime.Now;
                            order.KOTID = "1";
                        }
                        else
                        {
                            order = db.tbl_Order.Where(x => x.OrderId == orderID && x.OrderType == "DineIn").FirstOrDefault();
                        }
                        order.TblID = db.tbl_Table.Where(x => x.TableName == lblTableID.Text).FirstOrDefault().ID;
                        order.isComplete = false;
                        order.WaiterID = Convert.ToInt32(cmbxWaiter.SelectedValue);
                        order.Discount = Convert.ToDecimal(textBox7.Text.DefaultZero());
                        order.GST = Convert.ToDecimal(txtGstTotl.Text.DefaultZero());
                        order.OrderType = "DineIn";
                    }
                    else if (takeAway == true)
                    {
                        order.OrderType = "TakeAway";
                        order.OrderNo = "T" + GenerateRandomNo();
                        order.TblID = Convert.ToInt16(label30.Text);
                        order.isComplete = true;
                        order.Amount = Convert.ToDecimal(txtGrandtotal1.Text.DefaultZero());
                        order.Discount = Convert.ToDecimal(textBoex.Text.DefaultZero());
                        order.GST = Convert.ToDecimal(txtGstTotl2.Text.DefaultZero());
                        order.KOTID = comboBox1.SelectedValue.ToString();
                        order.OrderDate = DateTime.Now;
                    }
                    else if (HomeDelivery == true)
                    {
                        order.OrderType = "HomeDelivery";
                        order.OrderNo = "H" + GenerateRandomNo();
                        order.TblID = 0;
                        order.isComplete = true;
                        order.Amount = Convert.ToDecimal(txtGrandtotal1.Text.DefaultZero());
                        order.Discount = Convert.ToDecimal(textBoex.Text.DefaultZero());
                        order.GST = Convert.ToDecimal(txtGstTotl2.Text.DefaultZero());
                        order.KOTID = txtCards2.Text;
                        order.OrderDate = DateTime.Now;
                    }
                    else if (Express == true)
                    {
                        order.OrderType = txtMob.Text;
                        order.OrderNo = "E" + GenerateRandomNo();
                        order.TblID = Convert.ToInt16(label30.Text);
                        order.isComplete = true;
                        order.Amount = Convert.ToDecimal(txtGrandtotal1.Text.DefaultZero());
                        order.Discount = Convert.ToDecimal(textBoex.Text.DefaultZero());
                        order.GST = Convert.ToDecimal(txtGstTotl2.Text.DefaultZero());
                        order.KOTID = txtCards2.Text;
                        order.OrderDate = dtOrderDate.Value;
                    }
                    order.userID = Convert.ToInt16(UserObj.Id);
                    if (orderID == 0 && DineIn == true)
                    {
                        db.tbl_Order.Add(order);
                    }
                    if (DineIn == false)
                    {
                        db.tbl_Order.Add(order);
                    }
                    db.SaveChanges();

                    if (DineIn == true)
                    {
                        lblDineInID.Text = order.OrderId.ToString();
                    }
                    label4.Text = order.OrderNo.ToString();

                    tbl_KOT kot = new tbl_KOT();
                    kot.iscomplete = true;
                    kot.OrderID = order.OrderId.ToString();
                    kot.KotNo = "KOT-" + order.OrderNo;
                    db.tbl_KOT.Add(kot);
                    db.SaveChanges();

                    lblKOT.Text = kot.Id.ToString();
                    Decimal Amount = 0;
                    foreach (DataGridViewRow row in gridItems.Rows)
                    {
                        tbl_OrderDetails details = new tbl_OrderDetails();
                        details.OrderId = order.OrderId;
                        details.CatID = Convert.ToInt16(row.Cells[0].Value.DefaultZero());
                        details.itemID = Convert.ToInt16(row.Cells[1].Value.DefaultZero());
                        details.itemDtl = row.Cells[7].Value.ToString();
                        details.Rate = Convert.ToInt16(row.Cells[3].Value.DefaultZero());
                        details.Qty = Convert.ToInt16(row.Cells[4].Value.DefaultZero());
                        details.KOTID = kot.Id.ToString();
                        Amount += (details.Rate * details.Qty) ?? 0;
                        db.tbl_OrderDetails.Add(details);
                        db.SaveChanges();
                        details.itemName = row.Cells[2].Value.ToString();
                        PrintOrderList.OrderDetailsModel.Add(details);

                    }
                    var orders = db.tbl_Order.Where(x => x.OrderId == order.OrderId).FirstOrDefault();
                    if (orders != null && DineIn == true)
                    {
                        order.Amount += Amount;
                        db.Entry(order).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    PrintOrderList.Order = order;
                    if (type == "print")
                    {
                        string OrderType = order.OrderType;
                        switch (OrderType)
                        {
                            case "DineIn":
                                SilentPrint(order.OrderId, false);
                                break;
                            case "TakeAway":
                                SilentPrint(PrintOrderList, true);
                                SilentPrint(PrintOrderList, false);
                                break;
                            case "HomeDelivery":
                                SilentPrint(order.OrderId, true);
                                SilentPrint(order.OrderId, false);
                                break;
                            default:
                                var dsa = "dsad";
                                break;
                        }
                    }
                    pnlTotal.Hide();
                    transaction.Commit();
                    clear();

                }
                catch (DbEntityValidationException ex)
                {
                    transaction.Rollback();
                    //string msg = "";
                    //foreach (var eve in ex.EntityValidationErrors)
                    //{
                    //    msg = "Entity of type \"{0}\" in state \"{1}\" has the following validation errors: " + eve.Entry.Entity.GetType().Name + eve.Entry.State;
                    //    foreach (var ve in eve.ValidationErrors)
                    //    {
                    //        msg += "- Property: \"{0}\", Error: \"{1}\" " + ve.PropertyName + ve.ErrorMessage;
                    //    }
                    //}
                    //Lib.Entity.Excption Excepti = new Lib.Entity.Excption();
                    //Excepti.ExcptionName = msg;
                    //db.Excptions.Add(Excepti);
                    //db.SaveChanges();
                }
            }
        }
        public String GenerateRandomNo()
        {
            String OrdrNoString = "0001";
            String OrderNo = "0";
            //var order = db.tbl_Order.ToList().OrderByDescending(q => q.OrderDate).FirstOrDefault();
            //var order = db.tbl_Order.OrderByDescending(p => p.OrderDate).FirstOrDefault();
            SqlCommand cmd = new SqlCommand("GenerateLastNo", SqlHelper.DefaultSqlConnection);
            var rows = SqlHelper.ExecuteDataset(cmd).Tables[0].Rows;
            if (rows.Count > 0)
            {
                var row = rows[0];
                OrderNo = row["OrdrID"].ToString();
            }

            int NewOrdrN = Convert.ToInt32(OrderNo) + 1;
            OrdrNoString = NewOrdrN.ToString();
            if (OrdrNoString.Count() == 1)
            {
                OrdrNoString = "000" + OrdrNoString;
            }
            else if (OrdrNoString.Count() == 2)
            {
                OrdrNoString = "00" + OrdrNoString;
            }
            else if (OrdrNoString.Count() == 3)
            {
                OrdrNoString = "0" + OrdrNoString;
            }

            return OrdrNoString;
        }

        private void clear()
        {
            // cmbxItems.SelectedIndex = -1;
            //  txtBatch.Text = "0";
            lblBal.Text = "0.0";
            gridItems.DataSource = null;
            gridItems.Rows.Clear();
            //  txtNetAm.Text = "0";
            // txtTotalAm.Text = "0";
            txtGstAmt2.Text = "";
            textBox52.Text = "";
            textBox2.Text = "";
            textBox7.Text = "";
            txtGstAmt.Text = "";
            textBox5.Text = "";
            txtCards.Text = "";
            txtCards2.Text = "";
            txtGrandtotal.Text = "";
            txtGrandtotal1.Text = "";
            txtGstTotl2.Text = "";
            txtGstTotl.Text = "";
            textBoex.Text = "";
            pnlChk.Controls.OfType<RadioButton>().ToList().ForEach(btn => btn.Dispose());
            gridInv.DataSource = null;
            gridInv.Rows.Clear();
            txtGrandtotal.Text = "";
            panel7.Hide();
            panel3.Hide();
            pnlTotal.Hide();
            //lblOrderID.Text = "0"

            foreach (var control in pnlTotal.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                }
            }
        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            TablsList table = new TablsList(0, "new", lblTblID.Text, UserObj);
            this.Hide();
            table.Show();
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            TablsList table = new TablsList(0, "change", lblTblID.Text, UserObj);
            this.Hide();
            table.Show();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (gridItems.SelectedRows.Count > 0)
            {
                EditBox edit = new EditBox("rate", gridItems, lblBal, UserObj);
                edit.Show();
            }
            else
            {
                MessageBox.Show("Item is Not Selected");
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            if (gridItems.SelectedRows.Count > 0)
            {
                EditBox edit = new EditBox("qty", gridItems, lblBal, UserObj);
                edit.Show();
            }
            else
            {
                MessageBox.Show("Item is Not Selected");
            }
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            metroTile3.BackColor = System.Drawing.Color.Silver;
            metroTile1.BackColor = System.Drawing.Color.Gray;
            metroTile2.BackColor = System.Drawing.Color.Silver;
            metroTile4.BackColor = System.Drawing.Color.Silver;
            metroTile5.BackColor = System.Drawing.Color.Silver;
            pnlTab1.Visible = true;
            pnlTab2.Visible = false;
            takeAway = false;
            panel8.Visible = false;
            HomeDelivery = false;
            Express = false;
            DineIn = true;
            btnChangeTable.Enabled = true;
            btnTable.Enabled = true;
            //cmbxItems.Visible = true;
            //txtBarCode.Visible = true;
            //button2.Visible = true;
            //label34.Visible = true;
            txtAddress.Enabled = false;
            txtName.Enabled = false;
            txtMob.Enabled = false;
            CurrentBlock = "DineIn";
            label4.Text = "00";
            gridItems.Rows.Clear();

            if (lblTblID.Text == "0")
            {
                btnChangeTable.Enabled = false;
            }
            else
            {
                btnChangeTable.Enabled = true;
            }

            foreach (var control in panel4.Controls)
            {
                if (control is MetroButton)
                {
                    ((MetroButton)control).BackColor = System.Drawing.Color.DodgerBlue;
                }
            }
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            metroTile3.BackColor = System.Drawing.Color.Silver;
            metroTile1.BackColor = System.Drawing.Color.Silver;
            metroTile2.BackColor = System.Drawing.Color.Gray;
            metroTile4.BackColor = System.Drawing.Color.Silver;
            metroTile5.BackColor = System.Drawing.Color.Silver;

            pnlTab2.Visible = true;
            pnlTab1.Visible = true;
            panel8.Visible = true;
            var list = new List<COA_M>();
            list.Add(new COA_M { CAC_Code = 1, CATitle = "%" });
            list.Add(new COA_M { CAC_Code = 2, CATitle = "Rs" });
            FillCombo<COA_M>(cmbxDis, list, "CATitle", "CAC_Code");
            FillCombo<COA_M>(cmbxGst, list, "CATitle", "CAC_Code");
            cmbxItems.Visible = false;
            button2.Visible = false;
            txtAddress.Enabled = false;
            txtBarCode.Visible = false;
            label34.Visible = false;
            txtName.Enabled = false;
            txtMob.Enabled = false;
            CurrentBlock = "DineInbilling";
            label4.Text = "00";
            generateChk();
        }

        private void generateChk()
        {
            pnlChk.Controls.Clear();
            var TableList = db.tbl_Order.Where(x => x.isComplete == false).ToList();
            int locY = 43; // 73
            int TableLen = TableList.Count;

            for (int i = 0; i < TableLen; i++)
            {
                int ID = TableList[i].TblID ?? 0;
                var tableName = db.tbl_Table.Where(x => x.ID == ID).FirstOrDefault().TableName;
                var checkBox1 = new RadioButton();
                checkBox1.AutoSize = true;
                checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                checkBox1.Location = new System.Drawing.Point(13, locY);
                checkBox1.Name = ID.ToString();
                checkBox1.Size = new System.Drawing.Size(86, 19);
                checkBox1.TabIndex = 13;
                checkBox1.Text = tableName;
                checkBox1.UseVisualStyleBackColor = true;
                checkBox1.CheckedChanged += new System.EventHandler(CheckBoxCheckedChanged);
                pnlChk.Controls.Add(checkBox1);
                locY += 21;
            }
        }

        private void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            RadioButton checkbox = sender as RadioButton;
            gridInv.DataSource = null;
            gridInv.Rows.Clear();
            foreach (var control in pnlChk.Controls)
            {
                if (control is RadioButton)
                {
                    if (((RadioButton)control).Checked)
                    {
                        btnBillSave.Enabled = true;
                        break;
                    }
                }
            }

            var tblName = Convert.ToInt32(checkbox.Name);
            List<Orders> orders = (from order in db.tbl_Order
                                   join kot in db.tbl_KOT on order.OrderId.ToString() equals kot.OrderID
                                   join OrderDetails in db.tbl_OrderDetails on kot.Id.ToString() equals OrderDetails.KOTID
                                   join Items in db.Items on OrderDetails.itemID equals Items.IID
                                   where order.TblID == tblName && order.isComplete == false
                                   select new Orders
                                   {
                                       OrderId = order.OrderId,
                                       OrderDetailId = OrderDetails.Id,
                                       OrderNo = order.OrderNo,
                                       TblID = order.TblID,
                                       item = Items.IName,
                                       ItemID = Items.IID,
                                       Qty = OrderDetails.Qty,
                                       Rate = OrderDetails.Rate,
                                       Amount = OrderDetails.Qty * OrderDetails.Rate,
                                       Discount = order.Discount,
                                       isComplete = order.isComplete,
                                       Total = order.Amount
                                   }).ToList();


            gridInv.DataSource = null;

            orders = (from x in orders
                      group x by x.item into y
                      select new Orders
                      {
                          item = y.Key,
                          OrderDetailId = y.First().OrderDetailId,
                          OrderId = y.First().OrderId,
                          OrderNo = y.First().OrderNo,
                          TblID = y.First().TblID,
                          ItemID = y.First().ItemID,
                          Rate = y.First().Rate,
                          Amount = y.Sum(z => z.Qty) * y.First().Rate,
                          Discount = y.First().Discount,
                          Total = y.Sum(z => z.Amount),
                          Qty = y.Sum(z => z.Qty)
                      }
              ).ToList();

            if (orders.Count > 0)
            {
                var listBinding = new BindingList<Orders>(orders);
                gridInv.DataSource = listBinding;
                txtGrandtotal.Text = (orders.Sum(x => x.Total) - orders.Sum(x => x.Discount)).ToString();
                lblBillNo.Text = orders[0].OrderNo.ToString();

                gridInv.Columns["OrderId"].Visible = false;
                gridInv.Columns["OrderNo"].Visible = false;
                gridInv.Columns["OrderDetailId"].Visible = false;
                gridInv.Columns["TblID"].Visible = false;
                gridInv.Columns["KOTID"].Visible = false;
                gridInv.Columns["OrderDate"].Visible = false;
                gridInv.Columns["isComplete"].Visible = false;
                gridInv.Columns["Tbl"].Visible = false;
                gridInv.Columns["WaiterID"].Visible = false;
                gridInv.Columns["booker"].Visible = false;
                gridInv.Columns["ItemID"].Visible = false;
                gridInv.Columns["CatID"].Visible = false;
                gridInv.Columns["RowHeight"].Visible = false;
                gridInv.Columns["Rows"].Visible = false;
                gridInv.Columns["CashCard"].Visible = false;
                gridInv.Columns["Gst"].Visible = false;
                gridInv.Columns["OrderType"].Visible = false;
                gridInv.Columns["ItemDetails"].Visible = false;
                gridInv.Columns["Address"].Visible = false;

            }
            else
            {
                MessageBox.Show("No Item Found");
            }
        }

        public bool TableSelected(string TblClicked, String tblStat, String TblCurrent)
        {
            var tableID = Convert.ToInt32(TblClicked);
            var currTbl = Convert.ToInt32(TblCurrent);

            var Order = db.tbl_Order.Where(x => x.TblID == tableID && x.isComplete == false).FirstOrDefault();
            if (Order != null && tableID != currTbl && tblStat != "new")
            {
                MessageBox.Show("Reserved Table Cannot Be Selected !", "Table Cannot Changed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (tblStat == "change" && Order == null)
            {
                var Orders = db.tbl_Order.Where(x => x.TblID == currTbl && x.isComplete == false).FirstOrDefault();
                if (Orders != null)
                {
                    Orders.TblID = tableID;
                    db.Entry(Orders).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            var tables = db.tbl_Table.Where(x => x.ID == tableID).FirstOrDefault();
            lblTableID.Text = tables.TableName;
            lblTblID.Text = TblClicked;

            if (Order != null)
            {
                lblDineInID.Text = Order.OrderId.ToString();
                label4.Text = Order.OrderNo.ToString();
                cmbxWaiter.SelectedValue = Order.WaiterID;
            }
            if (lblTblID.Text == "0")
            {
                btnChangeTable.Enabled = false;
            }
            else
            {
                btnChangeTable.Enabled = true;
            }
            return true;
        }

        private Control _focusedControl;
        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            _focusedControl = (Control)sender;
        }
        private void metroButton19_Click(object sender, EventArgs e)
        {
            if (_focusedControl != null)
            {
                Button btn = sender as Button;
                if (btn.Text == "." && !_focusedControl.Text.Contains('.'))
                {
                    _focusedControl.Text += btn.Text;
                }
                if (btn.Text != ".")
                {
                    _focusedControl.Text += btn.Text;
                }
            }
        }
        private void metroTile4_Click(object sender, EventArgs e)
        {
            metroTile3.BackColor = System.Drawing.Color.Silver;
            metroTile1.BackColor = System.Drawing.Color.Silver;
            metroTile2.BackColor = System.Drawing.Color.Silver;
            metroTile4.BackColor = System.Drawing.Color.Gray;
            metroTile5.BackColor = System.Drawing.Color.Silver;
            CurrentBlock = "takeAway";
            pnlTab2.Visible = false;
            takeAway = true;
            panel8.Visible = false;
            HomeDelivery = false;
            Express = false;
            DineIn = false;
            btnChangeTable.Enabled = false;
            btnTable.Enabled = false;
            //label34.Visible = true;
            //cmbxItems.Visible = true;
            //txtBarCode.Visible = true;
            //button2.Visible = true;
            panel5.Controls.OfType<MetroButton>().ToList().ForEach(x => { if (x.TabIndex != 8) { x.Dispose(); } });
            panel5.Controls.OfType<Label>().ToList().ForEach(x => { if (x.TabIndex == 14) { x.Dispose(); } });
            pnlTab1.Visible = true;
            txtAddress.Enabled = false;
            txtName.Enabled = false;
            txtMob.Enabled = false;
            label23.Text = "Mobile #";
            label4.Text = "00";
            txtMob.Text = "";
            gridItems.Rows.Clear();

            foreach (var control in panel4.Controls)
            {
                if (control is MetroButton)
                {
                    ((MetroButton)control).BackColor = System.Drawing.Color.LimeGreen;
                }
            }
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            metroTile3.BackColor = System.Drawing.Color.Gray;
            metroTile1.BackColor = System.Drawing.Color.Silver;
            metroTile2.BackColor = System.Drawing.Color.Silver;
            metroTile4.BackColor = System.Drawing.Color.Silver;
            metroTile5.BackColor = System.Drawing.Color.Silver;
            CurrentBlock = "Express";
            pnlTab2.Visible = false;
            takeAway = false;
            HomeDelivery = false;
            panel8.Visible = false;
            Express = true;
            DineIn = false;
            btnChangeTable.Enabled = false;
            btnTable.Enabled = false;
            //txtBarCode.Visible = true;
            //label34.Visible = true;
            //cmbxItems.Visible = true;
            //button2.Visible = true;
            //panel5.Controls.OfType<MetroButton>().ToList().ForEach(btn => btn.Dispose());
            panel5.Controls.OfType<MetroButton>().ToList().ForEach(x => { if (x.TabIndex != 8) { x.Dispose(); } });
            panel5.Controls.OfType<Label>().ToList().ForEach(x => { if (x.TabIndex == 14) { x.Dispose(); } });
            pnlTab1.Visible = true;
            txtAddress.Enabled = false;
            txtName.Enabled = false;
            txtMob.Enabled = true;
            label23.Text = "Vendor";
            label4.Text = "00";
            txtMob.Text = "FoodPanda";
            gridItems.Rows.Clear();

            foreach (var control in panel4.Controls)
            {
                if (control is MetroButton)
                {
                    ((MetroButton)control).BackColor = System.Drawing.Color.DarkKhaki;
                }
            }
        }

        private void metroTile5_Click(object sender, EventArgs e)
        {
            metroTile3.BackColor = System.Drawing.Color.Silver;
            metroTile1.BackColor = System.Drawing.Color.Silver;
            metroTile2.BackColor = System.Drawing.Color.Silver;
            metroTile4.BackColor = System.Drawing.Color.Silver;
            metroTile5.BackColor = System.Drawing.Color.Gray;
            CurrentBlock = "HomeDelivery";
            pnlTab2.Visible = false;
            takeAway = false;
            HomeDelivery = true;
            panel8.Visible = false;
            Express = false;
            DineIn = false;
            btnChangeTable.Enabled = false;
            btnTable.Enabled = false;
            //cmbxItems.Visible = true;
            //button2.Visible = true;
            //label34.Visible = true;
            //txtBarCode.Visible = true;
            panel5.Controls.OfType<MetroButton>().ToList().ForEach(x => { if (x.TabIndex != 8) { x.Dispose(); } });
            panel5.Controls.OfType<Label>().ToList().ForEach(x => { if (x.TabIndex == 14) { x.Dispose(); } });
            pnlTab1.Visible = true;
            txtAddress.Enabled = true;
            txtName.Enabled = true;
            txtMob.Enabled = true;
            txtMob.Focus();
            label4.Text = "00";
            label23.Text = "Mobile #";
            txtMob.Text = "";
            gridItems.Rows.Clear();

            foreach (var control in panel4.Controls)
            {
                if (control is MetroButton)
                {
                    ((MetroButton)control).BackColor = System.Drawing.Color.DarkOrchid;
                }
            }
        }

        private void metroButton9_Click_1(object sender, EventArgs e)
        {
            var customer = db.Customers.Where(x => x.Cell == txtMob.Text).FirstOrDefault();
            if (customer == null)
            {
                customer = new Customer();
                customer.Cell = txtMob.Text;
                customer.CusName = txtName.Text;
                customer.Add = txtAddress.Text;
                db.Customers.Add(customer);
            }
            else
            {
                customer.CusName = txtName.Text;
                customer.Add = txtAddress.Text;
                db.Entry(customer).State = EntityState.Modified;
            }
            db.SaveChanges();
            label30.Text = customer.CID.ToString();
            savePrint("print");
            pnlTotal.Hide();
            btnSave.Enabled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                if (pnlTab2.Visible)
                {
                    metroButton21_Click(null, null);
                }
                else
                {
                    if (btnSave.Enabled)
                    {
                        btnSave_Click(null, null);
                    }
                    else
                    {
                        metroButton9_Click_1(null, null);
                    }
                }
                return true;
            }
            if (keyData == (Keys.Enter) && txtBarCode.Text != "")
            {
                String id = txtBarCode.Text;
                var items = db.Items.Where(x => x.BarcodeNo == id).FirstOrDefault();
                if (items != null)
                {
                    AddSubQty(items.IID, 1, "inc", "", 0);
                    txtBarCode.Text = "";
                    txtBarCode.Focus();
                }
                else
                {
                    MessageBox.Show("Barcode Not Exists", "Barcode Not Exists");
                }
                return true;
            }
            if (keyData == (Keys.Control | Keys.P))
            {

            }
            if (keyData == (Keys.Control | Keys.Q))
            {

            }
            if (keyData == (Keys.Control | Keys.E))
            {

            }
            if (keyData == (Keys.Escape))
            {
                clear();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void textBo_Leave(object sender, EventArgs e)
        {
            textBoex.Text = CalculateDis(cmbxDis1.SelectedValue, txtGrandtotal1.Text, textBo.Text);
            txtNetAm.Text = NetAm().ToString();
            //txtCards2_Leave(null, null);
        }

        private void txtGstAmt2_Leave(object sender, EventArgs e)
        {
            txtGstTotl2.Text = CalculateDis(cmbxGst1.SelectedValue, txtGrandtotal1.Text, txtGstAmt2.Text);
            txtNetAm.Text = NetAm().ToString();
            //txtCards2_Leave(null, null);
        }

        private string CalculateDis(object type, string Total, string Amt)
        {
            var total = (Convert.ToDecimal(Total == "" ? "0" : Total));
            int typ = Convert.ToInt32(type);
            Amt = Amt.DefaultZero();
            if (typ == 1)
            {
                var perc = Convert.ToDecimal(Amt.DefaultZero());
                //total = total * Convert.ToDecimal("0." + perc.ToString());

                if (perc < 10)
                {
                    Amt = "0.0" + Amt;
                    total = total * Convert.ToDecimal(Amt);
                }
                else if (perc < 100 && perc > 9)
                {
                    Amt = "0." + Amt;
                    total = total * Convert.ToDecimal(Amt);
                }
                return String.Format("{0:0.00}", total);
            }
            else
            {
                return Amt == "" ? "0" : Amt;
            }
        }

        private void OnlyNum_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private decimal NetAm()
        {
            return Convert.ToDecimal(txtGrandtotal1.Text.DefaultZero())
             +
             ((Convert.ToDecimal(txtGstTotl2.Text.DefaultZero())
             + Convert.ToDecimal(txtDelivery.Text.DefaultZero())) -
             Convert.ToDecimal(textBoex.Text.DefaultZero())
             );
        }

        private void txtCards2_Leave(object sender, EventArgs e)
        {
            //if (comboBox2.SelectedValue.ToString() == "1")
            //{
            //    txtCards2.Text = txtNetAm.Text;
            //}
            textBox52.Text =
                (Convert.ToDecimal(txtCards2.Text.DefaultZero())
                - (Convert.ToDecimal(txtGrandtotal1.Text.DefaultZero())
                +
                ((Convert.ToDecimal(txtGstTotl2.Text.DefaultZero())
                + Convert.ToDecimal(txtDelivery.Text.DefaultZero())) -
                Convert.ToDecimal(textBoex.Text.DefaultZero())
                )
                )
                ).ToString();
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

        private void metroButton20_Click(object sender, EventArgs e)
        {
            pnlTotal.Visible = false;
            btnSave.Enabled = true;
            foreach (var control in pnlTotal.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                }
            }
        }

        public void SilentPrint(Orders Obj, bool type)
        {
            try
            {
                List<tbl_OrderDetails> list;
                ReportViewer reportViewer1 = new ReportViewer();

                //var order = db.tbl_Order.Where(x => x.OrderId == OrderId).FirstOrDefault();
                //if (type)
                //{
                //    list = db.tbl_OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
                //}
                //else
                //{
                //    list = db.tbl_OrderDetails.Where(x => x.OrderId == order.OrderId && x.KOTID == lblKOT.Text).ToList();
                //}
                var tbl = db.tbl_Table.Where(x => x.ID == Obj.TblID).FirstOrDefault();
                List<Orders> orderList = new List<Orders>();
                var order = Obj.Order;
                foreach (var item in Obj.OrderDetailsModel)
                {
                    Orders orders = new Orders();
                    orders.OrderId = order.OrderId;
                    orders.OrderDetailId = item.Id;
                    orders.KOTID = order.KOTID;
                    orders.OrderNo = order.OrderNo;
                    orders.OrderDate = order.OrderDate;
                    orders.isComplete = order.isComplete;
                    orders.Discount = order.Discount;
                    orders.Address = txtAddress.Text;
                    orders.Amount = order.Amount;
                    orders.ItemDetails = item.itemDtl;
                    orders.Total = item.Rate * item.Qty;
                    orders.TblID = order.TblID;
                    orders.Gst = order.GST;
                    orders.OrderType = order.OrderType;
                    orders.DeliveryCharges = Convert.ToDecimal(txtDelivery.Text.DefaultZero());
                    if (tbl != null)
                    {
                        orders.Tbl = tbl.TableName;
                    }
                    orders.WaiterID = order.WaiterID;
                    orders.item = item.itemName; //db.Items.Where(x => x.IID == item.itemID).FirstOrDefault().IName;
                    orders.booker = UserObj.UserName;
                    orders.CatID = item.CatID ?? 0;
                    orders.Cat = db.Items_Cat.Where(x => x.CatID == orders.CatID).FirstOrDefault().Cat;
                    orders.ItemID = item.itemID ?? 0;
                    orders.Qty = item.Qty;
                    orders.Rate = item.Rate;
                    if (DineIn == true)
                    {
                        orders.CashCard = Convert.ToDecimal(txtCards.Text.DefaultZero());
                    }
                    else
                    {
                        orders.CashCard = Convert.ToDecimal(txtCards2.Text.DefaultZero());
                    }

                    orderList.Add(orders);
                }

                //orderList.ForEach(x =>
                //{
                //    if (x.Cat.Contains("Bar B"))
                //    {
                //        x.CatID = 1;
                //    }
                //    else
                //    {
                //        x.CatID = 2;
                //    }
                //});

                Silent silent = new Silent();
                if (type)
                {
                    //Printer.setDef(ConfigurationManager.AppSettings["Thermal"].ToString());
                    silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.Recpt.rdlc");
                    //silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.Recpt2.rdlc");
                }
                else
                {
                    //Printer.setDef(ConfigurationManager.AppSettings["KOT"].ToString());
                    silent.Run(reportViewer1, orderList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw new Exception("Printer Is Not Connected or Connected On Wrong Usb Port . Please Change Usb Port and Try Again!");
            }
        }

        public void SilentPrint(int OrderId, bool type)
        {
            try
            {
                List<tbl_OrderDetails> list;
                ReportViewer reportViewer1 = new ReportViewer();

                var order = db.tbl_Order.Where(x => x.OrderId == OrderId).FirstOrDefault();
                if (type)
                {
                    list = db.tbl_OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
                }
                else
                {
                    list = db.tbl_OrderDetails.Where(x => x.OrderId == order.OrderId && x.KOTID == lblKOT.Text).ToList();
                }

                List<Orders> orderList = new List<Orders>();
                foreach (var item in list)
                {
                    Orders orders = new Orders();
                    orders.OrderId = order.OrderId;
                    orders.OrderDetailId = item.Id;
                    orders.KOTID = order.KOTID;
                    orders.OrderNo = order.OrderNo;
                    orders.OrderDate = order.OrderDate;
                    orders.isComplete = order.isComplete;
                    orders.Discount = order.Discount;
                    orders.Address = txtAddress.Text;
                    orders.Amount = order.Amount;
                    orders.ItemDetails = item.itemDtl;
                    orders.Total = item.Rate * item.Qty;
                    orders.TblID = order.TblID;
                    orders.Gst = order.GST;
                    orders.DeliveryCharges = Convert.ToDecimal(txtDelivery.Text.DefaultZero());
                    orders.OrderType = order.OrderType;
                    var tbl = db.tbl_Table.Where(x => x.ID == order.TblID).FirstOrDefault();
                    if (tbl != null)
                    {
                        orders.Tbl = tbl.TableName;
                    }
                    orders.WaiterID = order.WaiterID;
                    orders.item = db.Items.Where(x => x.IID == item.itemID).FirstOrDefault().IName;
                    orders.booker = UserObj.UserName;
                    orders.CatID = item.CatID ?? 0;
                    orders.Cat = db.Items_Cat.Where(x => x.CatID == orders.CatID).FirstOrDefault().Cat;
                    orders.ItemID = item.itemID ?? 0;
                    orders.Qty = item.Qty;
                    orders.Rate = item.Rate;
                    if (DineIn == true)
                    {
                        orders.CashCard = Convert.ToDecimal(txtCards.Text.DefaultZero());
                    }
                    else
                    {
                        orders.CashCard = Convert.ToDecimal(txtCards2.Text.DefaultZero());
                    }

                    orderList.Add(orders);
                }

                orderList.ForEach(x =>
                {
                    x.CatID = 1;
                    //if (x.Cat.Contains("Bar B"))
                    //{
                    //    x.CatID = 1;
                    //}
                    //else
                    //{
                    //    x.CatID = 2;
                    //}
                });

                Silent silent = new Silent();
                if (type)
                {
                    //Printer.setDef(ConfigurationManager.AppSettings["Thermal"].ToString());
                    silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.Recpt.rdlc");
                    //silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.Recpt2.rdlc");
                }
                else
                {
                    //Printer.setDef(ConfigurationManager.AppSettings["KOT"].ToString());
                    silent.Run(reportViewer1, orderList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw new Exception("Printer Is Not Connected or Connected On Wrong Usb Port . Please Change Usb Port and Try Again!");
            }
        }

        private void metroButton21_Click(object sender, EventArgs e)
        {
            if (gridInv.Rows.Count > 0)
            {
                Saves();
            }
        }

        private void Saves()
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    int orderID = 0;
                    foreach (DataGridViewRow row in gridInv.Rows)
                    {
                        orderID = Convert.ToInt32(row.Cells[0].Value.DefaultZero());
                    }
                    tbl_Order order = db.tbl_Order.Where(x => x.OrderId == orderID).FirstOrDefault();
                    order.isComplete = true;
                    order.Amount = Convert.ToDecimal(txtGrandtotal.Text);
                    order.Discount = Convert.ToDecimal(textBox7.Text.DefaultZero());
                    order.GST = Convert.ToDecimal(txtGstTotl.Text.DefaultZero());
                    order.KOTID = comboBox1.SelectedValue.ToString();
                    order.OrderDate = DateTime.Now;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    SilentPrint(orderID, true);
                    clear();
                    generateChk();
                    BindOrders();
                    transaction.Commit();
                    lblDineInID.Text = "0";
                }
                catch (Exception ex)
                {
                    //var exceptions = ExceptionExtensions.ToMessageAndCompleteStacktrace(ex);
                    //Lib.Entity.Excption Excepti = new Lib.Entity.Excption();
                    //Excepti.ExcptionName = exceptions;
                    //db.Excptions.Add(Excepti);
                    //db.SaveChanges();
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnPnlSave_Click(object sender, EventArgs e)
        {
            savePrint("no");
            pnlTotal.Hide();
            btnSave.Enabled = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox7.Text = CalculateDis(cmbxDis.SelectedValue, txtGrandtotal.Text, textBox2.Text);
            txtCards2_Leave(null, null);
        }

        private void txtGstAmt_Leave(object sender, EventArgs e)
        {
            txtGstTotl.Text = CalculateDis(cmbxGst.SelectedValue, txtGrandtotal.Text, txtGstAmt.Text);
            txtRec.Text = NetAmount().ToString();
            //txtCards2_Leave(null, null);
        }

        private Decimal NetAmount()
        {
            return Convert.ToDecimal(txtGrandtotal.Text.DefaultZero()) + (Convert.ToDecimal(txtGstTotl.Text.DefaultZero()) - Convert.ToDecimal(textBox7.Text.DefaultZero()));
        }

        private void txtCards_Leave(object sender, EventArgs e)
        {
            textBox5.Text =
                (Convert.ToDecimal(txtCards.Text == "" ? "0" : txtCards.Text)
                - (Convert.ToDecimal(txtGrandtotal.Text == "" ? "0" : txtGrandtotal.Text)
                +
                ((Convert.ToDecimal(txtGstTotl.Text == "" ? "0" : txtGstTotl.Text)) -
                Convert.ToDecimal(textBox7.Text == "" ? "0" : textBox7.Text)
                )
                )
                ).ToString();
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            lblBal.Text = "0.0";
            gridItems.DataSource = null;
            gridItems.Rows.Clear();
        }

        private void metroButton12_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridItems.CurrentCell != null)
                {
                    int selectedIndex = gridItems.CurrentCell.RowIndex;
                    if (selectedIndex > -1)
                    {
                        gridItems.Rows.RemoveAt(selectedIndex);
                        LoadBal();
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                //var exceptions = ExceptionExtensions.ToMessageAndCompleteStacktrace(ex);
                //Lib.Entity.Excption Excepti = new Lib.Entity.Excption();
                //Excepti.ExcptionName = exceptions;
                //db.Excptions.Add(Excepti);
                //db.SaveChanges();
                MessageBox.Show("Unable to remove selected row at this time");
            }
        }
        private void gridItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (gridItems.SelectedRows.Count > 0)
            {
                var itemID = Convert.ToInt32(gridItems.SelectedRows[0].Cells[1].Value);
                AddSubQty(itemID, -1, "dec", "", "0");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var itmId = Convert.ToInt32(cmbxItems.SelectedValue);
            var itemList = db.Items.Where(x => x.IID == itmId).ToList();
            GenerateItems(itemList, false);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (gridItems.SelectedRows.Count > 0)
            {
                EditBox edit = new EditBox("Disc", gridItems, lblBal, UserObj);
                edit.Show();
            }
            else
            {
                MessageBox.Show("Item is Not Selected");
            }
        }

        private void metroButton15_Click(object sender, EventArgs e)
        {
            int IID = Convert.ToInt32(btnName);
            AddSubQty(IID, 1, "inc", "" + ddlpizzaSize.Text + " " + txtpizzaMore.Text, ddlpizzaSize.SelectedValue.ToString());
            panel7.Hide();
        }

        private void metroButton13_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            metroButton15.Enabled = true;
            foreach (var control in panel7.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                }
            }
        }

        private void txtMob_Leave(object sender, EventArgs e)
        {
            int n;
            bool isNumeric = int.TryParse(txtMob.Text, out n);
            if (isNumeric)
            {
                string MobNum = txtMob.Text;
                var customer = db.Customers.Where(x => x.Cell == MobNum).FirstOrDefault();
                if (customer != null)
                {
                    txtAddress.Text = customer.Add;
                    txtName.Text = customer.CusName;
                }
            }
        }

        private void Pos_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Main config = new Main(0, UserObj);
            config.Show();
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue.ToString() == "2")
            {
                txtCards.Text = (Convert.ToDecimal(txtGrandtotal.Text == "" ? "0" : txtGrandtotal.Text) + ((Convert.ToDecimal(txtGstTotl.Text == "" ? "0" : txtGstTotl.Text)) - Convert.ToDecimal(textBox7.Text == "" ? "0" : textBox7.Text))).ToString();
                txtCards_Leave(null, null);
                textBox5.Focus();
            }
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue.ToString() == "2")
            {
                txtCards2.Text = (Convert.ToDecimal(txtGrandtotal1.Text.DefaultZero()) + (Convert.ToDecimal(txtDelivery.Text.DefaultZero())) + ((Convert.ToDecimal(txtGstTotl2.Text.DefaultZero())) - Convert.ToDecimal(textBoex.Text.DefaultZero()))).ToString();
                txtCards2_Leave(null, null);
                textBox52.Focus();
            }
        }

        private void txtDelivery_Leave(object sender, EventArgs e)
        {
            txtNetAm.Text = NetAm().ToString();
        }

        private void metroButton14_Click_1(object sender, EventArgs e)
        {
            if (gridInv.Rows.Count > 0)
            {
                var orderID = Convert.ToInt32(gridInv.Rows[0].Cells[0].Value.DefaultZero());
                tbl_Order order = db.tbl_Order.Where(x => x.OrderId == orderID).FirstOrDefault();
                order.isComplete = true;
                order.Discount = 0;
                order.GST = 0;
                order.Amount = 0;
                order.KOTID = "2";
                order.OrderDate = DateTime.Now;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                clear();
                generateChk();
                lblDineInID.Text = "0";
            }
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            btnSave.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button btns = (Button)sender;
            var btn = btnName.Split('~');
            int IID = Convert.ToInt32(btn[0]);
            AddSubQty(IID, 1, "inc", btns.Text, 0);
            panel3.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button btns = (Button)sender;
            var btn = btnName.Split('~');
            int IID = Convert.ToInt32(btn[0]);
            int FullPrice = Convert.ToInt32(btn[1]);
            AddSubQty(IID, 1, "inc", btns.Text, FullPrice);
            panel3.Hide();
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            if (gridInv.SelectedRows.Count > 0)
            {
                var ItemIDs = Convert.ToInt32(gridInv.SelectedRows[0].Cells["ItemID"].Value.DefaultZero());
                var OrderN = gridInv.SelectedRows[0].Cells["OrderNo"].Value.ToString();
                var OrderID = Convert.ToInt32(gridInv.SelectedRows[0].Cells["OrderID"].Value.DefaultZero());

                if (gridInv.Rows.Count == 1)
                {
                    tbl_Order order = db.tbl_Order.Where(x => x.OrderNo == OrderN).FirstOrDefault();
                    order.isComplete = true;
                    order.Discount = 0;
                    order.GST = 0;
                    order.Amount = 0;
                    order.KOTID = "2";
                    order.OrderDate = DateTime.Now;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    clear();
                    generateChk();
                    lblDineInID.Text = "0";
                    gridInv.Rows.Clear();
                }
                else
                {
                    //tbl_OrderDetails ordersDetl = (from order in db.tbl_Order
                    //                               join kot in db.tbl_KOT on order.OrderId.ToString() equals kot.OrderID
                    //                               join OrderDetails in db.tbl_OrderDetails on kot.Id.ToString() equals OrderDetails.KOTID
                    //                               where OrderDetails.itemID == ItemIDs && order.OrderNo == OrderN
                    //                               select new tbl_OrderDetails
                    //                               {
                    //                                   OrderId = OrderDetails.OrderId,
                    //                                   Rate = OrderDetails.Rate,
                    //                                   itemID = OrderDetails.itemID

                    //                               }).FirstOrDefault();

                    var ordersDetl = db.tbl_OrderDetails.Where(x => x.OrderId == OrderID && x.itemID == ItemIDs).FirstOrDefault();
                    db.tbl_OrderDetails.Attach(ordersDetl);
                    db.tbl_OrderDetails.Remove(ordersDetl);
                    db.SaveChanges();
                    gridInv.Rows.RemoveAt(gridInv.SelectedRows[0].Index);

                    Decimal amt = 0;
                    foreach (DataGridViewRow row in gridInv.Rows)
                    {
                        amt += Convert.ToDecimal(row.Cells["Amount"].Value.DefaultZero());
                        //More code here
                    }
                    txtGrandtotal.Text = amt.ToString();
                }
            }
            else
            {
                MessageBox.Show("Row is Not Selected!");
            }
        }

        private void metroButton10_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var OrderID = Convert.ToInt32(cmbxOrders.SelectedValue);
            SilentPrint(OrderID, true);
        }

        private void cmbxOrders_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
public class Dict
{
    public int key { get; set; }
    public String Value { get; set; }
}
