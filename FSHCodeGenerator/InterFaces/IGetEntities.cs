
namespace FSHCodeGenerator.InterFaces
{
    public interface IGetEntities
    {
        Task<Dictionary<string, string>> Run();
    }
}