/****************************************************************************
 * FileName:   ApplicationInitializationException.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180921
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Runtime.Serialization;

namespace TCD.UI
{
    /// <summary>
    /// The exception that is thrown when a handle is null or invalid.
    /// </summary>
    [Serializable]
    public sealed class ApplicationInitializationException : SystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInitializationException"/> class.
        /// </summary>
        public ApplicationInitializationException() : this("") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInitializationException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that specifies the reason for the exception.</param>
        public ApplicationInitializationException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInitializationException"/> class with the specified error message
        /// and <see langword="abstract"/> reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that specifies the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public ApplicationInitializationException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInitializationException"/> class with serialized data.
        /// </summary>
        /// <param name="serializationInfo">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="streamingContext">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        internal ApplicationInitializationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}