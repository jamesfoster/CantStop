namespace CantStop.Web
{
	using Dto;
	using ServiceStack.ServiceInterface;

	public class CreateGameService : Service
	{
		public IUseCase<CreateGameRequest,CreateGameResponse> UseCase { get; set; }

		public CreateGameResponse Post(CreateGameRequest request)
		{
			return UseCase.Execute(request);
		}
	}
}