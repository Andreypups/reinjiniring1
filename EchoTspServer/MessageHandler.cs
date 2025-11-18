using System;
using EchoServer.Interfaces;

namespace EchoServer
{
    public class MessageHandler
    {
        private readonly ILogger _logger;

        public MessageHandler(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public byte[] ProcessMessage(byte[] message)
        {
            if (message == null || message.Length == 0)
            {
                return Array.Empty<byte>();
            }

            _logger.Log($"Processing message of {message.Length} bytes");
            
            // Echo logic - повертає те ж саме повідомлення
            return message;
        }
    }
}