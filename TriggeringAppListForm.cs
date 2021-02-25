using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GFEAppManager
{
    public partial class TriggeringAppListForm : Form
    {
        public TriggeringAppListForm()
        {
            InitializeComponent();
            this.TriggeringAppsDataGrid.Columns.Add(new DataGridViewImageColumn());
            this.TriggeringAppsDataGrid.Columns.Add(new DataGridViewTextBoxColumn());
            this.TriggeringAppsDataGrid.Columns.Add(new DataGridViewTextBoxColumn());

            this.TriggeringAppsDataGrid.Columns[0].Name = "Icon";
            this.TriggeringAppsDataGrid.Columns[1].Name = "Executable";
            this.TriggeringAppsDataGrid.Columns[2].Name = "Process ID";

            this.TriggeringAppsDataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.TriggeringAppsDataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.TriggeringAppsDataGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            foreach (int RunningAppProcID in Program.TheServiceOperations.RunningApps.Keys)
            {
                Process TheProc = Process.GetProcessById(RunningAppProcID);
                Icon TheIcon = Icon.ExtractAssociatedIcon(TheProc.MainModule.FileName);
                this.TriggeringAppsDataGrid.Rows.Add(new Bitmap(TheIcon.ToBitmap(), 16, 16), Program.TheServiceOperations.RunningApps[RunningAppProcID], RunningAppProcID);
            }
        }
    }
}
