namespace CantStop
{
	using System.Linq;
	using Data;
	using Domain;
	using Dto;

	public class PairDice : IUseCase
	{
		public IRepository<Game> Games { get; set; }

		public PairDiceResponse Execute(PairDiceRequest request)
		{
			var game = Games.Get(request.Id);

			if (!CheckDecision(game, request.FirstChoice))
				return null;

			game.Dice = new int[4];
			game.Status = GameState.PreDiceRolled;

			Games.Update(game);

			return new PairDiceResponse();
		}

		static bool CheckDecision(Game game, int number1)
		{
			var posibilities = DiceHelper.GetPosibilities(game.Dice);

			if (!posibilities.Contains(number1))
				return false;

			var number2 = game.Dice.Sum() - number1;

			var player = game.Players[game.CurrentPlayer - 1];

			var added = AddClimber(game, player, number1);
			added |= AddClimber(game, player, number2);

			return added;
		}

		static bool AddClimber(Game game, Player player, int num)
		{
			if (game.Climbers.Count < 3 && !game.Climbers.ContainsKey(num))
				game.Climbers[num] = player.Position[num - 2];

			if (game.Climbers.ContainsKey(num))
			{
				game.Climbers[num]++;
				return true;
			}

			return false;
		}
	}
}