
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SalesReporter.Models;

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

            folderDlg.SelectedPath = isInput ? @"C:\temp\AndroidPixika\Android_Reports" : @"C:\temp\AndroidPixika\Windows_Reports"; //txtInput.Text : txtOutput.Text;

            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (isInput)
                {
                    txtInput.Text = folderDlg.SelectedPath;
                    if (string.IsNullOrEmpty(txtOutput.Text))
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

        private void txt_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(txtInput.Text.Trim()) && !string.IsNullOrEmpty(txtOutput.Text.Trim());
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Reporter.CreateReports(txtInput.Text);
        }
    }
}
