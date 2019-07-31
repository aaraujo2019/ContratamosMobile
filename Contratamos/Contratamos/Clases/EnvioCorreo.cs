using System;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;

namespace Contratamos.Clases
{
    public class EnvioCorreo
    {
        public static string EnviarCorreo(string strParamAsunto, string strParamMensaje, string strParamDestinatarios)
        {
            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
            strParamDestinatarios = strParamDestinatarios.Replace(';', ',');
            string hostMail = "smtp.live.com";
            int puertoHostMail = 25;
            string credentialMailUser = "alvaro.araujo.arrieta@hotmail.com";
            string credentialMailPwd = "A1042418697";

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario
            //Asunto
            mmsg.To.Add(strParamDestinatarios);
            string strMensaje = "<HR SIZE=5 color=#0000ff NOSHADE> </BR><P><P><b>";
            strMensaje += strParamMensaje;
            strMensaje += "</b></BR><P><P><HR SIZE=5 color=#0000ff NOSHADE> <P><b>NOTA: </b>Favor no responder, este mensaje es generado automáticamente por contabilidad de la FECP.";

            mmsg.Subject = strParamAsunto;
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            //mmsg.Bcc.Add("alvaro.araujo.arrieta@hotmail.com"); //Opcional

            //Cuerpo del Mensaje
            mmsg.Body = strMensaje;
            mmsg.Priority = MailPriority.High;
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress(credentialMailUser);

            /*-------------------------CLIENTE DE CORREO----------------------*/
            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient(hostMail, puertoHostMail);
            cliente.Credentials = new System.Net.NetworkCredential(credentialMailUser, credentialMailPwd);
            cliente.EnableSsl = true;

            /*-------------------------ENVIO DE CORREO----------------------*/
            try
            {
                cliente.Send(mmsg);
                return "Mensajes enviados con exito.";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string EnviarCorreoConAdjunto(string strParamAsunto, string strParamMensaje, string strParamDestinatarios, string strParamArchivo)
        {
            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
            strParamDestinatarios = strParamDestinatarios.Replace(';', ',');
            string hostMail = "smtp.live.com";
            int puertoHostMail = 25;
            string credentialMailUser = "alvaro.araujo.arrieta@hotmail.com";
            string credentialMailPwd = "A1042418697";

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario
            //Asunto
            mmsg.To.Add(strParamDestinatarios);
            string strMensaje = "<HR SIZE=5 color=#0000ff NOSHADE> </BR><P><P><b>";
            strMensaje += strParamMensaje;
            strMensaje += "</b></BR><P><P><HR SIZE=5 color=#0000ff NOSHADE> <P><b>NOTA: </b>Favor no responder, este mensaje es generado automáticamente por contabilidad de la FECP.";

            mmsg.Subject = strParamAsunto;
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            //mmsg.Bcc.Add("alvaro.araujo.arrieta@hotmail.com"); //Opcional

            //Cuerpo del Mensaje
            mmsg.Body = strMensaje;
            mmsg.Priority = MailPriority.High;
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress(credentialMailUser);

            /*-------------------------CLIENTE DE CORREO----------------------*/
            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient(hostMail, puertoHostMail);
            cliente.Credentials = new System.Net.NetworkCredential(credentialMailUser, credentialMailPwd);
            cliente.EnableSsl = true;

            //PARA ENVIAR ARCHIVO ADJUNTO
            if (!string.IsNullOrEmpty(strParamArchivo))
            {
                //Create the file attachment for this e-mail message.
                using (Attachment data = new Attachment(strParamArchivo, MediaTypeNames.Application.Octet))
                {
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(strParamArchivo);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(strParamArchivo);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(strParamArchivo);
                    mmsg.Attachments.Add(data);
                    try
                    {
                        cliente.Send(mmsg);
                        return "Mensajes enviados con exito.";
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            else
            {
                try
                {
                    cliente.Send(mmsg);
                    return "Mensajes enviados con exito.";
                }
                catch (Exception)
                {
                    return "Ocurrio un problema al intentar enviar el mensaje, contactese con el administrador del sistema";
                }
            }
        }
    }
}
