
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
        private string _validatorType = string.Empty;
        private string _validatorName = string.Empty;
        private bool _hasNavigations = false;
        private bool fakeref = true;


        private IDictionary _foreignKeyEntities = new Dictionary<string, string>();



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
            var _parentKeys = new Dictionary<string, string>();
            var _foreignKeys = new Dictionary<string, string>();
            var _newEntities = new Dictionary<string, string>();
            var _applicationCatalog = string.Empty;

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
                        _entityName = string.Empty;
                        _validatorType = string.Empty;
                        _validatorName = string.Empty;
                        _entityName = t.DisplayName();
                        _repo_Repo = "_" + _entityName.ToLower() + "Repo,";
                        _repoRepo = _entityName.ToLower() + "Repo,";
                        _parentKeys.Clear();
                        _foreignKeys.Clear();

                        string applicationCatalog = Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToApplicationCatalog);

                        bool entityFound = theEntities.TryGetValue(t.DisplayName(), out _entityPlural);

                        if (entityFound) // the Ef core entity is found in the Dictionary of our context entities
                        {
                            // feed to Dictionary to holds only our new entities to be used outside foreach loop
                            _newEntities.Add(_entityName, _entityPlural);
                            string pathToCatalog = Path.Combine(applicationCatalog, _entityPlural.Trim());


                            if (!Directory.Exists(pathToCatalog))  // new Entity
                            {
                                _entityName = t.DisplayName();                                
                                _log.LogInformation("Resolving => " + t.DisplayName());
                                
                                Directory.CreateDirectory(pathToCatalog = Path.Combine(Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToApplicationCatalog + _entityPlural.Trim())));

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

                                    var parentkeys = t.GetDeclaredForeignKeys();
                                    if (parentkeys != null)
                                    {
                                        foreach (var key in parentkeys)
                                        {
                                            _principalEntity = key.PrincipalEntityType.DisplayName();
                                            //   _principalEntity = key.PrincipalEntityType.GetTableName();
                                            // string value = theEntities[_principalEntity];
                                            if (_principalEntity != _entityName)
                                            {
                                                _parentKeys.Add(_principalEntity, _entityPlural);
                                            }
                                        }
                                    }


                                    var stringNamespace = sourceSettings.StringNameSpace;
                                    this.EntityProperties(_entityName, _entityPlural, pathToEntity, stringNamespace, theEntities);

                                    applicationCatalog = applicationCatalog + "/" + _entityPlural.Trim();
                                    // create the source files 

                                    if (_hasNavigations)
                                    {
                                        Log.Information("Create EventHandlers");
                                        string pathToEventHandlers = applicationCatalog + "\\EventHandlers";
                                        CreateEventHandlers newEventHandlers = new CreateEventHandlers(pathToEventHandlers, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural);

                                        Log.Information("Create DTO");
                                        CreateDto newNavCreateDto = new CreateDto(applicationCatalog, _propertyLines.Trim() , _detailLines.Trim(), _parentLines.Trim(), _guidLines.Trim(), _dtoLines.Trim(), _theusings, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural.Trim(), _hasNavigations); ;

                                        Log.Information("Create " + _entityName + " Request");
                                        CreateChildEntityRequest newChildEntityRequest = new CreateChildEntityRequest(applicationCatalog, _request, _propertyLines.Trim(), sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, sourceSettings.EventsUsing, _guidLines.Trim());

                                        Log.Information("Create " + _entityName + " Request Validator");
                                        CreateEntityRequestValidator newCreateEntityRequestValidator = new CreateEntityRequestValidator(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, sourceSettings.EventsUsing, _validatorType, _validatorName, _parentKeys, _theusings);

                                        Log.Information("Create Delete " + _entityName + " Request");
                                        DeleteEntityWithNavRequest newDeleteEntityWithNavRequest = new DeleteEntityWithNavRequest(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural.Trim(), sourceSettings.EventsUsing);

                                        Log.Information("Create" + _entityName + " By Parent Spec");
                                        CreateEntityByParentSpec newEntityByParentSpec = new CreateEntityByParentSpec(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, _parentKeys);

                                        Log.Information("Create" + _entityName + " By Name spec");
                                        CreateEntityByTypeSpec newEntityByNameSpec = new CreateEntityByTypeSpec(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, _validatorType, _validatorName);

                                        Log.Information("Create Get" + _entityName + " via Dapper request");
                                        GetEntityViaDapperRequest newGetEntityViaDapperRequest = new GetEntityViaDapperRequest(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural);

                                        Log.Information("Create Get" + _entityName + " Request");
                                        GetEntityWithNavRequest newGetEntityWithNavRequest = new GetEntityWithNavRequest(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, _parentKeys);

                                        Log.Information("Create Search" + _entityName + " Request");
                                        SearchParentEntityRequest newSearchParentEntityRequest = new SearchParentEntityRequest(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, _parentKeys);
                                        EntitiesBySearchRequestWithParentSpec newEntitiesBySearchRequestWithParentSpec = new EntitiesBySearchRequestWithParentSpec(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, _parentKeys);

                                        Log.Information("Create Update" + _entityName + " Request");
                                        UpdateEntityWithNavRequest newUpdateEntityWithNavRequest = new UpdateEntityWithNavRequest(applicationCatalog, _request, _propertyLines.Trim(), sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, sourceSettings.EventsUsing, _validatorType, _validatorName);
                                        UpdateEntityRequestValidator newUpdateEntityRequestValidator = new UpdateEntityRequestValidator(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, sourceSettings.EventsUsing, _validatorType, _validatorName, _parentKeys, _theusings);

                                        Log.Information("Create Controller");
                                        var controllerpath = Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToControllers);
                                        EntityChildController newEntityController = new EntityChildController(sourceSettings.LocalTxtSourcesPath, sourceSettings.ControllersNamespace, controllerpath, _entityName, _entityPlural, sourceSettings.StringNameSpace);
                                    }
                                    else
                                    {
                                        Log.Information("Create DTO");
                                        CreateDto newCreateDto = new CreateDto(applicationCatalog, _propertyLines.Trim(), _detailLines.Trim(), _parentLines.Trim(), _guidLines.Trim(), _dtoLines.Trim(), _theusings, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural.Trim(), _hasNavigations);
                                        Log.Information("Create " + _entityName + " Request");
                                        CreateParentEntityRequest newEntityCreateRequest = new CreateParentEntityRequest(applicationCatalog, _request, _propertyLines.Trim(), sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, sourceSettings.EventsUsing, _validatorType, _validatorName);
                                        Log.Information("Create Delete " + _entityName + " Request");
                                        DeleteEntityRequest newDeleteEntityRequest = new DeleteEntityRequest(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, _usingpathtochildren, _readrepositoryLines.Trim(), _publicrepositoryLine, _repo_Repo, _repoRepo, _foreignKeys);
                                        Log.Information("Create " + _entityName + " By Name spec");
                                        CreateEntityByTypeSpec newEntityByNameSpec = new CreateEntityByTypeSpec(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, _validatorType, _validatorName);
                                        Log.Information("Create Update Eentity Request");
                                        UpdateEntityRequest newEntityUpdateRequest = new UpdateEntityRequest(sourceSettings.PathToData, applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural, sourceSettings.EventsUsing, _validatorType, _validatorName, _propertyLines.Trim(),_request);
                                        Log.Information("Create Get" + _entityName + " Request");
                                        GetSingleResultEntityRequest newGetEntityRequest = new GetSingleResultEntityRequest(sourceSettings.PathToData, applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural);
                                        Log.Information("Create Search" + _entityName + " Request");
                                        SearchEntityRequest newSearchEntityRequest = new SearchEntityRequest(applicationCatalog, sourceSettings.LocalTxtSourcesPath, sourceSettings.StringNameSpace, _entityName, _entityPlural);
                                        Log.Information("Create Controller");
                                        var controllerpath = Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToControllers);
                                        EntityController newEntityController = new EntityController(sourceSettings.LocalTxtSourcesPath, sourceSettings.ControllersNamespace, controllerpath, _entityName, _entityPlural, sourceSettings.StringNameSpace);
                                    }                                  
                                }
                            }

                        }
                    });
            
            // Add Entity to permissions if it not exists
            
            Log.Information("Check and if not Exist Insert" + _entityName + " Permissions");
            string filepath = Path.Combine(sourceSettings.PathToFSHBoilerPlate, sourceSettings.PathToPermissions);
            CheckAndAddPermissions newCheckAndAddPermissions = new CheckAndAddPermissions(sourceSettings.LocalTxtSourcesPath, filepath, _newEntities, sourceSettings.PermissionsNameSpace);

            Log.Information("Sources Generated");
        }

        private void EntityProperties(string entity, string entitypLural, string dataPath, string stringNamespace, Dictionary<string, string> theEntities)
        {
           
            _validatorType = string.Empty;
            _validatorName = string.Empty;            
            _propertyLines = string.Empty;
            _guidLines = string.Empty;
            _parentLines = string.Empty;
            _detailLines = string.Empty;
            _dtoLines = string.Empty;
            string _entityToCheck = string.Empty;
            string _props = string.Empty;
            string getSet = string.Empty;


            // Chose to do it like this because the efcore getproperties function return everything also the AuditableEntity which we don't want..

            foreach (string line in File.ReadLines(Path.Combine(dataPath, entity + ".cs")))
            {

                if (line.Contains("public") && !line.Contains("virtual") && !line.Contains("class") && !line.Contains(entity))
                {

                    // split the line in parts to get properties
                    string trimLine = line.Trim();
                    string[] parts = trimLine.Split();

                    // validatortype and name needed by  EntityByNameSpec,EntityRequestValidator, UpdateWithNavrequest and UpdateRequestValidator
                    if (_validatorType == string.Empty && _validatorName == string.Empty)
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
                        _guidLines = _guidLines  + inputLine.Trim() + " " + getSet + Environment.NewLine + "\t";
                    }
                    else
                    {

                        _propertyLines = _propertyLines + inputLine.Trim() + " " + getSet + Environment.NewLine + "\t";
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
                        
                        _detailLines = _detailLines + "public " + checklineparts[+1] + " " + _entityToCheck + " " + txtparts[+2] + " { get; set; } = default!;" + Environment.NewLine + "\t";
                        _dtoLines = _dtoLines  + "public " + _entityToCheck + "Dto " + _entityToCheck + " { get; set; } = default!;" + Environment.NewLine + "\t";

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
                               
                                _parentLines = _parentLines + inputLine + " " + getSet + Environment.NewLine; // + "\t";
                                break;
                            }
                        }

                    }
                }
            }
        }
    }
}


