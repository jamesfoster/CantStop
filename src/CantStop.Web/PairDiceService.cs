namespace CantStop.Web
{
	using Dto;
	using ServiceStack.ServiceInterface;

	public class PairDiceService : Service
	{
		public IUseCase<PairDiceRequest, PairDiceResponse> UseCase { get; set; }

		public PairDiceResponse Post(PairDiceRequest request)
		{
			return UseCase.Execute(request);
		}
	}
}