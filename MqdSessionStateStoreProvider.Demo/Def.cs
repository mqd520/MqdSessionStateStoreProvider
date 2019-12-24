using System;

namespace MqdSessionStateStoreProvider.Demo
{
    #region Enum

    /// <summary>
    /// Log Type
    /// </summary>
    public enum ELogType : int
    {
        /// <summary>
        /// Info
        /// </summary>
        Info,

        /// <summary>
        /// Debug
        /// </summary>
        Debug,

        /// <summary>
        /// Warn
        /// </summary>
        Warn,

        /// <summary>
        /// Error
        /// </summary>
        Error,

        /// <summary>
        /// Fatal
        /// </summary>
        Fatal,

        /// <summary>
        /// Exception
        /// </summary>
        Exception,

        /// <summary>
        /// http request
        /// </summary>
        HttpRequest
    }


    /// <summary>
    /// Return code
    /// </summary>
    public enum EReturnCode
    {
        /// <summary>
        /// Success
        /// </summary>
        Success = 0,





        /// <summary>
        /// None
        /// </summary>
        None = -999
    }

    #endregion
}
