using System.Threading.Tasks;
using Serilog;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using FSHCodeGenerator.InterFaces;

using Microsoft.Extensions.DependencyInjection;
using ILogger = Serilog.ILogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FSHCodeGenerator.SourceCodeGeneratorClasses;

namespace FSHCodeGenerator.Classes
{
    public class GenerateSources : IGenerateSources
    {
        private readonly IConfiguration _config;
        private readonly ILogger<GenerateSources> _log;
        private readonly SourceGenContext _genContext;
        private string _propertyLines = string.Empty;
        private string _entityName = string.Empty;
        private string _entityPlural = string.Empty;
        private string _usingpathtochildren = string.Empty;
        private string _declaringEntity = string.Empty;
        private string _readrepositoryLines = string.Empty;
        private string _publicrepositoryLine = string.Empty;
        private string _entityFullName = string.Empty;
        private string _principalEntity = string.Empty;
        private string _repo_Repo = string.Empty;
        private string _repoRepo = string.Empty;
        private string _detailLines = string.Empty;
        private string _parentLines = string.Empty;
        private string _guidLines = string.Empty;
        private string _dtoLines = string.Empty;
        private string _theusings = string.Empty;
        private string _request = string.Empty;
        private string _valuesingle = string.Empty;
        private bool _hasNavigations = false;


        private IDictionary _foreignKeys = new Dictionary<string, string>();
        private IDictionary _foreignKeyEntities = new Dictionary<string, string>();
        private IDictionary _principalKeys = new Dictionary<string, string>();


        public GenerateSources(ILogger<GenerateSources> log, IConfiguration config, SourceGenContext genContext)
        {
            _config = config;
            _log = log;
            _genContext = genContext;
        }
        public async Task Run()
        {
            _log.LogInformation("Start Creating Sources");
            var sourceSettings = _config.GetSection("GenerateSourcesSettings").Get<SourceGenSettings>();
            var myDatabaseSettings = _config.GetSection("DataBaseSettings").Get<DataBaseSettings>();
            var theEntities = GetEntities.entities;
            var entityTypes = _genContext.Model.GetEntityTypes();

            entityTypes?.ToList().ForEach(t =>
            {
                // clear entity related properties
                _propertyLines = string.Empty;
                _detailLines = string.Empty;
                _parentLines = string.Empty;
                _guidLines = String.Empty;
                _dtoLines = string.Empty;
                _theusings = string.Empty;
                _entityPlural = string.Empty;
                _foreignKeys.Clear();
                _readrepositoryLines = string.Empty;
                _publicrepositoryLine = string.Empty;
                _entityFullName = string.Empty;
                _entityName = t.DisplayName();
                _repo_Repo = "_" + _entityName.ToLower() + "Repo,";
                _repoRepo = _entityName.ToLower() + "Repo,";
                _principalKeys.Clear();

                string applicationCatalog = Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToApplicationCatalog);
                _log.LogInformation(t.DisplayName());
                bool entityFound = theEntities.TryGetValue(t.DisplayName(), out _entityPlural);

                if (entityFound) // the Ef core entity is found in the Dictionary of our context entities
                {
                    string pathToCatalog = Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToApplicationCatalog, _entityPlural.Trim());


                    if (!Directory.Exists(pathToCatalog))  // new Entity
                    {
                        _entityName = t.DisplayName();
                        _entityFullName = t.ClrType.FullName;
                        _log.LogInformation("Resolving => " + t.DisplayName());

                        //var efType = _genContext.Model.FindEntityType(typeof(T).FullName);                      
                        Directory.CreateDirectory(pathToCatalog = Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToApplicationCatalog + _entityPlural.Trim()));

                        var pathToEntity = Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToData);
                        _hasNavigations = t.GetNavigations().Count() > 0 ? true : false;
                        if (_hasNavigations)
                        {

                            // Get the foreignkeys from entity framework
                            var fKeys = t.GetForeignKeys().ToList();
                            if (fKeys.Any())  // we need eventhandlers Directory
                            {
                                Directory.CreateDirectory(pathToCatalog = Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToApplicationCatalog + _entityPlural.Trim()) + "/EventHandlers");

                                // string ent = t.DisplayName();
                                foreach (var fKey in fKeys)
                                {
                                    string fk = fKey.PrincipalEntityType.DisplayName();
                                    bool addnewline = _readrepositoryLines == string.Empty ? true : false;
                                    _usingpathtochildren = _usingpathtochildren + "using " + sourceSettings.StringNameSpace + _declaringEntity + ";" + Environment.NewLine;
                                    _readrepositoryLines = _readrepositoryLines + (addnewline ? " " : Environment.NewLine) + "private readonly IReadRepository<" + _valuesingle + "> _" + _valuesingle.ToLower() + "Repo;";
                                    _publicrepositoryLine = _publicrepositoryLine + " IReadRepository<" + _valuesingle + "> " + _valuesingle.ToLower() + "Repo" + ",";
                                    _repo_Repo = _repo_Repo + " _" + fk + "Repo,";
                                    _repoRepo = _repoRepo + " " + fk + "Repo,";
                                }
                            }
                            else
                            {
                                // reset _hasnavigations to false ef core returned no foreign keys  
                                // the _hasnavigations = true came from the use of a Guid (for other purposes) in the entity
                                _hasNavigations = false;
                            }

                            var stringNamespace = sourceSettings.StringNameSpace;
                            this.EntityProperties(_entityName, _entityPlural, pathToEntity, stringNamespace, theEntities);
                       

                            // create the source files 

                            if (_hasNavigations)
                            {
                                Log.Information("Create EventHandlers");                              
                                string pathToEventHandlers = applicationCatalog + _entityPlural.Trim() + "\\EventHandlers";                               
                                    CreateEventHandlers newEventHandlers = new CreateEventHandlers(pathToEventHandlers, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural);
                                Log.Information("Create " + _entityName + " Request");
                                CreateChildEntityRequest newChildEntityRequest = new CreateChildEntityRequest(applicationCatalog, _request, _propertyLines, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, sourceSettings.EventsUsing);

                            }
                            Log.Information("Create DTO");
                            CreateDto newCreateDto = new CreateDto(applicationCatalog, _propertyLines, _detailLines, _parentLines, _guidLines, _dtoLines, _theusings, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural.Trim(), _hasNavigations);
                       
                        }
                    }

                }
            });




        }

        private void EntityProperties(string entity, string entitypLural, string dataPath, string stringNamespace, Dictionary<string, string> theEntities)
        {
            string _props = string.Empty;
            string getSet = string.Empty;
            string _validatorType = string.Empty;
            string _validatorName = string.Empty;
            string _entityToCheck = string.Empty;
            //_guidLines = string.Empty;
            //_propertyLines = string.Empty;
            //_request = string.Empty;
            //_detailLines = string.Empty;
            //_dtoLines = string.Empty;
            //_parentLines = string.Empty;



            // Chose to do it like this because the efcore getproperties function return everything also the AuditableEntity which we don't want..

            foreach (string line in File.ReadLines(Path.Combine(dataPath, entity + ".cs")))
            {

                if (line.Contains("public") && !line.Contains("virtual") && !line.Contains("class") && !line.Contains(entity))
                {

                    // split the line in parts to get properties
                    string trimLine = line.Trim();
                    string[] parts = trimLine.Split();

                    // validatortype and name needed by  EntityByNameSpec,EntityRequestValidator, UpdateWithNavrequest and UpdateRequestValidator
                    if (_validatorType == String.Empty && _validatorName == String.Empty)
                    {
                        _validatorType = parts[1];
                        _validatorName = parts[2];
                    }
                    string inputLine = parts[0] + " " + parts[1] + " " + parts[2];
                    string requestPart = "request." + parts[2];
                    getSet = string.Equals(parts[1], "string") ? "{ get; set; } = default!;" : "{ get; set; }";
                    _request = _request + requestPart;
                    if (parts[1] == "Guid")
                    {
                        _guidLines = _guidLines + inputLine + " " + getSet + Environment.NewLine;
                    }
                    else
                    {
                        _propertyLines = _propertyLines + inputLine + " " + getSet + Environment.NewLine;
                    }

                    if (!String.IsNullOrEmpty(requestPart))
                    {
                        _request = _request + ", ";

                    }

                }
                if (line.Contains("public") && line.Contains("virtual")) // Navigation properties in entity
                {
                    // we need to get the properties from the navigation entity for the dto if the entity has a parent.
                    string trimLine = line.Trim();
                    string[] txtparts = trimLine.Split();
                    if (txtparts[2] == txtparts[3])
                    {
                        _entityToCheck = txtparts[2];
                        string[] checklineparts = line.Split();
                        _detailLines = _detailLines + "public " + checklineparts[+1] + " " + _entityToCheck + " " + txtparts[+2] + " { get; set; } = default!;" + Environment.NewLine;
                        _dtoLines = _dtoLines + "public " + _entityToCheck + "Dto " + _entityToCheck + " { get; set; } = default!;" + Environment.NewLine;

                        // Get the plural entitytocheck out of the array
                        bool entityFound = theEntities.TryGetValue(_entityToCheck, out string thePluralName);
                        if (entityFound)
                        {
                            _theusings = _theusings + "using " + stringNamespace + thePluralName.Trim() + ";" + Environment.NewLine;
                        }

                        // Get the first line of the _entityToCheck for the details dto
                        foreach (string virtualLine in File.ReadLines(Path.Combine(dataPath, _entityToCheck + ".cs")))
                        {
                            if (virtualLine.Contains("public") && !virtualLine.Contains("virtual") && !virtualLine.Contains("class") && !virtualLine.Contains(_entityToCheck))
                            {
                                string trimvirtualLine = virtualLine.Trim();
                                string[] parts = trimvirtualLine.Split();
                                getSet = string.Equals(parts[1], "string") ? "{ get; set; } = default!;" : "{ get; set; }";
                                string inputLine = parts[0] + " " + parts[1] + " " + _entityToCheck + parts[2];
                                _parentLines = _parentLines + inputLine + " " + getSet + Environment.NewLine;
                                break;
                            }
                        }

                    }
                }
            }
        }
    }
}


