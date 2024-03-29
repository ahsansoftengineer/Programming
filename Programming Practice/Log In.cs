﻿using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace Programming_Practice
{
    public partial class BaseForm
    {
        public User Currentuser { get; set; }
        private void RunOnUiThread(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }
        private void TabControlAddTabPage(TabPage page = null)
        {
            if (page != null)
                RunOnUiThread(() => TabControlProgramming.Controls.Add(page));
            RunOnUiThread(() => pbLogIn.Value += 3);
        }
        private void LogInMessage(string Message)
        {
            RunOnUiThread(() => lblLogInMessage.Text = "Message : " + Message);
        }
        private void DoThatOnCompleteofVerification()
        {
            txtLogInPassword.Text = "";
            if (Currentuser != null)
            {
                btnLogInShowControls.Visible = true;
                Text = Currentuser.Name + " Welcome!";
                if (Currentuser.Image != null)
                    ImgLogInUserImage.Image = ByteArrayToImage(Currentuser.Image);
                pbLogIn.Value = 100;
                btnLogInVerify.Visible = false;
                btnLogLogOut.Visible = true;
                pbLogIn.Visible = false;
            }
            else
            {
                HideControls();
            }
        }
        private void VerifyLogIn()
        {
            RunOnUiThread(() => pbLogIn.Visible = true);
            RunOnUiThread(() => pbLogIn.Value = 70);
            if (txtLogInUserName.Text != "" && txtLogInPassword.Text != "")
            {
                TabControlAddTabPage();
                Currentuser = dbContext.Users.FirstOrDefault(x => x.LogInID == txtLogInUserName.Text);
                if (Currentuser != null && Currentuser.Password == txtLogInPassword.Text)
                {
                    TabControlAddTabPage();
                    if (Currentuser.ExpiryDate < DateTime.Today)
                    {
                        LogInMessage("Your Password has Expired");
                        Currentuser = null;
                    }
                    else
                    {
                        RunOnUiThread(() => btnLogLogOut.Visible = true);
                        // Showing the Admin Controls
                        if (txtLogInUserName.Text == "Admin")
                        {
                            TabControlAddTabPage(tpSubject);
                            TabControlAddTabPage(tpChapter);
                            TabControlAddTabPage(tpQuestion);
                        }
                        // Showing the User MemberControl
                        TabControlAddTabPage(tpTest);
                        TabControlAddTabPage(tpProgress);
                        // Showing the Admin Controls
                        if (txtLogInUserName.Text == "Admin")
                            TabControlAddTabPage(tpUser);
                        LogInMessage("You have Logged In");
                        if (Currentuser.ExpiryDate < DateTime.Now.AddDays(5))
                            LogInMessage("Please change Your Password before it gets Expired \nExpiry Date = " + Currentuser.ExpiryDate);
                    }
                }
                else if (Currentuser != null)
                {
                    LogInMessage("Password is not matched Hint = \"" + Currentuser.Hint + "\"");
                    Currentuser = null;
                }
                else
                    LogInMessage("User Name and Password are not matched");
            }
            else
                LogInMessage("User Name and Password are Required");
            Thread.Sleep(1000);
            RunOnUiThread(() => DoThatOnCompleteofVerification());
        }
        private void BtnLogInVerify_Click(object sender, EventArgs e)
        {
            pbLogIn.Value = 4;
            Thread _thread = new Thread(new ThreadStart(VerifyLogIn));
            _thread.IsBackground = true;
            _thread.Start();
        }
        private void BtnLogLogOut_Click(object sender, EventArgs e)
        {
            HideControls();
        }
        private void BtnLogInShowControls_Click(object sender, EventArgs e)
        {
            pnlLogInChangePassword.Visible = !pnlLogInChangePassword.Visible;
        }
        private void BtnLogInSaveChanges_Click(object sender, EventArgs e)
        {
            if (txtLogInNewPassword.Text.Length > 7)
            {
                if (txtLogInNewPassword.Text == txtLogInConfirmPassword.Text)
                {
                    if (txtLogInNewPassword.Text != Currentuser.Password)
                    {
                        Currentuser.Password = txtLogInNewPassword.Text;
                        Currentuser.Hint = txtLogInHint.Text;
                        Currentuser.ExpiryDate = DateTime.Today.AddDays(45);
                        dbContext.SaveChanges();
                        txtLogInNewPassword.Text = "";
                        txtLogInConfirmPassword.Text = "";
                        txtLogInPassword.Text = "";
                        txtLogInHint.Text = "";
                        pnlLogInChangePassword.Visible = false;
                        lblLogInMessage.Text = "Your Password Expiration till " + Convert.ToDateTime(Currentuser.ExpiryDate).ToShortDateString();
                    }
                    else
                    {
                        lblLogInMessage.Text = "Please Enter the Crrect Old Password";
                    }
                }
                else
                {
                    lblLogInMessage.Text = "New Password / Confirm Password are not matched";
                }
            }
            else
            {
                lblLogInMessage.Text = "Password at least consist of 8 Characters";
            }

        }
        private void BtnLogInCancel_Click(object sender, EventArgs e)
        {
            pnlLogInChangePassword.Visible = false;
            lblLogInMessage.Text = "";
        }
    }
}
