using Android.Content;
using Contratamos.Models;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Contratamos.Generales
{
    public class modGeneral
    {
        public static string IDEstacion { set; get; }
        public static string usuario { set; get; }
        public static Usuarios clsUsuario { set; get; }

        public static string RutaArchivos { set; get; }
        public static string ArchivoGenerado { set; get; }
        public static string ArchivoOriginal { set; get; }
        public static string NombreAplicacion { set; get; }

        public static bool email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                    return true;
                else
                    return false;
            }
            else return false;
        }

        public static void EnviarEmail(Context context, string emailTo, string filePaths)
        {
            try
            {
                var file = new Java.IO.File(filePaths);
                var uri = Android.Net.Uri.FromFile(file);
                var email = new Intent(Intent.ActionSendMultiple);

                file.SetReadable(true, false);
                email.PutExtra(Intent.ExtraEmail, new string[] { emailTo });
                email.PutExtra(Intent.ExtraSubject, string.Concat("Log de operaciones BioFirma Mobile: ", DateTime.Now));
                email.PutExtra(Intent.ExtraStream, uri);
                email.SetType("plain/text");
                context.StartActivity(Intent.CreateChooser(email, "Enviando mail..."));
            }
            catch (Exception ex)
            { 
                App.Current.MainPage.DisplayAlert("BioFirma Mobile", "Ha ocurrido un problema al enviar el mail.", "Ok");
            }
        }

        public byte[] ConvertirDocBinary(string path)
        {
            try
            {
                byte[] buffer = null;
                int longitud;
                var PathfileName = string.Empty;
                PathfileName = path;
                using (var fs = new FileStream(PathfileName, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    longitud = (int)fs.Length;
                }
                return buffer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
