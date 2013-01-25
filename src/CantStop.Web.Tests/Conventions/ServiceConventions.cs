namespace CantStop.Web.Tests.Conventions
{
	using System.Linq;
	using ApprovalTests;
	using ApprovalTests.Reporters;
	using NUnit.Framework;

	[TestFixture]
	[UseReporter(typeof (KDiffReporter))]
	public class ServiceConventions : ConventionsBase
	{
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
			var serviceMethods = ServiceTypes.SelectMany(
				t => t.GetDeclaredMethods().Where(m => m.GetParameters().Length != 1)
				);

			var approval = "Service methods without exactly 1 argument:\n\t" + string.Join("\n\t", serviceMethods);

			Approvals.Verify(approval);
		}

		[Test]
		public void Service_methods_should_not_return_void()
		{
			var services = ServiceTypes.SelectMany(
				t => t.GetDeclaredMethods().Where(m => m.ReturnType == typeof (void))
				);

			var approval = "Service methods returning void:\n\t" + string.Join("\n\t", services);

			Approvals.Verify(approval);
		}
	}
}
