using System;
using System.Linq;
using System.Windows.Forms;

namespace Programming_Practice
{
    partial class Form1
    {
        private void InsertUpdateSubject(ref Subject subject)
        {
            subject.Subject_Name = txtSubjectName.Text;
            subject.Description = txtSubjectDescription.Text;
            subject.Sequence = Convert.ToInt32(nudSubjectSequence.Value);
            subject.Start_Date = dtpSubjectStart.Value;
            subject.End_Date = dtpSubjectEndDate.Value;
        }
        private void InitializeSubjectControl(Subject subject)
        {
            txtSubjectName.Text = subject.Subject_Name; ;
            txtSubjectDescription.Text = subject.Description;
            nudSubjectSequence.Value = subject.Sequence;
            dtpSubjectStart.Value = subject.Start_Date;
            dtpSubjectEndDate.Value = subject.End_Date;
        }
        private void BlankSubject()
        {
            txtSubjectName.Text = "";
            txtSubjectDescription.Text = "";
            nudSubjectSequence.Value = 0;
            dtpSubjectStart.Value = DateTime.Now;
            dtpSubjectEndDate.Value = DateTime.Now;
        }
        private void NudSubjectID_ValueChanged(object sender, EventArgs e)
        {
            lblSubjectMessage.Text = "Message : ";
            int id = Convert.ToInt32(nudSubjectID.Value);
            Subject subject = dbContext.Subjects.FirstOrDefault(x => x.ID >= id);
            if (subject != null)
                InitializeSubjectControl(subject);
            else
            {
                lblSubjectMessage.Text = "Message : Subject Not Found";
                BlankSubject();
            }
        }
        private void BtnSubjectShow_Click(object sender, EventArgs e)
        {
            gvSubject.AutoGenerateColumns = false;
            gvSubject.DataSource = dbContext.Subjects.OrderBy(x => x.Subject_Name).ToList();
        }
        private void BtnSubjectInsert_Click(object sender, EventArgs e)
        {
            Subject subject = new Subject();
            InsertUpdateSubject(ref subject);
            if (subject.Subject_Name.Length > 3)
            {
                if (dbContext.Subjects.FirstOrDefault(x => x.Subject_Name == txtSubjectName.Text) == null)
                {
                    dbContext.Subjects.Add(subject);
                    dbContext.SaveChanges();
                    nudSubjectID.Value = dbContext.Subjects.OrderByDescending(x => x.ID).FirstOrDefault().ID;
                    lblSubjectMessage.Text = "Message : Subject Inserted Successfully";
                }
                else
                {
                    lblSubjectMessage.Text = "Message : Subject with Name = " + txtSubjectName.Text + " Already Exsist";
                }

            }
            else
            {
                lblSubjectMessage.Text = "Message : Subject Not Inserted \r\nNote : Subject at least Contains 4 Character";
            }
        }
        private void BtnSubjectUpdate_Click(object sender, EventArgs e)
        {
            Subject subject = dbContext.Subjects.FirstOrDefault(x => x.ID == nudSubjectID.Value);
            if (subject != null)
            {
                InsertUpdateSubject(ref subject);
                dbContext.SaveChanges();
                lblUsersMessage.Text = "Message : Subject Updated Successfully";
            }
            else
            {
                lblUsersMessage.Text = "Message : Subject Not Updated";
            }
        }
        private void BtnSubjectDelete_Click(object sender, EventArgs e)
        {
            Subject subject = dbContext.Subjects.FirstOrDefault(x => x.ID == nudSubjectID.Value);
            if (subject != null)
            {
                dbContext.Subjects.Remove(subject);
                dbContext.SaveChanges();
                lblUsersMessage.Text = "Message : Subject Deleted Successfully \r\nNote : All the Chapters and the Test will also be deleted";
            }
            else
            {
                lblUsersMessage.Text = "Message : Subject Not Inserted \r\nNote : All the Chapters and the Test will also be deleted";
            }
        }
        private void GvSubject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nudSubjectID.Value = GridViewSelectedID(gvSubject, e);
        }

    }
}
