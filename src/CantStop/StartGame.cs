namespace CantStop
{
	using Data;
	using Domain;
	using Dto;

	public class StartGame : IUseCase
	{
		public IRepository<Game> Games { get; set; }

		public StartGameResponse Execute(StartGameRequest request)
		{
			var game = Games.Get(request.Id);

			if (game == null || game.Status != GameState.Created)
				return null;

			game.Status = GameState.PreDiceRolled;
			game.CurrentPlayer = 1;

			Games.Update(game);
			
			return new StartGameResponse();
		}
	}
}