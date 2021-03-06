namespace CantStop
{
	using Data;
	using Domain;
	using Dto;

	public class CreateGame : IUseCase
	{
		public IRepository<Game> Games { get; set; }

		public CreateGameResponse Execute(CreateGameRequest request)
		{
			var game = new Game();

			for (var i = 0; i < request.NumberOfPlayers; i++)
			{
				game.Players.Add(new Player());
			}

			game.Status = GameState.Created;

			Games.Add(game);

			return new CreateGameResponse {Id = game.Id};
		}
	}
}