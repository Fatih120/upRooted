using Google.Protobuf.Reflection;

namespace RootApp.App;

public enum RoleOrMemberPickerBehavior
{
	[OriginalName("ROLE_OR_MEMBER_PICKER_BEHAVIOR_UNSPECIFIED")]
	Unspecified,
	[OriginalName("ROLE_OR_MEMBER_PICKER_BEHAVIOR_USER")]
	User,
	[OriginalName("ROLE_OR_MEMBER_PICKER_BEHAVIOR_USERS")]
	Users,
	[OriginalName("ROLE_OR_MEMBER_PICKER_BEHAVIOR_ROLE")]
	Role,
	[OriginalName("ROLE_OR_MEMBER_PICKER_BEHAVIOR_ROLES")]
	Roles,
	[OriginalName("ROLE_OR_MEMBER_PICKER_BEHAVIOR_ROLES_AND_USERS")]
	RolesAndUsers
}
