/***************************************************************************************************
 * FileName:             DuplicateComponentException.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace TCD.InteropServices
{
    /// <summary>
    /// The exception that is thrown when a duplicate handle has been created.
    /// </summary>
    public sealed class DuplicateComponentException : SystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateComponentException"/> class.
        /// </summary>
        public DuplicateComponentException() : base("") { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="DuplicateComponentException"/> class
        ///  with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DuplicateComponentException(string message) : base(message) { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="DuplicateComponentException"/> class
        ///  with a specified error message and a reference to the inner exception that is
        ///  the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public DuplicateComponentException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateComponentException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public DuplicateComponentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}