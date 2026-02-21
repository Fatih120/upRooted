using System;
using System.Collections.Generic;
using System.Reflection;

namespace RootApp.WebApi.Shared.Permissions;

public static class CommunityPermissionsExtensions
{
	private static readonly PropertyInfo[] _permissionProperties = PermissionPropertyFactory.CreateArray<ICommunityPermissions, bool>();

	public static bool AllGreaterThan(this ICommunityPermissions P_0, ICommunityPermissions P_1)
	{
		PropertyInfo[] permissionProperties = _permissionProperties;
		foreach (PropertyInfo propertyInfo in permissionProperties)
		{
			bool flag = ((bool?)propertyInfo.GetValue(P_0)) ?? throw new InvalidOperationException();
			bool flag2 = ((bool?)propertyInfo.GetValue(P_1)) ?? throw new InvalidOperationException();
			if (!flag && flag2)
			{
				return false;
			}
		}
		return true;
	}

	public static void Copy(this ICommunityPermissions P_0, ICommunityPermissions P_1)
	{
		PropertyInfo[] permissionProperties = _permissionProperties;
		foreach (PropertyInfo propertyInfo in permissionProperties)
		{
			bool flag = ((bool?)propertyInfo.GetValue(P_0)) ?? throw new InvalidOperationException();
			propertyInfo.SetValue(P_1, flag);
		}
	}

	public static void SetAllTrue(this ICommunityPermissions P_0)
	{
		SetAll(P_0, true);
	}

	public static void SetAll(ICommunityPermissions P_0, bool P_1)
	{
		PropertyInfo[] permissionProperties = _permissionProperties;
		foreach (PropertyInfo propertyInfo in permissionProperties)
		{
			propertyInfo.SetValue(P_0, P_1);
		}
	}

	internal static bool Equals(ICommunityPermissions P_0, ICommunityPermissions P_1)
	{
		PropertyInfo[] permissionProperties = _permissionProperties;
		foreach (PropertyInfo propertyInfo in permissionProperties)
		{
			bool flag = ((bool?)propertyInfo.GetValue(P_0)) ?? throw new InvalidOperationException();
			bool flag2 = ((bool?)propertyInfo.GetValue(P_1)) ?? throw new InvalidOperationException();
			if (flag != flag2)
			{
				return false;
			}
		}
		return true;
	}

	public static CommunityPermission Flatten(this IEnumerable<ICommunityPermissions>? P_0)
	{
		if (P_0 == null)
		{
			return new CommunityPermission();
		}
		CommunityPermission communityPermission = new CommunityPermission();
		PropertyInfo[] permissionProperties = _permissionProperties;
		foreach (PropertyInfo propertyInfo in permissionProperties)
		{
			foreach (ICommunityPermissions item in P_0)
			{
				if (((bool?)propertyInfo.GetValue(item, null)) ?? throw new InvalidOperationException())
				{
					propertyInfo.SetValue(communityPermission, true);
					break;
				}
			}
		}
		return communityPermission;
	}
}
