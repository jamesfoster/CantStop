namespace CantStop.Web.Tests.Conventions
{
	using System.Linq;
	using ApprovalTests;
	using ApprovalTests.Reporters;
	using Configure;
	using Mocks;
	using NUnit.Framework;

	[TestFixture]
	[UseReporter(typeof(KDiffReporter))]
	public class RouteConventions : ConventionsBase
	{
		protected Routes Config { get; set; }
		protected MockServiceRoutes ServiceRoutes { get; set; }

		[SetUp]
		public void SetUp()
		{
			ServiceRoutes = new MockServiceRoutes();
			Config = new Routes();

			Config.Configure(ServiceRoutes);
		}

		[Test]
		public void Every_request_object_should_have_at_least_one_corresponding_route()
		{
			var requestTypes = RequestTypes.Where(t => !ServiceRoutes.RegisteredRoutes.Exists(r => r.RequestType == t));

			var approval = "Request types without a matching route\n\t" + string.Join("\n\t", requestTypes);

			Approvals.Verify(approval);
		}
	}
}
