using AKSDotNetCore.ConsoleApp.AdoDotNetCoreExamples;
using AKSDotNetCore.ConsoleApp.DapperExamples;
using AKSDotNetCore.ConsoleApp.EFCoreExamples;
using AKSDotNetCore.ConsoleApp.HttpClientExamples;
using AKSDotNetCore.ConsoleApp.RestClientExamples;

Console.WriteLine("Hello, World!");

//AdoDotNetCoreExample adoDotNetExample = new AdoDotNetCoreExample();
//adoDotNetExample.Run();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Run();

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.Run();

//RestClientExample restClientExample = new RestClientExample();
//await restClientExample.Run();

Console.ReadKey();