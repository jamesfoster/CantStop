namespace CantStop.Web.Configure
{
	using Dto;
	using ServiceStack.ServiceHost;

	public class Routes
	{
		public void Configure(IServiceRoutes routes)
		{
			routes.Add<CreateGameRequest>("game", "POST");
			routes.Add<StartGameRequest>("game/{Id}/start-game", "POST");
			routes.Add<RollDiceRequest>("game/{Id}/roll-dice", "POST");
			routes.Add<PairDiceRequest>("game/{Id}/pair-dice", "POST");
			routes.Add<StopRequest>("game/{Id}/stop", "POST");
		}
	}
}