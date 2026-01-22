\# MemCleaner



\*\*MemCleaner\*\* is a C# console application for Windows that helps optimize system memory. It can clear processes, free system cache, perform full memory optimization, and display memory status. It supports both human-readable and JSON output formats.



---



\## Features



\- \*\*Full Optimization\*\*: Clears system cache, working sets, and collects garbage.  

\- \*\*Process Cleanup\*\*: Frees memory used by all running processes.  

\- \*\*Cache Cleanup\*\*: Clears standby cache, modified page list, and working sets.  

\- \*\*Memory Status\*\*: Displays total, available, and used RAM with memory load percentage.  

\- \*\*JSON Output\*\*: Optionally output results in JSON format for scripting or logging.  

\- \*\*Administrator Check\*\*: Requires admin privileges for operations that modify system memory.  



---



\## Requirements



\- \*\*OS\*\*: Windows 7 or higher  

\- \*\*.NET Runtime\*\*: .NET 6 or higher  

\- \*\*Privileges\*\*: Some commands require administrator rights  



---



\## Installation



Clone this repository and build the project using the .NET SDK:



```bash

git clone https://github.com/yourusername/MemCleaner.git

cd MemCleaner

dotnet build -c Release

```



After building, you can run the executable from the `bin/Release/net6.0` folder.



---



\## Usage



```bash

MemCleaner <command> \[options]

```



\### Commands



| Command | Description |

|---------|-------------|

| `optimize`, `o` | Perform full optimization (cache + processes + GC) |

| `clear-processes`, `cp` | Clear memory used by all processes |

| `clear-cache`, `cc` | Clear system cache only |

| `status`, `s` | Display current memory usage |

| `help`, `h` | Show help information |



\### Options



| Option | Description |

|--------|-------------|

| `--json`, `-j` | Output results in JSON format |



---



\### Examples



1\. \*\*Full Optimization:\*\*



```bash

MemCleaner optimize

```



2\. \*\*Clean Processes Only in JSON:\*\*



```bash

MemCleaner clear-processes --json

```



3\. \*\*Show Memory Status:\*\*



```bash

MemCleaner status

```



---



\## Sample Output



\*\*Human-readable:\*\*



```

Memory optimization completed successfully!

Processes cleaned: 45/120

Memory freed: 120 MB

```



\*\*JSON:\*\*



```json

{

&nbsp; "success": true,

&nbsp; "operation": "optimize",

&nbsp; "before": {

&nbsp;   "TotalPhysicalMB": 16384,

&nbsp;   "AvailablePhysicalMB": 4500,

&nbsp;   "UsedPhysicalMB": 11884,

&nbsp;   "MemoryLoad": 73

&nbsp; },

&nbsp; "after": {

&nbsp;   "TotalPhysicalMB": 16384,

&nbsp;   "AvailablePhysicalMB": 4620,

&nbsp;   "UsedPhysicalMB": 11764,

&nbsp;   "MemoryLoad": 71

&nbsp; },

&nbsp; "freed\_mb": 120,

&nbsp; "processes\_cleaned": 45,

&nbsp; "total\_processes": 120

}

```



---



\## Notes



\- Administrator privileges are required for commands that modify memory.  

\- Exit code `0` indicates success, `1` indicates an error.  

\- Use `MemCleaner help` to display the usage guide at any time.



