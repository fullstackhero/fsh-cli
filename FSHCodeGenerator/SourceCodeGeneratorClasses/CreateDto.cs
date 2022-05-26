﻿using System.Text.RegularExpressions;

namespace FSHCodeGenerator.SourceCodeGeneratorClasses;
internal class CreateDto
{
    public CreateDto(string filesavelocation, string propertylines, string detaillines, string parentLines, string guidLines, string dtoLines, string theusings, string pathtobasicsources, string stringnamespace, string entity, string entitynameplural, bool hasnavigations)
    {
        string basicsources = pathtobasicsources + "BasicDto.txt";
        string thenamespace = stringnamespace + entitynameplural;
        string dtoText = File.ReadAllText(basicsources);        
        
        dtoText = dtoText.Replace("<&theusings&>", string.Empty);
        dtoText = dtoText.Replace("<&StringNameSpace&>", thenamespace);
        dtoText = dtoText.Replace("<&Entity&>", entity + "Dto");
        dtoText = dtoText.Replace("<&PropertyLines&>", propertylines);
        dtoText = dtoText.Replace("<&GuidLines&>", guidLines);        
        dtoText = dtoText.Replace("<&ParentLines&>", parentLines);
       
        //dtoText = dtoText.Replace("^(?:[\t ]*(?:\r?\n|\r))+", string.Empty);
        //dtoText = Regex.Replace(dtoText, @"(^\p{Zs}*\r\n){2,}", "\r\n", RegexOptions.Multiline);
        File.WriteAllText(filesavelocation+"/"+ entitynameplural.Trim() + "/" + entity.Trim() + "Dto.cs", dtoText);

        if (hasnavigations)
        {
            string className = entity + "DetailsDto";
            string basicsources1 = pathtobasicsources + "DetailsDto.txt";

            string dtoDetailsText = File.ReadAllText(basicsources1);
            dtoDetailsText = dtoDetailsText.Replace("<&theusings&>", theusings);
            dtoDetailsText = dtoDetailsText.Replace("<&StringNameSpace&>", thenamespace);
            dtoDetailsText = dtoDetailsText.Replace("<&Entity&>", className);
            dtoDetailsText = dtoDetailsText.Replace("<&PropertyLines&>", propertylines);
           // dtoDetailsText = dtoDetailsText.Replace("<&virtualLines&>", string.Empty);
            dtoDetailsText = dtoText.Replace("<&DtoLines&>", dtoLines);
        //    dtoDetailsText = dtoDetailsText.Replace("<&DetailLines&>", detaillines);
          //  dtoDetailsText = Regex.Replace(dtoDetailsText, @"(^\p{Zs}*\r\n){2,}", "\r\n", RegexOptions.Multiline);
            File.WriteAllText(filesavelocation + "/" + entitynameplural.Trim() + "/" + entity.Trim() + "DetailsDto.cs", dtoDetailsText);
        }
    }
}
