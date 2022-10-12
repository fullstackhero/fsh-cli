namespace FSHCodeGenerator.SourceCodeGeneratorClasses;
internal class SearchEntityRequest
{
    public SearchEntityRequest(string filesavelocation, string pathtobasicsources, string stringnamespace, string entity, string entitynameplural)
    {
        string thenamespace = stringnamespace + entitynameplural;
        string searchentityrequest = string.Empty;
        string entitytolower = entity.ToLower();
        string parentguids = string.Empty;
        string parententity = string.Empty;
        string parententityplural = string.Empty;      
        string basicsources = pathtobasicsources + "SearchEntityRequest.txt";
        searchentityrequest = File.ReadAllText(basicsources)
        .Replace("<&StringNameSpace&>", thenamespace)
       .Replace("<&EntityPlural&>", entitynameplural)
       .Replace("<&Entity&>", entity)
       .Replace("<&EntityToLower&>", entitytolower);      

        File.WriteAllText(filesavelocation + "/" + "Search" + entitynameplural + "Request.cs", searchentityrequest);

    }
}