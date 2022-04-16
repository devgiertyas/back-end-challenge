using System.Security.Cryptography;
using System.Text;

namespace TodoAPI.Helpers
{
    public static class CriptoHelper
    {

        public static string GetMD5(string text)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return CreateHash(md5Hash, text);
            }
        }

        private static  string CreateHash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

    }
}
