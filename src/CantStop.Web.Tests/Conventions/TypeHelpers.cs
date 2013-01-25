namespace CantStop.Web.Tests.Conventions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	internal static class TypeHelpers
	{
		public static bool IsSubclassOf<T>(this Type type)
		{
			return type.IsSubclassOf(typeof(T));
		}

		public static bool Implements<TInterface>(this Type type)
		{
			return type.GetInterfaces().Contains(typeof (TInterface));
		}

		public static bool IsSuperClassOf<T>(this Type type)
		{
			return typeof(T).IsSubclassOf(type);
		}

		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type)
		{
			return type
				.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
				.Where(m => !m.IsSpecialName);
		}

		public static IEnumerable<Type> GetAllTypes(string @namespace = null)
		{
			var types = GetAllAssemblies().SelectMany(a => a.GetTypes());

			if (!string.IsNullOrEmpty(@namespace))
				types = types.Where(t => t.Namespace != null && t.Namespace.StartsWith(@namespace));

			return types;
		}

		public static IEnumerable<Assembly> GetAllAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies();
		}

		public static IEnumerable<Type> WithAttribute<TAttribute>(this IEnumerable<Type> types)
		{
			return WithAttribute<TAttribute>(types, a => true);
		}

		public static IEnumerable<Type> WithAttribute<TAttribute>(this IEnumerable<Type> types, Func<TAttribute, bool> predicate)
		{
			return types.Where(t => t.GetCustomAttributes(typeof (TAttribute), false).Cast<TAttribute>().Any(predicate));
		}
	}
}