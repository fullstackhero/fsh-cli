using FSHCodeGenerator.InterFaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FSHCodeGenerator.Classes
{
    public class GetEntities : IGetEntities
    {
        private readonly IConfiguration _config;
        private readonly ILogger<GetEntities> _log;      
        public static Dictionary<string, string> entities = new Dictionary<string, string>();

        public GetEntities(IConfiguration config, ILogger<GetEntities> log) // ,ConsoleDataService consoleDataService)
        {
            _config = config;
            _log = log;
            //  _dataService = consoleDataService;

        }
        public async Task<Dictionary<string, string>> Run()

        {

            var sourceSettings = _config.GetSection("GenerateSourcesSettings").Get<SourceGenSettings>();
            var checkentities = new Dictionary<string, string>();
            string entitysingle = string.Empty;
            string entityplural = string.Empty;

            foreach (string line in File.ReadLines(Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToContextfile, sourceSettings.DbContext)))
            {
                if (line.Contains("DbSet"))
                {
                    string trimLine = line.Trim();
                    int start = trimLine.IndexOf('<');                    
                    int end = trimLine.IndexOf('>');                   
                    entitysingle = trimLine.Substring(start + 1, end - 1 - start);                    
                    int endplural = trimLine.IndexOf('=');
                    entityplural = trimLine.Substring(end + 1, endplural - 1 - end);                    

                        checkentities.Add(entitysingle.Trim(), entityplural.Trim());
                    
                }
            }
            _log.LogInformation(" Entities from dbContext : {0}", checkentities.Count);
            entities = checkentities;
            return entities;
        }
    }
}

