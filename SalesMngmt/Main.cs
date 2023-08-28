using LabExpressDesktop.Reporting;
using Lib.Entity;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using SalesMngmt.Invoice;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using TrialApp;

namespace SalesMngmt
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        int cmpID = 0;
        AspNetUser User = null;
        SaleManagerEntities db = null;
        public Main(int CompayId, AspNetUser Usr)
        {
            InitializeComponent();
            cmpID = CompayId;
            User = Usr;
            db = new SaleManagerEntities();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                if (User.Id != "1")
                {
                    metroTile1.Visible = false;
                    metroTile7.Visible = false;
                    metroTile9.Visible = false;
                }
                label2.Text = User.Email;

                int MinusDay = 0;
                TimeSpan start = new TimeSpan(3, 59, 59);
                TimeSpan now = DateTime.Now.TimeOfDay;

                if (now < start)
                {
                    MinusDay = -1;
                    //match found
                }
                Decimal? TotalSale = 0;
                var startTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, (DateTime.Today.Day + MinusDay), 16, 00, 00);

                var endTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, (DateTime.Today.Day + 1 + MinusDay), 3, 59, 59);
                var list = db.tbl_Order.Where(x => x.OrderDate > startTime && x.OrderDate < endTime).ToList();
                foreach (var item in list)
                {
                    TotalSale = item.Amount - item.Discount + item.GST;
                }
                label1.Text = TotalSale.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void metroPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Config config = new Config(cmpID, User);
            config.Show();
        }

        private void metroTile5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Pos con = new Pos(User);
            con.Show();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Signin con = new Signin();
            con.Show();
        }

        private void metroTile7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Reports con = new Reports(cmpID, User);
            con.Show();
        }

        private void metroTile6_Click(object sender, EventArgs e)
        {
            this.Dispose();
            OrderItemsSummary form = new OrderItemsSummary(User);
            form.Show();
        }

        private void metroTile9_Click(object sender, EventArgs e)
        {
            String DestPath = @"D:\Backup";
            String DbName = "SaleMgmt";
            string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\SaleMgmt.mdf;Integrated Security=True";

            try
            {
                string databaseName = DbName;//dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();

                //Define a Backup object variable.
                Backup sqlBackup = new Backup();

                ////Specify the type of backup, the description, the name, and the database to be backed up.
                sqlBackup.Action = BackupActionType.Database;
                sqlBackup.BackupSetDescription = "BackUp of:" + databaseName + "on" + DateTime.Now.ToShortDateString();
                sqlBackup.BackupSetName = "FullBackUp";
                sqlBackup.Database = databaseName;

                ////Declare a BackupDeviceItem
                string destinationPath = DestPath;
                Random _rdm = new Random();
                var rand = _rdm.Next(100000, 999999);
                string backupfileName = rand + DbName + ".bak";
                BackupDeviceItem deviceItem = new BackupDeviceItem(destinationPath + "\\" + backupfileName, DeviceType.File);
                ////Define Server connection

                //ServerConnection connection = new ServerConnection(frm.serverName, frm.userName, frm.password);
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
                SqlConnection con = new SqlConnection(constring);
                ServerConnection connection = new ServerConnection(con);
                ////To Avoid TimeOut Exception
                Server sqlServer = new Server(connection);
                sqlServer.ConnectionContext.StatementTimeout = 60 * 60;
                Database db = sqlServer.Databases[databaseName];

                sqlBackup.Initialize = true;
                sqlBackup.Checksum = true;
                sqlBackup.ContinueAfterError = true;

                ////Add the device to the Backup object.
                sqlBackup.Devices.Add(deviceItem);
                ////Set the Incremental property to False to specify that this is a full database backup.
                sqlBackup.Incremental = false;

                sqlBackup.ExpirationDate = DateTime.Now.AddDays(3);
                ////Specify that the log must be truncated after the backup is complete.
                sqlBackup.LogTruncation = BackupTruncateLogType.Truncate;

                sqlBackup.FormatMedia = false;
                ////Run SqlBackup to perform the full database backup on the instance of SQL Server.
                sqlBackup.SqlBackup(sqlServer);
                ////Remove the backup device from the Backup object.
                sqlBackup.Devices.Remove(deviceItem);
                label6.Text = "Backup successfully Created In " + destinationPath + "\\" + backupfileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // MessageBox.Show(ex.Message);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Environment.Exit(1);
                //Signin signi = new Signin();
            //signi.Show();
        }

        private void metroTile6_Click_2(object sender, EventArgs e)
        {
            Pass signi = new Pass(cmpID, User.Id);
            signi.Show();
        }
    }
}



