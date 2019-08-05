using System;
using System.Linq;
using System.Windows.Forms;

namespace Programming_Practice
{
    public partial class Form1
    {
        bool DoThat = true;
        private void CombSearchSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChapters(combSearchSubject, combSearchChapter);
        }
        private void CombSearchChapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentChapter = combSearchChapter.Text;
            if (DoThat)
            {
                int chapter_ID = ComboboxKVP.GetIDFromComboBox(combSearchChapter);
                gvSearch.AutoGenerateColumns = false;
                gvSearch.DataSource = dbContext.Logs
                    .Where(x => x.Chapter_ID == chapter_ID).ToList();
            }
        }
        private void TxtSearchSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gvSearch.AutoGenerateColumns = false;
                gvSearch.DataSource = dbContext.Logs.Where(x => x.Answer.Contains(txtSearchSearch.Text) ||
                x.Question.Contains(txtSearchSearch.Text) || x.Description.Contains(txtSearchSearch.Text)).Take(100).ToList();
            }

        }
        private void GvSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = GridViewSelectedID(gvSearch, e);
            Chapter chapter = dbContext.Logs.FirstOrDefault(x => x.ID == id).Chapter;
            DoThat = false;
            combSearchSubject.Text = chapter.Subject.Subject_Name;
            combSearchChapter.Text = chapter.Chapter_Name;
            DoThat = true;
        }
    }
}
