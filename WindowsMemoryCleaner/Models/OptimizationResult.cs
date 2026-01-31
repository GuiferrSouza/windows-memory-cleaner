namespace WindowsMemoryCleaner.Models;
internal sealed class OptimizationResult
{
    public required int ProcessesCleaned { get; init; }
    public required int TotalProcesses { get; init; }
}