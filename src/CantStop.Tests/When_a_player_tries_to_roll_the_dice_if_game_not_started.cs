namespace CantStop.Tests
{
	using Domain;
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	public class When_a_player_tries_to_roll_the_dice_if_game_not_started : Given_a_game_repository
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
						Status = GameState.Created
					};
				Game.Players.Add(new Player());
				Game.Players.Add(new Player());
				GameRepository.Add(Game);
			};

		Because of = () =>
			{
				Response = RollDice.Execute(Request);
			};

		It should_update_the_game_in_the_respository = () => GameRepositoryMock.Verify(r => r.Update(Game), Times.Never());
		It should_not_change_the_game_state = () => Game.Status.ShouldEqual(GameState.Created);
		It should_not_change_the_dice = () => Game.Dice.ShouldContainOnly(0, 0, 0, 0);

	}
}