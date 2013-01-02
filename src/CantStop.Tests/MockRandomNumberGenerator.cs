namespace CantStop.Tests
{
	using System;
	using System.Collections.Generic;
	using Data;
	using Moq;

	public class MockRandomNumberGenerator
	{
		public static Mock<IRandomNumberGenerator> Get(params int[] numbers)
		{
			var mock = new Mock<IRandomNumberGenerator>();
			var queue = new Queue<int>(numbers);
			var random = new Random();

			mock
				.Setup(r => r.Next(It.IsAny<int>()))
				.Returns<int>(max =>
					{
						if (queue.Count == 0)
							return random.Next(max);

						return queue.Dequeue();
					});

			return mock;
		}
	}
}