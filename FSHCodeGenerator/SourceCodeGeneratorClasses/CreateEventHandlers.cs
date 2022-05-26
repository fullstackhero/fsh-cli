namespace FSHCodeGenerator.SourceCodeGeneratorClasses;
internal class CreateEventHandlers
{
    public CreateEventHandlers(string filesavelocation, string pathtobasicsources, string stringnamespace, string entity, string entitynameplural)
    {
        try
        {
            string? basicsources = pathtobasicsources + "EntityEventHandler.txt";
            string thenamespace = stringnamespace + entitynameplural;
            string eventHandlerCreate = File.ReadAllText(basicsources)
            .Replace("<&StringNameSpace&>", thenamespace)
            .Replace("<&Entity&>", entity)
            .Replace("<&Action&>", "Created");
            File.WriteAllText(filesavelocation + "/" + entity.Trim() + "CreatedEventHandler.cs", eventHandlerCreate);

            string eventHandlerDelete = File.ReadAllText(basicsources)
            .Replace("<&StringNameSpace&>", thenamespace)
            .Replace("<&Entity&>", entity)
            .Replace("<&Action&>", "Deleted");
            File.WriteAllText(filesavelocation + "/" + entity.Trim() + "DeletedEventHandler.cs", eventHandlerDelete);

            string eventHandlerUpdate = File.ReadAllText(basicsources)
          .Replace("<&StringNameSpace&>", thenamespace)
          .Replace("<&Entity&>", entity)
          .Replace("<&Action&>", "Updated");
            File.WriteAllText(filesavelocation + "/" + entity.Trim() + "UpdateEventHandler.cs", eventHandlerUpdate);
        }
        catch (InvalidOperationException ex)
        {
            Console.Write("Invalid operation." + ex.Message);
        }
        catch (Exception ex)
        {
            Console.Write("Error info: " + ex.Message);
        }
    }
}
