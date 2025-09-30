using Viewer.PoC.Core.Logging;

namespace Viewer.PoC.uTests
{
    public class ConsoleLoggerTests
    {
        readonly ILogging logger = new ConsoleLogger();
        [Test]
        public void Info_WritesMessage()
        {           
            Assert.DoesNotThrow(() => logger.Info("Test info"));
        }

        [Test]
        public void Warn_WritesMessage()
        {
            Assert.DoesNotThrow(() => logger.Warn("Test warn"));
        }

        [Test]
        public void Error_WritesMessageAndException()
        {
            Assert.DoesNotThrow(() => logger.Error("Test error", new Exception("Test exception")));
        }
    }
}