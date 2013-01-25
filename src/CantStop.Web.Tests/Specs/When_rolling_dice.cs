namespace CantStop.Web.Tests.Specs
{
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	[Subject(typeof (RollDiceService))]
	public class When_rolling_dice
	{
		protected static Mock<IUseCase<RollDiceRequest, RollDiceResponse>> UseCase { get; set; }
		protected static RollDiceService Service { get; set; }
		protected static RollDiceRequest Request { get; set; }
		protected static RollDiceResponse Response { get; set; }
		protected static RollDiceResponse Expected { get; set; }

		Establish context = () =>
			{
				UseCase = new Mock<IUseCase<RollDiceRequest, RollDiceResponse>>();

				Service = new RollDiceService
					{
						UseCase = UseCase.Object
					};

				Request = new RollDiceRequest
					{
						Id = 1
					};

				Expected = new RollDiceResponse
					{
						Dice = new[] {1, 2, 3, 4}
					};

				UseCase
					.Setup(c => c.Execute(Moq.It.IsAny<RollDiceRequest>()))
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