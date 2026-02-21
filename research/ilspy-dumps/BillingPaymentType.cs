using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum BillingPaymentType
{
	[OriginalName("BILLING_PAYMENT_TYPE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("BILLING_PAYMENT_TYPE_ONE_TIME")]
	OneTime,
	[OriginalName("BILLING_PAYMENT_TYPE_RECURRING")]
	Recurring
}
