namespace CantStop.Tests
{
	using System.Collections.Generic;
	using Domain;
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	public class When_a_player_pairs_the_dice_with_3_climbers_and_no_matches : Given_a_game_repository
	{
		protected static PairDice PairDice { get; set; }
		protected static PairDiceRequest Request { get; set; }
		protected static PairDiceResponse Response { get; set; }
		protected static Game Game { get; set; }

		Establish that = () =>
			{
				PairDice = new PairDice {Games = GameRepository};
				var dice = new[] {3, 1, 5, 5};
				Request = new PairDiceRequest {Id = 1, FirstChoice = 6};

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
				Response = PairDice.Execute(Request);
			};

		It should_not_claa_update = () => GameRepositoryMock.Verify(r => r.Update(Game), Times.Never());
		It should_not_change_the_game_state = () => Game.Status.ShouldEqual(GameState.DiceRolled);
		It should_still_be_player_1s_turn = () => Game.CurrentPlayer.ShouldEqual(1);
		It should_not_reset_the_dice = () => Game.Dice.ShouldContainOnly(3, 1, 5, 5);
		It should_not_create_climber_9 = () => Game.Climbers.ShouldNotContain(p => p.Key == 9);
		It should_not_create_climber_7 = () => Game.Climbers.ShouldNotContain(p => p.Key == 7);

		It should_not_increment_climber_3 = () => Game.Climbers[3].ShouldEqual(1);
		It should_not_increment_climber_4 = () => Game.Climbers[4].ShouldEqual(1);
		It should_not_increment_climber_5 = () => Game.Climbers[5].ShouldEqual(1);
		It should_have_3_climbers = () => Game.Climbers.Count.ShouldEqual(3);
	}
}