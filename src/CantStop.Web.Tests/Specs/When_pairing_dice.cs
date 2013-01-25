namespace CantStop.Web.Tests.Specs
{
	using Dto;
	using Machine.Specifications;
	using Moq;
	using It = Machine.Specifications.It;

	[Subject(typeof (PairDiceService))]
	public class When_pairing_dice
	{
		protected static Mock<IUseCase<PairDiceRequest, PairDiceResponse>> UseCase { get; set; }
		protected static PairDiceService Service { get; set; }
		protected static PairDiceRequest Request { get; set; }
		protected static PairDiceResponse Response { get; set; }
		protected static PairDiceResponse Expected { get; set; }

		Establish context = () =>
			{
				UseCase = new Mock<IUseCase<PairDiceRequest, PairDiceResponse>>();

				Service = new PairDiceService
					{
						UseCase = UseCase.Object
					};

				Request = new PairDiceRequest
					{
						Id = 1,
						FirstChoice = 6
					};

				Expected = new PairDiceResponse();

				UseCase
					.Setup(c => c.Execute(Moq.It.IsAny<PairDiceRequest>()))
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