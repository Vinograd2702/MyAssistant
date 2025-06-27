using MimeKit;

namespace notification_service.Infrastructure.Mail.MessageTemplates
{
    public static class CheckNewUserEmailMessageTemplate
    {
        public static MimeMessage CreateMessage(string EmailFromSend,
            string EmailToSend, 
            string UrlToComfirmEmail, 
            string UrlToBlockEmail)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("MyAssistant", EmailFromSend));
            message.To.Add(new MailboxAddress("User", EmailToSend));
            message.Subject = "Регистрация в сервисе MyAssistant";

            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = @"
                <html>
                    <div>
                        <table>
                            <tbody>
                                <tr>
                                    <td style=""padding: 20px 30px 20px 30px; text-align: left;"">
                                        <a style=""background: #ffffff; text-decoration: none;"" href=""https://my-assistant-dev.ru"">
                                            <h1 style=""color: #000000; font-family:'arial' , sans-serif; font-weight:bold; line-height:20px;margin:0; font-size: 20px;"">MyAssistant</h1>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""background-color:#ffffff;border-top-left-radius:5px;border-top-right-radius:5px;font-family:'arial';font-size:15px;line-height:19px;padding:16px 30px 16px 30px"">
                                        <h1 style=""color: #000000; font-family:'arial' , sans-serif; font-weight:lighter;line-height:20px;margin:0; font-size: 20px;"">Подтверждение адреса электронной почты</h1>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""background-color:#ffffff;border-top-left-radius:5px;border-top-right-radius:5px;font-family:'arial';font-size:15px;line-height:19px;padding:16px 30px 16px 30px"">
                                        <p style=""margin:0"">Для завершения процесса регистрации и подтверждения почты нажмите на зеленую кнопку.</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""background-color:#ffffff;border-bottom-left-radius:5px;border-bottom-right-radius:5px;font-family:'arial';font-size:16px;font-weight:700;line-height:19px;padding:8px 30px 8px 30px"">
                                        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border:0;border-collapse:collapse;border-spacing:0""><tbody><tr><td style=""background:#ffffff;border-radius:4px"">
                                                    <a href=""" + UrlToComfirmEmail + @""" style=""background:#AFFF69;border:0px solid;border-radius:20px;color:#000000;display:block;font-family:'arial' , sans-serif;font-size:16px;line-height:16px;padding:12px 17px 12px 17px;text-align:center;text-decoration:none"">
                                                        Подтвердить Емаил
                                                    </a>
                                                </td></tr></tbody></table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""background-color:#ffffff;border-top-left-radius:5px;border-top-right-radius:5px;font-family:'arial';font-size:15px;line-height:19px;padding:16px 30px 16px 30px"">
                                        <p style=""margin:0"">Если Вы не регистрировались на сайте проекта и письмо попало к Вам по ошибке - нажмите на кнопку для блокировки отправки сообщений или игнорируйте это письмо.</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""background-color:#ffffff;border-top-left-radius:5px;border-top-right-radius:5px;font-family:'arial';font-size:15px;line-height:19px;padding:16px 30px 16px 30px"">
                                        <p style=""margin:0"">Важно: если Вы нажмете на красную кнопку - Вы больше не сможете зарегистрироваться с помощью этой почты. Свяжитесь с администратором проекта, если Вы хотели бы присоедниться с помощью этой почты.</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=""background-color:#ffffff;border-bottom-left-radius:5px;border-bottom-right-radius:5px;font-family:'arial';font-size:16px;font-weight:700;line-height:19px;padding:8px 30px 8px 30px"">
                                        <table align=""left"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border:0;border-collapse:collapse;border-spacing:0""><tbody><tr><td style=""background:#ffffff;border-radius:4px"">
                                                    <a href=""" + UrlToBlockEmail + @""" style=""background:#FF6767;border:0px solid; border-radius:20px;color:#000000;display:block;font-family:'arial' , sans-serif;font-size:16px;line-height:16px;padding:12px 17px 12px 17px;text-align:center;text-decoration:none"">
                                                        Заблокировать Емаил
                                                    </a>
                                                </td></tr></tbody></table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </html>

            ";
            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }
    }
}