using MemCleaner.Interop;
using MemCleaner.Models;
using System.Runtime.InteropServices;

namespace MemCleaner.Core;

internal static class MemoryStatusProvider
{
    public static MemoryInfo GetMemoryInfo()
    {
        var mem = new MEMORYSTATUSEX { dwLength = (uint)Marshal.SizeOf<MEMORYSTATUSEX>() };

        if (!NativeMethods.GlobalMemoryStatusEx(ref mem))
            throw new InvalidOperationException("Failed to retrieve memory status");

        return new MemoryInfo
        {
            TotalPhysicalMB = mem.ullTotalPhys / 1024 / 1024,
            AvailablePhysicalMB = mem.ullAvailPhys / 1024 / 1024,
            UsedPhysicalMB = (mem.ullTotalPhys - mem.ullAvailPhys) / 1024 / 1024,
            MemoryLoad = mem.dwMemoryLoad
        };
    }
}