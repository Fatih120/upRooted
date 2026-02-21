using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum ErrorCodeType
{
	[OriginalName("ERROR_CODE_TYPE_UNSPECIFIED")]
	Unspecified = 0,
	[OriginalName("ERROR_CODE_TYPE_SERVER_ERROR")]
	ServerError = 1,
	[OriginalName("ERROR_CODE_TYPE_NOT_FOUND")]
	NotFound = 2,
	[OriginalName("ERROR_CODE_TYPE_ALREADY_EXISTS")]
	AlreadyExists = 3,
	[OriginalName("ERROR_CODE_TYPE_WRONG_TYPE")]
	WrongType = 4,
	[OriginalName("ERROR_CODE_TYPE_NOT_ENOUGH_MEMBERS")]
	NotEnoughMembers = 5,
	[OriginalName("ERROR_CODE_TYPE_NOT_MEMBER_OF")]
	NotMemberOf = 6,
	[OriginalName("ERROR_CODE_TYPE_REQUESTED_SELF")]
	RequestedSelf = 7,
	[OriginalName("ERROR_CODE_TYPE_BANNED")]
	Banned = 8,
	[OriginalName("ERROR_CODE_TYPE_TOO_MANY_REQUESTS")]
	TooManyRequests = 9,
	[OriginalName("ERROR_CODE_TYPE_TOO_LARGE")]
	TooLarge = 10,
	[OriginalName("ERROR_CODE_TYPE_POLICY")]
	Policy = 11,
	[OriginalName("ERROR_CODE_TYPE_TIMEOUT")]
	Timeout = 12,
	[OriginalName("ERROR_CODE_TYPE_STILL_PROCESSING")]
	StillProcessing = 13,
	[OriginalName("ERROR_CODE_TYPE_UN_AUTHENTICATED")]
	UnAuthenticated = 16,
	[OriginalName("ERROR_CODE_TYPE_BLOCKED")]
	Blocked = 17,
	[OriginalName("ERROR_CODE_TYPE_FAILED_TO_CREATE")]
	FailedToCreate = 100,
	[OriginalName("ERROR_CODE_TYPE_FAILED_TO_PARSE")]
	FailedToParse = 101,
	[OriginalName("ERROR_CODE_TYPE_FAILED_TO_AUTHENTICATE")]
	FailedToAuthenticate = 102,
	[OriginalName("ERROR_CODE_TYPE_FAILED_TO_SIGN_UP")]
	FailedToSignUp = 103,
	[OriginalName("ERROR_CODE_TYPE_FAILED_TO_UPLOAD")]
	FailedToUpload = 104,
	[OriginalName("ERROR_CODE_TYPE_FAILED_EMAIL_VERIFICATION_CODE_TIMEOUT")]
	FailedEmailVerificationCodeTimeout = 105,
	[OriginalName("ERROR_CODE_TYPE_FAILED_EMAIL_VERIFICATION_CODE")]
	FailedEmailVerificationCode = 106,
	[OriginalName("ERROR_CODE_TYPE_PENDING_FRIENDSHIP_REQUESTED_SELF")]
	PendingFriendshipRequestedSelf = 200,
	[OriginalName("ERROR_CODE_TYPE_REQUEST_VALIDATION_FAILED")]
	RequestValidationFailed = 300,
	[OriginalName("ERROR_CODE_TYPE_POLICY_VIOLATION_WELL_KNOWN_PASSWORD")]
	PolicyViolationWellKnownPassword = 400,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_CREATE")]
	NoPermissionToCreate = 1000,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_ADD")]
	NoPermissionToAdd = 1001,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_READ")]
	NoPermissionToRead = 1002,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_EDIT")]
	NoPermissionToEdit = 1003,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_DELETE")]
	NoPermissionToDelete = 1004,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_MOVE")]
	NoPermissionToMove = 1005,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_UPLOAD")]
	NoPermissionToUpload = 1006,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_TYPE")]
	NoPermissionToType = 1007,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_SPEAK")]
	NoPermissionToSpeak = 1008,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_KICK")]
	NoPermissionToKick = 1009,
	[OriginalName("ERROR_CODE_TYPE_NO_PERMISSION_TO_BAN")]
	NoPermissionToBan = 1010
}
