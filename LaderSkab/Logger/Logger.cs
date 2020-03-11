using System;
using System.Collections.Generic;
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
        private List<ILog> _logs = new List<ILog>();

        //Methods
        public void Add(ILog log)
        {
            _logs.Add(log);
        }

        public List<ILog> Get()
        {
            return _logs;
        }

        public int Length => _logs.Count;

        public void Clear()
        {
            _logs.Clear();
        }
    }
}
