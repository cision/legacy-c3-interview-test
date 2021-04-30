using System;
using SendGrid.Helpers.Mail;

namespace SendGridWrapper.Models
{
    public class Email
    {
        public SendGridMessage Message { get; set; }

        [NonSerialized]
        public string ApiKey = string.Empty;

        [NonSerialized]
        public bool EnableTracking = false;
    }
}
