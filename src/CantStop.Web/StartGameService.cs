namespace CantStop.Web
{
	using Dto;
	using ServiceStack.ServiceInterface;

	public class StartGameService : Service
	{
		public IUseCase<StartGameRequest, StartGameResponse> UseCase { get; set; }

		public StartGameResponse Post(StartGameRequest request)
		{
			return UseCase.Execute(request);
		}
	}
}