namespace AunctionApp.BLL.Interfaces
{
	public interface IServiceFactory
	{
		T GetService<T>() where T : class;
	}
}
