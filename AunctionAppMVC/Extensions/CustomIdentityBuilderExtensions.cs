using AunctionAppMVC.Extensions;
using Microsoft.AspNetCore.Identity;

namespace AunctionApp.BLL.Extensions
{
	public static class CustomIdentityBuilderExtensions
	{
		public static IdentityBuilder AddPasswordlessLoginTotpTokenProvider(this IdentityBuilder builder)
		{
			var userType = builder.UserType;
			var totpProvider = typeof(PasswordlessLoginTotpTokenProvider<>).MakeGenericType(userType);
			return builder.AddTokenProvider("PasswordlessLoginTotpProvider", totpProvider);
		}
	}
}
