using WindowsMemoryCleaner.Core;
using System.Security.Principal;
using System.Text.Json;

namespace WindowsMemoryCleaner;

public static class Program
{
    private static readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    static int Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return 0;
            }

            var command = args[0].ToLowerInvariant();
            var useJson = args.Contains("--json") || args.Contains("-j");

            return command switch
            {
                "optimize" or "o" => ExecuteOptimize(useJson),
                "clear-processes" or "cp" => ExecuteCleanProcesses(useJson),
                "clear-cache" or "cc" => ExecuteCleanCache(useJson),
                "status" or "s" => ExecuteStatus(useJson),
                "help" or "h" => ShowHelp(),
                _ => HandleInvalidCommand(command)
            };
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro: {ex.Message}");
            return 1;
        }
    }

    private static int ExecuteOptimize(bool useJson)
    {
        if (!CheckAdminPrivileges()) return 1;

        var memBefore = MemoryStatusProvider.GetMemoryInfo();
        var result = MemoryOptimizer.OptimizeMemory();
        var memAfter = MemoryStatusProvider.GetMemoryInfo();

        if (useJson)
        {
            var output = new
            {
                success = true,
                operation = "optimize",
                before = memBefore,
                after = memAfter,
                freed_mb = memBefore.UsedPhysicalMB - memAfter.UsedPhysicalMB,
                processes_cleaned = result.ProcessesCleaned,
                total_processes = result.TotalProcesses
            };
            Console.WriteLine(JsonSerializer.Serialize(output, _options));
        }
        else
        {
            Console.WriteLine("Otimização completa executada com sucesso!");
            Console.WriteLine($"Processos limpos: {result.ProcessesCleaned}/{result.TotalProcesses}");
            Console.WriteLine($"Memória liberada: {memBefore.UsedPhysicalMB - memAfter.UsedPhysicalMB:N0} MB");
        }

        return 0;
    }

    private static int ExecuteCleanProcesses(bool useJson)
    {
        if (!CheckAdminPrivileges()) return 1;

        var result = MemoryOptimizer.CleanAllProcesses();

        if (useJson)
        {
            var output = new
            {
                success = true,
                operation = "clear_processes",
                processes_cleaned = result.ProcessesCleaned,
                total_processes = result.TotalProcesses
            };
            Console.WriteLine(JsonSerializer.Serialize(output, _options));
        }
        else
        {
            Console.WriteLine($"Processos limpos: {result.ProcessesCleaned}/{result.TotalProcesses}");
        }

        return 0;
    }

    private static int ExecuteCleanCache(bool useJson)
    {
        if (!CheckAdminPrivileges()) return 1;

        MemoryOptimizer.ClearSystemCache();

        if (useJson)
        {
            var output = new
            {
                success = true,
                operation = "clear_cache"
            };
            Console.WriteLine(JsonSerializer.Serialize(output));
        }
        else
        {
            Console.WriteLine("Cache do sistema limpo com sucesso!");
        }

        return 0;
    }

    private static int ExecuteStatus(bool useJson)
    {
        var info = MemoryStatusProvider.GetMemoryInfo();

        if (useJson)
        {
            Console.WriteLine(JsonSerializer.Serialize(info, _options));
        }
        else
        {
            Console.WriteLine($"Uso de memória: {info.MemoryLoad}%");
            Console.WriteLine($"RAM Total: {info.TotalPhysicalMB:N0} MB");
            Console.WriteLine($"RAM Livre: {info.AvailablePhysicalMB:N0} MB");
            Console.WriteLine($"RAM em Uso: {info.UsedPhysicalMB:N0} MB");
        }

        return 0;
    }

    private static int ShowHelp()
    {
        Console.WriteLine(string.Join(Environment.NewLine,
            "Windows Memory Cleaner - Memory Optimizer",
            "",
            "USAGE:",
            "   wmc <command> [options]",
            "",
            "COMMANDS:",
            "   optimize, o             Full optimization (cache + processes + GC)",
            "   clear-processes, cp     Clear processes only",
            "   clear-cache, cc         Clear system cache only",
            "   status, s               Show memory status",
            "   help, h                 Show this help",
            "",
            "OPTIONS:",
            "   --json, -j              Return result in JSON format",
            "",
            "NOTES:",
            "   - Requires administrator privileges for full functionality",
            "   - Exit code: 0 = success, 1 = error"
        ));
        return 0;
    }

    private static int HandleInvalidCommand(string command)
    {
        Console.Error.WriteLine($"Invalid command: '{command}'");
        Console.Error.WriteLine("Use 'WindowsMemoryCleaner help' to see available commands.");
        return 1;
    }

    private static bool CheckAdminPrivileges()
    {
        if (IsAdministrator()) return true;

        Console.Error.WriteLine("ERROR: This operation requires administrator privileges.");
        Console.Error.WriteLine("Run the program as an administrator.");
        return false;
    }

    private static bool IsAdministrator()
    {
        try
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch
        {
            return false;
        }
    }
}