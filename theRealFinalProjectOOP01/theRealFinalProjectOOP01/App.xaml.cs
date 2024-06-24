using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace theRealFinalProjectOOP01
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {//(2022110805)
        public App()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;

            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);

            TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(Application_DispatcherUnhandledException2);
        }

        //(2022110805)
        private static void writeMessage(Exception e, string srWhichHandle)
        {
            //.? means null check. if not null
            //string srMsg;
            //if (e.InnerException != null)
            //    srMsg = e.InnerException?.Message;

            string srMsg = e.InnerException?.Message;
            //2019103039
            if (!string.IsNullOrEmpty(srMsg))
            {
                srMsg += "\r\n\r\nStack\r\n" + e.InnerException?.StackTrace;
            }

            if (string.IsNullOrEmpty(srMsg))
            {
                srMsg = e.Message + "\r\n\r\nStack\r\n" + e.StackTrace;
            }

            srMsg += $"\r\n\r\n****{srWhichHandle}*****\r\n\r\n";

            File.AppendAllText("global_errors.txt", srMsg);

        }
        //(2022110805)
        private static void Application_DispatcherUnhandledException2(object o, UnobservedTaskExceptionEventArgs e)
        {
            writeMessage(e.Exception, "UnobservedTaskException");
        }
        //(2022110805)
        private static void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            writeMessage(e.Exception, "DispatcherUnhandledExceptionEventHandler");
        }
        //(2022110805)
        private static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            writeMessage(e, "UnhandledExceptionEventHandler");
        }



    }

}
