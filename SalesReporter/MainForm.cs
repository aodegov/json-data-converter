
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

            folderDlg.SelectedPath = isInput ? txtInput.Text : string.IsNullOrEmpty(txtOutput.Text) ? txtInput.Text : txtOutput.Text; // @"C:\temp\AndroidPixika\Android_Reports" : @"C:\temp\AndroidPixika\Windows_Reports";

            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (isInput)
                {
                    txtInput.Text = folderDlg.SelectedPath;
                    btnOutputBrowse.Focus();
                }
                else
                {
                    txtOutput.Text = folderDlg.SelectedPath;
                   
                    btnOK.Focus();
                }

                if(!string.IsNullOrEmpty(txtInput.Text) && !string.IsNullOrEmpty(txtOutput.Text))
                {
                    SetDefultStatus(null, false);
                }
                else if (string.IsNullOrEmpty(txtOutput.Text))
                {
                    SetDefultStatus("Please, select output folder...");
                }
            }

        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(txtInput.Text.Trim()) && !string.IsNullOrEmpty(txtOutput.Text.Trim());
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Reporter r = new Reporter();
                var result = r.CreateReports(txtInput.Text, txtOutput.Text);
                if (result)
                {
                    SetDefultStatus(null, false); //SetDefultStatus("Successfully created", false);
                }
            }
            catch (Exception ex)
            {
                SetDefultStatus(ex.Message);
            }
            
        }
    }
}
