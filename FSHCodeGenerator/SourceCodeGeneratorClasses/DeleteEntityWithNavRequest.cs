namespace FSHCodeGenerator.SourceCodeGeneratorClasses;
internal class DeleteEntityWithNavRequest
{
    public DeleteEntityWithNavRequest(string filesavelocation, string pathtobasicsources, string stringnamespace, string entity, string entitynameplural, string eventsusing)
    {
        string basicsources = pathtobasicsources + "DeleteEntityWithNavRequest.txt";
        string thenamespace = stringnamespace + entitynameplural;
        string entitytolower = entity.ToLower();

        string deleteentitywithnavsource = File.ReadAllText(basicsources)
            .Replace("<&EventsUsing&>", eventsusing)
            .Replace("<&StringNameSpace&>", thenamespace)
            .Replace("<&Entity&>", entity)
            .Replace("<&EntityToLower&>", entitytolower);

        File.WriteAllText(filesavelocation+ "/" + "Delete" + entity + "Request.cs", deleteentitywithnavsource);

    }
}
