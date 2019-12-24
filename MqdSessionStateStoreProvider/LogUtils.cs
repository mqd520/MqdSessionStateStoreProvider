using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace MqdSessionStateStoreProvider
{
    public static class LogUtils
    {
        private static readonly ILog logger;

        static LogUtils()
        {
            logger = LogManager.GetLogger("info");
        }

        public static void WriteInfo(object message, Exception e = null)
        {
            logger.Info(message, e);
        }
    }
}
