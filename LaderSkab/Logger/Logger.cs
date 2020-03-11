using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaderSkab.Logger
{
    public class ClosetLog : ILog
    {
        public DateTime Date { get; set; }
        public string Log { get; set; }
    }

    public class Logger : ILogger
    {
        //Private field
        //private List<ILog> _logs = new List<ILog>();
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        //Methods
        public void Add(ILog log)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(log.Date + log.Log);
            }
            //_logs.Add(log);
        }

        //public List<ILog> Get()
        public string Get()
        {
            return logFile;
        }

        //public int Length => _logs.Count;

        public void Clear()
        {
            if (File.Exists(logFile))
            {
                // If file found, delete it    
                File.Delete(logFile);
            }

            //_logs.Clear();
        }
    }
}
