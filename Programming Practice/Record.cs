using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Programming_Practice
{
    public partial class Form1
    {
        private void CombProgressSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChapters(combProgressSubjects, combProgressChapter);
        }
        private void CombProgressChapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentChapter = combProgressChapter.Text;
            lblProgressMessage.Text = "Message : No Record Found";
            gvSearchRecord.DataSource = null;
            //Here we are Assigning new Object to
            int chapter_ID = ((ComboboxKVP)combProgressChapter.SelectedItem).ID;
            List<ProgressOverRideClass> prog = Currentuser.Progresses
                .Where(x => x.Log.Chapter_ID == chapter_ID)
            .Select(up => new ProgressOverRideClass()
                        {
                            ID = up.ID,
                            Question = up.Log.Question,
                            Attempt = up.Attempt
                        }).ToList();

            if (prog.Count() > 0)
            {
                Attempt_Images.ImageLayout = DataGridViewImageCellLayout.Stretch;
                gvSearchRecord.AutoGenerateColumns = false;
                gvSearchRecord.DataSource = prog;
                lblProgressMessage.Text = "Message : " + prog.Count() + " Records Found";
            }
        }
    }
}
