﻿using AunctionApp.BLL.Extensions;
using AunctionApp.BLL.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using User = AunctionApp.DAL.Entities.User;

namespace AunctionApp.BLL.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly EmailSenderOptions _emailSenderOptions;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IGenerateEmailVerificationPage _generateEmailVerificationPage;
        private readonly IServiceFactory _serviceFactory;
        private string? _ApiKey;
        private string? _Url;

        public AuthenticationService(IServiceFactory serviceFactory, IConfiguration configuration, UserManager<User> userManager, IOptions<EmailSenderOptions> optionsAccessor)
        {
            _userManager = userManager;
            _serviceFactory = serviceFactory;
            _configuration = configuration;
            _emailSenderOptions = optionsAccessor.Value;
            _ApiKey = _configuration["ZeroBook:ApiKey"];
            _Url = _configuration["ZeroBook:Url"];
        }


        public async Task<(bool successful, string msg)> ConfirmEmail(string userId, string code)
        {
            var convert = Convert.FromBase64String(userId);
            string encodeduserId = Encoding.UTF8.GetString(convert);

            var user = await _userManager.FindByIdAsync(encodeduserId);
            if (user == null)
            {
                return (false, "Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return (true, "EmailConfirmed");
            }
            return (false, "Invalid Verification Code");
        }


        public async Task<(bool successful, string msg)> SendEmailAsync(string email, string subject, string message)
        {

            if (string.IsNullOrEmpty(_emailSenderOptions.Password))
            {
                return (false, "Couldn't complete operation");
            }
            await Execute(email, subject, message);
            return (true, "Verification Mail sent to your Email Address");
        }


        public async Task<bool> Execute(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Jermaine Auction", _emailSenderOptions.Username));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlMessage;
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {

                client.Connect(_emailSenderOptions.SmtpServer, _emailSenderOptions.Port, true);
                client.Authenticate(_emailSenderOptions.Email, _emailSenderOptions.Password);
                client.Send(message);
                client.Disconnect(true);

            }

            return true;
        }

        public async Task<bool> VerifyEmail(string emailAddress)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var parameters = $"api_key={_ApiKey}&email={emailAddress}";
                    var response = await httpClient.GetAsync($"{_Url}?{parameters}");
                    response.EnsureSuccessStatusCode();
                    
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var getResponse = JsonConvert.DeserializeObject<dynamic>(responseContent).status;
                    if (getResponse == "valid")
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error verifying email: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> RegistrationMail(User newUser)
        {
            var page = _serviceFactory.GetService<IGenerateEmailVerificationPage>().EmailVerificationPage;
            var context = _serviceFactory.GetService<IHttpContextAccessor>().HttpContext;

            var link = _serviceFactory.GetService<LinkGenerator>();
            var appcode = Convert.ToBase64String(Encoding.UTF8.GetBytes(newUser.Id));

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var callbackUrl = link.GetUriByAction(context,"ConfirmEmail", "User", new { appcode, code });
            string? baseUrl = link.GetUriByAction(context, action: "Index", controller: "Home");
            await SendEmailAsync(newUser.Email, "Confirm your email", page(newUser.UserName, callbackUrl, baseUrl));
             
            return true;
        }

    }
}
