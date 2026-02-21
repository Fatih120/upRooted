using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum ContentFlagReason
{
	[OriginalName("CONTENT_FLAG_REASON_UNSPECIFIED")]
	Unspecified,
	[OriginalName("CONTENT_FLAG_REASON_OTHER")]
	Other,
	[OriginalName("CONTENT_FLAG_REASON_DMCA")]
	Dmca,
	[OriginalName("CONTENT_FLAG_REASON_COPYRIGHT")]
	Copyright,
	[OriginalName("CONTENT_FLAG_REASON_SPAM")]
	Spam,
	[OriginalName("CONTENT_FLAG_REASON_HATESPEECH")]
	Hatespeech,
	[OriginalName("CONTENT_FLAG_REASON_VIOLENCE")]
	Violence,
	[OriginalName("CONTENT_FLAG_REASON_HARASSMENT")]
	Harassment,
	[OriginalName("CONTENT_FLAG_REASON_SEXUALCONTENT")]
	Sexualcontent,
	[OriginalName("CONTENT_FLAG_REASON_MISINFORMATION")]
	Misinformation,
	[OriginalName("CONTENT_FLAG_REASON_IMPERSONATION")]
	Impersonation,
	[OriginalName("CONTENT_FLAG_REASON_OBJECTIONABLE")]
	Objectionable
}
