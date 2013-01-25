namespace CantStop.Web.Tests.Mocks
{
	using System;
	using System.Collections.Generic;
	using ServiceStack.ServiceHost;

	public class MockServiceRoutes : IServiceRoutes
	{
		public List<RouteDef> RegisteredRoutes { get; private set; }

		public MockServiceRoutes()
		{
			RegisteredRoutes = new List<RouteDef>();
		}

		public IServiceRoutes Add<TRequest>(string restPath)
		{
			return Add(typeof (TRequest), restPath);
		}

		public IServiceRoutes Add<TRequest>(string restPath, string verbs)
		{
			return Add(typeof (TRequest), restPath, verbs);
		}

		public IServiceRoutes Add(Type requestType, string restPath, string verbs)
		{
			return Add(requestType, restPath, verbs, null, null);
		}

		public IServiceRoutes Add(Type requestType, string restPath, string verbs = null, string summary = null,
		                          string notes = null)
		{
			RegisteredRoutes.Add(new RouteDef(requestType, restPath, verbs, summary, notes));

			return this;
		}

		public class RouteDef
		{
			public Type RequestType { get; set; }
			public string RestPath { get; set; }
			public string Verbs { get; set; }
			public string Summary { get; set; }
			public string Notes { get; set; }

			public RouteDef(Type requestType, string restPath, string verbs, string summary, string notes)
			{
				RequestType = requestType;
				RestPath = restPath;
				Verbs = verbs;
				Summary = summary;
				Notes = notes;
			}
		}
	}
}