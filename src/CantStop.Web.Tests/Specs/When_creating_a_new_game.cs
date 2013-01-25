namespace CantStop.Web.Tests.Specs
{
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	[Subject(typeof (CreateGameService))]
	public class When_creating_a_new_game
	{
		protected static Mock<IUseCase<CreateGameRequest, CreateGameResponse>> UseCase { get; set; }
		protected static CreateGameService Service { get; set; }
		protected static CreateGameRequest Request { get; set; }
		protected static CreateGameResponse Response { get; set; }
		protected static CreateGameResponse Expected { get; set; }

		Establish context = () =>
			{
				UseCase = new Mock<IUseCase<CreateGameRequest, CreateGameResponse>>();

				Service = new CreateGameService
					{
						UseCase = UseCase.Object
					};

				Request = new CreateGameRequest
					{
						NumberOfPlayers = 3
					};

				Expected = new CreateGameResponse {Id = 123};

				UseCase
					.Setup(c => c.Execute(Moq.It.IsAny<CreateGameRequest>()))
					.Returns(Expected);
			};

		Because of = () =>
			{
				Response = Service.Post(Request);
			};

		It should_execute_the_use_case = () => UseCase.Verify(c => c.Execute(Request));
		It should_return_the_expected_result = () => Response.ShouldBeTheSameAs(Expected);
	}
}