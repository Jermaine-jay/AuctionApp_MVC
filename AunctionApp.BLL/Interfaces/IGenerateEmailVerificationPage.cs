﻿namespace AunctionApp.BLL.Interfaces
{
    public interface IGenerateEmailVerificationPage
    {
        public string EmailVerificationPage(string name, string callbackurl);
        public string PasswordResetPage(string callbackurl);
		public string ChangePasswordPage(string code);
	}
}
