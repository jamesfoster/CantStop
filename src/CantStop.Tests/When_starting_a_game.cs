namespace CantStop.Tests
{
	using Data;
	using Domain;
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	public class When_starting_a_game : Given_a_game_repository
	{
		protected static StartGame StartGame { get; set; }
		protected static StartGameRequest Request { get; set; }
		protected static StartGameResponse Response { get; set; }
		protected static Game Game { get; set; }

		Establish that = () =>
			{
				StartGame = new StartGame {Games = GameRepository};
				Request = new StartGameRequest {Id = 1};

				Game = new Game();
				Game.Players.Add(new Player());
				Game.Players.Add(new Player());
				GameRepository.Add(Game);
			};

		Because of = () =>
			{
				Response = StartGame.Execute(Request);
			};

		It should_update_the_game_in_the_respository = () => GameRepositoryMock.Verify(r => r.Update(Game), Times.Once());
		It should_change_the_game_state_to_PreDiceRolled = () => Game.Status.ShouldEqual(GameState.PreDiceRolled);
		It should_be_player_1s_turn = () => Game.CurrentPlayer.ShouldEqual(1);

	}
}