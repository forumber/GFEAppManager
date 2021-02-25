using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;

namespace GFEAppManager
{
    public partial class ManageApplicationsForm : Form
    {
        public ManageApplicationsForm()
        {
            InitializeComponent();
            RefreshListeningAppsList();
            RefreshCurrentlyRunningListButton_Click(null, null); // Maybe it is a bad practice, but who cares rn.
        }

        static bool CheckProcName(string ProcName)
        {
            if (ProcName == "Idle")
                return false;
            if (ProcName == "System")
                return false;
            if (ProcName == "Secure System")
                return false;
            if (ProcName == "Registry")
                return false;
            if (ProcName == "Memory Compression")
                return false;
            if (ProcName == "svchost")
                return false;
            if (ProcName == "conhost")
                return false;

            return true;
        }

        private void RefreshListeningAppsList()
        {
            ListeningAppsList.DataSource = Program.ListOfApplicationsToDisableService.Keys.ToArray();
        }

        private void RefreshCurrentlyRunningListButton_Click(object sender, EventArgs e)
        {
            this.CurrentlyRunningAppsList.Items.Clear();
            Process[] process = Process.GetProcesses();
            var FinalList = process.OrderBy(x => x.ProcessName).GroupBy(x => x.ProcessName).Select(x => x.First()); // Maybe there is a better way to do it, but if it works, it works.
            foreach (Process prs in FinalList)
            {
                if (CheckProcName(prs.ProcessName) && prs.Id != Process.GetCurrentProcess().Id)
                    this.CurrentlyRunningAppsList.Items.Add(prs.ProcessName + ".exe");
            }
        }

        private bool AddToListOfApplicationsToDisableService(string ProcName)
        {
            if (!ProcName.EndsWith(".exe"))
            {
                MessageBox.Show(owner: this, text: "You need to enter a process with .exe extension!", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                return false;
            }

            if (Program.ListOfApplicationsToDisableService.Keys.Any(x => x.ToLower() == ProcName.ToLower()))
            {
                MessageBox.Show(owner: this, text: "The process is already in list!", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                return false;
            }

            Program.ListOfApplicationsToDisableService.TryAdd(ProcName, ProcName);

            RefreshListeningAppsList();

            Program.SaveConfigFile();

            return true;
        }

        private void AddToListButton_Click(object sender, EventArgs e)
        {
            if (AddToListOfApplicationsToDisableService(AppNameTextBox.Text))
                AppNameTextBox.Clear();
        }

        private void RemoveFromTheListButton_Click(object sender, EventArgs e)
        {
            Program.ListOfApplicationsToDisableService.TryRemove(ListeningAppsList.SelectedItem.ToString(), out _);
            RefreshListeningAppsList();
            Program.SaveConfigFile();
        }

        private void AddFromListButton_Click(object sender, EventArgs e)
        {
            AddToListOfApplicationsToDisableService(CurrentlyRunningAppsList.SelectedItem.ToString());
        }

        private void AppNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (AddToListOfApplicationsToDisableService(AppNameTextBox.Text))
                    AppNameTextBox.Clear();
            }
        }
    }
}
