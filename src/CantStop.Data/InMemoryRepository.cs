namespace CantStop.Data
{
	using System.Collections.Generic;
	using System.Linq;
	using Domain;

	public class InMemoryRepository<T> : IRepository<T> 
		where T : class, IEntity
	{
		readonly IDictionary<long, T> _entities = new Dictionary<long, T>();
		long LastId = 1;

		public List<T> All()
		{
			return _entities.Values.ToList();
		}

		public void Add(params T[] entities)
		{
			foreach (var entity in entities)
			{
				entity.Id = LastId++;
				_entities[entity.Id] = entity;
			}
		}

		public T Get(long key)
		{
			T entity;

			if (_entities.TryGetValue(key, out entity))
				return entity;

			return null;
		}
	}
}