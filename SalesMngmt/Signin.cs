﻿using Lib.Entity;
using System;
using System.Linq;
using System.Windows.Forms;
//using TrialApp;

namespace SalesMngmt
{
    public partial class Signin : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        public Signin()
        {
            db = new SaleManagerEntities();
            InitializeComponent();
        }

        private void Signin_Load(object sender, EventArgs e)
        {
            //  checkTrail();
        }

        //private void checkTrail()
        //{
        //    Trail trail = new Trail();
        //    DateTime installDate = trail.GetInstallationDate();
        //    int launchCount = trail.GetLaunchCount();

        //    TimeSpan trialDuration = DateTime.Now - installDate;
        //    int remainingDays = 30 - trialDuration.Days;

        //    if (remainingDays > 0)
        //    {
        //        lblTrail.Text = "Trial period remaining: " + remainingDays + " days";
        //        // Continue using the application
        //    }
        //    else
        //    {
        //        lblTrail.Text = "Your trial period has expired.";
        //        //Close();
        //    }
        //    // Increment the launch count and save it
        //    trail.SaveLaunchCount(launchCount + 1);

        //}

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //
            try
            {
                String UsrName = metroTextBox1.Text.Trim();
                String Pass = metroTextBox2.Text.Trim();
                var user = db.AspNetUsers.Where(x => x.UserName == UsrName && x.PasswordHash == Pass).FirstOrDefault();
                if (user != null)
                {
                    this.Hide();
                    int CompayId = 0;
                    Main main = new Main(CompayId, user);
                    main.Show();
                }
                else
                {
                    MessageBox.Show("UserName And Password Is InCorrect", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                if (metroTextBox2.Text.Trim() != "")
                {
                    metroButton1_Click(null, null);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Signin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
