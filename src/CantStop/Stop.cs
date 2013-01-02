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

			var player = game.Players[game.CurrentPlayer - 1];

			foreach (var climber in game.Climbers)
			{
				player.Position[climber.Key - 2] = climber.Value;
			}

			game.NextPlayer();
			game.ResetDice();
			game.Climbers.Clear();

			Games.Update(game);

			return new StopResponse
				{
					FinalPosition = player.Position,
					NextPlayer = game.CurrentPlayer
				};
		}
	}
}