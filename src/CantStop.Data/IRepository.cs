namespace CantStop.Data
{
	using System.Collections.Generic;
	using Domain;

	public interface IRepository<T>
		where T : class, IEntity
	{
		List<T> All();
		T Get(long key);
		void Add(params T[] entities);
		void Update(params T[] entities);
	}
}