using log4net;
using System;

namespace Logging
{
    public class Logger
    {
        private readonly ILog log = null;

        public Logger()
        {
            log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        public void Info(object message)
        {
            log.Info(message);
        }
    }
}