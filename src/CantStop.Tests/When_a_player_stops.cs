namespace CantStop.Tests
{
	using System.Collections.Generic;
	using Domain;
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	public class When_a_player_stops : Given_a_game_repository
	{
		protected static Stop Stop { get; set; }
		protected static StopRequest Request { get; set; }
		protected static StopResponse Response { get; set; }
		protected static Game Game { get; set; }

		Establish that = () =>
			{
				Stop = new Stop {Games = GameRepository};
				Request = new StopRequest {Id = 1};

				Game = new Game
					{
						Status = GameState.PreDiceRolled,
						CurrentPlayer = 1,
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

		It should_update_the_game_in_the_respository = () => GameRepositoryMock.Verify(r => r.Update(Game), Times.Once());
		It should_change_the_game_state_to_PreDiceRolled = () => Game.Status.ShouldEqual(GameState.PreDiceRolled);
		It should_be_player_2s_turn = () => Game.CurrentPlayer.ShouldEqual(2);
		It should_reset_the_dice = () => Game.Dice.ShouldContainOnly(0, 0, 0, 0);
		It should_reset_the_climbers = () => Game.Climbers.Count.ShouldEqual(0);
		It should_update_player_1s_position = () => Game.Players[0].Position.ShouldContainOnly(0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0);
		It should_return_the_players_final_position = () => Response.FinalPosition.ShouldContainOnly(Game.Players[0].Position);
		It should_return_the_next_player_number = () => Response.NextPlayer.ShouldEqual(2);

	}
}