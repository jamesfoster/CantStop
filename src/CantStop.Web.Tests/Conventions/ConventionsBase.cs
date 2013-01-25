namespace CantStop.Web.Tests.Conventions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using NUnit.Framework;
	using ServiceStack.ServiceHost;

	public class ConventionsBase
	{
		protected List<Type> AllTypes { get; set; }
		protected List<Type> ServiceTypes { get; set; }
		protected List<Type> RequestTypes { get; set; }
		protected List<Type> ResponseTypes { get; set; }
		protected List<Type> UseCaseTypes { get; set; }

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			// ensure assemblies are loaded

			Assembly.Load("CantStop");
			Assembly.Load("CantStop.Web");
			Assembly.Load("CantStop.Dto");

			AllTypes = TypeHelpers.GetAllTypes("CantStop").ToList();

			ServiceTypes = AllTypes.Where(t => t.Namespace == "CantStop.Web" && t.Implements<IService>()).ToList();
			RequestTypes = AllTypes.Where(t => t.Namespace == "CantStop.Dto" && t.Name.EndsWith("Request")).ToList();
			ResponseTypes = AllTypes.Where(t => t.Namespace == "CantStop.Dto" && t.Name.EndsWith("Response")).ToList();
			UseCaseTypes = AllTypes.Where(t => t.Namespace == "CantStop" && t.Implements<IUseCase>() && !t.IsAbstract).ToList();
		}
	}
}