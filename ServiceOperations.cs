using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Threading.Tasks;


namespace GFEAppManager
{
    class ServiceOperations
    {
        private class DataOpException : Exception
        {
            public DataOpException() : base() { }
        }

        public readonly ConcurrentDictionary<int, string> RunningApps;
        private readonly int timeoutMilliseconds;
        private readonly ServiceController TheService;
        private readonly ManagementEventWatcher startWatch;

        public ServiceOperations(string ServiceName)
        {
            RunningApps = new ConcurrentDictionary<int, string>();
            timeoutMilliseconds = 2000;
            TheService = new ServiceController(ServiceName);
            startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
        }

        public bool CheckService()
        {
            if (TheService.Status == ServiceControllerStatus.Running)
                return true;
            else
                return false;
        }

        private void StopService()
        {
            Debug.WriteLine("StopService has arrived!");
           
            try
            {
                TheService.Stop();
            } 
            catch (InvalidOperationException) { 
                Debug.WriteLine("StopService: Service was not running"); 
            }

            bool isSuccess = false;

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    TheService.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMilliseconds(timeoutMilliseconds));
                    isSuccess = true;
                    break;
                } 
                catch (System.ServiceProcess.TimeoutException)
                {
                    Debug.WriteLine("StopService: Service is not stopped!");
                    continue;
                }
            }

            if (!isSuccess)
            {
                Debug.WriteLine("StopService: Could not stop the service!");
                Program.TheMainAppContext.trayIcon.ShowBalloonTip(0, "GFE could not be stopped!", " ", System.Windows.Forms.ToolTipIcon.Error);
            }
            else
            {
                Program.TheMainAppContext.trayIcon.ShowBalloonTip(0, "GFE has been stopped!", "Triggered By: " + RunningApps.First().Value, System.Windows.Forms.ToolTipIcon.Info);
            }
            
        }

        private void StartService()
        {
            Debug.WriteLine("StartService has arrived!");

            try
            {
                TheService.Start();
            }
            catch (InvalidOperationException)
            {
                Debug.WriteLine("StartService: Service was already running");
            }

            bool isSuccess = false;

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    TheService.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(timeoutMilliseconds));
                    isSuccess = true;
                    break;
                }
                catch (System.ServiceProcess.TimeoutException)
                {
                    Debug.WriteLine("StartService: Service is not running!");
                    continue;
                }
            }

            if (!isSuccess)
            {
                Debug.WriteLine("StartService: Could not start the service!");
                Program.TheMainAppContext.trayIcon.ShowBalloonTip(0, "GFE could not be started!", " ", System.Windows.Forms.ToolTipIcon.Error);
            }
            else
            {
                Program.TheMainAppContext.trayIcon.ShowBalloonTip(0, "GFE has been started!", " ", System.Windows.Forms.ToolTipIcon.Info);
            }
        }

        public void BeginListening()
        {
            startWatch.EventArrived += new EventArrivedEventHandler(StartWatch_EventArrived);
            startWatch.Start();
        }

        private void StartWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (Program.ListOfApplicationsToDisableService.Keys.Any(x => x.ToLower() == e.NewEvent.Properties["ProcessName"].Value.ToString().ToLower()))
                AppStarted_Event(e.NewEvent);
        }

        private void AppStarted_Event(ManagementBaseObject NewEvent)
        {
            Debug.WriteLine("AppStarted_Event has arrived!");
            try
            {
                int ProcID = Convert.ToInt32(NewEvent.Properties["ProcessId"].Value.ToString());
                string ProcName = NewEvent.Properties["ProcessName"].Value.ToString();
                var Process = System.Diagnostics.Process.GetProcessById(ProcID);
                if (RunningApps.TryAdd(ProcID, ProcName))
                    Debug.WriteLine("AppStarted_Event: RunningApps.TryAdd(ProcID, ProcName) succeed!");
                else
                    throw new DataOpException();
                if (RunningApps.Count == 1)
                    StopService();
                Task.Run(() => Process.WaitForExit()).ContinueWith(t => AppClosed_Event(ProcID));
            }
            catch (ArgumentException) { Debug.WriteLine("AppStarted_Event: Could not catch up!"); }
        }

        private void AppClosed_Event(int ProcID)
        {
            Debug.WriteLine("AppClosed_Event has arrived!");

            if (!RunningApps.TryRemove(ProcID, out _))
                throw new DataOpException();

            if (RunningApps.Count == 0)
                StartService();

        }
    }
}
