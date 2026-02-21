// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKManagedStreamHasPositionProxyDelegate
using System;
using System.Runtime.InteropServices;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
[return: MarshalAs(UnmanagedType.I1)]
internal unsafe delegate bool SKManagedStreamHasPositionProxyDelegate(IntPtr P_0, void* P_1);

