# Windows Memory Cleaner

A C# console application for Windows that helps optimize system memory. It can clear processes, free system cache, perform full memory optimization, and display memory status. It supports both human-readable and JSON output formats.

---

## Features

- **Full Optimization**: Clears system cache, working sets, and collects garbage.  
- **Process Cleanup**: Frees memory used by all running processes.  
- **Cache Cleanup**: Clears standby cache, modified page list, and working sets.  
- **Memory Status**: Displays total, available, and used RAM with memory load percentage.  
- **JSON Output**: Optionally output results in JSON format for scripting or logging.  
- **Administrator Check**: Requires admin privileges for operations that modify system memory.  

---

## Requirements

- **OS**: Windows 7 or higher  
- **.NET Runtime**: .NET 6 or higher  
- **Privileges**: Some commands require administrator rights  

---

## Usage

```bash
wmc <command> [options]
```

### Commands

| Command | Description |
|---------|-------------|
| `optimize`, `o` | Perform full optimization (cache + processes + GC) |
| `clear-processes`, `cp` | Clear memory used by all processes |
| `clear-cache`, `cc` | Clear system cache only |
| `status`, `s` | Display current memory usage |
| `help`, `h` | Show help information |

### Options

| Option | Description |
|--------|-------------|
| `--json`, `-j` | Output results in JSON format |

---

### Examples

1. **Full Optimization:**

```bash
wmc optimize
```

2. **Clean Processes Only in JSON:**

```bash
wmc clear-processes --json
```

3. **Show Memory Status:**

```bash
wmc status
```

---

## Sample Output

**Human-readable:**

```
Memory optimization completed successfully!
Processes cleaned: 45/120
Memory freed: 120 MB
```

**JSON:**

```json
{
  "success": true,
  "operation": "optimize",
  "before": {
    "TotalPhysicalMB": 16384,
    "AvailablePhysicalMB": 4500,
    "UsedPhysicalMB": 11884,
    "MemoryLoad": 73
  },
  "after": {
    "TotalPhysicalMB": 16384,
    "AvailablePhysicalMB": 4620,
    "UsedPhysicalMB": 11764,
    "MemoryLoad": 71
  },
  "freed_mb": 120,
  "processes_cleaned": 45,
  "total_processes": 120
}
```

---

## Notes

- Administrator privileges are required for commands that modify memory.  
- Exit code `0` indicates success, `1` indicates an error.  
- Use `wmc help` to display the usage guide at any time.
