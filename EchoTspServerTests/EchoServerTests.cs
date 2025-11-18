using NUnit.Framework;
using Moq;
using EchoServer;
using EchoServer.Interfaces;
using FluentAssertions;
using System;

namespace EchoTspServerTests
{
    [TestFixture]
    public class EchoServerTests
    {
        private Mock<ILogger> _loggerMock;
        private Mock<MessageHandler> _messageHandlerMock;
        private EchoServer.EchoServer _server;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger>();
            _messageHandlerMock = new Mock<MessageHandler>(_loggerMock.Object);
            _server = new EchoServer.EchoServer(5001, _loggerMock.Object, _messageHandlerMock.Object);
        }

        [Test]
        public void Constructor_WithNullLogger_ThrowsException()
        {
            // Act
            Action act = () => new EchoServer.EchoServer(5000, null, _messageHandlerMock.Object);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("logger");
        }

        [Test]
        public void Constructor_WithNullMessageHandler_ThrowsException()
        {
            // Act
            Action act = () => new EchoServer.EchoServer(5000, _loggerMock.Object, null);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("messageHandler");
        }

        [Test]
        public void Stop_DoesNotThrow()
        {
            // Act
            Action act = () => _server.Stop();

            // Assert
            act.Should().NotThrow();
            _loggerMock.Verify(l => l.Log("Server stopped."), Times.Once);
        }
    }
}