using System;
using System.Linq;

namespace Programming_Practice
{
    public partial class Form1
    {
        private void InsertUpdateUser(ref User user)
        {
            user.Name = txtUsersName.Text;
            user.Password = txtUsersPassword.Text;
            user.LogInID = user.LogInID != "" ? txtUsersLogInID.Text : user.LogInID;
            user.Status = combUsersStatus.Text;
            user.Hint = txtUsersHint.Text;
            user.EnrollmentDate = dtpUsersEnrollmentDate.Value;
            user.ExpiryDate = dtpUsersExpiryDate.Value;
        }
        private void InitializeUserControl(User user)
        {
            numUsersID.Value = user.ID;
            txtUsersName.Text = user.Name;
            txtUsersPassword.Text = user.Password;
            txtUsersLogInID.Text = user.LogInID;
            combUsersStatus.Text = user.Status;
            txtUsersHint.Text = user.Hint;
            dtpUsersEnrollmentDate.Value = user.EnrollmentDate;
            dtpUsersExpiryDate.Value = user.ExpiryDate;
        }
        private void BlankUser()
        {
            numUsersID.Value = 0;
            txtUsersName.Text = "";
            txtUsersPassword.Text = "";
            txtUsersLogInID.Text = "";
            combUsersStatus.Text = "";
            txtUsersHint.Text = "";
            dtpUsersEnrollmentDate.Value = DateTime.Now;
            dtpUsersExpiryDate.Value = DateTime.Now;
        }
        private void NumUsersID_ValueChanged(object sender, EventArgs e)
        {
            lblUsersMessage.Text = "Message : ";
            int id = Convert.ToInt32(numUsersID.Value);
            User user =  dbContext.Users.FirstOrDefault(x => x.ID >= id);
            if (user != null)
                InitializeUserControl(user);
            else
            {
                lblUsersMessage.Text = "Message : User Not Found";
                BlankUser();
            }
        }
        private void BtnUsersInsert_Click(object sender, EventArgs e)
        {
            User user = new User();
            InsertUpdateUser(ref user);
            if (dbContext.Users.FirstOrDefault(x => x.LogInID == user.LogInID) == null)
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                lblUsersMessage.Text = "Message : User Inserted Successfully";
            }
            else
            {
                lblUsersMessage.Text = "Message : User Not Inserted";
            }
        }
        private void BtnUsersUpdate_Click(object sender, EventArgs e)
        {
            User user = dbContext.Users.FirstOrDefault(x => x.ID == numUsersID.Value);
            string OldUserID = user.LogInID;
            if (user != null)
            {
                InsertUpdateUser(ref user);
                user.LogInID = OldUserID;
                dbContext.SaveChanges();
                InitializeUserControl(user);
                lblUsersMessage.Text = "Message : User Updated Successfully \r\nNote : You cannot Change User Log In Name";
            }
            else
                lblUsersMessage.Text = "Message : User Not Found";

        }
        private void BtnUsersDelete_Click(object sender, EventArgs e)
        {
            User user = dbContext.Users.FirstOrDefault(x => x.ID == numUsersID.Value);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                BlankUser();
                lblUsersMessage.Text = "Message : User with ID = " + user.ID + " User Name = " + user.Name + " Deleted";
            }
            else
            {
                lblUsersMessage.Text = "Message : User with ID = " + numUsersID.Value + " User Name = " + user.Name + " Not Found";
            }

        }
    }
}
