using Lib.Entity;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace SalesMngmt
{
    public partial class Pass : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        int cmpID = 0;
        string UsrID = "0";
        public Pass(int CompayId, string UserID)
        {
            InitializeComponent();
            cmpID = CompayId;
            UsrID = UserID;
            db = new SaleManagerEntities();
        }

        private void Pass_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = db.AspNetUsers.Where(x => x.Id == UsrID).FirstOrDefault();
            if (user != null)
            {
                if (user.PasswordHash == textBox1.Text.Trim())
                {
                    user.PasswordHash = textBox2.Text;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    label3.Text = "Password Change Successfully";
                }
                else
                {
                    label3.Text = "Old Password Is Incorrect";
                }
            }
        }
    }
}