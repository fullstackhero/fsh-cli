namespace FSHCodeGenerator.Classes
{
    public class SourceGenSettings
    {
     //   public const string GenerateSourcesSettings = "GenerateSourcesSettings";
        public string PathToFSHBoilerPlate { get; set; } = String.Empty;       
        public string PathToContextfile { get; set; } = String.Empty;
        public string DbContext { get; set; } = String.Empty;
        public string PathToData { get; set; } = String.Empty;
        public string StringNameSpace { get; set; } = String.Empty;
        public string PermissionsNameSpace { get; set; } = String.Empty;
        public string PathToControllers { get; set; } = String.Empty;
        public string ControllersNamespace { get; set; } = String.Empty;
        public string PathToApplicationCatalog { get; set; } = String.Empty;
        public string DetermineDomainEntityPath { get; set; } = String.Empty;
        public string EventsUsing { get; set; } = String.Empty;
        public string PathToPermissions { get; set; } = String.Empty;
        public string LocalSourcesGeneratorClasses { get; set; } = String.Empty;
        public string LocalTxtSourcesPath { get; set; } = String.Empty;

    }
}
