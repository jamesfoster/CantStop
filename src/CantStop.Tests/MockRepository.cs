namespace CantStop.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using Data;
	using Domain;
	using Moq;

	public class MockRepository
	{
		public static Mock<IRepository<T>> Get<T>() where T : class, IEntity
		{
			var mock = new Mock<IRepository<T>>();

			var entities = new Dictionary<long, T>();
			var lastId = 1;

			mock
				.Setup(r => r.Add(It.IsAny<T[]>()))
				.Callback<T[]>(x =>
					{
						foreach (var entity in x)
						{
							entity.Id = lastId++;
							entities.Add(entity.Id, entity);
						}
					});

			mock
				.Setup(r => r.All())
				.Returns(() => entities.Values.ToList());

			mock
				.Setup(r => r.Get(It.IsAny<long>()))
				.Returns<long>(key =>
					{
						T entity;

						if (entities.TryGetValue(key, out entity))
							return entity;

						return null;
					});

			mock
				.Setup(r => r.Update(It.IsAny<T[]>()))
				.Callback<T[]>(x =>
					{
						foreach (var entity in x)
						{
							if (entities.ContainsKey(entity.Id))
								entities[entity.Id] = entity;
						}
					});

			return mock;
		}
	}
}