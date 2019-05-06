namespace iBlogs.Site.Application.Utils.CodeDi
{
    public interface ICodeDiServiceProvider
    {
        T GetService<T>() where T : class;
        T GetService<T>(string name) where T : class;
    }
}
