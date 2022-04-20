using System;

namespace WebApi.Services
{
    public class DbLogger : ILoggerSevice
    {
        public void Write(string message)
        {
            Console.WriteLine("DbLogger - "+message);
        }
    }
}