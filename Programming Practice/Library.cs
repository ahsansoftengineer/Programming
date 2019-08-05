using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programming_Practice
{
    public partial class Form1
    {
        private void SetControlBlank()
        {
            combLibraryQuestion.Text = "";
            txtLibraryHint1.Text = "";
            txtLibraryHint2.Text = "";
            txtLibraryHint3.Text = "";
            txtLibraryAnswer.Text = "";
            txtLibraryDescription.Text = "";
        }
        private void InsertUpdateLog(ref Log newLog)
        {
            newLog.Chapter_ID = ComboboxKVP.GetIDFromComboBox(combLibraryChapters);
            newLog.Answer = txtLibraryAnswer.Text;
            newLog.Question = combLibraryQuestion.Text;
            newLog.Hint_1 = txtLibraryHint1.Text;
            newLog.Hint_2 = txtLibraryHint2.Text;
            newLog.Hint_3 = txtLibraryHint3.Text;
            newLog.Type_ID = ComboboxKVP.GetIDFromComboBox(combLibraryType);
            newLog.Level = Convert.ToInt32(nudLibraryLevel.Value);
            newLog.Description = txtLibraryDescription.Text;
        }
        private void FillLibrary(Log log)
        {
            if (log != null)
            {
                nudLibraryID.Value = log.ID;
                combLibrarySubject.Text = log.Chapter.Subject.Subject_Name;
                combLibraryChapters.Text = "Part " + log.Chapter.Chapter_No + " " + log.Chapter.Chapter_Name;
                combLibraryType.Text = log.Type.Type_;
                combLibraryQuestion.Text = log.Question;
                txtLibraryHint1.Text = log.Hint_1;
                txtLibraryHint2.Text = log.Hint_2;
                txtLibraryHint3.Text = log.Hint_3;
                txtLibraryAnswer.Text = log.Answer;
                txtLibraryDescription.Text = log.Description;
                nudLibraryLevel.Value = Convert.ToInt32(log.Level);
            }
            else
            {
                SetControlBlank();
                txtLibraryAnswer.Text = "Error Message: - ID \"" + nudLibraryID.Value + "\" does not Exsist";
            }
        }
        private void NudLibraryID_ValueChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(nudLibraryID.Value);
            Log log = dbContext.Logs.FirstOrDefault(x => x.ID >= ID);
            FillLibrary(log);
        }
        private void CombLibrarySubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChapters(combLibrarySubject, combLibraryChapters);
        }
        private void CombLibraryChapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillQuestion(combLibraryChapters, combLibraryQuestion);
        }
        private void CombLibraryQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ckbLibraryAutoFunctionaility.Checked)
            {
                //int log_ID = Convert.ToInt32(nudLibraryID.Value);
                //Log log = dbContext.Logs.OrderBy(x => x.Chapter_ID).FirstOrDefault(x => x.Chapter_ID == ID && x.ID >= log_ID);
                //if (log == null)
                //    log = dbContext.Logs.OrderBy(x => x.Chapter_ID).FirstOrDefault(x => x.Chapter_ID == ID);
                int ID = ComboboxKVP.GetIDFromComboBox(combLibraryQuestion);
                if (ID > 0)
                {
                    Log log = dbContext.Logs.FirstOrDefault(x => x.ID == ID);
                    FillLibrary(log);
                }
            }
        }
        //private void GvLibraryTable_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    int index = e.RowIndex;
        //    if (index != -1)
        //    {
        //        DataGridViewRow selectedrow = gvLibraryTable.Rows[index];
        //        nudLibraryID.Value = Convert.ToInt32(selectedrow.Cells[0].Value);
        //    }
        //}
        private void BtnLibraryInsert_Click(object sender, EventArgs e)
        {
            Log log = new Log();
            InsertUpdateLog(ref log);
            dbContext.Logs.Add(log);
            dbContext.SaveChanges();
            nudLibraryID.Value = dbContext.Logs.OrderByDescending(x => x.ID).FirstOrDefault().ID;
        }
        private void BtnLibraryUpdate_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(nudLibraryID.Value);
            Log log = dbContext.Logs.FirstOrDefault(x => x.ID == ID);
            InsertUpdateLog(ref log);
            dbContext.SaveChanges();
        }
        private void BtnLibraryGetData_Click(object sender, EventArgs e)
        {
            FillQuestion(combLibraryChapters, combLibraryQuestion);
        }
        private void BtnLibraryDelete_Click(object sender, EventArgs e)
        {

            int ID = Convert.ToInt32(nudLibraryID.Value);
            Log log = dbContext.Logs.FirstOrDefault(x => x.ID == ID);
            dbContext.Logs.Remove(log);
            dbContext.SaveChanges();
            SetControlBlank();
        }
    }
}
