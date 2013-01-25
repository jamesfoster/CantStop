namespace CantStop.Web
{
	using Configure;
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
			new Routes().Configure(Routes);
		}
	}
}