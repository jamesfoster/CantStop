namespace CantStop.Tests
{
	using System.Collections.Generic;
	using Domain;
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	public class When_a_player_stops_if_dice_rolled : Given_a_game_repository
	{
		protected static Stop Stop { get; set; }
		protected static StopRequest Request { get; set; }
		protected static StopResponse Response { get; set; }
		protected static Game Game { get; set; }

		Establish that = () =>
			{
				Stop = new Stop {Games = GameRepository};
				var dice = new[] {3, 4, 4, 5};
				Request = new StopRequest {Id = 1};

				Game = new Game
					{
						Status = GameState.DiceRolled,
						CurrentPlayer = 1,
						Dice = dice,
						Climbers = new Dictionary<int, int> {{3, 1}, {4, 1}, {5, 1}}
					};
				Game.Players.Add(new Player());
				Game.Players.Add(new Player());
				GameRepository.Add(Game);
			};

		Because of = () =>
			{
				Response = Stop.Execute(Request);
			};

		It should_update_the_game_in_the_respository = () => GameRepositoryMock.Verify(r => r.Update(Game), Times.Never());
		It should_change_the_game_state_to_PreDiceRolled = () => Game.Status.ShouldEqual(GameState.DiceRolled);
		It should_still_be_player_1s_turn = () => Game.CurrentPlayer.ShouldEqual(1);
		It should_not_reset_the_dice = () => Game.Dice.ShouldContainOnly(3, 4, 4, 5);
		It should_not_reset_the_climbers = () => Game.Climbers.ShouldEqual(new Dictionary<int, int> {{3, 1}, {4, 1}, {5, 1}});

	}
}