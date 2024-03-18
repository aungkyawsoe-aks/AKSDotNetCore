using AKSDotNetCore.ConsoleApp.AdoDotNetCoreExamples;
using AKSDotNetCore.ConsoleApp.DapperExamples;
using AKSDotNetCore.ConsoleApp.EFCoreExamples;
using AKSDotNetCore.ConsoleApp.HttpClientExamples;
using AKSDotNetCore.ConsoleApp.RefitExamples;
using AKSDotNetCore.ConsoleApp.RestClientExamples;
using Serilog;
using Serilog.Sinks.MSSqlServer;

//Log.Logger = new LoggerConfiguration()
//            .MinimumLevel.Debug()
//            .WriteTo.Console()
//            .WriteTo.File("logs/myapp.log", rollingInterval: RollingInterval.Hour)
//            .WriteTo
//            .MSSqlServer(
//                connectionString: "Server=.;Database=AKSDotNetCore;User ID=sa;Password=sa@123;TrustServerCertificate=True;",
//                sinkOptions: new MSSqlServerSinkOptions { TableName = "Tbl_Log", AutoCreateSqlTable = true })
//            .CreateLogger();

//Console.WriteLine("Hello, World!");
//Log.Information("Hello, world!");

//AdoDotNetCoreExample adoDotNetExample = new AdoDotNetCoreExample();
//adoDotNetExample.Run();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Run();

//HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.Run();

//RestClientExample restClientExample = new RestClientExample();
//await restClientExample.Run();

//Console.WriteLine("Please wait for Api");
//Console.ReadKey();

//RefitExample refitExample = new RefitExample();
//await refitExample.Run();

//int a = 10, b = 0;
//try
//{
//    Log.Debug("Dividing {A} by {B}", a, b);
//    Console.WriteLine(a / b);
//}
//catch (Exception ex)
//{
//    Log.Error(ex, "Something went wrong");
//}
//finally
//{
//    await Log.CloseAndFlushAsync();
//}

Console.ReadKey();