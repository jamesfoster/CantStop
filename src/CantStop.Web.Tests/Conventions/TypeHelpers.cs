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

		public static MethodInfo[] GetDeclaredMethods(this Type type)
		{
			return type.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
		}

		public static IEnumerable<Type> GetAllTypes()
		{
			return GetReferencedAssemblies()
				.SelectMany(a => a.GetTypes());
		}

		public static IEnumerable<Assembly> GetReferencedAssemblies()
		{
			return Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load);
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