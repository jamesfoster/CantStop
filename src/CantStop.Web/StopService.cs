namespace CantStop.Web
{
	using Dto;
	using ServiceStack.ServiceInterface;

	public class StopService : Service
	{
		public IUseCase<StopRequest, StopResponse> UseCase { get; set; }

		public StopResponse Post(StopRequest request)
		{
			return UseCase.Execute(request);
		}
	}
}