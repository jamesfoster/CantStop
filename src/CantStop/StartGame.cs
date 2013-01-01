namespace CantStop
{
	using Data;
	using Domain;
	using Dto;

	public class StartGame
	{
		public IRepository<Game> Games { get; set; }

		public StartGameResponse Execute(StartGameRequest request)
		{
			var game = Games.Get(request.Id);

			if (game == null)
				return null;

			game.Status = GameState.PreDiceRolled;
			game.CurrentPlayer = 1;

			Games.Update(game);
			
			return new StartGameResponse();
		}
	}
}