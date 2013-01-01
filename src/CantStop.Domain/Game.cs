namespace CantStop.Domain
{
	using System.Collections.Generic;

	public class Game : IEntity
	{
		public Game()
		{
			Players = new List<Player>();
		}

		public long Id { get; set; }

		public List<Player> Players { get; set; }

		public GameState Status { get; set; }

		public int CurrentPlayer { get; set; }
	}
}