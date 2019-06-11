using Android.Content;
using Android.Preferences;
using System;
using System.IO;

namespace Contratamos.Clases
{
    public static class clsConfiguracion
    {
        #region Shared Preferences
        private static ISharedPreferences mSharedPrefs;
        private static ISharedPreferencesEditor mPrefsEditor;
        public static Context mContext;

        private static string KEY_EsDebug = "EnDebug";
        private static string KEY_ModoIntegracion = "Modointegracion";
        private static string KEY_RutaArchivoLog = "RutaArchivoLog";

        public static void Iniciar(Context context)
        {
            mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public static string EsDebug
        {
            get
            {
                return mSharedPrefs.GetString(KEY_EsDebug, "SI");
            }
            set
            {
                mPrefsEditor.PutString(KEY_EsDebug, value);
                mPrefsEditor.Commit();
            }
        }

        public static string ModoIntegracion
        {
            get
            {
                return mSharedPrefs.GetString(KEY_ModoIntegracion, "NO");
            }
            set
            {
                mPrefsEditor.PutString(KEY_ModoIntegracion, value);
                mPrefsEditor.Commit();
            }
        }

        public static string RutaArchivoLog
        {
            get
            {
                return mSharedPrefs.GetString(KEY_RutaArchivoLog, string.Empty);
            }
            set
            {
                mPrefsEditor.PutString(KEY_RutaArchivoLog, value);
                mPrefsEditor.Commit();
            }
        }

        public static string TipoEjecucion()
        {
            if (ModoIntegracion == "SI")
                return "Modo Integración";
            else return "Modo Aplicación";
        }
        #endregion

        #region Log App
        public static void Main(string mensaje, string procedimiento, string usuario)
        {
            string archivoLog = "LogBioFirma.txt";

            if (RutaArchivoLog == string.Empty)
                RutaArchivoLog = CreatePathToFile(archivoLog);

            if (EsDebug == "SI")
            {
                if (FileExists(RutaArchivoLog))
                    EscribirLog(RutaArchivoLog, mensaje, procedimiento, usuario);
                else CrearArchivoLog(mensaje, RutaArchivoLog, procedimiento, usuario);
            }
        }

        private static void CrearArchivoLog(string mensaje, string archivoLog, string procedimiento, string usuario)
        {
            using (StreamWriter sw = File.CreateText(archivoLog))
                Log(mensaje, sw, procedimiento, usuario);
        }

        private static void EscribirLog(string rutaArchivo, string mensaje, string procedimiento, string usuario)
        {
            using (StreamWriter writer = File.AppendText(rutaArchivo))
                Log(mensaje, writer, procedimiento, usuario);
        }

        static bool FileExists(string filename) => File.Exists(CreatePathToFile(filename));

        static string CreatePathToFile(string filename)
        {
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(docsPath, filename);
        }

        static void Log(string logMessage, TextWriter w, string procedimiento, string usuario)
        {
            w.Write("\r\n----------------  BioFirma Mobile 1.0.0 ----------------\n");
            w.WriteLine(Environment.NewLine);
            w.WriteLine("\r\n>Tipo de Ejecución Aplicación: {0}\n", TipoEjecucion());
            w.WriteLine("\r\n>{0}: {1}\n", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
            w.WriteLine("\r\n>Usuario: {0}\n", usuario);
            w.WriteLine("\r\n>Metodo Ejecutado: {0}\n", procedimiento);
            w.WriteLine("\r\n>Mensaje: {0}\n", logMessage);
            w.WriteLine("\r\n>Enviado desde: {0}\n");
            w.WriteLine("\r\n--------------------------------------------------------\n");
            w.WriteLine(Environment.NewLine);
        }

        public static void BorrarLogs()
        {
            try
            {
                File.WriteAllText(RutaArchivoLog, String.Empty);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
