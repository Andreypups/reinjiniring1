using NUnit.Framework;
using EchoServer;
using EchoServer.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using FluentAssertions;

namespace EchoTspServerTests
{
    [TestFixture]
    public class EchoServerTests
    {
        private Mock<ILogger> _loggerMock;
        private MessageHandler _messageHandler;
        private EchoServer.EchoServer _server;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger>();
            _messageHandler = new MessageHandler(_loggerMock.Object);
            _server = new EchoServer.EchoServer(8080, _loggerMock.Object, _messageHandler);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                _server?.Stop();
            }
            catch (ObjectDisposedException)
            {
                // Ігноруємо, якщо вже disposed
            }
        }

        [Test]
        public async Task StartAsync_SetsIsRunningState()
        {
            // Arrange & Act
            var startTask = Task.Run(() => _server.StartAsync());
            await Task.Delay(100);

            // Assert
            _loggerMock.Verify(l => l.Log(It.Is<string>(s => s.Contains("Server started"))), Times.Once);
        }

        [Test]
        public void Stop_WhenNotStarted_DoesNotThrow()
        {
            // Arrange - сервер не запущений

            // Act
            Action act = () => _server.Stop();

            // Assert
            act.Should().NotThrow();
        }
    }
}