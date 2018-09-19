/****************************************************************************
 * FileName:   DuplicateHandleException.cs
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
    public sealed class DuplicateHandleException : SystemException
    {
        public DuplicateHandleException() : base("") { }
        public DuplicateHandleException(string message) : base(message) { }
        public DuplicateHandleException(string message, Exception innerException) : base(message, innerException) { }
        public DuplicateHandleException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}