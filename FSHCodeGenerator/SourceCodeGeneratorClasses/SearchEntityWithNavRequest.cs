using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSHCodeGenerator.SourceCodeGeneratorClasses;
internal class SearchEntityWithNavRequest
{
    public SearchEntityWithNavRequest(string filesavelocation, string pathtobasicsources, string stringnamespace, string entity, string entitynameplural,Dictionary<string, string> fks,Dictionary<string,string> theentities)
    {
        string basicsources = pathtobasicsources + "SearchEntityWithNavRequest.txt";
        string thenamespace = stringnamespace + entitynameplural;
        string searchentityrequest = string.Empty;
        string entitytolower = entity.ToLower();
        string parentguids = string.Empty;
        string parententity = string.Empty;
        string parententityplural = string.Empty;
        foreach (var key in fks)
        {
            parententity = key.Key;
            bool entityFound = theentities.TryGetValue(parententity, out parententityplural);
            //parententityplural = key.Value;

            parentguids = parentguids + "public Guid? " + parententity + "Id { get; set; }" + Environment.NewLine + "\t";                 
            searchentityrequest = File.ReadAllText(basicsources)
           .Replace("<&StringNameSpace&>", thenamespace)
           .Replace("<&ParentGuids&>", parentguids)
           .Replace("<&EntityPlural&>", entitynameplural)
           .Replace("<&Entity&>", entity)
           .Replace("<&EntityToLower&>", entitytolower)
           .Replace("<&ParentEntityPlural&>", parententityplural);
            File.WriteAllText(filesavelocation + "/" + "Search" + entitynameplural + "Request.cs", searchentityrequest);
        }
    }
}


//namespace FSHCodeGenerator.SourceCodeGeneratorClasses;
//internal class SearchParentEntityRequest
//{
//    public SearchParentEntityRequest(string filesavelocation, string pathtobasicsources, string stringnamespace, string entity, string entitynameplural, Dictionary<string, string> pks)
//    {
//        string thenamespace = stringnamespace + entitynameplural;
//        string searchparententityrequest = string.Empty;
//        string entitytolower = entity.ToLower();
//        string parentguids = string.Empty;
//        string parententity = string.Empty;
//        string parententityplural = string.Empty;
//        foreach (var key in pks)
//        {
//            parententity = key.Key;
//            parententityplural = key.Value;

//            parentguids = parentguids + "public Guid? " + parententity + "Id { get; set; }" + Environment.NewLine + "\t";
//        }

//        string entitybyid = pathtobasicsources + "SearchParentEntityRequest.txt";
//        string parentspec = File.ReadAllText(entitybyid)
//       .Replace("<&StringNameSpace&>", thenamespace)
//       .Replace("<&EntityPlural&>", entitynameplural)
//       .Replace("<&Entity&>", entity)
//       .Replace("<&EntityToLower&>", entitytolower)
//       .Replace("<&ParentGuids&>", parentguids)
//       .Replace("<&ParentEntity&>", parententity)
//       .Replace("<&ParentEntityPlural&>", parententityplural);

//        File.WriteAllText(filesavelocation + "/" + "Search" + entitynameplural + "Request.cs", parentspec);

//    }
//}