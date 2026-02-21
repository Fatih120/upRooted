namespace RootApp.WebApi.Client.Shared.Turnstile;

public static class TurnstileConstants
{
	public static class Actions
	{
		public const string SignIn = "signin";

		public const string SignUp = "signup";

		public const string ForgotPassword = "forgot_password";

		public const string ForgotUsername = "forgot_username";

		public const string ResetPassword = "reset_password";

		public const string ResendVerification = "resend_verification";

		public const string CommunityAppAdd = "community_app_add";
	}

	public const string ChallengeUrlHeader = "turnstile-challenge-url";
}
