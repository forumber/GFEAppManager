using GFEAppManager.Properties;
using System;
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
            //TheContextMenu.Items.Add("Notification Test", null, (sender, eventArgs) => trayIcon.ShowBalloonTip(100, "test", "test", ToolTipIcon.Info));
            TheContextMenu.Items.Add("Manage Applications", null, (sender, eventArgs) => new ManageApplicationsForm().Show());
            TheContextMenu.Items.Add("Exit", System.Drawing.SystemIcons.Error.ToBitmap(), (sender, eventArgs) => { 
                // Hide tray icon, otherwise it will remain shown until user mouses over it
                trayIcon.Visible = false;

                Application.Exit();
            });

            TheContextMenu.Items[0].Enabled = false;
            TheContextMenu.Items[1].Enabled = false;

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
        }
    }
}
