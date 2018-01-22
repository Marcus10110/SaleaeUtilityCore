# Saleae Utility Core

Command line utility written with C# in .NET targeting the cross-platform .net core framework.

## Converting Serial Export Data to Binary Files

The Saleae Logic Software supports exporting protocol analyzer results as text/csv files.

This utility just converts the csv export from the Async Serial analyzer to a binary file.

The output format is a single byte for every decoded serial byte from the analyzer.

## Setup

Install .NET core on your computer. Windows, Linux, and MacOS instructions can be found here:
https://www.microsoft.com/net/learn/get-started/

## Usage

First, using the Logic software, capture async serial data and properly configure the async serial analyzer.

Switch software's display format to hexadecimal, which is required for this utility to work.

Export the async serial results using he analyzer specific export. This is done by clicking the gear icon next to the serial analyzer and selecting "export as text/csv file".

Then, from the repository folder, run the application like so:

```
dotnet run -- serial.csv outputfile.bin
```

Where serial.csv is the file just exported from the saleae software and outputfile.bin is the desired output file.