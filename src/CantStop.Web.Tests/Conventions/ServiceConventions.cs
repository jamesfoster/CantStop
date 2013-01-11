namespace CantStop.Web.Tests.Conventions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using ApprovalTests;
	using ApprovalTests.Reporters;
	using Dto;
	using NUnit.Framework;
	using ServiceStack.ServiceHost;

	[TestFixture]
	[UseReporter(typeof (KDiffReporter))]
	internal class ServiceConventions
	{
		protected List<Type> AllTypes { get; set; }
		protected List<Type> ServiceTypes { get; set; }
		protected List<Type> UseCaseTypes { get; set; }

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			// load assemblies!
			var types = new[]
				{
					typeof (CreateGameService),
					typeof (CreateGameRequest),
					typeof (CreateGame)
				};

			AllTypes = TypeHelpers.GetAllTypes().ToList();
			ServiceTypes = AllTypes.Where(t => t.Namespace == "CantStop.Web" && t.Implements<IService>()).ToList();
			UseCaseTypes = AllTypes.Where(t => t.Namespace == "CantStop" && t.Implements<IUseCase>()).ToList();
		}

		[Test]
		public void Each_use_case_should_have_a_corresponding_service()
		{
			var services = ServiceTypes
				.Select(s => s.Name.Replace("Service", ""))
				.ToList();

			var useCases = UseCaseTypes
				.Where(t => !services.Contains(t.Name))
				.Select(t => t.FullName)
				.ToList();

			var approval = "Use cases without a matching service:\n\t" + string.Join("\n\t", useCases);

			Approvals.Verify(approval);
		}

		[Test]
		public void A_service_method_should_have_a_single_argument()
		{
			var services = ServiceTypes.SelectMany(t =>
			{
				var methods = t.GetDeclaredMethods();

				return methods.Where(m => m.GetParameters().Length != 1);
			});

			var approval = "Service methods without exactly 1 argument:\n\t" + string.Join("\n\t", services);

			Approvals.Verify(approval);
		}

		[Test]
		public void Service_methods_should_not_return_void()
		{
			var services = ServiceTypes.SelectMany(t =>
			{
				var methods = t.GetDeclaredMethods();

				return methods.Where(m => m.ReturnType == typeof(void));
			});

			var approval = "Service methods returning void:\n\t" + string.Join("\n\t", services);

			Approvals.Verify(approval);
		}
	}
}
