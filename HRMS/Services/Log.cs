/*using NLog;

namespace HRMS.Services
{
    public class Log : ILog
    {
        private readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public void Debug(string message)
        {
            logger.Info(message);
        }

        public void Error(string message)
        {
            logger.Warn(message);
        }

        public void Information(string message)
        {
            logger.Debug(message);
        }

        public void Warning(string message)
        {
            logger.Error(message);
        }
    }
}
*/