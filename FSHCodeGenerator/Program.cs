using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using FSHCodeGenerator.Classes;
using FSHCodeGenerator.InterFaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FSHCodeGenerator
{
    class Program
    {

        private static readonly CancellationTokenSource _cts = new CancellationTokenSource();

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
           //BuildConfig(builder);

           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = builder.Build();
            var mySourceGenSettings = config.GetSection("GenerateSourcesSettings").Get<SourceGenSettings>();
            var myDatabaseSettings = config.GetSection("DataBaseSettings").Get<DataBaseSettings>();
            var appdbcontext = mySourceGenSettings.DbContext;
            var connstring = myDatabaseSettings.ConnectionString;
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Starting...");

            var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
              
                services.AddDbContext<SourceGenContext>( options => options.UseSqlServer(connstring));
                services.AddTransient<ISourceGenerator, SourceGenerator>();
                services.AddTransient<IGenerateSources, GenerateSources>();
                services.AddTransient<IGetEntities, GetEntities>();
            })
               .UseSerilog()
               .Build();

            //var mySourceGenSettings = config.GetSection("GenerateSourcesSettings").Get<GenerateSourcesSettings>();


            Log.Logger.Information(" List Application settings.. Verify!");
            var sgen = ActivatorUtilities.CreateInstance<SourceGenerator>(host.Services);
            var result = sgen.Run();
            if (result.Result == true)
            {
                Console.WriteLine();
                var getent = ActivatorUtilities.CreateInstance<GetEntities>(host.Services);
                var entities = getent.Run();
                if (entities.Result.Count > 0)
                {
                    var gen = ActivatorUtilities.CreateInstance<GenerateSources>(host.Services);
                    await gen.Run();
                }
                //// var context = await ContextExists(_cancellationSource);

                //       //foreach (var customer in customersList)
                //       //{
                //       //    Console.WriteLine($"\t{customer}");
                //       //}


                //      // Console.WriteLine("Iterator");
                //       await Example3(_cancellationSource);


                //       var startTimeSpan = TimeSpan.Zero;
                //       var periodTimeSpan = TimeSpan.FromSeconds(20);

                //       Console.WriteLine();
                //       Console.WriteLine("Last example");
                //       var timer = new Timer(async (e) =>
                //       {
                //           var x = await Example1();

                //           Console.WriteLine(x.ToYesNo());

                //       }, null, startTimeSpan, periodTimeSpan);

                //       Console.ReadLine();
            }

            //  internal class MyContextFactory : IDesignTimeDbContextFactory<MyContext>
            //{
            //    public MyContext CreateDbContext(string[] args)
            //    {
            //        var dbContextBuilder = new DbContextOptionsBuilder<MyContext>();
            //        var connString = "myconnection string";
            //        dbContextBuilder.UseSqlServer(connString);
            //        return new MyContext(dbContextBuilder.Options);
            //    }
            //}


        }
    }

}
