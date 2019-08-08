# Sollicitus.NET
[![GitHub issues](https://img.shields.io/github/issues/davidfrp/Sollicitus.NET.svg)](https://github.com/davidfrp/Sollicitus.NET/issues)
[![Project license](https://img.shields.io/github/license/davidfrp/Sollicitus.NET.svg)](https://github.com/davidfrp/Sollicitus.NET/blob/master/LICENSE)

Sollicitus /solˈli.ki.tus/ is an open-source file shredder using zeroization and pseudo randomly generated data to overwrite files. 

## Example of use
```csharp
using System;
using System.IO;
using Sollicitus;

class Test
{
    static void Main() 
    {
        FileInfo fileInfo = new FileInfo(".\test.jpg");
        fileInfo.Shred();
    }
}
```
