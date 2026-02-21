using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace RootApp.WebApi.Shared.Permissions;

internal static class PermissionPropertyFactory
{
	public static FrozenDictionary<string, PropertyInfo> CreateDictionary<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TPermissions, TPropertyType>()
	{
		return GetProperties<TPermissions, TPropertyType>().ToFrozenDictionary((PropertyInfo x) => x.Name);
	}

	public static PropertyInfo[] CreateArray<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TPermissions, TPropertyType>()
	{
		return GetProperties<TPermissions, TPropertyType>().ToArray();
	}

	private static IEnumerable<PropertyInfo> GetProperties<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TPermissions, TPropertyType>()
	{
		return from x in typeof(TPermissions).GetProperties()
			where x.PropertyType == typeof(TPropertyType)
			select x;
	}
}
