namespace CantStop.Tests
{
	using Data;
	using Domain;
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	public class When_creating_a_game : Given_a_game_repository
	{
		protected static CreateGame CreateGame { get; set; }
		protected static CreateGameRequest Request { get; set; }
		protected static CreateGameResponse Response { get; set; }
		protected static Game Result { get; set; }

		Establish that = () =>
			{
				CreateGame = new CreateGame{Games = GameRepositoryMock.Object};
				Request = new CreateGameRequest{NumberOfPlayers = 5};
			};

		Because of = () =>
			{
				Response = CreateGame.Execute(Request);
				Result = GameRepositoryMock.Object.Get(1);
			};

		It should_add_a_game_to_the_repository = () => GameRepositoryMock.Verify(r => r.Add(Moq.It.IsAny<Game>()), Times.Once());
		It the_repository_should_only_contain_one_game = () => GameRepository.All().Count.ShouldEqual(1);
		It should_create_a_game_with_the_correct_id = () => Result.Id.ShouldEqual(1);
		It should_have_the_correct_number_of_players = () => Result.Players.Count.ShouldEqual(Request.NumberOfPlayers);
		It should_have_the_status_of_Created = () => Result.Status.ShouldEqual(GameState.Created);
		It should_return_the_correct_id = () => Response.Id.ShouldEqual(1);
	}
}
