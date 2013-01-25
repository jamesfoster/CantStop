namespace CantStop.Web
{
	using Dto;
	using ServiceStack.ServiceInterface;

	public class RollDiceService : Service
	{
		public IUseCase<RollDiceRequest, RollDiceResponse> UseCase { get; set; }

		public RollDiceResponse Post(RollDiceRequest request)
		{
			return UseCase.Execute(request);
		}
	}
}