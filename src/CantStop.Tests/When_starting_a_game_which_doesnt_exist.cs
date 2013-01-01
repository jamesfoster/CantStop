namespace CantStop.Tests
{
	using Domain;
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	public class When_starting_a_game_which_doesnt_exist : Given_a_game_repository
	{
		protected static StartGame StartGame { get; set; }
		protected static StartGameRequest Request { get; set; }
		protected static StartGameResponse Response { get; set; }
		protected static Game Game { get; set; }

		Establish that = () =>
			{
				StartGame = new StartGame {Games = GameRepository};
				Request = new StartGameRequest {Id = 2};

				Game = new Game();
				Game.Players.Add(new Player());
				Game.Players.Add(new Player());
				GameRepository.Add(Game);
			};

		Because of = () =>
			{
				Response = StartGame.Execute(Request);
			};

		It should_not_call_update = () => GameRepositoryMock.Verify(r => r.Update(Game), Times.Never());
		It should_return_null = () => Response.ShouldBeNull();

	}
}