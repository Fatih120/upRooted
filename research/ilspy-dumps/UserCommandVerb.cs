using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc;

public enum UserCommandVerb
{
	[OriginalName("USER_COMMAND_VERB_UNSPECIFIED")]
	Unspecified = 0,
	[OriginalName("USER_COMMAND_VERB_GET")]
	Get = 1,
	[OriginalName("USER_COMMAND_VERB_CREATE")]
	Create = 2,
	[OriginalName("USER_COMMAND_VERB_EDIT")]
	Edit = 3,
	[OriginalName("USER_COMMAND_VERB_DELETE")]
	Delete = 4,
	[OriginalName("USER_COMMAND_VERB_LIST")]
	List = 5,
	[OriginalName("USER_COMMAND_VERB_TARGET_SPECIFIC_1")]
	TargetSpecific1 = 101,
	[OriginalName("USER_COMMAND_VERB_TARGET_SPECIFIC_2")]
	TargetSpecific2 = 102,
	[OriginalName("USER_COMMAND_VERB_TARGET_SPECIFIC_3")]
	TargetSpecific3 = 103,
	[OriginalName("USER_COMMAND_VERB_TARGET_SPECIFIC_4")]
	TargetSpecific4 = 104,
	[OriginalName("USER_COMMAND_VERB_TARGET_SPECIFIC_5")]
	TargetSpecific5 = 105,
	[OriginalName("USER_COMMAND_VERB_TARGET_SPECIFIC_6")]
	TargetSpecific6 = 106,
	[OriginalName("USER_COMMAND_VERB_TARGET_SPECIFIC_7")]
	TargetSpecific7 = 107,
	[OriginalName("USER_COMMAND_VERB_TARGET_SPECIFIC_8")]
	TargetSpecific8 = 108,
	[OriginalName("USER_COMMAND_VERB_TARGET_SPECIFIC_9")]
	TargetSpecific9 = 109
}
