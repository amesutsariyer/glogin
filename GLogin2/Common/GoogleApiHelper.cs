using System.Text;

namespace GLogin2.Common
{
        public class GoogleApiHelper
        {
            public static string ApplicationName = "wc";
            public static string ClientId = "";
            public static string ClientSecret = "";
            public static string RedirectUri = "http://localhost:21955/Home/OauthCallback";
            public static string OauthUri = "https://accounts.google.com/o/oauth2/auth?";
            public static string TokenUri = "https://oauth2.googleapis.com/token";
            public static string Scopes = "https://www.googleapis.com/auth/userinfo.email";
          
            public static string GetOauthUri(string redirectUrl) 
            {
                StringBuilder sbUri = new StringBuilder(OauthUri);
                sbUri.Append("client_id=" + ClientId);
                sbUri.Append("&redirect_uri=" + RedirectUri);
                sbUri.Append("&response_type=" + "code");
                sbUri.Append("&scope=" + Scopes);
                sbUri.Append("&access_type=" + "offline");
                sbUri.Append("&state=" + redirectUrl);

                return sbUri.ToString();
            }
            
        }
}