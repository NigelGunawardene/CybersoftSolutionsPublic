using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IAppLogger<T>
    {
        /// <summary>
        /// LogInformation
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogInformation(string message, params object[] args);

        /// <summary>
        /// LogWarning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogWarning(string message, params object[] args);

        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void LogError(string message, params object[] args);
    }
}

