using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using Newtonsoft.Json.Linq;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Xamarin.Forms;
using IT4ClubCar.IT4ClubCar.Services.ScreenshotService;
using System.IO;
using Xamarin.Forms.Internals;

namespace IT4ClubCar.IT4ClubCar.Services.EmailService
{
    class EmailService : IEmailService
    {
        private WebService _webService;



        public EmailService(WebService webService)
        {
            _webService = webService;
        }


        /// <summary>
        /// Envia uma mensagem para um determinado email.
        /// </summary>
        /// <param name="emailDestino">Email para o qual enviar a mensagem.</param>
        /// <param name="mensagemConteudo">Mensagem a ser enviada.</param>
        public async Task EnviarEmail(string emailDestino, string assunto, string mensagemConteudo, AttachmentCollection attachments)
        {
            //Dados Acesso.
            string emailEnvio = await ObterEmailEnvio();
            string senha = await ObterSenhaEmailEnvio();
            
            //Criar Mensagem.
            MimeMessage mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("",emailEnvio));
            mensagem.To.Add(new MailboxAddress("", emailDestino));
            mensagem.Subject = assunto;

            byte[] imagemBytes = await DependencyService.Get<IScreenshotService>().TirarScreenshotAsync();

            BodyBuilder bodyBuilder = new BodyBuilder()
            {
                TextBody = mensagemConteudo
            };

            attachments.ForEach(p => bodyBuilder.Attachments.Add(p));

            mensagem.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587);
                await client.AuthenticateAsync(emailEnvio,senha);
                await client.SendAsync(mensagem);
                client.Disconnect(true);
            }
        }



        /// <summary>
        /// Obtém o email do qual se vai enviar todas as mensagens.
        /// </summary>
        /// <returns>String com o email.</returns>
        public async Task<string> ObterEmailEnvio()
        {
            string dataJson = await _webService.GetStringJson("GetEmailEnvio");

            JObject emailObject = JObject.Parse(dataJson);

            string email = emailObject["Email"].ToString();

            return email;
        }



        /// <summary>
        /// Obtém a senha do email do qual se vai enviar as mensagens.
        /// </summary>
        /// <returns>String com a senha.</returns>
        public async Task<string> ObterSenhaEmailEnvio()
        {
            string dataJson = await _webService.GetStringJson("GetEmailSenha");

            JObject senhaObject = JObject.Parse(dataJson);

            string senha = senhaObject["Senha"].ToString();

            return senha;
        }

    }
}
