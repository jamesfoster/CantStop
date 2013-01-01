namespace CantStop
{
	using System.Linq;
	using Data;
	using Domain;
	using Dto;

	public class PairDice
	{
		public IRepository<Game> Games { get; set; }

		public PairDiceResponse Execute(PairDiceRequest request)
		{
			var game = Games.Get(request.Id);

			if (CheckDecision(game, request.FirstChoice))
			{
				game.Dice = new int[4];
				game.Status = GameState.PreDiceRolled;

				Games.Update(game);
			}

			return new PairDiceResponse();
		}

		bool CheckDecision(Game game, int number1)
		{
			var posibilities = GetPosibilities(game.Dice);

			if (!posibilities.Contains(number1))
				return false;

			var number2 = game.Dice.Sum() - number1;

			var player = game.Players[game.CurrentPlayer - 1];

			var added = AddClimber(game, player, number1);
			added |= AddClimber(game, player, number2);

			return added;
		}

		bool AddClimber(Game game, Player player, int num)
		{
			if (game.Climbers.Count < 3 && !game.Climbers.ContainsKey(num))
				game.Climbers[num] = 0;

			if (game.Climbers.ContainsKey(num))
			{
				game.Climbers[num]++;
				return true;
			}

			return false;
		}

		int[] GetPosibilities(int[] dice)
		{
			var result = new int[6];
			var index = 0;

			for (int i = 0; i < 3; i++)
			{
				for (int j = i + 1; j < 4; j++)
				{
					result[index++] = dice[i] + dice[j];
				}
			}

			return result;
		}
	}
}