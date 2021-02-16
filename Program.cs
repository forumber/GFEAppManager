using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;

namespace GFEAppManager
{
    class Program
    {
        public static ConcurrentDictionary<string, string> ListOfApplicationsToDisableService = new ConcurrentDictionary<string, string>(); // Maybe it is a bad practice, but who cares rn.
        public static readonly ServiceOperations TheServiceOperations = new ServiceOperations("NvContainerLocalSystem");
        public static MainAppContext TheMainAppContext;

        [STAThread]
        static void Main(string[] args)
        {
            ListOfApplicationsToDisableService.TryAdd("test.exe", "test.exe");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TheMainAppContext = new MainAppContext();

            TheServiceOperations.BeginListening();

            Application.Run(TheMainAppContext);
        }
    }

}
