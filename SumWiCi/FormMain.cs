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
    public partial class FormMain : Form
    {
        public string SelectedFileName { get; set; }
        private SumWiCi sWiCi;
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog.Filter = "Text Files (.txt)|*.txt";

            openFileDialog.Multiselect = false;
            String pathDefault = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.InitialDirectory = pathDefault;
            // Call the ShowDialog method to show the dialog box.
            openFileDialog.ShowDialog();

            buttonStart.Enabled = true;
            SelectedFileName = openFileDialog.FileName;
            textBox1.Text = openFileDialog.SafeFileName;
            sWiCi = new SumWiCi(SelectedFileName);
            richTextBox1.Text = sWiCi.TasksToString();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            textBox2.Text = sWiCi.GetSumWiCiFromBehind().ToString();
            textBox3.Text = sWiCi.PermToString();
        }
    }
}
