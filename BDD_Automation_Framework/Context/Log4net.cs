using log4net;
using System;
using log4net.Layout;

namespace Logging
{
    public class Logger : PatternLayout
    {
        private readonly ILog log = null;

        public override string Header
        {
            get
            {
                var dateString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return string.Format("[START:  {0} ]\r\n", dateString);
            }
        }

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