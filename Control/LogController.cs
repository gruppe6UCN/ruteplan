using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Control
{
    public class LogController
    {
        private static LogController instance;

        private int maxLogLength = 1000;
        private Collection<String> logReceiver = null;

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static LogController Instance { 
                get { 
                    if (instance == null)
                        instance = new LogController();
                    return instance;
                }
            }

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private LogController() {
        }

        public void setLogReceiver(Collection<String> logReceiver)
        {
            this.logReceiver = logReceiver;
        }

        public void setMaxLogLength(int maxLogLength) {
            this.maxLogLength = maxLogLength;
        }


        public void StatusLog(String log) {
            //if (logReceiver != null) {
            //        //logReceiver.Insert(0, log);
            //        //maintainLog();
            //} else {
            //    Console.WriteLine(log);
            //}
        }

        private void maintainLog() {
            if (logReceiver.Count > maxLogLength) {
                logReceiver.Remove(logReceiver.Last());
            }
        }

        public string GetLatest()
        {
            try
            {
                string latest = logReceiver[logReceiver.Count - 1];
                return latest;
            }
            catch (NullReferenceException) { return ""; }
        }
    }
}
