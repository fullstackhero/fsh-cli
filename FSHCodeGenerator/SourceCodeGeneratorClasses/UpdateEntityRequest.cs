namespace FSHCodeGenerator.SourceCodeGeneratorClasses;
internal class UpdateEntityRequest
{
    public UpdateEntityRequest(string pathtodata, string filesavelocation, string pathtobasicsources, string stringnamespace, string entity, string entitynameplural, string eventpath, string validatortype, string validatorname, string propertyLines, string request, string guidlines)
    {
        string basicsources = pathtobasicsources + "UpDateEntityRequest.txt";
        string thenamespace = stringnamespace + entitynameplural;
        string entitytolower = entity.ToLower();
        string updateentityrequest = string.Empty;
        //string request = string.Empty;
        string datap = pathtodata + "/" + entity + ".cs";
        string relationalLines = string.Empty;
       
        updateentityrequest = File.ReadAllText(basicsources)
            .Replace("<&EntityPlural&>", entitynameplural)
            .Replace("<&EventsPath&>", eventpath)
            .Replace("<&PropertyLines&>", propertyLines)
            .Replace("<&GuidLines&>", guidlines)
            .Replace("<&StringNameSpace&>", thenamespace)
            .Replace("<&Entity&>", entity)
            .Replace("<&EntityToLower&>", entitytolower)
            .Replace("<&Request&>", request)
            .Replace("<&ValidatorName&>", validatorname)
            .Replace("<&ValidatorNameToLower&>", validatorname.ToLower());


        File.WriteAllText(filesavelocation+"/" + "Update" + entity + "Request.cs", updateentityrequest);
    }
}
