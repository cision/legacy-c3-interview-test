using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Configuration;
using SendGrid.Helpers.Mail;
using SendGridWrapper;
using SendGridWrapper.Models;
using Mindscape.Raygun4Net;
using System.Text;
using System.Windows.Forms;

namespace SendGridTester
{
    internal class MainViewModel : ViewModelBase
    {

        #region Properties
        private bool _buttonsEnabled;

        public bool ButtonsEnabled
        {
            get { return _buttonsEnabled; }
            set {
                if (_buttonsEnabled != value)
                {
                    _buttonsEnabled = value;
                    RaisePropertyChanged();
                }
                
            }
        }

        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool EnableTracking { get; set; }
        public RelayCommand SendEmailCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            ButtonsEnabled = true;
            ClearCommand = new RelayCommand(Clear);
            SendEmailCommand = new RelayCommand(SendEmail);

            EmailFrom = "noreply@cision.com";
            EmailTo = "michael.dacar@cision.com";
            Subject = "Test too large email";
            Body = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam nec mi sed tortor feugiat rhoncus ac sed tellus. Curabitur magna quam, lacinia sit amet purus at, dignissim pulvinar ex. Ut at venenatis sapien. Nunc volutpat lorem eu vehicula sodales. Phasellus convallis ipsum ligula, id ullamcorper dolor iaculis non. Cras eget elementum felis. Sed euismod ultricies libero vitae ultrices. Sed nulla quam, elementum id dapibus nec, lacinia ac purus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi congue volutpat nibh, vitae porttitor ex vulputate faucibus. Morbi eu rutrum purus.";
        }

        private static RaygunClient _raygunClient = new RaygunClient(ConfigurationManager.AppSettings["RaygunApiKey"]);

        internal void SendEmail()
        {
            try
            {
                ButtonsEnabled = false;
                Application.DoEvents();

                StringBuilder bigBody = new StringBuilder();
                for (int i = 0; i < 5000; i++)
                {
                    bigBody.AppendLine(Body);
                }
                //this is only handling the simplest to get things rolling with some way to test
                //no bcc,cc,html,attachments, etc....   
                var msg = new Email
                {
                    Message = new SendGridMessage
                    {
                        Subject = Subject,
                        From = new EmailAddress(EmailFrom)
                    },
                    ApiKey = ConfigurationManager.AppSettings["SendGridAPIKey"]
                };

                msg.EnableTracking = EnableTracking;
                msg.Message.Contents = new List<Content>
                {
                    new Content
                    {
                        Value = bigBody.ToString(),
                        Type = "text/plain"
                    }
                };

                msg.Message.Personalizations = new List<Personalization>();
                if (!string.IsNullOrWhiteSpace(EmailTo))
                {
                    msg.Message.Personalizations = (BuildToRecipients());
                }

                var sender = new EmailSender(LogExceptionToRaygun);
                sender.SendEmail(msg);
                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error sending email", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            finally
            {
                ButtonsEnabled = true;
            }
        }

        private void LogExceptionToRaygun(Exception ex, string payload, string url)
        {
            _raygunClient.SendInBackground(new Exception($"{ex.Message}\nPAYLOAD:\n\n{payload}\n\nEND PAYLOAD", ex), new List<string> { url });
        }

        private List<Personalization> BuildToRecipients()
        {
            var addressesTo = EmailTo?.Split(';');
            var addressesBcc = EmailBCC?.Split(';');
            var addressesCc = EmailCC?.Split(';');

            var personalizationList = new List<Personalization>();
            foreach (var address in addressesTo)
            {
                var personalization = new Personalization
                {
                    Tos = new List<EmailAddress> {new EmailAddress() {Email = address}}
                };

                if (addressesBcc != null)
                {
                    foreach (var bcc in addressesBcc)
                    {
                        personalization.Bccs = new List<EmailAddress> { new EmailAddress() { Email = bcc } };
                    }
                }

                if (addressesCc != null)
                {
                    foreach (var cc in addressesCc)
                    {
                        personalization.Ccs = new List<EmailAddress> { new EmailAddress() { Email = cc } };
                    }
                }

                personalizationList.Add(personalization);
            }

            return personalizationList;
        }

        internal void Clear()
        {
            EmailBCC = string.Empty;
            EmailCC = string.Empty;
            EmailTo = string.Empty;
            EmailFrom = string.Empty;
            Subject = string.Empty;
            Body = string.Empty;
        }

    }
}
