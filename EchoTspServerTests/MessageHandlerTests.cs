using NUnit.Framework;
using Moq;
using EchoServer;
using EchoServer.Interfaces;
using FluentAssertions;
using System;

namespace EchoTspServerTests
{
    [TestFixture]
    public class MessageHandlerTests
    {
        private Mock<ILogger> _loggerMock;
        private MessageHandler _handler;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger>();
            _handler = new MessageHandler(_loggerMock.Object);
        }

        [Test]
        public void ProcessMessage_WithValidData_ReturnsEchoedData()
        {
            // Arrange
            byte[] message = new byte[] { 0x01, 0x02, 0x03 };

            // Act
            var result = _handler.ProcessMessage(message);

            // Assert
            result.Should().BeEquivalentTo(message);
            _loggerMock.Verify(l => l.Log(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ProcessMessage_WithEmptyArray_ReturnsEmptyArray()
        {
            // Arrange
            byte[] message = Array.Empty<byte>();

            // Act
            var result = _handler.ProcessMessage(message);

            // Assert
            result.Should().BeEmpty();
            _loggerMock.Verify(l => l.Log(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ProcessMessage_WithNull_ReturnsEmptyArray()
        {
            // Act
            var result = _handler.ProcessMessage(null);

            // Assert
            result.Should().BeEmpty();
            _loggerMock.Verify(l => l.Log(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ProcessMessage_WithLargeData_ReturnsEchoedData()
        {
            // Arrange
            byte[] message = new byte[1024];
            for (int i = 0; i < message.Length; i++)
            {
                message[i] = (byte)(i % 256);
            }

            // Act
            var result = _handler.ProcessMessage(message);

            // Assert
            result.Should().BeEquivalentTo(message);
        }

        [Test]
        public void Constructor_WithNullLogger_ThrowsException()
        {
            // Act
            Action act = () => new MessageHandler(null);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("logger");
        }
    }
}