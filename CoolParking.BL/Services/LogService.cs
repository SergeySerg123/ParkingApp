// TODO: implement the LogService class from the ILogService interface.
//       One explicit requirement - for the read method, if the file is not found, an InvalidOperationException should be thrown
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in LogServiceTests you can find the necessary constructor format.
using CoolParking.BL.Interfaces;
using System;
using System.IO;

namespace CoolParking.BL.Services
{
    public class LogService : ILogService
    {
        public LogService(string logPath)
        {
            LogPath = logPath;
        }

        public string LogPath { get; }

        public string Read()
        {
            try
            {
                string actual;
                using (var file = new StreamReader(LogPath))
                {
                    actual = file.ReadToEnd();
                }
                return actual;
            } catch (Exception ex)
            {
                throw new InvalidOperationException();
            }                    
        }

        public void Write(string logInfo)
        {
            using (StreamWriter sw = new StreamWriter(LogPath, true))
            {
                sw.WriteLine(logInfo);
            }
        }
    }
}