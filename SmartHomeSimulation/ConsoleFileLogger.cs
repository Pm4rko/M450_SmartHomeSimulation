using System;
using System.IO;

namespace M320_SmartHome {
    public class ConsoleFileLogger : ILogger {
        private readonly string? filePath;
        public ConsoleFileLogger(string? filePath = null) {
            this.filePath = filePath;
        }
        public void Log(string message) {
            Console.WriteLine(message);
            if (!string.IsNullOrEmpty(filePath)) {
                File.AppendAllText(filePath, message + Environment.NewLine);
            }
        }
    }
}
