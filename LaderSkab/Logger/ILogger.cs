using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaderSkab.Logger
{
    public interface ILog
    {
        DateTime Date { get; set; }
        string Log { get; set; }
    };

    public interface ILogger
    {
        void Add(ILog log);             //Inserts one log into Logger
        List<ILog> Get();               //Gets list containing logs
        int Length { get; }             //Returns length of log list
        void Clear();                   //Removes all logs from Logger
    };
}
