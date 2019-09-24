using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Programming_Practice
{
    public partial class BaseForm
    {
        List<Log> tests = new List<Log>();
        private int LastAttempt = 0;
        private int TotalQuestions = 0;
        private void TestFillControl(int Question_Index)
        {
            txtTestAnswer.Text = "";
            txtTestQuestion.Text = "";
            pbTestAttempt.Value = 0;
            if (Currentuser != null)
            {
                if (tests.Count() < 1)
                    txtTestQuestion.Text = "Please Generate the Test First";
                else
                {
                    lblTestQuestionID.Text = tests[Question_Index].ID.ToString();
                    txtTestQuestion.Text = tests[Question_Index].Question;
                    lblTestStartofEnd.Text = (Question_Index + 1) + " of " + TotalQuestions;
                    if (ckbTestShowAnswer.Checked)
                    {
                        txtTestAnswer.Text = tests[Question_Index].Answer;
                    }
                    Progress progresses = Currentuser.Progresses.FirstOrDefault(x => x.Log_ID == tests[Question_Index].ID);
                    if (progresses != null)
                    {
                        int progress = Convert.ToInt32(progresses.Attempt);
                        pbTestAttempt.Value = progress;
                    }
                    combTestType.Text = tests[Question_Index].Type.Type_;
                }
            }
            else
            {
                txtTestQuestion.Text = "Please Log In First";
            }
        }
        private void TestGenerate()
        {
            int subject_ID = ComboboxKVP.GetIDFromComboBox(combTestSubject);
            int chapter_ID = ComboboxKVP.GetIDFromComboBox(combTestChapters);
            if (combTestChapters.Text == "")
                tests = dbContext.Logs.Where(x => x.Chapter.Subject_ID == subject_ID).ToList();
            else
                tests = dbContext.Logs.Where(x => x.Chapter_ID == chapter_ID).ToList();
            if (tests.Count > 0)
            {
                TotalQuestions = tests.Count();
                LastAttempt = 0;
                TestFillControl(LastAttempt);
            }
        }
        private void CombTestSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChapters(combTestSubject, combTestChapters);
        }
        private void CombTestChapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentChapter = combTestChapters.Text;
            int chapter_ID = ComboboxKVP.GetIDFromComboBox(combTestChapters);
            tests = dbContext.Logs.Where(x => x.Chapter_ID == chapter_ID).ToList();
            TestGenerate();
        }
        private void UpdatedProgress(int UserID, int TestID)
        {
            if (ckbTestIknow.Checked)
            {
                Progress progress = dbContext.Progresses.FirstOrDefault(x => x.User_ID == UserID && x.Log_ID == TestID);
                if (progress == null)
                {
                    Progress newProgress = new Progress();
                    newProgress.User_ID = UserID;
                    newProgress.Log_ID = TestID;
                    newProgress.Attempt = 1;
                    dbContext.Progresses.Add(newProgress);
                }
                else
                {
                    progress.Attempt++;
                }
                dbContext.SaveChanges();
            }
        }
        private void BtnTestPrevious_Click(object sender, EventArgs e)
        {
            UpdatedProgress(Currentuser.ID, tests[LastAttempt].ID);
            --LastAttempt;
            if (LastAttempt < 0)
                LastAttempt = TotalQuestions - 1;
            TestFillControl(LastAttempt);
        }
        private void BtnTestNext_Click(object sender, EventArgs e)
        {
            UpdatedProgress(Currentuser.ID, tests[LastAttempt].ID);
            ++LastAttempt;
            if (LastAttempt >= TotalQuestions)
                LastAttempt = 0;
            TestFillControl(LastAttempt);
        }
        private void BtnHelp_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "Hint 1":
                    txtTestAnswer.Text = "Hint 1 \r\n\r\n" + tests[LastAttempt].Hint_1;
                    break;
                case "Hint 2":
                    txtTestAnswer.Text = "Hint 2 \r\n\r\n" + tests[LastAttempt].Hint_2;
                    break;
                case "Hint 3":
                    txtTestAnswer.Text = "Hint 3 \r\n\r\n" + tests[LastAttempt].Hint_3;
                    break;
                case "Answer":
                    txtTestAnswer.Text = "Answer \r\n \r\n" + tests[LastAttempt].Answer;
                    break;
                case "Description":
                    txtTestAnswer.Text = "Description \r\n \r\n" + tests[LastAttempt].Description;
                    break;
                default:
                    txtTestAnswer.Text = "Unknown";
                    break;
            }
        }
    }
}
