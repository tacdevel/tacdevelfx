/***********************************************************************************************************************
 * FileName:             MiniAudio.cs
 * Copyright:            Copyright © 2017-2020 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
 **********************************************************************************************************************/

// OS Configuration
#define WINDOWS
#define UNIX
#define LINUX
#define MACOS
#define FREEBSD

// Arch Configuration
#define X86
#define X64
#define ARM32
#define ARM64

#if WINDOWS
#define MA_SUPPORT_WASAPI
#define MA_SUPPORT_DSOUND
#define MA_SUPPORT_WINMM
#define MA_SUPPORT_JACK
#define MA_SUPPORT_NULL
#elif UNIX
#define MA_SUPPORT_NULL
#if FREEBSD
#define MA_SUPPORT_OSS
#elif MACOS
#define MA_SUPPORT_COREAUDIO
#elif LINUX
#define MA_SUPPORT_ALSA
#define MA_SUPPORT_PULSEAUDIO
#define MA_SUPPORT_JACK
#endif
#endif

using System;
using System.Runtime.InteropServices;
using System.Security;
using TACDevel.Runtime;
using TACDevel.Runtime.InteropServices;

using ma_int8 = System.SByte;
using ma_uint8 = System.Byte;
using ma_int16 = System.Int16;
using ma_uint16 = System.UInt16;
using ma_int32 = System.Int32;
using ma_uint32 = System.UInt32;
using ma_int64 = System.Int64;
using ma_uint64 = System.UInt64;
using ma_uintptr = System.UIntPtr;
using ma_bool8 = System.Byte;
using ma_bool32 = System.UInt32;
//using ma_handle = void*;
//using ma_ptr = void*;
//using ma_context = void*;
//using ma_device = void*;

namespace TACDevel.Native
{
    // Miniaudio.cs
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA1712 // Do not prefix enum values with type name
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1028 // Enum Storage should be Int32
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CA1815 // Override equals and operator equals on value types
    [CLSCompliant(false)]
    public static partial class Miniaudio
    {
        #region    General
        [UnmanagedFunctionPointer(convention)]
        public delegate void ma_proc();

        public const uint MA_SIZE_MAX = uint.MaxValue;

        //TODO: Is this needed?
        public const int MA_SIMD_ALIGNMENT = 64;

        //TODO: Is this needed?
        public const int MA_LOG_LEVEL_VERBOSE = 4;
        public const int MA_LOG_LEVEL_INFO = 3;
        public const int MA_LOG_LEVEL_WARNING = 2;
        public const int MA_LOG_LEVEL_ERROR = 1;
        public const int MA_LOG_LEVEL = MA_LOG_LEVEL_ERROR;

        public enum ma_channel : byte
        {
            MA_CHANNEL_NONE = 0,
            MA_CHANNEL_MONO = 1,
            MA_CHANNEL_FRONT_LEFT = 2,
            MA_CHANNEL_FRONT_RIGHT = 3,
            MA_CHANNEL_FRONT_CENTER = 4,
            MA_CHANNEL_LFE = 5,
            MA_CHANNEL_BACK_LEFT = 6,
            MA_CHANNEL_BACK_RIGHT = 7,
            MA_CHANNEL_FRONT_LEFT_CENTER = 8,
            MA_CHANNEL_FRONT_RIGHT_CENTER = 9,
            MA_CHANNEL_BACK_CENTER = 10,
            MA_CHANNEL_SIDE_LEFT = 11,
            MA_CHANNEL_SIDE_RIGHT = 12,
            MA_CHANNEL_TOP_CENTER = 13,
            MA_CHANNEL_TOP_FRONT_LEFT = 14,
            MA_CHANNEL_TOP_FRONT_CENTER = 15,
            MA_CHANNEL_TOP_FRONT_RIGHT = 16,
            MA_CHANNEL_TOP_BACK_LEFT = 17,
            MA_CHANNEL_TOP_BACK_CENTER = 18,
            MA_CHANNEL_TOP_BACK_RIGHT = 19,
            MA_CHANNEL_AUX_0 = 20,
            MA_CHANNEL_AUX_1 = 21,
            MA_CHANNEL_AUX_2 = 22,
            MA_CHANNEL_AUX_3 = 23,
            MA_CHANNEL_AUX_4 = 24,
            MA_CHANNEL_AUX_5 = 25,
            MA_CHANNEL_AUX_6 = 26,
            MA_CHANNEL_AUX_7 = 27,
            MA_CHANNEL_AUX_8 = 28,
            MA_CHANNEL_AUX_9 = 29,
            MA_CHANNEL_AUX_10 = 30,
            MA_CHANNEL_AUX_11 = 31,
            MA_CHANNEL_AUX_12 = 32,
            MA_CHANNEL_AUX_13 = 33,
            MA_CHANNEL_AUX_14 = 34,
            MA_CHANNEL_AUX_15 = 35,
            MA_CHANNEL_AUX_16 = 36,
            MA_CHANNEL_AUX_17 = 37,
            MA_CHANNEL_AUX_18 = 38,
            MA_CHANNEL_AUX_19 = 39,
            MA_CHANNEL_AUX_20 = 40,
            MA_CHANNEL_AUX_21 = 41,
            MA_CHANNEL_AUX_22 = 42,
            MA_CHANNEL_AUX_23 = 43,
            MA_CHANNEL_AUX_24 = 44,
            MA_CHANNEL_AUX_25 = 45,
            MA_CHANNEL_AUX_26 = 46,
            MA_CHANNEL_AUX_27 = 47,
            MA_CHANNEL_AUX_28 = 48,
            MA_CHANNEL_AUX_29 = 49,
            MA_CHANNEL_AUX_30 = 50,
            MA_CHANNEL_AUX_31 = 51,
            MA_CHANNEL_LEFT = MA_CHANNEL_FRONT_LEFT,
            MA_CHANNEL_RIGHT = MA_CHANNEL_FRONT_RIGHT,
            MA_CHANNEL_POSITION_COUNT = MA_CHANNEL_AUX_31 + 1
        }

        public enum ma_result : int
        {
            MA_SUCCESS = 0,
            MA_ERROR = -1,
            MA_INVALID_ARGS = -2,
            MA_INVALID_OPERATION = -3,
            MA_OUT_OF_MEMORY = -4,
            MA_ACCESS_DENIED = -5,
            MA_TOO_LARGE = -6,
            MA_TIMEOUT = -7,
            MA_FORMAT_NOT_SUPPORTED = -100,
            MA_DEVICE_TYPE_NOT_SUPPORTED = -101,
            MA_SHARE_MODE_NOT_SUPPORTED = -102,
            MA_NO_BACKEND = -103,
            MA_NO_DEVICE = -104,
            MA_API_NOT_FOUND = -105,
            MA_INVALID_DEVICE_CONFIG = -106,
            MA_DEVICE_BUSY = -200,
            MA_DEVICE_NOT_INITIALIZED = -201,
            MA_DEVICE_NOT_STARTED = -202,
            MA_DEVICE_UNAVAILABLE = -203,
            MA_FAILED_TO_MAP_DEVICE_BUFFER = -300,
            MA_FAILED_TO_UNMAP_DEVICE_BUFFER = -301,
            MA_FAILED_TO_INIT_BACKEND = -302,
            MA_FAILED_TO_READ_DATA_FROM_CLIENT = -303,
            MA_FAILED_TO_READ_DATA_FROM_DEVICE = -304,
            MA_FAILED_TO_SEND_DATA_TO_CLIENT = -305,
            MA_FAILED_TO_SEND_DATA_TO_DEVICE = -306,
            MA_FAILED_TO_OPEN_BACKEND_DEVICE = -307,
            MA_FAILED_TO_START_BACKEND_DEVICE = -308,
            MA_FAILED_TO_STOP_BACKEND_DEVICE = -309,
            MA_FAILED_TO_CONFIGURE_BACKEND_DEVICE = -310,
            MA_FAILED_TO_CREATE_MUTEX = -311,
            MA_FAILED_TO_CREATE_EVENT = -312,
            MA_FAILED_TO_CREATE_SEMAPHORE = -313,
            MA_FAILED_TO_CREATE_THREAD = -314,
        }

        public const int MA_SAMPLE_RATE_8000 = 8000;
        public const int MA_SAMPLE_RATE_11025 = 11025;
        public const int MA_SAMPLE_RATE_16000 = 16000;
        public const int MA_SAMPLE_RATE_22050 = 22050;
        public const int MA_SAMPLE_RATE_24000 = 24000;
        public const int MA_SAMPLE_RATE_32000 = 32000;
        public const int MA_SAMPLE_RATE_44100 = 44100;
        public const int MA_SAMPLE_RATE_48000 = 48000;
        public const int MA_SAMPLE_RATE_88200 = 88200;
        public const int MA_SAMPLE_RATE_96000 = 96000;
        public const int MA_SAMPLE_RATE_176400 = 176400;
        public const int MA_SAMPLE_RATE_192000 = 192000;
        public const int MA_SAMPLE_RATE_352800 = 352800;
        public const int MA_SAMPLE_RATE_384000 = 384000;

        public const int MA_MIN_CHANNELS = 1;
        public const int MA_MAX_CHANNELS = 32;
        public const int MA_MIN_SAMPLE_RATE = MA_SAMPLE_RATE_8000;
        public const int MA_MAX_SAMPLE_RATE = MA_SAMPLE_RATE_384000;

        //TODO: Is this needed?
        public const int MA_MAX_FILTER_ORDER = 8;

        public enum ma_stream_format
        {
            ma_stream_format_pcm = 0
        }

        public enum ma_stream_layout
        {
            ma_stream_layout_interleaved = 0,
            ma_stream_layout_deinterleaved
        }

        public enum ma_dither_mode
        {
            ma_dither_mode_none = 0,
            ma_dither_mode_rectangle,
            ma_dither_mode_triangle
        }

        public enum ma_format
        {
            ma_format_unknown = 0,
            ma_format_u8 = 1,
            ma_format_s16 = 2,
            ma_format_s24 = 3,
            ma_format_s32 = 4,
            ma_format_f32 = 5,
            ma_format_count
        }

        public enum ma_channel_mix_mode
        {
            ma_channel_mix_mode_rectangular = 0,
            ma_channel_mix_mode_simple,
            ma_channel_mix_mode_custom_weights,
            ma_channel_mix_mode_planar_blend = ma_channel_mix_mode_rectangular,
            ma_channel_mix_mode_default = ma_channel_mix_mode_planar_blend
        }

        public enum ma_standard_channel_map
        {
            ma_standard_channel_map_microsoft,
            ma_standard_channel_map_alsa,
            ma_standard_channel_map_rfc3551,
            ma_standard_channel_map_flac,
            ma_standard_channel_map_vorbis,
            ma_standard_channel_map_sound4,
            ma_standard_channel_map_sndio,
            ma_standard_channel_map_webaudio = ma_standard_channel_map_flac,
            ma_standard_channel_map_default = ma_standard_channel_map_microsoft
        }

        public enum ma_performance_profile
        {
            ma_performance_profile_low_latency = 0,
            ma_performance_profile_conservative
        }

        public struct ma_allocation_callbacks
        {
            public unsafe void* pUserData;
            public ma_allocation_callbacks_onMalloc onMalloc;
            public ma_allocation_callbacks_onRealloc onRealloc;
            public ma_allocation_callbacks_onFree onFree;
        }
        [UnmanagedFunctionPointer(convention)]
        public unsafe delegate void* ma_allocation_callbacks_onMalloc(UIntPtr sz, void* pUserData);
        [UnmanagedFunctionPointer(convention)]
        public unsafe delegate void* ma_allocation_callbacks_onRealloc(void* p, UIntPtr sz, void* pUserData);
        [UnmanagedFunctionPointer(convention)]
        public unsafe delegate void ma_allocation_callbacks_onFree(void* p, void* pUserData);
        #endregion General
        #region    Biquad Filtering
        public struct ma_biquad_coefficient
        {
            public float f32;
            public int s32;
        }

        public struct ma_biquad_config
        {
            public ma_format format;
            public uint channels;
            public double b0;
            public double b1;
            public double b2;
            public double a0;
            public double a1;
            public double a2;
        }

        public static ma_biquad_config ma_biquad_config_init(ma_format format, uint channels, double b0, double b1, double b2, double a0, double a1, double a2)
            => Call<NM.ma_biquad_config_init>()(format, channels, b0, b1, b2, a0, a1, a2);

        public struct ma_biquad
        {
            public ma_format format;
            public uint channels;
            public ma_biquad_coefficient b0;
            public ma_biquad_coefficient b1;
            public ma_biquad_coefficient b2;
            public ma_biquad_coefficient a1;
            public ma_biquad_coefficient a2;
            [MarshalAs(UnmanagedType.LPArray, SizeConst = MA_MAX_CHANNELS)]
            public ma_biquad_coefficient[] r1;
            [MarshalAs(UnmanagedType.LPArray, SizeConst = MA_MAX_CHANNELS)]
            public ma_biquad_coefficient[] r2;
        }

        public static unsafe ma_result ma_biquad_init(ma_biquad_config* pConfig, ma_biquad* pBQ)
            => Call<_.ma_biquad_init>()(pConfig, pBQ);
        public static unsafe ma_result ma_biquad_reinit(ma_biquad_config* pConfig, ma_biquad* pBQ)
            => Call<_.ma_biquad_reinit>()(pConfig, pBQ);
        public static unsafe ma_result ma_biquad_process_pcm_frames(ma_biquad* pBQ, void* pFramesOut, void* pFramesIn, ulong frameCount)
            => Call<_.ma_biquad_process_pcm_frames>()(pBQ, pFramesOut, pFramesIn, frameCount);
        public static unsafe uint ma_biquad_get_latency(ma_biquad* pBQ)
            => Call<_.ma_biquad_get_latency>()(pBQ);
        #endregion Biquad Filtering
        #region    Low-Pass Filtering
        internal struct ma_lpf1_config
        {
            internal ma_format format;
            internal uint channels;
            internal uint sampleRate;
            internal double cutoffFrequency;
        }

        internal struct ma_lpf2_config
        {
            internal ma_format format;
            internal uint channels;
            internal uint sampleRate;
            internal double cutoffFrequency;
        }

        internal static ma_lpf1_config ma_lpf1_config_init(ma_format format, uint channels, uint sampleRate, double cutoffFrequency)
            => Call<_.ma_lpf1_config_init>()(format, channels, sampleRate, cutoffFrequency);
        internal static ma_lpf2_config ma_lpf2_config_init(ma_format format, uint channels, uint sampleRate, double cutoffFrequency)
            => Call<_.ma_lpf2_config_init>()(format, channels, sampleRate, cutoffFrequency);

        internal unsafe struct ma_lpf1
        {
            internal ma_format format;
            internal uint channels;
            internal ma_biquad_coefficient a;
            internal unsafe fixed ma_biquad_coefficient r1[MA_MAX_CHANNELS];
        }

        internal static unsafe ma_result ma_lpf1_init(ma_lpf1_config* pConfig, ma_lpf1* pLPF)
            => Call<_.ma_lpf1_init>()(pConfig, pLPF);
        internal static unsafe ma_result ma_lpf1_reinit(ma_lpf1_config* pConfig, ma_lpf1* pLPF)
            => Call<_.ma_lpf1_reinit>()(pConfig, pLPF);
        internal static unsafe ma_result ma_lpf1_process_pcm_frames(ma_lpf1* pLPF, void* pFramesOut, void* pFramesIn, ulong frameCount)
            => Call<_.ma_lpf1_process_pcm_frames>()(pLPF, pFramesOut, pFramesIn, frameCount);
        internal static unsafe uint ma_lpf1_get_latency(ma_lpf1* pLPF)
            => Call<_.ma_lpf1_get_latency>()(pLPF);

        internal struct ma_lpf2
        {
            ma_biquad bq;
        }

        internal static unsafe ma_result ma_lpf2_init(ma_lpf2_config* pConfig, ma_lpf2* pLPF)
            => Call<_.ma_lpf2_init>()(pConfig, pLPF);
        internal static unsafe ma_result ma_lpf2_reinit(ma_lpf2_config* pConfig, ma_lpf2* pLPF)
            => Call<_.ma_lpf2_reinit>()(pConfig, pLPF);
        internal static unsafe ma_result ma_lpf2_process_pcm_frames(ma_lpf2* pLPF, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount)
            => Call<_.ma_lpf2_process_pcm_frames>()(pLPF, pFramesOut, pFramesIn, frameCount);
        internal static unsafe ma_uint32 ma_lpf2_get_latency(ma_lpf2* pLPF)
            => Call<_.ma_lpf2_get_latency>()(pLPF);

        internal struct ma_lpf_config
        {
            internal ma_format format;
            internal ma_uint32 channels;
            internal ma_uint32 sampleRate;
            internal double cutoffFrequency;
            internal ma_uint32 poles;
        }

        internal static ma_lpf_config ma_lpf_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, double cutoffFrequency, ma_uint32 poles)
            => Call<_.ma_lpf_config_init>()(format, channels, sampleRate, cutoffFrequency, poles);

        internal unsafe struct ma_lpf
        {
            internal ma_format format;
            internal ma_uint32 channels;
            internal ma_uint32 lpf2Count;
            internal ma_uint32 lpf1Count;
            internal unsafe fixed ma_lpf2 lpf2[MA_MAX_FILTER_POLES / 2];
            internal unsafe fixed ma_lpf1 lpf1[1];
        }

        internal static unsafe ma_result ma_lpf_init(ma_lpf_config* pConfig, ma_lpf* pLPF)
            => Call<_.ma_lpf_init>()(pConfig, pLPF);
        internal static unsafe ma_result ma_lpf_reinit(ma_lpf_config* pConfig, ma_lpf* pLPF)
            => Call<_.ma_lpf_reinit>()(pConfig, pLPF);
        internal static unsafe ma_result ma_lpf_process_pcm_frames(ma_lpf* pLPF, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount)
            => Call<_.ma_lpf_process_pcm_frames>()(pLPF, pFramesOut, pFramesIn, frameCount);
        internal static unsafe ma_uint32 ma_lpf_get_latency(ma_lpf* pLPF)
            => Call<_.ma_lpf_get_latency>()(pLPF);
        #endregion Low-Pass Filtering
        #region    High-Pass Filtering
        internal struct ma_hpf1_config
        {
            ma_format format;
            ma_uint32 channels;
            ma_uint32 sampleRate;
            double cutoffFrequency;
        }

        internal struct ma_hpf2_config
        {
            ma_format format;
            ma_uint32 channels;
            ma_uint32 sampleRate;
            double cutoffFrequency;
        }

        internal struct ma_hpf1
        {
            ma_format format;
            ma_uint32 channels;
            ma_biquad_coefficient a;
            unsafe fixed ma_biquad_coefficient r1[MA_MAX_CHANNELS];
        }

        internal struct ma_hpf2
        {
            ma_biquad bq;
        }

        internal struct ma_hpf_config
        {
            ma_format format;
            ma_uint32 channels;
            ma_uint32 sampleRate;
            double cutoffFrequency;
            ma_uint32 poles;
        }

        internal struct ma_hpf
        {
            ma_format format;
            ma_uint32 channels;
            ma_uint32 hpf2Count;
            ma_uint32 hpf1Count;
            unsafe fixed ma_hpf2 hpf2[MA_MAX_FILTER_POLES / 2];
            unsafe fixed ma_hpf1 hpf1[1];
        }
        #endregion High-Pass Filtering
        #region    Band-Pass Filtering
        internal struct ma_bpf2_config
        {
            ma_format format;
            ma_uint32 channels;
            ma_uint32 sampleRate;
            double cutoffFrequency;
        }

        internal struct ma_bpf2
        {
            ma_biquad bq;
        }

        internal struct ma_bpf_config
        {
            ma_format format;
            ma_uint32 channels;
            ma_uint32 sampleRate;
            double cutoffFrequency;
            ma_uint32 poles;
        }

        internal struct ma_bpf
        {
            ma_format format;
            ma_uint32 channels;
            ma_uint32 bpf2Count;
            unsafe fixed ma_bpf2 bpf2[MA_MAX_FILTER_POLES / 2];
        }
        #endregion Band-Pass Filtering
        #region    Notch Filtering
        internal struct ma_notch2_config
        {
            ma_format format;
            uint channels;
            uint sampleRate;
            double q;
            double frequency;
        }

        internal struct ma_notch2
        {
            ma_biquad bq;
        }
        #endregion Notch Filtering
        #region    Peak EQ Filtering
        internal struct ma_peak2_config
        {
            ma_format format;
            uint channels;
            uint sampleRate;
            double gainDB;
            double q;
            double frequency;
        }

        internal struct ma_peak2
        {
            ma_biquad bq;
        };
        #endregion Peak EQ Filtering
        #region    Low Shelf Filtering
        internal struct ma_loshelf2_config
        {
            ma_format format;
            uint channels;
            uint sampleRate;
            double gainDB;
            double shelfSlope;
            double frequency;
        }

        internal struct ma_loshelf2
        {
            ma_biquad bq;
        }
        #endregion Low Shelf Filtering
        #region    High Shelf Filtering
        internal struct ma_hishelf2_config
        {
            ma_format format;
            ma_uint32 channels;
            ma_uint32 sampleRate;
            double gainDB;
            double shelfSlope;
            double frequency;
        }

        internal struct ma_hishelf2
        {
            ma_biquad bq;
        }
        #endregion High Shelf Filtering
    }
#pragma warning restore CA1815 // Override equals and operator equals on value types
#pragma warning restore CA1034 // Nested types should not be visible
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore CA1028 // Enum Storage should be Int32
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1712 // Do not prefix enum values with type name
#pragma warning restore IDE1006 // Naming Styles

    // Miniaudio.Private.cs
    public static partial class Miniaudio
    {
        private const CallingConvention convention = CallingConvention.Cdecl;
        private const LayoutKind layout = LayoutKind.Sequential;
        private static readonly NativeAssembly assembly =
            (Platform.IsWindows && Platform.Is32Bit) ? throw new NotImplementedException() :
            (Platform.IsWindows && Platform.Is64Bit) ? throw new NotImplementedException() :
            (Platform.IsWindows && Platform.IsARM32) ? throw new PlatformNotSupportedException() :
            (Platform.IsMacOS && Platform.Is64Bit) ? throw new NotImplementedException() :
            (Platform.IsLinux && Platform.Is64Bit) ? throw new NotImplementedException() :
            (Platform.IsLinux && Platform.IsARM32) ? throw new PlatformNotSupportedException() :
            (Platform.IsLinux && Platform.IsARM64) ? throw new PlatformNotSupportedException() :
            (Platform.IsFreeBSD && Platform.Is64Bit) ? throw new NotImplementedException() :

            (Platform.IsWindows && Platform.Is32Bit) ? new NativeAssembly(@"runtimes\win-x86\native\miniaudio.dll") :
            (Platform.IsMacOS && Platform.Is64Bit) ? new NativeAssembly(@"runtimes/osx-x64/native/miniaudio.dylib") :
            ((Platform.IsLinux || Platform.IsFreeBSD) && Platform.Is64Bit) ? new NativeAssembly(@"runtimes/linux-x64/native/miniaudio.so") :
            throw new PlatformNotSupportedException();
        private static T Call<T>() where T : Delegate => assembly.LoadFunction<T>();

        private static class NM
        {
            #region    General
            internal delegate ma_biquad_config ma_biquad_config_init(ma_format format, uint channels, double b0, double b1, double b2, double a0, double a1, double a2);
            #endregion General
            #region    Biquad Filtering
            internal unsafe delegate ma_result ma_biquad_init(ma_biquad_config* pConfig, ma_biquad* pBQ);
            internal unsafe delegate ma_result ma_biquad_reinit(ma_biquad_config* pConfig, ma_biquad* pBQ);
            internal unsafe delegate ma_result ma_biquad_process_pcm_frames(ma_biquad* pBQ, void* pFramesOut, void* pFramesIn, ulong frameCount);
            internal unsafe delegate uint ma_biquad_get_latency(ma_biquad* pBQ);
            #endregion Biquad Filtering
            #region    Low-Pass Filtering
            internal delegate ma_lpf1_config ma_lpf1_config_init(ma_format format, uint channels, uint sampleRate, double cutoffFrequency);
            internal delegate ma_lpf2_config ma_lpf2_config_init(ma_format format, uint channels, uint sampleRate, double cutoffFrequency);

            internal unsafe delegate ma_result ma_lpf1_init(ma_lpf1_config* pConfig, ma_lpf1* pLPF);
            internal unsafe delegate ma_result ma_lpf1_reinit(ma_lpf1_config* pConfig, ma_lpf1* pLPF);
            internal unsafe delegate ma_result ma_lpf1_process_pcm_frames(ma_lpf1* pLPF, void* pFramesOut, void* pFramesIn, ulong frameCount);
            internal unsafe delegate uint ma_lpf1_get_latency(ma_lpf1* pLPF);

            internal unsafe delegate ma_result ma_lpf2_init(ma_lpf2_config* pConfig, ma_lpf2* pLPF);
            internal unsafe delegate ma_result ma_lpf2_reinit(ma_lpf2_config* pConfig, ma_lpf2* pLPF);
            internal unsafe delegate ma_result ma_lpf2_process_pcm_frames(ma_lpf2* pLPF, void* pFramesOut, void* pFramesIn, ulong frameCount);
            internal unsafe delegate uint ma_lpf2_get_latency(ma_lpf2* pLPF);

            internal delegate ma_lpf_config ma_lpf_config_init(ma_format format, uint channels, uint sampleRate, double cutoffFrequency, uint poles);

            internal unsafe delegate ma_result ma_lpf_init(ma_lpf_config* pConfig, ma_lpf* pLPF);
            internal unsafe delegate ma_result ma_lpf_reinit(ma_lpf_config* pConfig, ma_lpf* pLPF);
            internal unsafe delegate ma_result ma_lpf_process_pcm_frames(ma_lpf* pLPF, void* pFramesOut, void* pFramesIn, ulong frameCount);
            internal unsafe delegate uint ma_lpf_get_latency(ma_lpf* pLPF);
            #endregion Low-Pass Filtering
            #region    High-Pass Filtering
            ma_hpf1_config ma_hpf1_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, double cutoffFrequency);
            ma_hpf2_config ma_hpf2_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, double cutoffFrequency);

            internal unsafe delegate ma_result ma_hpf1_init(ma_hpf1_config* pConfig, ma_hpf1* pHPF);
            internal unsafe delegate ma_result ma_hpf1_reinit(ma_hpf1_config* pConfig, ma_hpf1* pHPF);
            internal unsafe delegate ma_result ma_hpf1_process_pcm_frames(ma_hpf1* pHPF, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount);
            ma_uint32 ma_hpf1_get_latency(ma_hpf1* pHPF);

            internal unsafe delegate ma_result ma_hpf2_init(ma_hpf2_config* pConfig, ma_hpf2* pHPF);
            internal unsafe delegate ma_result ma_hpf2_reinit(ma_hpf2_config* pConfig, ma_hpf2* pHPF);
            internal unsafe delegate ma_result ma_hpf2_process_pcm_frames(ma_hpf2* pHPF, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount);
            internal unsafe delegate ma_uint32 ma_hpf2_get_latency(ma_hpf2* pHPF);

            ma_hpf_config ma_hpf_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, double cutoffFrequency, ma_uint32 poles);

            internal unsafe delegate ma_result ma_hpf_init(ma_hpf_config* pConfig, ma_hpf* pHPF);
            internal unsafe delegate ma_result ma_hpf_reinit(ma_hpf_config* pConfig, ma_hpf* pHPF);
            internal unsafe delegate ma_result ma_hpf_process_pcm_frames(ma_hpf* pHPF, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount);
            internal unsafe delegate ma_uint32 ma_hpf_get_latency(ma_hpf* pHPF);
            #endregion High-Pass Filtering
            #region    Band-Pass Filtering
            ma_bpf2_config ma_bpf2_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, double cutoffFrequency);

            internal unsafe delegate ma_result ma_bpf2_init(ma_bpf2_config* pConfig, ma_bpf2* pBPF);
            internal unsafe delegate ma_result ma_bpf2_reinit(ma_bpf2_config* pConfig, ma_bpf2* pBPF);
            internal unsafe delegate ma_result ma_bpf2_process_pcm_frames(ma_bpf2* pBPF, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount);
            internal unsafe delegate ma_uint32 ma_bpf2_get_latency(ma_bpf2* pBPF);

            ma_bpf_config ma_bpf_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, double cutoffFrequency, ma_uint32 poles);

            internal unsafe delegate ma_result ma_bpf_init(ma_bpf_config* pConfig, ma_bpf* pBPF);
            internal unsafe delegate ma_result ma_bpf_reinit(ma_bpf_config* pConfig, ma_bpf* pBPF);
            internal unsafe delegate ma_result ma_bpf_process_pcm_frames(ma_bpf* pBPF, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount);
            internal unsafe delegate ma_uint32 ma_bpf_get_latency(ma_bpf* pBPF);
            #endregion Band-Pass Filtering
            #region    Notch Filtering
            ma_notch2_config ma_notch2_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, double q, double frequency);

            ma_result ma_notch2_init(ma_notch2_config* pConfig, ma_notch2* pFilter);
            ma_result ma_notch2_reinit(ma_notch2_config* pConfig, ma_notch2* pFilter);
            ma_result ma_notch2_process_pcm_frames(ma_notch2* pFilter, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount);
            ma_uint32 ma_notch2_get_latency(ma_notch2* pFilter);
            #endregion Notch Filtering
            #region    Peak EQ Filtering
            ma_peak2_config ma_peak2_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, double gainDB, double q, double frequency);

            ma_result ma_peak2_init(ma_peak2_config* pConfig, ma_peak2* pFilter);
            ma_result ma_peak2_reinit(ma_peak2_config* pConfig, ma_peak2* pFilter);
            ma_result ma_peak2_process_pcm_frames(ma_peak2* pFilter, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount);
            ma_uint32 ma_peak2_get_latency(ma_peak2* pFilter);
            #endregion Peak EQ Filtering
            #region    Low Shelf Filtering
            ma_loshelf2_config ma_loshelf2_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, double gainDB, double shelfSlope, double frequency);

            ma_result ma_loshelf2_init(ma_loshelf2_config* pConfig, ma_loshelf2* pFilter);
            ma_result ma_loshelf2_reinit(ma_loshelf2_config* pConfig, ma_loshelf2* pFilter);
            ma_result ma_loshelf2_process_pcm_frames(ma_loshelf2* pFilter, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount);
            ma_uint32 ma_loshelf2_get_latency(ma_loshelf2* pFilter);
            #endregion Low Shelf Filtering
            #region    High Shelf Filtering
            ma_hishelf2_config ma_hishelf2_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, double gainDB, double shelfSlope, double frequency);

            ma_result ma_hishelf2_init(ma_hishelf2_config* pConfig, ma_hishelf2* pFilter);
            ma_result ma_hishelf2_reinit(ma_hishelf2_config* pConfig, ma_hishelf2* pFilter);
            ma_result ma_hishelf2_process_pcm_frames(ma_hishelf2* pFilter, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount);
            ma_uint32 ma_hishelf2_get_latency(ma_hishelf2* pFilter);
            #endregion High Shelf Filtering
        }
    }

    [SuppressUnmanagedCodeSecurity]
    internal static class MiniAudio
    {
        #region Helpers
        #endregion
        private static class _
        {






            ma_linear_resampler_config ma_linear_resampler_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRateIn, ma_uint32 sampleRateOut);

            ma_result ma_linear_resampler_init(ma_linear_resampler_config* pConfig, ma_linear_resampler* pResampler);
            void ma_linear_resampler_uninit(ma_linear_resampler* pResampler);
            ma_result ma_linear_resampler_process_pcm_frames(ma_linear_resampler* pResampler, void* pFramesIn, ma_uint64* pFrameCountIn, void* pFramesOut, ma_uint64* pFrameCountOut);
            ma_result ma_linear_resampler_set_rate(ma_linear_resampler* pResampler, ma_uint32 sampleRateIn, ma_uint32 sampleRateOut);
            ma_result ma_linear_resampler_set_rate_ratio(ma_linear_resampler* pResampler, float ratioInOut);
            ma_uint64 ma_linear_resampler_get_required_input_frame_count(ma_linear_resampler* pResampler, ma_uint64 outputFrameCount);
            ma_uint64 ma_linear_resampler_get_expected_output_frame_count(ma_linear_resampler* pResampler, ma_uint64 inputFrameCount);
            ma_uint64 ma_linear_resampler_get_input_latency(ma_linear_resampler* pResampler);
            ma_uint64 ma_linear_resampler_get_output_latency(ma_linear_resampler* pResampler);

            ma_resampler_config ma_resampler_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRateIn, ma_uint32 sampleRateOut, ma_resample_algorithm algorithm);

            // MISSING

            ma_result ma_waveform_init(ma_waveform_config* pConfig, ma_waveform* pWaveform);
            ma_uint64 ma_waveform_read_pcm_frames(ma_waveform* pWaveform, void* pFramesOut, ma_uint64 frameCount);
            ma_result ma_waveform_set_amplitude(ma_waveform* pWaveform, double amplitude);
            ma_result ma_waveform_set_frequency(ma_waveform* pWaveform, double frequency);
            ma_result ma_waveform_set_sample_rate(ma_waveform* pWaveform, ma_uint32 sampleRate);

            ma_noise_config ma_noise_config_init(ma_format format, ma_uint32 channels, ma_noise_type type, ma_int32 seed, double amplitude);

            ma_result ma_noise_init(ma_noise_config* pConfig, ma_noise* pNoise);
            ma_uint64 ma_noise_read_pcm_frames(ma_noise* pNoise, void* pFramesOut, ma_uint64 frameCount);
        }

        /*
        internal struct ma_linear_resampler_config
        {
            ma_format format;
            ma_uint32 channels;
            ma_uint32 sampleRateIn;
            ma_uint32 sampleRateOut;
            ma_uint32 lpfPoles;
            double lpfNyquistFactor;
        }

        internal struct ma_linear_resampler
        {
            ma_linear_resampler_config config;
            ma_uint32 inAdvanceInt;
            ma_uint32 inAdvanceFrac;
            ma_uint32 inTimeInt;
            ma_uint32 inTimeFrac;
            ma_linear_resampler_x0 x0;
            ma_linear_resampler_x1 x1;
            ma_lpf lpf;
        }
        internal struct ma_linear_resampler_x0
        {
            unsafe fixed float f32[MA_MAX_CHANNELS];
            unsafe fixed ma_int16 s16[MA_MAX_CHANNELS];
        }
        internal struct ma_linear_resampler_x1
        {
            unsafe fixed float f32[MA_MAX_CHANNELS];
            unsafe fixed ma_int16 s16[MA_MAX_CHANNELS];
        }

        internal enum ma_resample_algorithm
        {
            ma_resample_algorithm_linear = 0,
            ma_resample_algorithm_speex
        }

        internal struct ma_resampler_config
        {
            ma_format format;
            ma_uint32 channels;
            ma_uint32 sampleRateIn;
            ma_uint32 sampleRateOut;
            ma_resample_algorithm algorithm;
            ma_resampler_config_linear linear;
            ma_resampler_config_speex speex;
        }
        internal struct ma_resampler_config_linear
        {
            ma_uint32 lpfPoles;
            double lpfNyquistFactor;
        }
        internal struct ma_resampler_config_speex
        {
            int quality;
        }

        internal struct ma_resampler
        {
            ma_resampler_config config;
            ma_resampler_state state;
        }
        internal struct ma_resampler_state
        {
            ma_linear_resampler linear;
            ma_resampler_state_speex speex;
        }
        internal struct ma_resampler_state_speex
        {
            void* pSpeexResamplerState;
        }

        internal unsafe struct ma_channel_converter_config
        {
            ma_format format;
            ma_uint32 channelsIn;
            ma_uint32 channelsOut;
            unsafe fixed ma_channel channelMapIn[MA_MAX_CHANNELS];
            unsafe fixed ma_channel channelMapOut[MA_MAX_CHANNELS];
            ma_channel_mix_mode mixingMode;
            unsafe fixed float weights[MA_MAX_CHANNELS][MA_MAX_CHANNELS];
}

        typedef struct
        {
    ma_format format;
        ma_uint32 channelsIn;
        ma_uint32 channelsOut;
        ma_channel channelMapIn[MA_MAX_CHANNELS];
        ma_channel channelMapOut[MA_MAX_CHANNELS];
        ma_channel_mix_mode mixingMode;
        union
    {
        float f32[MA_MAX_CHANNELS][MA_MAX_CHANNELS];
        ma_int32 s16[MA_MAX_CHANNELS][MA_MAX_CHANNELS];
    }
    weights;
    ma_bool32 isPassthrough         : 1;
    ma_bool32 isSimpleShuffle       : 1;
    ma_bool32 isSimpleMonoExpansion : 1;
    ma_bool32 isStereoToMono        : 1;
    ma_uint8 shuffleTable[MA_MAX_CHANNELS];
}
ma_channel_converter;

typedef struct
{
    ma_format formatIn;
ma_format formatOut;
ma_uint32 channelsIn;
ma_uint32 channelsOut;
ma_uint32 sampleRateIn;
ma_uint32 sampleRateOut;
ma_channel channelMapIn[MA_MAX_CHANNELS];
ma_channel channelMapOut[MA_MAX_CHANNELS];
ma_dither_mode ditherMode;
ma_channel_mix_mode channelMixMode;
float channelWeights[MA_MAX_CHANNELS][MA_MAX_CHANNELS];
    struct
    {
        ma_resample_algorithm algorithm;
ma_bool32 allowDynamicSampleRate;
struct
        {
            ma_uint32 lpfPoles;
double lpfNyquistFactor;
        } linear;
        struct
        {
            int quality;
        } speex;
    } resampling;
} ma_data_converter_config;

internal struct ma_data_converter
{
    ma_data_converter_config config;
    ma_channel_converter channelConverter;
    ma_resampler resampler;
    ma_bool32 hasPreFormatConversion  : 1;
    ma_bool32 hasPostFormatConversion : 1;
    ma_bool32 hasChannelConverter     : 1;
    ma_bool32 hasResampler            : 1;
    ma_bool32 isPassthrough           : 1;
}

internal struct ma_rb
{
    void* pBuffer;
    ma_uint32 subbufferSizeInBytes;
    ma_uint32 subbufferCount;
    ma_uint32 subbufferStrideInBytes;
    volatile ma_uint32 encodedReadOffset;
    volatile ma_uint32 encodedWriteOffset;
    ma_bool32 ownsBuffer = 1;
    ma_bool32 clearOnWriteAcquire = 1;
    ma_allocation_callbacks allocationCallbacks;
}

internal struct ma_pcm_rb
{
    ma_rb rb;
    ma_format format;
    ma_uint32 channels;
}

internal struct ma_lcg
{
    internal ma_int32 state;
}

internal enum ma_noise_type
{
    ma_noise_type_white,
}

internal struct ma_noise_config
{
    ma_format format;
    internal ma_uint32 channels;
    internal ma_noise_type type;
    internal ma_int32 seed;
    internal double amplitude;
    internal ma_bool32 duplicateChannels;
}

internal struct ma_noise
{
    internal ma_noise_config config;
    internal ma_lcg lcg;
}

ma_result ma_resampler_init(ma_resampler_config* pConfig, ma_resampler* pResampler);
void ma_resampler_uninit(ma_resampler* pResampler);
ma_result ma_resampler_process_pcm_frames(ma_resampler* pResampler, void* pFramesIn, ma_uint64* pFrameCountIn, void* pFramesOut, ma_uint64* pFrameCountOut);
ma_result ma_resampler_set_rate(ma_resampler* pResampler, ma_uint32 sampleRateIn, ma_uint32 sampleRateOut);
ma_result ma_resampler_set_rate_ratio(ma_resampler* pResampler, float ratio);
ma_uint64 ma_resampler_get_required_input_frame_count(ma_resampler* pResampler, ma_uint64 outputFrameCount);
ma_uint64 ma_resampler_get_expected_output_frame_count(ma_resampler* pResampler, ma_uint64 inputFrameCount);
ma_uint64 ma_resampler_get_input_latency(ma_resampler* pResampler);
ma_uint64 ma_resampler_get_output_latency(ma_resampler* pResampler);

ma_channel_converter_config ma_channel_converter_config_init(ma_format format, ma_uint32 channelsIn, ma_channel channelMapIn[MA_MAX_CHANNELS], ma_uint32 channelsOut, ma_channel channelMapOut[MA_MAX_CHANNELS], ma_channel_mix_mode mixingMode);

ma_result ma_channel_converter_init(ma_channel_converter_config* pConfig, ma_channel_converter* pConverter);
void ma_channel_converter_uninit(ma_channel_converter* pConverter);
ma_result ma_channel_converter_process_pcm_frames(ma_channel_converter* pConverter, void* pFramesOut, void* pFramesIn, ma_uint64 frameCount);

ma_data_converter_config ma_data_converter_config_init_default(void);
ma_data_converter_config ma_data_converter_config_init(ma_format formatIn, ma_format formatOut, ma_uint32 channelsIn, ma_uint32 channelsOut, ma_uint32 sampleRateIn, ma_uint32 sampleRateOut);

ma_result ma_data_converter_init(ma_data_converter_config* pConfig, ma_data_converter* pConverter);
void ma_data_converter_uninit(ma_data_converter* pConverter);
ma_result ma_data_converter_process_pcm_frames(ma_data_converter* pConverter, void* pFramesIn, ma_uint64* pFrameCountIn, void* pFramesOut, ma_uint64* pFrameCountOut);
ma_result ma_data_converter_set_rate(ma_data_converter* pConverter, ma_uint32 sampleRateIn, ma_uint32 sampleRateOut);
ma_result ma_data_converter_set_rate_ratio(ma_data_converter* pConverter, float ratioInOut);
ma_uint64 ma_data_converter_get_required_input_frame_count(ma_data_converter* pConverter, ma_uint64 outputFrameCount);
ma_uint64 ma_data_converter_get_expected_output_frame_count(ma_data_converter* pConverter, ma_uint64 inputFrameCount);
ma_uint64 ma_data_converter_get_input_latency(ma_data_converter* pConverter);
ma_uint64 ma_data_converter_get_output_latency(ma_data_converter* pConverter);

void ma_pcm_u8_to_s16(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_u8_to_s24(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_u8_to_s32(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_u8_to_f32(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s16_to_u8(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s16_to_s24(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s16_to_s32(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s16_to_f32(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s24_to_u8(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s24_to_s16(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s24_to_s32(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s24_to_f32(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s32_to_u8(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s32_to_s16(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s32_to_s24(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_s32_to_f32(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_f32_to_u8(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_f32_to_s16(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_f32_to_s24(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_f32_to_s32(void* pOut, void* pIn, ma_uint64 count, ma_dither_mode ditherMode);
void ma_pcm_convert(void* pOut, ma_format formatOut, void* pIn, ma_format formatIn, ma_uint64 sampleCount, ma_dither_mode ditherMode);
void ma_convert_pcm_frames_format(void* pOut, ma_format formatOut, void* pIn, ma_format formatIn, ma_uint64 frameCount, ma_uint32 channels, ma_dither_mode ditherMode);
void ma_deinterleave_pcm_frames(ma_format format, ma_uint32 channels, ma_uint64 frameCount, void* pInterleavedPCMFrames, void** ppDeinterleavedPCMFrames);
void ma_interleave_pcm_frames(ma_format format, ma_uint32 channels, ma_uint64 frameCount, void** ppDeinterleavedPCMFrames, void* pInterleavedPCMFrames);

void ma_get_standard_channel_map(ma_standard_channel_map standardChannelMap, ma_uint32 channels, ma_channel channelMap[MA_MAX_CHANNELS]);
void ma_channel_map_copy(ma_channel* pOut, ma_channel* pIn, ma_uint32 channels);
ma_bool32 ma_channel_map_valid(ma_uint32 channels, ma_channel channelMap[MA_MAX_CHANNELS]);
ma_bool32 ma_channel_map_equal(ma_uint32 channels, ma_channel channelMapA[MA_MAX_CHANNELS], ma_channel channelMapB[MA_MAX_CHANNELS]);
ma_bool32 ma_channel_map_blank(ma_uint32 channels, ma_channel channelMap[MA_MAX_CHANNELS]);
ma_bool32 ma_channel_map_contains_channel_position(ma_uint32 channels, ma_channel channelMap[MA_MAX_CHANNELS], ma_channel channelPosition);

ma_uint64 ma_convert_frames(void* pOut, ma_uint64 frameCountOut, ma_format formatOut, ma_uint32 channelsOut, ma_uint32 sampleRateOut, void* pIn, ma_uint64 frameCountIn, ma_format formatIn, ma_uint32 channelsIn, ma_uint32 sampleRateIn);
ma_uint64 ma_convert_frames_ex(void* pOut, ma_uint64 frameCountOut, void* pIn, ma_uint64 frameCountIn, ma_data_converter_config* pConfig);

ma_result ma_rb_init_ex(size_t subbufferSizeInBytes, size_t subbufferCount, size_t subbufferStrideInBytes, void* pOptionalPreallocatedBuffer, ma_allocation_callbacks* pAllocationCallbacks, ma_rb* pRB);
ma_result ma_rb_init(size_t bufferSizeInBytes, void* pOptionalPreallocatedBuffer, ma_allocation_callbacks* pAllocationCallbacks, ma_rb* pRB);
void ma_rb_uninit(ma_rb* pRB);
void ma_rb_reset(ma_rb* pRB);
ma_result ma_rb_acquire_read(ma_rb* pRB, size_t* pSizeInBytes, void** ppBufferOut);
ma_result ma_rb_commit_read(ma_rb* pRB, size_t sizeInBytes, void* pBufferOut);
ma_result ma_rb_acquire_write(ma_rb* pRB, size_t* pSizeInBytes, void** ppBufferOut);
ma_result ma_rb_commit_write(ma_rb* pRB, size_t sizeInBytes, void* pBufferOut);
ma_result ma_rb_seek_read(ma_rb* pRB, size_t offsetInBytes);
ma_result ma_rb_seek_write(ma_rb* pRB, size_t offsetInBytes);
ma_int32 ma_rb_pointer_distance(ma_rb* pRB);
ma_uint32 ma_rb_available_read(ma_rb* pRB);
ma_uint32 ma_rb_available_write(ma_rb* pRB);
size_t ma_rb_get_subbuffer_size(ma_rb* pRB);
size_t ma_rb_get_subbuffer_stride(ma_rb* pRB);
size_t ma_rb_get_subbuffer_offset(ma_rb* pRB, size_t subbufferIndex);
void* ma_rb_get_subbuffer_ptr(ma_rb* pRB, size_t subbufferIndex, void* pBuffer);

ma_result ma_pcm_rb_init_ex(ma_format format, ma_uint32 channels, ma_uint32 subbufferSizeInFrames, ma_uint32 subbufferCount, ma_uint32 subbufferStrideInFrames, void* pOptionalPreallocatedBuffer, ma_allocation_callbacks* pAllocationCallbacks, ma_pcm_rb* pRB);
ma_result ma_pcm_rb_init(ma_format format, ma_uint32 channels, ma_uint32 bufferSizeInFrames, void* pOptionalPreallocatedBuffer, ma_allocation_callbacks* pAllocationCallbacks, ma_pcm_rb* pRB);
void ma_pcm_rb_uninit(ma_pcm_rb* pRB);
void ma_pcm_rb_reset(ma_pcm_rb* pRB);
ma_result ma_pcm_rb_acquire_read(ma_pcm_rb* pRB, ma_uint32* pSizeInFrames, void** ppBufferOut);
ma_result ma_pcm_rb_commit_read(ma_pcm_rb* pRB, ma_uint32 sizeInFrames, void* pBufferOut);
ma_result ma_pcm_rb_acquire_write(ma_pcm_rb* pRB, ma_uint32* pSizeInFrames, void** ppBufferOut);
ma_result ma_pcm_rb_commit_write(ma_pcm_rb* pRB, ma_uint32 sizeInFrames, void* pBufferOut);
ma_result ma_pcm_rb_seek_read(ma_pcm_rb* pRB, ma_uint32 offsetInFrames);
ma_result ma_pcm_rb_seek_write(ma_pcm_rb* pRB, ma_uint32 offsetInFrames);
ma_int32 ma_pcm_rb_pointer_distance(ma_pcm_rb* pRB);
ma_uint32 ma_pcm_rb_available_read(ma_pcm_rb* pRB);
ma_uint32 ma_pcm_rb_available_write(ma_pcm_rb* pRB);
ma_uint32 ma_pcm_rb_get_subbuffer_size(ma_pcm_rb* pRB);
ma_uint32 ma_pcm_rb_get_subbuffer_stride(ma_pcm_rb* pRB);
ma_uint32 ma_pcm_rb_get_subbuffer_offset(ma_pcm_rb* pRB, ma_uint32 subbufferIndex);
void* ma_pcm_rb_get_subbuffer_ptr(ma_pcm_rb* pRB, ma_uint32 subbufferIndex, void* pBuffer);

void* ma_malloc(size_t sz, ma_allocation_callbacks* pAllocationCallbacks);
void* ma_realloc(void* p, size_t sz, ma_allocation_callbacks* pAllocationCallbacks);
void ma_free(void* p, ma_allocation_callbacks* pAllocationCallbacks);
void* ma_aligned_malloc(size_t sz, size_t alignment, ma_allocation_callbacks* pAllocationCallbacks);
void ma_aligned_free(void* p, ma_allocation_callbacks* pAllocationCallbacks);
char* ma_get_format_name(ma_format format);
void ma_blend_f32(float* pOut, float* pInA, float* pInB, float factor, ma_uint32 channels);
ma_uint32 ma_get_bytes_per_sample(ma_format format);
static MA_INLINE ma_uint32 ma_get_bytes_per_frame(ma_format format, ma_uint32 channels) { return ma_get_bytes_per_sample(format) * channels; }
char* ma_log_level_to_string(ma_uint32 logLevel);

/*
#if MA_SUPPORT_WASAPI
typedef struct
{
    void* lpVtbl;
    ma_uint32 counter;
    ma_device* pDevice;
} ma_IMMNotificationClient;
#endif

typedef enum
        {
    ma_backend_wasapi,
    ma_backend_dsound,
    ma_backend_winmm,
    ma_backend_coreaudio,
    ma_backend_sndio,
    ma_backend_audio4,
    ma_backend_oss,
    ma_backend_pulseaudio,
    ma_backend_alsa,
    ma_backend_jack,
    ma_backend_aaudio,
    ma_backend_opensl,
    ma_backend_webaudio,
    ma_backend_null
}
ma_backend;

typedef enum
{
    ma_thread_priority_idle = -5,
    ma_thread_priority_lowest = -4,
    ma_thread_priority_low = -3,
    ma_thread_priority_normal = -2,
    ma_thread_priority_high = -1,
    ma_thread_priority_highest = 0,
    ma_thread_priority_realtime = 1,
    ma_thread_priority_default = 0
}
ma_thread_priority;

typedef struct
{
    ma_context* pContext;

union
    {
#if MA_WIN32
        struct
        {
            ma_handle hThread;
    }
    win32;
#endif
#if MA_POSIX
        struct
        {
            pthread_t thread;
}
posix;
#endif
        int _unused;
    };
} ma_thread;

typedef struct
{
    ma_context* pContext;

union
    {
#if MA_WIN32
        struct
        {
            ma_handle hMutex;
        } win32;
#endif
#if MA_POSIX
        struct
        {
            pthread_mutex_t mutex;
        } posix;
#endif
        int _unused;
    };
} ma_mutex;

typedef struct
{
    ma_context* pContext;

union
    {
#if MA_WIN32
        struct
        {
            ma_handle hEvent;
        } win32;
#endif
#if MA_POSIX
        struct
        {
            pthread_mutex_t mutex;
pthread_cond_t condition;
ma_uint32 value;
        } posix;
#endif
        int _unused;
    };
} ma_event;

typedef struct
{
    ma_context* pContext;

union
    {
#if MA_WIN32
        struct
        {
            ma_handle hSemaphore;
        } win32;
#endif
#if MA_POSIX
        struct
        {
            sem_t semaphore;
        } posix;
#endif
        int _unused;
    };
} ma_semaphore;


typedef void (* ma_device_callback_proc) (ma_device* pDevice, void* pOutput, void* pInput, ma_uint32 frameCount);

typedef void (* ma_stop_proc) (ma_device* pDevice);

typedef void (* ma_log_proc) (ma_context* pContext, ma_device* pDevice, ma_uint32 logLevel, char* message);

typedef enum
{
    ma_device_type_playback = 1,
    ma_device_type_capture = 2,
    ma_device_type_duplex = ma_device_type_playback | ma_device_type_capture,
    ma_device_type_loopback = 4
}
ma_device_type;

typedef enum
{
    ma_share_mode_shared = 0,
    ma_share_mode_exclusive
}
ma_share_mode;

typedef enum
{
    ma_ios_session_category_default = 0,
    ma_ios_session_category_none,
    ma_ios_session_category_ambient,
    ma_ios_session_category_solo_ambient,
    ma_ios_session_category_playback,
    ma_ios_session_category_record,
    ma_ios_session_category_play_and_record,
    ma_ios_session_category_multi_route
}
ma_ios_session_category;

typedef enum
{
    ma_ios_session_category_option_mix_with_others = 0x01,
    ma_ios_session_category_option_duck_others = 0x02,
    ma_ios_session_category_option_allow_bluetooth = 0x04,
    ma_ios_session_category_option_default_to_speaker = 0x08,
    ma_ios_session_category_option_interrupt_spoken_audio_and_mix_with_others = 0x11,
    ma_ios_session_category_option_allow_bluetooth_a2dp = 0x20,
    ma_ios_session_category_option_allow_air_play = 0x40,
}
ma_ios_session_category_option;

typedef union
{
    ma_int64 counter;
    double counterD;
}
ma_timer;

typedef union
{
    wchar_t wasapi [64];
    ma_uint8 dsound [16];
    ma_uint32 winmm;                       
    char alsa [256];                         
    char pulse [256];                        
    int jack;                             
    char coreaudio [256];                    
    char sndio [256];                   
    char audio4 [256];                  
    char oss [64];
    ma_int32 aaudio;
    ma_uint32 opensl;                        
    char webaudio [32];                               
    int nullbackend;
}
ma_device_id;

typedef struct
{
    ma_device_id id;
char name[256];

ma_uint32 formatCount;
ma_format formats[ma_format_count];
ma_uint32 minChannels;
ma_uint32 maxChannels;
ma_uint32 minSampleRate;
ma_uint32 maxSampleRate;

struct
    {
        ma_bool32 isDefault;
    } _private;
} ma_device_info;

typedef struct
{
    ma_device_type deviceType;
ma_uint32 sampleRate;
ma_uint32 periodSizeInFrames;
ma_uint32 periodSizeInMilliseconds;
ma_uint32 periods;
ma_performance_profile performanceProfile;
ma_bool32 noPreZeroedOutputBuffer;
ma_bool32 noClip;
ma_device_callback_proc dataCallback;
ma_stop_proc stopCallback;
void* pUserData;
struct
    {
        ma_resample_algorithm algorithm;
struct
        {
            ma_uint32 lpfPoles;
        } linear;
        struct
        {
            int quality;
        } speex;
    } resampling;
    struct
    {
        ma_device_id* pDeviceID;
ma_format format;
ma_uint32 channels;
ma_channel channelMap[MA_MAX_CHANNELS];
ma_share_mode shareMode;
    } playback;
    struct
    {
        ma_device_id* pDeviceID;
ma_format format;
ma_uint32 channels;
ma_channel channelMap[MA_MAX_CHANNELS];
ma_share_mode shareMode;
    } capture;

    struct
    {
        ma_bool32 noAutoConvertSRC;
ma_bool32 noDefaultQualitySRC;
ma_bool32 noAutoStreamRouting;
ma_bool32 noHardwareOffloading;       
    } wasapi;
    struct
    {
        ma_bool32 noMMap;      
    } alsa;
    struct
    {
        const char* pStreamNamePlayback;
const char* pStreamNameCapture;
    } pulse;
} ma_device_config;

typedef struct
{
    ma_log_proc logCallback;
ma_thread_priority threadPriority;
void* pUserData;
ma_allocation_callbacks allocationCallbacks;
struct
    {
        ma_bool32 useVerboseDeviceEnumeration;
    } alsa;
    struct
    {
        const char* pApplicationName;
const char* pServerName;
ma_bool32 tryAutoSpawn;          
    } pulse;
    struct
    {
        ma_ios_session_category sessionCategory;
ma_uint32 sessionCategoryOptions;
    } coreaudio;
    struct
    {
        const char* pClientName;
ma_bool32 tryStartServer;
    } jack;
} ma_context_config;

typedef ma_bool32(* ma_enum_devices_callback_proc)(ma_context* pContext, ma_device_type deviceType, ma_device_info* pInfo, void* pUserData);

struct ma_context
{
    ma_backend backend;
    ma_log_proc logCallback;
    ma_thread_priority threadPriority;
    void* pUserData;
    ma_allocation_callbacks allocationCallbacks;
    ma_mutex deviceEnumLock;
    ma_mutex deviceInfoLock;
    ma_uint32 deviceInfoCapacity;
    ma_uint32 playbackDeviceInfoCount;
    ma_uint32 captureDeviceInfoCount;
    ma_device_info* pDeviceInfos;
    ma_bool32 isBackendAsynchronous : 1;

    ma_result(* onUninit        )(ma_context* pContext);
    ma_bool32(* onDeviceIDEqual )(ma_context* pContext, ma_device_id* pID0, ma_device_id* pID1);
    ma_result(* onEnumDevices   )(ma_context* pContext, ma_enum_devices_callback_proc callback, void* pUserData);             
    ma_result(* onGetDeviceInfo )(ma_context* pContext, ma_device_type deviceType, ma_device_id* pDeviceID, ma_share_mode shareMode, ma_device_info* pDeviceInfo);
    ma_result(* onDeviceInit    )(ma_context* pContext, ma_device_config* pConfig, ma_device* pDevice);
    void (* onDeviceUninit  ) (ma_device* pDevice);
    ma_result(* onDeviceStart   )(ma_device* pDevice);
    ma_result(* onDeviceStop    )(ma_device* pDevice);
    ma_result(* onDeviceMainLoop)(ma_device* pDevice);

    union
    {
#if MA_SUPPORT_WASAPI
        struct
        {
            int _unused;
}
wasapi;
#endif
#if MA_SUPPORT_DSOUND
        struct
        {
            ma_handle hDSoundDLL;
ma_proc DirectSoundCreate;
ma_proc DirectSoundEnumerateA;
ma_proc DirectSoundCaptureCreate;
ma_proc DirectSoundCaptureEnumerateA;
        } dsound;
#endif
#if MA_SUPPORT_WINMM
        struct
        {
            ma_handle hWinMM;
ma_proc waveOutGetNumDevs;
ma_proc waveOutGetDevCapsA;
ma_proc waveOutOpen;
ma_proc waveOutClose;
ma_proc waveOutPrepareHeader;
ma_proc waveOutUnprepareHeader;
ma_proc waveOutWrite;
ma_proc waveOutReset;
ma_proc waveInGetNumDevs;
ma_proc waveInGetDevCapsA;
ma_proc waveInOpen;
ma_proc waveInClose;
ma_proc waveInPrepareHeader;
ma_proc waveInUnprepareHeader;
ma_proc waveInAddBuffer;
ma_proc waveInStart;
ma_proc waveInReset;
        } winmm;
#endif
#if MA_SUPPORT_ALSA
        struct
        {
            ma_handle asoundSO;
ma_proc snd_pcm_open;
ma_proc snd_pcm_close;
ma_proc snd_pcm_hw_params_sizeof;
ma_proc snd_pcm_hw_params_any;
ma_proc snd_pcm_hw_params_set_format;
ma_proc snd_pcm_hw_params_set_format_first;
ma_proc snd_pcm_hw_params_get_format_mask;
ma_proc snd_pcm_hw_params_set_channels_near;
ma_proc snd_pcm_hw_params_set_rate_resample;
ma_proc snd_pcm_hw_params_set_rate_near;
ma_proc snd_pcm_hw_params_set_buffer_size_near;
ma_proc snd_pcm_hw_params_set_periods_near;
ma_proc snd_pcm_hw_params_set_access;
ma_proc snd_pcm_hw_params_get_format;
ma_proc snd_pcm_hw_params_get_channels;
ma_proc snd_pcm_hw_params_get_channels_min;
ma_proc snd_pcm_hw_params_get_channels_max;
ma_proc snd_pcm_hw_params_get_rate;
ma_proc snd_pcm_hw_params_get_rate_min;
ma_proc snd_pcm_hw_params_get_rate_max;
ma_proc snd_pcm_hw_params_get_buffer_size;
ma_proc snd_pcm_hw_params_get_periods;
ma_proc snd_pcm_hw_params_get_access;
ma_proc snd_pcm_hw_params;
ma_proc snd_pcm_sw_params_sizeof;
ma_proc snd_pcm_sw_params_current;
ma_proc snd_pcm_sw_params_get_boundary;
ma_proc snd_pcm_sw_params_set_avail_min;
ma_proc snd_pcm_sw_params_set_start_threshold;
ma_proc snd_pcm_sw_params_set_stop_threshold;
ma_proc snd_pcm_sw_params;
ma_proc snd_pcm_format_mask_sizeof;
ma_proc snd_pcm_format_mask_test;
ma_proc snd_pcm_get_chmap;
ma_proc snd_pcm_state;
ma_proc snd_pcm_prepare;
ma_proc snd_pcm_start;
ma_proc snd_pcm_drop;
ma_proc snd_pcm_drain;
ma_proc snd_device_name_hint;
ma_proc snd_device_name_get_hint;
ma_proc snd_card_get_index;
ma_proc snd_device_name_free_hint;
ma_proc snd_pcm_mmap_begin;
ma_proc snd_pcm_mmap_commit;
ma_proc snd_pcm_recover;
ma_proc snd_pcm_readi;
ma_proc snd_pcm_writei;
ma_proc snd_pcm_avail;
ma_proc snd_pcm_avail_update;
ma_proc snd_pcm_wait;
ma_proc snd_pcm_info;
ma_proc snd_pcm_info_sizeof;
ma_proc snd_pcm_info_get_name;
ma_proc snd_config_update_free_global;

ma_mutex internalDeviceEnumLock;
ma_bool32 useVerboseDeviceEnumeration;
        } alsa;
#endif
#if MA_SUPPORT_PULSEAUDIO
        struct
        {
            ma_handle pulseSO;
ma_proc pa_mainloop_new;
ma_proc pa_mainloop_free;
ma_proc pa_mainloop_get_api;
ma_proc pa_mainloop_iterate;
ma_proc pa_mainloop_wakeup;
ma_proc pa_context_new;
ma_proc pa_context_unref;
ma_proc pa_context_connect;
ma_proc pa_context_disconnect;
ma_proc pa_context_set_state_callback;
ma_proc pa_context_get_state;
ma_proc pa_context_get_sink_info_list;
ma_proc pa_context_get_source_info_list;
ma_proc pa_context_get_sink_info_by_name;
ma_proc pa_context_get_source_info_by_name;
ma_proc pa_operation_unref;
ma_proc pa_operation_get_state;
ma_proc pa_channel_map_init_extend;
ma_proc pa_channel_map_valid;
ma_proc pa_channel_map_compatible;
ma_proc pa_stream_new;
ma_proc pa_stream_unref;
ma_proc pa_stream_connect_playback;
ma_proc pa_stream_connect_record;
ma_proc pa_stream_disconnect;
ma_proc pa_stream_get_state;
ma_proc pa_stream_get_sample_spec;
ma_proc pa_stream_get_channel_map;
ma_proc pa_stream_get_buffer_attr;
ma_proc pa_stream_set_buffer_attr;
ma_proc pa_stream_get_device_name;
ma_proc pa_stream_set_write_callback;
ma_proc pa_stream_set_read_callback;
ma_proc pa_stream_flush;
ma_proc pa_stream_drain;
ma_proc pa_stream_is_corked;
ma_proc pa_stream_cork;
ma_proc pa_stream_trigger;
ma_proc pa_stream_begin_write;
ma_proc pa_stream_write;
ma_proc pa_stream_peek;
ma_proc pa_stream_drop;
ma_proc pa_stream_writable_size;
ma_proc pa_stream_readable_size;

char* pApplicationName;
char* pServerName;
ma_bool32 tryAutoSpawn;
        } pulse;
#endif
#if MA_SUPPORT_JACK
        struct
        {
            ma_handle jackSO;
ma_proc jack_client_open;
ma_proc jack_client_close;
ma_proc jack_client_name_size;
ma_proc jack_set_process_callback;
ma_proc jack_set_buffer_size_callback;
ma_proc jack_on_shutdown;
ma_proc jack_get_sample_rate;
ma_proc jack_get_buffer_size;
ma_proc jack_get_ports;
ma_proc jack_activate;
ma_proc jack_deactivate;
ma_proc jack_connect;
ma_proc jack_port_register;
ma_proc jack_port_name;
ma_proc jack_port_get_buffer;
ma_proc jack_free;

char* pClientName;
ma_bool32 tryStartServer;
        } jack;
#endif
#if MA_SUPPORT_COREAUDIO
        struct
        {
            ma_handle hCoreFoundation;
ma_proc CFStringGetCString;
ma_proc CFRelease;

ma_handle hCoreAudio;
ma_proc AudioObjectGetPropertyData;
ma_proc AudioObjectGetPropertyDataSize;
ma_proc AudioObjectSetPropertyData;
ma_proc AudioObjectAddPropertyListener;
ma_proc AudioObjectRemovePropertyListener;

ma_handle hAudioUnit;
ma_proc AudioComponentFindNext;
ma_proc AudioComponentInstanceDispose;
ma_proc AudioComponentInstanceNew;
ma_proc AudioOutputUnitStart;
ma_proc AudioOutputUnitStop;
ma_proc AudioUnitAddPropertyListener;
ma_proc AudioUnitGetPropertyInfo;
ma_proc AudioUnitGetProperty;
ma_proc AudioUnitSetProperty;
ma_proc AudioUnitInitialize;
ma_proc AudioUnitRender;

ma_ptr component;
        } coreaudio;
#endif
#if MA_SUPPORT_SNDIO
        struct
        {
            ma_handle sndioSO;
ma_proc sio_open;
ma_proc sio_close;
ma_proc sio_setpar;
ma_proc sio_getpar;
ma_proc sio_getcap;
ma_proc sio_start;
ma_proc sio_stop;
ma_proc sio_read;
ma_proc sio_write;
ma_proc sio_onmove;
ma_proc sio_nfds;
ma_proc sio_pollfd;
ma_proc sio_revents;
ma_proc sio_eof;
ma_proc sio_setvol;
ma_proc sio_onvol;
ma_proc sio_initpar;
        } sndio;
#endif
#if MA_SUPPORT_AUDIO4
        struct
        {
            int _unused;
        } audio4;
#endif
#if MA_SUPPORT_OSS
        struct
        {
            int versionMajor;
int versionMinor;
        } oss;
#endif
#if MA_SUPPORT_AAUDIO
        struct
        {
            ma_handle hAAudio;
ma_proc AAudio_createStreamBuilder;
ma_proc AAudioStreamBuilder_delete;
ma_proc AAudioStreamBuilder_setDeviceId;
ma_proc AAudioStreamBuilder_setDirection;
ma_proc AAudioStreamBuilder_setSharingMode;
ma_proc AAudioStreamBuilder_setFormat;
ma_proc AAudioStreamBuilder_setChannelCount;
ma_proc AAudioStreamBuilder_setSampleRate;
ma_proc AAudioStreamBuilder_setBufferCapacityInFrames;
ma_proc AAudioStreamBuilder_setFramesPerDataCallback;
ma_proc AAudioStreamBuilder_setDataCallback;
ma_proc AAudioStreamBuilder_setErrorCallback;
ma_proc AAudioStreamBuilder_setPerformanceMode;
ma_proc AAudioStreamBuilder_openStream;
ma_proc AAudioStream_close;
ma_proc AAudioStream_getState;
ma_proc AAudioStream_waitForStateChange;
ma_proc AAudioStream_getFormat;
ma_proc AAudioStream_getChannelCount;
ma_proc AAudioStream_getSampleRate;
ma_proc AAudioStream_getBufferCapacityInFrames;
ma_proc AAudioStream_getFramesPerDataCallback;
ma_proc AAudioStream_getFramesPerBurst;
ma_proc AAudioStream_requestStart;
ma_proc AAudioStream_requestStop;
        } aaudio;
#endif
#if MA_SUPPORT_OPENSL
        struct
        {
            int _unused;
        } opensl;
#endif
#if MA_SUPPORT_WEBAUDIO
        struct
        {
            int _unused;
        } webaudio;
#endif
#if MA_SUPPORT_NULL
        struct
        {
            int _unused;
        } null_backend;
#endif
    };

union
    {
#if MA_WIN32
        struct
        {
            ma_handle hOle32DLL;
ma_proc CoInitializeEx;
ma_proc CoUninitialize;
ma_proc CoCreateInstance;
ma_proc CoTaskMemFree;
ma_proc PropVariantClear;
ma_proc StringFromGUID2;

ma_handle hUser32DLL;
ma_proc GetForegroundWindow;
ma_proc GetDesktopWindow;

ma_handle hAdvapi32DLL;
ma_proc RegOpenKeyExA;
ma_proc RegCloseKey;
ma_proc RegQueryValueExA;
        } win32;
#endif
#if MA_POSIX
        struct
        {
            ma_handle pthreadSO;
ma_proc pthread_create;
ma_proc pthread_join;
ma_proc pthread_mutex_init;
ma_proc pthread_mutex_destroy;
ma_proc pthread_mutex_lock;
ma_proc pthread_mutex_unlock;
ma_proc pthread_cond_init;
ma_proc pthread_cond_destroy;
ma_proc pthread_cond_wait;
ma_proc pthread_cond_signal;
ma_proc pthread_attr_init;
ma_proc pthread_attr_destroy;
ma_proc pthread_attr_setschedpolicy;
ma_proc pthread_attr_getschedparam;
ma_proc pthread_attr_setschedparam;
        } posix;
#endif
        int _unused;
    };
};

struct ma_device
{
    ma_context* pContext;
    ma_device_type type;
    ma_uint32 sampleRate;
    volatile ma_uint32 state;
    ma_device_callback_proc onData;
    ma_stop_proc onStop;
    void* pUserData;
    ma_mutex lock;
    ma_event wakeupEvent;
    ma_event startEvent;
    ma_event stopEvent;
    ma_thread thread;
    ma_result workResult;
    ma_bool32 usingDefaultSampleRate  : 1;
    ma_bool32 usingDefaultBufferSize  : 1;
    ma_bool32 usingDefaultPeriods     : 1;
    ma_bool32 isOwnerOfContext        : 1;
    ma_bool32 noPreZeroedOutputBuffer : 1;
    ma_bool32 noClip                  : 1;
    volatile float masterVolumeFactor;
    struct
    {
        ma_resample_algorithm algorithm;
    struct
        {
            ma_uint32 lpfPoles;
}
linear;
        struct
        {
            int quality;
        } speex;
    } resampling;
    struct
    {
        char name[256];
ma_share_mode shareMode;
ma_bool32 usingDefaultFormat     : 1;
        ma_bool32 usingDefaultChannels   : 1;
        ma_bool32 usingDefaultChannelMap : 1;
        ma_format format;
ma_uint32 channels;
ma_channel channelMap[MA_MAX_CHANNELS];
ma_format internalFormat;
ma_uint32 internalChannels;
ma_uint32 internalSampleRate;
ma_channel internalChannelMap[MA_MAX_CHANNELS];
ma_uint32 internalPeriodSizeInFrames;
ma_uint32 internalPeriods;
ma_data_converter converter;
    } playback;
    struct
    {
        char name[256];
ma_share_mode shareMode;
ma_bool32 usingDefaultFormat     : 1;
        ma_bool32 usingDefaultChannels   : 1;
        ma_bool32 usingDefaultChannelMap : 1;
        ma_format format;
ma_uint32 channels;
ma_channel channelMap[MA_MAX_CHANNELS];
ma_format internalFormat;
ma_uint32 internalChannels;
ma_uint32 internalSampleRate;
ma_channel internalChannelMap[MA_MAX_CHANNELS];
ma_uint32 internalPeriodSizeInFrames;
ma_uint32 internalPeriods;
ma_data_converter converter;
    } capture;

    union
    {
#if MA_SUPPORT_WASAPI
        struct
        {
             ma_ptr pAudioClientPlayback;
ma_ptr pAudioClientCapture;
ma_ptr pRenderClient;
ma_ptr pCaptureClient;
ma_ptr pDeviceEnumerator;
ma_IMMNotificationClient notificationClient;
ma_handle hEventPlayback;          
ma_handle hEventCapture;              
ma_uint32 actualPeriodSizeInFramesPlayback;        
ma_uint32 actualPeriodSizeInFramesCapture;
ma_uint32 originalPeriodSizeInFrames;
ma_uint32 originalPeriodSizeInMilliseconds;
ma_uint32 originalPeriods;
ma_bool32 hasDefaultPlaybackDeviceChanged;       
ma_bool32 hasDefaultCaptureDeviceChanged;        
ma_uint32 periodSizeInFramesPlayback;
ma_uint32 periodSizeInFramesCapture;
ma_bool32 isStartedCapture;                  
ma_bool32 isStartedPlayback;                       
ma_bool32 noAutoConvertSRC               : 1;    
            ma_bool32 noDefaultQualitySRC            : 1;   
            ma_bool32 noHardwareOffloading           : 1;
            ma_bool32 allowCaptureAutoStreamRouting  : 1;
            ma_bool32 allowPlaybackAutoStreamRouting : 1;
        } wasapi;
#endif
#if MA_SUPPORT_DSOUND
        struct
        {
            ma_ptr pPlayback;
ma_ptr pPlaybackPrimaryBuffer;
ma_ptr pPlaybackBuffer;
ma_ptr pCapture;
ma_ptr pCaptureBuffer;
        } dsound;
#endif
#if MA_SUPPORT_WINMM
        struct
        {
            ma_handle hDevicePlayback;
ma_handle hDeviceCapture;
ma_handle hEventPlayback;
ma_handle hEventCapture;
ma_uint32 fragmentSizeInFrames;
ma_uint32 fragmentSizeInBytes;
ma_uint32 iNextHeaderPlayback;             
ma_uint32 iNextHeaderCapture;              
ma_uint32 headerFramesConsumedPlayback;    
ma_uint32 headerFramesConsumedCapture;     

ma_uint8* pWAVEHDRPlayback;   

ma_uint8* pWAVEHDRCapture;    
ma_uint8* pIntermediaryBufferPlayback;
ma_uint8* pIntermediaryBufferCapture;
ma_uint8* _pHeapData;                      
        } winmm;
#endif
#if MA_SUPPORT_ALSA
        struct
        {
             ma_ptr pPCMPlayback;

ma_ptr pPCMCapture;
ma_bool32 isUsingMMapPlayback : 1;
            ma_bool32 isUsingMMapCapture  : 1;
        } alsa;
#endif
#if MA_SUPPORT_PULSEAUDIO
        struct
        {
             ma_ptr pMainLoop;

ma_ptr pAPI;

ma_ptr pPulseContext;

ma_ptr pStreamPlayback;

ma_ptr pStreamCapture;

ma_uint32 pulseContextState;
void* pMappedBufferPlayback;
const void* pMappedBufferCapture;
ma_uint32 mappedBufferFramesRemainingPlayback;
ma_uint32 mappedBufferFramesRemainingCapture;
ma_uint32 mappedBufferFramesCapacityPlayback;
ma_uint32 mappedBufferFramesCapacityCapture;
ma_bool32 breakFromMainLoop : 1;
        } pulse;
#endif
#if MA_SUPPORT_JACK
        struct
        {
             ma_ptr pClient;

ma_ptr pPortsPlayback[MA_MAX_CHANNELS];

ma_ptr pPortsCapture[MA_MAX_CHANNELS];
float* pIntermediaryBufferPlayback; 
float* pIntermediaryBufferCapture;
ma_pcm_rb duplexRB;
        } jack;
#endif
#if MA_SUPPORT_COREAUDIO
        struct
        {
            ma_uint32 deviceObjectIDPlayback;
ma_uint32 deviceObjectIDCapture;

ma_ptr audioUnitPlayback;

ma_ptr audioUnitCapture;

ma_ptr pAudioBufferList;  
ma_event stopEvent;
ma_uint32 originalPeriodSizeInFrames;
ma_uint32 originalPeriodSizeInMilliseconds;
ma_uint32 originalPeriods;
ma_bool32 isDefaultPlaybackDevice;
ma_bool32 isDefaultCaptureDevice;
ma_bool32 isSwitchingPlaybackDevice;   
ma_bool32 isSwitchingCaptureDevice;    
ma_pcm_rb duplexRB;
void* pRouteChangeHandler;             
        } coreaudio;
#endif
#if MA_SUPPORT_SNDIO
        struct
        {
            ma_ptr handlePlayback;
ma_ptr handleCapture;
ma_bool32 isStartedPlayback;
ma_bool32 isStartedCapture;
        } sndio;
#endif
#if MA_SUPPORT_AUDIO4
        struct
        {
            int fdPlayback;
int fdCapture;
        } audio4;
#endif
#if MA_SUPPORT_OSS
        struct
        {
            int fdPlayback;
int fdCapture;
        } oss;
#endif
#if MA_SUPPORT_AAUDIO
        struct
        {
             ma_ptr pStreamPlayback;

ma_ptr pStreamCapture;
ma_pcm_rb duplexRB;
        } aaudio;
#endif
#if MA_SUPPORT_OPENSL
        struct
        {
             ma_ptr pOutputMixObj;

ma_ptr pOutputMix;

ma_ptr pAudioPlayerObj;

ma_ptr pAudioPlayer;

ma_ptr pAudioRecorderObj;

ma_ptr pAudioRecorder;

ma_ptr pBufferQueuePlayback;

ma_ptr pBufferQueueCapture;
ma_bool32 isDrainingCapture;
ma_bool32 isDrainingPlayback;
ma_uint32 currentBufferIndexPlayback;
ma_uint32 currentBufferIndexCapture;
ma_uint8* pBufferPlayback;     
ma_uint8* pBufferCapture;
ma_pcm_rb duplexRB;
        } opensl;
#endif
#if MA_SUPPORT_WEBAUDIO
        struct
        {
            int indexPlayback;            
int indexCapture;
ma_pcm_rb duplexRB;            
        } webaudio;
#endif
#if MA_SUPPORT_NULL
        struct
        {
            ma_thread deviceThread;
ma_event operationEvent;
ma_event operationCompletionEvent;
ma_uint32 operation;
ma_result operationResult;
ma_timer timer;
double priorRunTime;
ma_uint32 currentPeriodFramesRemainingPlayback;
ma_uint32 currentPeriodFramesRemainingCapture;
ma_uint64 lastProcessedFramePlayback;
ma_uint32 lastProcessedFrameCapture;
ma_bool32 isStarted;
        } null_device;
#endif
    };
};
#if defined(_MSC_VER) && !defined(__clang__)
#pragma warning(pop)
#else
#pragma GCC diagnostic pop  
#endif

ma_context_config ma_context_config_init(void);

ma_result ma_context_init(ma_backend backends[], ma_uint32 backendCount, ma_context_config* pConfig, ma_context* pContext);

ma_result ma_context_uninit(ma_context* pContext);

ma_result ma_context_enumerate_devices(ma_context* pContext, ma_enum_devices_callback_proc callback, void* pUserData);

ma_result ma_context_get_devices(ma_context* pContext, ma_device_info** ppPlaybackDeviceInfos, ma_uint32* pPlaybackDeviceCount, ma_device_info** ppCaptureDeviceInfos, ma_uint32* pCaptureDeviceCount);

ma_result ma_context_get_device_info(ma_context* pContext, ma_device_type deviceType, ma_device_id* pDeviceID, ma_share_mode shareMode, ma_device_info* pDeviceInfo);

ma_bool32 ma_context_is_loopback_supported(ma_context* pContext);



ma_device_config ma_device_config_init(ma_device_type deviceType);


ma_result ma_device_init(ma_context* pContext, ma_device_config* pConfig, ma_device* pDevice);

ma_result ma_device_init_ex(ma_backend backends[], ma_uint32 backendCount, ma_context_config* pContextConfig, ma_device_config* pConfig, ma_device* pDevice);

void ma_device_uninit(ma_device* pDevice);

ma_result ma_device_start(ma_device* pDevice);

ma_result ma_device_stop(ma_device* pDevice);

ma_bool32 ma_device_is_started(ma_device* pDevice);

ma_result ma_device_set_master_volume(ma_device* pDevice, float volume);

ma_result ma_device_get_master_volume(ma_device* pDevice, float* pVolume);

ma_result ma_device_set_master_gain_db(ma_device* pDevice, float gainDB);

ma_result ma_device_get_master_gain_db(ma_device* pDevice, float* pGainDB);



ma_result ma_mutex_init(ma_context* pContext, ma_mutex* pMutex);

void ma_mutex_uninit(ma_mutex* pMutex);

void ma_mutex_lock(ma_mutex* pMutex);

void ma_mutex_unlock(ma_mutex* pMutex);


const char* ma_get_backend_name(ma_backend backend);

ma_bool32 ma_is_loopback_supported(ma_backend backend);


ma_uint32 ma_scale_buffer_size(ma_uint32 baseBufferSize, float scale);

ma_uint32 ma_calculate_buffer_size_in_milliseconds_from_frames(ma_uint32 bufferSizeInFrames, ma_uint32 sampleRate);

ma_uint32 ma_calculate_buffer_size_in_frames_from_milliseconds(ma_uint32 bufferSizeInMilliseconds, ma_uint32 sampleRate);

void ma_zero_pcm_frames(void* p, ma_uint32 frameCount, ma_format format, ma_uint32 channels);

void ma_clip_samples_f32(float* p, ma_uint32 sampleCount);
MA_INLINE void ma_clip_pcm_frames_f32(float* p, ma_uint32 frameCount, ma_uint32 channels) { ma_clip_samples_f32(p, frameCount * channels); }

void ma_copy_and_apply_volume_factor_u8(ma_uint8* pSamplesOut, ma_uint8* pSamplesIn, ma_uint32 sampleCount, float factor);
void ma_copy_and_apply_volume_factor_s16(ma_int16* pSamplesOut, ma_int16* pSamplesIn, ma_uint32 sampleCount, float factor);
void ma_copy_and_apply_volume_factor_s24(void* pSamplesOut, void* pSamplesIn, ma_uint32 sampleCount, float factor);
void ma_copy_and_apply_volume_factor_s32(ma_int32* pSamplesOut, ma_int32* pSamplesIn, ma_uint32 sampleCount, float factor);
void ma_copy_and_apply_volume_factor_f32(float* pSamplesOut, float* pSamplesIn, ma_uint32 sampleCount, float factor);

void ma_apply_volume_factor_u8(ma_uint8* pSamples, ma_uint32 sampleCount, float factor);
void ma_apply_volume_factor_s16(ma_int16* pSamples, ma_uint32 sampleCount, float factor);
void ma_apply_volume_factor_s24(void* pSamples, ma_uint32 sampleCount, float factor);
void ma_apply_volume_factor_s32(ma_int32* pSamples, ma_uint32 sampleCount, float factor);
void ma_apply_volume_factor_f32(float* pSamples, ma_uint32 sampleCount, float factor);

void ma_copy_and_apply_volume_factor_pcm_frames_u8(ma_uint8* pPCMFramesOut, ma_uint8* pPCMFramesIn, ma_uint32 frameCount, ma_uint32 channels, float factor);
void ma_copy_and_apply_volume_factor_pcm_frames_s16(ma_int16* pPCMFramesOut, ma_int16* pPCMFramesIn, ma_uint32 frameCount, ma_uint32 channels, float factor);
void ma_copy_and_apply_volume_factor_pcm_frames_s24(void* pPCMFramesOut, void* pPCMFramesIn, ma_uint32 frameCount, ma_uint32 channels, float factor);
void ma_copy_and_apply_volume_factor_pcm_frames_s32(ma_int32* pPCMFramesOut, ma_int32* pPCMFramesIn, ma_uint32 frameCount, ma_uint32 channels, float factor);
void ma_copy_and_apply_volume_factor_pcm_frames_f32(float* pPCMFramesOut, float* pPCMFramesIn, ma_uint32 frameCount, ma_uint32 channels, float factor);
void ma_copy_and_apply_volume_factor_pcm_frames(void* pFramesOut, void* pFramesIn, ma_uint32 frameCount, ma_format format, ma_uint32 channels, float factor);

void ma_apply_volume_factor_pcm_frames_u8(ma_uint8* pFrames, ma_uint32 frameCount, ma_uint32 channels, float factor);
void ma_apply_volume_factor_pcm_frames_s16(ma_int16* pFrames, ma_uint32 frameCount, ma_uint32 channels, float factor);
void ma_apply_volume_factor_pcm_frames_s24(void* pFrames, ma_uint32 frameCount, ma_uint32 channels, float factor);
void ma_apply_volume_factor_pcm_frames_s32(ma_int32* pFrames, ma_uint32 frameCount, ma_uint32 channels, float factor);
void ma_apply_volume_factor_pcm_frames_f32(float* pFrames, ma_uint32 frameCount, ma_uint32 channels, float factor);
void ma_apply_volume_factor_pcm_frames(void* pFrames, ma_uint32 frameCount, ma_format format, ma_uint32 channels, float factor);


float ma_factor_to_gain_db(float factor);

float ma_gain_db_to_factor(float gain);

#endif  


#if !MA_NO_DECODING || !MA_NO_ENCODING
typedef enum
        {
    ma_seek_origin_start,
    ma_seek_origin_current
}
ma_seek_origin;

typedef enum
{
    ma_resource_format_wav
}
ma_resource_format;
#endif

#ifndef MA_NO_DECODING
typedef struct ma_decoder ma_decoder;

typedef size_t(* ma_decoder_read_proc)                    (ma_decoder* pDecoder, void* pBufferOut, size_t bytesToRead);            
typedef ma_bool32(* ma_decoder_seek_proc)                    (ma_decoder* pDecoder, int byteOffset, ma_seek_origin origin);
typedef ma_uint64(* ma_decoder_read_pcm_frames_proc)         (ma_decoder* pDecoder, void* pFramesOut, ma_uint64 frameCount);                
typedef ma_result(* ma_decoder_seek_to_pcm_frame_proc)       (ma_decoder* pDecoder, ma_uint64 frameIndex);
typedef ma_result(* ma_decoder_uninit_proc)                  (ma_decoder* pDecoder);
typedef ma_uint64(* ma_decoder_get_length_in_pcm_frames_proc)(ma_decoder* pDecoder);

typedef struct
{
    ma_format format;
ma_uint32 channels;
ma_uint32 sampleRate;
ma_channel channelMap[MA_MAX_CHANNELS];
ma_channel_mix_mode channelMixMode;
ma_dither_mode ditherMode;
struct
            {
        ma_resample_algorithm algorithm;
struct
                {
            ma_uint32 lpfPoles;
    }
    linear;
        struct
        {
            int quality;
}
speex;
    } resampling;
    ma_allocation_callbacks allocationCallbacks;
} ma_decoder_config;

struct ma_decoder
{
    ma_decoder_read_proc onRead;
    ma_decoder_seek_proc onSeek;
    void* pUserData;
    ma_uint64 readPointer;
    ma_format internalFormat;
    ma_uint32 internalChannels;
    ma_uint32 internalSampleRate;
    ma_channel internalChannelMap[MA_MAX_CHANNELS];
    ma_format outputFormat;
    ma_uint32 outputChannels;
    ma_uint32 outputSampleRate;
    ma_channel outputChannelMap[MA_MAX_CHANNELS];
    ma_data_converter converter;
    ma_allocation_callbacks allocationCallbacks;
    ma_decoder_read_pcm_frames_proc onReadPCMFrames;
    ma_decoder_seek_to_pcm_frame_proc onSeekToPCMFrame;
    ma_decoder_uninit_proc onUninit;
    ma_decoder_get_length_in_pcm_frames_proc onGetLengthInPCMFrames;
    void* pInternalDecoder;
    struct
    {
        const ma_uint8* pData;
    size_t dataSize;
    size_t currentReadPos;
}
memory;               
};

ma_decoder_config ma_decoder_config_init(ma_format outputFormat, ma_uint32 outputChannels, ma_uint32 outputSampleRate);

ma_result ma_decoder_init(ma_decoder_read_proc onRead, ma_decoder_seek_proc onSeek, void* pUserData, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_wav(ma_decoder_read_proc onRead, ma_decoder_seek_proc onSeek, void* pUserData, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_flac(ma_decoder_read_proc onRead, ma_decoder_seek_proc onSeek, void* pUserData, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_vorbis(ma_decoder_read_proc onRead, ma_decoder_seek_proc onSeek, void* pUserData, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_mp3(ma_decoder_read_proc onRead, ma_decoder_seek_proc onSeek, void* pUserData, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_raw(ma_decoder_read_proc onRead, ma_decoder_seek_proc onSeek, void* pUserData, ma_decoder_config* pConfigIn, ma_decoder_config* pConfigOut, ma_decoder* pDecoder);

ma_result ma_decoder_init_memory(void* pData, size_t dataSize, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_memory_wav(void* pData, size_t dataSize, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_memory_flac(void* pData, size_t dataSize, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_memory_vorbis(void* pData, size_t dataSize, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_memory_mp3(void* pData, size_t dataSize, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_memory_raw(void* pData, size_t dataSize, ma_decoder_config* pConfigIn, ma_decoder_config* pConfigOut, ma_decoder* pDecoder);

ma_result ma_decoder_init_file(char* pFilePath, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_file_wav(char* pFilePath, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_file_flac(char* pFilePath, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_file_vorbis(char* pFilePath, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_file_mp3(char* pFilePath, ma_decoder_config* pConfig, ma_decoder* pDecoder);

ma_result ma_decoder_init_file_w(wchar_t* pFilePath, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_file_wav_w(wchar_t* pFilePath, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_file_flac_w(wchar_t* pFilePath, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_file_vorbis_w(wchar_t* pFilePath, ma_decoder_config* pConfig, ma_decoder* pDecoder);
ma_result ma_decoder_init_file_mp3_w(wchar_t* pFilePath, ma_decoder_config* pConfig, ma_decoder* pDecoder);

ma_result ma_decoder_uninit(ma_decoder* pDecoder);
ma_uint64 ma_decoder_get_length_in_pcm_frames(ma_decoder* pDecoder);
ma_uint64 ma_decoder_read_pcm_frames(ma_decoder* pDecoder, void* pFramesOut, ma_uint64 frameCount);
ma_result ma_decoder_seek_to_pcm_frame(ma_decoder* pDecoder, ma_uint64 frameIndex);

ma_result ma_decode_file(char* pFilePath, ma_decoder_config* pConfig, ma_uint64* pFrameCountOut, void** ppDataOut);
ma_result ma_decode_memory(void* pData, size_t dataSize, ma_decoder_config* pConfig, ma_uint64* pFrameCountOut, void** ppDataOut);

typedef struct ma_encoder ma_encoder;

typedef size_t(* ma_encoder_write_proc)           (ma_encoder* pEncoder, void* pBufferIn, size_t bytesToWrite);
typedef ma_bool32(* ma_encoder_seek_proc)            (ma_encoder* pEncoder, int byteOffset, ma_seek_origin origin);
typedef ma_result(* ma_encoder_init_proc)            (ma_encoder* pEncoder);
typedef void (* ma_encoder_uninit_proc) (ma_encoder* pEncoder);
typedef ma_uint64(* ma_encoder_write_pcm_frames_proc)(ma_encoder* pEncoder, void* pFramesIn, ma_uint64 frameCount);

typedef struct
{
    ma_resource_format resourceFormat;
ma_format format;
ma_uint32 channels;
ma_uint32 sampleRate;
ma_allocation_callbacks allocationCallbacks;
} ma_encoder_config;

ma_encoder_config ma_encoder_config_init(ma_resource_format resourceFormat, ma_format format, ma_uint32 channels, ma_uint32 sampleRate);

struct ma_encoder
{
    ma_encoder_config config;
    ma_encoder_write_proc onWrite;
    ma_encoder_seek_proc onSeek;
    ma_encoder_init_proc onInit;
    ma_encoder_uninit_proc onUninit;
    ma_encoder_write_pcm_frames_proc onWritePCMFrames;
    void* pUserData;
    void* pInternalEncoder;
    void* pFile;
};

ma_result ma_encoder_init(ma_encoder_write_proc onWrite, ma_encoder_seek_proc onSeek, void* pUserData, ma_encoder_config* pConfig, ma_encoder* pEncoder);
ma_result ma_encoder_init_file(char* pFilePath, ma_encoder_config* pConfig, ma_encoder* pEncoder);
ma_result ma_encoder_init_file_w(wchar_t* pFilePath, ma_encoder_config* pConfig, ma_encoder* pEncoder);
void ma_encoder_uninit(ma_encoder* pEncoder);
ma_uint64 ma_encoder_write_pcm_frames(ma_encoder* pEncoder, void* pFramesIn, ma_uint64 frameCount);


internal enum ma_waveform_type
{
    ma_waveform_type_sine,
    ma_waveform_type_square,
    ma_waveform_type_triangle,
    ma_waveform_type_sawtooth
}

internal struct ma_waveform_config
{
    ma_format format;
    ma_uint32 channels;
    ma_uint32 sampleRate;
    ma_waveform_type type;
    double amplitude;
    double frequency;
}

ma_waveform_config ma_waveform_config_init(ma_format format, ma_uint32 channels, ma_uint32 sampleRate, ma_waveform_type type, double amplitude, double frequency);

internal struct ma_waveform
{
    ma_waveform_config config;
    double advance;
    double time;
}
*/
    }
}