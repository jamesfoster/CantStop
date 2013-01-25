namespace CantStop.Web.Tests.Specs
{
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	[Subject(typeof (StopService))]
	public class When_stopping_on_your_turn
	{
		protected static Mock<IUseCase<StopRequest, StopResponse>> UseCase { get; set; }
		protected static StopService Service { get; set; }
		protected static StopRequest Request { get; set; }
		protected static StopResponse Response { get; set; }
		protected static StopResponse Expected { get; set; }

		Establish context = () =>
			{
				UseCase = new Mock<IUseCase<StopRequest, StopResponse>>();

				Service = new StopService
					{
						UseCase = UseCase.Object
					};

				Request = new StopRequest
					{
						Id = 1
					};

				Expected = new StopResponse
					{
						FinalPosition = new[] {0, 0, 0, 0, 1, 2, 3, 0, 0, 0, 0},
						NextPlayer = 2
					};

				UseCase
					.Setup(c => c.Execute(Moq.It.IsAny<StopRequest>()))
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