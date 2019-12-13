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
        public static void Error(string entryLog)
        {
            eventLog.WriteEntry(entryLog, System.Diagnostics.EventLogEntryType.Error);
        }
        public static void Info(string entryLog)
        {
            eventLog.WriteEntry(entryLog, System.Diagnostics.EventLogEntryType.Information);
        }
        public static void Warning(string entryLog)
        {
            eventLog.WriteEntry(entryLog, System.Diagnostics.EventLogEntryType.Warning);
        }
    }
}
