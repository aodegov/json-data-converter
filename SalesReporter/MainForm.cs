using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesReporter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetDefultStatus("Please, select input folder...");
        }

        private void SetDefultStatus(string msg = null, bool isRed = true)
        {
            lblStatus.Text = msg ?? "Ready to create report...";
            lblStatus.ForeColor = (isRed) ? Color.Red : Color.Green;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            bool isInput = ((Button)sender) == btnInputBrowse;

            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            folderDlg.ShowNewFolderButton = true;

            folderDlg.SelectedPath = isInput ? txtInput.Text : txtOutput.Text;

            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (isInput)
                {
                    txtInput.Text = folderDlg.SelectedPath;
                    SetDefultStatus("Please, select output folder...");
                    btnOutputBrowse.Focus();
                }
                else
                {
                    txtOutput.Text = folderDlg.SelectedPath;
                    SetDefultStatus(null, false);
                    btnOK.Focus();
                }
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
