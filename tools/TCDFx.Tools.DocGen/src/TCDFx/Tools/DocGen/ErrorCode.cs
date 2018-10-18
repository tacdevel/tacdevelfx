namespace TCDFx.Tools.DocGen
{
    internal enum ErrorCode
    {
        Unknown = -1,
        Success = 0,
        MissingArguments = 0x00000001,
        MissingArgument = 0x0002,
        NullArgument = 0x0003,
        InvalidArgument = 0x0004,
        DuplicateArgument = 0x0005
    }
}