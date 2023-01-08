using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;

namespace Console.Facebook
{
    public class FacebookTest
    {
        public static void GetPosts()
        {
            string appAccessToken = string.Concat("578640529201132", "|", "2d951609385c66abc6e94126b1e94573");
            var fb = new FacebookClient("EAADpTHoXkTcBALEtnkEPaxY2v46iTsplO6W90CrglVSfIg3gyr6QPcaMp9tZC03LiZCZApKMyj7ZCRigQ02ZAQAhl9phIFPCkgLAL9BZB1VnwuJBH2aMFZCpcQk84BBty3ZACIiEZAglVpVQcjCtjHlUiPM6n7r8TCozDBywZA0UEPph61LLrIIyrOEzAOZBzZCm708ZD");
            dynamic result = fb.Get("me");
        }
    }
}
