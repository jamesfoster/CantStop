namespace CantStop.Domain
{
	using System.Collections.Generic;

	public class Game : IEntity
	{
		public Game()
		{
			Players = new List<Player>();
			Dice = new int[4];
		}

		public long Id { get; set; }

		public GameState Status { get; set; }
		public List<Player> Players { get; set; }
		public int CurrentPlayer { get; set; }
		public int[] Dice { get; set; }
	}
}