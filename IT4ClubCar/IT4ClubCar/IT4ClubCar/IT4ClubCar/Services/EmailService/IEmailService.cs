using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.EmailService
{
    interface IEmailService
    {
        /// <summary>
        /// Envia uma mensagem para um email.
        /// </summary>
        /// <param name="emailDestino">Email para o qual enviar a mensagem.</param>
        /// <param name="mensagem">Mensagem a ser enviada.</param>
        Task EnviarEmail(string emailDestino,string assunto,string mensagemConteudo,AttachmentCollection attachments);

        /// <summary>
        /// Obtém o email do qual se vai enviar todas as mensagens.
        /// </summary>
        /// <returns>String com o email.</returns>
        Task<string> ObterEmailEnvio();

        /// <summary>
        /// Obtém a senha do email do qual se vai enviar as mensagens.
        /// </summary>
        /// <returns>String com a senha.</returns>
        Task<string> ObterSenhaEmailEnvio();
    }
}
