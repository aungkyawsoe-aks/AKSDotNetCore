using AKSDotNetCore.ConsoleApp.AdoDotNetCoreExamples;
using AKSDotNetCore.ConsoleApp.DapperExamples;

Console.WriteLine("Hello, World!");

//AdoDotNetCoreExample adoDotNetExample = new AdoDotNetCoreExample();
//adoDotNetExample.Run();

DapperExample dapperExample = new DapperExample();
dapperExample.Run();

Console.ReadKey();