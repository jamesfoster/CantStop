namespace CantStop.Tests
{
	using Data;
	using Domain;
	using Machine.Specifications;
	using Mocks;
	using Moq;

	public class Given_a_game_repository
	{
		protected static Mock<IRepository<Game>> GameRepositoryMock { get; set; }
		protected static IRepository<Game> GameRepository { get { return GameRepositoryMock.Object; } }

		Establish that = () =>
			{
				GameRepositoryMock = MockDataRepository.Get<Game>();
			};
	}
}