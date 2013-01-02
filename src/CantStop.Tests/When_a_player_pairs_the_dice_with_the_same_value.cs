namespace CantStop.Tests
{
	using System.Collections.Generic;
	using Domain;
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	public class When_a_player_pairs_the_dice_with_the_same_value : Given_a_game_repository
	{
		protected static PairDice PairDice { get; set; }
		protected static PairDiceRequest Request { get; set; }
		protected static PairDiceResponse Response { get; set; }
		protected static Game Game { get; set; }

		Establish that = () =>
			{
				PairDice = new PairDice {Games = GameRepository};
				var dice = new[] {3, 4, 4, 5};
				Request = new PairDiceRequest {Id = 1, FirstChoice = 8};

				Game = new Game
					{
						Status = GameState.DiceRolled,
						CurrentPlayer = 1,
						Dice = dice,
						Climbers = new Dictionary<int, int>()
					};
				Game.Players.Add(new Player());
				Game.Players.Add(new Player());
				GameRepository.Add(Game);
			};

		Because of = () =>
			{
				Response = PairDice.Execute(Request);
			};

		It should_update_the_game_in_the_respository = () => GameRepositoryMock.Verify(r => r.Update(Game), Times.Once());
		It should_change_the_game_state_to_PreDiceRolled = () => Game.Status.ShouldEqual(GameState.PreDiceRolled);
		It should_still_be_player_1s_turn = () => Game.CurrentPlayer.ShouldEqual(1);
		It should_reset_the_dice = () => Game.Dice.ShouldContainOnly(0, 0, 0, 0);
		It should_increment_climber_8_twice = () => Game.Climbers[8].ShouldEqual(2);

		It should_have_1_climbers = () => Game.Climbers.Count.ShouldEqual(1);
	}
}