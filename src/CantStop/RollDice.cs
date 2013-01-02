namespace CantStop
{
	using Data;
	using Domain;
	using Dto;
	using System.Linq;

	public class RollDice
	{
		public IRepository<Game> Games { get; set; }
		public IRandomNumberGenerator Random { get; set; }

		public RollDiceResponse Execute(RollDiceRequest request)
		{
			var game = Games.Get(request.Id);

			if (game == null || game.Status != GameState.PreDiceRolled)
				return null;

			var dice = new int[4];
			for (int i = 0; i < game.Dice.Length; i++)
			{
				dice[i] = Random.Next(6) + 1;
			}

			if (CheckPossibilities(game, dice))
			{
				game.Dice = dice;
				game.Status = GameState.DiceRolled;
			}
			else
			{
				game.Climbers.Clear();
				game.NextPlayer();
				game.ResetDice();
			}

			Games.Update(game);

			return new RollDiceResponse {Dice = dice};
		}

		bool CheckPossibilities(Game game, int[] dice)
		{
			if (game.Climbers.Count < 3)
				return true;

			var possibilities = DiceHelper.GetPosibilities(dice);

			return game.Climbers.Keys.Any(possibilities.Contains);
		}
	}
}