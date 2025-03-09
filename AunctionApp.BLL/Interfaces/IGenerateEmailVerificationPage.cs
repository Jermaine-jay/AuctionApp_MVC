namespace AunctionApp.BLL.Interfaces
{
    public interface IGenerateEmailVerificationPage
    {
        public string EmailVerificationPage(string name, string callbackurl, string baseURL);
        public string PasswordResetPage(string callbackurl, string baseURL);
		public string ChangePasswordPage(string code, string baseURL);
	}
}
