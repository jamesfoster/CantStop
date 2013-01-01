namespace CantStop.Tests
{
	using Data;
	using Domain;
	using Dto;
	using Machine.Specifications;

	public class When_creating_a_game
	{
		protected static IRepository<Game> GameRepository { get; set; }
		protected static CreateGame CreateGame { get; set; }
		protected static CreateGameRequest Request { get; set; }
		protected static CreateGameResponse Response { get; set; }
		protected static Game Result { get; set; }

		Establish that = () =>
			{
				GameRepository = new InMemoryRepository<Game>();
				CreateGame = new CreateGame{Games = GameRepository};
				Request = new CreateGameRequest{NumberOfPlayers = 5};
			};

		Because of = () =>
			{
				Response = CreateGame.Execute(Request);
				Result = GameRepository.Get(1);
			};

		It should_add_a_game_to_the_repository = () => GameRepository.All().Count.ShouldEqual(1);
		It should_create_a_game_with_the_correct_id = () => Result.Id.ShouldEqual(1);
		It should_have_the_correct_number_of_players = () => Result.Players.Count.ShouldEqual(Request.NumberOfPlayers);
		It should_return_the_correct_id = () => Response.Id.ShouldEqual(1);
	}
}
