namespace CantStop.Tests
{
	using Domain;
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	public class When_a_player_rolls_the_dice : Given_a_game_repository
	{
		protected static RollDice RollDice { get; set; }
		protected static RollDiceRequest Request { get; set; }
		protected static RollDiceResponse Response { get; set; }
		protected static Game Game { get; set; }

		Establish that = () =>
			{
				RollDice = new RollDice {Games = GameRepository};
				Request = new RollDiceRequest {Id = 1};

				Game = new Game
					{
						Status = GameState.PreDiceRolled,
						CurrentPlayer = 1
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
		It should_change_the_game_state_to_DiceRolled = () => Game.Status.ShouldEqual(GameState.DiceRolled);
		It should_still_be_player_1s_turn = () => Game.CurrentPlayer.ShouldEqual(1);
		It should_roll_the_dice = () => Game.Dice.ShouldEachConformTo(d => 1 <= d && d <= 6);
		It should_return_the_dice_results = () => Response.Dice.ShouldContainOnly(Game.Dice);

	}
}