using SendGridWrapper.Models;
using System;
using RestSharp;
using Newtonsoft.Json;

namespace SendGridWrapper
{
    public class EmailSender
    {
        private Action<Exception, string, string> _loggingFunction;

        public EmailSender()
        {
            _loggingFunction = null;
        }

        public EmailSender(Action<Exception, string, string> loggingFunction)
        {
            _loggingFunction = loggingFunction;
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        public void SendEmail(Email emailDetails)
        {
            SendEmail(emailDetails, 0);
        }

        private void SendEmail(Email email, int retryCount)
        {
            if(string.IsNullOrEmpty(email.ApiKey))
                throw new Exception("Send Grid ApiKey is needed");

            try
            {
                var emailValidation = new EmailValidation();
                emailValidation.IsEmailValid(email.Message);

                //turn tracking off for request
                if (!email.EnableTracking)
                {
                    email.Message.SetOpenTracking(false, "");
                    email.Message.SetClickTracking(false, false);
                }

                var client = new RestClient("https://api.sendgrid.com/v3/mail/send");

                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer " + email.ApiKey);
                request.AddParameter("application/json", JsonConvert.SerializeObject(email.Message),
                    ParameterType.RequestBody);

                var response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    return;
                throw new Exception($"Failed to send the email through sendgrid:\n{response.Content}");
            }
            catch (ArgumentException ex)
            {
                LogException(ex, email);
                throw ex;
            }
            catch (Exception ex)
            {
                HandleEmailException(email, ++retryCount, ex);
            }
        }

        private void HandleEmailException(Email email, int retryCount, Exception ex)
        {
            if (retryCount < 3)
                SendEmail(email, retryCount);
            else
            {
                LogException(ex, email);
                throw ex;
            }
        }

        private void LogException(Exception ex, Email email)
        {
            try
            {
                _loggingFunction?.Invoke(ex, JsonConvert.SerializeObject(email.Message), email.ApiKey);
            }
            catch (Exception) { }
        }
    }
}
