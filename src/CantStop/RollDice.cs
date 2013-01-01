namespace CantStop
{
	using System;
	using Data;
	using Domain;
	using Dto;

	public class RollDice
	{
		public IRepository<Game> Games { get; set; }
		readonly Random Random = new Random();

		public RollDiceResponse Execute(RollDiceRequest request)
		{
			var game = Games.Get(request.Id);

			if (game == null || game.Status != GameState.PreDiceRolled)
				return null;

			for (int i = 0; i < game.Dice.Length; i++)
			{
				game.Dice[i] = Random.Next(6) + 1;
			}

			game.Status = GameState.DiceRolled;

			Games.Update(game);

			return new RollDiceResponse();
		}
	}
}