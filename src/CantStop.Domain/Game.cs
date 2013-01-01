namespace CantStop.Domain
{
	using System.Collections.Generic;

	public class Game : IEntity
	{
		public Game()
		{
			Players = new List<Player>();
			Dice = new int[4];
			Climbers = new Dictionary<int, int>();
		}

		public long Id { get; set; }

		public GameState Status { get; set; }
		public List<Player> Players { get; set; }
		public int CurrentPlayer { get; set; }
		public int[] Dice { get; set; }
		public Dictionary<int, int> Climbers { get; set; }

		public void NextPlayer()
		{
			CurrentPlayer = CurrentPlayer == Players.Count ? 1 : CurrentPlayer + 1;
		}

		public void ResetDice()
		{
			Dice = new int[4];
		}
	}
}