using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SumWiCi
{
    public partial class Form1 : Form
    {
        string SelectedFileName { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Text Files (.txt)|*.txt";

            openFileDialog1.Multiselect = false;
            String pathDefault = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.InitialDirectory = pathDefault;
            // Call the ShowDialog method to show the dialog box.
            openFileDialog1.ShowDialog();
            buttonStart.Enabled = true;
            SelectedFileName = openFileDialog1.FileName;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            SumWiCi sWiCi = new SumWiCi(SelectedFileName);
            textBox1.Text = sWiCi.GetSumWiCiFromBehind().ToString();
            richTextBox1.AppendText(sWiCi.permToString());
        }
    }
}
