using System.Security.Cryptography;
using System.Text;

namespace PruebaTecnica.Helpers
{
    public static class SHA256Helper
    {
        public static string GetSHA256(string rawData)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
