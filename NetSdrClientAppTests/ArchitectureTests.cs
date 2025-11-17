using NetArchTest.Rules;
using NUnit.Framework;

namespace NetSdrClientAppTests
{
    [TestFixture]
    public class ArchitectureTests
    {
        [Test]
        public void Messages_ShouldNotDependOn_Networking()
        {
            // Arrange
            var assembly = typeof(NetSdrClientApp.NetSdrClient).Assembly;

            // Act
            var result = Types.InAssembly(assembly)
                .That()
                .ResideInNamespace("NetSdrClientApp.Messages")
                .ShouldNot()
                .HaveDependencyOn("NetSdrClientApp.Networking")
                .GetResult();

            // Assert
            Assert.IsTrue(result.IsSuccessful, 
                $"Messages layer should not depend on Networking layer. " +
                $"Violations: {string.Join(", ", result.FailingTypeNames ?? new string[0])}");
        }

        [Test]
        public void NetworkingClasses_ShouldBeInternal()
        {
            // Arrange
            var assembly = typeof(NetSdrClientApp.NetSdrClient).Assembly;

            // Act
            var result = Types.InAssembly(assembly)
                .That()
                .ResideInNamespace("NetSdrClientApp.Networking")
                .And()
                .AreClasses()
                .Should()
                .NotBePublic()
                .GetResult();

            // Assert
            Assert.IsTrue(result.IsSuccessful,
                $"Networking classes should be internal (encapsulation). " +
                $"Violations: {string.Join(", ", result.FailingTypeNames ?? new string[0])}");
        }

        [Test]
        public void Interfaces_ShouldStartWithI()
        {
            // Arrange
            var assembly = typeof(NetSdrClientApp.NetSdrClient).Assembly;

            // Act
            var result = Types.InAssembly(assembly)
                .That()
                .AreInterfaces()
                .Should()
                .HaveNameStartingWith("I")
                .GetResult();

            // Assert
            Assert.IsTrue(result.IsSuccessful,
                $"All interfaces should start with 'I'. " +
                $"Violations: {string.Join(", ", result.FailingTypeNames ?? new string[0])}");
        }

        [Test]
        public void HelperClasses_ShouldBeStatic()
        {
            // Arrange
            var assembly = typeof(NetSdrClientApp.NetSdrClient).Assembly;

            // Act
            var result = Types.InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Helper")
                .Should()
                .BeStatic()
                .GetResult();

            // Assert
            Assert.IsTrue(result.IsSuccessful,
                $"Helper classes should be static. " +
                $"Violations: {string.Join(", ", result.FailingTypeNames ?? new string[0])}");
        }
    }
}