using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GFEAppManager
{
    class Program
    {
        public static readonly ConcurrentDictionary<string, string> ListOfApplicationsToDisableService = new ConcurrentDictionary<string, string>(); // Maybe it is a bad practice, but who cares rn.
        public static readonly ServiceOperations TheServiceOperations = new ServiceOperations("NvContainerLocalSystem");
        public static readonly string ConfigFileLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\list_of_processes.txt";
        public static readonly string ExceptionLogFilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\exception_log.txt";
        public static MainAppContext TheMainAppContext;

        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionMessageBox);
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadUnhandledExceptionMessageBox);

            if (args.Length >= 1)
            {
                if (args[0] == "startup")
                {
                    // Lets make sure that the tray icon is visible after login by restarting the app with significant delay
                    ProcessStartInfo Info = new ProcessStartInfo();
                    string ThePath = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.Length - 4) + ".exe";
                    Info.Arguments = "/C ping 127.0.0.1 -n 4 && start \"\" \"" + ThePath +  "\"";
                    Info.WindowStyle = ProcessWindowStyle.Hidden;
                    Info.CreateNoWindow = true;
                    Info.FileName = "cmd.exe";
                    Process.Start(Info);
                    System.Environment.Exit(0);
                }
            }

            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show(owner: null, text: "An instance of application is already running!", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                System.Environment.Exit(1);
            }

            try
            {
                TheServiceOperations.CheckService();
            } catch (InvalidOperationException)
            {
                MessageBox.Show(owner: null, text: "Could not find the GeForce® Experience™ service!", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                System.Environment.Exit(1);
            }

            try
            {
                string[] Lines = File.ReadAllLines(ConfigFileLocation);

                foreach (string ProcName in Lines)
                {
                    if (!ProcName.EndsWith(".exe"))
                    {
                        MessageBox.Show(owner: null, text: "You need to enter a process with .exe extension!", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                        System.Environment.Exit(1);
                    }

                    if (ListOfApplicationsToDisableService.Keys.Any(x => x.ToLower() == ProcName.ToLower()))
                    {
                        MessageBox.Show(owner: null, text: "The process is already in list!", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                        System.Environment.Exit(1);
                    }

                    ListOfApplicationsToDisableService.TryAdd(ProcName, ProcName);
                }
            } catch (FileNotFoundException)
            {
                // Default example app list
                ListOfApplicationsToDisableService.TryAdd("vmplayer.exe", "vmplayer.exe");
                ListOfApplicationsToDisableService.TryAdd("WindowsTerminal.exe", "WindowsTerminal.exe");
                SaveConfigFile();
            }

            TheMainAppContext = new MainAppContext();

            TheServiceOperations.BeginListening();

            Application.Run(TheMainAppContext);
        }

        public static void SaveConfigFile()
        {
            File.WriteAllLines(ConfigFileLocation, ListOfApplicationsToDisableService.Keys);
        }

        private static void UIThreadUnhandledExceptionMessageBox(object sender, ThreadExceptionEventArgs e)
        {
            File.AppendAllText(ExceptionLogFilePath,
                Environment.NewLine + DateTime.Now.ToString() + Environment.NewLine + e.Exception.ToString() + Environment.NewLine);

            try
            {
                MessageBox.Show(e.Exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal exception happend inside UIThreadException handler",
                        "Fatal Windows Forms Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Here we can decide if we want to end our application or do something else
            Application.Exit();
        }

        private static void UnhandledExceptionMessageBox(object sender, UnhandledExceptionEventArgs e)
        {
            File.AppendAllText(ExceptionLogFilePath,
                Environment.NewLine + DateTime.Now.ToString() + Environment.NewLine + ((Exception)e.ExceptionObject).ToString() + Environment.NewLine);

            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal exception happend inside UnhadledExceptionHandler: \n\n"
                        + exc.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Application.Exit();
                }
            }

            Application.Exit(); // This can be unnecessary
        }
    }

}
