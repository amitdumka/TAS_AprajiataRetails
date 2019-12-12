using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetailsService.RESTAPI.Utils
{
    public class LogEvent
    {
        static LogEvent()
        {
            eventLog = new System.Diagnostics.EventLog()
            {
                Source = "AprajitaRetailsService",
                Log = "AprajitaRetailsLog"
            };
            if (!System.Diagnostics.EventLog.SourceExists("AprajitaRetailsService"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "AprajitaRetailsService", "AprajitaRetailsLog");
            }
            eventLog.WriteEntry("LogEvent: LogEntry Constructer called");
        }

        public static System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog()
        {
            Source = "AprajitaRetailsService",
            Log = "AprajitaRetailsLog"
        };

        public static void WriteEvent(string entryLog)
        {
            eventLog.WriteEntry(entryLog);
        }
    }
}
