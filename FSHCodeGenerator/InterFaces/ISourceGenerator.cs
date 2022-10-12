namespace FSHCodeGenerator.InterFaces
{
    public interface ISourceGenerator
    {
        Task<bool> Run();
    }
}