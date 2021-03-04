using GFEAppManager.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GFEAppManager
{
    public class MainAppContext : ApplicationContext
    {
        public NotifyIcon trayIcon;
        private ContextMenuStrip TheContextMenu;

        public MainAppContext()
        {
            TheContextMenu = new ContextMenuStrip();
            TheContextMenu.Items.Add("Application Status: Running", System.Drawing.SystemIcons.Information.ToBitmap(), null);
            TheContextMenu.Items.Add("GFE Status: ", System.Drawing.SystemIcons.Information.ToBitmap(), null);
            TheContextMenu.Items.Add("Triggering Apps", null, (sender, eventArgs) => new TriggeringAppListForm().Show());
            TheContextMenu.Items.Add("Open at startup", null, (sender, eventArgs) => TaskSchedulerOperations.AddToTS(System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.Length - 4) + ".exe"));
            TheContextMenu.Items.Add("Open at startup", null, (sender, eventArgs) => TaskSchedulerOperations.RemoveFromTS());
            TheContextMenu.Items.Add("Manage Applications", null, (sender, eventArgs) => new ManageApplicationsForm().Show());
            TheContextMenu.Items.Add("Exit", System.Drawing.SystemIcons.Error.ToBitmap(), (sender, eventArgs) => { 
                // Hide tray icon, otherwise it will remain shown until user mouses over it
                trayIcon.Visible = false;

                Application.Exit();
            });

            TheContextMenu.Items[0].Enabled = false;
            TheContextMenu.Items[1].Enabled = false;

            ((ToolStripMenuItem)TheContextMenu.Items[4]).Checked = true;

            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenuStrip = TheContextMenu,
                Visible = true
            };

            trayIcon.Click += TrayIcon_Click;
        }

        private void TrayIcon_Click(object sender, EventArgs e)
        {
            TheContextMenu.Items[1].Text = "GFE Status: ";

            if (Program.TheServiceOperations.CheckService())
                TheContextMenu.Items[1].Text += "Running";
            else
                TheContextMenu.Items[1].Text += "Stopped";

            TheContextMenu.Items[2].Enabled = true;

            (TheContextMenu.Items[2] as ToolStripMenuItem).DropDownItems.Clear();

            foreach (string RunningApp in Program.TheServiceOperations.RunningApps.Values.Distinct())
            {
                Process TheProc = Process.GetProcessesByName(RunningApp[0..^4]).First();
                Icon TheIcon = Icon.ExtractAssociatedIcon(TheProc.MainModule.FileName);
                (TheContextMenu.Items[2] as ToolStripMenuItem).DropDownItems.Add(RunningApp, TheIcon.ToBitmap(), (sender, eventArgs) => {
                    ExternalOperations.SetForegroundWindow(TheProc.MainWindowHandle);
                    ExternalOperations.ShowWindowAsync(TheProc.MainWindowHandle, (int)ExternalOperations.ShowWindowAsyncModes.SW_SHOWNORMAL);
                });
            }

            if ((TheContextMenu.Items[2] as ToolStripMenuItem).DropDownItems.Count == 0)
                TheContextMenu.Items[2].Enabled = false;

            // It is a bad practice, but clearing the events from EventHandler is much more complex than I thought.
            if (TaskSchedulerOperations.IsItInTS())
            {
                TheContextMenu.Items[3].Visible = false;
                TheContextMenu.Items[4].Visible = true;
            }
            else
            {
                TheContextMenu.Items[3].Visible = true;
                TheContextMenu.Items[4].Visible = false;
            }
        }



    }
}
