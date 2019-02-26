/***************************************************************************************************
 * FileName:             InvalidHandleException.cs
 * Date:                 20180918
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace TCD.InteropServices
{
    /// <summary>
    /// The exception that is thrown when a handle's value is invalid.
    /// </summary>
    public sealed class InvalidHandleException : SystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHandleException"/> class.
        /// </summary>
        public InvalidHandleException() : base("") { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="InvalidHandleException"/> class
        ///  with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidHandleException(string message) : base(message) { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="InvalidHandleException"/> class
        ///  with a specified error message and a reference to the inner exception that is
        ///  the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidHandleException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHandleException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public InvalidHandleException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}