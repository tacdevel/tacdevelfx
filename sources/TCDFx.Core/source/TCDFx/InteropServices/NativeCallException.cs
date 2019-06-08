/***************************************************************************************************
 * FileName:             NativeCallException.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace TCDFx.InteropServices
{
    /// <summary>
    /// The exception that is thrown when the <see cref="NativeCalls.Load"/> method fails.
    /// </summary>
    [Serializable]
    public class NativeCallException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NativeCallException"/> class.
        /// </summary>
        public NativeCallException() : base("") { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="NativeCallException"/> class
        ///  with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NativeCallException(string message) : base(message) { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="NativeCallException"/> class
        ///  with a specified error message and a reference to the inner exception that is
        ///  the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public NativeCallException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeCallException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public NativeCallException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}