﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LSystem.Multimedia.MCI
{
    /// <summary>
    /// An abstract class that serves as a base class to concrete media classes.
    /// </summary>
    public abstract class Media : IDisposable
    {

        /// <summary>
        /// A reference name for the media.
        /// </summary>
        internal string MediaName { get; set; }

        /// <summary>
        /// Sends a specified command to the MCI Device.
        /// </summary>
        /// <param name="strCommand">Command</param>
        /// <param name="strReturn">Return data.</param>
        /// <param name="iReturnLength">Return data size.</param>
        /// <param name="oCallback">Callback window handle.</param>
        /// <returns>Indicates whether the execution was successful or if any errors occurred.</returns>
        [DllImport("winmm.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr oCallback);

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command to be executed in the format 'command {0} command' where {0} will be replaced by the MediaName property.</param>
        /// <exception cref="MCIException">Throws this exception if any problem occurs during the mciSendString execution.</exception>
        internal void ExecuteMCICommand(string command)
        {
            int errorCode = mciSendString(String.Format(command, this.MediaName), null, 0, IntPtr.Zero);

            if (errorCode != 0)
                throw new MCIException(errorCode);
        }

        /// <summary>
        /// Starts the media execution.
        /// </summary>
        internal void Play()
        {
            ExecuteMCICommand($"play {MediaName}");
        }

        /// <summary>
        /// Pauses the media execution
        /// </summary>
        internal void Pause()
        {
            ExecuteMCICommand($"pause {MediaName}");
        }

        /// <summary>
        /// Resumes the media execution
        /// </summary>
        internal void Resume()
        {
            ExecuteMCICommand($"resume {MediaName}");
        }

        /// <summary>
        /// Stops the media execution.
        /// </summary>
        internal void Stop()
        {
            ExecuteMCICommand($"stop {MediaName}");
        }

        /// <summary>
        /// Closes the media.
        /// </summary>
        internal void Close()
        {
            ExecuteMCICommand($"close {MediaName}");
        }



        /// <summary>
        /// Open have to be implemented because it depends 
        /// of the media type.
        /// </summary>
        internal abstract void Open();


        public abstract void Dispose();
    }
}
