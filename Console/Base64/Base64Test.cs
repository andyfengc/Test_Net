using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Base64
{
    public class Base64Test
    {
        public static void TestBaseAndBaseUrl()
        {
            var base64 = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS/Xaw=";
            var base64Url = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw"; //  replace + and / with - and _, trim =

            // base64 -> base64
            var base64Bytes = Encoding.UTF8.GetBytes(base64);
            var base64Revert = Encoding.UTF8.GetString(base64Bytes);
            //base64Revert.Dump();

            // base64 -> str
            byte[] data = System.Convert.FromBase64String(base64);
            string decodedString = Encoding.UTF8.GetString(data);
            //decodedString.Dump();

            // base64 -> base64url	
            var base64UrlEncoded = base64.TrimEnd('=').Replace('+', '-').Replace('/', '_');
            //base64UrlEncoded.Dump();

            // base64url -> get back base64 string
            var base64Decoded = Base64UrlTextEncoder.Pad(base64Url.Replace('-', '+').Replace('_', '/'));
            //base64Decoded.Dump();
        }
    }

    public class Base64UrlTextEncoder : ITextEncoder
    {
        public string Encode(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            return System.Convert.ToBase64String(data).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        public byte[] Decode(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            return System.Convert.FromBase64String(Pad(text.Replace('-', '+').Replace('_', '/')));
        }

        public static string Pad(string text)
        {
            var padding = 3 - ((text.Length + 3) % 4);
            if (padding == 0)
            {
                return text;
            }
            return text + new string('=', padding);
        }
    }
}
