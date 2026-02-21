using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace RootApp.WebApi.Shared.Permissions;

public static class ChannelPermissionsExtensions
{
	internal static readonly FrozenDictionary<string, PropertyInfo> _permissionProperties = PermissionPropertyFactory.CreateDictionary<IChannelPermissions, bool>();

	public static bool AllGreaterThan(this IChannelPermissions P_0, IChannelPermissions P_1)
	{
		ImmutableArray<PropertyInfo>.Enumerator enumerator = _permissionProperties.Values.GetEnumerator();
		while (enumerator.MoveNext())
		{
			PropertyInfo current = enumerator.Current;
			bool flag = ((bool?)current.GetValue(P_0)) ?? throw new InvalidOperationException();
			bool flag2 = ((bool?)current.GetValue(P_1)) ?? throw new InvalidOperationException();
			if (!flag && flag2)
			{
				return false;
			}
		}
		return true;
	}

	public static void Copy(this IChannelPermissions P_0, IChannelPermissions P_1)
	{
		ImmutableArray<PropertyInfo>.Enumerator enumerator = _permissionProperties.Values.GetEnumerator();
		while (enumerator.MoveNext())
		{
			PropertyInfo current = enumerator.Current;
			bool flag = ((bool?)current.GetValue(P_0)) ?? throw new InvalidOperationException();
			current.SetValue(P_1, flag);
		}
	}

	public static void SetAll(IChannelPermissions P_0, bool P_1)
	{
		ImmutableArray<PropertyInfo>.Enumerator enumerator = _permissionProperties.Values.GetEnumerator();
		while (enumerator.MoveNext())
		{
			PropertyInfo current = enumerator.Current;
			current.SetValue(P_0, P_1);
		}
	}

	public static bool Equals(IChannelPermissions P_0, IChannelPermissions P_1)
	{
		ImmutableArray<PropertyInfo>.Enumerator enumerator = _permissionProperties.Values.GetEnumerator();
		while (enumerator.MoveNext())
		{
			PropertyInfo current = enumerator.Current;
			bool flag = ((bool?)current.GetValue(P_0)) ?? throw new InvalidOperationException();
			bool flag2 = ((bool?)current.GetValue(P_1)) ?? throw new InvalidOperationException();
			if (flag != flag2)
			{
				return false;
			}
		}
		return true;
	}

	public static ChannelPermission Flatten(this IEnumerable<IChannelPermissions>? P_0)
	{
		if (P_0 == null)
		{
			return new ChannelPermission();
		}
		IReadOnlyCollection<IChannelPermissions> readOnlyCollection = (IReadOnlyCollection<IChannelPermissions>)(((object)(P_0 as IReadOnlyCollection<IChannelPermissions>)) ?? ((object)P_0.ToArray()));
		ChannelPermission channelPermission = new ChannelPermission();
		ImmutableArray<PropertyInfo>.Enumerator enumerator = _permissionProperties.Values.GetEnumerator();
		while (enumerator.MoveNext())
		{
			PropertyInfo current = enumerator.Current;
			foreach (IChannelPermissions item in readOnlyCollection)
			{
				if (((bool?)current.GetValue(item, null)) ?? throw new InvalidOperationException())
				{
					current.SetValue(channelPermission, true);
					break;
				}
			}
		}
		return channelPermission;
	}
}
