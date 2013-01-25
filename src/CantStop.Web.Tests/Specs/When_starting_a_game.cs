namespace CantStop.Web.Tests.Specs
{
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	[Subject(typeof (StartGameService))]
	public class When_starting_a_game
	{
		protected static Mock<IUseCase<StartGameRequest, StartGameResponse>> UseCase { get; set; }
		protected static StartGameService Service { get; set; }
		protected static StartGameRequest Request { get; set; }
		protected static StartGameResponse Response { get; set; }
		protected static StartGameResponse Expected { get; set; }

		Establish context = () =>
			{
				UseCase = new Mock<IUseCase<StartGameRequest, StartGameResponse>>();

				Service = new StartGameService
					{
						UseCase = UseCase.Object
					};

				Request = new StartGameRequest
					{
						Id = 1
					};

				Expected = new StartGameResponse();

				UseCase
					.Setup(c => c.Execute(Moq.It.IsAny<StartGameRequest>()))
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