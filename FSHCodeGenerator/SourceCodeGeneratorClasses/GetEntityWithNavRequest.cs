namespace FSHCodeGenerator.SourceCodeGeneratorClasses;
internal class GetEntityWithNavRequest
{
    public GetEntityWithNavRequest(string filesavelocation, string pathtobasicsources, string stringnamespace, string entity, string entitynameplural, Dictionary<string, string> fks)
    {
        string entityrequesthandle = string.Empty;
        string getentityhandle = string.Empty;
        string parententity = string.Empty;
        string ent = entity.Substring(0, 1).ToLower();
        string querystring = ".Where(" + ent + " => " + ent + ".Id == id)"+Environment.NewLine + "\t";

        
        var pk = fks.ElementAt(0);
        parententity = pk.Key;
        string handlename = fks.Count > 1 ? "Navigations" : parententity;
        string requesthandlesource = pathtobasicsources + "GetEntityRequestHandle.txt";
            getentityhandle = File.ReadAllText(requesthandlesource)
            .Replace("<&Entity&>", entity)
            .Replace("<&Parent&>", handlename);
            entityrequesthandle = entityrequesthandle + getentityhandle + Environment.NewLine;

           
            // adapt code in class as needed Now jump out foreach
           
        foreach (var key in fks)
        {
            querystring = querystring + ".Include(" + ent + " => " + ent + "." + key.Key + ")" + Environment.NewLine +"\t"+"\t";
        }

      //  string filename = fks.Count > 1 ? "Navigations" : entity;

        string basicsources = pathtobasicsources + "GetEntityRequest.txt";
        string thenamespace = stringnamespace + entitynameplural;
        string getentityrequest = string.Empty;
        string entitytolower = entity.ToLower();
        getentityrequest = File.ReadAllText(basicsources) 
       .Replace("<&StringNameSpace&>", thenamespace)
       .Replace("<&Entity&>", entity)
       .Replace("<&EntityToLower&>", entitytolower)
       .Replace("<&GetEntityRequestHandle&>", entityrequesthandle);
        File.WriteAllText(filesavelocation + "/"+ "Get" + entity + "Request.cs", getentityrequest);


        string parentname = fks.Count > 1 ? "Navigations" : parententity;
        
        string entitybyid = pathtobasicsources + "EntityByIdWithParentSpec.txt";
        string parent = File.ReadAllText(entitybyid)
            .Replace("<&StringNameSpace&>", thenamespace)
            .Replace("<&QueryString&>", querystring+";")
            .Replace("<&Entity&>", entity)
            .Replace("<&EntityPlural&>", entitynameplural)
            .Replace("<&Parent&>", parentname)
            .Replace("<&ParentToLower&>", parentname.ToLower());

            File.WriteAllText(filesavelocation+"/"+ entity + "ByIdWith" + parentname + "Spec.cs", parent);
        }

    }

