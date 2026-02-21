using Google.Protobuf.Reflection;

namespace RootApp.Core.Enums;

public enum UploadTokenStatus
{
	[OriginalName("UPLOAD_TOKEN_STATUS_UNSPECIFIED")]
	Unspecified,
	[OriginalName("UPLOAD_TOKEN_STATUS_PENDING")]
	Pending,
	[OriginalName("UPLOAD_TOKEN_STATUS_INVALID")]
	Invalid,
	[OriginalName("UPLOAD_TOKEN_STATUS_ACCEPTED")]
	Accepted,
	[OriginalName("UPLOAD_TOKEN_STATUS_REJECTED")]
	Rejected
}
