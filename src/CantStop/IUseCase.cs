namespace CantStop
{
	public interface IUseCase<in TRequest, out TResponse> : IUseCase
	{
		TResponse Execute(TRequest request);
	}

	public interface IUseCase
	{
	}
}