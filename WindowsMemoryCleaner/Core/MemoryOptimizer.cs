using WindowsMemoryCleaner.Interop;
using WindowsMemoryCleaner.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsMemoryCleaner.Core;

internal static class MemoryOptimizer
{
    public static OptimizationResult OptimizeMemory()
    {
        ClearSystemCache();
        var processResult = CleanAllProcesses();
        CollectGarbage();

        return processResult;
    }

    public static OptimizationResult CleanAllProcesses()
    {
        int successCount = 0;
        int totalCount = 0;

        foreach (var process in Process.GetProcesses())
        {
            totalCount++;
            try
            {
                if (!process.HasExited)
                {
                    NativeMethods.EmptyWorkingSet(process.Handle);
                    successCount++;
                }
            }
            catch { }
            finally
            {
                process.Dispose();
            }
        }

        return new OptimizationResult
        {
            ProcessesCleaned = successCount,
            TotalProcesses = totalCount
        };
    }

    public static void ClearSystemCache()
    {
        ClearCache(4); // Standby Cache
        ClearCache(2); // Modified Page List
        ClearCache(1); // Working Sets
    }

    private static void CollectGarbage()
    {
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true, true);
        GC.WaitForPendingFinalizers();
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true, true);
    }

    private static void ClearCache(int command)
    {
        IntPtr ptr = Marshal.AllocHGlobal(sizeof(int));
        try
        {
            Marshal.WriteInt32(ptr, command);
            _ = NativeMethods.NtSetSystemInformation(80, ptr, sizeof(int));
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }
}