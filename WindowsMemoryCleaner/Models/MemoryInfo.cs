namespace WindowsMemoryCleaner.Models;
internal sealed class MemoryInfo
{
    public required ulong TotalPhysicalMB { get; init; }
    public required ulong AvailablePhysicalMB { get; init; }
    public required ulong UsedPhysicalMB { get; init; }
    public required uint MemoryLoad { get; init; }
}