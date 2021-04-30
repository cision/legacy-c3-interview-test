using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SendGridWrapper
{
    public class EmailValidation
    {

        public void IsEmailValid(SendGridMessage email)
        {
            var errors = GetValidationErrors(email);
            if (!string.IsNullOrWhiteSpace(errors))
                throw new ArgumentException($"Email is invalid:\n{errors}");

        }

        public string GetValidationErrors(SendGridMessage email)
        {
            var errors = new StringBuilder();

          

    if (string.IsNullOrEmpty(email.From.Email))
        errors.AppendLine("FromAddress is required");

        //need to see if the first values is blank
        if (string.IsNullOrEmpty(email.Contents[0].Value))
            errors.AppendLine("Body is required");

    if(email.Personalizations.Count == 0)
        errors.AppendLine("At least one recipient is required");

            foreach (var personalization in email.Personalizations)
            {
                if (personalization.Tos == null || personalization.Tos.Count == 0)
                    errors.AppendLine("At least one recipient is required");
                else
                {
                    foreach (var t in personalization.Tos)
                    {
                        if (!IsValidEmailAddress(t.Email))
                            errors.AppendLine($"{t.Email} is an invalid e-mail address");
                    }
                }

                int ccCounts = 0;
                if (personalization.Ccs != null)
                {
                    foreach (var t in personalization.Ccs)
                    {
                        if (!IsValidEmailAddress(t.Email))
                            errors.AppendLine($"{t.Email} is an invalid Cc e-mail address");
                    }
                    ccCounts = personalization.Ccs.Count;
                }

                int bccCounts = 0;
                if (personalization.Bccs != null)
                {
                    foreach (var t in personalization.Bccs)
                    {
                        if (!IsValidEmailAddress(t.Email))
                            errors.AppendLine($"{t.Email} is an invalid Bcc e-mail address");
                    }
                    bccCounts = personalization.Bccs.Count;
                }

                if(personalization.Tos != null &&  (bccCounts + ccCounts + personalization.Tos.Count) > 1000)
                    errors.AppendLine("To many total reciepients in this email reques");
            }

            if (string.IsNullOrWhiteSpace(email.Subject))
                errors.AppendLine("Subject is required");

            return errors.ToString();
        }

        private bool IsValidEmailAddress(string address)
        {
            return EmailValidator.IsMatch(address);
        }

        private Regex _emailValidator;


        private Regex EmailValidator
        {
            get
            {
                if (null != _emailValidator) { return _emailValidator; }

                // text that doesn't include carriage return, ", \, or international characters.
                string quotedText = "[^\\x0d\\x22\\x5c\\x80-\\xff]";

                //text that contains 0-9,a-z, or -
                string domainText = "[\\x30-\\x39\\x61-\\x7a\\x2d]";

                //text that doesn't include any control characters, ", (, ), comma, ., :, ;, <, >, @, [, \, ], internation characters
                string atom = "[^\\x00-\\x20\\x22\\x2c\\x2e\\x3a-\\x3c\\x3e\\x40\\x5b-\\x5d\\x7f-\\xff]+";
                //text that contains 0-9,a-z, or -
                string domainAtom = "[\\x30-\\x39\\x61-\\x7a\\x2d]+";

                // \[any us ascii character] cannot include any escaped character  (\*)
                string pair = "\\x5c[^[(\\x5c.)]\\x00-\\x7f]";

                //[ dtext or pair ]
                string domainLiteral = "\\x5b(" + domainText + "|" + pair + ")*\\x5d";

                // " qtext or pair "
                string quotedString = "\\x22(" + quotedText + "|" + pair + ")+\\x22";

                string subDomain = "(" + domainAtom + "|" + domainLiteral + ")";

                //Cannot start with -%$!#*/?^`{\|}~ or any special international characters unless quoted
                string word = "([^\\x00-\\x25\\x28\\x29\\x2a\\x2c\\x2e-\\x2f\\x3a-\\x3c\\x3e\\x3f\\x40\\x5b-\\x5e\\x60\\x7b-\\xff]("
                    + atom + ")?|" + quotedString + ")";
                string firstWord = "([^\\x00-\\x25\\x28\\x29\\x2a\\x2c-\\x2f\\x3a-\\x3c\\x3e\\x3f\\x40\\x5b-\\x5e\\x60\\x7b-\\xff]("
                    + atom + ")?|" + quotedString + ")";

                //must end with a . and 1+ atoms.
                string domain = subDomain + "(\\x2e" + subDomain + ")*\\x2e(" + domainAtom + ")+";
                string localPart = firstWord + "(\\x2e" + word + ")*";

                _emailValidator = new Regex("^" + localPart + "\\x40" + domain + "$", RegexOptions.IgnoreCase);


                return _emailValidator;
            }
        }
    }
}
