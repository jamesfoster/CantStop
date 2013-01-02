namespace CantStop.Tests
{
	using System.Collections.Generic;
	using Data;
	using Domain;
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	public class When_a_player_rolls_the_dice_has_3_climbers_and_cant_make_a_match : Given_a_game_repository
	{
		protected static Mock<IRandomNumberGenerator> RandomNumberGeneratorMock { get; set; }
		protected static RollDice RollDice { get; set; }
		protected static RollDiceRequest Request { get; set; }
		protected static RollDiceResponse Response { get; set; }
		protected static Game Game { get; set; }

		Establish that = () =>
			{
				RandomNumberGeneratorMock = MockRandomNumberGenerator.Get(0, 4, 4, 5);
				RollDice = new RollDice
					{
						Games = GameRepository,
						Random = RandomNumberGeneratorMock.Object
					};
				Request = new RollDiceRequest {Id = 1};

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
				Response = RollDice.Execute(Request);
			};

		It should_update_the_game_in_the_respository = () => GameRepositoryMock.Verify(r => r.Update(Game), Times.Once());
		It should_change_the_game_state_to_PreDiceRolled = () => Game.Status.ShouldEqual(GameState.PreDiceRolled);
		It should_be_player_2s_turn = () => Game.CurrentPlayer.ShouldEqual(2);
		It should_roll_the_dice = () => Game.Dice.ShouldContainOnly(0, 0, 0, 0);
		It should_return_the_dice_results = () => Response.Dice.ShouldContainOnly(1, 5, 5, 6);
		It should_not_change_the_players_position = () => Game.Players[0].Position.ShouldContainOnly(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

	}
}