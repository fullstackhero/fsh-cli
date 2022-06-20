//using FSHCodeGenerator.InterFaces;
using FSHCodeGenerator.InterFaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace FSHCodeGenerator.Classes
{
    public class SourceGenerator : ISourceGenerator
    {
        //private readonly SourceGenSettingsOptions _options;
        private readonly IConfiguration _config;
        private readonly ILogger<SourceGenerator> _logger;
        private bool lContinue = false;
        //public SourceGen(IConfiguration configuration, SourceGenSettingsOptions options)
        public SourceGenerator(IConfiguration config, ILogger<SourceGenerator> log)
        {
            _config = config;
            _logger = log;
           

        }
        public async Task<bool> Run()
        {
            var sourceSettings = _config.GetSection("GenerateSourcesSettings").Get<SourceGenSettings>();          
            bool lDbContextExists = true;
           
            _logger.LogInformation("& Path To FSH Web - Api - BoilerPlate : " + sourceSettings.PathToFSHBoilerPlate);
            _logger.LogInformation("FullPath To DbContext : " + Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToContextfile, sourceSettings.DbContext));
            if (!File.Exists(Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToContextfile, sourceSettings.DbContext)))
            {
                lDbContextExists = false;
                _logger.LogError("DB context file not Found!!");
                return false;
            }
            _logger.LogInformation("Path To Data : " + sourceSettings.PathToData);
            _logger.LogInformation("NameSpace : " + sourceSettings.StringNameSpace);
            _logger.LogInformation("Path To Controllers : " + sourceSettings.PathToData);
            _logger.LogInformation("Controllers NameSpace : " + sourceSettings.PathToControllers);
            _logger.LogInformation("Path To Data : " + sourceSettings.PathToControllers);
            _logger.LogInformation("Path To ApplicationCatalog : " + sourceSettings.PathToApplicationCatalog);
            _logger.LogInformation("Domain Entity Path : " + sourceSettings.DetermineDomainEntityPath);
            _logger.LogInformation("Events using : " + sourceSettings.EventsUsing);
            _logger.LogInformation("Path to Permissions : " + sourceSettings.PathToPermissions);
            Console.WriteLine("");


            if (lDbContextExists)
            {
                _logger.LogInformation("Press Enter to Continue any other  key to quit ");
                var cki = Console.ReadKey(true);
                switch (cki.Key.ToString())
                {

                    case "Enter":
                        _logger.LogInformation("You pressed " + cki.Key.ToString());
                        _logger.LogInformation("Continuing");
                        lContinue = true;
                        break;
                    default:
                        _logger.LogInformation("You pressed which is not valid..." + cki.Key.ToString());
                        lContinue = false;
                        break;

                }
                Console.WriteLine("");
                if (lContinue)
                {
                    _logger.LogInformation("Determine entities");
                    return true;
                }
                else
                {
                    _logger.LogInformation("Application Terminated by User");
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }



    }

}
