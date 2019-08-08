using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programming_Practice
{
    public static class Prompt
    {
        public static bool ShowDialog(string text)
        {
            string DeleteMessage = "This will effect all the undermentioned Dependant items also\r\n";
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Delete Confirmation",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label()
            {
                Width = 450, Height = 50, Left = 20, Top = 10,
                Text = DeleteMessage + text,
                ForeColor = Color.Red,
                Font = new Font(new FontFamily("Arial"), 10f)
            };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Cancel", Left = 230, Width = 100, Top = 70, DialogResult = DialogResult.Cancel };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = cancel;
            return prompt.ShowDialog() == DialogResult.OK;
        }
    }
}
