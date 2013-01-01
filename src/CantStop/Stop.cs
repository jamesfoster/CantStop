namespace CantStop
{
	using Data;
	using Domain;
	using Dto;

	public class Stop
	{
		public IRepository<Game> Games { get; set; }

		public StopResponse Execute(StopRequest request)
		{
			var game = Games.Get(request.Id);

			if (game == null || game.Status != GameState.PreDiceRolled)
				return null;

			game.NextPlayer();
			game.ResetDice();
			game.Climbers.Clear();

			Games.Update(game);

			return new StopResponse();
		}
	}
}