using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programming_Practice
{
    class ProgressOverRideClass
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public int Attempt { get; set; }
        public Image Attempt_Image
        {
            get
            {
                if (Attempt > 0)
                {
                    string directory = Directory.GetParent(
                        Directory.GetParent(
                            Environment.CurrentDirectory).ToString()).ToString();
                    string newpath = directory + "\\ProgressBar\\" + Attempt + ".PNG";
                    if (File.Exists(newpath))
                        return Image.FromFile(newpath);
                    else return null;
                }
                else
                    return null;
            }
            set { }
        }
    }
}
