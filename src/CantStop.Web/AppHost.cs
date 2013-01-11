namespace CantStop.Web
{
	using Funq;
	using ServiceStack.WebHost.Endpoints;

	public class AppHost : AppHostBase
	{
		public AppHost()
			: base("Cant Stop", typeof (AppHost).Assembly)
		{
		}

		public override void Configure(Container container)
		{

		}
	}
}