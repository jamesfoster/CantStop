namespace CantStop.Data
{
	using System.Collections.Generic;
	using Domain;

	public interface IRepository<T>
		where T : class, IEntity
	{
		List<T> All();
		void Add(params T[] entities);
		T Get(long key);
	}
}