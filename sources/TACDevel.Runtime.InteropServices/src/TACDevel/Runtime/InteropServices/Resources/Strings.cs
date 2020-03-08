using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;

namespace TACDevel.Runtime.InteropServices.Resources
{
    [DebuggerNonUserCode()]
    internal class Strings
    {
        internal Strings() => ResourceManager = new ResourceManager("TACDevel.Runtime.InteropServices.Resources.String", typeof(Strings).Assembly);

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture { get; set; }

        internal static string ArrayPlusOffsetTooSmall => ResourceManager.GetString(nameof(ArrayPlusOffsetTooSmall), Culture);
        internal static string CannotReleaseInvalidHandle => ResourceManager.GetString(nameof(CannotReleaseInvalidHandle), Culture);
        internal static string IndexMustBeNonNegative => ResourceManager.GetString(nameof(IndexMustBeNonNegative), Culture);
        internal static string InvalidArrayType => ResourceManager.GetString(nameof(InvalidArrayType), Culture);
        internal static string MultiDimArrayNotSupported => ResourceManager.GetString(nameof(MultiDimArrayNotSupported), Culture);
        internal static string NativeAssemblyNotFound => ResourceManager.GetString(nameof(NativeAssemblyNotFound), Culture);
        internal static string NativeFunctionNotFound => ResourceManager.GetString(nameof(NativeFunctionNotFound), Culture);
        internal static string ObjectIsImmutable => ResourceManager.GetString(nameof(ObjectIsImmutable), Culture);
        internal static string ObjectMustNotBeNull => ResourceManager.GetString(nameof(ObjectMustNotBeNull), Culture);
        internal static string ObjectMustNotBeNullEmptyOrWhitespace => ResourceManager.GetString(nameof(ObjectMustNotBeNullEmptyOrWhitespace), Culture);
        internal static string ObjectMustNotBeNullOrEmpty => ResourceManager.GetString(nameof(ObjectMustNotBeNullOrEmpty), Culture);
    }
}