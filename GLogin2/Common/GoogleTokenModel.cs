namespace GLogin2.Common
{
    public class GoogleTokenModel
    {
        public string code { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string redirect_uri { get; set; }
        public string grant_type => "authorization_code";
    }
}