using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Aplicacion.Helpers
{
    public static class ModificacionDatos
    {
        public static string DesencriptarString(string texto, string password)
        {
            byte[] baPwd = Encoding.UTF8.GetBytes(password);

            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baTexto = Convert.FromBase64String(texto);

            byte[] baDesencriptado = DesencriptarAes(baTexto, baPwdHash);

            int longitudSalt = ModificacionDatos.ObtenerLongitudSalt();
            byte[] baResultado = new byte[baDesencriptado.Length - longitudSalt];
            for (int i = 0; i < baResultado.Length; i++)
            {
                baResultado[i] = baDesencriptado[i + longitudSalt];
            }

            return Encoding.UTF8.GetString(baResultado);
        }

        private static byte[] DesencriptarAes(byte[] bytesASerDesencriptados, byte[] bytesPassword)
        {
            byte[] bytesDesencriptados;

            byte[] bytesSalt = { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(bytesPassword, bytesSalt, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesASerDesencriptados, 0, bytesASerDesencriptados.Length);
                        cs.Close();
                    }
                    bytesDesencriptados = ms.ToArray();
                }
            }

            return bytesDesencriptados;
        }

        public static string EncriptarStringRijndael(string valor, string llave)
        {
            UTF8Encoding codificador = new UTF8Encoding();
            Rijndael encriptador = Rijndael.Create();

            try
            {
                encriptador.Key = codificador.GetBytes(llave);

                byte[] encriptarTexto = codificador.GetBytes(valor);
                byte[] textoEncriptado = encriptador.CreateEncryptor().TransformFinalBlock(encriptarTexto, 0, encriptarTexto.Length);
                byte[] retorno = new byte[encriptador.IV.Length + textoEncriptado.Length];

                encriptador.IV.CopyTo(retorno, 0);
                textoEncriptado.CopyTo(retorno, encriptador.IV.Length);

                return Convert.ToBase64String(retorno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static int ObtenerLongitudSalt()
        {
            return 8;
        }
    }
}
