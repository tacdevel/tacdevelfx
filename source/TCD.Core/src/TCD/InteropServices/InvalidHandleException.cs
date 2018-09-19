/****************************************************************************
 * FileName:   InvalidHandleException.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180918
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Runtime.Serialization;

namespace TCD.InteropServices
{
    public sealed class InvalidHandleException : SystemException
    {
        public InvalidHandleException() : base("") { }
        public InvalidHandleException(string message) : base(message) { }
        public InvalidHandleException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidHandleException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}