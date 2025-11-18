using System;
using EchoServer.Interfaces;

namespace EchoServer
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}