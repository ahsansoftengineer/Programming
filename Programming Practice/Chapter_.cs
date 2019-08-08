using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programming_Practice
{
    partial class Form1
    {
        private void InsertUpdateChapter(ref Chapter chapter)
        {
            chapter.Chapter_Name = txtChapterChapterName.Text;
            chapter.Chapter_No = Convert.ToInt32(nudChapterChapterNo.Value);
            chapter.Description = txtChapterDescrption.Text;
            chapter.Subject_ID = ComboboxKVP.GetIDFromComboBox(combChapterSubjectName);
        }
        private void InitializeChapterControl(Chapter chapter)
        {
            nudChapterID.Value = chapter.ID;
            txtChapterChapterName.Text = chapter.Chapter_Name;
            nudChapterChapterNo.Value = chapter.Chapter_No;
            txtChapterDescrption.Text = chapter.Description;
            combChapterSubjectName.Text = chapter.Subject.Subject_Name;
        }
        private void BlankChapter()
        {
            txtChapterChapterName.Text = "";
            nudChapterChapterNo.Value = 0;
            txtChapterDescrption.Text = "";
        }
        private void NudChapterID_ValueChanged(object sender, EventArgs e)
        {
            lblChapterMessage.Text = "Message : ";
            int id = Convert.ToInt32(nudChapterID.Value);
            Chapter chapter = dbContext.Chapters.FirstOrDefault(x => x.ID >= id);
            if (chapter != null)
                InitializeChapterControl(chapter);
            else
            {
                lblChapterMessage.Text = "Message : Chapter Not Found";
                BlankChapter();
            }
        }
        private void CombChapterSubjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentSubject = combChapterSubjectName.Text;
            gvChapter.AutoGenerateColumns = false;
            int subID = ComboboxKVP.GetIDFromComboBox(combChapterSubjectName);
            gvChapter.DataSource = dbContext.Chapters
                .Where(x => x.Subject.ID == subID)
                .OrderBy(x => x.Chapter_No).ToList();
        }
        private void BtnChapterInsert_Click(object sender, EventArgs e)
        {
            Chapter chapter = new Chapter();
            InsertUpdateChapter(ref chapter);
            dbContext.Chapters.Add(chapter);
            dbContext.SaveChanges();
            nudChapterID.Value = dbContext.Chapters.OrderByDescending(x => x.ID).FirstOrDefault().ID;
            lblUsersMessage.Text = "Message : Chapter Inserted Successfully";
        }
        private void BtnChapterUpdate_Click(object sender, EventArgs e)
        {
            Chapter chapter = dbContext.Chapters.FirstOrDefault(x => x.ID == nudChapterID.Value);
            if (chapter != null)
            {
                InsertUpdateChapter(ref chapter);
                dbContext.SaveChanges();
                InitializeChapterControl(chapter);
                lblUsersMessage.Text = "Message : Chapter Updated Successfully";
            }
            else
                lblUsersMessage.Text = "Message : Chapter Not Found";
        }
        private void BtnChapterDelete_Click(object sender, EventArgs e)
        {
            Chapter chapter = dbContext.Chapters.FirstOrDefault(x => x.ID == nudChapterID.Value);
            if (chapter != null)
            {
                if (Prompt.ShowDialog("Library, Progress\r\nRelated to ID = " + chapter.ID + " Name = " + chapter.Chapter_Name))
                {
                    dbContext.Chapters.Remove(chapter);
                    dbContext.SaveChanges();
                    BlankChapter();
                    lblChapterMessage.Text = "Message : Chapter Deleted Successfully";
                }
                else
                {
                    lblChapterMessage.Text = "Message : Chapter with ID = "+ chapter.ID + " not Deleted Successfully";
                }
            }
            else
                lblChapterMessage.Text = "Message : Chapter Not Found";
        }
        private void GvChapter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nudChapterID.Value = GridViewSelectedID(gvChapter, e);
        }
    }
}
