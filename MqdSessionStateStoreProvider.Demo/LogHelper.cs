using System;
using System.Web;
using System.IO;
using log4net;

namespace MqdSessionStateStoreProvider.Demo
{
    /// <summary>
    /// Log helper
    /// </summary>
    public static class LogHelper
    {
        private static ILog infoLog = null;     // infor logger obj
        private static ILog debugLog = null;    // debug logger obj
        private static ILog warnLog = null;     // warnn logger obj
        private static ILog errorLog = null;    // error logger obj
        private static ILog fatalLog = null;    // fatal logger obj
        private static ILog exLog = null;       // excep logger obj
        private static ILog hrLog = null;       // http request logger obj

        static LogHelper()
        {
            infoLog = LogManager.GetLogger("info");
            debugLog = LogManager.GetLogger("debug");
            warnLog = LogManager.GetLogger("warn");
            errorLog = LogManager.GetLogger("error");
            fatalLog = LogManager.GetLogger("fatal");
            exLog = LogManager.GetLogger("exception");
            hrLog = LogManager.GetLogger("httprequest");
        }

        /// <summary>
        /// Write log
        /// </summary>
        /// <param name="log">log</param>
        /// <param name="type">log type</param>
        public static void WriteLine(object log, ELogType type = ELogType.Info, Exception e = null)
        {
            if (type == ELogType.Info)
            {
                infoLog.Info(log, e);
            }
            else if (type == ELogType.Warn)
            {
                if (e != null)
                {
                    warnLog.Warn(log, e);
                }
                else
                {
                    warnLog.Warn(log);
                }
            }
            else if (type == ELogType.Debug)
            {
                if (e != null)
                {
                    debugLog.Debug(log, e);
                }
                else
                {
                    debugLog.Debug(log);
                }
            }
            else if (type == ELogType.Error)
            {
                if (e != null)
                {
                    errorLog.Error(log, e);
                }
                else
                {
                    errorLog.Error(log);
                }
            }
            else if (type == ELogType.Fatal)
            {
                if (e != null)
                {
                    fatalLog.Fatal(log, e);
                }
                else
                {
                    fatalLog.Fatal(log);
                }
            }
            else if (type == ELogType.Exception)
            {
                if (e != null)
                {
                    exLog.Info(log, e);
                }
                else
                {
                    exLog.Info(log);
                }
            }
        }

        public static void WriteHttpRequest(HttpRequest hr, HttpResponse hres)
        {
            string log = "Recv a http request: " + Environment.NewLine;
            if (hr != null)
            {
                log += "Request: " + Environment.NewLine;
                log += "Url: " + hr.Url.AbsoluteUri + Environment.NewLine;
                log += "Method: " + hr.HttpMethod + Environment.NewLine;
                log += "Client Ip: " + hr.ServerVariables["REMOTE_ADDR"].ToString() + Environment.NewLine;
                if (hr.HttpMethod == "POST")
                {
                    log += "param: " + Environment.NewLine;
                    foreach (var item in hr.Form.AllKeys)
                    {
                        log += item + ": " + hr.Form[item].ToString() + Environment.NewLine;
                    }
                }
            }

            if (hres != null)
            {
                log += Environment.NewLine + "Response: " + Environment.NewLine;
                log += "Status: " + hres.StatusCode + " " + hres.StatusDescription + Environment.NewLine;
            }

            hrLog.Info(log);
        }
    }
}