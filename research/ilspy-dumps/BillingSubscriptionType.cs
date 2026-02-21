using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum BillingSubscriptionType
{
	[OriginalName("BILLING_SUBSCRIPTION_TYPE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("BILLING_SUBSCRIPTION_TYPE_COMMUNITY")]
	Community
}
