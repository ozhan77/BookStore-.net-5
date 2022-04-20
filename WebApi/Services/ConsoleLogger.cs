using System;

namespace WebApi.Services
{
    public class ConsoleLogger : ILoggerSevice
    {
        public void Write(string message)
        {
            Console.WriteLine("Consolelogger -"+message);
        }
    }
}