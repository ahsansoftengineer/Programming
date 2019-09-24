using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Programming_Practice
{
    public partial class BaseForm : Form
    {
        string CurrentSubject = "";
        string CurrentChapter = "";
        public ProgrammingDbContext dbContext = new ProgrammingDbContext();
        private void FillSubject(ComboBox subject)
        {
            subject.Items.Clear();
            ComboboxKVP[] subjects = dbContext.Subjects.OrderBy(x => x.Subject_Name).ToList()
               .Select(x => new ComboboxKVP() { ID = x.ID, Text = x.Subject_Name }).ToArray();
            subject.Items.AddRange(subjects);
            if (subjects.Count() > 0 && CurrentSubject == "")
            {
                subject.SelectedIndex = 0;
                CurrentChapter = "";
            }
            else if (subjects.Count() > 0 && CurrentSubject != "")
                subject.Text = CurrentSubject;
        }
        private void FillChapters(ComboBox subject, ComboBox chapter)
        {
            CurrentSubject = subject.Text;
            chapter.Items.Clear();
            int subid = ComboboxKVP.GetIDFromComboBox(subject);
            ComboboxKVP[] chapters = dbContext.Chapters
            .Where(x => x.Subject_ID == subid)
            .OrderBy(x => x.Chapter_No).ToList()
            .Select(y => new ComboboxKVP() { ID = y.ID, Text = "Part " + y.Chapter_No + " " + y.Chapter_Name }).ToArray();
            chapter.Items.AddRange(chapters);
            if (chapters.Count() > 0 && CurrentChapter != "")
                chapter.Text = CurrentChapter;
            if (chapters.Count() > 0 && chapter.SelectedIndex < 0)
            {
                chapter.SelectedIndex = 0;
            }
        }
        private void FillQuestion(ComboBox chapter, ComboBox question)
        {
            CurrentChapter = chapter.Text;
            question.Items.Clear();
            int chapter_id = ComboboxKVP.GetIDFromComboBox(chapter);
            ComboboxKVP[] questions = dbContext.Logs
            .Where(x => x.Chapter_ID == chapter_id)
            .Select(y => new ComboboxKVP() { ID = y.ID, Text = y.Question }).ToArray();
            if (questions.Count() > 0 && CurrentChapter != "")
            {
                question.Items.AddRange(questions);
                question.SelectedIndex = 0;
            }
        }
        private void FillType(ComboBox type)
        {
            type.Items.Clear();
            ComboboxKVP[] types = dbContext.Types
                .OrderBy(x => x.Type_).Select(x => new ComboboxKVP() { ID = x.ID, Text = x.Type_ }).ToArray();
            foreach (ComboboxKVP item in types)
            {
                type.Items.Add(item);
            }
            if (types.Count() > 0)
                type.SelectedIndex = 0;
        }
        public BaseForm()
        {
            InitializeComponent();
        }
        private void HideControls()
        {
            Currentuser = null;
            //Hiding the User MemberControl
            TabControlProgramming.Controls.Remove(tpTest);
            TabControlProgramming.Controls.Remove(tpProgress);
            //Hiding the Admin Controls
            TabControlProgramming.Controls.Remove(tpQuestion);
            TabControlProgramming.Controls.Remove(tpSubject);
            TabControlProgramming.Controls.Remove(tpChapter);
            TabControlProgramming.Controls.Remove(tpUser);
            //Disabling Control on Failure of Log In
            pbLogIn.Visible = false;
            btnLogLogOut.Visible = false;
            pnlLogInChangePassword.Visible = false;
            btnLogInShowControls.Visible = false;
            Text = "Welcome to Programming Practice";
            //This Control needs to be Enabled when all the Controls are hidden
            btnLogInVerify.Visible = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            HideControls();
        }
        private void TabControlProgramming_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text == "Test")
            {
                FillSubject(combTestSubject);
                FillChapters(combTestSubject, combTestChapters);
                FillType(combTestType);

            }
            else if (e.TabPage.Text == "Progress")
            {
                FillSubject(combProgressSubjects);
                FillChapters(combProgressSubjects, combProgressChapter);
            }
            else if (e.TabPage.Text == "Search")
            {
                FillSubject(combSearchSubject);
                FillChapters(combSearchSubject, combSearchChapter);
            }

            else if (e.TabPage == tpQuestion)
            {
                FillSubject(combLibrarySubject);
                FillChapters(combLibrarySubject, combLibraryChapters);
                FillType(combLibraryType);
            }
            else if (e.TabPage.Text == "Chapter")
            {
                FillSubject(combChapterSubjectName);
            }
            else if (e.TabPage.Text == "Subject")
            {
                List<Subject> subjects = dbContext.Subjects.OrderBy(x => x.Subject_Name).ToList();
                if (subjects.Count() > 0)
                {
                    gvSubject.AutoGenerateColumns = false;
                    gvSubject.DataSource = subjects;
                    lblSubjectMessage.Text = "Total Subjects = (" + subjects.Count() + ") Available";
                }
            }
        }
        private int GridViewSelectedID(DataGridView gridView, DataGridViewCellEventArgs events)
        {
            int index = events.RowIndex;
            if (index != -1)
            {
                DataGridViewRow selectedrow = gridView.Rows[index];
                return Convert.ToInt32(selectedrow.Cells[0].Value);
            }
            else
                return 1;
        }
    }
    public class ComboboxKVP
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public static int GetIDFromComboBox(ComboBox combobox)
        {
            return ((ComboboxKVP)combobox.SelectedItem).ID;
        }
    }
}
