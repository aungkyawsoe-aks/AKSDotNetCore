using AKSDotNetCore.ConsoleApp.AdoDotNetCoreExamples;
using AKSDotNetCore.ConsoleApp.DapperExamples;
using AKSDotNetCore.ConsoleApp.EFCoreExamples;

Console.WriteLine("Hello, World!");

//AdoDotNetCoreExample adoDotNetExample = new AdoDotNetCoreExample();
//adoDotNetExample.Run();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Run();


Console.ReadKey();