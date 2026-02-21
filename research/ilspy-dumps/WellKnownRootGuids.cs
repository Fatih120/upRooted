namespace RootApp.Core.Identifiers;

public static class WellKnownRootGuids
{
	public static readonly CommunityRoleGuid EveryoneRole = new CommunityRoleGuid((High64: 10345041100832779uL, Low64: 9223372036854775809uL));

	public static readonly FriendshipGroupGuid FriendshipGroupDefault = new FriendshipGroupGuid(10345041100832775uL, 9223372036854775809uL);

	public static readonly BadgeGuid RootEmployeeBadge = new BadgeGuid(10345041100832802uL, 9223372036854775809uL);
}
